using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.ServiceTypes
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ServiceTypeDto> CreateAsync(CreateServiceTypeDto dto, CancellationToken cancellationToken = default);
        Task<ServiceTypeDto?> UpdateAsync(int id, UpdateServiceTypeDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
