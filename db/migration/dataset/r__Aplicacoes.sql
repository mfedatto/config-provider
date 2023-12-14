INSERT INTO Aplicacoes (AppId, Nome, Sigla, Aka, Habilitado, VigenteDe, VigenteAte)
VALUES 
    ('54a429b1-e433-4e22-8537-b4c2fc5217de', 'API Gateway', 'AG', 'Gateway', true, '2023-12-01', '2024-12-01')
ON CONFLICT (AppId)
DO UPDATE SET
    Nome = EXCLUDED.Nome,
    Sigla = EXCLUDED.Sigla,
    Aka = EXCLUDED.Aka,
    Habilitado = EXCLUDED.Habilitado,
    VigenteDe = EXCLUDED.VigenteDe,
    VigenteAte = EXCLUDED.VigenteAte;

INSERT INTO Aplicacoes (AppId, Nome, Sigla, Aka, Habilitado, VigenteDe, VigenteAte)
VALUES 
    ('531d63e3-a6ac-43ad-8928-c346a46c91c7', 'BFF Portal do Usuário', 'BPDU', 'BFF Portal', true, '2023-12-01', '2024-12-01')
ON CONFLICT (AppId)
DO UPDATE SET
    Nome = EXCLUDED.Nome,
    Sigla = EXCLUDED.Sigla,
    Aka = EXCLUDED.Aka,
    Habilitado = EXCLUDED.Habilitado,
    VigenteDe = EXCLUDED.VigenteDe,
    VigenteAte = EXCLUDED.VigenteAte;

INSERT INTO Aplicacoes (AppId, Nome, Sigla, Aka, Habilitado, VigenteDe, VigenteAte)
VALUES 
    ('08a63961-1237-4b9c-86c8-05e6c7545339', 'Sistema Integrado de Operação', 'SIO', 'Monolito', true, '2023-12-01', '2024-12-01')
ON CONFLICT (AppId)
DO UPDATE SET
    Nome = EXCLUDED.Nome,
    Sigla = EXCLUDED.Sigla,
    Aka = EXCLUDED.Aka,
    Habilitado = EXCLUDED.Habilitado,
    VigenteDe = EXCLUDED.VigenteDe,
    VigenteAte = EXCLUDED.VigenteAte;
