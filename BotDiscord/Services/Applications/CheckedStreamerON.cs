using BotDiscord.Services.HostHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.GeneralServices.User;

namespace BotDiscord.Services.Applications;

public class CheckedStreamerON : TwitchHandler
{
    private readonly ILogger<CheckedStreamerON> _logger;
    private readonly IUserRequest _userTwitch;
    private readonly IConfiguration _config;        
    private readonly IDiscordService _discord; 

    public CheckedStreamerON(IDiscordService discord,IUserRequest userTwitch, IConfiguration config,
        ILogger<CheckedStreamerON> logger) : base(logger)
    {
        _logger = logger;
        _userTwitch = userTwitch;
        _config = config;        
        _discord = discord;
    }
    
    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (Token != null)
                {
                    await Task.Run(async () => {await StreamerOn();}
                        , cancellationToken);

                    await Task.Delay(TimeSpan.FromHours(1.5));                        
                }
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            catch (Exception err)
            {
                _logger.LogError($"Error: {err.Message}... {err.Data}");
            }
        }        
    }

    private async Task StreamerOn()
    {
        string[] listStreamers = _config.GetSection("StreamerPermission").Get<string[]>();        

        for (int count = 0; count < listStreamers.Length; count++)
        {
            try
            {                
                Task<ObjectStreamerOn> objectStreamerOn = _userTwitch.GetStreamAsync(Token.access_token, listStreamers[count]);                                                                                                
                Task<ObjectStreamerInfo> objectStreamerInfo =  _userTwitch.GetInfoAsync(Token.access_token, listStreamers[count]); 

                await Task.WhenAll(objectStreamerOn, objectStreamerInfo);            

                if(objectStreamerOn.Result.data.Length != 0)
                {
                    await _discord.SendMsgStreamOn(objectStreamerInfo.Result,objectStreamerOn.Result);
                }                    
            }
            catch (Exception err)
            {
                _logger.LogError($"Error:{err.Message}");
                return;
            }
        }
    }
}