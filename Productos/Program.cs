using Microsoft.EntityFrameworkCore;
using Productos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Agrega los servicios de controladores al contenedor de dependencias.
// Esto permite que la API pueda manejar solicitudes HTTP y enviar respuestas en formato JSON.
builder.Services.AddControllers();

// Configura el contexto de la base de datos "ProductosContext" para usar SQL Server.
// "UseSqlServer" obtiene la cadena de conexión de la configuración del proyecto.
builder.Services.AddDbContext<ProductosContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
