using System.Diagnostics.CodeAnalysis;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public static class ExceptionsExtensions
{
    public static async Task<TResult> ThenThrowIfNull<TResult, TException>(
        this Task<TResult> task)
        where TException : Exception, new()
    {
        TResult result = await task.ConfigureAwait(false);
        
        if (result is null) throw new TException();

        return result;
    }

    public static async Task<TResult> ThenThrowIfNotNull<TResult, TException>(
        this Task<TResult> task)
        where TException : Exception, new()
    {
        TResult result = await task.ConfigureAwait(false);
        
        if (result is not null) throw new TException();

        return result;
    }

    public static async Task<TResult> ThenThrowIfNullOrUnavailable<TResult, TException>(
        this Task<TResult> task,
        Func<TResult, bool> available)
        where TException : Exception, new()
    {
        TResult result = await task
            .ThenThrowIfNull<TResult, TException>()
            .ConfigureAwait(false);
        
        if (!available(result)) throw new TException();

        return result;
    }

    public static void ThrowIfClientClosedRequest(this CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) throw new RequisicaoCanceladaException();
    }
}
