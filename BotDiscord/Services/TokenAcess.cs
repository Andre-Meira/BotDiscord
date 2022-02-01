using Microsoft.Extensions.Logging;
using TwitchService.Services.Auth;
using BotDiscord.Services.HostHandler;

namespace BotDiscord.Services;

public class TokenAcess : TwitchHandler
{
    private readonly RefreshToken _RefreshToken;
    private readonly GenerateToken _generateToken;
    private readonly ILogger<TokenAcess> _logger;

    public TokenAcess(ILogger<TokenAcess> logger
    , RefreshToken refreshToken, GenerateToken generateToken) : base(logger)
    {
        _logger = logger;
        _RefreshToken = refreshToken;
        _generateToken = generateToken;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        do
        {
            try
            {
                Token = await _generateToken.GetTokenAsync();
                _logger.LogWarning($"Token Criado!! Token: {Token.access_token}");
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
