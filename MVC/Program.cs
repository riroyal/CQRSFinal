using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MVC.Data;
using MVC.Features.Players.Commands;
using MVC.Features.Players.Queries;
using MVC.Models;
using MVC.Services;
using System.Reflection;
using static MVC.Features.Players.Commands.CreatePlayerCommand;
using static MVC.Features.Players.Commands.DeletePlayerCommand;
using static MVC.Features.Players.Commands.UpdatePlayerCommand;
using static MVC.Features.Players.Queries.GetAllPlayersQuery;
using static MVC.Features.Players.Queries.GetPlayerByIdQuery;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<PlayerDbContext>(options => options.UseInMemoryDatabase("PlayerDb"));
builder.Services.AddDbContext<PlayerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlayerDbConnectionStrings")));
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGetAllPlayersQuery2, GetAllPlayersQuery2>();
builder.Services.AddScoped<IMediator, Mediator>(); //registering MediatR and all required dependencies
                                                              //registering handlers
builder.Services.AddScoped<IRequestHandler<GetAllPlayersQuery, IEnumerable<PlayerDto>>, GetAllPlayersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetPlayerByIdQuery, PlayerDto>, GetPlayerByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreatePlayerCommand, PlayerDto>, CreatePlayerCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdatePlayerCommand, PlayerDto>, UpdatePlayerCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeletePlayerCommand, int>, DeletePlayerCommandHandler>();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
