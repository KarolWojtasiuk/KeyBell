namespace KeyBell.Write.Domain.Users;

public class User : AggregateRoot<Guid>
{
    public static User Create(long keyId, byte[] publicKey)
    {
        return new User(Guid.NewGuid());
    }

    private User(Guid id) : base(id)
    {
    }
}