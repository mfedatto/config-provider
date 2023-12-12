using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Exceptions;

namespace Fedatto.ConfigProvider.Service;

public class AplicacaoService : IAplicacaoService
{
    private readonly IAplicacaoRepository _repository;

    public AplicacaoService(IAplicacaoRepository repository)
    {
        _repository = repository;
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
        return await _repository.BuscarAplicacoes(
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
        return await _repository.ContarAplicacoes(
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
        await _repository.BuscarAplicacaoPorId(
                cancellationToken,
                aplicacao.AppId)!
            .ThenThrowIfNull<IAplicacao, AppIdEmUsoException>();
        await _repository.BuscarAplicacaoPorNome(
                cancellationToken,
                aplicacao.Nome)!
            .ThenThrowIfNull<IAplicacao, NomeDeAplicacaoEmUsoException>();
        await _repository.BuscarAplicacaoPorSigla(
                cancellationToken,
                aplicacao.Sigla)!
            .ThenThrowIfNull<IAplicacao, SiglaDeAplicacaoEmUsoException>();
        
        await _repository.IncluirAplicacao(
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
            result =  await _repository.BuscarAplicacaoPorId(
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
        await _repository.BuscarAplicacaoPorId(
                cancellationToken,
                aplicacao.AppId)!
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _repository.AtualizarAplicacao(
            cancellationToken,
            aplicacao);
    }
    
    public async Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId)
    {
        await _repository.BuscarAplicacaoPorId(
                cancellationToken,
                appId)!
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _repository.ExcluirAplicacao(
            cancellationToken,
            appId);
    }
}
