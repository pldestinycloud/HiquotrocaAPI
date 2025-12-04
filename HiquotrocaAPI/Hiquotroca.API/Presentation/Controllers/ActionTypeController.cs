using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.ActionType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers
{
    //Uncomment the attributes below to enable routing and API controller behavior if we need backoffice access

   /* [Route("api/[controller]")]
    [ApiController]
    public class ActionTypeController : ControllerBase
    {
        private readonly ActionTypeService _actionTypeService;

        public ActionTypeController(ActionTypeService service)
        {
            _actionTypeService = service;
        }

        private long GetUserId()
        {
            var claim = User.FindFirst("id")?.Value;

            if (claim == null)
                throw new UnauthorizedAccessException("User ID claim is missing.");

            return long.Parse(claim);
        }

        // GET: api/actiontype
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _actionTypeService.GetAllAsync());
        }

        // GET: api/actiontype/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await _actionTypeService.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: api/actiontype
        [HttpPost]
        public async Task<IActionResult> Create(ActionTypeCreateRequest request)
        {
            var userId = GetUserId();

            var result = await _actionTypeService.CreateAsync(request, userId);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/actiontype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, ActionTypeUpdateRequest request)
        {
            var userId = GetUserId();

            var success = await _actionTypeService.UpdateAsync(id, request, userId);

            if (!success)
                return NotFound();

            return Ok(new { message = "ActionType updated successfully." });
        }

        // DELETE (soft delete): api/actiontype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(long id)
        {
            var userId = GetUserId();

            var success = await _actionTypeService.DeleteAsync(id, userId);

            if (!success)
                return NotFound();

            return Ok(new { message = "ActionType deleted successfully." });
        }
    }*/
}
