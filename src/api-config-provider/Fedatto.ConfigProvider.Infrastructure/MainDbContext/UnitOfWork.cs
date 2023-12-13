using System.Data;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Npgsql;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseConfig _config;
    private IDbTransaction? _transaction;

    public IDbConnection DbConnection { get; }

    public UnitOfWork(
        DatabaseConfig config)
    {
        _config = config;
        
        NpgsqlConnectionStringBuilder connectionStringBuilder = new(
            _config.ConnectionString)
        {
            IncludeErrorDetail = _config.IncludeErrorDetail
        };
        
        DbConnection = new NpgsqlConnection(connectionStringBuilder.ToString());

        DbConnection.Open();
    }

    public void BeginTransaction()
    {
        _transaction = DbConnection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        DbConnection.Dispose();
    }
}
