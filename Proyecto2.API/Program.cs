using Microsoft.AspNetCore.Mvc.Formatters;
using Proyecto2.API.Datos;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICrud<Inventario>>(new DatosEnMemoria<Inventario>(new List<Inventario>
{
    new Inventario{ Id=1,Nombre="Cacique",Precio=1500,Stock=2,BodegaId=1,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.AguaArdiente },
    new Inventario{ Id=2,Nombre="Ponche",Precio=5000,Stock=20,BodegaId=2,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.Digestivo },
     new Inventario{ Id=3,Nombre="Cacique",Precio=1500,Stock=2,BodegaId=2,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.AguaArdiente },
    new Inventario{ Id=4,Nombre="Ponche",Precio=5000,Stock=20,BodegaId=1,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.Digestivo }
}));

builder.Services.AddSingleton<ICrud<Cliente>>(new DatosEnMemoria<Cliente>(new List<Cliente>
{
    new Cliente{
        Id=1,Identificacion="7-0288-0213",
        NombreCompleto="Melissa",
        Provincia="Alajuela",
        Canton="Upala",
        Distrito="Canalete",
        CantidadDineroTotal=20000,
        CantidadDineroUltimoAno=50000,
        CantidadDineroUltimos6Meses=50000/6,
        Contrasena = "123456"
    }
}));

builder.Services.AddSingleton<ICrud<Pedido>>(new DatosEnMemoria<Pedido>(new List<Pedido>
{
    new Pedido{
        Id=1,
        ProductoId=1,
        ClienteId=1,
         Cliente = new Cliente{
        Id=1,Identificacion="7-0288-0213",
        NombreCompleto="Melissa",
        Provincia="Alajuela",
        Canton="Upala",
        Distrito="Canalete",
        CantidadDineroTotal=20000,
        CantidadDineroUltimoAno=50000,
        CantidadDineroUltimos6Meses=50000/6,
        Contrasena = "123456"
    },
        Cantidad=2,
        FechaPedido = DateTime.Now,
        CostoSinIVA=1500*2,
        CostoTotal=(1500*2) * 1.13M,
        Estado= EstadoPedido.Facturado,
        Producto = new Inventario{ Id=1,Nombre="Cacique",Precio=1500,Stock=2,BodegaId=1,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.AguaArdiente }
        },
    new Pedido{
        Id=2,
        ProductoId=2,
        ClienteId=1,
        Cliente = new Cliente{
        Id=1,Identificacion="7-0288-0213",
        NombreCompleto="Melissa",
        Provincia="Alajuela",
        Canton="Upala",
        Distrito="Canalete",
        CantidadDineroTotal=20000,
        CantidadDineroUltimoAno=50000,
        CantidadDineroUltimos6Meses=50000/6,
        Contrasena = "123456"
    },
        Cantidad=2,
         FechaPedido = DateTime.Now,
        CostoSinIVA=1500*2,
        CostoTotal=(1500*2) * 1.13M,
        Estado= EstadoPedido.Facturado,
        Producto = new Inventario{ Id=2,Nombre="Ponche",Precio=5000,Stock=20,BodegaId=2,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.Digestivo }
        },
    new Pedido{
        Id=3,
        ProductoId=3,
        ClienteId=1,
         Cliente = new Cliente{
        Id=1,Identificacion="7-0288-0213",
        NombreCompleto="Melissa",
        Provincia="Alajuela",
        Canton="Upala",
        Distrito="Canalete",
        CantidadDineroTotal=20000,
        CantidadDineroUltimoAno=50000,
        CantidadDineroUltimos6Meses=50000/6,
        Contrasena = "123456"
    },
        Cantidad=2,
        FechaPedido = DateTime.Now,
        CostoSinIVA=1500*2,
        CostoTotal=(1500*2) * 1.13M,
        Estado= EstadoPedido.Facturado,
        Producto = new Inventario{ Id=3,Nombre="Cacique",Precio=1500,Stock=2,BodegaId=2,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.AguaArdiente }
        },
    new Pedido{
        Id=4,
        ProductoId=4,
        ClienteId=1,
         Cliente = new Cliente{
        Id=1,Identificacion="7-0288-0213",
        NombreCompleto="Melissa",
        Provincia="Alajuela",
        Canton="Upala",
        Distrito="Canalete",
        CantidadDineroTotal=20000,
        CantidadDineroUltimoAno=50000,
        CantidadDineroUltimos6Meses=50000/6,
        Contrasena = "123456"
    },
        Cantidad=2,
        FechaPedido = DateTime.Now,
        CostoSinIVA=1500*2,
        CostoTotal=(1500*2) * 1.13M,
        Estado= EstadoPedido.Facturado,
        Producto = new Inventario{ Id=4,Nombre="Ponche",Precio=5000,Stock=20,BodegaId=1,FechaIngreso= DateTime.Now,FechaVencimiento=null,TipoLicor = TipoLicor.Digestivo }
        }
}));

builder.Services.AddSingleton<ICrud<Sesion>>(new DatosEnMemoria<Sesion>(new List<Sesion> { new Sesion(0) }));

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


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

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed((host) => true));

app.Run();
