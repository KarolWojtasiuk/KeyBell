namespace KeyBell.Presentation.WebApi.Services;

public interface IPgpMessageReader
{
    public Task<string> ReadAsync(Stream encryptedDataStream);
}