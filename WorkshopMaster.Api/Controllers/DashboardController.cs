using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.Dashboard;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("booking-stats")]
        public async Task<IActionResult> GetStats()
        {
            var now = DateTime.UtcNow;
            var thirtyDaysAgo = now.AddDays(-30);
            var weekAgo = now.AddDays(-7);

            var openOrders = await _db.Bookings
                .CountAsync(b => b.Status == "Pending" || b.Status == "Confirmed");

            var completedThisWeek = await _db.Bookings
                .CountAsync(b => b.Status == "Completed" && b.EndTime >= weekAgo);

            decimal revenueLast30Days = 0m;

            var totalCustomers = await _db.Customers.CountAsync();

            return Ok(new
            {
                openOrders,
                completedThisWeek,
                revenueLast30Days,
                totalCustomers
            });
        }
    }
}
