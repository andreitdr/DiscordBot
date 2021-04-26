using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Leveling_System_Event
{
    public class Core
    {
        private static readonly string folder = @".\Data\Resources\LvUpSystem\";

        public static int GetLevel(ulong id) => int.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[0].Split('=')[1]);


        public static Int64 GetExp(ulong id) => Int64.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[1].Split('=')[1]);


        public static Int64 GetReqEXP(ulong id) => Int64.Parse(File.ReadAllText(Path.Combine(folder, id.ToString() + ".data")).Split(',')[2].Split('=')[1]);
        public static void SaveData(ulong id, int lv, Int64 cexp, Int64 rexp) {
            Directory.CreateDirectory(folder);
            File.WriteAllText(Path.Combine(folder, id.ToString() + ".data"), $"Level={lv},EXP={cexp},REXP={rexp}");
        }

        /*
         * 
         * formula
         * 
         * level ^ 2 + 2 * level + 20
         * 
         * 
         */
        private static Int64 NextLevelXP(int level) {
            return (level * level) + 2 * level + 20;
        }

        //if level up -=> true

        public static (bool, int) MessageSent(ulong id) {
            if (!File.Exists(Path.Combine(folder, id.ToString() + ".data")))
            {
                SaveData(id, 0, 0, 0);
            }
            Int64 cEXp = GetExp(id);
            Int64 rExp = GetReqEXP(id);
            int random = new Random().Next(3, 6);
            cEXp += random;
            if (cEXp >= rExp)
            {
                cEXp = cEXp - rExp;
                int lv = GetLevel(id);
                rExp = NextLevelXP(lv);
                lv++;
                SaveData(id, lv, cEXp, rExp);
                return (true, lv);
            }

            SaveData(id, GetLevel(id), cEXp, rExp);
            return (false, -1);


        }

    }
}
