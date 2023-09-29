using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Services;
using Backend_Project.Persistance.DataContexts;
using FileBaseContext.Context.Models.Configurations;

var contextOptions = new FileContextOptions<AppFileContext>(Path.Combine("Data", "DataStorage"));
var context = new AppFileContext(contextOptions);
context.FetchAsync().AsTask().Wait();

var reservationService = new ReservationService(context);

var validReservation = new Reservation(new Guid("5ef122d8-0b88-4f1c-91f5-ab999bc8b4b0"), Guid.NewGuid(),Guid.NewGuid()
    ,new DateTime(2023,10,10), new DateTime(2023,10,11),5000);

await reservationService.CreateAsync(validReservation);
