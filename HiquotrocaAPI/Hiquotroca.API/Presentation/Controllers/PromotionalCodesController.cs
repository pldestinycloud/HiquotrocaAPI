using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.CreatePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.DeletePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetAllPromotionalCodes;
using Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionalCodesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PromotionalCodesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private long GetCurrentUserId()
    {
        var idClaim = User.Claims.FirstOrDefault(c => c.Type == "id" || c.Type == "sub");
        if (idClaim == null)
            throw new Exception("User ID not found in token");
        return long.Parse(idClaim.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DTOs.PromotionalCode.CreatePromotionalCodeDto dto)
    {
        long currentUserId = 1; // GetCurrentUserId();
        var id = await _mediator.Send(new CreatePromotionalCodeCommand(dto, currentUserId));
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var code = await _mediator.Send(new GetPromotionalCodeByIdQuery(id));
        if (code == null)
            return NotFound();
        return Ok(code);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var codes = await _mediator.Send(new GetAllPromotionalCodesQuery());
        return Ok(codes);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DTOs.PromotionalCode.UpdatePromotionalCodeDto dto)
    {
        long currentUserId = GetCurrentUserId();
        var code = await _mediator.Send(new UpdatePromotionalCodeCommand(dto, currentUserId));
        if (code == null)
            return NotFound();
        return Ok(code);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        long currentUserId = GetCurrentUserId();
        var success = await _mediator.Send(new DeletePromotionalCodeCommand(id, currentUserId));
        if (!success)
            return NotFound();
        return Ok();
    }
}
