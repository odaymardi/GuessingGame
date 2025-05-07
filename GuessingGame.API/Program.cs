using GuessingGame.API.Middleware;
using GuessingGame.API.Services;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRandomNumberGenerator, RandomNumberGenerator>();

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GuessingGame API", Version = "v1" });

    c.AddSecurityDefinition("Player-Id", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Player-Id",
        Type = SecuritySchemeType.ApiKey,
        Description = "Player identifier (GUID)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Player-Id"
                }
            },
            new List<string>()
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<PlayerIdentificationMiddleware>();
app.MapControllers();
app.Run();
