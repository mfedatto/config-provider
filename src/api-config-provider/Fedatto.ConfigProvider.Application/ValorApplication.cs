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
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        return await _service.BuscarValores(
            chave,
            vigenteEm,
            habilitado);
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(Guid appId)
    {
        return await _service.BuscarAplicacaoPorId(appId);
    }

    public async Task<IChave?> ObterChavePorId(
        IAplicacao aplicacao,
        int idChave)
    {
        return await _service.BuscarChavePorId(
            aplicacao,
            idChave);
    }
}
