using DataBaseApplication.Models;
using DataBaseApplication.Repositories.DiscordServers;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Controller.Commands;

public class ServCommands : ModuleBase<SocketCommandContext>
{
    private readonly IDiscordServerRepos _discServer;
    private readonly DiscordSocketClient _client;

    public ServCommands(IDiscordServerRepos discServer, DiscordSocketClient client)
    {
        _client = client;
        _discServer = discServer;
    }

    [Command("RegisterChannel")]
    public async Task RegisterChannel()
    {
        try
        {
            Discordserver discordserver = new Discordserver((long)Context.Guild.Id,
                (long)Context.Channel.Id, Context.Guild.Name, Context.Channel.Name);

            await _discServer.AddChannelAsync(discordserver);
            await ReplyAsync("Canal de notificação cadastrado!!");

        }
        catch (Exception err)
        {
            await ReplyAsync(err.Message);
        }
    }

    [Command("UpdateChannelNotification")]
    public async Task UpdateChannel()
    {
        try
        {
            Discordserver discordserver = await _discServer.GetChannel((long)Context.Guild.Id);

            if (discordserver == null)
                await ReplyAsync("Esse Servidor não tem um canal de notificação!!");
            else
            {
                discordserver.IdChanel = (long)Context.Channel.Id;
                discordserver.NameChannel = Context.Channel.Name;

                await _discServer.UpdateChannel(discordserver);
                await ReplyAsync($"Canal de notificação atualizado para {discordserver.NameChannel}");
            }
        }
        catch (Exception err)
        {
            await ReplyAsync($"Error: {err.Message}");
        }
    }

    [Command("ChannelNotification")]
    public async Task ChannelNotification()
    {
        Discordserver serverInfo = await _discServer.GetChannel((long)Context.Guild.Id);

        if (serverInfo is null)
            await ReplyAsync("Esser Servidor ainda não possui um canal de notificação!! use o seguinte commando: !RegisterChannel");
        else
        {
            SocketGuildChannel channel = Context.Guild.GetChannel((ulong)serverInfo.IdChanel);
            await ReplyAsync($"Canal de notificação: {channel.Name}");
        }
    }
}