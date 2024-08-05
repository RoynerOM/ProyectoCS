using Microsoft.AspNetCore.Mvc.Formatters;
using Proyecto2.API.Datos;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MokkilicoresDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLUser")));

builder.Services.AddScoped<ICrud<Cliente>>(options =>
{
    return new DatosEnMemoria<Cliente>(options.GetRequiredService<MokkilicoresDbContext>());
});

builder.Services.AddScoped<ICrud<Inventario>>(options =>
{
    return new DatosEnMemoria<Inventario>(options.GetRequiredService<MokkilicoresDbContext>());
});


builder.Services.AddScoped<ICrud<Pedido>>(options =>
{
    return new DatosEnMemoria<Pedido>(options.GetRequiredService<MokkilicoresDbContext>());
});

builder.Services.AddScoped<ICrud<Sesion>>(options =>
{
    return new DatosEnMemoria<Sesion>(options.GetRequiredService<MokkilicoresDbContext>());
});


builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
}).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed((host) => true));

app.Run();
