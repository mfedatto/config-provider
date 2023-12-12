using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Valor;

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
        CancellationToken cancellationToken,
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        return await _service.BuscarValores(
            cancellationToken,
            chave,
            vigenteEm,
            habilitado);
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        return await _service.BuscarAplicacaoPorId(
            cancellationToken,
            appId);
    }

    public async Task<IChave?> ObterChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int idChave)
    {
        return await _service.BuscarChavePorId(
            cancellationToken,
            aplicacao,
            idChave);
    }
}
