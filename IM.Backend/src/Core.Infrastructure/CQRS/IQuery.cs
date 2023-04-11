using MediatR;

namespace Core.Infrastructure.CQRS;

public interface IQuery<out T> : IRequest<T>
    where T : notnull
{
}
