using DataBaseApplication.Models;
using DataBaseApplication.Repositories.DiscordServers;
using DataBaseApplication.Repositories.Streamer;
using DataBaseApplication.Repositories.StreamerXServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchService.Services.Auth;
using TwitchService.Services.GeneralServices.User;

namespace ApplicationDependecy;

public static class AppDependencies
{
    public static IServiceCollection AddDependecy(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TokenTwitch>();
        services.AddScoped<IUserRequest,UserRequest>();
        
        services.AddScoped<IStreamerRepositore, StreamerRepositore>();
        services.AddScoped<IDiscordServerRepos,DiscordServerRepos>();
        services.AddScoped<IStreamerServer,StreamerServer>();
        

        services.AddDbContext<DiscordBotAplicationContext>(options => options.UseNpgsql(configuration["StringConnection"]),ServiceLifetime.Transient);   

        services.AddHttpClient("UriTokenTwitch", config =>
         {
            config.BaseAddress = new Uri(configuration["UriTwitch"]);
            config.DefaultRequestHeaders.Add("Client-Id", configuration["IdClientTwitch"]);                        
         });         
        services.AddHttpClient("UriTwitchApi",config =>
        {
            config.BaseAddress = new Uri(configuration["UriTwitchStream"]);
            config.DefaultRequestHeaders.Add("Client-Id", configuration["IdClientTwitch"]);            
        });      
            
        return services;
    }
}
