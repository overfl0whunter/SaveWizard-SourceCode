namespace PS3SaveEditor
{
	// Token: 0x020001CF RID: 463
	public partial class MainForm3 : global::System.Windows.Forms.Form
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x0008943C File Offset: 0x0008763C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00089474 File Offset: 0x00087674
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.MainForm3));
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new global::System.Windows.Forms.DataGridViewCellStyle();
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
			this.tabPageResign = new global::System.Windows.Forms.Panel();
			this.contextMenuStrip2 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.resignToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.resignToolStripMenuItem1.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.resignToolStripMenuItem1.Font);
			this.registerProfileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.registerProfileToolStripMenuItem.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.registerProfileToolStripMenuItem.Font);
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.deleteSaveToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.deleteSaveToolStripMenuItem1.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.deleteSaveToolStripMenuItem1.Font);
			this.tabPageGames = new global::System.Windows.Forms.Panel();
			this.pnlNoSaves = new global::System.Windows.Forms.Panel();
			this.pnlNoSaves2 = new global::System.Windows.Forms.Panel();
			this.lblNoSaves = new global::System.Windows.Forms.Label();
			this.lblNoSaves2 = new global::System.Windows.Forms.Label();
			this.chkShowAll = new global::System.Windows.Forms.CheckBox();
			this.btnResign = new global::System.Windows.Forms.Button();
			this.btnCheats = new global::System.Windows.Forms.Button();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.btnGamesInServer = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cbDrives = new global::System.Windows.Forms.ComboBox();
			this.pnlBackup = new global::System.Windows.Forms.Panel();
			this.Multi = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.picVersion = new global::System.Windows.Forms.PictureBox();
			this.pictureBox2 = new global::System.Windows.Forms.PictureBox();
			this.picTraffic = new global::System.Windows.Forms.PictureBox();
			this.gbManageProfile = new global::PS3SaveEditor.CustomGroupBox();
			this.gbProfiles = new global::PS3SaveEditor.CustomGroupBox();
			this.lblManageProfiles = new global::System.Windows.Forms.Label();
			this.btnManageProfiles = new global::System.Windows.Forms.Button();
			this.groupBox2 = new global::PS3SaveEditor.CustomGroupBox();
			this.cbLanguage = new global::System.Windows.Forms.ComboBox();
			this.lblLanguage = new global::System.Windows.Forms.Label();
			this.cbScale = new global::System.Windows.Forms.ComboBox();
			this.lblScale = new global::System.Windows.Forms.Label();
			this.lblDeactivate = new global::System.Windows.Forms.Label();
			this.btnDeactivate = new global::System.Windows.Forms.Button();
			this.groupBox1 = new global::PS3SaveEditor.CustomGroupBox();
			this.diagnosticBox = new global::PS3SaveEditor.CustomGroupBox();
			this.lblRSSSection = new global::System.Windows.Forms.Label();
			this.btnRss = new global::System.Windows.Forms.Button();
			this.lblDiagnosticSection = new global::System.Windows.Forms.Label();
			this.btnDiagnostic = new global::System.Windows.Forms.Button();
			this.gbBackupLocation = new global::PS3SaveEditor.CustomGroupBox();
			this.btnOpenFolder = new global::System.Windows.Forms.Button();
			this.lblBackup = new global::System.Windows.Forms.Label();
			this.btnBrowse = new global::System.Windows.Forms.Button();
			this.txtBackupLocation = new global::System.Windows.Forms.TextBox();
			this.chkBackup = new global::System.Windows.Forms.CheckBox();
			this.btnApply = new global::System.Windows.Forms.Button();
			this.dgResign = new global::CSUST.Data.CustomDataGridView();
			this._Head = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GameID = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SysVer = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PSNID = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgServerGames = new global::CSUST.Data.CustomDataGridView();
			this.Choose = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn5 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Addded = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1.SuspendLayout();
			this.pnlHome.SuspendLayout();
			this.tabPageResign.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.tabPageGames.SuspendLayout();
			this.pnlNoSaves.SuspendLayout();
			this.pnlNoSaves2.SuspendLayout();
			this.pnlBackup.SuspendLayout();
			this.panel3.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picVersion).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picTraffic).BeginInit();
			this.gbManageProfile.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.diagnosticBox.SuspendLayout();
			this.gbBackupLocation.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dgResign).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.dgServerGames).BeginInit();
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
			this.pnlHome.Controls.Add(this.tabPageResign);
			this.pnlHome.Controls.Add(this.tabPageGames);
			this.pnlHome.Controls.Add(this.chkShowAll);
			this.pnlHome.Controls.Add(this.btnResign);
			this.pnlHome.Controls.Add(this.btnCheats);
			this.pnlHome.Controls.Add(this.btnImport);
			this.pnlHome.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(257), global::PS3SaveEditor.Util.ScaleSize(15));
			this.pnlHome.Name = "pnlHome";
			this.pnlHome.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(511, 347));
			this.pnlHome.TabIndex = 8;
			this.tabPageResign.Controls.Add(this.pnlNoSaves2);
			this.tabPageResign.Controls.Add(this.dgResign);
			this.tabPageResign.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(22));
			this.tabPageResign.Name = "tabPageResign";
			this.tabPageResign.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPageResign.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 325));
			this.tabPageResign.TabIndex = 1;
			this.tabPageResign.Text = "Re-Sign";
			this.contextMenuStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.resignToolStripMenuItem1, this.registerProfileToolStripMenuItem, this.toolStripSeparator3, this.deleteSaveToolStripMenuItem1 });
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(163, 76));
			this.resignToolStripMenuItem1.Name = "resignToolStripMenuItem1";
			this.resignToolStripMenuItem1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(162, 22));
			this.resignToolStripMenuItem1.Text = "Re-sign";
			this.registerProfileToolStripMenuItem.Name = "registerProfileToolStripMenuItem";
			this.registerProfileToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(162, 22));
			this.registerProfileToolStripMenuItem.Text = "Register Profile...";
			this.registerProfileToolStripMenuItem.Click += new global::System.EventHandler(this.registerProfileToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(159, 6));
			this.deleteSaveToolStripMenuItem1.Name = "deleteSaveToolStripMenuItem1";
			this.deleteSaveToolStripMenuItem1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(162, 22));
			this.deleteSaveToolStripMenuItem1.Text = "Delete Save";
			this.deleteSaveToolStripMenuItem1.Click += new global::System.EventHandler(this.deleteSaveToolStripMenuItem1_Click);
			this.tabPageGames.Controls.Add(this.pnlNoSaves);
			this.tabPageGames.Controls.Add(this.dgServerGames);
			this.tabPageGames.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(22));
			this.tabPageGames.Name = "tabPageGames";
			this.tabPageGames.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(507, 325));
			this.tabPageGames.TabIndex = 0;
			this.tabPageGames.Text = "Cheats";
			this.pnlNoSaves.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlNoSaves.Controls.Add(this.lblNoSaves);
			this.pnlNoSaves.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(1), 0);
			this.pnlNoSaves.Name = "pnlNoSaves";
			this.pnlNoSaves.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(506, 325));
			this.pnlNoSaves.TabIndex = 7;
			this.pnlNoSaves.Visible = false;
			this.pnlNoSaves2.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlNoSaves2.Controls.Add(this.lblNoSaves2);
			this.pnlNoSaves2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(1), 0);
			this.pnlNoSaves2.Name = "pnlNoSaves2";
			this.pnlNoSaves2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(506, 325));
			this.pnlNoSaves2.TabIndex = 7;
			this.pnlNoSaves2.Visible = false;
			this.lblNoSaves.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblNoSaves.BackColor = global::System.Drawing.Color.Transparent;
			this.lblNoSaves.ForeColor = global::System.Drawing.Color.White;
			this.lblNoSaves.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(125));
			this.lblNoSaves.Name = "lblNoSaves";
			this.lblNoSaves.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(481, 20));
			this.lblNoSaves.TabIndex = 10;
			this.lblNoSaves.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lblNoSaves2.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblNoSaves2.BackColor = global::System.Drawing.Color.Transparent;
			this.lblNoSaves2.ForeColor = global::System.Drawing.Color.White;
			this.lblNoSaves2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(125));
			this.lblNoSaves2.Name = "lblNoSaves2";
			this.lblNoSaves2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(481, 20));
			this.lblNoSaves2.TabIndex = 10;
			this.lblNoSaves2.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.chkShowAll.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.chkShowAll.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(415), 0);
			this.chkShowAll.Name = "chkShowAll";
			this.chkShowAll.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(97, 16));
			this.chkShowAll.TabIndex = 13;
			this.chkShowAll.Text = "Show All";
			this.chkShowAll.UseVisualStyleBackColor = true;
			this.btnResign.BackColor = global::System.Drawing.Color.FromArgb(230, 230, 230);
			this.btnResign.FlatAppearance.BorderSize = 0;
			this.btnResign.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnResign.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(80), 0);
			this.btnResign.Name = "btnResign";
			this.btnResign.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnResign.TabIndex = 9;
			this.btnResign.Text = "Re-Sign";
			this.btnResign.UseVisualStyleBackColor = false;
			this.btnCheats.BackColor = global::System.Drawing.Color.White;
			this.btnCheats.FlatAppearance.BorderSize = 0;
			this.btnCheats.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnCheats.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(4), 0);
			this.btnCheats.Name = "btnCheats";
			this.btnCheats.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCheats.TabIndex = 8;
			this.btnCheats.Text = "Cheats";
			this.btnCheats.UseVisualStyleBackColor = false;
			this.btnImport.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnImport.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(437), global::PS3SaveEditor.Util.ScaleSize(-1));
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnImport.TabIndex = 16;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
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
			this.cbDrives.Location = (global::PS3SaveEditor.Util.IsUnixOrMacOSX() ? new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(65), global::PS3SaveEditor.Util.ScaleSize(2)) : new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(65), global::PS3SaveEditor.Util.ScaleSize(5)));
			this.cbDrives.Width = global::PS3SaveEditor.Util.ScaleSize(165);
			this.cbDrives.Height = global::PS3SaveEditor.Util.ScaleSize(21);
			this.cbDrives.Name = "cbDrives";
			this.cbDrives.TabIndex = 3;
			this.panel1.Controls.Add(this.cbDrives);
			this.pnlBackup.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.pnlBackup.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.pnlBackup.Controls.Add(this.gbManageProfile);
			this.pnlBackup.Controls.Add(this.groupBox2);
			this.pnlBackup.Controls.Add(this.groupBox1);
			this.pnlBackup.Controls.Add(this.diagnosticBox);
			this.pnlBackup.Controls.Add(this.gbBackupLocation);
			this.pnlBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(257), global::PS3SaveEditor.Util.ScaleSize(15));
			this.pnlBackup.Name = "pnlBackup";
			this.pnlBackup.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 347));
			this.pnlBackup.TabIndex = 9;
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
			this.panel2.TabIndex = 12;
			this.panel3.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left;
			this.panel3.BackColor = global::System.Drawing.Color.FromArgb(0, 56, 115);
			this.panel3.BackgroundImage = (global::System.Drawing.Image)componentResourceManager.GetObject("panel3.BackgroundImage");
			this.panel3.Controls.Add(this.picVersion);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Controls.Add(this.picTraffic);
			this.panel3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(207));
			this.panel3.Name = "panel3";
			this.panel3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 122));
			this.panel3.TabIndex = 13;
			this.picVersion.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.picVersion.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(23));
			this.picVersion.Name = "picVersion";
			this.picVersion.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 26));
			this.picVersion.TabIndex = 13;
			this.picVersion.TabStop = false;
			this.pictureBox2.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox2.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox2.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 122));
			this.pictureBox2.TabIndex = 12;
			this.pictureBox2.TabStop = false;
			this.picTraffic.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left;
			this.picTraffic.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.picTraffic.Location = new global::System.Drawing.Point(0, 0);
			this.picTraffic.Name = "picTraffic";
			this.picTraffic.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 26));
			this.picTraffic.TabIndex = 11;
			this.picTraffic.TabStop = false;
			this.gbManageProfile.Controls.Add(this.gbProfiles);
			this.gbManageProfile.Controls.Add(this.lblManageProfiles);
			this.gbManageProfile.Controls.Add(this.btnManageProfiles);
			this.gbManageProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(270));
			this.gbManageProfile.Name = "gbManageProfile";
			this.gbManageProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 65));
			this.gbManageProfile.TabIndex = 10;
			this.gbManageProfile.TabStop = false;
			bool flag = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag)
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
			bool flag2 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag2)
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
			this.groupBox2.Controls.Add(this.cbScale);
			this.groupBox2.Controls.Add(this.lblScale);
			this.groupBox2.Controls.Add(this.lblDeactivate);
			this.groupBox2.Controls.Add(this.btnDeactivate);
			this.groupBox2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(200));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(483, 65));
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.cbLanguage.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLanguage.FormattingEnabled = true;
			bool flag3 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag3)
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
			bool flag4 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag4)
			{
				this.lblLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(335), global::PS3SaveEditor.Util.ScaleSize(12));
			}
			else
			{
				this.lblLanguage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(332), global::PS3SaveEditor.Util.ScaleSize(16));
			}
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(55, 13));
			this.lblLanguage.TabIndex = 9;
			this.lblLanguage.Text = "Language";
			this.cbScale.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScale.FormattingEnabled = true;
			bool flag5 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag5)
			{
				this.cbScale.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(195), global::PS3SaveEditor.Util.ScaleSize(32));
			}
			else
			{
				this.cbScale.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(195), global::PS3SaveEditor.Util.ScaleSize(36));
			}
			this.cbScale.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(122, 21));
			this.cbScale.Name = "cbScale";
			this.cbScale.TabIndex = 9;
			this.cbScale.Visible = true;
			this.lblScale.AutoSize = true;
			this.lblScale.BackColor = global::System.Drawing.Color.Transparent;
			this.lblScale.ForeColor = global::System.Drawing.Color.White;
			bool flag6 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag6)
			{
				this.lblScale.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(195), global::PS3SaveEditor.Util.ScaleSize(12));
			}
			else
			{
				this.lblScale.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(195), global::PS3SaveEditor.Util.ScaleSize(16));
			}
			this.lblScale.Name = "lblScale";
			this.lblScale.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(55, 13));
			this.lblScale.TabIndex = 9;
			this.lblScale.Text = "Scale";
			this.lblScale.Visible = true;
			this.lblDeactivate.AutoSize = true;
			this.lblDeactivate.ForeColor = global::System.Drawing.Color.White;
			bool flag7 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag7)
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
			this.btnDeactivate.ForeColor = global::System.Drawing.Color.White;
			bool flag8 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag8)
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
			this.groupBox1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(240, 67));
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.lblRSSSection.AutoSize = true;
			this.lblRSSSection.ForeColor = global::System.Drawing.Color.White;
			bool flag9 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag9)
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
			bool flag10 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag10)
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
			this.diagnosticBox.Controls.Add(this.lblDiagnosticSection);
			this.diagnosticBox.Controls.Add(this.btnDiagnostic);
			this.diagnosticBox.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(255), global::PS3SaveEditor.Util.ScaleSize(128));
			this.diagnosticBox.Name = "diagnosticBox";
			this.diagnosticBox.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(240, 67));
			this.diagnosticBox.TabIndex = 9;
			this.diagnosticBox.TabStop = false;
			this.lblDiagnosticSection.AutoSize = true;
			this.lblDiagnosticSection.ForeColor = global::System.Drawing.Color.White;
			bool flag11 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag11)
			{
				this.lblDiagnosticSection.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			}
			else
			{
				this.lblDiagnosticSection.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.lblDiagnosticSection.Name = "lblDiagnosticSection";
			this.lblDiagnosticSection.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(295, 13));
			this.lblDiagnosticSection.TabIndex = 8;
			this.lblDiagnosticSection.Text = "Diagnostic Section";
			this.btnDiagnostic.ForeColor = global::System.Drawing.Color.White;
			bool flag12 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag12)
			{
				this.btnDiagnostic.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(33));
			}
			else
			{
				this.btnDiagnostic.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(37));
			}
			this.btnDiagnostic.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(115, 23));
			this.btnDiagnostic.Name = "btnDiagnostic";
			this.btnDiagnostic.TabIndex = 0;
			this.btnDiagnostic.Text = "Diagnostic";
			this.btnDiagnostic.UseVisualStyleBackColor = false;
			this.btnDiagnostic.Click += new global::System.EventHandler(this.btnDiagnostic_Click);
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
			bool flag13 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag13)
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
			bool flag14 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag14)
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
			bool flag15 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag15)
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
			bool flag16 = global::PS3SaveEditor.Util.IsUnixOrMacOSX();
			if (flag16)
			{
				this.chkBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			}
			else
			{
				this.chkBackup.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(15));
			}
			this.chkBackup.Name = "chkBackup";
			this.chkBackup.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(96, 17));
			this.chkBackup.AutoSize = true;
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
			this.dgResign.AllowUserToAddRows = false;
			this.dgResign.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8.25f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgResign.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgResign.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgResign.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this._Head, this.GameID, this.SysVer, this.PSNID });
			this.dgResign.ContextMenuStrip = this.contextMenuStrip2;
			this.dgResign.Location = new global::System.Drawing.Point(0, 0);
			this.dgResign.Name = "dgResign";
			this.dgResign.ReadOnly = true;
			this.dgResign.RowHeadersVisible = false;
			this.dgResign.RowTemplate.Height = global::PS3SaveEditor.Util.ScaleSize(24);
			this.dgResign.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgResign.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 325));
			this.dgResign.TabIndex = 0;
			this.dgResign.Click += new global::System.EventHandler(this.dgResign_Click);
			this._Head.FillWeight = global::PS3SaveEditor.Util.ScaleSize(20f);
			this._Head.HeaderText = "";
			this._Head.Name = "_Head";
			this._Head.ReadOnly = true;
			this._Head.Width = global::PS3SaveEditor.Util.ScaleSize(20);
			this.GameID.FillWeight = global::PS3SaveEditor.Util.ScaleSize(200f);
			this.GameID.HeaderText = "Game";
			this.GameID.Name = "GameID";
			this.GameID.ReadOnly = true;
			this.GameID.Width = global::PS3SaveEditor.Util.ScaleSize(200);
			this.SysVer.FillWeight = global::PS3SaveEditor.Util.ScaleSize(50f);
			this.SysVer.HeaderText = "Sys. Ver.";
			this.SysVer.Name = "SysVer";
			this.SysVer.ReadOnly = true;
			this.PSNID.FillWeight = global::PS3SaveEditor.Util.ScaleSize(50f);
			this.PSNID.HeaderText = "Profile/PSN ID";
			this.PSNID.Name = "PSNID";
			this.PSNID.ReadOnly = true;
			this.PSNID.Width = global::PS3SaveEditor.Util.ScaleSize(250);
			this.dgServerGames.AllowUserToAddRows = false;
			this.dgServerGames.AllowUserToDeleteRows = false;
			this.dgServerGames.AllowUserToResizeRows = false;
			this.dgServerGames.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.dgServerGames.ClipboardCopyMode = global::System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgServerGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgServerGames.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgServerGames.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.Choose, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewCheckBoxColumn1, this.dataGridViewTextBoxColumn5, this.Addded, this.dataGridViewTextBoxColumn6 });
			this.dgServerGames.ContextMenuStrip = this.contextMenuStrip1;
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgServerGames.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgResign.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgServerGames.Location = new global::System.Drawing.Point(0, 0);
			this.dgServerGames.Name = "dgServerGames";
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle4.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dgServerGames.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgResign.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgServerGames.AutoSizeColumnsMode = global::System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
			this.dgServerGames.RowHeadersVisible = false;
			this.dgServerGames.RowHeadersWidth = global::PS3SaveEditor.Util.ScaleSize(25);
			this.dgServerGames.RowTemplate.Height = global::PS3SaveEditor.Util.ScaleSize(24);
			this.dgServerGames.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgServerGames.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(508, 325));
			this.dgServerGames.TabIndex = 12;
			this.dgServerGames.Click += new global::System.EventHandler(this.dgServerGames_Click);
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
			dataGridViewCellStyle5.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewTextBoxColumn2.Frozen = true;
			this.dataGridViewTextBoxColumn2.HeaderText = "Cheats";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = global::PS3SaveEditor.Util.ScaleSize(60);
			dataGridViewCellStyle6.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
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
			this.dataGridViewTextBoxColumn6.HeaderText = "Cheats changing time";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Visible = false;
			this.dataGridViewTextBoxColumn6.Width = global::PS3SaveEditor.Util.ScaleSize(80);
			this.Addded.HeaderText = "Added";
			this.Addded.Name = "Addded";
			this.Addded.ReadOnly = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.FromArgb(0, 44, 101);
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(780, 377));
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnOptions);
			base.Controls.Add(this.btnHome);
			base.Controls.Add(this.btnHelp);
			base.Controls.Add(this.pnlHome);
			base.Controls.Add(this.pnlBackup);
			this.DoubleBuffered = true;
			base.Name = "MainForm3";
			this.Text = "PS4 Save Editor";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.contextMenuStrip1.ResumeLayout(false);
			this.pnlHome.ResumeLayout(false);
			this.tabPageResign.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.tabPageGames.ResumeLayout(false);
			this.pnlNoSaves.ResumeLayout(false);
			this.pnlNoSaves2.ResumeLayout(false);
			this.pnlBackup.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.picVersion).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picTraffic).EndInit();
			this.gbManageProfile.ResumeLayout(false);
			this.gbManageProfile.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.diagnosticBox.ResumeLayout(false);
			this.diagnosticBox.PerformLayout();
			this.gbBackupLocation.ResumeLayout(false);
			this.gbBackupLocation.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dgResign).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.dgServerGames).EndInit();
			base.ResumeLayout(false);
			typeof(global::System.Windows.Forms.Panel).InvokeMember("DoubleBuffered", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.SetProperty, null, this.pnlHome, new object[] { true });
			typeof(global::System.Windows.Forms.Panel).InvokeMember("DoubleBuffered", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.SetProperty, null, this.pnlBackup, new object[] { true });
		}

		// Token: 0x04000B70 RID: 2928
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000B71 RID: 2929
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000B72 RID: 2930
		private global::System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;

		// Token: 0x04000B73 RID: 2931
		private global::System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;

		// Token: 0x04000B74 RID: 2932
		private global::System.Windows.Forms.Button btnHome;

		// Token: 0x04000B75 RID: 2933
		private global::System.Windows.Forms.Button btnHelp;

		// Token: 0x04000B76 RID: 2934
		private global::System.Windows.Forms.Button btnOptions;

		// Token: 0x04000B77 RID: 2935
		private global::System.Windows.Forms.Panel pnlHome;

		// Token: 0x04000B78 RID: 2936
		private global::System.Windows.Forms.ToolStripMenuItem restoreFromBackupToolStripMenuItem;

		// Token: 0x04000B79 RID: 2937
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000B7A RID: 2938
		private global::System.Windows.Forms.ComboBox cbDrives;

		// Token: 0x04000B7B RID: 2939
		private global::System.Windows.Forms.ToolStripMenuItem deleteSaveToolStripMenuItem;

		// Token: 0x04000B7C RID: 2940
		private global::System.Windows.Forms.Button btnGamesInServer;

		// Token: 0x04000B7D RID: 2941
		private global::System.Windows.Forms.Panel pnlNoSaves;

		// Token: 0x04000B7E RID: 2942
		private global::System.Windows.Forms.Panel pnlNoSaves2;

		// Token: 0x04000B7F RID: 2943
		private global::System.Windows.Forms.Label lblNoSaves;

		// Token: 0x04000B80 RID: 2944
		private global::System.Windows.Forms.Label lblNoSaves2;

		// Token: 0x04000B81 RID: 2945
		private global::System.Windows.Forms.Button btnOpenFolder;

		// Token: 0x04000B82 RID: 2946
		private global::System.Windows.Forms.Label lblBackup;

		// Token: 0x04000B83 RID: 2947
		private global::System.Windows.Forms.Button btnBrowse;

		// Token: 0x04000B84 RID: 2948
		private global::System.Windows.Forms.TextBox txtBackupLocation;

		// Token: 0x04000B85 RID: 2949
		private global::System.Windows.Forms.CheckBox chkBackup;

		// Token: 0x04000B86 RID: 2950
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x04000B87 RID: 2951
		private global::System.Windows.Forms.Label lblRSSSection;

		// Token: 0x04000B88 RID: 2952
		private global::System.Windows.Forms.Button btnRss;

		// Token: 0x04000B89 RID: 2953
		private global::System.Windows.Forms.Label lblDiagnosticSection;

		// Token: 0x04000B8A RID: 2954
		private global::System.Windows.Forms.Button btnDiagnostic;

		// Token: 0x04000B8B RID: 2955
		private global::System.Windows.Forms.Label lblDeactivate;

		// Token: 0x04000B8C RID: 2956
		private global::System.Windows.Forms.Button btnDeactivate;

		// Token: 0x04000B8D RID: 2957
		private global::System.Windows.Forms.Panel pnlBackup;

		// Token: 0x04000B8E RID: 2958
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Multi;

		// Token: 0x04000B8F RID: 2959
		private global::System.Windows.Forms.Label lblManageProfiles;

		// Token: 0x04000B90 RID: 2960
		private global::System.Windows.Forms.Button btnManageProfiles;

		// Token: 0x04000B91 RID: 2961
		private global::System.Windows.Forms.ToolStripMenuItem registerPSNIDToolStripMenuItem;

		// Token: 0x04000B92 RID: 2962
		private global::System.Windows.Forms.ToolStripMenuItem resignToolStripMenuItem;

		// Token: 0x04000B93 RID: 2963
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000B94 RID: 2964
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000B95 RID: 2965
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000B96 RID: 2966
		private global::PS3SaveEditor.CustomGroupBox gbBackupLocation;

		// Token: 0x04000B97 RID: 2967
		private global::PS3SaveEditor.CustomGroupBox groupBox1;

		// Token: 0x04000B98 RID: 2968
		private global::PS3SaveEditor.CustomGroupBox groupBox2;

		// Token: 0x04000B99 RID: 2969
		private global::PS3SaveEditor.CustomGroupBox gbManageProfile;

		// Token: 0x04000B9A RID: 2970
		private global::PS3SaveEditor.CustomGroupBox gbProfiles;

		// Token: 0x04000B9B RID: 2971
		private global::PS3SaveEditor.CustomGroupBox diagnosticBox;

		// Token: 0x04000B9C RID: 2972
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x04000B9D RID: 2973
		private global::System.Windows.Forms.PictureBox picTraffic;

		// Token: 0x04000B9E RID: 2974
		private global::System.Windows.Forms.PictureBox pictureBox2;

		// Token: 0x04000B9F RID: 2975
		private global::System.Windows.Forms.PictureBox picVersion;

		// Token: 0x04000BA0 RID: 2976
		private global::System.Windows.Forms.ComboBox cbLanguage;

		// Token: 0x04000BA1 RID: 2977
		private global::System.Windows.Forms.Label lblLanguage;

		// Token: 0x04000BA2 RID: 2978
		private global::System.Windows.Forms.ComboBox cbScale;

		// Token: 0x04000BA3 RID: 2979
		private global::System.Windows.Forms.Label lblScale;

		// Token: 0x04000BA4 RID: 2980
		private global::System.Windows.Forms.Panel tabPageGames;

		// Token: 0x04000BA5 RID: 2981
		private global::System.Windows.Forms.CheckBox chkShowAll;

		// Token: 0x04000BA6 RID: 2982
		private global::CSUST.Data.CustomDataGridView dgServerGames;

		// Token: 0x04000BA7 RID: 2983
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Choose;

		// Token: 0x04000BA8 RID: 2984
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x04000BA9 RID: 2985
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000BAA RID: 2986
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x04000BAB RID: 2987
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		// Token: 0x04000BAC RID: 2988
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		// Token: 0x04000BAD RID: 2989
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		// Token: 0x04000BAE RID: 2990
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		// Token: 0x04000BAF RID: 2991
		private global::System.Windows.Forms.Panel tabPageResign;

		// Token: 0x04000BB0 RID: 2992
		private global::CSUST.Data.CustomDataGridView dgResign;

		// Token: 0x04000BB1 RID: 2993
		private global::System.Windows.Forms.Button btnResign;

		// Token: 0x04000BB2 RID: 2994
		private global::System.Windows.Forms.Button btnCheats;

		// Token: 0x04000BB3 RID: 2995
		private global::System.Windows.Forms.Button btnImport;

		// Token: 0x04000BB4 RID: 2996
		private global::System.Windows.Forms.DataGridViewTextBoxColumn _Head;

		// Token: 0x04000BB5 RID: 2997
		private global::System.Windows.Forms.DataGridViewTextBoxColumn GameID;

		// Token: 0x04000BB6 RID: 2998
		private global::System.Windows.Forms.DataGridViewTextBoxColumn PSNID;

		// Token: 0x04000BB7 RID: 2999
		private global::System.Windows.Forms.DataGridViewTextBoxColumn SysVer;

		// Token: 0x04000BB8 RID: 3000
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Addded;

		// Token: 0x04000BB9 RID: 3001
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

		// Token: 0x04000BBA RID: 3002
		private global::System.Windows.Forms.ToolStripMenuItem resignToolStripMenuItem1;

		// Token: 0x04000BBB RID: 3003
		private global::System.Windows.Forms.ToolStripMenuItem registerProfileToolStripMenuItem;

		// Token: 0x04000BBC RID: 3004
		private global::System.Windows.Forms.ToolStripMenuItem deleteSaveToolStripMenuItem1;

		// Token: 0x04000BBD RID: 3005
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	}
}
