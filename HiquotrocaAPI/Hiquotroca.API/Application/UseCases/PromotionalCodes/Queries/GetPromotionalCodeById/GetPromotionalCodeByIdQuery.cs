using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodeById;

public record GetPromotionalCodeByIdQuery(long Id) : IRequest<PromotionalCodeDto?>;
