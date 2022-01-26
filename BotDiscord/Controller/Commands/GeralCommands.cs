using BotDiscord.Services;
using Discord;
using Discord.Commands;
using TwitchService.Data.ObjectResponse;
using TwitchService.Services.Auth;
using TwitchService.Services.GeneralServices;

namespace BotDiscord.Controller.Commands
{
    public class GeralCommands : ModuleBase<SocketCommandContext>
    {
        private readonly GenerateToken _generateToken;
        private readonly StreamerOn _streamerOn;

        private TokenObjectResponse TokenObject = TokenAcess.Token;

        public GeralCommands(GenerateToken generateToken, StreamerOn streamerOn)
        {
            _generateToken = generateToken;
            _streamerOn = streamerOn;
        }

        [Command("Streamer")]
        public async Task GetToken(string Stramer)
        {
            try
            {
                ObjectStreamerOn objectStreamer = await _streamerOn.GetStreamAsync(Stramer, TokenObject.access_token);
                var dataInicio = objectStreamer.data[0].started_at.ToLocalTime();

                var embed = new EmbedBuilder();
                embed.WithTitle($"{objectStreamer.data[0].user_name} Está ONNN! Jogando: {objectStreamer.data[0].game_name} desdas: {dataInicio}");
                embed.WithColor(Color.Blue);
                embed.WithImageUrl($"{objectStreamer.data[0].thumbnail_url.Replace("{width}x{height}", "1360x720")}");
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
