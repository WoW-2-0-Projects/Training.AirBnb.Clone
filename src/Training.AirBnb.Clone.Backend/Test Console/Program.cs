using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Services;
using Backend_Project.Persistance.DataContexts;
using FileBaseContext.Context.Models.Configurations;

var contextOptions = new FileContextOptions<AppFileContext>(Path.Combine("Data", "DataStorage"));
var context = new AppFileContext(contextOptions);
context.FetchAsync().AsTask().Wait();
