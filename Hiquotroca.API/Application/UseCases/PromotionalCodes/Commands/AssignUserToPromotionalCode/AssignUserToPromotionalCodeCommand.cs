using MediatR;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Commands.AssignUserToPromotionalCode;

public record AssignUserToPromotionalCodeCommand(long PromoCodeId, long UserId) : IRequest;
