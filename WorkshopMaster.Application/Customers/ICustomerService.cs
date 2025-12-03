using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CustomerDto> CreateAsync(CreateCustomerDto dto, CancellationToken cancellationToken = default);
        Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
