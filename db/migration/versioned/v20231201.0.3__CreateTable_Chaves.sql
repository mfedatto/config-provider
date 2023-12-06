CREATE TABLE IF NOT EXISTS Chaves (
    Id SERIAL PRIMARY KEY,
    AppId UUID,
    Nome TEXT,
    IdTipo INT,
    Lista BOOLEAN,
    PermiteNulo BOOLEAN,
    IdChavePai INT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
