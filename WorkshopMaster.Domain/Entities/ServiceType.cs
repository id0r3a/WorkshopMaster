using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Domain.Entities
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }

        public ICollection<BookingServiceType> BookingServiceTypes { get; set; }
            = new List<BookingServiceType>();
    }
}
