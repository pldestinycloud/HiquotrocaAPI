using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.RemoveUserFromPromotionalCode;

public record RemoveUserFromPromotionalCodeCommand(long PromoCodeId, long UserId) : IRequest<bool>;
