using MediatR;

namespace AirBnB.Domain.Queries;

/// <summary>
/// Represents a query in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IQuery<out TResult> : IRequest<TResult>
{
}