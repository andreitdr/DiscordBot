using Discord.Commands;
using Discord.WebSocket;

using DiscordBotPluginManager;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBot.Discord.Core
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;
        private readonly char botPrefix;

        public CommandHandler(DiscordSocketClient client, CommandService commandService, char botPrefix)
        {
            this.client = client;
            this.commandService = commandService;
            this.botPrefix = botPrefix;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += MessageHandler;
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task MessageHandler(SocketMessage Message)
        {
            if (Message as SocketUserMessage == null)
                return;

            var message = Message as SocketUserMessage;

            int argPos = 0;

            if (message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                await message.Channel.SendMessageAsync("Can not exec mentioned commands !");
                return;
            }

            if (!(message.HasCharPrefix(botPrefix, ref argPos) || message.Author.IsBot))
                return;

            var context = new SocketCommandContext(client, message);

            await commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null
            );

            DBPlugin plugin = PluginLoader.Plugins.Where(p => p.Command == (message.Content.Split(' ')[0]).Substring(1)).FirstOrDefault();

            if (plugin != null)
            {
                plugin.Execute(context, message, client);
                Functions.WriteLogFile("Executed command : " + plugin.Command);
            }
        }
    }
}