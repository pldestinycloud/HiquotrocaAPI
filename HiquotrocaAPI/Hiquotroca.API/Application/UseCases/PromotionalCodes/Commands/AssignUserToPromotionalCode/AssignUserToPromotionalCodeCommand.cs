using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.AssignUserToPromotionalCode;

public record AssignUserToPromotionalCodeCommand(long PromoCodeId, long UserId) : IRequest;
