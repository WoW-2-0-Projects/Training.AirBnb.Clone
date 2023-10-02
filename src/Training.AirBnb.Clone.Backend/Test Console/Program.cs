using Backend_Project.Domain.Entities;
using Backend_Project.Infrastructure.Services.LocationServices;
using Backend_Project.Persistance.DataContexts;
using FileBaseContext.Context.Models.Configurations;

var contextOptions = new FileContextOptions<AppFileContext>(Path.Combine("Data", "DataStorage"));
var context = new AppFileContext(contextOptions);
context.FetchAsync().AsTask().Wait();

//var countryService = new CountryService(context);

//var country  = new Country("Ozbekiston ","+998",7);
//var country2 = new Country("Tursa", "+563", 12);
//var country3 = new Country("fda", "+66d",16);
//var country4 = new Country("fsda", "+664", 4);



//await countryService.CreateAsync(country);
//await countryService.CreateAsync(country2);
//await countryService.CreateAsync(country3);
//await countryService.CreateAsync(country4);