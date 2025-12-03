using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Application.Bookings;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;

namespace WorkshopMaster.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public BookingService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> GetAllAsync(BookingFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _db.Bookings
                .AsNoTracking()
                .Include(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Include(b => b.BookingServiceTypes)
                    .ThenInclude(bst => bst.ServiceType)
                .AsQueryable();

            if (filter.From.HasValue)
            {
                query = query.Where(b => b.StartTime >= filter.From.Value);
            }

            if (filter.To.HasValue)
            {
                query = query.Where(b => b.EndTime <= filter.To.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Status))
            {
                query = query.Where(b => b.Status == filter.Status);
            }

            if (!string.IsNullOrWhiteSpace(filter.VehicleRegistrationNumber))
            {
                query = query.Where(b => b.Vehicle.RegistrationNumber == filter.VehicleRegistrationNumber);
            }

            var entities = await query
                .OrderByDescending(b => b.StartTime)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<BookingDto>>(entities);
        }

        public async Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Bookings
                .Include(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Include(b => b.BookingServiceTypes)
                    .ThenInclude(bst => bst.ServiceType)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            return entity is null ? null : _mapper.Map<BookingDto>(entity);
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = new Booking
            {
                VehicleId = dto.VehicleId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = "Pending",
                Notes = dto.Notes
            };

            foreach (var serviceTypeId in dto.ServiceTypeIds.Distinct())
            {
                booking.BookingServiceTypes.Add(new BookingServiceType
                {
                    ServiceTypeId = serviceTypeId
                });
            }

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync(cancellationToken);

            // ladda om inkl navigationer
            booking = await _db.Bookings
                .Include(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Include(b => b.BookingServiceTypes)
                    .ThenInclude(bst => bst.ServiceType)
                .FirstAsync(b => b.Id == booking.Id, cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto?> UpdateAsync(int id, UpdateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = await _db.Bookings
                .Include(b => b.BookingServiceTypes)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (booking is null) return null;

            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.Status = dto.Status;
            booking.Notes = dto.Notes;

            // uppdatera many-to-many
            booking.BookingServiceTypes.Clear();
            foreach (var serviceTypeId in dto.ServiceTypeIds.Distinct())
            {
                booking.BookingServiceTypes.Add(new BookingServiceType
                {
                    BookingId = id,
                    ServiceTypeId = serviceTypeId
                });
            }

            await _db.SaveChangesAsync(cancellationToken);

            booking = await _db.Bookings
                .Include(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Include(b => b.BookingServiceTypes)
                    .ThenInclude(bst => bst.ServiceType)
                .FirstAsync(b => b.Id == id, cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var booking = await _db.Bookings.FindAsync(new object[] { id }, cancellationToken);
            if (booking is null) return false;

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
