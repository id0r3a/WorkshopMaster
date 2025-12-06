using Microsoft.AspNetCore.Mvc;
using WorkshopMaster.Application.Vehicles;

namespace WorkshopMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDto>>> GetAll()
        {
            var items = await _vehicleService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleDto>> GetById(int id)
        {
            var item = await _vehicleService.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpGet("by-registration/{registration}")]
        public async Task<ActionResult<VehicleDto>> GetByRegistration(string registration)
        {
            var item = await _vehicleService.GetByRegistrationAsync(registration);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpGet("by-customer/{customerId:int}")]
        public async Task<ActionResult<List<VehicleDto>>> GetByCustomer(int customerId)
        {
            var items = await _vehicleService.GetByCustomerAsync(customerId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> Create(CreateVehicleDto dto)
        {
            var created = await _vehicleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<VehicleDto>> Update(int id, UpdateVehicleDto dto)
        {
            var updated = await _vehicleService.UpdateAsync(id, dto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _vehicleService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
