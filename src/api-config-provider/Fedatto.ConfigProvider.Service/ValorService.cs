using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Service;

public class ValorService : IValorService
{
    private readonly IUnitOfWork _uow;

    public ValorService(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValores(
        CancellationToken cancellationToken,
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true)
    {
        return chave.Tipo.Id switch
        {
            3 => await _uow.ValorRepository.BuscarValoresDouble(cancellationToken, chave.Id, vigenteEm, habilitado),
            5 => await _uow.ValorRepository.BuscarValoresString(cancellationToken, chave.Id, vigenteEm, habilitado),
            7 => await _uow.ValorRepository.BuscarValoresBool(cancellationToken, chave.Id, vigenteEm, habilitado),
            11 => await _uow.ValorRepository.BuscarValoresDatas(cancellationToken, chave.Id, vigenteEm, habilitado),
            13 => await _uow.ValorRepository.BuscarValoresJson(cancellationToken, chave.Id, vigenteEm, habilitado),
            17 => await _uow.ValorRepository.BuscarValoresBinaryB64(cancellationToken, chave.Id, vigenteEm, habilitado),
            _ => throw new Http501NaoImplementadoException()
        };
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        return await _uow.AplicacaoRepository.BuscarAplicacaoPorId(
            cancellationToken,
            appId);
    }

    public async Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int idChave)
    {
        return await _uow.ChaveRepository.BuscarChavePorId(
            cancellationToken,
            aplicacao,
            idChave,
            (ct, i) => _uow.TipoRepository.BuscarTipo(ct, i).Result);
    }
}
