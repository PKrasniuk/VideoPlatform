using MediatR;

namespace VideoPlatform.CQRS.Commands;

public abstract class BaseCommand<T> : IRequest<T>
{
}