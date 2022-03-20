using BotDiscord.Services.HostHandler;
using DataBaseApplication.Models;
using DataBaseApplication.Repositories.DiscordServers;
using DataBaseApplication.Repositories.StreamerXServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.GeneralServices.User;

namespace BotDiscord.Services.Applications;

public class CheckedStreamerON : TwitchHandler
{
    private readonly ILogger<CheckedStreamerON> _logger;
    private readonly IUserRequest _userTwitch;
    private readonly IDiscordService _discordServices; 
    private readonly IStreamerServer _streamerServer;    
    private readonly IDiscordServerRepos _dbDiscord;

    public CheckedStreamerON(IDiscordService discordServices ,IUserRequest userTwitch, IStreamerServer streamerServer,
        IDiscordServerRepos dbDiscord,ILogger<CheckedStreamerON> logger) : base(logger)
    {
        _logger = logger;
        _userTwitch = userTwitch;        
        _discordServices = discordServices;
        _streamerServer = streamerServer;
        _dbDiscord = dbDiscord;
    }
    
    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {         
            try
            {
                if (Token != null)
                {
                    await Task.Run(async () => { await StreamerOn();}
                        ,cancellationToken);
                    
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
        IEnumerable<Discordserver> listServers = _dbDiscord.ListServers();

        foreach (Discordserver server in listServers)
        {
            IEnumerable<Streamerdisc> listStreamer = _streamerServer.ListStreamerServ(server.IdServer);       

            foreach(Streamerdisc streamer in listStreamer)
            {
                Task<ObjectStreamerOn> objectStreamerOn = _userTwitch.GetStreamAsync(Token.access_token, streamer.Nickname);                                                                                                
                Task<ObjectStreamerInfo> objectStreamerInfo =  _userTwitch.GetInfoAsync(Token.access_token, streamer.Nickname); 

                await Task.WhenAll(objectStreamerOn, objectStreamerInfo);

                if (objectStreamerOn.Result.data.Length == 0)
                    continue;

                await _discordServices.SendMsgStreamOn(objectStreamerInfo.Result,objectStreamerOn.Result, server.IdChanel);
            }
        }
    }
}