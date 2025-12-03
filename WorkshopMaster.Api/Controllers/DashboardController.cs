using Microsoft.AspNetCore.Mvc;
using WorkshopMaster.Application.Dashboard;

namespace WorkshopMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IBookingStatsService _statsService;

        public DashboardController(IBookingStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("booking-stats")]
        public async Task<ActionResult<BookingStatsDto>> GetBookingStats()
        {
            var stats = await _statsService.GetStatsAsync();
            return Ok(stats);
        }
    }
}
