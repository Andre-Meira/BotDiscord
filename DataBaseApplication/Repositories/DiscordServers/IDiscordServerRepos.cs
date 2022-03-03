using DataBaseApplication.Models;

namespace DataBaseApplication.Repositories.DiscordServers;
public interface IDiscordServerRepos
{

    /// <summary>
    ///     Adiciona um Novo Canal para receber as notificações da Live
    /// </summary>
    /// <param name="Discordserver">Class com as informações do Canal</param>
    Task AddChannelAsync(Discordserver Serv);
    Task RemoveChannel(Discordserver Serv);
    Task UpdateChannel(Discordserver Serv);
    Task<Discordserver> GetChannel(long IdServer);   
}