using Microsoft.AspNetCore.Mvc;
using WorkshopMaster.Application.Bookings;

namespace WorkshopMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> GetAll(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? status,
            [FromQuery(Name = "vehicleReg")] string? vehicleRegistrationNumber)
        {
            var filter = new BookingFilter
            {
                From = from,
                To = to,
                Status = status,
                VehicleRegistrationNumber = vehicleRegistrationNumber
            };

            var items = await _bookingService.GetAllAsync(filter);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookingDto>> GetById(int id)
        {
            var item = await _bookingService.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
        {
            var created = await _bookingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BookingDto>> Update(int id, UpdateBookingDto dto)
        {
            var updated = await _bookingService.UpdateAsync(id, dto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
