using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace TimeApi.Tests;

public class TimeApiIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TimeApiIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTime_Endpoint_ReturnsSuccessAndCorrectData()
    {
        // Act
        var response = await _client
            .GetAsync("/time");

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var result = await response.Content
            .ReadFromJsonAsync<TimeResponse>();

        result
            .Should()
            .NotBeNull();
        result!.TimeZone.Should().Be("UTC");

        result.CurrentTime
            .Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        // Act
        var response = await _client
            .GetAsync("/health");

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var content = await response.Content
            .ReadAsStringAsync();

        content
            .Should()
            .Be("Healthy");
    }
}
