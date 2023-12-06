using System.Data;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Npgsql;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext;

public class UnitOfWork : IUnitOfWork
{
    private IDbTransaction? _transaction;

    public IDbConnection DbConnection { get; }

    public UnitOfWork()
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new(
            "Server=services.agz.vbox:5433;Database=config_provider;User Id=config_provider;Password=config_provider;")
        {
            IncludeErrorDetail = true
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
