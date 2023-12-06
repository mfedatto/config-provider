-- Remove todos os dados existentes
TRUNCATE TABLE Tipos;

-- Inserção de dados
INSERT INTO Tipos (Id, Nome, Habilitado) VALUES
    (3, 'Número', true),
    (5, 'Texto', true),
    (7, 'Lógico', true),
    (11, 'Data', true),
    (13, 'Json', true),
    (17, 'Binário', false);
