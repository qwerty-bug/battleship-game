using Battleship;
using Battleship.Abstraction;
using Battleship.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services
        .AddSingleton<IGameScheduler,GameScheduler>()
        .AddSingleton<IUIEngine,UIEngine>()
        .AddSingleton<IShipGridMappingService, ShipGridMappingService>()
        .AddSingleton<IInputService,InputService>()
        .AddHostedService<GameHostedService>();
    }).UseConsoleLifetime();

builder.RunConsoleAsync();
