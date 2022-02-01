using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TwitchService.Data.ObjectResponse;

namespace TwitchService.Services.GeneralServices.User;
public class UserRequest : IUserRequest
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    public UserRequest(IHttpClientFactory http, IConfiguration config)
    {
        _http = http.CreateClient("UriTwitchApi");
        _config = config;
    }

    public async Task<ObjectStreamerOn> GetStreamAsync(string user, string token)
    {
        try
        {
            HttpRequestMessage requestStream = new HttpRequestMessage(HttpMethod.Get, $"{_http.BaseAddress}/streams?user_login={user}");
            requestStream.Headers.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage ResponseStream = await _http.SendAsync(requestStream);
            string BodyResponse = await ResponseStream.Content.ReadAsStringAsync();
            ObjectStreamerOn objectStreamer = JsonConvert.DeserializeObject<ObjectStreamerOn>(BodyResponse);

            if(objectStreamer.data.Length == 0)                       
                throw new ExecpetionObject("Streamer não está online",BodyResponse, (int)ResponseStream.StatusCode);

            return objectStreamer;
        }
        catch (Exception err)
        {
            throw new ExecpetionObject(err.Message);
        }
    }

    public async Task<ObjectStreamerInfo> GetInfoAsync(string user,string token)
    {
        try
        {
            HttpRequestMessage requestStream = new HttpRequestMessage(HttpMethod.Get, $"{_http.BaseAddress}/users?login={user}");
            requestStream.Headers.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage ResponseStream = await _http.SendAsync(requestStream);
            string BodyResponse = await ResponseStream.Content.ReadAsStringAsync();
            
            if(ResponseStream.IsSuccessStatusCode)  
                return JsonConvert.DeserializeObject<ObjectStreamerInfo>(BodyResponse);

            throw new ExecpetionObject($"Erro:{ResponseStream.ReasonPhrase}",BodyResponse,((int)ResponseStream.StatusCode));
        }
        catch (Exception err)
        {            
            throw new ExecpetionObject($"Erro:{err.Message}",err.Data);
        }
    }
}