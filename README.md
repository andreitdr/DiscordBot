# DiscordBot

Model for plugin:
```cs
namespace Master_Plugin.Commands.User
{
    internal class Random : DiscordBotPluginManager.DBPlugin
    {
        // The command name
        // The command name is that string that appears in discord chat along with the prefix
        // For example: this command will be called with {prefix}random
        public string Command => "random";
        
        // The command description
        public string Description => "Generates a random value between two values";
        
        // The command usage: Pure informative -> tell the user how to use this command
        public string Usage => "random [value1] [value2]";
        
        // The main body: this is where the execution starts (this is the Main function)
        public async void Execute(Discord.Commands.SocketCommandContext context, Discord.WebSocket.SocketMessage message, Discord.WebSocket.DiscordSocketClient client)
        {
            try
            {
                // Get the 2 values from message
                var value1 = int.Parse(message.Content.Split(' ')[1]);
                var value2 = int.Parse(message.Content.Split(' ')[2]);
                // Generate the random value
                System.Random r = new System.Random();
          
                await context.Channel.SendMessageAsync("Your random generated value is : " + r.Next());
            }
            catch (System.Exception ex)
            {
                // Exception handler
                await context.Channel.SendMessageAsync("Invalid arguments\nUsage: " + Usage);

                DiscordBotPluginManager.Functions.WriteErrFile(ex.Message);
            }
        }
    }
}
```

Model for Addon
```cs
//soon
```
