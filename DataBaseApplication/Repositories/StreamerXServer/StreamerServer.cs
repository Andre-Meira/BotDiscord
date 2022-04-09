using DataBaseApplication.Models;
using DataBaseApplication.Repositories.DiscordServers;
using DataBaseApplication.Repositories.Streamer;
using Microsoft.EntityFrameworkCore;

namespace DataBaseApplication.Repositories.StreamerXServer;

public class StreamerServer : IStreamerServer
{
    private readonly DiscordBotAplicationContext _context; 
    private readonly IStreamerRepositore _streamerRepositore;
    private readonly IDiscordServerRepos _discordServer;

    public StreamerServer(DiscordBotAplicationContext context, IStreamerRepositore streamerRepositore,
        IDiscordServerRepos discordServer)
    {
        _context = context;        
        _streamerRepositore = streamerRepositore;
        _discordServer = discordServer;
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
        Streamerdisc streamerInfo = await _streamerRepositore.GetStreamerAsync(streamer.IdStreamer);
        Discordserver discordInfo =  await _discordServer.GetChannel(server.IdServer);

        if(relRegistration == null)
        {
            if(streamerInfo == null)
                await _streamerRepositore.AddStreamerAsync(streamer);

            if(discordInfo == null)
                await _discordServer.AddChannelAsync(server);

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
            RelStreamerXDiscordserf infoRel = GetRelStreamerXDiscord(streamer,server);

            if(infoRel != null)
            {
                _context.Entry(infoRel).State = EntityState.Deleted;
                _context.RelStreamerXDiscordserves.Remove(infoRel);                        
                await _context.SaveChangesAsync();
            }

        }catch(Exception err)
        {
            System.Console.WriteLine($"error: {err.Message}");
        }        
    }

    public IEnumerable<RelStreamerXDiscordserf> ListStreamerXDiscord()
    {
        return _context.RelStreamerXDiscordserves.AsEnumerable();
    }

    public IEnumerable<Streamerdisc> ListStreamerServ(long idSserver)
    {
        var query = from relStreamDiscord in _context.RelStreamerXDiscordserves
            join server in _context.Discordservers on relStreamDiscord.FkIdServe equals server.IdServer 
            join streamers in _context.Streamerdiscs on relStreamDiscord.FkStreamer equals streamers.IdStreamer
            where relStreamDiscord.FkIdServe == idSserver
            select new Streamerdisc(streamers.IdStreamer,streamers.Nickname);

        return query.AsEnumerable();
    }
}