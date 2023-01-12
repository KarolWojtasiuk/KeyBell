using KeyBell.Write.Domain.Users;

namespace KeyBell.Write.Application.Users;

internal class RegisterCommandHandler : CommandHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<Guid> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.KeyId, command.PublicKey);
        await _userRepository.InsertAsync(user, cancellationToken);
        return user.Id;
    }
}