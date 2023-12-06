CREATE TABLE IF NOT EXISTS ValoresTextosPaginados (
    Id SERIAL PRIMARY KEY,
    IdValorText INT,
    Valor TEXT,
    Pagina INT,
    Habilitado BOOLEAN,
    VigenteDe DATE,
    VigenteAte DATE
);
