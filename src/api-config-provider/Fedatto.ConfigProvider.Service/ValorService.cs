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
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        return chave.Tipo.Id switch
        {
            3 => await _repository.BuscarValoresDouble(chave.Id, vigenteEm, habilitado),
            5 => await _repository.BuscarValoresString(chave.Id, vigenteEm, habilitado),
            7 => await _repository.BuscarValoresBool(chave.Id, vigenteEm, habilitado),
            11 => await _repository.BuscarValoresDatas(chave.Id, vigenteEm, habilitado),
            13 => await _repository.BuscarValoresJson(chave.Id, vigenteEm, habilitado),
            17 => await _repository.BuscarValoresBinaryB64(chave.Id, vigenteEm, habilitado),
            _ => throw new Http501NaoImplementadoException()
        };
    }

    public async Task<bool> AplicacaoExiste(Guid appId)
    {
        return (await _aplicacaoRepository.BuscarAplicacaoPorId(appId)) is not null;
    }

    public async Task<IChave> BuscarChavePorId(
        Guid appId,
        int idChave,
        DateTime vigenteEm)
    {
        return await _chaveRepository.BuscarChavePorId(
            appId,
            idChave,
            vigenteEm);
    }
}
