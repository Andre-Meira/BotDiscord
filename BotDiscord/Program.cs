using ApplicationDependecy;
using BotDiscord.Services.Applications;
using BotDiscord.Services.DiscordApplication;
using BotDiscord.Services.HostHandler;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace BotDiscord;

class Program
{
    static async Task Main(string[] args)
    {
        var config1 = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

        var builder = new HostBuilder()
            .ConfigureAppConfiguration(appConfig =>
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();
                appConfig.AddConfiguration(configuration);
            })
            .ConfigureLogging(ConfigLoggin =>
            {
                ConfigLoggin.AddConsole();
                    // ConfigLoggin.SetMinimumLevel(LogLevel.Debug);                    
                })
            .ConfigureDiscordHost((context, config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = Discord.LogSeverity.Debug,
                    AlwaysDownloadUsers = false,
                    MessageCacheSize = 200,
                };
                config.Token = context.Configuration["TokenBot"];
            })
            .UseCommandService((context, config) =>
            {
                config.CaseSensitiveCommands = false;
                config.LogLevel = Discord.LogSeverity.Debug;
                config.DefaultRunMode = Discord.Commands.RunMode.Sync;
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDependecy(config1);
                services.AddScoped<IDiscordService,DiscordService>();      

                services.AddHostedService<TokenAcess>();                
                services.AddHostedService<CommandHandler>();
                services.AddHostedService<CheckedStreamerON>();

            }).UseConsoleLifetime();

        var host = builder.Build();

        await host.RunAsync();
    }
}