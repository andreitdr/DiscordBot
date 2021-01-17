using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotPluginManager
{
    public interface DBPlugin
    {
        string Command { get; }

        string Description { get; }

        string Usage { get; }

        void Execute(SocketCommandContext context, SocketMessage message, DiscordSocketClient client);
    }
}