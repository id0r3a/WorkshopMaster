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
            var stats = new BookingStatsDto();

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
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync(cancellationToken);

            // Topp 3 tjänster
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
