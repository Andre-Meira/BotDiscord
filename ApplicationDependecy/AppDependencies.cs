using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchService.Services.Auth;
using TwitchService.Services.GeneralServices;

namespace ApplicationDependecy;
public static class AppDependencies
{
    public static IServiceCollection AddDependecy(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GenerateToken>();
        services.AddScoped<StreamerOn>();
        services.AddScoped<RefreshToken>();
        
        services.AddHttpClient("ApiTwitch", config =>
         {
            config.BaseAddress = new Uri(configuration["UriTwitch"]);
            config.DefaultRequestHeaders.Add("Client-Id", configuration["IdClientTwitch"]);                        
         });         
        services.AddHttpClient<StreamerOn>(config =>
        {
            config.BaseAddress = new Uri(configuration["UriTwitchStream"]);
            config.DefaultRequestHeaders.Add("Client-Id", configuration["IdClientTwitch"]);            
        });      
            
        return services;
    }
}
