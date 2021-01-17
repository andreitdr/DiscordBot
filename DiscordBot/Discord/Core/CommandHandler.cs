using Discord.Commands;
using Discord.WebSocket;

using DiscordBotPluginManager;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Core
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;
        private readonly char botPrefix;

        public CommandHandler(DiscordSocketClient client, CommandService service, char botPrefix)
        {
            this.client = client;
            this.commandService = service;
            this.botPrefix = botPrefix;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += MessageHandler;
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task MessageHandler(SocketMessage message)
        {
            if (message as SocketUserMessage == null)
                return;

            int argPos = 0;
            if (!((message as SocketUserMessage).HasCharPrefix(botPrefix, ref argPos) ||
                  (message as SocketUserMessage).HasMentionPrefix(client.CurrentUser, ref argPos) ||
                  (message as SocketUserMessage).Author.IsBot))
                return;

            var context = new SocketCommandContext(client, message as SocketUserMessage);

            await commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null
            );

            DBPlugin plugin = PluginLoader.Plugins.Where(p => p.Command == (message.Content.Split(' ')[0]).Substring(1)).FirstOrDefault();
            if (plugin != null)
                plugin.Execute(context, message, client);
        }
    }
}