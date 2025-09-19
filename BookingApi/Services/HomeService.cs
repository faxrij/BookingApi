using BookingApi.DTOs;
using BookingApi.Repositories.Abstractions;
using BookingApi.Models;
using BookingApi.Services.Abstractions;

namespace BookingApi.Services;

public class HomeService : IHomeService
{
    private readonly IHomeRepository _repository;

    public HomeService(IHomeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<HomeDto>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("StartDate must be less than EndDate.");

        List<DateTime> range = Enumerable.Range(0, (endDate - startDate).Days + 1)
            .Select(offset => startDate.AddDays(offset)).ToList();

        IReadOnlyCollection<Home> homes = await _repository.GetAllHomesAsync();

        List<HomeDto> result = homes
            .Where(home => range.All(date => home.AvailableSlots.Contains(date)))
            .Select(home => new HomeDto
            {
                HomeId = home.HomeId,
                HomeName = home.HomeName,
                AvailableSlots = home.AvailableSlots
                    .Where(slot => slot >= startDate && slot <= endDate)
                    .OrderBy(slot => slot)
                    .Select(slot => slot.ToString("yyyy-MM-dd"))
                    .ToList()
            })
            .ToList();

        return result;
    }
}
