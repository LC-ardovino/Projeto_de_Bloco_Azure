using EditoraApi;
using Microsoft.EntityFrameworkCore;
using EditoraDAL.Repositories;
using EditoraAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Novo
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
//Fim novo


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFContext>(options =>
                options.UseSqlServer(connectionString));

// Dependence Injection para o Amigo 
builder.Services.AddScoped<DbContext, EFContext>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IQuadrinhoRepository, QuadrinhoRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();

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
