using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.ServiceTypes;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Infrastructure.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ServiceTypeService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<ServiceTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.ServiceTypes
                .AsNoTracking()
                .ProjectTo<ServiceTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<ServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.ServiceTypes.FindAsync(new object[] { id }, cancellationToken);
            return entity is null ? null : _mapper.Map<ServiceTypeDto>(entity);
        }

        public async Task<ServiceTypeDto> CreateAsync(CreateServiceTypeDto dto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<ServiceType>(dto);
            _db.ServiceTypes.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ServiceTypeDto>(entity);
        }

        public async Task<ServiceTypeDto?> UpdateAsync(int id, UpdateServiceTypeDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _db.ServiceTypes.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ServiceTypeDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.ServiceTypes.FindAsync(new object[] { id }, cancellationToken);
            if (entity is null) return false;

            _db.ServiceTypes.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
