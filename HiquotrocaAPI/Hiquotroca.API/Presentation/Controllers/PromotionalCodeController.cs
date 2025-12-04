using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.PromotionalCode;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionalCodeController : ControllerBase
    {
        private readonly PromotionalCodeService _service;

        [HttpPost]
        public async Task<IActionResult> Create(CreatePromotionalCodeDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePromotionalCodeDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
