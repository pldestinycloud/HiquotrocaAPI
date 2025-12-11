using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodesByIds;

public record GetPromotionalCodesByIdsQuery(List<long> PromoCodesIds) : IRequest<List<Domain.Entities.PromotionalCode>>;
