namespace BookingApi.Models;

public class Home
{
    public string HomeId { get; set; }
    public string HomeName { get; set; }
    public HashSet<DateTime> AvailableSlots { get; set; }
}
