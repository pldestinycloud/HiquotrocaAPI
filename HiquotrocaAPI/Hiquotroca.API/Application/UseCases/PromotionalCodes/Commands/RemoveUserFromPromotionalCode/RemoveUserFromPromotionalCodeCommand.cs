using MediatR;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Commands.RemoveUserFromPromotionalCode;

public record RemoveUserFromPromotionalCodeCommand(long PromoCodeId, long UserId) : IRequest;
