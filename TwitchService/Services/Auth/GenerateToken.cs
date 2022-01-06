using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TwitchService.Services.Auth;


public class TokenObjectResponse
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public int expires_in { get; set; }
    public string[] scope { get; set; }
    public string token_type { get; set; }
}

public class GenerateToken
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    public GenerateToken(IConfiguration configuration, IHttpClientFactory httpClient)
    {
        _config = configuration;
        _httpClient = httpClient.CreateClient("ApiTwitch");
    }

    public async Task<TokenObjectResponse> GetTokenAsync()
    {
        HttpRequestMessage requestToken = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress);
        requestToken.Content = new FormUrlEncodedContent(BodyRequestToken());

        HttpResponseMessage reponseToken = await _httpClient.SendAsync(requestToken);
        string responseBody = reponseToken.Content.ReadAsStringAsync().Result;

        if (reponseToken.StatusCode == HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<TokenObjectResponse>(responseBody);

        throw new Exception($"Error: {reponseToken.StatusCode}, Mensagem: {responseBody}");
    }

    private Dictionary<string, string> BodyRequestToken()
    {
        Dictionary<string, string> body = new Dictionary<string, string>();        
        body.Add("grant_type", "client_credentials");
        body.Add("client_secret", _config["SecreatIDTwitch"]);
        return body;
    }
}
