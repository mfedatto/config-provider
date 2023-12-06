CREATE TABLE IF NOT EXISTS ValoresDatas (
    Id SERIAL PRIMARY KEY,
    IdChave INT,
    Valor DATE,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
