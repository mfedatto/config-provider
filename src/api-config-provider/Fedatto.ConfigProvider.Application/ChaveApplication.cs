using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Application;

public class ChaveApplication : IChaveApplication
{
    private readonly IChaveService _service;
    private readonly IAplicacaoService _aplicacaoService;

    public ChaveApplication(
        IChaveService service,
        IAplicacaoService aplicacaoService)
    {
        _service = service;
        _aplicacaoService = aplicacaoService;
    }
    
    public async Task<PagedListWrapper<IChave>> BuscarChaves(
        Guid appId,
        DateTime vigenteEm,
        string? nome = null,
        int? idTipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null)
    {
        int total = await _service.ContarChaves(
            appId,
            vigenteEm,
            nome,
            idTipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado);

        if (0.Equals(total)) return Enumerable.Empty<IChave>().WrapUp();
        
        return (await _service.BuscarChaves(
                appId,
                vigenteEm,
                nome,
                idTipo,
                lista,
                permiteNulo,
                idChavePai,
                habilitado,
                skip,
                limit))
            .WrapUp(skip ?? 0, limit, total);
    }

    public async Task<bool> AplicacaoExiste(
        Guid appId)
    {
        return (await _aplicacaoService.BuscarAplicacaoPorId(appId)) is not null;
    }

    public async Task<IChave> BuscarChavePorId(
        Guid appId,
        int id,
        DateTime vigenteEm)
    {
        return await _service.BuscarChavePorId(
            appId,
            id,
            vigenteEm);
    }

    public async Task<IChave> IncluirChave(
        IChave chave)
    {
        return await _service.IncluirChave(chave);
    }
}
