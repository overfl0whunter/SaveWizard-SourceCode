namespace PS3SaveEditor
{
	// Token: 0x020001CE RID: 462
	public partial class MainForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x0007B9E8 File Offset: 0x00079BE8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0007BA20 File Offset: 0x00079C20
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.MainForm));
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.simpleToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.advancedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.resignToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.registerPSNIDToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.restoreFromBackupToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.deleteSaveToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btnHome = new global::System.Windows.Forms.Button();
			this.btnHelp = new global::System.Windows.Forms.Button();
			this.btnOptions = new global::System.Windows.Forms.Button();
			this.pnlHome = new global::System.Windows.Forms.Panel();
			this.chkShowAll = new global::System.Windows.Forms.CheckBox();
			this.dgServerGames = new global::CSUST.Data.CustomDataGridView();
			this.Choose = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn5 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnlNoSaves = new global::System.Windows.Forms.Panel();
			this.lblNoSaves = new global::System.Windows.Forms.Label();
			this.btnGamesInServer = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cbDrives = new global::System.Windows.Forms.ComboBox();
			this.pnlBackup = new global::System.Windows.Forms.Panel();
			this.groupBox3 = new global::PS3SaveEditor.CustomGroupBox();
			this.gbManageProfile = new global::PS3SaveEditor.CustomGroupBox();
			this.gbProfiles = new global::PS3SaveEditor.CustomGroupBox();
			this.lblManageProfiles = new global::System.Windows.Forms.Label();
			this.btnManageProfiles = new global::System.Windows.Forms.Button();
			this.groupBox2 = new global::PS3SaveEditor.CustomGroupBox();
			this.cbLanguage = new global::System.Windows.Forms.ComboBox();
			this.lblLanguage = new global::System.Windows.Forms.Label();
			this.lblDeactivate = new global::System.Windows.Forms.Label();
			this.btnDeactivate = new global::System.Windows.Forms.Button();
			this.groupBox1 = new global::PS3SaveEditor.CustomGroupBox();
			this.lblRSSSection = new global::System.Windows.Forms.Label();
			this.btnRss = new global::System.Windows.Forms.Button();
			this.gbBackupLocation = new global::PS3SaveEditor.CustomGroupBox();
			this.btnOpenFolder = new global::System.Windows.Forms.Button();
			this.lblBackup = new global::System.Windows.Forms.Label();
			this.btnBrowse = new global::System.Windows.Forms.Button();
			this.txtBackupLocation = new global::System.Windows.Forms.TextBox();
			this.chkBackup = new global::System.Windows.Forms.CheckBox();
			this.btnApply = new global::System.Windows.Forms.Button();
			this.Multi = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.picContact = new global::System.Windows.Forms.PictureBox();
			this.picVersion = new global::System.Windows.Forms.PictureBox();
			this.pictureBox2 = new global::System.Windows.Forms.PictureBox();
			this.picTraffic = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtSerial4 = new global::System.Windows.Forms.TextBox();
			this.txtSerial3 = new global::System.Windows.Forms.TextBox();
			this.txtSerial2 = new global::System.Windows.Forms.TextBox();
			this.txtSerial1 = new global::System.Windows.Forms.TextBox();
			this.btnActivatePackage = new global::System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			this.pnlHome.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dgServerGames).BeginInit();
			this.pnlNoSaves.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlBackup.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gbManageProfile.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.gbBackupLocation.SuspendLayout();
			this.panel3.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picContact).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picVersion).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picTraffic).BeginInit();
			base.SuspendLayout();
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.simpleToolStripMenuItem, this.advancedToolStripMenuItem, this.toolStripSeparator1, this.resignToolStripMenuItem, this.registerPSNIDToolStripMenuItem, this.toolStripSeparator2, this.restoreFromBackupToolStripMenuItem, this.deleteSaveToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(185, 148));
			this.contextMenuStrip1.Opening += new global::System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			this.simpleToolStripMenuItem.Name = "simpleToolStripMenuItem";
			this.simpleToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.simpleToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.simpleToolStripMenuItem.Font);
			this.simpleToolStripMenuItem.Text = "Simple";
			this.simpleToolStripMenuItem.Click += new global::System.EventHandler(this.simpleToolStripMenuItem_Click);
			this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
			this.advancedToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.advancedToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.advancedToolStripMenuItem.Font);
			this.advancedToolStripMenuItem.Text = "Advanced";
			this.advancedToolStripMenuItem.Click += new global::System.EventHandler(this.advancedToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 6));
			this.resignToolStripMenuItem.Name = "resignToolStripMenuItem";
			this.resignToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.resignToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.resignToolStripMenuItem.Font);
			this.resignToolStripMenuItem.Text = "Re-sign...";
			this.resignToolStripMenuItem.Click += new global::System.EventHandler(this.resignToolStripMenuItem_Click);
			this.registerPSNIDToolStripMenuItem.Name = "registerPSNIDToolStripMenuItem";
			this.registerPSNIDToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.registerPSNIDToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.registerPSNIDToolStripMenuItem.Font);
			this.registerPSNIDToolStripMenuItem.Text = "Register PSN ID...";
			this.registerPSNIDToolStripMenuItem.Click += new global::System.EventHandler(this.registerPSNIDToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 6));
			this.restoreFromBackupToolStripMenuItem.Name = "restoreFromBackupToolStripMenuItem";
			this.restoreFromBackupToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.restoreFromBackupToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.restoreFromBackupToolStripMenuItem.Font);
			this.restoreFromBackupToolStripMenuItem.Text = "Restore from Backup";
			this.restoreFromBackupToolStripMenuItem.Click += new global::System.EventHandler(this.restoreFromBackupToolStripMenuItem_Click);
			this.deleteSaveToolStripMenuItem.Name = "deleteSaveToolStripMenuItem";
			this.deleteSaveToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 22));
			this.deleteSaveToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.deleteSaveToolStripMenuItem.Font);
			this.deleteSaveToolStripMenuItem.Text = "Delete Save";
			this.deleteSaveToolStripMenuItem.Click += new global::System.EventHandler(this.deleteSaveToolStripMenuItem_Click);
			this.btnHome.BackColor = global::System.Drawing.Color.Transparent;
			this.btnHome.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnHome.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(127, 215, 255);
			this.btnHome.FlatAppearance.BorderSize = 0;
			this.btnHome.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnHome.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnHome.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(15));
			this.btnHome.Name = "btnHome";
			this.btnHome.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 61));
			this.btnHome.TabIndex = 3;
			this.btnHome.UseVisualStyleBackColor = false;
			this.btnHome.Click += new global::System.EventHandler(this.btnHome_Click);
			this.btnHelp.BackColor = global::System.Drawing.Color.Transparent;
			this.btnHelp.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnHelp.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(127, 215, 255);
			this.btnHelp.FlatAppearance.BorderSize = 0;
			this.btnHelp.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnHelp.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnHelp.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(143));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 61));
			this.btnHelp.TabIndex = 6;
			this.btnHelp.UseVisualStyleBackColor = false;
			this.btnHelp.Click += new global::System.EventHandler(this.btnHelp_Click);
			this.btnOptions.BackColor = global::System.Drawing.Color.Transparent;
			this.btnOptions.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnOptions.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(127, 215, 255);
			this.btnOptions.FlatAppearance.BorderSize = 0;
			this.btnOptions.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnOptions.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnOptions.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(79));
			this.btnOptions.Name = "btnOptions";
			this.btnOptions.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 61));
			this.btnOptions.TabIndex = 5;
			this.btnOptions.UseVisualStyleBackColor = false;
			this.btnOptions.Click += new global::System.EventHandler(this.btnBackup_Click);
			this.pnlHome.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlHome.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.pnlHome.Controls.Add(this.chkShowAll);
			this.pnlHome.Controls.Add(this.dgServerGames);
			this.pnlHome.Controls.Add(this.pnlNoSaves);
			this.pnlHome.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(257), global::PS3SaveEditor.Util.ScaleSize(15));
			this.pnlHome.Name = "pnlHome";
			this.pnlHome.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 347));
			this.pnlHome.TabIndex = 8;
			this.chkShowAll.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.chkShowAll.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(324));
			this.chkShowAll.Name = "chkShowAll";
			this.chkShowAll.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(97, 16));
			this.chkShowAll.TabIndex = 11;
			this.chkShowAll.Text = "Show All";
			this.chkShowAll.UseVisualStyleBackColor = true;
			this.dgServerGames.AllowUserToAddRows = false;
			this.dgServerGames.AllowUserToDeleteRows = false;
			this.dgServerGames.AllowUserToResizeRows = false;
			this.dgServerGames.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.dgServerGames.ClipboardCopyMode = global::System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dgServerGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgServerGames.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgServerGames.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.Choose, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewCheckBoxColumn1, this.dataGridViewTextBoxColumn5 });
			this.dgServerGames.ContextMenuStrip = this.contextMenuStrip1;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgServerGames.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgServerGames.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.dgServerGames.Name = "dgServerGames";
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dgServerGames.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgServerGames.RowHeadersVisible = false;
			this.dgServerGames.RowHeadersWidth = global::PS3SaveEditor.Util.ScaleSize(25);
			this.dgServerGames.RowTemplate.Height = global::PS3SaveEditor.Util.ScaleSize(24);
			this.dgServerGames.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgServerGames.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(484, 304));
			this.dgServerGames.TabIndex = 1;
			this.Choose.Frozen = true;
			this.Choose.HeaderText = "Choose";
			this.Choose.Name = "Choose";
			this.Choose.ReadOnly = true;
			this.Choose.Width = global::PS3SaveEditor.Util.ScaleSize(20);
			this.dataGridViewTextBoxColumn1.FillWeight = global::PS3SaveEditor.Util.ScaleSize(20f);
			this.dataGridViewTextBoxColumn1.Frozen = true;
			this.dataGridViewTextBoxColumn1.HeaderText = "Game Name";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = global::PS3SaveEditor.Util.ScaleSize(240);
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn2.Frozen = true;
			this.dataGridViewTextBoxColumn2.HeaderText = "Cheats";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = global::PS3SaveEditor.Util.ScaleSize(60);
			dataGridViewCellStyle5.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewTextBoxColumn3.HeaderText = "GameCode";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = global::PS3SaveEditor.Util.ScaleSize(80);
			this.dataGridViewTextBoxColumn4.HeaderText = "Client";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = global::PS3SaveEditor.Util.ScaleSize(80);
			this.dataGridViewCheckBoxColumn1.HeaderText = "Local Save Exist";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Visible = false;
			this.dataGridViewTextBoxColumn5.HeaderText = "Client";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Visible = false;
			this.pnlNoSaves.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlNoSaves.Controls.Add(this.lblNoSaves);
			this.pnlNoSaves.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.pnlNoSaves.Name = "pnlNoSaves";
			this.pnlNoSaves.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(485, 311));
			this.pnlNoSaves.TabIndex = 7;
			this.pnlNoSaves.Visible = false;
			this.lblNoSaves.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblNoSaves.BackColor = global::System.Drawing.Color.Transparent;
			this.lblNoSaves.ForeColor = global::System.Drawing.Color.White;
			this.lblNoSaves.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(100));
			this.lblNoSaves.Name = "lblNoSaves";
			this.lblNoSaves.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(480, 13));
			this.lblNoSaves.TabIndex = 10;
			this.lblNoSaves.Text = "No PS4 saves were found on this USB drive.";
			this.lblNoSaves.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.btnGamesInServer.Location = new global::System.Drawing.Point(0, 0);
			this.btnGamesInServer.Name = "btnGamesInServer";
			this.btnGamesInServer.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnGamesInServer.TabIndex = 0;
			this.panel1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left;
			this.panel1.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(332));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 30));
			this.panel1.TabIndex = 11;
			this.cbDrives.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDrives.FormattingEnabled = true;
			this.cbDrives.IntegralHeight = false;
			bool flag = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag)
			{
				this.cbDrives.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(65), global::PS3SaveEditor.Util.ScaleSize(2));
				this.cbDrives.Width = global::PS3SaveEditor.Util.ScaleSize(165);
			}
			else
			{
				this.cbDrives.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(185), global::PS3SaveEditor.Util.ScaleSize(5));
				this.cbDrives.Width = global::PS3SaveEditor.Util.ScaleSize(45);
			}
			this.cbDrives.Name = "cbDrives";
			this.cbDrives.TabIndex = 3;
			this.panel1.Controls.Add(this.cbDrives);
			this.pnlBackup.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlBackup.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.pnlBackup.Controls.Add(this.groupBox3);
			this.pnlBackup.Controls.Add(this.gbManageProfile);
			this.pnlBackup.Controls.Add(this.groupBox2);
			this.pnlBackup.Controls.Add(this.groupBox1);
			this.pnlBackup.Controls.Add(this.gbBackupLocation);
			this.pnlBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(257), global::PS3SaveEditor.Util.ScaleSize(15));
			this.pnlBackup.Name = "pnlBackup";
			this.pnlBackup.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 347));
			this.pnlBackup.TabIndex = 9;
			this.groupBox3.Controls.Add(this.btnActivatePackage);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtSerial4);
			this.groupBox3.Controls.Add(this.txtSerial3);
			this.groupBox3.Controls.Add(this.txtSerial2);
			this.groupBox3.Controls.Add(this.txtSerial1);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(276));
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 64));
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "groupBox3";
			this.gbManageProfile.Controls.Add(this.gbProfiles);
			this.gbManageProfile.Controls.Add(this.lblManageProfiles);
			this.gbManageProfile.Controls.Add(this.btnManageProfiles);
			this.gbManageProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(251), global::PS3SaveEditor.Util.ScaleSize(200));
			this.gbManageProfile.Name = "gbManageProfile";
			this.gbManageProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(244, 65));
			this.gbManageProfile.TabIndex = 10;
			this.gbManageProfile.TabStop = false;
			bool flag2 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag2)
			{
				this.gbProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(134), global::PS3SaveEditor.Util.ScaleSize(29));
				this.gbProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(80, 29));
			}
			else
			{
				this.gbProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(134), global::PS3SaveEditor.Util.ScaleSize(30));
				this.gbProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(80, 27));
			}
			this.gbProfiles.Name = "gbProfiles";
			this.gbProfiles.TabIndex = 9;
			this.gbProfiles.TabStop = false;
			this.lblManageProfiles.AutoSize = true;
			this.lblManageProfiles.ForeColor = global::System.Drawing.Color.White;
			bool flag3 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag3)
			{
				this.lblManageProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(12));
			}
			else
			{
				this.lblManageProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.lblManageProfiles.Name = "lblManageProfiles";
			this.lblManageProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(106, 13));
			this.lblManageProfiles.TabIndex = 8;
			this.lblManageProfiles.Text = "Manage PS4 Profiles";
			this.btnManageProfiles.AutoSize = true;
			this.btnManageProfiles.ForeColor = global::System.Drawing.Color.White;
			this.btnManageProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(33));
			this.btnManageProfiles.Name = "btnManageProfiles";
			this.btnManageProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(115, 23));
			this.btnManageProfiles.TabIndex = 0;
			this.btnManageProfiles.Text = "Manage Profiles";
			this.btnManageProfiles.UseVisualStyleBackColor = false;
			this.btnManageProfiles.Click += new global::System.EventHandler(this.btnManageProfiles_Click);
			this.groupBox2.Controls.Add(this.cbLanguage);
			this.groupBox2.Controls.Add(this.lblLanguage);
			this.groupBox2.Controls.Add(this.lblDeactivate);
			this.groupBox2.Controls.Add(this.btnDeactivate);
			this.groupBox2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(200));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 65));
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.cbLanguage.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLanguage.FormattingEnabled = true;
			bool flag4 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag4)
			{
				this.cbLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(335), global::PS3SaveEditor.Util.ScaleSize(32));
			}
			else
			{
				this.cbLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(335), global::PS3SaveEditor.Util.ScaleSize(36));
			}
			this.cbLanguage.Name = "cbLanguage";
			this.cbLanguage.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(142, 21));
			this.cbLanguage.TabIndex = 10;
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.BackColor = global::System.Drawing.Color.Transparent;
			this.lblLanguage.ForeColor = global::System.Drawing.Color.White;
			bool flag5 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag5)
			{
				this.lblLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(333), global::PS3SaveEditor.Util.ScaleSize(12));
			}
			else
			{
				this.lblLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(332), global::PS3SaveEditor.Util.ScaleSize(16));
			}
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(55, 13));
			this.lblLanguage.TabIndex = 9;
			this.lblLanguage.Text = "Language";
			this.lblDeactivate.AutoSize = true;
			this.lblDeactivate.ForeColor = global::System.Drawing.Color.White;
			bool flag6 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag6)
			{
				this.lblDeactivate.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			}
			else
			{
				this.lblDeactivate.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.lblDeactivate.Name = "lblDeactivate";
			this.lblDeactivate.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(42, 13));
			this.lblDeactivate.TabIndex = 8;
			this.lblDeactivate.Text = "Testing";
			this.btnDeactivate.AutoSize = true;
			this.btnDeactivate.ForeColor = global::System.Drawing.Color.White;
			bool flag7 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag7)
			{
				this.btnDeactivate.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(30));
			}
			else
			{
				this.btnDeactivate.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(35));
			}
			this.btnDeactivate.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(115, 23));
			this.btnDeactivate.Name = "btnDeactivate";
			this.btnDeactivate.TabIndex = 0;
			this.btnDeactivate.Text = "Deactivate";
			this.btnDeactivate.UseVisualStyleBackColor = false;
			this.btnDeactivate.Click += new global::System.EventHandler(this.btnDeactivate_Click);
			this.groupBox1.Controls.Add(this.lblRSSSection);
			this.groupBox1.Controls.Add(this.btnRss);
			this.groupBox1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(128));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 67));
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.lblRSSSection.AutoSize = true;
			this.lblRSSSection.ForeColor = global::System.Drawing.Color.White;
			bool flag8 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag8)
			{
				this.lblRSSSection.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			}
			else
			{
				this.lblRSSSection.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.lblRSSSection.Name = "lblRSSSection";
			this.lblRSSSection.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(295, 13));
			this.lblRSSSection.TabIndex = 6;
			this.lblRSSSection.Text = "Select below button to check the availability of latest version.";
			this.btnRss.ForeColor = global::System.Drawing.Color.White;
			bool flag9 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag9)
			{
				this.btnRss.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(33));
			}
			else
			{
				this.btnRss.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(37));
			}
			this.btnRss.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(115, 23));
			this.btnRss.Name = "btnRss";
			this.btnRss.TabIndex = 0;
			this.btnRss.Text = "Update";
			this.btnRss.UseVisualStyleBackColor = false;
			this.btnRss.Click += new global::System.EventHandler(this.btnRss_Click);
			this.gbBackupLocation.Controls.Add(this.btnOpenFolder);
			this.gbBackupLocation.Controls.Add(this.lblBackup);
			this.gbBackupLocation.Controls.Add(this.btnBrowse);
			this.gbBackupLocation.Controls.Add(this.txtBackupLocation);
			this.gbBackupLocation.Controls.Add(this.chkBackup);
			this.gbBackupLocation.Controls.Add(this.btnApply);
			this.gbBackupLocation.ForeColor = global::System.Drawing.Color.White;
			this.gbBackupLocation.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(8));
			this.gbBackupLocation.Margin = new global::System.Windows.Forms.Padding(0);
			this.gbBackupLocation.Name = "gbBackupLocation";
			this.gbBackupLocation.Padding = new global::System.Windows.Forms.Padding(0);
			this.gbBackupLocation.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 115));
			this.gbBackupLocation.TabIndex = 3;
			this.gbBackupLocation.TabStop = false;
			this.btnOpenFolder.ForeColor = global::System.Drawing.Color.White;
			this.btnOpenFolder.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(85));
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(123, 23));
			this.btnOpenFolder.TabIndex = 3;
			this.btnOpenFolder.Text = "Open Folder";
			this.btnOpenFolder.UseVisualStyleBackColor = false;
			this.btnOpenFolder.Click += new global::System.EventHandler(this.btnOpenFolder_Click);
			this.lblBackup.AutoSize = true;
			bool flag10 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag10)
			{
				this.lblBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(34));
			}
			else
			{
				this.lblBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(40));
			}
			this.lblBackup.Name = "lblBackup";
			this.lblBackup.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblBackup.TabIndex = 5;
			this.btnBrowse.ForeColor = global::System.Drawing.Color.White;
			bool flag11 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag11)
			{
				this.btnBrowse.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(281), global::PS3SaveEditor.Util.ScaleSize(54));
			}
			else
			{
				this.btnBrowse.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(281), global::PS3SaveEditor.Util.ScaleSize(60));
			}
			this.btnBrowse.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = false;
			this.btnBrowse.Click += new global::System.EventHandler(this.btnBrowse_Click);
			bool flag12 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag12)
			{
				this.txtBackupLocation.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(54));
				this.txtBackupLocation.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(264, 15));
			}
			else
			{
				this.txtBackupLocation.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(61));
				this.txtBackupLocation.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(264, 23));
			}
			this.txtBackupLocation.Name = "txtBackupLocation";
			this.txtBackupLocation.TabIndex = 0;
			this.chkBackup.AutoSize = true;
			this.chkBackup.ForeColor = global::System.Drawing.Color.White;
			bool flag13 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag13)
			{
				this.chkBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			}
			else
			{
				this.chkBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.chkBackup.Name = "chkBackup";
			this.chkBackup.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(96, 17));
			this.chkBackup.TabIndex = 0;
			this.chkBackup.Text = "Backup Saves";
			this.chkBackup.UseVisualStyleBackColor = true;
			this.chkBackup.CheckedChanged += new global::System.EventHandler(this.chkBackup_CheckedChanged);
			this.chkBackup.Click += new global::System.EventHandler(this.chkBackup_Click);
			this.btnApply.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnApply.ForeColor = global::System.Drawing.Color.White;
			this.btnApply.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(85));
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = false;
			this.btnApply.Visible = false;
			this.btnApply.Click += new global::System.EventHandler(this.btnApply_Click);
			this.Multi.FillWeight = global::PS3SaveEditor.Util.ScaleSize(20f);
			this.Multi.Frozen = true;
			this.Multi.Name = "Multi";
			this.Multi.ReadOnly = true;
			this.Multi.Width = global::PS3SaveEditor.Util.ScaleSize(20);
			this.panel2.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left;
			this.panel2.BackColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			this.panel2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(215));
			this.panel2.Name = "panel2";
			this.panel2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 3));
			this.panel2.TabIndex = global::PS3SaveEditor.Util.ScaleSize(12);
			this.panel3.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left;
			this.panel3.BackColor = global::System.Drawing.Color.FromArgb(0, 56, 115);
			this.panel3.BackgroundImage = (global::System.Drawing.Image)componentResourceManager.GetObject("panel3.BackgroundImage");
			this.panel3.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.panel3.Controls.Add(this.picContact);
			this.panel3.Controls.Add(this.picVersion);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Controls.Add(this.picTraffic);
			this.panel3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(207));
			this.panel3.Name = "panel3";
			this.panel3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 122));
			this.panel3.TabIndex = 13;
			this.picContact.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.picContact.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(23));
			this.picContact.Name = "picContact";
			this.picContact.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 24));
			this.picContact.TabIndex = 14;
			this.picContact.TabStop = false;
			this.picVersion.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.picVersion.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(23));
			this.picVersion.Name = "picVersion";
			this.picVersion.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 26));
			this.picVersion.TabIndex = 13;
			this.picVersion.TabStop = false;
			this.pictureBox2.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox2.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.pictureBox2.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(78));
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 45));
			this.pictureBox2.TabIndex = 12;
			this.pictureBox2.TabStop = false;
			this.picTraffic.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.picTraffic.Location = new global::System.Drawing.Point(0, 0);
			this.picTraffic.Name = "picTraffic";
			this.picTraffic.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 26));
			this.picTraffic.TabIndex = 11;
			this.picTraffic.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(14));
			this.label1.Name = "label1";
			this.label1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(86, 13));
			this.label1.TabIndex = 0;
			this.label1.Text = "lblPackageSerial";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.ForeColor = global::System.Drawing.Color.White;
			this.label4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(165), global::PS3SaveEditor.Util.ScaleSize(37));
			this.label4.Name = "label4";
			this.label4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label4.TabIndex = 19;
			this.label4.Text = "-";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.ForeColor = global::System.Drawing.Color.White;
			this.label3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(110), global::PS3SaveEditor.Util.ScaleSize(37));
			this.label3.Name = "label3";
			this.label3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label3.TabIndex = 18;
			this.label3.Text = "-";
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.Color.White;
			this.label2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(54), global::PS3SaveEditor.Util.ScaleSize(37));
			this.label2.Name = "label2";
			this.label2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label2.TabIndex = 17;
			this.label2.Text = "-";
			this.txtSerial4.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial4.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtSerial4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(179), global::PS3SaveEditor.Util.ScaleSize(34));
			this.txtSerial4.MaxLength = 4;
			this.txtSerial4.Name = "txtSerial4";
			this.txtSerial4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial4.TabIndex = 16;
			this.txtSerial3.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial3.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtSerial3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(123), global::PS3SaveEditor.Util.ScaleSize(34));
			this.txtSerial3.MaxLength = 4;
			this.txtSerial3.Name = "txtSerial3";
			this.txtSerial3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial3.TabIndex = 15;
			this.txtSerial2.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial2.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtSerial2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(67), global::PS3SaveEditor.Util.ScaleSize(34));
			this.txtSerial2.MaxLength = 4;
			this.txtSerial2.Name = "txtSerial2";
			this.txtSerial2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial2.TabIndex = 14;
			this.txtSerial1.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial1.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtSerial1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(34));
			this.txtSerial1.MaxLength = 4;
			this.txtSerial1.Name = "txtSerial1";
			this.txtSerial1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial1.TabIndex = 13;
			this.btnActivatePackage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(237), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnActivatePackage.Name = "btnActivatePackage";
			this.btnActivatePackage.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(55, 23));
			this.btnActivatePackage.TabIndex = 20;
			this.btnActivatePackage.Text = "Activate";
			this.btnActivatePackage.UseVisualStyleBackColor = true;
			this.btnActivatePackage.Click += new global::System.EventHandler(this.btnActivatePackage_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.FromArgb(0, 44, 101);
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(780, 377));
			base.Controls.Add(this.pnlBackup);
			base.Controls.Add(this.pnlHome);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnOptions);
			base.Controls.Add(this.btnHome);
			base.Controls.Add(this.btnHelp);
			this.DoubleBuffered = true;
			this.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(780, 377));
			base.Name = "MainForm";
			this.Text = "PS4 Save Editor";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.contextMenuStrip1.ResumeLayout(false);
			this.pnlHome.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dgServerGames).EndInit();
			this.pnlNoSaves.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.pnlBackup.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.gbManageProfile.ResumeLayout(false);
			this.gbManageProfile.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbBackupLocation.ResumeLayout(false);
			this.gbBackupLocation.PerformLayout();
			this.panel3.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.picContact).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picVersion).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picTraffic).EndInit();
			base.ResumeLayout(false);
			typeof(global::System.Windows.Forms.Panel).InvokeMember("DoubleBuffered", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.SetProperty, null, this.pnlHome, new object[] { true });
			typeof(global::System.Windows.Forms.Panel).InvokeMember("DoubleBuffered", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.SetProperty, null, this.pnlBackup, new object[] { true });
		}

		// Token: 0x04000AFE RID: 2814
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000AFF RID: 2815
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000B00 RID: 2816
		private global::System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;

		// Token: 0x04000B01 RID: 2817
		private global::System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;

		// Token: 0x04000B02 RID: 2818
		private global::System.Windows.Forms.Button btnHome;

		// Token: 0x04000B03 RID: 2819
		private global::System.Windows.Forms.Button btnHelp;

		// Token: 0x04000B04 RID: 2820
		private global::System.Windows.Forms.Button btnOptions;

		// Token: 0x04000B05 RID: 2821
		private global::System.Windows.Forms.Panel pnlHome;

		// Token: 0x04000B06 RID: 2822
		private global::System.Windows.Forms.ToolStripMenuItem restoreFromBackupToolStripMenuItem;

		// Token: 0x04000B07 RID: 2823
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000B08 RID: 2824
		private global::System.Windows.Forms.ComboBox cbDrives;

		// Token: 0x04000B09 RID: 2825
		private global::CSUST.Data.CustomDataGridView dgServerGames;

		// Token: 0x04000B0A RID: 2826
		private global::System.Windows.Forms.ToolStripMenuItem deleteSaveToolStripMenuItem;

		// Token: 0x04000B0B RID: 2827
		private global::System.Windows.Forms.Button btnGamesInServer;

		// Token: 0x04000B0C RID: 2828
		private global::System.Windows.Forms.CheckBox chkShowAll;

		// Token: 0x04000B0D RID: 2829
		private global::System.Windows.Forms.Panel pnlNoSaves;

		// Token: 0x04000B0E RID: 2830
		private global::System.Windows.Forms.Label lblNoSaves;

		// Token: 0x04000B0F RID: 2831
		private global::System.Windows.Forms.Button btnOpenFolder;

		// Token: 0x04000B10 RID: 2832
		private global::System.Windows.Forms.Label lblBackup;

		// Token: 0x04000B11 RID: 2833
		private global::System.Windows.Forms.Button btnBrowse;

		// Token: 0x04000B12 RID: 2834
		private global::System.Windows.Forms.TextBox txtBackupLocation;

		// Token: 0x04000B13 RID: 2835
		private global::System.Windows.Forms.CheckBox chkBackup;

		// Token: 0x04000B14 RID: 2836
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x04000B15 RID: 2837
		private global::System.Windows.Forms.Label lblRSSSection;

		// Token: 0x04000B16 RID: 2838
		private global::System.Windows.Forms.Button btnRss;

		// Token: 0x04000B17 RID: 2839
		private global::System.Windows.Forms.Label lblDeactivate;

		// Token: 0x04000B18 RID: 2840
		private global::System.Windows.Forms.Button btnDeactivate;

		// Token: 0x04000B19 RID: 2841
		private global::System.Windows.Forms.Panel pnlBackup;

		// Token: 0x04000B1A RID: 2842
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Multi;

		// Token: 0x04000B1B RID: 2843
		private global::System.Windows.Forms.Label lblManageProfiles;

		// Token: 0x04000B1C RID: 2844
		private global::System.Windows.Forms.Button btnManageProfiles;

		// Token: 0x04000B1D RID: 2845
		private global::System.Windows.Forms.ToolStripMenuItem registerPSNIDToolStripMenuItem;

		// Token: 0x04000B1E RID: 2846
		private global::System.Windows.Forms.ToolStripMenuItem resignToolStripMenuItem;

		// Token: 0x04000B1F RID: 2847
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000B20 RID: 2848
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000B21 RID: 2849
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Choose;

		// Token: 0x04000B22 RID: 2850
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x04000B23 RID: 2851
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000B24 RID: 2852
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x04000B25 RID: 2853
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		// Token: 0x04000B26 RID: 2854
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		// Token: 0x04000B27 RID: 2855
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		// Token: 0x04000B28 RID: 2856
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000B29 RID: 2857
		private global::PS3SaveEditor.CustomGroupBox gbBackupLocation;

		// Token: 0x04000B2A RID: 2858
		private global::PS3SaveEditor.CustomGroupBox groupBox1;

		// Token: 0x04000B2B RID: 2859
		private global::PS3SaveEditor.CustomGroupBox groupBox2;

		// Token: 0x04000B2C RID: 2860
		private global::PS3SaveEditor.CustomGroupBox gbManageProfile;

		// Token: 0x04000B2D RID: 2861
		private global::PS3SaveEditor.CustomGroupBox gbProfiles;

		// Token: 0x04000B2E RID: 2862
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x04000B2F RID: 2863
		private global::System.Windows.Forms.PictureBox picTraffic;

		// Token: 0x04000B30 RID: 2864
		private global::System.Windows.Forms.PictureBox pictureBox2;

		// Token: 0x04000B31 RID: 2865
		private global::System.Windows.Forms.PictureBox picVersion;

		// Token: 0x04000B32 RID: 2866
		private global::System.Windows.Forms.ComboBox cbLanguage;

		// Token: 0x04000B33 RID: 2867
		private global::System.Windows.Forms.Label lblLanguage;

		// Token: 0x04000B34 RID: 2868
		private global::System.Windows.Forms.PictureBox picContact;

		// Token: 0x04000B35 RID: 2869
		private global::PS3SaveEditor.CustomGroupBox groupBox3;

		// Token: 0x04000B36 RID: 2870
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000B37 RID: 2871
		private global::System.Windows.Forms.Button btnActivatePackage;

		// Token: 0x04000B38 RID: 2872
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000B39 RID: 2873
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000B3A RID: 2874
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000B3B RID: 2875
		private global::System.Windows.Forms.TextBox txtSerial4;

		// Token: 0x04000B3C RID: 2876
		private global::System.Windows.Forms.TextBox txtSerial3;

		// Token: 0x04000B3D RID: 2877
		private global::System.Windows.Forms.TextBox txtSerial2;

		// Token: 0x04000B3E RID: 2878
		private global::System.Windows.Forms.TextBox txtSerial1;
	}
}
