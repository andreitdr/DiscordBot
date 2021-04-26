using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.WebSocket;

using DiscordBotPluginManager;

namespace Leveling_System_Event
{
    public class Class1 : DBEvent
    {
        public string name => "Leveling System";

        public string description => "no descr";

        public void Start(DiscordSocketClient client) {
            client.MessageReceived += Client_MessageReceived;
        }

        private async Task Client_MessageReceived(SocketMessage arg) {
            if (arg.Author.IsBot)
                return;
            (bool x, int lv) = Core.MessageSent(arg.Author.Id);
            if (x)
                await arg.Channel.SendMessageAsync($"Level UP ! => {lv}");

        }
    }
}
