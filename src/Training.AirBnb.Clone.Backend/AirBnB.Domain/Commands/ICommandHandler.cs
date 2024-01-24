using MediatR;

namespace AirBnB.Domain.Commands;


public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}