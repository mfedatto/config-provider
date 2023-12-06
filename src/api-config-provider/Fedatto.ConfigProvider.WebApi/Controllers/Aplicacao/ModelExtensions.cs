using Fedatto.ConfigProvider.Domain.Aplicacao;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Aplicacao;

public static class ModelExtensions
{
    public static GetAplicacaoResponseModel ToGetResponseModel(this IAplicacao aplicacao)
    {
        return new GetAplicacaoResponseModel
        {
            AppId = aplicacao.AppId,
            Nome = aplicacao.Nome,
            Sigla = aplicacao.Sigla,
            Aka = aplicacao.Aka,
            Habilitado = aplicacao.Habilitado,
            VigenteDe = aplicacao.VigenteDe,
            VigenteAte = aplicacao.VigenteAte
        };
    }
    
    public static IAplicacao ToEntity(this AplicacaoFactory factory, PostAplicacaoRequestModel requestModel)
    {
        return factory.Create(
            requestModel.AppId ?? Guid.NewGuid(),
            requestModel.Nome,
            requestModel.Sigla,
            requestModel.Aka,
            requestModel.Habilitado,
            requestModel.VigenteDe,
            requestModel.VigenteAte);
    }
    
    public static PostAplicacaoResponseModel ToPostResponseModel(this IAplicacao aplicacao)
    {
        return new PostAplicacaoResponseModel
        {
            AppId = aplicacao.AppId,
            Nome = aplicacao.Nome,
            Sigla = aplicacao.Sigla,
            Aka = aplicacao.Aka,
            Habilitado = aplicacao.Habilitado,
            VigenteDe = aplicacao.VigenteDe,
            VigenteAte = aplicacao.VigenteAte
        };
    }
    
    public static IAplicacao ToEntity(this AplicacaoFactory factory, PutAplicacaoRequestModel requestModel, Guid appId)
    {
        return factory.Create(
            appId,
            requestModel.Nome,
            requestModel.Sigla,
            requestModel.Aka,
            requestModel.Habilitado,
            requestModel.VigenteDe,
            requestModel.VigenteAte);
    }
}
