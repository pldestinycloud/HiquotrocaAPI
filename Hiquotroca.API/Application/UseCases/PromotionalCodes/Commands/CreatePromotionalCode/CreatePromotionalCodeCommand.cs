using Hiquotroca.API.DTOs.PromotionalCode;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Commands.CreatePromotionalCode;

public record CreatePromotionalCodeCommand(
    string Code,
    double BonusPercentage,
    DateTime ExpiryDate) : IRequest;
