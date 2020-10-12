using MediatR;

namespace VideoPlatform.CQRS.Queries
{
    public abstract class BaseQuery<TResult> : IRequest<TResult>
    {
    }
}