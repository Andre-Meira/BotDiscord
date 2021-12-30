using Discord.Commands;
using TwitchService.Services.Auth;
using TwitchService.Services.GeneralServices;

namespace BotDiscord.Controller.Commands
{
    public class GeralCommands : ModuleBase<SocketCommandContext> 
    {
        private readonly GenerateToken _generateToken; 
        private readonly StreamerOn _streamerOn; 

        public GeralCommands(GenerateToken generateToken, StreamerOn streamerOn)
        {
            _generateToken = generateToken;
            _streamerOn = streamerOn;
        }

        [Command("Token")]
        public async Task GetToken()
        {
            TokenObjectResponse tokenObject = await _generateToken.GetTokenAsync();                
            
            await ReplyAsync(tokenObject.access_token);
        }
    }
}