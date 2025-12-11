using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;

public record UpdatePromotionalCodeCommand(UpdatePromotionalCodeDto Dto, long CurrentUserId) : IRequest<Domain.Entities.PromotionalCode?>;
