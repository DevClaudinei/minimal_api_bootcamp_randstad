using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using minimal_api.Application.Models.DTOs;
using minimal_api.Application.Models.ModelView;
using minimal_api.Configurations;
using minimal_api.DomainServices;
using minimal_api.DomainServices.Exceptions;
using minimal_api.DomainServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbConfiguration(builder.Configuration);

var app = builder.Build();

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion 

#region Administrators
app.MapPost("/administrators/login/{loginDto}", ([FromBody] LoginDto loginDto, IAdministratorService administratorService) => {
    try
    {
        var admin = administratorService.Login(loginDto);
        return Results.Ok($"Login successful. {admin.Id}");
    }
    catch (BadRequestException ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithTags("Administrators");

app.MapPost("/administrators/", (CreateAdministrator createAdministrator, IAdministratorService administratorService) => {
    try
    {
        var admin = administratorService.Insert(createAdministrator);
        return Results.Ok($"Create administrator with Id: {admin}");
    }
    catch (BadRequestException ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithTags("Administrators");

app.MapGet("/administrators/", ([FromQuery] int? page, IAdministratorService administratorService) => 
{
    var administrators = administratorService.GetAll(page);

    return Results.Ok(administrators);
}).WithTags("Administrators");
#endregion

#region Vehicles
app.MapGet("/vehicles/{id:long}", (long id, IVehicleService vehicleService) => 
{
    try
    {
        var vehicle = vehicleService.GetById(id);
        return Results.Ok(vehicle);
    }
    catch (NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
}).WithTags("Vehicles");

app.MapPost("/vehicles", (VehicleDto vehicleDto, IVehicleService vehicleService) => 
{
    var id = vehicleService.Insert(vehicleDto);
    return Results.Created($"/vehicles/{id}", vehicleDto);
}).WithTags("Vehicles");

app.MapGet("/vehicles", (int? page, IVehicleService vehicleService) => 
{
    var vehicles = vehicleService.GetAll(page);
    return Results.Ok(vehicles);
}).WithTags("Vehicles");

app.MapPut("/vehicles/{id:long}", (long id, VehicleDto vehicleDto, IVehicleService vehicleService) => 
{
    try
    {
        vehicleService.Update(id, vehicleDto);
        return Results.NoContent();
    }
    catch (NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
}).WithTags("Vehicles");

app.MapDelete("/vehicles/{id:long}", (long id, IVehicleService vehicleService) => 
{
    try
    {
        vehicleService.Delete(id);
        return Results.NoContent();
    }
    catch (NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
}).WithTags("Vehicles");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion
