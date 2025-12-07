using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Dashboard
{
    public class MonthlyBookingCountDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }

    public class TopServiceTypeDto
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; } = default!;
        public int Count { get; set; }
    }

    public class BookingStatsDto
    {
        public int OpenOrders { get; set; }
        public int CompletedThisWeek { get; set; }
        public decimal RevenueLast30Days { get; set; }
        public int TotalCustomers { get; set; }

        public List<MonthlyBookingCountDto> BookingsPerMonth { get; set; } = new();
        public List<TopServiceTypeDto> TopServiceTypes { get; set; } = new();
    }

    public interface IBookingStatsService
    {
        Task<BookingStatsDto> GetStatsAsync(CancellationToken cancellationToken = default);
    }
}
