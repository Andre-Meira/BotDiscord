using Discord;
using Discord.Commands;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.GeneralServices.User;
using TwitchService.Data;
using BotDiscord.Services.Applications;
using DataBaseApplication.Models;
using DataBaseApplication.Repositories.StreamerXServer;

namespace BotDiscord.Controller.Commands
{
    public class StreamerCommands : ModuleBase<SocketCommandContext>
    {        
        private readonly IUserRequest _twitchUser;
        private readonly IStreamerServer _serverStream;        
        private TokenObjectResponse TokenObject = TokenAcess.Token;

        public StreamerCommands(IUserRequest twitchUser,IStreamerServer streamerServer)
        {            
            _twitchUser = twitchUser;
            _serverStream = streamerServer;            
        }

        [Command("CadastrarStreamer")]
        public async Task RegisterStreamer(string streamer)
        {
            try
            {
                ObjectStreamerInfo streamerInfo = await _twitchUser.GetInfoAsync(TokenObject.access_token, streamer);
                
                Streamerdisc streamerDt = new Streamerdisc();
                streamerDt.IdStreamer = int.Parse(streamerInfo.data[0].id);
                streamerDt.Nickname = streamerInfo.data[0].login;

                Discordserver discordserver = new Discordserver(
                    (long)Context.Guild.Id,
                    (long)Context.Channel.Id, 
                    Context.Guild.Name, Context.Channel.Name
                );                

                await _serverStream.AddAsync(streamerDt, discordserver);
                await ReplyAsync("Streamer Cadastrado!!");

            } catch (Exception err)
            {
                await ReplyAsync(err.Message);
            }            
        }

        [Command("Streamer")]
        public async Task GetToken(string streamer)
        {
            try
            {       
                Task<ObjectStreamerOn> taskObjectStreamer =  _twitchUser.GetStreamAsync(TokenObject.access_token,streamer);
                Task<ObjectStreamerInfo> taskObjectInfoUser = _twitchUser.GetInfoAsync(TokenObject.access_token, streamer);

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
                                
            }catch (Exception err)
            {
                await ReplyAsync(err.Message);
            }
        }
    }
}
