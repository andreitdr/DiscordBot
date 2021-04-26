using Discord.Commands;
using Discord.WebSocket;

using DiscordBotPluginManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leveling_System
{
    public class level : DBPlugin
    {
        public string Command => "rank";

        public string Description => "Display your current level";

        public string Usage => "rank";

        public async void Execute(SocketCommandContext context, SocketMessage message, DiscordSocketClient client) {
            try
            {
                int cLv = Player.Data.GetLevel(message.Author.Id);
                Int64 cEXP = Player.Data.GetExp(message.Author.Id);
                Int64 rEXP = Player.Data.GetReqEXP(message.Author.Id);

                await message.Channel.SendMessageAsync($"Your level is {cLv}\nExp: {cEXP}\nRequired EXP to Level {cLv + 1}: {rEXP}");
            }
            catch
            {
                await message.Channel.SendMessageAsync("Please send some messages, you are not yet registered !");
                return;
            }


        }
    }
}
