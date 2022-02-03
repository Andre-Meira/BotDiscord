using BotDiscord.Services.DiscordApplication;
using BotDiscord.Services.HostHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.GeneralServices.User;

namespace BotDiscord.Services;

public class CheckedStreamerON : TwitchHandler
{
    private readonly ILogger<CheckedStreamerON> _logger;
    private readonly IUserRequest _userTwitch;
    private readonly IConfiguration _config;    
    
    private readonly DiscordMensagem _discord; 

    public CheckedStreamerON(DiscordMensagem discord,IUserRequest userTwitch, IConfiguration config,
        ILogger<CheckedStreamerON> logger) : base(logger)
    {
        _logger = logger;
        _userTwitch = userTwitch;
        _config = config;        
        _discord = discord;
    }
    
    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        do
        {
            try
            {
                if (Token != null)
                {
                    await Task.Run(async () =>
                        {
                            await StreamerOn();
                        }, cancellationToken);

                    await Task.Delay(TimeSpan.FromHours(1.5));                        
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Error: {err.Message}... {err.Data}");
            }

        }
        while (!cancellationToken.IsCancellationRequested);
    }

    private async Task StreamerOn()
    {
        string[] listStreamers = _config.GetSection("StreamerPermission").Get<string[]>();        

        for (int count = 0; count < listStreamers.Length; count++)
        {
            try
            {                
                ObjectStreamerOn objectStreamerOn = await _userTwitch.GetStreamAsync(Token.access_token, listStreamers[count]);                                                                                                
                ObjectStreamerInfo objectStreamerInfo =  await _userTwitch.GetInfoAsync(Token.access_token, listStreamers[count]); 

                if(objectStreamerOn.data.Length != 0)
                {
                    await _discord.SendAsync(objectStreamerInfo,objectStreamerOn);
                }
                    
            }
            catch (System.Exception err)
            {
                System.Console.WriteLine(err);
                return;
            }
        }
    }
}