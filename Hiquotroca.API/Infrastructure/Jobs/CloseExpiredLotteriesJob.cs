using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hiquotroca.API.Application.UseCases.Lotteries.Commands.CloseLottery;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hiquotroca.API.Infrastructure.Jobs;

public class CloseExpiredLotteriesJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CloseExpiredLotteriesJob> _logger;

    public CloseExpiredLotteriesJob(IServiceProvider serviceProvider, ILogger<CloseExpiredLotteriesJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var nextRunAt = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 3, 0);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("CloseExpiredLotteriesJob started at {StartTime}", DateTime.UtcNow);
            try
            {
                var currentTime = DateTime.UtcNow;
                nextRunAt = nextRunAt.AddDays(1);

                var delay = nextRunAt - currentTime;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var expiredLotteries = await dbContext.Lotteries
                        .Where(l => l.IsActive && l.ExpiryDate <= DateTime.UtcNow)
                        .Select(l => l.Id)
                        .ToListAsync(stoppingToken);

                    if(expiredLotteries.Any())  
                        await mediator.Send(new CloseLotteriesCommand(expiredLotteries), stoppingToken);
                }

                _logger.LogInformation("CloseExpiredLotteriesJob completed. Next run at: {NextRunAt}", nextRunAt);
                await Task.Delay(delay, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // Graceful shutdown
            }
            catch (Exception ex)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception innerEx)
                {
                    continue;
                }
            }
        }
    }
}
