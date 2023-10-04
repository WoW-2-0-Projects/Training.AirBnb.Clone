using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Infrastructure.CompositionServices;
using Backend_Project.Infrastructure.Services;
using Backend_Project.Infrastructure.Services.AccountServices;
using Backend_Project.Infrastructure.Services.ListingServices;
using Backend_Project.Infrastructure.Services.NotificationsServices;
using Backend_Project.Persistance.DataContexts;
using FileBaseContext.Context.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataContext, AppFileContext>(_ =>
{
    var contextOptions = new FileContextOptions<AppFileContext>
    {
        StorageRootPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "DataStorage")
    };

    var context = new AppFileContext(contextOptions);
    context.FetchAsync().AsTask().Wait();

    return context;
});

builder.Services.AddScoped<IEntityBaseService<User>, UserService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<IEntityBaseService<ListingCategory>, ListingCategoryService>();
builder.Services.AddScoped<IListingCategoryDetailsService, ListingCategoryDetailsService>();
builder.Services.AddScoped<IEntityBaseService<ListingFeature>, ListingFeatureService>();
builder.Services.AddScoped<IEntityBaseService<ListingFeatureOption>, ListingFeatureOptionService>();
builder.Services.AddScoped<IEntityBaseService<ListingCategoryFeatureOption>, ListingCategoryFeatureOptionService>();
builder.Services.AddScoped<IEntityBaseService<Listing>, ListingService>();
builder.Services.AddScoped<IEntityBaseService<Email>, EmailService>();
builder.Services.AddScoped<IEntityBaseService<EmailTemplate>, EmailTemplateService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();