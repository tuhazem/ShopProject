using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Mapping;
using ShopProject.Infrastructure.Persistence;
using FluentValidation;

using System.Data;
using MediatR;
using ShopProject.Application.Features.Products.Commands.Behavior;
using ShopProject.API.Exceptions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IApplicationDbContext>(provider =>
provider.GetRequiredService<ApplicationDbContext>()
);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>) , typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(IApplicationDbContext).Assembly);

builder.Services.AddControllers().AddJsonOptions(options =>
{

    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

