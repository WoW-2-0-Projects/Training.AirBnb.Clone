using AirBnB.Domain.Common.Exceptions;

namespace AirBnB.Domain.Extensions;

/// <summary>
/// Extension methods for handling exceptions in asynchronous operations and converting them into FuncResult.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Asynchronously invokes a Func<Task<T>> and returns the result wrapped in a FuncResult.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="func">The asynchronous function to invoke.</param>
    /// <returns>A ValueTask<FuncResult<T>> representing the result of the asynchronous operation.</returns>
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception e)
        {
            result = new FuncResult<T>(e);
        }
        
        return result;
    }

    /// <summary>
    /// Asynchronously invokes a Func<ValueTask<T>> and returns the result wrapped in a FuncResult<T>.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="func">The asynchronous function to invoke.</param>
    /// <returns>A ValueTask<FuncResult<T>> representing the result of the asynchronous operation.</returns>
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<ValueTask<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception e)
        {
            result = new FuncResult<T>(e);
        }
        
        return result;
    }
}