using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Queries.GetPromotionalCodeById;

public record GetPromotionalCodeByCodeQuery(string Code) : IRequest<PromotionalCodeDto?>;
