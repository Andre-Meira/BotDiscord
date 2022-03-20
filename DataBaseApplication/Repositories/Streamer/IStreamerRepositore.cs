using DataBaseApplication.Models;

namespace DataBaseApplication.Repositories.Streamer;

public interface IStreamerRepositore
{
    IEnumerable<Streamerdisc> ListStreamer();    
    Task<Streamerdisc> GetStreamerAsync(int idStreamer);
    Task AddStreamerAsync(Streamerdisc streamer);        
    Task RemoveStreamerAsync(Streamerdisc streamer);
}