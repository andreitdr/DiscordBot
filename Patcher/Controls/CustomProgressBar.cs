using System.Drawing;
using System.Windows.Forms;

namespace Patcher.Controls
{
	public class CustomProgressBar
	{
		public delegate void ProcentChanged(int newPercent);

		private readonly Panel          master;
		private readonly Panel          subPanel;
		public           ProcentChanged percentChanged;

		public CustomProgressBar(Panel p1, Panel p2)
		{
			master   = p1;
			subPanel = p2;

			subPanel.Width  = 0;
			subPanel.Height = master.Height;
		}


		public Color BackColor
		{
			set => master.BackColor = value;
		}

		public Color ForeColor
		{
			set => subPanel.BackColor = value;
		}

		public int GetPercent => 100 * subPanel.Width / master.Width;

		public int SetPercent
		{
			set
			{
				subPanel.Invoke(new MethodInvoker(delegate { subPanel.Width = value * master.Width / 100; }));
				if (percentChanged != null)
					percentChanged.Invoke(GetPercent);
			}
		}
	}
}