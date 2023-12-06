# Migration

Diretório com os shell scripts para execução das atividades de migração (`migrate`, `repair`, `info` e `erase`) e os scripts SQL.

Fluxo de leitura e execução dos diretórios e controle de versão emudanças do Evolve-DB:

1. Todos os arquivos SQL dos diretórios informados serão considerados scripts de migração
2. O Evolve-DB percorre os diretórios encerra com erro ao encontrar arquivos SQL que não atendam os padrões de nomenclatura:
    - Script versionado com o sufixo `v` seguido de número de versão (maiores e menores) e um divisor `__` (dois `_`) para o nome do script.
    - Script repetitivo com o sufixo `r` e um divisor `__` (dois `_`) para o nome do script.
3. A ordem de execução dos scripts segue a ordem crescente de versões dos scripts versionados, seguida dos scripts repetitivos em ordem alfabética.
   - Em caso de colisão de versão o Evolve-DB encerra com erro
4. As versões enumeradas a partir dos scripts são confrontadas com as vesões executadas na tabela de changelog. Versões intermediárias não executadas na sequência correta serão consideradas violação do histórico e o Evolve-DB encerrará com erro.
5. Versões já executadas terão seu checksum comparados com o checksum dos arquivos encontrados, ao enconstrar diferença o Evolve-DB encerra com erro.
6. Os scripts repetitivos só serão executados se forem novos ou se tiverem alguma alteração em comparação com a execução anterior. Mudanças nos placeholders afetam o resultado final do script, provocando uma nova execução.
