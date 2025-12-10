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

        public PromotionalCodeController(PromotionalCodeService service)
        {
            _service = service;
        }
        private long GetCurrentUserId()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "id" || c.Type == "sub");

            if (idClaim == null)
                throw new Exception("User ID not found in token");

            return long.Parse(idClaim.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePromotionalCodeDto dto)
        {
            long currentUserId = 1; // GetCurrentUserId();

            var result = await _service.CreateAsync(dto, currentUserId);

            if (!result.isSuccess)
                return BadRequest(result.Errors?.FirstOrDefault()?.Description);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result.Errors?.FirstOrDefault()?.Description);

            if (result.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePromotionalCodeDto dto)
        {
            long currentUserId = GetCurrentUserId();

            var result = await _service.UpdateAsync(dto, currentUserId);

            if (!result.isSuccess)
                return BadRequest(result.Errors?.FirstOrDefault()?.Description);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            long currentUserId = GetCurrentUserId();

            var result = await _service.DeleteAsync(id, currentUserId);

            if (!result.isSuccess)
                return BadRequest(result.Errors?.FirstOrDefault()?.Description);

            if (result.Data == false)
                return NotFound();

            return Ok();
        }
    }
}
