using Discord;
using Discord.WebSocket;

using DiscordBotPluginManager;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leveling_System.Player
{
    public static class Data
    {
        private static readonly string folder = @".\Data\Resources\LvUpSystem\";
        public static void registerPlayer(SocketGuildUser user) {
            ulong id = user.Id;
            Directory.CreateDirectory(folder);
            File.WriteAllText(Path.Combine(folder, id.ToString() + ".data"), "Level=0,EXP=0,REXP=100");
        }

        public static int GetLevel(ulong id) => int.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[0].Split('=')[1]);


        public static Int64 GetExp(ulong id) => Int64.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[1].Split('=')[1]);


        public static Int64 GetReqEXP(ulong id) => Int64.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[2].Split('=')[1]);


    }
}
