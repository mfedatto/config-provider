using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Application;

public class ValorApplication : IValorApplication
{
    private readonly IValorService _service;

    public ValorApplication(
        IValorService service)
    {
        _service = service;
    }
    
    public async Task<IEnumerable<IValor<object>>> BuscarValores(
        Guid appId,
        int idChave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        IChave chave = await _service.BuscarChavePorId(
            appId,
            idChave,
            vigenteEm);

        return await _service.BuscarValores(
            chave,
            vigenteEm,
            habilitado);
    }

    public async Task<bool> AplicacaoExiste(Guid appId)
    {
        return await _service.AplicacaoExiste(appId);
    }

    public async Task<bool> ChaveExiste(
        Guid appId,
        int idChave,
        DateTime vigenteEm)
    {
        return (await _service.BuscarChavePorId(
            appId,
            idChave,
            vigenteEm)) is not null;
    }
}
