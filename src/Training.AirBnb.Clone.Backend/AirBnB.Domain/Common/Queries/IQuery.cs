using MediatR;

namespace AirBnB.Domain.Common.Queries;

/// <summary>
/// Represents a query in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IQuery<out TResult> : IRequest<TResult>, IQuery
{
}

/// <summary>
/// Represents a query in a CQRS architecture
/// </summary>
public interface IQuery
{
}