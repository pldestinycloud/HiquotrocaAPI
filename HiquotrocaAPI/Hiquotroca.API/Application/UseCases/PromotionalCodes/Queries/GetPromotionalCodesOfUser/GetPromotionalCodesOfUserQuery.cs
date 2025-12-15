using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodesOfUser;

public record GetPromotionalCodesOfUserQuery(long UserId) : IRequest<List<PromotionalCodeDto>>;
