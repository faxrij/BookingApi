using Microsoft.AspNetCore.Mvc;

using BookingApi.DTOs;
using BookingApi.Services.Abstractions;

namespace BookingApi.Controllers;

[ApiController]
[Route("api/available-homes")]
public class HomesController : ControllerBase
{
    private readonly IHomeService _homeService;

    public HomesController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AvailableHomesRequestDto request)
    {
        List<HomeDto> homes = await _homeService.GetAvailableHomesAsync(request.StartDate, request.EndDate);

        AvailableHomesResponseDto response = new AvailableHomesResponseDto
        {
            Status = "OK",
            Homes = homes
        };

        return Ok(response);
    }
}
