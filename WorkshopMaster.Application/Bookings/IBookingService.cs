using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Bookings
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllAsync(BookingFilter filter, CancellationToken cancellationToken = default);
        Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<BookingDto> CreateAsync(CreateBookingDto dto, CancellationToken cancellationToken = default);
        Task<BookingDto?> UpdateAsync(int id, UpdateBookingDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<BookingDto?> UpdateStatusAsync(int id, UpdateBookingStatusDto dto, CancellationToken cancellationToken = default);
    }
}
