using System.Text.Json.Serialization;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Implementations;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;
using Labo08_LeticiaMaldonado.Services.Implementations;
using Labo08_LeticiaMaldonado.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Labo08_LeticiaMaldonado.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Lab08DbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=root;";
    options.UseNpgsql(connectionString);
});


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Labo08 LINQ API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at application root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();