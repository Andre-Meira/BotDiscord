using System.Security.Claims;
using DataBaseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseApplication.Repositories.DiscordServers;

public class DiscordServerRepos : IDiscordServerRepos
{
    private readonly DiscordBotAplicationContext _context; 
    public DiscordServerRepos(DiscordBotAplicationContext context)
    {
        _context = context;    
    }

    public async Task AddChannelAsync(Discordserver Serv)
    {
        
        bool checkedChanelExist = await GetChannel(Serv.IdServer) != null;

        if(checkedChanelExist)
            throw new Exception("error: Esse server já possui um canal de notificação..");

        _context.Add(Serv);
        await _context.SaveChangesAsync();
    }

    public async Task<Discordserver> GetChannel(long IdServer)
    {
        return await _context.Discordservers.FindAsync(IdServer);
    }

    public async Task RemoveChannel(Discordserver Serv)
    {
        _context.Discordservers.Remove(Serv);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateChannel(Discordserver Serv)
    {
        _context.Discordservers.Update(Serv);
        await _context.SaveChangesAsync();
    }
}