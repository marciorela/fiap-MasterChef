using MasterChef.Contracts.Data;
using MasterChef.Contracts.Services;
using MasterChef.Data.Repositories;
using MasterChef.Database;
using MasterChef.Services.Receitas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MainDbContext>();
builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
builder.Services.AddScoped<IReceitaService, ReceitaService>();

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
