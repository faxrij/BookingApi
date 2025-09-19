namespace BookingApi.DTOs;

public class HomeDto
{
    public string HomeId { get; set; }
    public string HomeName { get; set; }
    public List<string> AvailableSlots { get; set; } = [];
}
