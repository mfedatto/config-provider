using System.Data.Common;

namespace Fedatto.ConfigProvider.Domain.MainDbContext;

public interface IUnitOfWork
{
    DbConnection DbConnection { get; }

    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}
