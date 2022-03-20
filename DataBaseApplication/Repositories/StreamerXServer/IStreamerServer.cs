using DataBaseApplication.Models;
using DataBaseApplication.Repositories.DiscordServers;
using DataBaseApplication.Repositories.Streamer;

namespace DataBaseApplication.Repositories.StreamerXServer;

public interface IStreamerServer
{
    Task AddAsync(Streamerdisc streamer, Discordserver server);
    Task RemoveAsync(Streamerdisc streamer, Discordserver server);                   
    protected RelStreamerXDiscordserf GetRelStreamerXDiscord(Streamerdisc streamer, Discordserver server);
    IEnumerable<RelStreamerXDiscordserf> ListStreamerXDiscord();
    IEnumerable<Streamerdisc> ListStreamerServ(long idDerver);
}   