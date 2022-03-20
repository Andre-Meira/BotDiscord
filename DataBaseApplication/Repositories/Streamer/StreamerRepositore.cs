using DataBaseApplication.Models;
using DataBaseApplication.Repositories.StreamerXServer;

namespace DataBaseApplication.Repositories.Streamer;

public class StreamerRepositore : IStreamerRepositore
{
    private readonly DiscordBotAplicationContext _context; 
    
    public StreamerRepositore(DiscordBotAplicationContext context)
    {
        _context = context;        
    }    
    public IEnumerable<Streamerdisc> ListStreamer()
    {
        return _context.Streamerdiscs.AsEnumerable();
    }

    public async Task<Streamerdisc> GetStreamerAsync(int idStreamer)
    {
        return await _context.Streamerdiscs.FindAsync(idStreamer);        
    }

    public async Task AddStreamerAsync(Streamerdisc streamer)
    {
        try
        {
            Streamerdisc streamExist = await GetStreamerAsync(streamer.IdStreamer);

            if(streamExist != null)
                throw new Exception($"Esse Streamer ja esta cadastrado!!");
            
            await _context.AddAsync(streamer);
            await _context.SaveChangesAsync();

        }catch (Exception )
        {
            throw;
        }
    }

    public async Task RemoveStreamerAsync(Streamerdisc streamer)
    {
        try
        {
            Streamerdisc streamExist = await GetStreamerAsync(streamer.IdStreamer);

            if(streamer == null)
                throw new Exception($"Esse Streamer nao esta cadastrado!!");
            
            _context.Remove(streamer);
            await _context.SaveChangesAsync();
            
        }catch (Exception )
        {
            throw;
        }
    }
}