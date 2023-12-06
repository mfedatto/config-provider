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
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null)
    {
        return await _repository.BuscarAplicacoes(
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm);
    }
    
    public async Task<int> ContarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null)
    {
        return await _repository.ContarAplicacoes(
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm);
    }
    
    public async Task IncluirAplicacao(IAplicacao aplicacao)
    {
        if ((await _repository.BuscarAplicacaoPorId(aplicacao.AppId)) is not null) throw new AppIdEmUsoException();
        if ((await _repository.BuscarAplicacaoPorNome(aplicacao.Nome)) is not null) throw new NomeDeAplicacaoEmUsoException();
        if ((await _repository.BuscarAplicacaoPorSigla(aplicacao.Sigla)) is not null) throw new SiglaDeAplicacaoEmUsoException();
        
        await _repository.IncluirAplicacao(aplicacao);
    }

    public async Task<IAplicacao> BuscarAplicacaoPorId(Guid appId)
    {
        IAplicacao? result;
        
        try
        {
            result =  await _repository.BuscarAplicacaoPorId(appId);
            
            if (result is null) throw new AplicacaoNaoEncontradaException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmaAplicacaoEncontradaException(ex);
        }

        return result;
    }
    
    public async Task AtualizarAplicacao(IAplicacao aplicacao)
    {
        if ((await _repository.BuscarAplicacaoPorId(aplicacao.AppId)) is null)
            throw new AplicacaoNaoEncontradaException();

        await _repository.AtualizarAplicacao(aplicacao);
    }
    
    public async Task ExcluirAplicacao(Guid appId)
    {
        if ((await _repository.BuscarAplicacaoPorId(appId)) is null)
            throw new AplicacaoNaoEncontradaException();

        await _repository.ExcluirAplicacao(appId);
    }
}
