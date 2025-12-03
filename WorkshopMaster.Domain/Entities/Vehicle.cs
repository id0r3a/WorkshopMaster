using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
