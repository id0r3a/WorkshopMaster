using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.Vehicles;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public VehicleService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<VehicleDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Vehicles
                .AsNoTracking()
                .Include(v => v.Customer)
                .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<VehicleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            return entity is null ? null : _mapper.Map<VehicleDto>(entity);
        }

        public async Task<VehicleDto?> GetByRegistrationAsync(string registrationNumber, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(v => v.RegistrationNumber == registrationNumber, cancellationToken);

            return entity is null ? null : _mapper.Map<VehicleDto>(entity);
        }

        public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Vehicle>(dto);
            _db.Vehicles.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<VehicleDto>(entity);
        }

        public async Task<VehicleDto?> UpdateAsync(int id, UpdateVehicleDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Vehicles.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<VehicleDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Vehicles.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return false;

            _db.Vehicles.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<List<VehicleDto>> GetByCustomerAsync(int customerId)
        {
            var vehicles = await _db.Vehicles
                .Include(v => v.Customer)
                .Where(v => v.CustomerId == customerId)
                .ToListAsync();

            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                CustomerId = v.CustomerId,
                CustomerName = v.Customer.FullName
            }).ToList();
        }
    }
}
