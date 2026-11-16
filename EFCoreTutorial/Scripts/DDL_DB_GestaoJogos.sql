
CREATE DATABASE DB_GestaoJogos;
GO

USE DB_GestaoJogos;
GO

-- 1. Tabela Jogador
CREATE TABLE Jogador (
    IdJogador INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    DataNascimento DATE NOT NULL
);
GO

-- 2. Tabela PerfilJogador (1:1)
CREATE TABLE PerfilJogador (
    IdPerfil INT IDENTITY(1,1) PRIMARY KEY,
    IdJogador INT NOT NULL UNIQUE,
    Nickname VARCHAR(50) NOT NULL UNIQUE,
    Plataforma VARCHAR(50) NULL,
    Biografia VARCHAR(300) NULL,

    CONSTRAINT FK_PerfilJogador_Jogador
        FOREIGN KEY (IdJogador)
        REFERENCES Jogador(IdJogador)
);
GO

-- 3. Tabela Desenvolvedora
CREATE TABLE Desenvolvedora (
    IdDesenvolvedora INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Pais VARCHAR(80) NOT NULL
);
GO

-- 4. Tabela Jogo
CREATE TABLE Jogo (
    IdJogo INT IDENTITY(1,1) PRIMARY KEY,
    IdDesenvolvedora INT NOT NULL,
    Nome VARCHAR(150) NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    AnoLancamento INT NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Jogo_Desenvolvedora
        FOREIGN KEY (IdDesenvolvedora)
        REFERENCES Desenvolvedora(IdDesenvolvedora),

    CONSTRAINT CK_Jogo_AnoLancamento
        CHECK (AnoLancamento BETWEEN 1970 AND 2100),

    CONSTRAINT CK_Jogo_Preco
        CHECK (Preco >= 0)
);
GO

-- 5. Tabela JogadorJogo (N:N)
CREATE TABLE JogadorJogo (
    IdJogadorJogo INT IDENTITY(1,1) PRIMARY KEY,
    IdJogador INT NOT NULL,
    IdJogo INT NOT NULL,
    DataAquisicao DATE NOT NULL DEFAULT GETDATE(),
    HorasJogadas DECIMAL(8,2) NOT NULL DEFAULT 0,
    Status VARCHAR(20) NOT NULL DEFAULT 'Na biblioteca',

    CONSTRAINT FK_JogadorJogo_Jogador
        FOREIGN KEY (IdJogador)
        REFERENCES Jogador(IdJogador),

    CONSTRAINT FK_JogadorJogo_Jogo
        FOREIGN KEY (IdJogo)
        REFERENCES Jogo(IdJogo),

    CONSTRAINT UQ_JogadorJogo
        UNIQUE (IdJogador, IdJogo),

    CONSTRAINT CK_JogadorJogo_Horas
        CHECK (HorasJogadas >= 0),

    CONSTRAINT CK_JogadorJogo_Status
        CHECK (Status IN (
            'Na biblioteca',
            'Jogando',
            'Concluido',
            'Abandonado'
        ))
);
GO
