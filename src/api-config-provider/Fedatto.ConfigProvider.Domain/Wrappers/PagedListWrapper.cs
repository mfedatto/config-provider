namespace Fedatto.ConfigProvider.Domain.Wrappers;

public struct PagedListWrapper<T>
{
    public int Total { get; init; }
    public int Skip { get; init; }
    public int? Limit { get; init; }
    public int Count { get; init; }
    public IEnumerable<T> Payload { get; init; }
    
    public PagedListWrapper(IEnumerable<T> payload, int total, int skip, int? limit, int count)
    {
        Payload = payload;
        Total = total;
        Skip = skip;
        Limit = limit;
        Count = count;
    }
}
