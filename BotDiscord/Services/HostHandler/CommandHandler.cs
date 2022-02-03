using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BotDiscord.Services.HostHandler;

public class CommandHandler : DiscordClientService
{
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _service;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public CommandHandler(DiscordSocketClient client, ILogger<CommandHandler> logger,
            IServiceProvider provider, CommandService commandService, IConfiguration config) : base(client, logger)
        {
            _service = commandService;
            _serviceProvider = provider;
            _configuration = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Client.MessageReceived += OnMenssageReceived;
            await _service.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), _serviceProvider);            
        }

        private async Task OnMenssageReceived(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return;
            if (message.Source != Discord.MessageSource.User) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_configuration["prefix"], ref argPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argPos))
                return;

            var context = new SocketCommandContext(Client, message);
            await _service.ExecuteAsync(context, argPos, _serviceProvider);
        }
}