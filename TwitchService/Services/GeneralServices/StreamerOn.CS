using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TwitchService.Data.ObjectResponse;

namespace TwitchService.Services.GeneralServices;

public class StreamerOn
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public StreamerOn(HttpClient httpclient, IConfiguration config)
    {
        _httpClient = httpclient;
        _config = config;
    }

    public async Task<ObjectStreamerOn> GetStreamAsync(string user, string Token)
    {
        var uri = _httpClient.BaseAddress;
        HttpRequestMessage requestStream = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + user);
        requestStream.Headers.Add("Authorization", $"Bearer {Token}");

        HttpResponseMessage ResponseStream = await _httpClient.SendAsync(requestStream);
        string BodyResponse = await ResponseStream.Content.ReadAsStringAsync();

        if (ResponseStream.StatusCode == HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<ObjectStreamerOn>(BodyResponse);            

        throw new Exception($"Error: Status Code:{ResponseStream.StatusCode}, Mensagem: {BodyResponse}");
    }

}