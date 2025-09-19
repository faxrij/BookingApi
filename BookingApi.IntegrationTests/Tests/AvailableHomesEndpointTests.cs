using System.Net;
using System.Net.Http.Json;

using Xunit;

using BookingApi.DTOs;
using BookingApi.Models;

namespace BookingApi.IntegrationTests.Tests
{
    public class AvailableHomesEndpointTests
    {
        private readonly HttpClient _client;

        public AvailableHomesEndpointTests()
        {
            Home[] testHomes =
            [
                new Home
                {
                    HomeId = "123",
                    HomeName = "Home 1",
                    AvailableSlots =
                    [
                        new DateTime(2025, 7, 15),
                        new DateTime(2025, 7, 16),
                        new DateTime(2025, 7, 17)
                    ]
                },
                new Home
                {
                    HomeId = "456",
                    HomeName = "Home 2",
                    AvailableSlots =
                    [
                        new DateTime(2025, 7, 17),
                        new DateTime(2025, 7, 18)
                    ]
                }
            ];

            TestWebApplicationFactory factory = new TestWebApplicationFactory(testHomes);
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("2025-07-15", "2025-07-16", 1, "123")]
        [InlineData("2025-07-17", "2025-07-18", 1, "456")]
        [InlineData("2025-07-15", "2025-07-17", 1, "123")]
        [InlineData("2025-07-16", "2025-07-17", 1, "123")]
        [InlineData("2025-07-15", "2025-07-18", 0, null)]
        public async Task ReturnsCorrectHomesForDateRange(string startDate, string endDate, int expectedCount, string expectedHomeId)
        {
            var response = await _client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<AvailableHomesResponseDto>();
            Assert.NotNull(result);
            Assert.Equal("OK", result.Status);
            Assert.Equal(expectedCount, result.Homes.Count);

            if (expectedHomeId != null)
            {
                Assert.Contains(result.Homes, h => h.HomeId == expectedHomeId);
            }
        }

        [Theory]
        [InlineData("2025-07-15", null)]
        [InlineData("notadate", "2025-07-16")]
        [InlineData("2025-07-16", "2025-07-15")] // StartDate >= EndDate
        public async Task ReturnsBadRequestForInvalidRequests(string startDate, string? endDate)
        {
            string url = $"/api/available-homes?startDate={startDate}";
            if (endDate != null) url += $"&endDate={endDate}";

            var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
