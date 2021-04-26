using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotPluginManager
{
    public interface DBEvent
    {
        string name { get; }
        string description { get; }

        void Start(DiscordSocketClient client);
    }
}
