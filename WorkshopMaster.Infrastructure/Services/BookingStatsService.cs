using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.Dashboard;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Infrastructure.Services
{
    public class BookingStatsService : IBookingStatsService
    {
        private readonly AppDbContext _db;

        public BookingStatsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<BookingStatsDto> GetStatsAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var thirtyDaysAgo = now.AddDays(-30);
            var weekAgo = now.AddDays(-7);

            var stats = new BookingStatsDto();

            // 🔹 KPI: Open orders (Pending + Confirmed)
            stats.OpenOrders = await _db.Bookings
                .CountAsync(b => b.Status == "Pending" || b.Status == "Confirmed", cancellationToken);

            // 🔹 KPI: Completed this week
            stats.CompletedThisWeek = await _db.Bookings
                .CountAsync(b => b.Status == "Completed" && b.EndTime >= weekAgo, cancellationToken);

            // 🔹 KPI: Revenue last 30 days 
            stats.RevenueLast30Days = 0m;

            // 🔹 KPI: Total customers
            stats.TotalCustomers = await _db.Customers
                .CountAsync(cancellationToken);

            // Bokningar per månad (senaste 12 månaderna)
            var fromDate = DateTime.UtcNow.AddMonths(-11);

            stats.BookingsPerMonth = await _db.Bookings
                .Where(b => b.StartTime >= fromDate)
                .GroupBy(b => new { b.StartTime.Year, b.StartTime.Month })
                .Select(g => new MonthlyBookingCountDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync(cancellationToken);

            // ⭐ Topp 3 tjänster
            stats.TopServiceTypes = await _db.BookingServiceTypes
                .GroupBy(bst => new { bst.ServiceTypeId, bst.ServiceType.Name })
                .Select(g => new TopServiceTypeDto
                {
                    ServiceTypeId = g.Key.ServiceTypeId,
                    Name = g.Key.Name,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToListAsync(cancellationToken);

            return stats;
        }
    }
}
