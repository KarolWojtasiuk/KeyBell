using MediatR;

namespace KeyBell.Write.Application;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}