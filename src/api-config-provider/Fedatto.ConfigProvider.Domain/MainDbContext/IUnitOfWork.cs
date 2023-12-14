using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Valor;

namespace Fedatto.ConfigProvider.Domain.MainDbContext;

public interface IUnitOfWork : IDisposable
{
    IAplicacaoRepository AplicacaoRepository { get; }
    ITipoRepository TipoRepository { get; }
    IChaveRepository ChaveRepository { get; }
    IValorRepository ValorRepository { get; }

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
