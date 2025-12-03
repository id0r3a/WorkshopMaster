using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
        public string? Notes { get; set; }

        public ICollection<BookingServiceType> BookingServiceTypes { get; set; }
            = new List<BookingServiceType>();
    }
}
