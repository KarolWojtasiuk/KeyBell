namespace KeyBell.Write.Domain;

public interface IDateTimeProvider
{
    public DateTime GetUtcNow();
}