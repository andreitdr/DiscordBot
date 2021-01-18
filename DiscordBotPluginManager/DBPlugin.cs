namespace DiscordBotPluginManager
{
    public interface DBPlugin
    {
        string Command { get; }

        string Description { get; }

        string Usage { get; }

        void Execute(Discord.Commands.SocketCommandContext context, Discord.WebSocket.SocketMessage message,
                                            Discord.WebSocket.DiscordSocketClient client);
    }
}