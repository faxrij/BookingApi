using BookingApi.Models;
using BookingApi.Repositories.Abstractions;

namespace BookingApi.IntegrationTests.Fakes;

internal class FakeHomeRepository : IHomeRepository
{
    private readonly IReadOnlyCollection<Home> _homes;

    public FakeHomeRepository(IEnumerable<Home> homes)
    {
        _homes = homes.ToList().AsReadOnly();
    }

    public Task<IReadOnlyCollection<Home>> GetAllHomesAsync()
    {
        return Task.FromResult(_homes);
    }
}
