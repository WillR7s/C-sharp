-- ============================================
-- 1. CADASTRO DOS JOGADORES
-- Insere os dados pessoais dos jogadores.
-- ============================================

INSERT INTO Jogador (Nome, Email, DataNascimento)
VALUES
('Ana Silva', 'ana@email.com', '2000-05-15'),
('Bruno Santos', 'bruno@email.com', '1998-08-22'),
('Carla Oliveira', 'carla@email.com', '2002-01-10'),
('Daniel Costa', 'daniel@email.com', '1999-11-30'),
('Eduarda Lima', 'eduarda@email.com', '2001-03-18'),
('Felipe Martins', 'felipe@email.com', '1997-07-05'),
('Gabriela Souza', 'gabriela@email.com', '2003-09-12'),
('Henrique Alves', 'henrique@email.com', '2000-12-01');
GO


-- ============================================
-- 2. CADASTRO DOS PERFIS DOS JOGADORES
-- Armazena nickname, plataforma e biografia.
-- ============================================

INSERT INTO PerfilJogador
    (IdJogador, Nickname, Plataforma, Biografia)
VALUES
(1, 'AnaGamer', 'PC', 'Fã de jogos de aventura.'),
(2, 'BrunoXP', 'PlayStation', 'Jogador competitivo.'),
(3, 'CarlaPlay', 'Nintendo Switch', 'Apaixonada por RPG.'),
(5, 'EduGames', 'PC', 'Interessada em jogos indie.'),
(7, 'GabiPlayer', 'Xbox', 'Fã de jogos de ação.');
GO


-- ============================================
-- 3. CADASTRO DAS DESENVOLVEDORAS
-- Registra o nome e o país de cada empresa.
-- ============================================

INSERT INTO Desenvolvedora (Nome, Pais)
VALUES
('Nintendo', 'Japão'),
('Rockstar Games', 'Estados Unidos'),
('CD Projekt Red', 'Polônia'),
('FromSoftware', 'Japão'),
('Mojang Studios', 'Suécia'),
('Valve', 'Estados Unidos'),
('Supergiant Games', 'Estados Unidos');
GO


-- ============================================
-- 4. CADASTRO DOS JOGOS
-- Registra os jogos e suas desenvolvedoras.
-- Também armazena gênero, ano e preço.
-- ============================================

INSERT INTO Jogo
    (IdDesenvolvedora, Nome, Genero, AnoLancamento, Preco)
VALUES

-- Jogos da Nintendo
(1, 'The Legend of Zelda: Breath of the Wild', 'Aventura', 2017, 299.90),
(1, 'Super Mario Odyssey', 'Plataforma', 2017, 249.90),
(1, 'Mario Kart 8 Deluxe', 'Corrida', 2017, 299.90),

-- Jogos da Rockstar Games
(2, 'Grand Theft Auto V', 'Ação', 2013, 149.90),
(2, 'Red Dead Redemption 2', 'Aventura', 2018, 199.90),

-- Jogos da CD Projekt Red
(3, 'The Witcher 3', 'RPG', 2015, 129.90),
(3, 'Cyberpunk 2077', 'RPG', 2020, 179.90),

-- Jogos da FromSoftware
(4, 'Elden Ring', 'RPG', 2022, 249.90),
(4, 'Dark Souls III', 'RPG', 2016, 159.90),

-- Jogos da Mojang Studios
(5, 'Minecraft', 'Sandbox', 2011, 99.90),

-- Jogos da Valve
(6, 'Portal 2', 'Puzzle', 2011, 39.90),

-- Jogos da Supergiant Games
(7, 'Hades', 'Roguelike', 2020, 73.90);
GO


-- ============================================
-- 5. CADASTRO DOS JOGOS DOS JOGADORES
-- Registra quais jogos cada jogador possui,
-- a data de aquisição, horas jogadas e status.
-- ============================================

INSERT INTO JogadorJogo
    (IdJogador, IdJogo, DataAquisicao, HorasJogadas, Status)
VALUES

-- Jogos da Ana
(1, 1, '2026-01-10', 85.5, 'Concluido'),
(1, 3, '2026-02-15', 32.0, 'Jogando'),
(1, 6, '2026-03-01', 12.5, 'Na biblioteca'),

-- Jogos do Bruno
(2, 4, '2026-01-12', 120.0, 'Jogando'),
(2, 8, '2026-02-20', 95.0, 'Jogando'),
(2, 9, '2026-03-05', 45.0, 'Abandonado'),

-- Jogos da Carla
(3, 1, '2026-01-15', 60.0, 'Concluido'),
(3, 2, '2026-02-10', 40.0, 'Jogando'),
(3, 12, '2026-03-05', 18.0, 'Jogando'),

-- Jogos do Daniel
(4, 4, '2026-01-20', 75.0, 'Jogando'),
(4, 5, '2026-02-25', 30.0, 'Na biblioteca'),

-- Jogos da Eduarda
(5, 6, '2026-02-01', 110.0, 'Concluido'),
(5, 7, '2026-03-10', 25.0, 'Jogando'),

-- Jogos do Felipe
(6, 10, '2026-02-05', 200.0, 'Jogando'),
(6, 11, '2026-03-15', 15.0, 'Concluido'),

-- Jogos da Gabriela
(7, 3, '2026-02-10', 55.0, 'Concluido'),
(7, 8, '2026-03-20', 40.0, 'Jogando');
GO


-- ============================================
-- 6. CADASTRO DE UMA NOVA DESENVOLVEDORA
-- Adiciona a empresa Team Cherry, da Austrália, sem Jogos. 
-- Assim, será possível explorar consultas com LEFT JOIN 
-- e identificar desenvolvedoras que ainda não possuem jogos cadastrados.
-- ============================================

INSERT INTO Desenvolvedora (Nome, Pais)
VALUES
('Team Cherry', 'Austrália');
GO