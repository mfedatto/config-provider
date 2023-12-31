using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;

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
        CancellationToken cancellationToken,
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
        cancellationToken.ThrowIfClientClosedRequest();
        
        int total = await _service.ContarChaves(
            cancellationToken,
            aplicacao,
            vigenteEm,
            nome,
            tipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado);

        if (0.Equals(total)) return Enumerable.Empty<IChave>().WrapUp();
        
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _service.BuscarChaves(
                cancellationToken,
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
        CancellationToken cancellationToken,
        Guid appId)
    {        
        return await _aplicacaoService.BuscarAplicacaoPorId(
            cancellationToken,
            appId);
    }

    public async Task<ITipo> BuscarTipoPorId(
        CancellationToken cancellationToken,
        int id)
    {
        return await _tipoService.BuscarTipoPorId(
            cancellationToken,
            id);
    }

    public async Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int id)
    {
        return await _service.BuscarChavePorId(
            cancellationToken,
            aplicacao,
            id);
    }

    public async Task<IChave> IncluirChave(
        CancellationToken cancellationToken,
        IChave chave)
    {
        return await _service.IncluirChave(
            cancellationToken,
            chave);
    }

    public async Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar)
    {
        return await _service.AlterarChave(
            cancellationToken,
            chaveAAlterar);
    }

    public async Task ExcluirChave(
        CancellationToken cancellationToken,
        int idChave)
    {
        await _service.ExcluirChave(
            cancellationToken,
            idChave);
    }
}
