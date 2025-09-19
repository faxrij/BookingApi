using System.ComponentModel.DataAnnotations;

namespace BookingApi.DTOs;

public class AvailableHomesRequestDto
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}
