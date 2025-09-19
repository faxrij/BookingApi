namespace BookingApi.DTOs;

public class AvailableHomesResponseDto
{
    public string Status { get; set; }
    public List<HomeDto> Homes { get; set; } = [];
}