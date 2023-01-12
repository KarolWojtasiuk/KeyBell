using KeyBell.Presentation.WebApi;
using KeyBell.Presentation.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOptions<MvcOptions>()
    .Configure<IPgpMessageReader>((o, reader) => { o.InputFormatters.Add(new PgpInputFormatter(reader)); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPgpMessageReader, PgpMessageReader>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();