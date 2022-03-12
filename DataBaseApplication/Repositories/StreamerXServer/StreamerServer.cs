using DataBaseApplication.Models;
using DataBaseApplication.Repositories.Streamer;

namespace DataBaseApplication.Repositories.StreamerXServer;

public class StreamerServer : IStreamerServer
{
    private readonly DiscordBotAplicationContext _context; 
    private readonly IStreamerRepositore _streamerRepositore;

    public StreamerServer(DiscordBotAplicationContext context, IStreamerRepositore streamerRepositore)
    {
        _context = context;        
        _streamerRepositore = streamerRepositore;
    }

    public RelStreamerXDiscordserf GetRelStreamerXDiscord(Streamerdisc streamer, Discordserver server)
    {
        return _context.RelStreamerXDiscordserves.Where(
            t => t.FkStreamer == streamer.IdStreamer && t.FkIdServe == server.IdServer
        ).FirstOrDefault();
    }

    public async Task AddAsync(Streamerdisc streamer, Discordserver server)
    {       
        RelStreamerXDiscordserf relRegistration = GetRelStreamerXDiscord(streamer,server);
        Streamerdisc streamerInfo  = await _streamerRepositore.GetStreamerAsync(streamer.IdStreamer);

        if(relRegistration == null)
        {
            if(streamerInfo == null)
                await _streamerRepositore.AddStreamerAsync(streamer);

            RelStreamerXDiscordserf StreamerXDiscord = new RelStreamerXDiscordserf(streamer.IdStreamer,server.IdServer);        
            _context.Add(StreamerXDiscord);            
            await _context.SaveChangesAsync();                        

        }else
        {
            throw new Exception($"O Streamer ja esta cadastrado nesse Servidor!!");
        }        
    }
    

    public async Task RemoveAsync(Streamerdisc streamer, Discordserver server)
    {
        try
        {
            RelStreamerXDiscordserf relRegister = new RelStreamerXDiscordserf();

            _context.RelStreamerXDiscordserves.Remove(relRegister);
            await _context.SaveChangesAsync();

        }catch(Exception err)
        {
            System.Console.WriteLine($"error: {err.Message}");
        }        
    }

}