using FluentValidation;
using FluentValidation.AspNetCore;
using RestaurantApi.Core.Application;
using RestaurantApi.Extensions;
using RestaurantApi.Infrastructure.Persistence;
using RestaurantApi.Infrastructure.Identity;
using RestaurantApi.Infrastructure.Shared;
using System.Text.Json.Serialization;
using RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateIngredientCommandValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

await app.RunIdentitySeeds();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
