using MediatR;

namespace AirBnB.Domain.Commands;

/// <summary>
/// Represents a command in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
}

/// <summary>
/// Marker interface for commands.
/// </summary>
public interface ICommand
{
    
}