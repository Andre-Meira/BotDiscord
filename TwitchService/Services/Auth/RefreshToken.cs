
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TwitchService.Services.Auth;

public class RefreshToken
{
    private readonly HttpClient _Http;
    private readonly IConfiguration _config;
    public RefreshToken(IHttpClientFactory http, IConfiguration config)
    {
        _Http = http.CreateClient("ApiTwitch");
        _config = config;
    }   

    public async Task<TokenObjectResponse> RefreshAsync(TokenObjectResponse tokenObject)
    {
        Dictionary<string,string> BodyRequest = new Dictionary<string, string>();
        BodyRequest.Add("grant_type","refresh_token");
        BodyRequest.Add("client_secret", _config["SecreatIDTwitch"]);
        BodyRequest.Add("refresh_token", tokenObject.refresh_token);

        HttpRequestMessage RequestRefreshToken = new HttpRequestMessage(HttpMethod.Post, _Http.BaseAddress);
        RequestRefreshToken.Content = new FormUrlEncodedContent(BodyRequest); 

        HttpResponseMessage ResponseRefreshToken =  await _Http.SendAsync(RequestRefreshToken);          
        string ResponseBody = ResponseRefreshToken.Content.ReadAsStringAsync().Result;

        if (ResponseRefreshToken.StatusCode != HttpStatusCode.OK)
            throw new Exception($"Error: {ResponseRefreshToken.StatusCode}, Mensagem: {ResponseBody}");

        return JsonConvert.DeserializeObject<TokenObjectResponse>(ResponseBody);
    }
}