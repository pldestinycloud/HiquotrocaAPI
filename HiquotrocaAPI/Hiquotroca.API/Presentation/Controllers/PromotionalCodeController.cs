using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.AssignUserToPromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.CreatePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.DeletePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.RemoveUserFromPromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;
using Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetAllPromotionalCodes;
using Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodeById;
using Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodesOfUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionalCodeController : ControllerBase
{
    private readonly IMediator _mediator;
    public PromotionalCodeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var codes = await _mediator.Send(new GetAllPromotionalCodesQuery());
        return Ok(codes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var code = await _mediator.Send(new GetPromotionalCodeByIdQuery(id));
        if (code == null)
            return NotFound();

        return Ok(code);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePromotionalCodeCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePromotionalCodeCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _mediator.Send(new DeletePromotionalCodeCommand(id));
        return Ok();
    }

    [HttpGet("of-user/{userId:long}")]
    public async Task<IActionResult> GetPromoCodesOfUser(long userId)
    {
        var result = await _mediator.Send(new GetPromotionalCodesOfUserQuery(userId));
        if (result == null || !result.Any())
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{promoCodeId:long}/assign-user/{userId:long}")]
    public async Task<IActionResult> AssignUserToPromotionalCode(long promoCodeId, long userId)
    {
        await _mediator.Send(new AssignUserToPromotionalCodeCommand(promoCodeId, userId));;
        return Ok();
    }

    [HttpDelete("{promoCodeId:long}/remove-user/{userId:long}")]
    public async Task<IActionResult> RemoveUserFromPromotionalCode(long promoCodeId, long userId)
    {
        await _mediator.Send(new RemoveUserFromPromotionalCodeCommand(promoCodeId, userId));
        return Ok();
    }
}
