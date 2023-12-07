using Fedatto.ConfigProvider.Domain.Valor;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Valor;

public static class ModelExtensions
{
    public static GetValorResponseModel<T> ToGetResponseModel<T>(this IValor<T> valor)
    {
        return new GetValorResponseModel<T>
        {
            Id = valor.Id,
            IdChave = valor.IdChave,
            Valor = valor.Valor,
            Habilitado = valor.Habilitado,
            VigenteDe = valor.VigenteDe,
            VigenteAte = valor.VigenteAte
        };
    }
}
