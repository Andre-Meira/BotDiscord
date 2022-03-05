using Microsoft.Extensions.Logging;
using TwitchService.Services.Auth;
using BotDiscord.Services.HostHandler;
using TwitchService.Data;

namespace BotDiscord.Services.Applications;

public class TokenAcess : TwitchHandler
{
    private readonly ILogger<TokenAcess> _logger;
    private readonly TokenTwitch _tokenTwitch;

    public TokenAcess(TokenTwitch tokenTwitch, ILogger<TokenAcess> logger) : base(logger)
    {
        _logger = logger;
        _tokenTwitch = tokenTwitch;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        
        do
        {
            try
            {               
                Token = await _tokenTwitch.GetTokenAsync();
                _logger.LogWarning($@"Token Criado!! Token: {Token.access_token}, Expira em {Token.expires_in}");
                await Task.Delay(Token.expires_in, cancellationToken);
                Token = new TokenObjectResponse();
            }
            catch (Exception err)
            {
                _logger.LogError($"Error: {err.Message}");
            }

        }
        while (!cancellationToken.IsCancellationRequested);
    }
}
