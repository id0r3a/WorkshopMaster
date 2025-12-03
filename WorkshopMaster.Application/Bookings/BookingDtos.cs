using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public string VehicleRegistrationNumber { get; set; } = default!;
        public string VehicleBrand { get; set; } = default!;
        public string CustomerName { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = default!;
        public string? Notes { get; set; }

        public List<string> ServiceTypes { get; set; } = new();
    }

    public class CreateBookingDto
    {
        public int VehicleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ServiceTypeIds { get; set; } = new();
        public string? Notes { get; set; }
    }

    public class UpdateBookingDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ServiceTypeIds { get; set; } = new();
        public string Status { get; set; } = default!;
        public string? Notes { get; set; }
    }

    public class BookingFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Status { get; set; }
        public string? VehicleRegistrationNumber { get; set; }
    }
}
