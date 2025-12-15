using Hiquotroca.API.DTOs.Lotteries;
using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Queries.GetAllLotteries;

public record GetAllLotteriesQuery() : IRequest<List<LotteryDto>>;