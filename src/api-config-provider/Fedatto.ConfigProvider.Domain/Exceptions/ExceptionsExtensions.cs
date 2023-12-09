namespace Fedatto.ConfigProvider.Domain.Exceptions;

public static class ExceptionsExtensions
{
    public static async Task<TResult> ThrowIfNull<TResult, TException>(
        this Task<TResult> task)
        where TException : Exception, new()
    {
        TResult result = await task.ConfigureAwait(false);
        
        if (result is null) throw new TException();

        return result;
    }
    
    public static async Task<TResult> ThrowIfNullOrUnavailable<TResult, TException>(
        this Task<TResult> task,
        Func<TResult, bool> available)
        where TException : Exception, new()
    {
        TResult result = await task
            .ThrowIfNull<TResult, TException>()
            .ConfigureAwait(false);
        
        if (available(result)) throw new TException();

        return result;
    }
}
