using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DiscordBotPluginManager
{
    public class PluginLoader
    {
        private const string pluginCMDFolder = @".\Data\Plugins\Commands\";
        private const string pluginADDFolder = @".\Data\Plugins\Addons\";
        private const string pluginExtension = ".dll";

        public static List<DBPlugin> Plugins { get; set; }
        public static List<DBAddon> Addons { get; set; }

        public int LoadPlugins(RichTextBox logs)
        {
            Plugins = new List<DBPlugin>();
            Addons = new List<DBAddon>();

            int loadedPlugins = 0;
            int loadedAddons = 0;
            if (Directory.Exists(pluginCMDFolder))
            {
                string[] files = Directory.GetFiles(pluginCMDFolder).Where(p => p.EndsWith(pluginExtension)).ToArray();
                foreach (string file in files)
                {
                    Assembly.LoadFile(Path.GetFullPath(file));

                    logs.Invoke(new MethodInvoker(delegate () { logs.AppendText("[PLUGIN/CMD] " + file.Split('\\')[file.Split('\\').Length - 1] + " has been loaded !\n"); }));
                    loadedPlugins++;
                }
            }
            else
                return 0;

            if (Directory.Exists(pluginADDFolder))
            {
                string[] files = Directory.GetFiles(pluginADDFolder).Where(p => p.EndsWith(pluginExtension)).ToArray();
                foreach (string file in files)
                {
                    Assembly.LoadFile(Path.GetFullPath(file));

                    logs.Invoke(new MethodInvoker(delegate () { logs.AppendText("[PLUGIN/ADDON] " + file.Split('\\')[file.Split('\\').Length - 1] + " has been loaded !\n"); }));
                    loadedAddons++;
                }
            }
            else
                return 0;

            try
            {
                Type interfaceType = typeof(DBPlugin);
                Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                    .ToArray();
                foreach (Type type in types)
                {
                    Plugins.Add((DBPlugin)Activator.CreateInstance(type));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

            try
            {
                Type interfaceType2 = typeof(DBAddon);
                Type[] types2 = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(p => interfaceType2.IsAssignableFrom(p) && p.IsClass)
                    .ToArray();
                foreach (Type type1 in types2)
                {
                    Addons.Add((DBAddon)Activator.CreateInstance(type1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            return loadedPlugins;
        }
    }
}