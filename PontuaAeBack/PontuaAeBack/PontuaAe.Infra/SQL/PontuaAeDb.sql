CREATE DATABASE PONTUAE
go

USE PONTUAE

GO
CREATE TABLE USUARIO(
Id int IDENTITY(1,1) PRIMARY KEY,
NomeCompleto varchar(30),
Email varchar(50) ,
Senha varchar(30) ,
ClaimType varchar(25),
ClaimValue varchar(15),
Estado varchar(5)

);

CREATE TABLE EMPRESA(
Id int IDENTITY(1,1) PRIMARY KEY ,
NomeFantasia varchar(100),
Descricao varchar(300),
NomeResponsavel varchar(50),
Telefone varchar(15),
Email varchar(50),
Documento varchar(18),
Seguimento varchar(25),
Horario varchar(35),
Facebook varchar(200),
WebSite varchar(60),
Instagram varchar(140),
Delivery varchar(30),
IdUsuario int FOREIGN KEY REFERENCES USUARIO(Id),
Bairro varchar(30),
Rua varchar(30),
Numero varchar(30),
Cep varchar(10),
Cidade varchar(28),
Estado varchar(50),
Complemento varchar(70),
Banner varchar(200), 
Logo varchar(200),

);

CREATE TABLE CONFIG_PONTUACAO(
Id int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
Reais decimal(20,2),
PontosFidelidade decimal(20,2),
ValidadePontos int,
);

CREATE TABLE CONTA_SMS(
Id int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
Saldo int,
);

CREATE TABLE CLIENTE(
Id int IDENTITY(1,1) PRIMARY KEY, 
IdUsuario int FOREIGN KEY REFERENCES USUARIO(Id),
Nome varchar(55),
DataNascimeto DateTime,
Sexo varchar,
Contato varchar(15),
Email varchar(50),
Cpf varchar(14),
Segmentacao varchar(6),
Estado bit,
SegCustomizado varchar(8)
);   

CREATE TABLE PONTUACAO(
Id int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
IdCliente int FOREIGN KEY REFERENCES CLIENTE(Id),
ValorGasto decimal,
Saldo decimal(20,2),
SaldoTransacao decimal(20,2),
DataVisita DateTime
);

CREATE TABLE PREMIOS(
Id int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
Nome varchar(40),
Descricao varchar(250),
Quantidade int,
Imagem varchar(200),
Validade datetime,
PontosNecessario decimal(20,2),

);

CREATE TABLE OFERTAS(
Id int IDENTITY(1,1) PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
Nome varchar(30),
Descricao varchar(300),
Imagem varchar(200),
Validade varchar(19),
);

CREATE TABLE RECEITA(
Id int IDENTITY(1,1) PRIMARY KEY,
IdUsuario int FOREIGN KEY REFERENCES USUARIO(Id),
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
IdCliente int FOREIGN KEY REFERENCES CLIENTE(Id),
Valor decimal,
DataVenda datetime,
TipoAtividade varchar(20),  --aqui vai se registrado se o atendimento foi feito cadastro, pontuação ou resgate, Ativação do cliente,
);

CREATE TABLE MENSAGEM(
Id int IDENTITY(1,1) ,
IdCampanha int PRIMARY KEY,
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
TipoCanal int,   -- 1 sms, 2 push-notification
Nome varchar(40),
CampanhaAutomatica varchar(30),
Segmentacao varchar(30),
segCustomizado varchar(10),
MeioComunicacao int,
DiasAntesAniversario int,  --usado em aniversário e Apos completa o cartão e apos ultima fidelização
DiaSemana varchar(15),
DiaMes int,
DataAgendada datetime,
HoraEnvio Time, 
Conteudo varchar(180),
QtdSelecionado int, 
ValorInvestido decimal,
QtdEnviado int,
EstadoEnvio varchar(30), --Enviada, agendada ou concluido  "Automatico"   quando a agenda for enviada muda o satus para concluido
Estado bit,--ativa e desativa automacao


);

CREATE TABLE SITUACAO_SMS(
Id int IDENTITY(1,1) PRIMARY KEY,
IdMensagem int FOREIGN KEY REFERENCES MENSAGEM(IdCampanha),
IdEmpresa int FOREIGN KEY REFERENCES EMPRESA(Id),
Estado varchar(10),
Verificado varchar(10),
DataRecebida Datetime, 
DataCompra  DateTime,
TotalVendas decimal,
Contatos varchar(11),
);
