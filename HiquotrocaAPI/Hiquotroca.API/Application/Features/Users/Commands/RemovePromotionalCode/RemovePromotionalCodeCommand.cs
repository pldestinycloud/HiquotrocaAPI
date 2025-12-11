using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.RemovePromotionalCode;

public record RemovePromotionalCodeCommand(long UserId, long PromoCodeId) : IRequest;
