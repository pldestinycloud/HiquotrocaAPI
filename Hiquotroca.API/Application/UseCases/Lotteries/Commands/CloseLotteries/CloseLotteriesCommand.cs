using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.CloseLottery;

public record CloseLotteriesCommand(List<long> Ids) : IRequest;