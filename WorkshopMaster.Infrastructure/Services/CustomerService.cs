using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.Customers;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Customers
                .AsNoTracking()
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Customers.FindAsync(new object[] { id }, cancellationToken);
            return entity is null ? null : _mapper.Map<CustomerDto>(entity);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Customer>(dto);
            _db.Customers.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Customers.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Customers.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return false;

            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
