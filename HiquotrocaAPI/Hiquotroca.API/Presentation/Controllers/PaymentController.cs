using Hiquotroca.API.Application.UseCases.Payments.Commands.RefillHiquo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("refill-hiquos")]
    public async Task<IActionResult> RefillHiquos([FromBody] RefillHiquoCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
