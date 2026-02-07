using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapGet("/time", () =>
{
    return new
    {
        CurrentTime = DateTime.UtcNow,
        TimeZone = "UTC"
    };
});

app.Run();
