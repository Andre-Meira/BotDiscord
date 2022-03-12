using DataBaseApplication.Models;

namespace DataBaseApplication.Repositories.StreamerXServer;

public interface IStreamerServer
{
    Task AddAsync(Streamerdisc streamer, Discordserver server);
    Task RemoveAsync(Streamerdisc streamer, Discordserver server);                   
    protected RelStreamerXDiscordserf GetRelStreamerXDiscord(Streamerdisc streamer, Discordserver server);
}