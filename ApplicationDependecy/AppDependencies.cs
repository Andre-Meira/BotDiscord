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
        services.AddHttpClient("ApiTwitch", config =>
         {
            config.BaseAddress = new Uri(configuration["UriTwitch"]);
         });
        services.AddHttpClient<StreamerOn>(config =>
        {
            config.BaseAddress = new Uri("https://api.twitch.tv/helix/streams?user_login=");
            config.DefaultRequestHeaders.Add("Client-Id", configuration["IdClientTwitch"]);
        });
        
        services.AddScoped<GenerateToken>();
        services.AddScoped<StreamerOn>();

        // var myhandlers = AppDomain.CurrentDomain.Load("TwitchService");
        // services.AddMediatR(myhandlers);
        return services;
    }
}
