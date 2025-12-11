using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetAllPromotionalCodes;

public record GetAllPromotionalCodesQuery() : IRequest<List<Domain.Entities.PromotionalCode>>;
