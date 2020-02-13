using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using PontuaAe.Api.Seguranca;
using System.IO;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Quartz.Impl;
using Quartz;
using Quartz.Spi;
using PontuaAe.Api.GereciamentoJobsTask.Jobs;

using PontuaAe.Infra.Repositorios.RepositorioAvaliacao;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using PontuaAe.Infra.Repositorios.RepositorioFidelidade;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;

namespace PontuaAe.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6dRj1PVkH645024";
        private const string SECRET_KEY = "c1f51f42-5727-4d15-b787-NiDmHLpUa229sqsfhqGbMrDr1PV";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddCors();

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                ValidateAudience = true,
                ValidAudience = AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = ISSUER;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {          
             
                options.AddPolicy("Admin", policy => policy.RequireClaim("PontuaAe", "Admin"));
                options.AddPolicy("Funcionario", policy => policy.RequireClaim("PontuaAe", "Funcionario"));
                options.AddPolicy("Cliente", policy => policy.RequireClaim("PontuaAe", "Cliente"));


            });

            services.AddResponseCompression();

            services.AddScoped<PontuaAeDataContexto, PontuaAeDataContexto>();

            // Add Quartz services
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<QuartzJobRunner>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            // Add  job
            services.AddScoped<ClassificaTipoClienteJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(ClassificaTipoClienteJob),
                cronExpression: "0 52 18 ? * MON-TUE,WED-THU,FRI,SAT-SUN")); // run every 5 seconds
                                                                             // documentação: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html

            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IPontuacaoRepositorio, PontuacaoRepositorio>();
            services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            services.AddTransient<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddTransient<IPremioRepositorio, PremioRepositorio>();
            services.AddTransient<IReceitaRepositorio, ReceitaRepositorio>();
            services.AddTransient<IConfigPontosRepositorio, ConfigPontosRepositorio>();
            services.AddTransient<IAutomacaoMSGRepositorio, AutomacaoSMSReposiorio>();
            services.AddTransient<ICampanhaMSGRepositorio, CampanhaMSGRepositorio>();
            services.AddTransient<IContaSMSRepositorio, ContaSMSRepositorio>();
            services.AddTransient<IEnviarSMS, EnviarSMS>();
            services.AddTransient<IContaSMSRepositorio, ContaSMSRepositorio>();

            services.AddTransient<UsuarioManipulador, UsuarioManipulador>();
            services.AddTransient<PontuacaoManipulador, PontuacaoManipulador>();
            services.AddTransient<ClienteManipulador, ClienteManipulador>();          
            services.AddTransient<PremioManipulador, PremioManipulador>();
            services.AddTransient<EmpresaManipulador, EmpresaManipulador>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Projeto Pontuaae", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseCors(x =>
            {

                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Dados")),
                RequestPath = new PathString("/Dados")
            });

            app.UseMvc();
            app.UseResponseCompression();         

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pontuaae - V1");

            });
        }     
    }
}
