using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;

public record UpdatePromotionalCodeCommand(
    long promotionalCodeId,
    string code,
    DateTime expiryDate,
    bool isActive) : IRequest;
