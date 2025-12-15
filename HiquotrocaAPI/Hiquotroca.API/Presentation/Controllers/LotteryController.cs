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
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateLotteryCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("purchase-ticket")]
    public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseTicketCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok(new { message = "Ticket comprado com sucesso!" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}