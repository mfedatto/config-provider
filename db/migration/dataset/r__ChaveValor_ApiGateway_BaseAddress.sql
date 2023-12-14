WITH ins_chaves AS (
    INSERT INTO Chaves (AppId, Nome, IdTipo, Lista, PermiteNulo, IdChavePai, Habilitado, VigenteDe, VigenteAte)
    VALUES (
        '54a429b1-e433-4e22-8537-b4c2fc5217de',
        'base_address',
        5,
        false,
        false,
        null,
        true,
        '2023-12-01'::DATE,
        '2024-12-01'::DATE
    )
    ON CONFLICT (AppId, Nome, VigenteDe, VigenteAte)
    DO UPDATE SET
        IdTipo = EXCLUDED.IdTipo,
        Lista = EXCLUDED.Lista,
        PermiteNulo = EXCLUDED.PermiteNulo,
        IdChavePai = EXCLUDED.IdChavePai,
        Habilitado = EXCLUDED.Habilitado
    RETURNING Id
)

INSERT INTO ValoresTextos (IdChave, Valor, Habilitado, VigenteDe, VigenteAte)
SELECT Id, 'http://127.0.0.1:5045', true, '2023-12-01'::DATE, '2024-12-01'::DATE
FROM ins_chaves
ON CONFLICT (IdChave, VigenteDe, VigenteAte)
DO UPDATE SET
    Valor = EXCLUDED.Valor,
    Habilitado = EXCLUDED.Habilitado;
