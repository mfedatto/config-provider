ALTER TABLE ValoresTextos
ADD CONSTRAINT uk_IdChave_Vigencia UNIQUE (IdChave, VigenteDe, VigenteAte);
