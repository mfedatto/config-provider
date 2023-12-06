CREATE TABLE IF NOT EXISTS ValoresBinariosBase64 (
    Id SERIAL PRIMARY KEY,
    IdChave INT,
    Valor TEXT,
    Checksum TEXT,
    NumeroDePaginas INT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
