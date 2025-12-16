using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Queries.GetAllPromotionalCodes;

public record GetAllPromotionalCodesQuery() : IRequest<List<PromotionalCodeDto>>;
