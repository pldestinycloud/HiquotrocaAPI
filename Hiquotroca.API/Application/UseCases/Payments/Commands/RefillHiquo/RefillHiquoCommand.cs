using MediatR;

namespace Hiquotroca.API.Application.UseCases.Payments.Commands.RefillHiquo;

public record RefillHiquoCommand(
    long UserId,
    float Quantity,
    string? PromoCode
) : IRequest;