using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class AvailabilitySeedData
{
    public static async ValueTask InitializeAvailabilitySeedData(this IDataContext context)
    {
        if (!context.Availabilities.Any())
            await context.InitializeAvailability(1000);
    }

    private static async ValueTask InitializeAvailability(this IDataContext context, int count)
    {
        var availabilityList = new List<Availability>();

        int minNights, maxNights;

        var random = new Random();

        for (int index = 0; index < count; index++)
        {
            minNights = random.Next(0, 729);
            maxNights = random.Next(minNights, 730);

            availabilityList.Add(new Availability()
            {
                MinNights = minNights,
                MaxNights = maxNights,
                PreparationDays = random.Next(0, 3),
                AvailabilityWindow = random.Next(3, 24),
            });
        }

        await context.Availabilities.AddRangeAsync(availabilityList);
        await context.SaveChangesAsync();
    }
}