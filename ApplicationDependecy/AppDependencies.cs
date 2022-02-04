﻿using Microsoft.Extensions.Configuration;
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
