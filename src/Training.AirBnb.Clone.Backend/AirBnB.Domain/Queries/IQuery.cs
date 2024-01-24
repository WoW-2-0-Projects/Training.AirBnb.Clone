using MediatR;

namespace AirBnB.Domain.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}