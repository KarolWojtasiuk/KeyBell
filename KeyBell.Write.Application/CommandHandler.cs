using MediatR;

namespace KeyBell.Write.Application;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        await HandleAsync(command, cancellationToken);
        return Unit.Value;
    }

    protected abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        return HandleAsync(command, cancellationToken);
    }

    protected abstract Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}