using MediatR;

namespace AirBnB.Domain.Common.Queries;

/// <summary>
/// Represents a handler for processing queries in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}