using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class ListingRulesSeedData
{
    public static async ValueTask InitializeListingRulesSeedData(this IDataContext context)
    {
        if (!context.ListingRules.Any())
            await context.AddListingRules(1000);
    }

    private static async ValueTask AddListingRules(this IDataContext context, int count)
    {
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            TimeOnly? checkInTimeStart = null;
            TimeOnly? checkInTimeEnd = null;

            if (random.NextDouble() < 0.5)
            {
                var checkInTimeStartHour = random.Next(0, 22); 
                var checkInTimeStartMinutes = 0;
                checkInTimeStart = new TimeOnly(checkInTimeStartHour, checkInTimeStartMinutes);

                if ( checkInTimeStart != null && random.NextDouble() < 0.5)
                {
                    var minCheckInTimeEndHour = checkInTimeStartHour + 2; 
                    var checkInTimeEndHour = random.Next(minCheckInTimeEndHour, 24);
                    var checkInTimeEndMinutes = 0;
                    checkInTimeEnd = new TimeOnly(checkInTimeEndHour, checkInTimeEndMinutes);
                }

                else if(checkInTimeStart == null)
                {
                    checkInTimeEnd = null;
                }
            }

            var listingRule = new ListingRules
            {
                Guests = random.Next(1, 10),  
                PetsAllowed = random.NextDouble() < 0.5, 
                EventsAllowed = random.NextDouble() < 0.5,  
                SmokingAllowed = random.NextDouble() < 0.5,  
                CommercialFilmingAllowed = random.NextDouble() < 0.5,  
                CheckInTimeStart = checkInTimeStart,  
                CheckInTimeEnd = checkInTimeEnd,  
                CheckOutTime = new TimeOnly(random.Next(0, 24), random.Next(0, 0)),  
                AdditionalRules = "Random rule " + i  
            };

            await context.ListingRules.AddAsync(listingRule);
        }

        await context.SaveChangesAsync();
    }
}
