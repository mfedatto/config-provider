using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Application;

public class ChaveApplication : IChaveApplication
{
    private readonly IChaveService _service;
    private readonly IAplicacaoService _aplicacaoService;
    private readonly ITipoService _tipoService;

    public ChaveApplication(
        IChaveService service,
        IAplicacaoService aplicacaoService,
        ITipoService tipoService)
    {
        _service = service;
        _aplicacaoService = aplicacaoService;
        _tipoService = tipoService;
    }
    
    public async Task<PagedListWrapper<IChave>> BuscarChaves(
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null)
    {
        int total = await _service.ContarChaves(
            aplicacao,
            vigenteEm,
            nome,
            tipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado);

        if (0.Equals(total)) return Enumerable.Empty<IChave>().WrapUp();
        
        return (await _service.BuscarChaves(
                aplicacao,
                vigenteEm,
                nome,
                tipo,
                lista,
                permiteNulo,
                idChavePai,
                habilitado,
                skip,
                limit))
            .WrapUp(skip ?? 0, limit, total);
    }

    public async Task<IAplicacao> BuscarAplicacaoPorId(
        Guid appId)
    {
        return await _aplicacaoService.BuscarAplicacaoPorId(appId);
    }

    public async Task<ITipo> BuscarTipoPorId(
        int id)
    {
        return await _tipoService.BuscarTipoPorId(id);
    }

    public async Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int id,
        DateTime vigenteEm)
    {
        return await _service.BuscarChavePorId(
            aplicacao,
            id,
            vigenteEm);
    }

    public async Task<IChave> IncluirChave(
        IChave chave)
    {
        return await _service.IncluirChave(chave);
    }
}
