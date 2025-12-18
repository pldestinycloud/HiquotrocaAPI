using MediatR;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Commands.DeletePromotionalCode;

public record DeletePromotionalCodeCommand(long Id) : IRequest;
