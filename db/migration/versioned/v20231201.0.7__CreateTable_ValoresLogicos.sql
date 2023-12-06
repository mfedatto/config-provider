CREATE TABLE IF NOT EXISTS ValoresLogicos (
    Id SERIAL PRIMARY KEY,
    IdChave INT,
    Valor BOOLEAN,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
