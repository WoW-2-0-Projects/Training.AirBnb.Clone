using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class LocationSeedData
{
    public static async ValueTask InitializeLocationSeedData(this IDataContext context)
    {
        if(!context.Countries.Any())
            await context.AddCountries();
    }
    public static async ValueTask AddCountries(this IDataContext context)
    {
        var countries = new List<Country>
        {
            new Country { Name = "Australia", CountryDialingCode = "+61", RegionPhoneNumberLength = 9},
            new Country { Name = "Canada", CountryDialingCode = "+1", RegionPhoneNumberLength = 10},
            new Country { Name = "Egypt", CountryDialingCode = "+20", RegionPhoneNumberLength = 10},
            new Country { Name = "France", CountryDialingCode = "+33", RegionPhoneNumberLength = 9},
            new Country { Name = "Germany", CountryDialingCode = "+49", RegionPhoneNumberLength = 10},
            new Country { Name = "Spain", CountryDialingCode = "+34", RegionPhoneNumberLength = 10},
            new Country { Name = "Turkey", CountryDialingCode = "+90", RegionPhoneNumberLength = 11},
            new Country { Name = "USA", CountryDialingCode = "+1", RegionPhoneNumberLength = 10},
            new Country { Name = "Greenland", CountryDialingCode = "+299", RegionPhoneNumberLength = 6},
            new Country { Name = "Russia", CountryDialingCode = "+7", RegionPhoneNumberLength = 10},
            new Country { Name = "Azerbaijan", CountryDialingCode = "+994", RegionPhoneNumberLength = 9},
            new Country { Name = "Brazil", CountryDialingCode = "+55", RegionPhoneNumberLength = 11},
            new Country { Name = "Colombia", CountryDialingCode = "+57", RegionPhoneNumberLength = 10},
            new Country { Name = "India", CountryDialingCode = "+91", RegionPhoneNumberLength = 10},
            new Country { Name = "Japan", CountryDialingCode = "+81", RegionPhoneNumberLength = 10},
            new Country { Name = "Uzbekistan", CountryDialingCode = "+998", RegionPhoneNumberLength = 9},
            new Country { Name = "Kazakhstan", CountryDialingCode = "+7", RegionPhoneNumberLength = 10},
            new Country { Name = "Kyrgyzstan", CountryDialingCode = "+996", RegionPhoneNumberLength = 9},
            new Country { Name = "Turkmenistan", CountryDialingCode = "+993", RegionPhoneNumberLength = 8},
            new Country { Name = "United Arab Emirates", CountryDialingCode = "+971", RegionPhoneNumberLength = 10}
        };
        //phone number uzunliklariga code ni uzunligi qo'shilmagan

        await context.AddCities(countries);

        await context.Countries.AddRangeAsync(countries);
        await context.SaveChangesAsync();
    }

    public static async ValueTask AddCities(this IDataContext context, List<Country> countries)
    {
        var cities = new List<City>
        {
            new City{ Name = "Casey"},
            new City{ Name = "Alberta"},
            new City{ Name = "Alexandria"},
            new City{ Name = "Paris"},
            new City{ Name = "Berlin"},
            new City{ Name = "Madrid"},
            new City{ Name = "Ankara"},
            new City{ Name = "Washington"},
            new City{ Name = "Nuuk"},
            new City{ Name = "Moscow"},
            new City{ Name = "Baku"},
            new City{ Name = "Brasilia"},
            new City{ Name = "Bogota"},
            new City{ Name = "New Delhi"},
            new City{ Name = "Tokyo"},
            new City{ Name = "Tashkent"},
            new City{ Name = "Nur Sultan"},
            new City{ Name = "Bishkek"},
            new City{ Name = "Ashgabat"},
            new City{ Name = "Abu Dhabi"},
        };
        for (int i = 0; i < 20; i++)
            cities[i].CountryId = countries[i].Id;

        await context.Cities.AddRangeAsync(cities);
        await context.SaveChangesAsync();
    }
}
