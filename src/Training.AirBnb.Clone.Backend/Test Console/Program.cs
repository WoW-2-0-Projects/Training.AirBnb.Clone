using Backend_Project.Persistance.DataContexts;
using FileBaseContext.Context.Models.Configurations;

var contextOptions = new FileContextOptions<AppFileContext>(Path.Combine("Data", "DataStorage"));
var context = new AppFileContext(contextOptions);
context.FetchAsync().AsTask().Wait();

