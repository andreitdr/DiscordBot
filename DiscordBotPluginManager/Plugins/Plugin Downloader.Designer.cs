
namespace DiscordBotPluginManager.Plugins
{
	partial class Plugin_Downloader
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.PluginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PluginType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PluginDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PluginDownload = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PluginName,
            this.PluginType,
            this.PluginDescription,
            this.PluginDownload});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(670, 530);
			this.dataGridView1.TabIndex = 0;
			// 
			// PluginName
			// 
			this.PluginName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PluginName.HeaderText = "PluginName";
			this.PluginName.Name = "PluginName";
			this.PluginName.ReadOnly = true;
			this.PluginName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// PluginType
			// 
			this.PluginType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PluginType.HeaderText = "Plugin Type";
			this.PluginType.Name = "PluginType";
			this.PluginType.ReadOnly = true;
			this.PluginType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// PluginDescription
			// 
			this.PluginDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PluginDescription.HeaderText = "Description";
			this.PluginDescription.Name = "PluginDescription";
			this.PluginDescription.ReadOnly = true;
			this.PluginDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// PluginDownload
			// 
			this.PluginDownload.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PluginDownload.HeaderText = "Download";
			this.PluginDownload.Name = "PluginDownload";
			this.PluginDownload.ReadOnly = true;
			this.PluginDownload.Text = "Download";
			// 
			// Plugin_Downloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(670, 530);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Plugin_Downloader";
			this.Text = "Plugin_Downloader";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn PluginName;
		private System.Windows.Forms.DataGridViewTextBoxColumn PluginType;
		private System.Windows.Forms.DataGridViewTextBoxColumn PluginDescription;
		private System.Windows.Forms.DataGridViewButtonColumn PluginDownload;
	}
}