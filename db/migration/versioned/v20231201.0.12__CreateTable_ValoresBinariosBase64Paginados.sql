CREATE TABLE IF NOT EXISTS ValoresBinariosBase64Paginados (
    Id SERIAL PRIMARY KEY,
    IdValorBinarioBase64 INT,
    Valor TEXT,
    Pagina INT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
