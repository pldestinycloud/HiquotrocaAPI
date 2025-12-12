using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.CreatePromotionalCode;

public record CreatePromotionalCodeCommand(
    string Code,
    DateTime ExpiryDate) : IRequest;
