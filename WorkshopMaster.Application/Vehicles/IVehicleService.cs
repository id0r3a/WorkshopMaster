using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Vehicles
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<VehicleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<VehicleDto?> GetByRegistrationAsync(string registrationNumber, CancellationToken cancellationToken = default);
        Task<List<VehicleDto>> GetByCustomerAsync(int customerId);

        Task<VehicleDto> CreateAsync(CreateVehicleDto dto, CancellationToken cancellationToken = default);
        Task<VehicleDto?> UpdateAsync(int id, UpdateVehicleDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
