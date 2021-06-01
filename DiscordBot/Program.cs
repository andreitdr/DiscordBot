using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;

using DiscordBot.Discord.Core;

using DiscordBotPluginManager;

namespace DiscordBot
{
    public class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        [Obsolete]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AppendPrivatePath("Data\\lib");
            if (args.Length == 0)
                Application.Run(new Form1());
            else
                HandleInput(args).Wait();
        }

        private static void ResetSettings()
        {
            string[] files = Directory.GetFiles(@".\Data\Resources");
            foreach (string file in files) File.Delete(file);
        }

        private static async Task NoGUI(Boot discordbooter)
        {
            while (true)
            {
                string[] data = Console.ReadLine().Split(' ');
                if (data[0].Length < 2) continue;
                switch (data[0])
                {
                    case "/shutdown":
                    case "/sd":
                        if (discordbooter.client.ConnectionState == ConnectionState.Connected)
                            await discordbooter.ShutDown().ContinueWith(t => { Environment.Exit(0); });

                        break;
                    case "/loadplugins":
                    case "/lp":
                        var loader = new PluginLoader(discordbooter.client);
                        loader.onADDLoad += (name, success, exception) =>
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (success) Console.WriteLine("[ADDON] Successfully loaded addon : " + name);
                            else
                                Console.WriteLine("[ADDON] Failed to load ADDON : " + name + " because " +
                                                  exception.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        };
                        loader.onCMDLoad += (name, success, exception) =>
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (success) Console.WriteLine("[CMD] Successfully loaded addon : " + name);
                            else
                                Console.WriteLine("[CMD] Failed to load ADDON : " + name + " because " +
                                                  exception.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        };
                        loader.onEVELoad += (name, success, exception) =>
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (success) Console.WriteLine("[EVENT] Successfully loaded addon : " + name);
                            else
                                Console.WriteLine("[EVENT] Failed to load ADDON : " + name + " because " +
                                                  exception.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        };
                        loader.LoadPlugins();
                        break;
                    case "/help":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(
                            "/lp | /loadplugins -> load all plugins\n/sd | /shutdown -> close connectong to the server (stop bot)");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        goto case "/help";
                }
            }
        }

        private static async Task<Boot> StartNoGUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Discord BOT\n\nCreated by: Wizzy\nDiscord: Wizzy#9181\nCommands:");
            Console.WriteLine(
                "/lp | /loadplugins -> load all plugins\n/sd | /shutdown -> close connectong to the server (stop bot)");
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            string token =
                Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"), "BOT_TOKEN",
                                           '\t');
            string prefix = Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"),
                                                       "BOT_PREFIX",
                                                       '\t');
            var discordbooter = new Boot(token, prefix);

            await discordbooter.AwakeNoGUI();

            return discordbooter;
        }

        private static async Task ClearFolder(string d)
        {
            string[] files = Directory.GetFiles(d);
            int fileNumb = files.Length;
            for (var i = 0; i < fileNumb; i++)
            {
                File.Delete(files[i]);
                Console.WriteLine("Deleting : " + files[i]);
            }
        }

        private static void ReplaceText(string file, string code, string value)
        {
            try
            {
                var f = false;
                string[] text = File.ReadAllLines(file);
                foreach (string line in text)
                    if (line.StartsWith(code))
                    {
                        line.Replace(line.Split('\t')[1], value);
                        f = true;
                    }

                if (f)
                    File.WriteAllLines(@".\Data\Resources\DiscordBotCore.data", text);
                else throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                File.AppendAllText(file, code + "\t" + value + "\n");
            }
        }


        private static async Task HandleInput(string[] args)
        {
            int len = args.Length;
            if (len == 0 || args[0] != "--exec" && args[0] != "--execute")
            {
                Application.Run(new Form1());
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Execute command interface noGUI\n\n");
            Console.WriteLine(
                "\tCommand name\t\t\t\tDescription\n" +
                "-- help | -help\t\t ------ \tDisplay the help message\n" +
                "--reset-full\t\t ------ \tReset all files (clear files)\n" +
                "--reset-settings\t ------ \tReset only bot settings\n" +
                "--reset-logs\t\t ------ \tClear up the output folder\n" +
                "--set-token [token]\t ------ \tSet the bot token\n" +
                "--set-prefix [prefix]\t ------ \tSet the bot prefix\n" +
                "exit\t\t\t ------ \tClose the application"
            );
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                string[] message = Console.ReadLine().Split(' ');

                switch (message[0])
                {
                    case "--reset-settings":
                        ResetSettings();
                        Console.WriteLine("Successfully reseted all settings !");
                        break;
                    case "--help":
                    case "-help":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(
                            "\tCommand name\t\t\t\tDescription\n" +
                            "-- help | -help\t\t ------ \tDisplay the help message\n" +
                            "--reset-full\t\t ------ \tReset all files (clear files)\n" +
                            "--reset-settings\t ------ \tReset only bot settings\n" +
                            "--reset-logs\t\t ------ \tClear up the output folder\n" +
                            "--set-token [token]\t ------ \tSet the bot token\n" +
                            "--set-prefix [prefix]\t ------ \tSet the bot prefix\n" +
                            "exit\t\t\t ------ \tClose the application"
                        );
                        break;
                    case "--nogui":
                        Boot b = await StartNoGUI();
                        await NoGUI(b);
                        break;
                    case "--reset-full":
                        await ClearFolder(".\\Data\\Resources\\");
                        await ClearFolder(".\\Output\\Logs\\");
                        await ClearFolder(".\\Output\\Errors\\");
                        await ClearFolder(".\\Data\\Languages\\");
                        await ClearFolder(".\\Data\\Plugins\\Addons\\");
                        await ClearFolder(".\\Data\\Plugins\\Commands\\");
                        await ClearFolder(".\\Data\\Plugins\\Events\\");
                        Console.WriteLine("Successfully cleared all folders");
                        break;
                    case "--reset-logs":
                        await ClearFolder(".\\Output\\Logs\\");
                        await ClearFolder(".\\Output\\Errors\\");
                        Console.WriteLine("Successfully cleard logs folder");
                        break;
                    case "--exit":
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Failed to execute command " + message[0]);
                        break;
                }
            }
        }
    }
}
