namespace KeyBell.Write.Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    public Task InsertAsync(User user, CancellationToken cancellationToken);
}