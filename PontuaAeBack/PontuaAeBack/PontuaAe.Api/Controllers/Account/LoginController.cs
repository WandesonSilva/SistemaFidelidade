using System;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PontuaAe.Api.Seguranca;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;

namespace PontuaAe.Api.Controllers.Account
{
    public class LoginController : Controller
    {
        //private readonly UsuarioManipulador _manipulador;

        private Usuario _usuario;
        private ObterIDConsulta _IdEmpresa;
        private readonly IUsuarioRepositorio _repository;
        private readonly IEmpresaRepositorio _empresaRepository;
        private readonly IClienteRepositorio _clienteRepository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        public LoginController(IOptions<TokenOptions> jwtOptions, IUsuarioRepositorio repository, IEmpresaRepositorio empresaRepository, IClienteRepositorio clienteRepository)
        {

            // _manipulador = manipulador;
            _repository = repository;
            _clienteRepository = clienteRepository;
            _empresaRepository = empresaRepository;
            _tokenOptions = jwtOptions.Value;

            //ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AutenticarUsuarioComando comando)
        {

            var identity = await GetClaims(comando);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, comando.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("PontuaAe")
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                users = new
                {
                    id = _usuario.ID,
                    email = comando.Email,
                    claimValue =  _usuario.ClaimValue,                 

                }
            };


            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        //COPIA E COLA
        private static void SeInvalidaOpcao(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        private Task<ClaimsIdentity> GetClaims(AutenticarUsuarioComando comando)
        {
            var usuario = _repository.OterUsuario(comando.Email);


            if (usuario.ClaimValue == "Cliente")
            {
                ObterIDConsulta IdEmpresa = _clienteRepository.ObterID(usuario.ID);
                if (_repository.Autenticar(comando.Email, comando.Senha))
                    _usuario = usuario;
                _IdEmpresa = IdEmpresa;

                return Task.FromResult(new ClaimsIdentity(
               new GenericIdentity(IdEmpresa.ID.ToString(), "Token"),
               new[] {
                    new Claim(usuario.ClaimType, usuario.ClaimValue),

               }));

            }
            if (usuario.ClaimValue == "Funcionario")
            {
                ObterIDConsulta IDFuncionario = _empresaRepository.ObterIdEmpresa(usuario.ID);

                if (_repository.Autenticar(comando.Email, comando.Senha))
                    _usuario = usuario;
                _IdEmpresa = IDFuncionario;

                return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(IDFuncionario.ID.ToString(), "Token"),
                new[] {
                    new Claim(usuario.ClaimType, usuario.ClaimValue),

                }));


            }


            ObterIDConsulta ID = _empresaRepository.ObterIdEmpresa(usuario.ID);

            if (_repository.Autenticar(comando.Email, comando.Senha))
                _usuario = usuario;
            _IdEmpresa = ID;

            return Task.FromResult(new ClaimsIdentity(
            new GenericIdentity(ID.ID.ToString(), "Token"),
            new[] {
                    new Claim(usuario.ClaimType, usuario.ClaimValue),

            }));

        }
    }
}

