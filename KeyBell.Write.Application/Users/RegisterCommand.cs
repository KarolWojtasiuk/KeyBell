namespace KeyBell.Write.Application.Users;

public record RegisterCommand(long KeyId, byte[] PublicKey) : ICommand<Guid>;