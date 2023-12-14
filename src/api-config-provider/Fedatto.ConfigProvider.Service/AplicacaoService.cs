using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;

namespace Fedatto.ConfigProvider.Service;

public class AplicacaoService : IAplicacaoService
{
    private readonly IUnitOfWork _uow;

    public AplicacaoService(
        IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<IAplicacao>> BuscarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null)
    {
        return await _uow.AplicacaoRepository.BuscarAplicacoes(
            cancellationToken,
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm);
    }
    
    public async Task<int> ContarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null)
    {
        return await _uow.AplicacaoRepository.ContarAplicacoes(
            cancellationToken,
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm);
    }
    
    public async Task IncluirAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        await _uow.AplicacaoRepository.BuscarAplicacaoPorId(
                cancellationToken,
                aplicacao.AppId)!
            .ThenThrowIfNull<IAplicacao, AppIdEmUsoException>();
        await _uow.AplicacaoRepository.BuscarAplicacaoPorNome(
                cancellationToken,
                aplicacao.Nome)!
            .ThenThrowIfNull<IAplicacao, NomeDeAplicacaoEmUsoException>();
        await _uow.AplicacaoRepository.BuscarAplicacaoPorSigla(
                cancellationToken,
                aplicacao.Sigla)!
            .ThenThrowIfNull<IAplicacao, SiglaDeAplicacaoEmUsoException>();
        
        await _uow.AplicacaoRepository.IncluirAplicacao(
            cancellationToken,
            aplicacao);
    }

    public async Task<IAplicacao> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        IAplicacao? result;
        
        try
        {
            result =  await _uow.AplicacaoRepository.BuscarAplicacaoPorId(
                cancellationToken,
                appId);
            
            if (result is null) throw new AplicacaoNaoEncontradaException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmaAplicacaoEncontradaException(ex);
        }

        return result;
    }
    
    public async Task AtualizarAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        await _uow.AplicacaoRepository.BuscarAplicacaoPorId(
                cancellationToken,
                aplicacao.AppId)!
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _uow.AplicacaoRepository.AtualizarAplicacao(
            cancellationToken,
            aplicacao);
    }
    
    public async Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId)
    {
        await _uow.AplicacaoRepository.BuscarAplicacaoPorId(
                cancellationToken,
                appId)!
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _uow.AplicacaoRepository.ExcluirAplicacao(
            cancellationToken,
            appId);
    }
}
