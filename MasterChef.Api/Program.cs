using MasterChef.Contracts.Data;
using MasterChef.Contracts.Services;
using MasterChef.Data.Repositories;
using MasterChef.Database;
using MasterChef.Log;
using MasterChef.Services.Receitas;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

LogService.Configure(builder.Host);
try
{
    // Add services to the container.

    builder.Services.AddControllers();
    //.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

    builder.Services.AddDbContext<MainDbContext>();
    builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
    builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
    builder.Services.AddScoped<IReceitaService, ReceitaService>();
    builder.Services.AddScoped<IFotoService, FotoService>();

    builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = "Jwt";
        o.DefaultChallengeScheme = "Jwt";
    }).AddJwtBearer("Jwt", o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidAudience = "clients-api",
            ValidIssuer = "api",
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Token:SecretKey"))),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "Jwt",
            In = ParameterLocation.Header,
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
finally
{
    LogService.CloseAndFlush();
}