using MediatR;

namespace AirBnB.Domain.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
    
}

public interface ICommand
{
    
}