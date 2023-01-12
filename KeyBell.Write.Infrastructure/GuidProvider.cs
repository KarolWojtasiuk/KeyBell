using KeyBell.Write.Domain;

namespace KeyBell.Write.Infrastructure;

internal class GuidProvider : IGuidProvider
{
    public Guid GetNew()
    {
        return Guid.NewGuid();
    }
}