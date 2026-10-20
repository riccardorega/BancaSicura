
using Banca.Conti.Business;
using Banca.Conti.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrazione dei Controller
builder.Services.AddControllers();

// 2. Configurazione OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Registrazione di Entity Framework Core (Repository Layer)
builder.Services.AddDbContext<ContiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContiDb")));

builder.Services.AddScoped<IContiService, ContiService>();


var app = builder.Build();

// 5. Pipeline HTTP: Abilita Swagger in ambiente di sviluppo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banca.Conti.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();