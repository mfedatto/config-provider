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

    public static IChave ToEntity(this ChaveFactory factory, PostChaveRequestModel requestModel)
    {
        return factory.Create(
            -1,
            requestModel.AppId,
            requestModel.Nome,
            requestModel.IdTipo,
            requestModel.Lista,
            requestModel.PermiteNulo,
            requestModel.IdChavePai,
            requestModel.Habilitado,
            requestModel.VigenteDe,
            requestModel.VigenteAte);
    }
    
    public static PostChaveResponseModel ToPostResponseModel(this IChave chave)
    {
        return new PostChaveResponseModel
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
