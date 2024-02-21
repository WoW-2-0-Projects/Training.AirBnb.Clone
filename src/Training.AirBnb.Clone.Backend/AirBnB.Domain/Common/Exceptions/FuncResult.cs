namespace AirBnB.Domain.Common.Exceptions;

/// <summary>
/// Represents the result of a function that can return either data of type T or an exception.
/// </summary>
/// <typeparam name="T">The type of the data returned on success.</typeparam>
public class FuncResult<T>
{
    /// <summary>
    /// Gets the data result when the operation is successful.
    /// </summary>
    public T Data { get; init; }
    
    /// <summary>
    /// Gets the exception if the operation encounters an error; otherwise, is null.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess => Exception is null;
    
    /// <summary>
    /// Initializes a new instance of the FuncResult. Class with successful data.
    /// </summary>
    /// <param name="data">The data result of type T.</param>
    public FuncResult(T data) => Data = data;

    /// <summary>
    /// Initializes a new instance of the FuncResult. Class with an exception.
    /// </summary>
    /// <param name="exception">The exception encountered during the operation.</param>
    public FuncResult(Exception exception) => Exception = exception;
}