using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using Proyecto_Heladeria.Repositorio;
using Proyecto_Heladeria.Repositorio.IRepositorio;
using ProyectoHeladeria1.Repositorio;
using ProyectoHeladeria1.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AplicationDbContext>(opciones =>
                 opciones.UseSqlServer(builder.Configuration.GetConnectionString("conexionSQL")));
				 
builder.Services.AddScoped<ILoginRepositorio,LoginRepositorio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            // Reemplaza 4200 con el puerto de tu frontend (si es diferente)
            policy.WithOrigins("http://localhost:4200",
                               "https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Usar CORS
app.UseCors();

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
