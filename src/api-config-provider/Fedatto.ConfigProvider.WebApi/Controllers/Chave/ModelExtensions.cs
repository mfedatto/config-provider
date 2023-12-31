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
            Aplicacao = chave.Aplicacao,
            Nome = chave.Nome,
            Tipo = chave.Tipo,
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

    public static IChave ToEntity(
        this ChaveFactory factory,
        PutChaveRequestModel requestModel,
        IAplicacao aplicacao,
        ITipo tipo)
    {
        return factory.Create(
            requestModel.Id,
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
            Aplicacao = chave.Aplicacao,
            Nome = chave.Nome,
            Tipo = chave.Tipo,
            Lista = chave.Lista,
            PermiteNulo = chave.PermiteNulo,
            IdChavePai = chave.IdChavePai,
            Habilitado = chave.Habilitado
        };
    }
    
    public static PutChaveResponseModel ToPutResponseModel(
        this IChave chave)
    {
        return new PutChaveResponseModel
        {
            Id = chave.Id,
            Aplicacao = chave.Aplicacao,
            Nome = chave.Nome,
            Tipo = chave.Tipo,
            Lista = chave.Lista,
            PermiteNulo = chave.PermiteNulo,
            IdChavePai = chave.IdChavePai,
            Habilitado = chave.Habilitado
        };
    }
}
