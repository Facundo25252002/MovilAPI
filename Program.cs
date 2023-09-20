using MovilAPI.Models.Data;
using MovilAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Connections;
using MovilAPI.Models.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cadena de conexion puede ser local o la del servidor .depende del nombre que  le ponga en GetConnectionString();
builder.Services.AddDbContext<MovilContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
   

});

// para usar  el repositorio
builder.Services.AddTransient<IProductoRepository,ProductosRepository>();
builder.Services.AddTransient<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddTransient<LoginRequest>();
builder.Services.AddTransient<LoginResponse>(); 

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


