CREATE TABLE Aplicacoes (
    AppId UUID PRIMARY KEY,
    Nome TEXT NOT NULL,
    Sigla TEXT NOT NULL,
    Aka TEXT,
    Habilitado BOOLEAN NOT NULL,
    VigenteDe DATE,
    VigenteAte DATE
);
