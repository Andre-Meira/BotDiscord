using System;
using BotDiscord.Services;
using Discord;
using Discord.Commands;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.Auth;
using TwitchService.Services.GeneralServices;
using BotDiscord.Services.HostHandler;
using TwitchService.Services.GeneralServices.User;
using TwitchService.Data;

namespace BotDiscord.Controller.Commands
{
    public class GeralCommands : ModuleBase<SocketCommandContext>
    {
        private readonly TokenTwitch _generateToken;
        private readonly IUserRequest _twitchUser;

        private TokenObjectResponse TokenObject = TokenAcess.Token;

        public GeralCommands(TokenTwitch generateToken, IUserRequest twitchUser)
        {
            _generateToken = generateToken;
            _twitchUser = twitchUser;
        }

        [Command("Streamer")]
        public async Task GetToken(string Stramer)
        {
            try
            {
                Task<ObjectStreamerOn> taskObjectStreamer =  _twitchUser.GetStreamAsync(TokenObject.access_token,Stramer);
                Task<ObjectStreamerInfo> taskObjectInfoUser = _twitchUser.GetInfoAsync(TokenObject.access_token, Stramer); 

                await Task.WhenAll(taskObjectStreamer,taskObjectInfoUser);
                            
                ObjectStreamerOn objectStreamer = taskObjectStreamer.Result;
                ObjectStreamerInfo objectStreamerInfo = taskObjectInfoUser.Result;
                                                                
                if(objectStreamer.data.Length == 0)                       
                    throw new ExecpetionObject("Streamer não está online");

                var dataInicio = objectStreamer.data[0].started_at.ToLocalTime();
                                
                var embed = new EmbedBuilder();   
                embed.WithThumbnailUrl(objectStreamerInfo.data[0].profile_image_url);             
                embed.WithTitle($"Stream On!!");
                embed.WithColor(Color.Blue);                
                embed.WithImageUrl($"{objectStreamer.data[0].thumbnail_url.Replace("{width}x{height}", "1920x1080")}");
                embed.WithDescription($"Titulo: {objectStreamer.data[0].title}");
                embed.WithUrl($"https://www.twitch.tv/{objectStreamer.data[0].user_login}");

                await ReplyAsync(embed: embed.Build()); 
            }
            catch (Exception err)
            {                
                await ReplyAsync(err.Message);
            }                        
        }
    }
}
