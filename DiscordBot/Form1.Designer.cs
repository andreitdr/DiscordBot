namespace DiscordBot
{
    partial class Form1
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonManagePlugins = new System.Windows.Forms.Button();
			this.labelFailedLoadPlugin = new System.Windows.Forms.Label();
			this.buttonReloadPlugins = new System.Windows.Forms.Button();
			this.labelClipboardCopy = new System.Windows.Forms.Label();
			this.buttonCopyToken = new System.Windows.Forms.Button();
			this.textBoxPrefix = new System.Windows.Forms.TextBox();
			this.textBoxToken = new System.Windows.Forms.TextBox();
			this.labelPrefix = new System.Windows.Forms.Label();
			this.labelToken = new System.Windows.Forms.Label();
			this.buttonStartBot = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.labelConnectionStatus = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.buttonManagePlugins);
			this.groupBox1.Controls.Add(this.labelFailedLoadPlugin);
			this.groupBox1.Controls.Add(this.buttonReloadPlugins);
			this.groupBox1.Controls.Add(this.labelClipboardCopy);
			this.groupBox1.Controls.Add(this.buttonCopyToken);
			this.groupBox1.Controls.Add(this.textBoxPrefix);
			this.groupBox1.Controls.Add(this.textBoxToken);
			this.groupBox1.Controls.Add(this.labelPrefix);
			this.groupBox1.Controls.Add(this.labelToken);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBox1.Location = new System.Drawing.Point(456, 28);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(319, 238);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Bot Information";
			// 
			// buttonManagePlugins
			// 
			this.buttonManagePlugins.AutoSize = true;
			this.buttonManagePlugins.Location = new System.Drawing.Point(184, 151);
			this.buttonManagePlugins.Name = "buttonManagePlugins";
			this.buttonManagePlugins.Size = new System.Drawing.Size(93, 23);
			this.buttonManagePlugins.TabIndex = 7;
			this.buttonManagePlugins.Text = "Manage Plugins";
			this.buttonManagePlugins.UseVisualStyleBackColor = true;
			// 
			// labelFailedLoadPlugin
			// 
			this.labelFailedLoadPlugin.AutoSize = true;
			this.labelFailedLoadPlugin.ForeColor = System.Drawing.Color.Coral;
			this.labelFailedLoadPlugin.Location = new System.Drawing.Point(44, 198);
			this.labelFailedLoadPlugin.Name = "labelFailedLoadPlugin";
			this.labelFailedLoadPlugin.Size = new System.Drawing.Size(255, 13);
			this.labelFailedLoadPlugin.TabIndex = 6;
			this.labelFailedLoadPlugin.Text = "Can not load plugins if the bot is not in ONLINE state";
			this.labelFailedLoadPlugin.Visible = false;
			// 
			// buttonReloadPlugins
			// 
			this.buttonReloadPlugins.AutoSize = true;
			this.buttonReloadPlugins.Location = new System.Drawing.Point(65, 151);
			this.buttonReloadPlugins.Name = "buttonReloadPlugins";
			this.buttonReloadPlugins.Size = new System.Drawing.Size(88, 23);
			this.buttonReloadPlugins.TabIndex = 4;
			this.buttonReloadPlugins.Text = "Load Plugins";
			this.buttonReloadPlugins.UseVisualStyleBackColor = true;
			// 
			// labelClipboardCopy
			// 
			this.labelClipboardCopy.AutoSize = true;
			this.labelClipboardCopy.Location = new System.Drawing.Point(124, 74);
			this.labelClipboardCopy.Name = "labelClipboardCopy";
			this.labelClipboardCopy.Size = new System.Drawing.Size(189, 13);
			this.labelClipboardCopy.TabIndex = 5;
			this.labelClipboardCopy.Text = "Successfully copied token to clipboard";
			this.labelClipboardCopy.Visible = false;
			// 
			// buttonCopyToken
			// 
			this.buttonCopyToken.Location = new System.Drawing.Point(238, 48);
			this.buttonCopyToken.Name = "buttonCopyToken";
			this.buttonCopyToken.Size = new System.Drawing.Size(44, 23);
			this.buttonCopyToken.TabIndex = 4;
			this.buttonCopyToken.Text = "copy";
			this.buttonCopyToken.UseVisualStyleBackColor = true;
			// 
			// textBoxPrefix
			// 
			this.textBoxPrefix.Location = new System.Drawing.Point(85, 99);
			this.textBoxPrefix.MaxLength = 1;
			this.textBoxPrefix.Name = "textBoxPrefix";
			this.textBoxPrefix.ReadOnly = true;
			this.textBoxPrefix.Size = new System.Drawing.Size(35, 20);
			this.textBoxPrefix.TabIndex = 3;
			this.textBoxPrefix.TabStop = false;
			this.textBoxPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBoxToken
			// 
			this.textBoxToken.Location = new System.Drawing.Point(85, 50);
			this.textBoxToken.Name = "textBoxToken";
			this.textBoxToken.ReadOnly = true;
			this.textBoxToken.Size = new System.Drawing.Size(148, 20);
			this.textBoxToken.TabIndex = 2;
			this.textBoxToken.TabStop = false;
			this.textBoxToken.UseSystemPasswordChar = true;
			// 
			// labelPrefix
			// 
			this.labelPrefix.AutoSize = true;
			this.labelPrefix.Location = new System.Drawing.Point(25, 102);
			this.labelPrefix.Name = "labelPrefix";
			this.labelPrefix.Size = new System.Drawing.Size(33, 13);
			this.labelPrefix.TabIndex = 1;
			this.labelPrefix.Text = "Prefix";
			// 
			// labelToken
			// 
			this.labelToken.AutoSize = true;
			this.labelToken.Location = new System.Drawing.Point(22, 53);
			this.labelToken.Name = "labelToken";
			this.labelToken.Size = new System.Drawing.Size(38, 13);
			this.labelToken.TabIndex = 0;
			this.labelToken.Text = "Token";
			// 
			// buttonStartBot
			// 
			this.buttonStartBot.Location = new System.Drawing.Point(583, 281);
			this.buttonStartBot.Name = "buttonStartBot";
			this.buttonStartBot.Size = new System.Drawing.Size(75, 23);
			this.buttonStartBot.TabIndex = 1;
			this.buttonStartBot.Text = "Start Bot";
			this.buttonStartBot.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.Location = new System.Drawing.Point(22, 28);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(362, 337);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// labelConnectionStatus
			// 
			this.labelConnectionStatus.AutoSize = true;
			this.labelConnectionStatus.Location = new System.Drawing.Point(472, 12);
			this.labelConnectionStatus.Name = "labelConnectionStatus";
			this.labelConnectionStatus.Size = new System.Drawing.Size(0, 13);
			this.labelConnectionStatus.TabIndex = 3;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(787, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// languageToolStripMenuItem
			// 
			this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
			this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
			this.languageToolStripMenuItem.Text = "Language";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(787, 387);
			this.Controls.Add(this.labelConnectionStatus);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.buttonStartBot);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Discord Bot";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCopyToken;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.Label labelToken;
        private System.Windows.Forms.Button buttonStartBot;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labelConnectionStatus;
        private System.Windows.Forms.Label labelClipboardCopy;
        private System.Windows.Forms.Button buttonReloadPlugins;
        private System.Windows.Forms.Label labelFailedLoadPlugin;
        private System.Windows.Forms.Button buttonManagePlugins;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
	}
}

