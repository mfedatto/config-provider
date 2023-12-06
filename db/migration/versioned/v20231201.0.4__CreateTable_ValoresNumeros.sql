CREATE TABLE IF NOT EXISTS ValoresNumeros (
    Id SERIAL PRIMARY KEY,
    IdChave INT,
    Valor NUMERIC,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
