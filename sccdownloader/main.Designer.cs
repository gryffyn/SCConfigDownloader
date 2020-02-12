namespace sccdownloader
{
    partial class main
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
            this.components = new System.ComponentModel.Container();
            this.inputAppID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.get = new System.Windows.Forms.Button();
            this.datagridConfigs = new System.Windows.Forms.DataGridView();
            this.appDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RatesUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RatesDown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.currentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountInfoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize) (this.datagridConfigs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.configItemBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // inputAppID
            //
            this.inputAppID.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.inputAppID.Location = new System.Drawing.Point(58, 27);
            this.inputAppID.Name = "inputAppID";
            this.inputAppID.Size = new System.Drawing.Size(768, 23);
            this.inputAppID.TabIndex = 0;
            this.inputAppID.TextChanged += new System.EventHandler(this.inputAppID_TextChanged);
            this.inputAppID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputAppID_KeyUp);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "AppID";
            //
            // get
            //
            this.get.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.get.Location = new System.Drawing.Point(838, 26);
            this.get.Name = "get";
            this.get.Size = new System.Drawing.Size(87, 23);
            this.get.TabIndex = 2;
            this.get.Text = "Get";
            this.get.UseVisualStyleBackColor = true;
            this.get.Click += new System.EventHandler(this.get_Click);
            //
            // datagridConfigs
            //
            this.datagridConfigs.AllowUserToAddRows = false;
            this.datagridConfigs.AllowUserToDeleteRows = false;
            this.datagridConfigs.AllowUserToResizeRows = false;
            this.datagridConfigs.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top |
                                                         System.Windows.Forms.AnchorStyles.Bottom) |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.datagridConfigs.AutoGenerateColumns = false;
            this.datagridConfigs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.datagridConfigs.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                {this.appDataGridViewTextBoxColumn, this.nameDataGridViewTextBoxColumn, this.RatesUp, this.RatesDown});
            this.datagridConfigs.DataSource = this.configItemBindingSource;
            this.datagridConfigs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.datagridConfigs.Location = new System.Drawing.Point(12, 56);
            this.datagridConfigs.MultiSelect = false;
            this.datagridConfigs.Name = "datagridConfigs";
            this.datagridConfigs.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.datagridConfigs.RowHeadersVisible = false;
            this.datagridConfigs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridConfigs.Size = new System.Drawing.Size(913, 380);
            this.datagridConfigs.TabIndex = 3;
            this.datagridConfigs.CellDoubleClick +=
                new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            //
            // appDataGridViewTextBoxColumn
            //
            this.appDataGridViewTextBoxColumn.DataPropertyName = "App";
            this.appDataGridViewTextBoxColumn.HeaderText = "App";
            this.appDataGridViewTextBoxColumn.Name = "appDataGridViewTextBoxColumn";
            //
            // nameDataGridViewTextBoxColumn
            //
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            //
            // RatesUp
            //
            this.RatesUp.DataPropertyName = "RatesUp";
            this.RatesUp.HeaderText = "RatesUp";
            this.RatesUp.Name = "RatesUp";
            //
            // RatesDown
            //
            this.RatesDown.DataPropertyName = "RatesDown";
            this.RatesDown.HeaderText = "RatesDown";
            this.RatesDown.Name = "RatesDown";
            //
            // configItemBindingSource
            //
            this.configItemBindingSource.DataSource = typeof(sccdownloader.ConfigItem);
            //
            // saveFileDialog1
            //
            this.saveFileDialog1.Filter = "Config|*.vdf";
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.currentStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(937, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            //
            // currentStatus
            //
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(128, 17);
            this.currentStatus.Text = "Connecting to Steam...";
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.fileToolStripMenuItem1, this.accountToolStripMenuItem, this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(937, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            //
            // fileToolStripMenuItem1
            //
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            //TODO this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            //
            // accountToolStripMenuItem
            //
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.accountInfoToolStripMenuItem, this.toolStripSeparator1, this.accountInfoToolStripMenuItem1});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "Account";
            //
            // settingsToolStripMenuItem
            //
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // TODO
            //settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            //
            // accountInfoToolStripMenuItem
            //
            this.accountInfoToolStripMenuItem.Name = "accountInfoToolStripMenuItem";
            this.accountInfoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.accountInfoToolStripMenuItem.Text = "Re-login";
            //
            // accountInfoToolStripMenuItem1
            //
            this.accountInfoToolStripMenuItem1.Name = "accountInfoToolStripMenuItem1";
            this.accountInfoToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.accountInfoToolStripMenuItem1.Text = "Account Info...";
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            //
            // exitToolStripMenuItem
            //
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            //
            // main
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 470);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.datagridConfigs);
            this.Controls.Add(this.get);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputAppID);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "main";
            this.Text = "SCConfigDownloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.main_FormClosed);
            this.Load += new System.EventHandler(this.main_Load);
            ((System.ComponentModel.ISupportInitialize) (this.datagridConfigs)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.configItemBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox inputAppID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button get;
        private System.Windows.Forms.DataGridView datagridConfigs;
        private System.Windows.Forms.DataGridViewTextBoxColumn appDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource configItemBindingSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RatesUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn RatesDown;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel currentStatus;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountInfoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem accountInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

