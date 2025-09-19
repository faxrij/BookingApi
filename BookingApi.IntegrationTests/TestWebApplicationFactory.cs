using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using BookingApi.IntegrationTests.Fakes;
using BookingApi.Models;
using BookingApi.Repositories.Abstractions;

namespace BookingApi.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly IEnumerable<Home> _testHomes;

    public TestWebApplicationFactory(IEnumerable<Home> testHomes)
    {
        _testHomes = testHomes;
    }

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the real repository
            ServiceDescriptor descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IHomeRepository));
            if (descriptor != null) services.Remove(descriptor);

            // Add the fake repository with test-specific data
            services.AddSingleton<IHomeRepository>(new FakeHomeRepository(_testHomes));
        });
    }
}