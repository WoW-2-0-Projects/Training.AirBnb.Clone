using MediatR;

namespace AirBnB.Domain.Common.Commands;


/// <summary>
/// Represents a handler for processing commands in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}