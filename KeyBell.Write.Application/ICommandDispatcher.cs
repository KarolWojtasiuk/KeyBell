namespace KeyBell.Write.Application;

public interface ICommandDispatcher
{
    public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand;

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>;
}