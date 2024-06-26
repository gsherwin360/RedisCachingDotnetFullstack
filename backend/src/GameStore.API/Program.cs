using GameStore.API.Caching;
using GameStore.API.Data;
using GameStore.API.Endpoints;
using GameStore.API.Extensions;
using GameStore.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameStoreDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("GameStore")));

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
	options.InstanceName = builder.Configuration["Redis:InstanceName"];
});

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API v1");
		c.RoutePrefix = string.Empty;
	});
	await app.MigrateDbAsync();
}

app.MapGamesEndpoint();
app.MapGenresEndpoint();

app.Run();