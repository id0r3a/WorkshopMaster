using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.ServiceTypes
{
    public class ServiceTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class CreateServiceTypeDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class UpdateServiceTypeDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
    }
}
