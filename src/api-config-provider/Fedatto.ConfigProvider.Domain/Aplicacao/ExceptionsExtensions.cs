using System.Threading.Channels;

namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public static class ExceptionsExtensions
{
    public static IAplicacao ThrowIfNull<T>(this IAplicacao aplicacao)
        where T : Exception, new()
    {
        if (aplicacao is null) throw new T();

        return aplicacao;
    }
}
