using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Vehicles
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
    }

    public class CreateVehicleDto
    {
        public string RegistrationNumber { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public int CustomerId { get; set; }
    }

    public class UpdateVehicleDto
    {
        public string RegistrationNumber { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public int CustomerId { get; set; }
    }
}
