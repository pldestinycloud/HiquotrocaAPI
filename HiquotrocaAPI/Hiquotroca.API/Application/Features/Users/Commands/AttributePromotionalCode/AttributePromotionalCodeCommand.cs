using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.AttributePromotionalCode;

public record AttributePromotionalCodeCommand(long UserId, long PromoCodeId) : IRequest;
