using AirBnB.Application.Common.Settings;
using AirBnB.Application.Ratings.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Ratings.Services;

public class RatingRecalculationScheduler(IServiceScopeFactory scopeFactory, 
    IOptions<BackgroundServiceSettings> backgroundServiceSettings)
    : BackgroundService
{
    private readonly BackgroundServiceSettings _backgroundServiceSettings = backgroundServiceSettings.Value;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await RunRatingsRecalculationService();
        
        using PeriodicTimer timer = new(TimeSpan
            .FromSeconds(_backgroundServiceSettings.ListingRatingRecalculationIntervalInHours));
        
        while (await timer.WaitForNextTickAsync(stoppingToken))
            await RunRatingsRecalculationService();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await RunRatingsRecalculationService();
    }

    private async ValueTask RunRatingsRecalculationService()
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var ratingRecalculationService = scope.ServiceProvider.GetRequiredService<IRatingRecalculationService>();
        await ratingRecalculationService.RecalculateListingsRatings();
    }
}