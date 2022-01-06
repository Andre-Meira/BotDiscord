using BotDiscord.Services.Hosted;
using Microsoft.Extensions.Logging;
using TwitchService.Services.Auth;

namespace BotDiscord.Services
{
    public class CheckTokenAcess : RefreshTokenHandler
    {
        public static TokenObjectResponse Token { get; set; }

        private readonly RefreshToken _RefreshToken;
        private readonly GenerateToken _generateToken; 
        private readonly ILogger<CheckTokenAcess> _logger;

        public CheckTokenAcess(ILogger<CheckTokenAcess> logger
        ,RefreshToken refreshToken,GenerateToken generateToken) : base(logger)
        {
            _logger = logger;
            _RefreshToken = refreshToken;
            _generateToken = generateToken;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            do 
            {                
                Token = await _generateToken.GetTokenAsync();                                   
                await Task.Delay(Token.expires_in, cancellationToken);                   
                Token = new TokenObjectResponse();
            }
            while(!cancellationToken.IsCancellationRequested);
        }
    }
}