
namespace DiscordBotPluginManager.Plugins
{
    partial class Plugins_Manager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1                   = new System.Windows.Forms.GroupBox();
            this.dataGridAddons              = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDescription           = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3  = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox2                   = new System.Windows.Forms.GroupBox();
            this.dataGridCommands            = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usage                       = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridAddons)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridAddons);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name     = "groupBox1";
            this.groupBox1.Size     = new System.Drawing.Size(557, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop  = false;
            this.groupBox1.Text     = "Addons";
            // 
            // dataGridAddons
            // 
            this.dataGridAddons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAddons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.columnDescription, this.dataGridViewTextBoxColumn3});
            this.dataGridAddons.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.dataGridAddons.Location = new System.Drawing.Point(3, 16);
            this.dataGridAddons.Name     = "dataGridAddons";
            this.dataGridAddons.Size     = new System.Drawing.Size(551, 358);
            this.dataGridAddons.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn1.HeaderText   = "NrCrt";
            this.dataGridViewTextBoxColumn1.Name         = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly     = true;
            this.dataGridViewTextBoxColumn1.Resizable    = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode     = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width        = 37;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode     = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment                 = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText       = "Path";
            this.dataGridViewTextBoxColumn2.Name             = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly         = true;
            this.dataGridViewTextBoxColumn2.SortMode         = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnDescription.HeaderText   = "Description";
            this.columnDescription.Name         = "columnDescription";
            this.columnDescription.ReadOnly     = true;
            this.columnDescription.SortMode     = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn3.HeaderText   = "Load State";
            this.dataGridViewTextBoxColumn3.Name         = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly     = true;
            this.dataGridViewTextBoxColumn3.Resizable    = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width        = 65;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridCommands);
            this.groupBox2.Location = new System.Drawing.Point(635, 12);
            this.groupBox2.Name     = "groupBox2";
            this.groupBox2.Size     = new System.Drawing.Size(554, 377);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop  = false;
            this.groupBox2.Text     = "Commands";
            // 
            // dataGridCommands
            // 
            this.dataGridCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.Usage, this.dataGridViewCheckBoxColumn1});
            this.dataGridCommands.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.dataGridCommands.Location = new System.Drawing.Point(3, 16);
            this.dataGridCommands.Name     = "dataGridCommands";
            this.dataGridCommands.Size     = new System.Drawing.Size(548, 358);
            this.dataGridCommands.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn4.HeaderText   = "NrCrt";
            this.dataGridViewTextBoxColumn4.Name         = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly     = true;
            this.dataGridViewTextBoxColumn4.Resizable    = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode     = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width        = 37;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode     = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment                 = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText       = "Command";
            this.dataGridViewTextBoxColumn5.Name             = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly         = true;
            this.dataGridViewTextBoxColumn5.SortMode         = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText   = "Description";
            this.dataGridViewTextBoxColumn6.Name         = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly     = true;
            this.dataGridViewTextBoxColumn6.SortMode     = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Usage
            // 
            this.Usage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Usage.HeaderText   = "Usage";
            this.Usage.Name         = "Usage";
            this.Usage.ReadOnly     = true;
            this.Usage.SortMode     = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewCheckBoxColumn1.HeaderText   = "Load State";
            this.dataGridViewCheckBoxColumn1.Name         = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly     = true;
            this.dataGridViewCheckBoxColumn1.Resizable    = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.Width        = 65;
            // 
            // Plugins_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1224, 429);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Plugins_Manager";
            this.Text = "Plugins_Manager";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridAddons)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridCommands)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn  dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn  dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn  dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn  Usage;

        #endregion

        private System.Windows.Forms.GroupBox                   groupBox1;
        private System.Windows.Forms.DataGridView               dataGridAddons;
        private System.Windows.Forms.GroupBox                   groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn  dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn  dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn  columnDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView               dataGridCommands;
    }
}