using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Domain.Entities
{
    public class BookingServiceType
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = default!;
    }
}
