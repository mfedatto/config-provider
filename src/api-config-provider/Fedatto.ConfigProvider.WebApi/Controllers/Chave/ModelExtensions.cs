using Fedatto.ConfigProvider.Domain.Chave;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Chave;

public static class ModelExtensions
{
    public static GetChaveResponseModel ToGetResponseModel(this IChave chave)
    {
        return new GetChaveResponseModel
        {
            Id = chave.Id,
            AppId = chave.AppId,
            Nome = chave.Nome,
            IdTipo = chave.IdTipo,
            Lista = chave.Lista,
            PermiteNulo = chave.PermiteNulo,
            IdChavePai = chave.IdChavePai,
            Habilitado = chave.Habilitado
        };
    }
}
