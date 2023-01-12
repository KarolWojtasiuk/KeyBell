using System.Text;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace KeyBell.Presentation.WebApi.Services;

internal class PgpMessageReader : IPgpMessageReader
{
    private readonly IConfiguration _configuration;

    public PgpMessageReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> ReadAsync(Stream encryptedDataStream)
    {
        var key = ReadServerSecretKey();

        var objectFactory = new PgpObjectFactory(PgpUtilities.GetDecoderStream(encryptedDataStream));
        var dataList = GetEncryptedDataList(objectFactory);

        var encryptedData = dataList.GetEncryptedDataObjects().OfType<PgpPublicKeyEncryptedData>().FirstOrDefault()
                            ?? throw new InvalidOperationException(
                                "PGP message does not contain public key encrypted data");

        var serverSecretKey = ReadServerSecretKey();
        if (serverSecretKey.KeyId != encryptedData.KeyId)
            throw new InvalidOperationException("Server is not a recipient of this PGP message");

        throw new NotImplementedException();
    }

    private PgpEncryptedDataList GetEncryptedDataList(PgpObjectFactory objectFactory)
    {
        while (true)
        {
            var pgpObject = objectFactory.NextPgpObject();

            if (pgpObject is PgpEncryptedDataList dataList)
                return dataList;

            if (pgpObject is null)
                throw new InvalidOperationException("PGP message does not contain encrypted data list");
        }
    }

    private PgpSecretKey ReadServerSecretKey()
    {
        var keyPath = _configuration["PgpSecretKeyPath"]
                      ?? throw new InvalidOperationException("PgpSecretKeyPath is required for decryption");

        using var fileStream = File.OpenRead(keyPath);
        using var decoderStream = PgpUtilities.GetDecoderStream(fileStream);
        var keyRingBundle = new PgpSecretKeyRingBundle(decoderStream);

        return keyRingBundle
                   .GetKeyRings()
                   .SelectMany(kr => kr.GetSecretKeys())
                   .FirstOrDefault(k => !k.IsMasterKey && k.IsSigningKey)
               ?? throw new InvalidOperationException("Pgp secret key file does not contain valid key");
    }
}