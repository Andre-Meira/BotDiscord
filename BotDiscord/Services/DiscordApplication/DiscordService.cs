using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using TwitchService.Data.ObjectResponse;

namespace BotDiscord.Services.DiscordApplication;

public class DiscordService : IDiscordService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _config;

    public DiscordService(DiscordSocketClient client, IConfiguration config)
    {
        _config = config;
        _client = client;
    }

    public async Task SendMsgStreamOn(ObjectStreamerInfo objectStreamerInfo, ObjectStreamerOn objectStreamer, long idChannelDiscord)
    {
       var dataInicio = objectStreamer.data[0].started_at.ToLocalTime();

        var embed = new EmbedBuilder();
        embed.WithThumbnailUrl(objectStreamerInfo.data[0].profile_image_url);
        embed.WithTitle($"Stream On!!");
        embed.WithColor(Color.Blue);
        embed.WithImageUrl($"{objectStreamer.data[0].thumbnail_url.Replace("{width}x{height}", "1920x1080")}");
        embed.WithDescription($"Titulo: {objectStreamer.data[0].title}");
        embed.WithUrl($"https://www.twitch.tv/{objectStreamer.data[0].user_login}");

        IMessageChannel channel = await _client.GetChannelAsync((ulong)idChannelDiscord) as IMessageChannel;        
        await channel.SendMessageAsync(embed: embed.Build());
    }
}
