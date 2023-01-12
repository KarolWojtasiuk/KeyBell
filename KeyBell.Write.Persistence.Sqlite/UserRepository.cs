using KeyBell.Write.Domain.Users;

namespace KeyBell.Write.Persistence.Sqlite;

internal class UserRepository : IUserRepository
{
    public Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}