using System.Text.Json;
using KeyBell.Presentation.WebApi.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;

namespace KeyBell.Presentation.WebApi;

public class PgpInputFormatter : InputFormatter
{
    private readonly IPgpMessageReader _messageReader;

    public PgpInputFormatter(IPgpMessageReader messageReader)
    {
        _messageReader = messageReader;

        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/pgp-encrypted"));
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        try
        {
            using var bodyReader = new StreamReader(context.HttpContext.Request.Body);
            var bodyContent = await bodyReader.ReadToEndAsync();

            var pgpData = Convert.FromBase64String(bodyContent);
            using var pgpDataStream = new MemoryStream(pgpData);

            var result = await _messageReader.ReadAsync(pgpDataStream);

            return await InputFormatterResult.SuccessAsync(JsonSerializer.Deserialize(result, context.ModelType));
        }
        catch (Exception exception)
        {
            var message = $"Error processing PGP data. {exception.Message}";
            context.ModelState.TryAddModelError("Body", message);
            return await InputFormatterResult.FailureAsync();
        }
    }
}