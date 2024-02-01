using AirBnB.Application.Ratings.Services;
using AirBnB.Application.Ratings.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Ratings.Services;

public class RatingProcessingScheduler(IServiceScopeFactory scopeFactory, 
    IOptions<RatingProcessingSchedulerSettings> backgroundServiceSettings)
    : BackgroundService
{
    private readonly RatingProcessingSchedulerSettings _backgroundServiceSettings = backgroundServiceSettings.Value;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await RunRatingsProcessingService();
        
        using PeriodicTimer timer = new(TimeSpan
            .FromSeconds(_backgroundServiceSettings.ExecutionIntervalInSeconds));
        
        while (await timer.WaitForNextTickAsync(stoppingToken))
            await RunRatingsProcessingService();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await RunRatingsProcessingService();
    }

    private async ValueTask RunRatingsProcessingService()
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var ratingRecalculationService = scope.ServiceProvider.GetRequiredService<IRatingProcessingService>();
        await ratingRecalculationService.ProcessListingsRatings();
    }
}