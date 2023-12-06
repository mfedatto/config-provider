CREATE TABLE IF NOT EXISTS ValoresJsonPaginados (
    Id SERIAL PRIMARY KEY,
    IdValorJson INT,
    Valor TEXT,
    Pagina INT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
