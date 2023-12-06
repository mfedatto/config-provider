using System.Data;

namespace Fedatto.ConfigProvider.Domain.MainDbContext;

public interface IUnitOfWork
{
    IDbConnection DbConnection { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
}
