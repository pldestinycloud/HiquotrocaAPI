using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.DeletePromotionalCode;

public record DeletePromotionalCodeCommand(long Id) : IRequest;
