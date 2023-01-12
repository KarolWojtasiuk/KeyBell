using MediatR;

namespace KeyBell.Write.Application;

internal class MediatrCommandDispatcher : ICommandDispatcher
{
    private readonly IMediator _mediator;

    public MediatrCommandDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>
    {
        return _mediator.Send(command, cancellationToken);
    }
}