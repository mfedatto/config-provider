using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Chave;

public static class ModelExtensions
{
    public static GetChaveResponseModel ToGetResponseModel(
        this IChave chave)
    {
        return new GetChaveResponseModel
        {
            Id = chave.Id,
            AppId = chave.Aplicacao.AppId,
            Nome = chave.Nome,
            IdTipo = chave.Tipo.Id,
            Lista = chave.Lista,
            PermiteNulo = chave.PermiteNulo,
            IdChavePai = chave.IdChavePai,
            Habilitado = chave.Habilitado
        };
    }

    public static IChave ToEntity(
        this ChaveFactory factory,
        PostChaveRequestModel requestModel,
        IAplicacao aplicacao,
        ITipo tipo)
    {
        return factory.Create(
            -1,
            aplicacao,
            requestModel.Nome,
            tipo,
            requestModel.Lista,
            requestModel.PermiteNulo,
            requestModel.IdChavePai,
            requestModel.Habilitado,
            requestModel.VigenteDe,
            requestModel.VigenteAte);
    }
    
    public static PostChaveResponseModel ToPostResponseModel(
        this IChave chave)
    {
        return new PostChaveResponseModel
        {
            Id = chave.Id,
            AppId = chave.Aplicacao.AppId,
            Nome = chave.Nome,
            IdTipo = chave.Tipo.Id,
            Lista = chave.Lista,
            PermiteNulo = chave.PermiteNulo,
            IdChavePai = chave.IdChavePai,
            Habilitado = chave.Habilitado
        };
    }
}
