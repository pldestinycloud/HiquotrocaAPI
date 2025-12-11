using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserPromotionalCodes;

public record GetUserPromotionalCodesQuery(long UserId) : IRequest<List<long>>;
