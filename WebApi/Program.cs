using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Data;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<PlayerDbContext>(options => options.UseInMemoryDatabase("PlayerDb"));
builder.Services.AddDbContext<PlayerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlayerDbConnectionStrings")));
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddMemoryCache();
//builder.Services.AddScoped<IMemoryCache, MemoryCache>();

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
