ALTER TABLE Chaves
ADD CONSTRAINT uk_AppId_Nome_Vigencia UNIQUE (AppId, Nome, VigenteDe, VigenteAte);
