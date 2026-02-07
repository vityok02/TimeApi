using Scalar.AspNetCore;
using TimeApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapGet("/time", () =>
{
    var time = new TimeResponse(DateTime.UtcNow, "UTC");
    return TypedResults.Ok(time);
}).WithName("CurrentTime");

await app.RunAsync();
