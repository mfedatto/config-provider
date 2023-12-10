namespace Fedatto.ConfigProvider.WebApi.Constants;

public static class Rotas
{
    public const string Aplicacoes = "aplicacoes";
    public const string AplicacoesGetAplicacoes = "";
    public const string AplicacoesPostAplicacao = "";
    public const string AplicacoesGetAplicacao = $"{{{ArgumentosNomeados.AppId}}}";
    public const string AplicacoesPutAplicacao = $"{{{ArgumentosNomeados.AppId}}}";
    public const string AplicacoesDeleteAplicacao = $"{{{ArgumentosNomeados.AppId}}}";
    public const string AplicacoesHeadAplicacao = $"{{{ArgumentosNomeados.AppId}}}";
    public const string Tipos = "tipos";
    public const string TiposGetTipos = "";
    public const string TiposGetTipo = $"{{{ArgumentosNomeados.IdTipo}}}";
    public const string TiposHeadTipo = $"{{{ArgumentosNomeados.IdTipo}}}";
    public const string Chaves = $"{Aplicacoes}/{{{ArgumentosNomeados.AppId}}}/chaves";
    public const string ChavesGetChaves = "";
    public const string ChavesGetChave = $"{{{ArgumentosNomeados.IdChave}}}";
    public const string ChavesPostChave = $"";
    public const string ChavesPutChave = $"{{{ArgumentosNomeados.IdChave}}}";
    public const string ChavesDeleteChave = $"{{{ArgumentosNomeados.IdChave}}}";
    public const string Valores = $"{Aplicacoes}/{{{ArgumentosNomeados.AppId}}}/chaves/{{{ArgumentosNomeados.IdChave}}}/valores";
    public const string ValoresGetValores = "";
}
