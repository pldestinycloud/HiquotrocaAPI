using Hiquotroca.API.DTOs.Lotteries;
using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Queries.GetLotteryById;

public record GetLotteryByIdQuery(long Id) : IRequest<LotteryDto?>;