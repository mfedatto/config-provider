CREATE TABLE IF NOT EXISTS ValoresTextos (
    Id SERIAL PRIMARY KEY,
    IdChave INT,
    Valor TEXT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
