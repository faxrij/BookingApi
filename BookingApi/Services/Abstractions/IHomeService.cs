using BookingApi.DTOs;

namespace BookingApi.Services.Abstractions;

public interface IHomeService
{
    Task<List<HomeDto>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate);
}
