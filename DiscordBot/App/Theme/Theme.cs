using DiscordBotPluginManager;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBot.App.Theme
{
    public class Theme
    {
        public static Theme CurrentTheme;
        public string ThemeName { get; private set; }

        private readonly Color MainBackColor, MainForeColor, SecBackColor, SecForeColor;

        public Theme(string Name, Color MainBackColor, Color MainForeColor, Color SecBackColor, Color SecForeColor)
        {
            this.MainBackColor = MainBackColor;
            this.MainForeColor = MainForeColor;
            this.SecBackColor = SecBackColor;
            this.SecForeColor = SecForeColor;

            ThemeName = Name;
        }

        public static Theme LoadFromFile(string path)
        {
            try
            {
                string tn = Functions.readCodeFromFile(path, "THEME_NAME", '=');
                string tmbc = Functions.readCodeFromFile(path, "THEME_MAIN_BACKGROUND_COLOR", '=');
                string tmfc = Functions.readCodeFromFile(path, "THEME_MAIN_FOREGROUND_COLOR", '=');
                string tsbc = Functions.readCodeFromFile(path, "THEME_SEC_BACKGROUND_COLOR", '=');
                string tsfc = Functions.readCodeFromFile(path, "THEME_SEC_FOREGROUND_COLOR", '=');

                return new Theme(tn, ColorTranslator.FromHtml(tmbc), ColorTranslator.FromHtml(tmfc),
                                ColorTranslator.FromHtml(tsbc), ColorTranslator.FromHtml(tsfc));
            }
            catch { return null; }

        }


        public void SetTheme(Control form)
        {
            form.BackColor = MainBackColor;
            form.ForeColor = MainForeColor;
            foreach (Control c in form.Controls) SetTheme(c);
            CurrentTheme = this;
        }

    }
}
