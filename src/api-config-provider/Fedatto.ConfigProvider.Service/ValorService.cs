using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Service;

public class ValorService : IValorService
{
    private readonly IValorRepository _repository;
    private readonly IAplicacaoRepository _aplicacaoRepository;
    private readonly IChaveRepository _chaveRepository;

    public ValorService(
        IValorRepository repository,
        IAplicacaoRepository aplicacaoRepository,
        IChaveRepository chaveRepository)
    {
        _repository = repository;
        _aplicacaoRepository = aplicacaoRepository;
        _chaveRepository = chaveRepository;
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValores(
        CancellationToken cancellationToken,
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        return chave.Tipo.Id switch
        {
            3 => await _repository.BuscarValoresDouble(cancellationToken, chave.Id, vigenteEm, habilitado),
            5 => await _repository.BuscarValoresString(cancellationToken, chave.Id, vigenteEm, habilitado),
            7 => await _repository.BuscarValoresBool(cancellationToken, chave.Id, vigenteEm, habilitado),
            11 => await _repository.BuscarValoresDatas(cancellationToken, chave.Id, vigenteEm, habilitado),
            13 => await _repository.BuscarValoresJson(cancellationToken, chave.Id, vigenteEm, habilitado),
            17 => await _repository.BuscarValoresBinaryB64(cancellationToken, chave.Id, vigenteEm, habilitado),
            _ => throw new Http501NaoImplementadoException()
        };
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        return await _aplicacaoRepository.BuscarAplicacaoPorId(
            cancellationToken,
            appId);
    }

    public async Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int idChave)
    {
        return await _chaveRepository.BuscarChavePorId(
            cancellationToken,
            aplicacao,
            idChave);
    }
}
