using Microsoft.AspNetCore.Mvc;
using WorkshopMaster.Application.ServiceTypes;

namespace WorkshopMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypesController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceTypeDto>>> GetAll()
        {
            var items = await _serviceTypeService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceTypeDto>> GetById(int id)
        {
            var item = await _serviceTypeService.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceTypeDto>> Create(CreateServiceTypeDto dto)
        {
            var created = await _serviceTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServiceTypeDto>> Update(int id, UpdateServiceTypeDto dto)
        {
            var updated = await _serviceTypeService.UpdateAsync(id, dto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serviceTypeService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
