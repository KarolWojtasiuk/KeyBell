using KeyBell.Write.Domain;

namespace KeyBell.Write.Infrastructure;

internal class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}