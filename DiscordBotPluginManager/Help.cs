using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotPluginManager
{
    public class Help : ModuleBase<SocketCommandContext>, DBPlugin
    {
        public string Command => "help";

        public string Description => "This command allows you to check all loadded commands";

        public string Usage => "help";

        public void Execute(SocketCommandContext context, SocketMessage message, DiscordSocketClient client)
        {
            foreach (DBPlugin p in PluginLoader.Plugins)
                context.Channel.SendMessageAsync(p.Usage + "\t" + p.Description);
        }
    }
}