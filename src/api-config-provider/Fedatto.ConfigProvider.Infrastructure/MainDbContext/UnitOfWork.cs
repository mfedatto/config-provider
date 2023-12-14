using System.Data;
using System.Data.Common;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Npgsql;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext;

public sealed class UnitOfWork : IUnitOfWork
{
    private DbTransaction? _transaction;
    private bool _disposed = false;

    public DbConnection DbConnection { get; }

    public UnitOfWork(
        DatabaseConfig config)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new(
            config.ConnectionString)
        {
            IncludeErrorDetail = config.IncludeErrorDetail
        };

        DbConnection = new NpgsqlConnection(connectionStringBuilder.ToString());
    }

    public async Task BeginTransaction()
    {
        if (!ConnectionState.Open.Equals(DbConnection.State))
        {
            await DbConnection.OpenAsync();
        }

        if (_transaction is not null)
        {
            throw new ConexaoEmUsoPorOutraTransacaoException();
        }


        _transaction = await DbConnection.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (_transaction is null)
        {
            throw new ConexaoSemTransacaoException();
        }

        await _transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        if (_transaction is null)
        {
            throw new ConexaoSemTransacaoException();
        }

        await _transaction.RollbackAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                DbConnection.Dispose();
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
