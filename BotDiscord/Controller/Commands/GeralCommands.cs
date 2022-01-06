using BotDiscord.Services;
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

        private TokenObjectResponse TokenObject = CheckTokenAcess.Token;

        public GeralCommands(GenerateToken generateToken, StreamerOn streamerOn)
        {
            _generateToken = generateToken;
            _streamerOn = streamerOn;
        }

        [Command("Streamer")]        
        public async Task GetToken(string Stramer)
        {                                 
            ObjectStreamerOn objectStreamer = await _streamerOn.GetStreamAsync(Stramer,TokenObject.access_token);
            await ReplyAsync(@$"{objectStreamer.data[0].user_name} Está ONNN!! Jogando: {objectStreamer.data[0].game_name}.. Com {objectStreamer.data[0].viewer_count} Viewers..");
        }
    }
}