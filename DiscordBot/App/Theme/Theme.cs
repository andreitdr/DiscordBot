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
        public string ThemeName { get; private set; }

        private readonly Color MainBackColor, MainForeColor, SecBackColor, SecForeColor;

        public Theme(string Name, Color MainBackColor, Color MainForeColor, Color SecBackColor, Color SecForeColor) {
            this.MainBackColor = MainBackColor;
            this.MainForeColor = MainForeColor;
            this.SecBackColor = SecBackColor;
            this.SecForeColor = SecForeColor;

            ThemeName = Name;
        }


        public void SetTheme(Control form) {
            form.BackColor = MainBackColor;
            form.ForeColor = MainForeColor;
            foreach (Control c in form.Controls) SetTheme(c);
        }

    }
}
