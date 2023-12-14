using System.Data;
using System.Data.Common;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;
using Npgsql;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ChaveFactory _chaveFactory;
    private readonly DbConnection _dbConnection;
    private DbTransaction? _dbTransaction;
    private bool _disposed;

    public UnitOfWork(
        DatabaseConfig config,
        ChaveFactory chaveFactory)
    {
        _chaveFactory = chaveFactory;
        _dbConnection = new NpgsqlConnection(
                new NpgsqlConnectionStringBuilder(
                        config.ConnectionString)
                    {
                        IncludeErrorDetail = config.IncludeErrorDetail
                    }
                    .ToString()
            );
    }

    public IAplicacaoRepository AplicacaoRepository => new AplicacaoRepository(_dbConnection, _dbTransaction!);
    public ITipoRepository TipoRepository => new TipoRepository(_dbConnection, _dbTransaction!);
    public IChaveRepository ChaveRepository => new ChaveRepository(_dbConnection, _dbTransaction!, _chaveFactory);
    public IValorRepository ValorRepository => new ValorRepository(_dbConnection, _dbTransaction!);

    public async Task BeginTransactionAsync()
    {
        if (_dbTransaction is not null) throw new ConexaoEmUsoPorOutraTransacaoException();

        if (!ConnectionState.Open.Equals(_dbConnection.State)) await _dbConnection.OpenAsync();

        _dbTransaction = await _dbConnection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_dbTransaction is null) throw new ConexaoSemTransacaoException();

        await _dbTransaction.CommitAsync();
        await _dbTransaction.DisposeAsync();

        _dbTransaction = null;
    }

    public async Task RollbackAsync()
    {
        await _dbTransaction?.RollbackAsync()!;
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbTransaction?.Dispose();
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}
