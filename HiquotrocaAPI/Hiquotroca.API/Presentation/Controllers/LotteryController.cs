using Hiquotroca.API.Application.Features.Lotteries.Commands.CreateLottery;
using Hiquotroca.API.Application.Features.Lotteries.Commands.PurchaseTicket;
using Hiquotroca.API.Application.Features.Lotteries.Commands.UpdateLottery;
using Hiquotroca.API.Application.Features.Lotteries.Queries.GetAllLotteries;
using Hiquotroca.API.Application.Features.Lotteries.Queries.GetLotteryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotteryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LotteryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllLotteriesQuery());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _mediator.Send(new GetLotteryByIdQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLotteryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateLotteryCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:long}/purchase-ticket/{userId:long}")]
    public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseTicketCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}