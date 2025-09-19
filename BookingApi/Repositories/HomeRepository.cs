using System.Collections.Concurrent;

using BookingApi.Models;
using BookingApi.Repositories.Abstractions;

namespace BookingApi.Repositories;

public class HomeRepository : IHomeRepository
{
    private readonly ConcurrentDictionary<string, Home> _homes = new(
        [
            new KeyValuePair<string, Home>("123", new Home
            {
                HomeId = "123",
                HomeName = "Home 1",
                AvailableSlots =
                [
                    new DateTime(2025, 7, 15),
                    new DateTime(2025, 7, 16),
                    new DateTime(2025, 7, 17)
                ]
            }),
            new KeyValuePair<string, Home>("456", new Home
            {
                HomeId = "456",
                HomeName = "Home 2",
                AvailableSlots =
                [
                    new DateTime(2025, 7, 15),
                    new DateTime(2025, 7, 16),
                    new DateTime(2025, 7, 17),
                    new DateTime(2025, 7, 18)
                ]
            }),
            new KeyValuePair<string, Home>("789", new Home
            {
                HomeId = "789",
                HomeName = "Home 3",
                AvailableSlots =
                [
                    new DateTime(2025, 7, 17),
                    new DateTime(2025, 7, 18)
                ]
            })
        ]
    );

    public async Task<IReadOnlyCollection<Home>> GetAllHomesAsync()
    {
        return await Task.FromResult(_homes.Values.ToList().AsReadOnly());
    }
}
