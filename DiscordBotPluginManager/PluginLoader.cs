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

        private const string         pluginCMDFolder    = @".\Data\Plugins\Commands\";
        private const string         pluginADDFolder    = @".\Data\Plugins\Addons\";
        private const string         pluginCMDExtension = ".dll";
        private const string         pluginADDExtension = ".dll";
        public static List<DBPlugin> Plugins { get; set; }
        public static List<DBAddon>  Addons  { get; set; }

        public delegate void CMDLoaded(string name, bool success, Exception e = null);

        public delegate void ADDLoaded(string name, bool success, Exception e = null);

        public CMDLoaded onCMDLoad;
        public ADDLoaded onADDLoad;

        public void LoadPlugins()
        {
            Plugins = new List<DBPlugin>();
            Addons  = new List<DBAddon>();
            
            //Load commands
            CommandsLoader CMDLoader = new CommandsLoader(pluginCMDFolder, pluginCMDExtension);
            CMDLoader.OnCommandLoaded += OnCommandLoaded;
            Plugins                   =  CMDLoader.LoadCommands();

            //Load addons
            AddonsLoader ADDLoader = new AddonsLoader(pluginADDFolder, pluginADDExtension);
            ADDLoader.OnAddonLoaded += OnAddonLoaded;
            Addons                  =  ADDLoader.LoadAddons();
        }

        private void OnAddonLoaded(string typename, bool success, DBAddon addon, Exception exception)
        {
            if (addon != null && success)
                addon.Execute(Form.ActiveForm);
            if(onADDLoad != null)
                onADDLoad.Invoke(typename,success,exception);
            
        }

        private void OnCommandLoaded(string name, bool success,DBPlugin command, Exception exception)
        {
            if(onCMDLoad != null)
                onCMDLoad.Invoke(name,success,exception);
        }
    }
}