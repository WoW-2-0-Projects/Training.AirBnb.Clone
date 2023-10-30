using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Extensions;
using Backend_Project.Persistence.DataContexts;
using Bogus;
using System.Security.Cryptography;

namespace Backend_Project.Persistence.SeedData;

public static class LocationSeedData
{
    public static async ValueTask InitializeLocationSeedData(this IDataContext context)
    {
        if (!context.Countries.Any())
            await context.AddCountries();

        if (!context.Cities.Any())
            await context.AddCities();

        if (!context.Addresses.Any())
            await context.AddAddresses(100);

        if (!context.Locations.Any())
            await context.AddLocations(100);

        if(!context.ScenicViews.Any())
            await context.AddScenicViews();

        if (!context.LocationScenicViews.Any())
            await context.AddLocationScenicView(150);
    }
    public static async ValueTask AddCountries(this IDataContext context)
    {
        var countries = new List<Country>
        {
            new Country { Name = "Australia", CountryDialingCode = "+61", RegionPhoneNumberLength = 12},
            new Country { Name = "Canada", CountryDialingCode = "+1", RegionPhoneNumberLength = 12},
            new Country { Name = "Egypt", CountryDialingCode = "+20", RegionPhoneNumberLength = 13},
            new Country { Name = "France", CountryDialingCode = "+33", RegionPhoneNumberLength = 12},
            new Country { Name = "Germany", CountryDialingCode = "+49", RegionPhoneNumberLength = 13},
            new Country { Name = "Spain", CountryDialingCode = "+34", RegionPhoneNumberLength = 13},
            new Country { Name = "Turkey", CountryDialingCode = "+90", RegionPhoneNumberLength = 14},
            new Country { Name = "USA", CountryDialingCode = "+1", RegionPhoneNumberLength = 12},
            new Country { Name = "Greenland", CountryDialingCode = "+299", RegionPhoneNumberLength = 10},
            new Country { Name = "Russia", CountryDialingCode = "+7", RegionPhoneNumberLength = 12},
            new Country { Name = "Azerbaijan", CountryDialingCode = "+994", RegionPhoneNumberLength = 13},
            new Country { Name = "Brazil", CountryDialingCode = "+55", RegionPhoneNumberLength = 14},
            new Country { Name = "Colombia", CountryDialingCode = "+57", RegionPhoneNumberLength = 13},
            new Country { Name = "India", CountryDialingCode = "+91", RegionPhoneNumberLength = 13},
            new Country { Name = "Japan", CountryDialingCode = "+81", RegionPhoneNumberLength = 13},
            new Country { Name = "Uzbekistan", CountryDialingCode = "+998", RegionPhoneNumberLength = 13},
            new Country { Name = "Kazakhstan", CountryDialingCode = "+7", RegionPhoneNumberLength = 12},
            new Country { Name = "Kyrgyzstan", CountryDialingCode = "+996", RegionPhoneNumberLength = 13},
            new Country { Name = "Turkmenistan", CountryDialingCode = "+993", RegionPhoneNumberLength = 12},
            new Country { Name = "United Arab Emirates", CountryDialingCode = "+971", RegionPhoneNumberLength = 14}
        };

        await context.Countries.AddRangeAsync(countries);
        await context.SaveChangesAsync();
    }

    public static async ValueTask AddCities(this IDataContext context)
    {
        var countries = context.Countries.ToList();
        var cities = new List<City>
        {
            new City{ Name = "Casey", CountryId = countries[0].Id},
            new City{ Name = "Sydney", CountryId = countries[0].Id},
            new City{ Name = "Melbourne", CountryId = countries[0].Id},
            new City{ Name = "Brisbane", CountryId = countries[0].Id},
            new City{ Name = "Perth", CountryId = countries[0].Id},
            new City{ Name = "Alberta", CountryId = countries[1].Id},
            new City{ Name = "Toronto", CountryId = countries[1].Id},
            new City{ Name = "Montreal", CountryId = countries[1].Id},
            new City{ Name = "Calgary", CountryId = countries[1].Id},
            new City{ Name = "Ottawa", CountryId = countries[1].Id},
            new City{ Name = "Alexandria", CountryId = countries[2].Id},
            new City{ Name = "Aswan", CountryId = countries[2].Id},
            new City{ Name = "Luxor", CountryId = countries[2].Id},
            new City{ Name = "Sharm El Sheikh", CountryId = countries[2].Id},
            new City{ Name = "Hurghada", CountryId = countries[2].Id},
            new City{ Name = "Paris", CountryId = countries[3].Id},
            new City{ Name = "Marseille", CountryId = countries[3].Id},
            new City{ Name = "Lyon", CountryId = countries[3].Id},
            new City{ Name = "Toulouse", CountryId = countries[3].Id},
            new City{ Name = "Strasbourg", CountryId = countries[3].Id},
            new City{ Name = "Berlin", CountryId = countries[4].Id},
            new City{ Name = "Hamburg", CountryId = countries[4].Id},
            new City{ Name = "Munich", CountryId = countries[4].Id},
            new City{ Name = "Cologne", CountryId = countries[4].Id},
            new City{ Name = "Frankfurt am Main", CountryId = countries[4].Id},
            new City{ Name = "Madrid", CountryId = countries[5].Id},
            new City{ Name = "Barcelona", CountryId = countries[5].Id},
            new City{ Name = "Valencia", CountryId = countries[5].Id},
            new City{ Name = "Zaragoza", CountryId = countries[5].Id},
            new City{ Name = "Seville", CountryId = countries[5].Id},
            new City{ Name = "Ankara", CountryId = countries[6].Id},
            new City{ Name = "Istanbul", CountryId = countries[6].Id},
            new City{ Name = "Edirne", CountryId = countries[6].Id},
            new City{ Name = "Bursa", CountryId = countries[6].Id},
            new City{ Name = "Izmir", CountryId = countries[6].Id},
            new City{ Name = "Washington", CountryId = countries[7].Id},
            new City{ Name = "Alexandria", CountryId = countries[7].Id},
            new City{ Name = "Cambridge", CountryId = countries[7].Id},
            new City{ Name = "Hayward", CountryId = countries[7].Id},
            new City{ Name = "Hollywood", CountryId = countries[7].Id},
            new City{ Name = "Nuuk", CountryId = countries[8].Id},
            new City{ Name = "Sisimiut", CountryId = countries[8].Id},
            new City{ Name = "Ilulissat", CountryId = countries[8].Id},
            new City{ Name = "Qaqortoq", CountryId = countries[8].Id},
            new City{ Name = "Maniitsoq", CountryId = countries[8].Id},
            new City{ Name = "Moscow", CountryId = countries[9].Id},
            new City{ Name = "St. Petersburg", CountryId = countries[9].Id},
            new City{ Name = "Novosibirsk", CountryId = countries[9].Id},
            new City{ Name = "Yekaterinburg", CountryId = countries[9].Id},
            new City{ Name = "Nizhny Novogorod", CountryId = countries[9].Id},
            new City{ Name = "Baku", CountryId = countries[10].Id},
            new City{ Name = "Sumgait", CountryId = countries[10].Id},
            new City{ Name = "Ganja", CountryId = countries[10].Id},
            new City{ Name = "Xırdalan", CountryId = countries[10].Id},
            new City{ Name = "Mingachevir", CountryId = countries[10].Id},
            new City{ Name = "Brasilia", CountryId = countries[11].Id},
            new City{ Name = "Sao Paulo", CountryId = countries[11].Id},
            new City{ Name = "Rio de Janeiro", CountryId = countries[11].Id},
            new City{ Name = "Salvador", CountryId = countries[11].Id},
            new City{ Name = "Belo Horizonte", CountryId = countries[11].Id},
            new City{ Name = "Bogota", CountryId = countries[12].Id},
            new City{ Name = "Leticia", CountryId = countries[12].Id},
            new City{ Name = "El Encanto", CountryId = countries[12].Id},
            new City{ Name = "La Chorrera", CountryId = countries[12].Id},
            new City{ Name = "La Pedrera", CountryId = countries[12].Id},
            new City{ Name = "Delhi", CountryId = countries[13].Id},
            new City{ Name = "Bengaluru", CountryId = countries[13].Id},
            new City{ Name = "Hyderabad", CountryId = countries[13].Id},
            new City{ Name = "Visakhapatnam", CountryId = countries[13].Id},
            new City{ Name = "Lucknow", CountryId = countries[13].Id},
            new City{ Name = "Tokyo", CountryId = countries[14].Id},
            new City{ Name = "Yokohama", CountryId = countries[14].Id},
            new City{ Name = "Osaka", CountryId = countries[14].Id},
            new City{ Name = "Nagoya", CountryId = countries[14].Id},
            new City{ Name = "Sapporo", CountryId = countries[14].Id},
            new City{ Name = "Tashkent", CountryId = countries[15].Id},
            new City{ Name = "Samarkand", CountryId = countries[15].Id},
            new City{ Name = "Bukhara", CountryId = countries[15].Id},
            new City{ Name = "Andijan", CountryId = countries[15].Id},
            new City{ Name = "Namangan", CountryId = countries[15].Id},
            new City{ Name = "Nur Sultan", CountryId = countries[16].Id},
            new City{ Name = "Almaty", CountryId = countries[16].Id},
            new City{ Name = "Shymkent", CountryId = countries[16].Id},
            new City{ Name = "Karaganda", CountryId = countries[16].Id},
            new City{ Name = "Pavlodar", CountryId = countries[16].Id},
            new City{ Name = "Bishkek", CountryId = countries[17].Id},
            new City{ Name = "Cholpon-Ata", CountryId = countries[17].Id},
            new City{ Name = "Isfana", CountryId = countries[17].Id},
            new City{ Name = "Jalal-Abad", CountryId = countries[17].Id},
            new City{ Name = "Aydarken", CountryId = countries[17].Id},
            new City{ Name = "Ashgabat", CountryId = countries[18].Id},
            new City{ Name = "Turkmenabat", CountryId = countries[18].Id},
            new City{ Name = "Daşoguz", CountryId = countries[18].Id},
            new City{ Name = "Mary", CountryId = countries[18].Id},
            new City{ Name = "Gyzylarbat", CountryId = countries[18].Id},
            new City{ Name = "Abu Dhabi", CountryId = countries[19].Id},
            new City{ Name = "Dubai", CountryId = countries[19].Id},
            new City{ Name = "Sharjah", CountryId = countries[19].Id},
            new City{ Name = "Al Ain", CountryId = countries[19].Id},
            new City{ Name = "Ajman", CountryId = countries[19].Id},
        };

        await context.Cities.AddRangeAsync(cities);
        await context.SaveChangesAsync();
    }

    public static async ValueTask AddAddresses(this IDataContext context, int count)
    {
        var addressFaker = new Faker<Address>()
            .RuleFor(address => address.CityId, faker => faker.PickRandom(context.Cities.Select(city => city.Id)))
            .RuleFor(address => address.AddressLine1, faker => faker.Address.StreetAddress())
            .RuleFor(address => address.AddressLine2, faker => faker.Lorem.Word())
            .RuleFor(address => address.AddressLine3, faker => faker.Lorem.Word())
            .RuleFor(address => address.AddressLine4, faker => faker.Lorem.Word())
            .RuleFor(address => address.Province, faker => faker.Address.SecondaryAddress())
            .RuleFor(address => address.ZipCode, faker => faker.Address.ZipCode());

        await context.Addresses.AddRangeAsync(addressFaker.Generate(count));
        await context.SaveChangesAsync();
    }

    public static async ValueTask AddLocations(this IDataContext context, int count)
    {
        var addressId = new Stack<Guid>(context.Addresses.Select(address => address.Id));
        var locationFaker = new Faker<Location>()
            .RuleFor(location => location.AddressId, addressId.Pop())
            .RuleFor(location => location.NeighborhoodDescription, faker => faker.Lorem.Text())
            .RuleFor(location => location.GettingAround, faker => faker.Lorem.Text());

        await context.Locations.AddRangeAsync(locationFaker.Generate(count));
        await context.SaveChangesAsync();
    }
    public static async ValueTask AddScenicViews(this IDataContext context)
    {
        var ScenicViews = new List<ScenicView>
        {
            new ScenicView{ Name = "Bay view" },
            new ScenicView{ Name = "Beach view" },
            new ScenicView{ Name = "Canal view" },
            new ScenicView{ Name = "City skyline view" },
            new ScenicView{ Name = "Courtyard view" },
            new ScenicView{ Name = "Desert view" },
            new ScenicView{ Name = "Garden view" },
            new ScenicView{ Name = "Golf course view" },
            new ScenicView{ Name = "Harbour view" },
            new ScenicView{ Name = "Lake view" },
            new ScenicView{ Name = "Marina view" },            
            new ScenicView{ Name = "Mountain view" },
            new ScenicView{ Name = "Ocean view" },
            new ScenicView{ Name = "Park view" },
            new ScenicView{ Name = "Pool view" },
            new ScenicView{ Name = "Resort view" },
            new ScenicView{ Name = "River view" },
            new ScenicView{ Name = "Sea view" },
            new ScenicView{ Name = "Valley view" },
            new ScenicView{ Name = "Vineyard view" },
        };

        await context.ScenicViews.AddRangeAsync(ScenicViews);
        await context.SaveChangesAsync();
    }
    public static async ValueTask AddLocationScenicView(this IDataContext context, int count)
    {
        var locationScenicViewFaker = new Faker<LocationScenicViews>()
            .RuleFor(lsv => lsv.LocationId, faker => faker.PickRandom(
                context.Locations.Select(location => location.Id)))
            .RuleFor(lsv => lsv.ScenicViewId, faker => faker.PickRandom(
                context.ScenicViews.Select(scenicView => scenicView.Id)));

        await context.LocationScenicViews.AddRangeAsync(locationScenicViewFaker.Generate(count));
        await context.SaveChangesAsync();
    }
}
