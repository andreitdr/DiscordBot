using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DiscordBotPluginManager
{
	public class AddonsLoader
	{
		private readonly string ADDPath;
		private readonly string ADDExtension;


		public delegate void onAddonLoaded(string     typeName, bool success, DBAddon addonName = null, Exception exception = null);
		public delegate void onAddonFileLoaded(string path);

		public onAddonLoaded     OnAddonLoaded;
		public onAddonFileLoaded OnAddonFileLoaded;
		
		public AddonsLoader(string AddonsPath, string addonsExtension)
		{
			ADDPath      = AddonsPath;
			ADDExtension = addonsExtension;
		}

		public List<DBAddon> LoadAddons()
		{
			if (!Directory.Exists(ADDPath))
			{
				Directory.CreateDirectory(ADDPath);
				return null;
			}
			
			string[] files = Directory.GetFiles(ADDPath, $"*{ADDExtension}", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				Assembly.LoadFile(Path.GetFullPath(file));
				if(OnAddonFileLoaded != null)
					OnAddonFileLoaded.Invoke(file);
			}

			List<DBAddon> addons = new List<DBAddon>();
			try
			{
				
				Type interfaceType = typeof(DBAddon);
				Type[] types = AppDomain.CurrentDomain.GetAssemblies()
										.SelectMany(t => t.GetTypes())
										.Where(t => t.IsClass && interfaceType.IsAssignableFrom(t))
										.ToArray();
				foreach (var type in types)
				{
					try
					{
						DBAddon addon = (DBAddon) Activator.CreateInstance(type);
						addons.Add(addon);
						if (OnAddonLoaded != null)
							OnAddonLoaded.Invoke(type.FullName, true, addon);
						
					} catch (Exception exception)
					{
						if (OnAddonLoaded != null)
							OnAddonLoaded.Invoke(type.FullName, false,null, exception);
					}
				}
				
			} catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}

			return addons;

		}
	}
}
