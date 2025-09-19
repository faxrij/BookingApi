using BookingApi.Models;

namespace BookingApi.Repositories.Abstractions;

public interface IHomeRepository
{
    Task<IReadOnlyCollection<Home>> GetAllHomesAsync();
}
