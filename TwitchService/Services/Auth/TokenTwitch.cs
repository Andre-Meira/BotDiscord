using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TwitchService.Data;

namespace TwitchService.Services.Auth;
public class TokenTwitch
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    public TokenTwitch(IConfiguration configuration, IHttpClientFactory httpClient)
    {
        _config = configuration;
        _httpClient = httpClient.CreateClient("UriTokenTwitch");
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
        body.Add("scope", "user:read:email");
        return body;
    }
}