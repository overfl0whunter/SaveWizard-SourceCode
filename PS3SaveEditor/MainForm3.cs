using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CSUST.Data;
using Ionic.Zip;
using Microsoft.Win32;
using PS3SaveEditor.Diagnostic;
using PS3SaveEditor.Resources;
using PS3SaveEditor.SubControls;
using PS3SaveEditor.Utilities;
using Rss;

namespace PS3SaveEditor
{
	// Token: 0x020001CF RID: 463
	public partial class MainForm3 : Form
	{
		// Token: 0x060017B4 RID: 6068 RVA: 0x0007EA68 File Offset: 0x0007CC68
		public MainForm3()
		{
			this.InitializeComponent();
			this.m_games = new List<game>();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.RegionMap = new Dictionary<int, string>();
			this.chkShowAll.CheckedChanged += this.chkShowAll_CheckedChanged;
			this.chkShowAll.EnabledChanged += this.chkShowAll_EnabledChanged;
			this.picTraffic.Visible = false;
			base.ResizeBegin += delegate(object s, EventArgs e)
			{
				base.SuspendLayout();
			};
			base.SizeChanged += delegate(object s, EventArgs e)
			{
				base.ResumeLayout(true);
				this.chkShowAll_CheckedChanged(null, null);
				base.Invalidate(true);
				bool visible = this.pnlHome.Visible;
				if (visible)
				{
					bool flag8 = base.WindowState == FormWindowState.Maximized || this.previousState == FormWindowState.Maximized;
					if (flag8)
					{
						this.ResizeColumns(this.chkShowAll.Checked);
					}
				}
				this.previousState = base.WindowState;
			};
			base.ResizeEnd += delegate(object s, EventArgs e)
			{
				bool visible2 = this.pnlHome.Visible;
				if (visible2)
				{
					this.ResizeColumns(this.chkShowAll.Checked);
				}
				base.ResumeLayout(true);
			};
			this.txtBackupLocation.ReadOnly = true;
			this.dgServerGames.Columns[0].ReadOnly = true;
			this.MinimumSize = base.Size;
			this.dgServerGames.CellClick += this.dgServerGames_CellClick;
			this.dgServerGames.SelectionChanged += this.dgServerGames_SelectionChanged;
			this.dgServerGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.btnGamesInServer.Visible = false;
			this.btnRss.BackColor = SystemColors.ButtonFace;
			this.btnOpenFolder.BackColor = SystemColors.ButtonFace;
			this.btnBrowse.BackColor = SystemColors.ButtonFace;
			this.btnDeactivate.BackColor = SystemColors.ButtonFace;
			this.btnManageProfiles.BackColor = SystemColors.ButtonFace;
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnDiagnostic.BackColor = SystemColors.ButtonFace;
			this.btnRss.ForeColor = Color.Black;
			this.btnOpenFolder.ForeColor = Color.Black;
			this.btnBrowse.ForeColor = Color.Black;
			this.btnDeactivate.ForeColor = Color.Black;
			this.btnManageProfiles.ForeColor = Color.Black;
			this.btnApply.ForeColor = Color.Black;
			this.btnApply.ForeColor = Color.Black;
			this.btnDiagnostic.ForeColor = Color.Black;
			this.tabPageGames.BackColor = (this.tabPageGames.BackColor = (this.tabPageResign.BackColor = (this.pnlBackup.BackColor = (this.pnlHome.BackColor = (this.pnlHome.BackColor = (this.pnlNoSaves.BackColor = (this.pnlNoSaves2.BackColor = Color.FromArgb(127, 204, 204, 204))))))));
			this.gbBackupLocation.BackColor = (this.gbManageProfile.BackColor = (this.groupBox1.BackColor = (this.groupBox2.BackColor = (this.diagnosticBox.BackColor = Color.Transparent))));
			this.chkShowAll.BackColor = Color.FromArgb(0, 204, 204, 204);
			this.chkShowAll.ForeColor = Color.White;
			this.panel2.Visible = false;
			this.registerPSNIDToolStripMenuItem.Visible = false;
			this.resignToolStripMenuItem.Visible = false;
			this.toolStripSeparator1.Visible = false;
			base.CenterToScreen();
			this.SetLabels();
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				bool flag2 = base.WindowState == FormWindowState.Minimized;
				if (flag2)
				{
					base.WindowState = FormWindowState.Normal;
				}
				base.Activate();
			}
			else
			{
				Util.SetForegroundWindow(base.Handle);
			}
			this.cbDrives.SelectedIndexChanged += this.cbDrives_SelectedIndexChanged;
			this.cbScale.SelectedIndexChanged += this.cbScale_SelectedIndexChanged;
			this.dgServerGames.CellMouseDown += this.dgServerGames_CellMouseDown;
			this.dgServerGames.CellDoubleClick += this.dgServerGames_CellDoubleClick;
			this.dgServerGames.ColumnHeaderMouseClick += this.dgServerGames_ColumnHeaderMouseClick;
			this.dgServerGames.ShowCellToolTips = true;
			this.panel2.BackgroundImage = null;
			List<CultureInfo> list = new List<CultureInfo>();
			string registryValue = Util.GetRegistryValue("Language");
			bool flag3 = Util.IsUnixOrMacOSX();
			if (flag3)
			{
				this.cbLanguage.DisplayMember = "DisplayName";
			}
			else
			{
				this.cbLanguage.DisplayMember = "NativeName";
			}
			this.cbLanguage.ValueMember = "Name";
			this.cbLanguage.SelectedValueChanged += this.cbLanguage_SelectedIndexChanged;
			string text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "languages");
			bool flag4 = Directory.Exists(text);
			if (flag4)
			{
				string[] directories = Directory.GetDirectories(text);
				foreach (string text2 in directories)
				{
					try
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
						CultureInfo cultureInfo = new CultureInfo(fileNameWithoutExtension);
						list.Add(cultureInfo);
					}
					catch
					{
					}
				}
			}
			bool flag5 = list.Count == 0;
			if (flag5)
			{
				list.Add(new CultureInfo("en"));
			}
			this.cbLanguage.DataSource = list;
			bool flag6 = registryValue == null;
			if (flag6)
			{
				int num = this.cbLanguage.FindStringExact("English");
				this.cbLanguage.SelectedIndex = ((num < 0) ? 0 : num);
			}
			else
			{
				this.cbLanguage.SelectedValue = registryValue;
			}
			this.dgResign.CellDoubleClick += this.dgResign_CellDoubleClick;
			this.cbDrives.DrawMode = DrawMode.OwnerDrawFixed;
			this.cbDrives.DrawItem += this.cbDrives_DrawItem;
			this.drivesHelper = new DrivesHelper(this.cbDrives, this.m_games, this.chkShowAll, this.pnlNoSaves, this.btnResign, this.btnImport);
			this.drivesHelper.FillDrives();
			this.cbScale.Items.Add("75%");
			this.cbScale.Items.Add("100%");
			this.cbScale.Items.Add("125%");
			this.cbScale.Items.Add("150%");
			this.cbScale.Items.Add("175%");
			this.cbScale.Items.Add("200%");
			this.cbScale.SelectedIndex = Util.ScaleIndex;
			this.panel1.AutoScroll = true;
			base.Load += this.MainForm_Load;
			this.btnHome.ChangeUICues += this.btnHome_ChangeUICues;
			this.dgServerGames.BackgroundColor = Color.White;
			this.dgResign.BackgroundColor = Color.White;
			this.dgServerGames.ScrollBars = ScrollBars.Both;
			this.dgResign.ScrollBars = ScrollBars.Both;
			this.dgResign.SortCompare += this.dgResign_SortCompare;
			this.dgResign.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dgResign.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
			this.btnCheats.Click += this.btnCheats_Click;
			this.btnResign.Click += this.btnResign_Click;
			this.btnImport.Visible = (this.tabPageResign.Visible = false);
			this.tabPageGames.BackColor = (this.tabPageResign.BackColor = (this.pnlHome.BackColor = Color.Transparent));
			this.tabPageGames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tabPageResign.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.dgResign.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.btnImport.Click += this.btnImport_Click;
			this.dgServerGames.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
			this.dgServerGames.Columns[7].Visible = false;
			this.resignToolStripMenuItem1.Click += this.resignToolStripMenuItem1_Click;
			this.contextMenuStrip2.Opening += this.contextMenuStrip2_Opening;
			this.dgServerGames.ColumnWidthChanged += this.dgServerGames_ColumnWidthChanged;
			bool flag7 = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag7)
			{
				base.Visible = false;
				this.MainForm_Load(null, null);
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0007F3AC File Offset: 0x0007D5AC
		private void cbScale_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = ((ComboBox)sender).SelectedIndex;
			bool flag = selectedIndex == Util.ScaleIndex;
			if (!flag)
			{
				Util.ScaleIndex = selectedIndex;
				Util.SetRegistryValue("SelectedScaleIndex", selectedIndex.ToString());
				int num = base.Size.Width - base.ClientSize.Width;
				int num2 = base.Size.Height - base.ClientSize.Height;
				this.MinimumSize = new Size(Util.ScaleSize(780) + num, Util.ScaleSize(377) + num2);
				base.Size = this.MinimumSize;
				this.simpleToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.advancedToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.resignToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.registerPSNIDToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.restoreFromBackupToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.deleteSaveToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.resignToolStripMenuItem1.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.registerProfileToolStripMenuItem.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.deleteSaveToolStripMenuItem1.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnHome.Location = new Point(Util.ScaleSize(15), Util.ScaleSize(15));
				this.btnHome.Size = Util.ScaleSize(new Size(237, 61));
				this.btnHelp.Location = new Point(Util.ScaleSize(15), Util.ScaleSize(143));
				this.btnHelp.Size = Util.ScaleSize(new Size(237, 61));
				this.btnOptions.Location = new Point(Util.ScaleSize(15), Util.ScaleSize(79));
				this.btnOptions.Size = Util.ScaleSize(new Size(237, 61));
				this.panel3.Location = new Point(Util.ScaleSize(15), Util.ScaleSize(207));
				this.panel3.Size = Util.ScaleSize(new Size(237, 122));
				this.picVersion.Location = new Point(0, Util.ScaleSize(23));
				this.picVersion.Size = Util.ScaleSize(new Size(237, 26));
				this.pictureBox2.Location = new Point(0, 0);
				this.pictureBox2.Size = Util.ScaleSize(new Size(237, 122));
				this.picTraffic.Location = new Point(0, 0);
				this.picTraffic.Size = Util.ScaleSize(new Size(237, 26));
				this.panel1.Location = new Point(Util.ScaleSize(15), Util.ScaleSize(332));
				this.panel1.Size = Util.ScaleSize(new Size(237, 30));
				this.cbDrives.Location = (Util.IsUnixOrMacOSX() ? new Point(Util.ScaleSize(65), Util.ScaleSize(2)) : new Point(Util.ScaleSize(65), Util.ScaleSize(5)));
				this.cbDrives.Width = Util.ScaleSize(165);
				this.cbDrives.Height = Util.ScaleSize(21);
				this.cbDrives.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.pnlHome.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
				this.pnlHome.Size = Util.ScaleSize(new Size(511, 347));
				this.pnlBackup.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
				this.pnlBackup.Size = Util.ScaleSize(new Size(508, 347));
				this.btnCheats.Location = new Point(Util.ScaleSize(4), 0);
				this.btnCheats.Size = Util.ScaleSize(new Size(75, 23));
				this.btnResign.Location = new Point(Util.ScaleSize(80), 0);
				this.btnResign.Size = Util.ScaleSize(new Size(75, 23));
				this.btnImport.Location = new Point(Util.ScaleSize(437), Util.ScaleSize(-1));
				this.btnImport.Size = Util.ScaleSize(new Size(75, 23));
				this.chkShowAll.Location = new Point(Util.ScaleSize(415), 0);
				this.chkShowAll.Size = Util.ScaleSize(new Size(97, 16));
				this.tabPageGames.Location = new Point(Util.ScaleSize(4), Util.ScaleSize(22));
				this.tabPageGames.Size = Util.ScaleSize(new Size(507, 325));
				this.tabPageResign.Location = new Point(Util.ScaleSize(4), Util.ScaleSize(22));
				this.tabPageResign.Padding = new Padding(3);
				this.tabPageResign.Size = Util.ScaleSize(new Size(508, 325));
				this.lblNoSaves.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(125));
				this.lblNoSaves.Size = Util.ScaleSize(new Size(481, 20));
				this.lblNoSaves2.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(125));
				this.lblNoSaves2.Size = Util.ScaleSize(new Size(481, 20));
				this.pnlNoSaves.Location = new Point(Util.ScaleSize(1), 0);
				this.pnlNoSaves.Size = Util.ScaleSize(new Size(506, 325));
				this.pnlNoSaves2.Location = new Point(Util.ScaleSize(1), 0);
				this.pnlNoSaves2.Size = Util.ScaleSize(new Size(506, 325));
				this.tabPageGames.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.tabPageResign.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnCheats.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnResign.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnImport.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.chkShowAll.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblNoSaves.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblNoSaves2.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblNoSaves.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblNoSaves2.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.gbBackupLocation.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(8));
				this.gbBackupLocation.Size = Util.ScaleSize(new Size(483, 115));
				this.groupBox1.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(128));
				this.groupBox1.Size = Util.ScaleSize(new Size(240, 67));
				this.diagnosticBox.Location = new Point(Util.ScaleSize(255), Util.ScaleSize(128));
				this.diagnosticBox.Size = Util.ScaleSize(new Size(240, 67));
				this.groupBox2.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(200));
				this.groupBox2.Size = Util.ScaleSize(new Size(483, 65));
				this.gbManageProfile.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(270));
				this.gbManageProfile.Size = Util.ScaleSize(new Size(483, 65));
				this.btnOpenFolder.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(85));
				this.btnOpenFolder.Size = Util.ScaleSize(new Size(123, 23));
				bool flag2 = Util.IsUnixOrMacOSX();
				if (flag2)
				{
					this.chkBackup.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(10));
					this.lblBackup.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(34));
					this.txtBackupLocation.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(54));
					this.txtBackupLocation.Size = Util.ScaleSize(new Size(264, 15));
					this.btnBrowse.Location = new Point(Util.ScaleSize(281), Util.ScaleSize(54));
					this.lblRSSSection.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(10));
					this.btnRss.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(33));
					this.lblDiagnosticSection.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(10));
					this.btnDiagnostic.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(33));
					this.lblDeactivate.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(10));
					this.btnDeactivate.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(30));
					this.lblScale.Location = new Point(Util.ScaleSize(195), Util.ScaleSize(12));
					this.cbScale.Location = new Point(Util.ScaleSize(195), Util.ScaleSize(32));
					this.lblLanguage.Location = new Point(Util.ScaleSize(335), Util.ScaleSize(12));
					this.cbLanguage.Location = new Point(Util.ScaleSize(335), Util.ScaleSize(32));
					this.lblManageProfiles.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(12));
					this.gbProfiles.Location = new Point(Util.ScaleSize(134), Util.ScaleSize(29));
					this.gbProfiles.Size = Util.ScaleSize(new Size(80, 29));
				}
				else
				{
					this.chkBackup.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(15));
					this.lblBackup.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(40));
					this.txtBackupLocation.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(61));
					this.txtBackupLocation.Size = Util.ScaleSize(new Size(264, 23));
					this.btnBrowse.Location = new Point(Util.ScaleSize(281), Util.ScaleSize(60));
					this.lblRSSSection.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(15));
					this.btnRss.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(37));
					this.lblDiagnosticSection.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(15));
					this.btnDiagnostic.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(37));
					this.lblDeactivate.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(15));
					this.btnDeactivate.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(35));
					this.lblScale.Location = new Point(Util.ScaleSize(195), Util.ScaleSize(16));
					this.cbScale.Location = new Point(Util.ScaleSize(195), Util.ScaleSize(36));
					this.lblLanguage.Location = new Point(Util.ScaleSize(332), Util.ScaleSize(16));
					this.cbLanguage.Location = new Point(Util.ScaleSize(335), Util.ScaleSize(36));
					this.lblManageProfiles.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(15));
					this.gbProfiles.Location = new Point(Util.ScaleSize(134), Util.ScaleSize(30));
					this.gbProfiles.Size = Util.ScaleSize(new Size(80, 27));
				}
				this.chkBackup.Size = Util.ScaleSize(new Size(96, 17));
				this.lblBackup.Size = Util.ScaleSize(new Size(0, 13));
				this.btnBrowse.Size = Util.ScaleSize(new Size(75, 23));
				this.lblRSSSection.Size = Util.ScaleSize(new Size(295, 13));
				this.btnRss.Size = Util.ScaleSize(new Size(115, 23));
				this.lblDiagnosticSection.Size = Util.ScaleSize(new Size(295, 13));
				this.btnDiagnostic.Size = Util.ScaleSize(new Size(115, 23));
				this.lblDeactivate.Size = Util.ScaleSize(new Size(42, 13));
				this.btnDeactivate.Size = Util.ScaleSize(new Size(115, 23));
				this.lblScale.Size = Util.ScaleSize(new Size(55, 13));
				this.cbScale.Size = Util.ScaleSize(new Size(122, 21));
				this.lblLanguage.Size = Util.ScaleSize(new Size(55, 13));
				this.cbLanguage.Size = Util.ScaleSize(new Size(142, 21));
				this.lblManageProfiles.Size = Util.ScaleSize(new Size(106, 13));
				this.btnManageProfiles.Location = new Point(Util.ScaleSize(10), Util.ScaleSize(33));
				this.btnManageProfiles.Size = Util.ScaleSize(new Size(115, 23));
				this.chkBackup.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblBackup.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.txtBackupLocation.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnBrowse.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnOpenFolder.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblRSSSection.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnRss.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblDiagnosticSection.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnDiagnostic.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblDeactivate.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnDeactivate.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblScale.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.cbScale.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblLanguage.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.cbLanguage.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.lblManageProfiles.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.btnManageProfiles.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.gbProfiles.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.RefreshProfiles();
				for (int i = 0; i < this.dgServerGames.RowCount; i++)
				{
					this.dgServerGames.Rows[i].Height = Util.ScaleSize(24);
				}
				this.dgServerGames.RowTemplate.Height = Util.ScaleSize(24);
				this.dgServerGames.DefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.dgServerGames.RowHeadersDefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.dgServerGames.ColumnHeadersDefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				for (int i = 0; i < this.dgResign.RowCount; i++)
				{
					this.dgResign.Rows[i].Height = Util.ScaleSize(24);
				}
				this.dgResign.RowTemplate.Height = Util.ScaleSize(24);
				this.dgResign.DefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.dgResign.RowHeadersDefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				this.dgResign.ColumnHeadersDefaultCellStyle.Font = new Font(Util.GetFontFamily(), Util.ScaleSize(8f), FontStyle.Regular, GraphicsUnit.Point, 0);
				bool flag3 = Util.IsUnixOrMacOSX();
				if (flag3)
				{
					foreach (object obj in this.dgServerGames.Controls)
					{
						ScrollBar scrollBar = (ScrollBar)obj;
						bool flag4 = scrollBar.GetType() == typeof(VScrollBar);
						if (flag4)
						{
							scrollBar.Location = new Point(this.dgServerGames.Width - scrollBar.Width, scrollBar.Location.Y);
							scrollBar.Height = this.dgServerGames.Height;
							break;
						}
					}
					foreach (object obj2 in this.dgResign.Controls)
					{
						ScrollBar scrollBar2 = (ScrollBar)obj2;
						bool flag5 = scrollBar2.GetType() == typeof(VScrollBar);
						if (flag5)
						{
							scrollBar2.Location = new Point(this.dgResign.Width - scrollBar2.Width, scrollBar2.Location.Y);
							scrollBar2.Height = this.dgResign.Height;
							break;
						}
					}
				}
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgServerGames_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00080958 File Offset: 0x0007EB58
		private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.dgResign.SelectedRows.Count != 1;
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				game game = this.dgResign.SelectedRows[0].Tag as game;
				bool flag2 = game == null || (string)this.dgResign.SelectedRows[0].Cells[1].Tag == "U";
				if (flag2)
				{
					e.Cancel = true;
				}
				else
				{
					this.registerProfileToolStripMenuItem.Visible = !this.m_psnIDs.ContainsKey(game.PSN_ID);
				}
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00080A10 File Offset: 0x0007EC10
		private void resignToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			bool flag = this.dgResign.SelectedRows.Count != 1;
			if (!flag)
			{
				this.DoResign(this.dgResign.SelectedRows[0].Index);
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00080A58 File Offset: 0x0007EC58
		private void btnImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Zip Files|*.zip";
			bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				try
				{
					ZipFile zipFile = new ZipFile(openFileDialog.FileName);
					IEnumerator<ZipEntry> enumerator = zipFile.GetEnumerator();
					Dictionary<ZipEntry, ZipEntry> dictionary = new Dictionary<ZipEntry, ZipEntry>();
					while (enumerator.MoveNext())
					{
						ZipEntry zipEntry = enumerator.Current;
						string[] array = zipEntry.FileName.Split(new char[] { '/' });
						bool flag2 = !zipEntry.IsDirectory && zipEntry.UncompressedSize > 2048L && array.Length > 1 && array[array.Length - 2].StartsWith("CUSA") && zipFile.EntryFileNames.Contains(zipEntry.FileName + ".bin");
						if (flag2)
						{
							IEnumerator<ZipEntry> enumerator2 = zipFile.SelectEntries(zipEntry.FileName + ".bin", Path.GetDirectoryName(zipEntry.FileName)).GetEnumerator();
							bool flag3 = enumerator2 != null;
							if (flag3)
							{
								bool flag4 = enumerator2.MoveNext();
								if (flag4)
								{
									string text = array[array.Length - 2];
									bool flag5 = this.IsValidForResign(new game
									{
										id = text,
										containers = new containers
										{
											_containers = new List<container>
											{
												new container
												{
													pfs = array[array.Length - 1]
												}
											}
										}
									});
									if (flag5)
									{
										dictionary.Add(zipEntry, enumerator2.Current);
									}
								}
							}
						}
					}
					bool flag6 = dictionary.Count > 0;
					if (flag6)
					{
						string text2 = this.cbDrives.SelectedItem as string;
						bool flag7 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text2);
						if (flag7)
						{
							text2 = string.Format("/Volumes{0}", text2);
						}
						else
						{
							bool flag8 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text2);
							if (flag8)
							{
								text2 = string.Format("/media/{0}{1}", Environment.UserName, text2);
							}
						}
						Import import = new Import(this.m_games, dictionary, zipFile, this.m_psnIDs, text2);
						bool flag9 = import.ShowDialog(this) == DialogResult.OK;
						if (flag9)
						{
							this.cbDrives_SelectedIndexChanged(null, null);
						}
					}
					else
					{
						Util.ShowMessage(Resources.msgNoValidSavesInZip);
					}
				}
				catch (Exception)
				{
					Util.ShowMessage(Resources.msgInvalidZip);
				}
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		private void btnResign_Click(object sender, EventArgs e)
		{
			bool visible = this.tabPageResign.Visible;
			if (!visible)
			{
				this.btnImport.Visible = (this.tabPageResign.Visible = true);
				this.chkShowAll.Visible = (this.tabPageGames.Visible = false);
				bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag)
				{
					this.btnImport.Size = new Size(Util.ScaleSize(75), Util.ScaleSize(23));
					this.chkShowAll.Size = new Size(0, 0);
					int num = 0;
					int num2 = 0;
					foreach (object obj in this.dgServerGames.Controls)
					{
						ScrollBar scrollBar = (ScrollBar)obj;
						bool flag2 = scrollBar.GetType() == typeof(VScrollBar);
						if (flag2)
						{
							num = scrollBar.Height;
							num2 = scrollBar.Location.X;
							break;
						}
					}
					foreach (object obj2 in this.dgResign.Controls)
					{
						ScrollBar scrollBar2 = (ScrollBar)obj2;
						bool flag3 = scrollBar2.GetType() == typeof(VScrollBar);
						if (flag3)
						{
							scrollBar2.Height = num;
							scrollBar2.Location = new Point(num2, scrollBar2.Location.Y);
							break;
						}
					}
				}
				this.btnResign.BackColor = Color.White;
				this.btnCheats.BackColor = Color.FromArgb(230, 230, 230);
				this.dgResign.Focus();
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00080EE8 File Offset: 0x0007F0E8
		private void btnCheats_Click(object sender, EventArgs e)
		{
			bool visible = this.tabPageGames.Visible;
			if (!visible)
			{
				this.chkShowAll.Visible = (this.tabPageGames.Visible = true);
				this.btnImport.Visible = (this.tabPageResign.Visible = false);
				bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag)
				{
					this.chkShowAll.Size = new Size(Util.ScaleSize(97), Util.ScaleSize(16));
					this.btnImport.Size = new Size(0, 0);
				}
				this.btnCheats.BackColor = Color.White;
				this.btnResign.BackColor = Color.FromArgb(230, 230, 230);
				this.dgServerGames.Focus();
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00080FC0 File Offset: 0x0007F1C0
		private void dgResign_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			bool flag = e.Column.Index == 1;
			if (flag)
			{
				string text = e.CellValue1 as string;
				string text2 = e.CellValue2 as string;
				bool flag2 = text.IndexOf("    ") >= 0;
				if (flag2)
				{
					game game = this.dgResign.Rows[e.RowIndex1].Tag as game;
					text = (string.IsNullOrEmpty(game.name) ? game.id : (game.name + " (" + game.id + ")"));
				}
				bool flag3 = text2.IndexOf("    ") >= 0;
				if (flag3)
				{
					game game2 = this.dgResign.Rows[e.RowIndex2].Tag as game;
					text2 = (string.IsNullOrEmpty(game2.name) ? game2.id : (game2.name + " (" + game2.id + ")"));
				}
				string[] array = text.Split(new string[] { " (" }, StringSplitOptions.None);
				string[] array2 = text2.Split(new string[] { " (" }, StringSplitOptions.None);
				bool flag4 = text == text2;
				if (flag4)
				{
					bool flag5 = (e.CellValue1 as string).IndexOf("    ") >= 0 && (e.CellValue2 as string).IndexOf("    ") >= 0;
					if (flag5)
					{
						e.SortResult = (e.CellValue1 as string).CompareTo(e.CellValue2 as string);
					}
					else
					{
						bool flag6 = (e.CellValue1 as string).IndexOf("    ") >= 0;
						if (flag6)
						{
							e.SortResult = ((this.dgResign.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending) ? 1 : (-1));
						}
						bool flag7 = (e.CellValue2 as string).IndexOf("    ") >= 0;
						if (flag7)
						{
							e.SortResult = ((this.dgResign.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending) ? (-1) : 1);
						}
					}
					e.Handled = true;
				}
				else
				{
					bool flag8 = array.Length >= 2 && array2.Length >= 2;
					if (flag8)
					{
						bool flag9 = array[0] == array2[0];
						if (flag9)
						{
							e.SortResult = array[1].CompareTo(array2[1]);
						}
						else
						{
							e.SortResult = array[0].CompareTo(array2[0]);
						}
					}
					else
					{
						bool flag10 = array.Length >= 2 && array2.Length == 1;
						if (flag10)
						{
							e.SortResult = array[0].CompareTo("ZZZZ");
						}
						else
						{
							bool flag11 = array2.Length >= 2 && array.Length == 1;
							if (flag11)
							{
								e.SortResult = "ZZZZ".CompareTo(array2[0]);
							}
							else
							{
								bool flag12 = array.Length == 1 && array2.Length == 1;
								if (flag12)
								{
									e.SortResult = array[0].CompareTo(array2[0]);
								}
							}
						}
					}
					e.Handled = true;
				}
			}
			else
			{
				e.Handled = false;
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00081314 File Offset: 0x0007F514
		private void dgResign_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgResign.SelectedCells.Count == 0 || this.dgResign.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					int rowIndex = this.dgResign.SelectedCells[0].RowIndex;
					string text = this.dgResign.Rows[rowIndex].Cells[1].Value as string;
					string toolTipText = this.dgResign.Rows[rowIndex].Cells[1].ToolTipText;
					game game = this.dgResign.Rows[rowIndex].Tag as game;
					bool flag3 = game == null;
					if (flag3)
					{
						List<game> list = this.dgResign.Rows[this.dgResign.SelectedCells[0].RowIndex].Tag as List<game>;
						bool flag4 = list == null;
						if (flag4)
						{
							bool flag5 = toolTipText == Resources.msgUnsupported;
							if (flag5)
							{
								Util.ShowMessage(toolTipText);
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex = this.dgResign.FirstDisplayedScrollingRowIndex;
							bool flag6 = this.dgResign.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending;
							int num = e.RowIndex;
							this.FillResignSaves(this.dgResign.Rows[this.dgResign.SelectedCells[0].RowIndex].Cells[1].Value as string, flag6);
							bool flag7 = this.m_expandedGameResign != null;
							if (flag7)
							{
								foreach (object obj in ((IEnumerable)this.dgResign.Rows))
								{
									DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
									bool flag8 = dataGridViewRow.Cells[1].Value as string == this.m_expandedGameResign;
									if (flag8)
									{
										num = dataGridViewRow.Index;
										break;
									}
								}
							}
							bool flag9 = this.dgResign.Rows.Count > e.RowIndex + 1;
							if (flag9)
							{
								this.dgResign.Rows[num + 1].Selected = true;
								this.dgResign.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
							}
							else
							{
								try
								{
									this.dgResign.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
								}
								catch (Exception)
								{
								}
							}
						}
					}
					else
					{
						int firstDisplayedScrollingRowIndex2 = this.dgResign.FirstDisplayedScrollingRowIndex;
						bool flag10 = (string)this.dgResign.Rows[rowIndex].Cells[1].Tag == "U";
						if (!flag10)
						{
							this.DoResign(rowIndex);
							try
							{
								this.dgResign.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex2;
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00081658 File Offset: 0x0007F858
		private void DoResign(int index)
		{
			game game = this.dgResign.Rows[index].Tag as game;
			bool flag = game != null && game.LocalSaveFolder != null && !Util.HasWritePermission(game.LocalSaveFolder);
			if (flag)
			{
				Util.ShowMessage(Resources.errWriteForbidden);
			}
			else
			{
				bool flag2 = Util.GetRegistryValue("NoResignMessage") == null;
				if (flag2)
				{
					ResignInfo resignInfo = new ResignInfo();
					resignInfo.ShowDialog(this);
				}
				byte[] array = File.ReadAllBytes(game.LocalSaveFolder);
				bool flag3 = this.m_psnIDs.Count == 0;
				if (flag3)
				{
					Util.ShowMessage(Resources.msgNoProfiles);
				}
				else
				{
					ChooseProfile chooseProfile = new ChooseProfile(this.m_psnIDs, game.PSN_ID);
					bool flag4 = chooseProfile.ShowDialog(this) == DialogResult.OK;
					if (flag4)
					{
						string text = game.LocalSaveFolder.Replace(game.PSN_ID, chooseProfile.SelectedAccount);
						bool flag5 = File.Exists(text);
						if (flag5)
						{
							bool flag6 = Util.IsUnixOrMacOSX();
							if (flag6)
							{
								bool flag7 = Util.ShowMessage(Resources.msgConfirmResignOverwrite, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.No;
								if (flag7)
								{
									return;
								}
							}
						}
						ResignFilesUplaoder resignFilesUplaoder = new ResignFilesUplaoder(game, Path.GetDirectoryName(game.LocalSaveFolder), chooseProfile.SelectedAccount, new List<string>());
						bool flag8 = resignFilesUplaoder.ShowDialog(this) == DialogResult.OK;
						if (flag8)
						{
							ResignMessage resignMessage = new ResignMessage();
							resignMessage.ShowDialog(this);
							bool deleteExisting = resignMessage.DeleteExisting;
							if (deleteExisting)
							{
								File.Delete(game.LocalSaveFolder);
								File.Delete(game.LocalSaveFolder.Substring(0, game.LocalSaveFolder.Length - 4));
								string directoryName = Path.GetDirectoryName(game.LocalSaveFolder);
								bool flag9 = Directory.GetFiles(directoryName).Length == 0;
								if (flag9)
								{
									Directory.Delete(directoryName);
								}
							}
							this.cbDrives_SelectedIndexChanged(null, null);
						}
					}
					this.m_expandedGameResign = null;
				}
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0008183C File Offset: 0x0007FA3C
		private void btnHome_ChangeUICues(object sender, UICuesEventArgs e)
		{
			bool changeFocus = e.ChangeFocus;
			if (changeFocus)
			{
				this.btnHome.Focus();
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00081860 File Offset: 0x0007FA60
		private void chkShowAll_EnabledChanged(object sender, EventArgs e)
		{
			bool enabled = this.chkShowAll.Enabled;
			if (enabled)
			{
				this.chkShowAll.ForeColor = Color.White;
				this.chkShowAll.FlatStyle = FlatStyle.Standard;
			}
			else
			{
				this.chkShowAll.ForeColor = Color.FromArgb(190, 190, 190);
				this.chkShowAll.FlatStyle = FlatStyle.Flat;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000818D0 File Offset: 0x0007FAD0
		private void cbDrives_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = this.cbDrives.SelectedIndex < 0;
			if (!flag)
			{
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
					e.Graphics.DrawString(this.cbDrives.Items[e.Index].ToString(), e.Font, Brushes.White, new Point(e.Bounds.X, e.Bounds.Y));
				}
				else
				{
					e.Graphics.FillRectangle(Brushes.White, e.Bounds);
					e.Graphics.DrawString(this.cbDrives.Items[e.Index].ToString(), e.Font, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
				}
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000819FC File Offset: 0x0007FBFC
		private void dgServerGames_SelectionChanged(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedRows != null && this.dgServerGames.SelectedRows.Count > 0;
			if (flag)
			{
				int index = this.dgServerGames.SelectedRows[0].Index;
				bool flag2 = Util.IsUnixOrMacOSX();
				if (flag2)
				{
					this.dgServerGames.SelectionChanged -= this.dgServerGames_SelectionChanged;
				}
				bool @checked = this.chkShowAll.Checked;
				if (@checked)
				{
					this.dgServerGames.CurrentCell = this.dgServerGames.SelectedRows[0].Cells[1];
				}
				else
				{
					this.dgServerGames.CurrentCell = this.dgServerGames.SelectedRows[0].Cells[0];
				}
				bool flag3 = Util.IsUnixOrMacOSX();
				if (flag3)
				{
					this.dgServerGames.SelectionChanged += this.dgServerGames_SelectionChanged;
				}
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00081AF2 File Offset: 0x0007FCF2
		private void MainForm_Resize(object sender, EventArgs e)
		{
			this.chkShowAll_CheckedChanged(null, null);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00081B00 File Offset: 0x0007FD00
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool flag = base.ClientRectangle.Width == 0 || base.ClientRectangle.Height == 0;
			if (!flag)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
				}
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00081B98 File Offset: 0x0007FD98
		private void dgServerGames_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			bool flag = e.Column.Index == 1;
			if (flag)
			{
				SortOrder sortGlyphDirection = this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection;
				bool flag2 = sortGlyphDirection == SortOrder.Descending;
				if (flag2)
				{
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					bool flag3 = e.SortResult == 0;
					if (flag3)
					{
						bool flag4 = this.dgServerGames.Rows[e.RowIndex1].Cells[1].Value.ToString().StartsWith("    ");
						if (flag4)
						{
							e.SortResult = -1;
						}
						bool flag5 = this.dgServerGames.Rows[e.RowIndex2].Cells[1].Value.ToString().StartsWith("    ");
						if (flag5)
						{
							e.SortResult = 1;
						}
					}
				}
				else
				{
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					bool flag6 = e.SortResult == 0;
					if (flag6)
					{
						bool flag7 = this.dgServerGames.Rows[e.RowIndex1].Cells[1].Value.ToString().StartsWith("    ");
						if (flag7)
						{
							e.SortResult = 1;
						}
						bool flag8 = this.dgServerGames.Rows[e.RowIndex2].Cells[1].Value.ToString().StartsWith("    ");
						if (flag8)
						{
							e.SortResult = -1;
						}
					}
				}
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00081E5C File Offset: 0x0008005C
		private void dgServerGames_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgServerGames.SelectedCells.Count == 0 || this.dgServerGames.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
					bool flag3 = toolTipText == Resources.msgUnsupported;
					if (flag3)
					{
						Util.ShowMessage(toolTipText);
					}
				}
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00081F00 File Offset: 0x00080100
		private void chkShowAll_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.chkShowAll.Checked;
			if (@checked)
			{
				bool flag = sender != null;
				if (flag)
				{
					this.dgServerGames.Rows.Clear();
					this.pnlNoSaves.Visible = false;
					this.pnlNoSaves.SendToBack();
					this.dgServerGames.Columns[0].Visible = false;
					this.dgServerGames.Columns[3].Visible = false;
					this.dgServerGames.Columns[4].Visible = false;
					this.dgServerGames.Columns[7].Visible = true;
					this.dgServerGames.Columns[8].Visible = false;
					this.m_games.Sort((game item1, game item2) => item2.acts.CompareTo(item1.acts));
					this.ShowAllGames();
				}
			}
			else
			{
				bool flag2 = sender != null;
				if (flag2)
				{
					this.dgServerGames.Rows.Clear();
					this.dgServerGames.Columns[0].Visible = true;
					this.dgServerGames.Columns[3].Visible = true;
					this.dgServerGames.Columns[4].Visible = true;
					this.dgServerGames.Columns[7].Visible = false;
					this.dgServerGames.Columns[8].Visible = true;
					this.dgServerGames.Columns[3].HeaderText = Resources.colGameCode;
					this.m_games.Sort((game item1, game item2) => (item1.name + item1.LocalSaveFolder).CompareTo(item2.name + item1.LocalSaveFolder));
					this.cbDrives_SelectedIndexChanged(null, null);
				}
			}
			this.dgServerGames.Focus();
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000820FC File Offset: 0x000802FC
		private void ShowAllGames()
		{
			((ISupportInitialize)this.dgServerGames).BeginInit();
			this.dgServerGames.Rows.Clear();
			List<DataGridViewRow> list = new List<DataGridViewRow>();
			foreach (game game in this.m_games)
			{
				foreach (alias alias in game.GetAllAliases(true, true))
				{
					bool flag = game.name == alias.name && game.id != alias.id;
					if (!flag)
					{
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(this.dgServerGames);
						dataGridViewRow.Tag = game;
						dataGridViewRow.Height = Util.ScaleSize(24);
						dataGridViewRow.Cells[1].Value = alias.name;
						dataGridViewRow.Cells[2].Value = game.GetCheatCount();
						dataGridViewRow.Cells[7].Value = ((game.acts != 0) ? new DateTime(1970, 1, 1).AddSeconds((double)game.acts).ToString("yyyy-MM-dd") : "");
						string text = "";
						text = Util.GetRegion(this.RegionMap, game.region, text);
						List<string> list2 = new List<string>();
						bool flag2 = game.name == alias.name;
						if (flag2)
						{
							list2.Add(game.id);
						}
						bool flag3 = game.aliases != null && game.aliases._aliases.Count > 0;
						if (flag3)
						{
							foreach (alias alias2 in game.aliases._aliases)
							{
								bool flag4 = alias2.name != alias.name;
								if (!flag4)
								{
									string region = Util.GetRegion(this.RegionMap, alias2.region, text);
									bool flag5 = text.IndexOf(region) < 0;
									if (flag5)
									{
										text += region;
									}
									list2.Add(alias2.id);
								}
							}
						}
						list2.Sort();
						dataGridViewRow.Cells[3].Value = text;
						dataGridViewRow.Cells[1].ToolTipText = string.Format(Resources.tootlTipSupported, string.Join(",", list2.ToArray()));
						list.Add(dataGridViewRow);
					}
				}
			}
			this.dgServerGames.Rows.AddRange(list.ToArray());
			((ISupportInitialize)this.dgServerGames).EndInit();
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00082454 File Offset: 0x00080654
		private void dgServerGames_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.chkShowAll.Checked && e.ColumnIndex == 2;
			if (!flag)
			{
				this.SortGames(e.ColumnIndex, this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending);
				bool @checked = this.chkShowAll.Checked;
				if (@checked)
				{
					this.ShowAllGames();
				}
				else
				{
					this.FillLocalSaves(null, this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
				}
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000824F4 File Offset: 0x000806F4
		private void dgServerGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgServerGames.SelectedCells.Count == 0 || this.dgServerGames.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					string text = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].Value as string;
					string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
					game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
					bool flag3 = game != null && game.LocalSaveFolder != null && !Util.HasWritePermission(game.LocalSaveFolder);
					if (flag3)
					{
						Util.ShowMessage(Resources.errWriteForbidden);
					}
					else
					{
						bool flag4 = game == null;
						if (flag4)
						{
							List<game> list = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as List<game>;
							bool flag5 = list == null;
							if (flag5)
							{
								bool flag6 = toolTipText == Resources.msgUnsupported;
								if (flag6)
								{
									Util.ShowMessage(toolTipText);
								}
							}
							else
							{
								int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
								bool flag7 = this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending;
								this.FillLocalSaves(this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].Value as string, flag7);
								bool flag8 = this.dgServerGames.Rows.Count > e.RowIndex + 1;
								if (flag8)
								{
									this.dgServerGames.Rows[e.RowIndex + 1].Selected = true;
									this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
								}
								else
								{
									this.dgServerGames.Rows[Math.Min(e.RowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
									try
									{
										this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
									}
									catch (Exception)
									{
									}
								}
							}
						}
						else
						{
							this.simpleToolStripMenuItem_Click(null, null);
						}
					}
				}
			}
		}

		// Token: 0x060017CB RID: 6091
		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		// Token: 0x060017CC RID: 6092
		[DllImport("user32.dll")]
		private static extern bool InsertMenu(IntPtr hMenu, int wPosition, int wFlags, int wIDNewItem, string lpNewItem);

		// Token: 0x060017CD RID: 6093 RVA: 0x000827C4 File Offset: 0x000809C4
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 274;
			if (flag)
			{
				int num = m.WParam.ToInt32();
				if (num == 1000)
				{
					AboutBox1 aboutBox = new AboutBox1();
					aboutBox.ShowDialog();
					return;
				}
			}
			else
			{
				bool flag2 = m.Msg == 537 && this.m_bSerialChecked;
				if (flag2)
				{
					bool flag3 = m.WParam.ToInt32() == 32768;
					if (flag3)
					{
						bool flag4 = m.LParam != IntPtr.Zero;
						if (flag4)
						{
							MainForm3.DEV_BROADCAST_HDR dev_BROADCAST_HDR = (MainForm3.DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(MainForm3.DEV_BROADCAST_HDR));
							bool flag5 = dev_BROADCAST_HDR.dbch_DeviceType == 2U;
							if (flag5)
							{
								MainForm3.DEV_BROADCAST_VOLUME dev_BROADCAST_VOLUME = (MainForm3.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(MainForm3.DEV_BROADCAST_VOLUME));
								for (int i = 0; i < 26; i++)
								{
									bool flag6 = ((dev_BROADCAST_VOLUME.dbcv_unitmask >> i) & 1U) == 1U;
									if (flag6)
									{
										this.drivesHelper.FillDrives();
									}
								}
							}
						}
					}
					else
					{
						bool flag7 = m.WParam.ToInt32() == 32772;
						if (flag7)
						{
							bool flag8 = m.LParam != IntPtr.Zero;
							if (flag8)
							{
								MainForm3.DEV_BROADCAST_HDR dev_BROADCAST_HDR2 = (MainForm3.DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(MainForm3.DEV_BROADCAST_HDR));
								bool flag9 = dev_BROADCAST_HDR2.dbch_DeviceType == 2U;
								if (flag9)
								{
									MainForm3.DEV_BROADCAST_VOLUME dev_BROADCAST_VOLUME2 = (MainForm3.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(MainForm3.DEV_BROADCAST_VOLUME));
									for (int j = 0; j < 26; j++)
									{
										bool flag10 = ((dev_BROADCAST_VOLUME2.dbcv_unitmask >> j) & 1U) == 1U;
										if (flag10)
										{
											for (int k = 0; k < this.cbDrives.Items.Count; k++)
											{
												bool flag11 = this.cbDrives.Items[k].ToString() == string.Format("{0}:\\", (char)(65 + j));
												if (flag11)
												{
													this.cbDrives.Items.RemoveAt(k);
												}
											}
										}
									}
									bool flag12 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
									if (flag12)
									{
										this.dgResign.Rows.Clear();
										this.chkShowAll.Checked = true;
										this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = false));
										this.drivesHelper.FillDrives();
									}
									else
									{
										this.cbDrives.SelectedIndex = 0;
									}
								}
							}
						}
					}
				}
			}
			bool flag13 = m.Msg == 274 && (int)m.WParam == 61728;
			if (flag13)
			{
				base.Invalidate(true);
			}
			base.WndProc(ref m);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00082B18 File Offset: 0x00080D18
		private int InitSession(int attempt)
		{
			bool flag = string.IsNullOrEmpty(Util.forceServer);
			string text;
			if (flag)
			{
				text = string.Format("Trying Random Server {0}", attempt);
			}
			else
			{
				text = string.Format("Trying Server from arguments - {0}", Util.forceServer);
			}
			WaitingForm waitingForm = new WaitingForm(text);
			waitingForm.Start();
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				string uid = Util.GetUID(false, false);
				bool flag2 = string.IsNullOrEmpty(uid);
				if (flag2)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase(), true);
					try
					{
						registryKey.DeleteValue("Hash");
					}
					catch
					{
					}
					Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME), Resources.msgError);
					base.Close();
					return 0;
				}
				byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"START_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), uid)));
				string @string = Encoding.ASCII.GetString(array);
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag3 = dictionary.ContainsKey("pid") && dictionary.ContainsKey("cid");
				if (flag3)
				{
					int num = Convert.ToInt32(dictionary["pid"]);
					int num2 = Convert.ToInt32(dictionary["cid"]);
					Util.pid = num;
					bool flag4 = num == 16 || num == 18 || num == 22;
					if (flag4)
					{
						this.pictureBox2.BackgroundImage = Resources.logo_swps4us;
						this.lblLanguage.Visible = false;
						this.cbLanguage.Visible = false;
						Util.PRODUCT_NAME = "Save Wizard for PS4";
						Util.IsHyperkinMode = true;
						this.Text = Util.PRODUCT_NAME;
					}
				}
				bool flag5 = dictionary.ContainsKey("update");
				if (flag5)
				{
					Util.ShowMessage(string.Format("{0} has been upgraded. Please download a BETA version from https://savewizard.net/beta/", Util.PRODUCT_NAME));
					base.Close();
					return -2;
				}
				bool flag6 = dictionary.ContainsKey("token");
				if (flag6)
				{
					bool flag7 = dictionary.ContainsKey("minfsize");
					if (flag7)
					{
						Util.SetMinFileSize(Convert.ToInt32(dictionary["minfsize"]));
					}
					bool flag8 = dictionary.ContainsKey("maxfsize");
					if (flag8)
					{
						Util.SetMaxFileSize(Convert.ToInt32(dictionary["maxfsize"]));
					}
					Util.SetAuthToken(dictionary["token"] as string);
					Thread thread = new Thread(new ParameterizedThreadStart(this.Pinger));
					thread.Start(Convert.ToInt32(dictionary["expiry_ts"]) - Convert.ToInt32(dictionary["current_ts"]));
					Thread thread2 = new Thread(new ParameterizedThreadStart(this.TrafficPoller));
					thread2.Start();
					this.GetPSNIDInfo();
					this.m_sessionInited = true;
					return 1;
				}
				bool flag9 = dictionary.ContainsKey("code") && (dictionary["code"].ToString() == "10009" || dictionary["code"].ToString() == "4071");
				if (flag9)
				{
					return -1;
				}
				Util.DeleteRegistryValue("User");
				bool flag10 = dictionary.ContainsKey("code");
				if (flag10)
				{
					Util.ShowErrorMessage(dictionary, string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME));
				}
				else
				{
					Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME));
				}
				base.Close();
				bool flag11 = dictionary.ContainsKey("code") && (dictionary["code"].ToString() == "4041" || dictionary["code"].ToString() == "4045" || dictionary["code"].ToString() == "4005");
				if (flag11)
				{
					return -2;
				}
				return 0;
			}
			catch (Exception)
			{
			}
			finally
			{
				waitingForm.Stop();
			}
			return -1;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00082FBC File Offset: 0x000811BC
		private void MainForm_Load(object sender, EventArgs e)
		{
			bool flag = !Util.IsUnixOrMacOSX();
			if (flag)
			{
				IntPtr systemMenu = MainForm3.GetSystemMenu(base.Handle, false);
				MainForm3.InsertMenu(systemMenu, 5, 3072, 0, string.Empty);
				MainForm3.InsertMenu(systemMenu, 6, 1024, 1000, "About Save Wizard for PS4...");
			}
			bool flag2 = !this.CheckForVersion();
			if (!flag2)
			{
				StartupScreen startupScreen = new StartupScreen(Util.IsNeedToShowUpdateScreen);
				bool flag3 = startupScreen.ShowDialog(this) != DialogResult.OK;
				if (flag3)
				{
					base.Close();
				}
				else
				{
					bool flag4 = !this.CheckSerial();
					if (flag4)
					{
						base.Close();
					}
					else
					{
						this.m_bSerialChecked = true;
						int num = 1;
						int num2 = this.InitSession(num);
						while (num2 <= 0 && num < Util.SERVERS.Length + 1 && string.IsNullOrEmpty(Util.forceServer))
						{
							bool flag5 = num2 == -2;
							if (flag5)
							{
								base.Close();
								return;
							}
							num++;
							Util.ChangeServer();
							Thread.Sleep(500);
							num2 = this.InitSession(num);
						}
						bool flag6 = num2 == 0;
						if (!flag6)
						{
							bool flag7 = num2 < 0;
							if (flag7)
							{
								Util.ShowMessage(Resources.errServerConnection);
								base.Close();
							}
							else
							{
								GameListDownloader gameListDownloader = new GameListDownloader();
								gameListDownloader.ShowDialog();
								bool flag8 = this.m_psnIDs.Count == 0;
								if (flag8)
								{
								}
								try
								{
									this.FillSavesList(gameListDownloader.GameListXml);
								}
								catch (Exception)
								{
									Util.ShowMessage(Resources.errInternal, Resources.msgError);
									base.Close();
									return;
								}
								bool flag9 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
								if (flag9)
								{
									this.chkShowAll.Checked = true;
									this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = false));
									this.btnHome_Click(null, null);
								}
								else
								{
									this.PrepareLocalSavesMap();
									this.FillLocalSaves(null, true);
									this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
									this.btnHome_Click(string.Empty, null);
								}
								bool flag10 = !this.isRunning && Util.IsUnixOrMacOSX();
								if (flag10)
								{
									global::System.Timers.Timer timer = new global::System.Timers.Timer();
									this.previousDriveNum = DriveInfo.GetDrives().Length;
									timer.Elapsed += delegate(object s, ElapsedEventArgs e2)
									{
										DriveInfo[] drives = DriveInfo.GetDrives();
										bool flag12 = this.previousDriveNum != drives.Length;
										if (flag12)
										{
											this.previousDriveNum = drives.Length;
											this.drivesHelper.FillDrives();
											bool flag13 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
											if (flag13)
											{
												this.dgResign.Rows.Clear();
												this.chkShowAll.Checked = true;
												this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = false));
											}
										}
									};
									timer.Interval = 10000.0;
									timer.Enabled = true;
									this.isRunning = true;
								}
								MainForm3.isFirstRunning = false;
								this.ShowHideNoSavesPanels();
								bool flag11 = Util.CurrentPlatform == Util.Platform.MacOS;
								if (flag11)
								{
									base.Visible = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000832BC File Offset: 0x000814BC
		private void TrafficPoller(object ob)
		{
			this.evt2 = new AutoResetEvent(false);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000832CC File Offset: 0x000814CC
		private void Pinger(object tim)
		{
			int num = (int)tim;
			this.evt = new AutoResetEvent(false);
			string text = "{{\"action\":\"SESSION_REFRESH\",\"userid\":\"{0}\",\"token\":\"{1}\"}}";
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			while (!this.evt.WaitOne((num - 10) * 1000))
			{
				for (;;)
				{
					try
					{
						byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format(text, Util.GetUserId(), Util.GetAuthToken())));
						string @string = Encoding.ASCII.GetString(array);
						bool flag = @string.Contains("ERROR");
						if (flag)
						{
							return;
						}
						Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
						bool flag2 = dictionary.ContainsKey("token");
						if (flag2)
						{
							Util.SetAuthToken(dictionary["token"] as string);
						}
						break;
					}
					catch (Exception ex)
					{
						Thread.Sleep(3000);
					}
				}
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00083404 File Offset: 0x00081604
		private void PrepareLocalSavesMap()
		{
			this.m_dictLocalSaves.Clear();
			this.m_dictAllLocalSaves.Clear();
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				string text = this.cbDrives.SelectedItem.ToString();
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text);
				if (flag2)
				{
					text = string.Format("/Volumes{0}", text);
				}
				else
				{
					bool flag3 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text);
					if (flag3)
					{
						text = string.Format("/media/{0}{1}", Environment.UserName, text);
					}
				}
				string dataPath = Util.GetDataPath(text);
				bool flag4 = !Directory.Exists(dataPath);
				if (!flag4)
				{
					string[] array = Directory.GetDirectories(dataPath);
					List<string> list = new List<string>();
					foreach (string text2 in array)
					{
						string fileName = Path.GetFileName(text2);
						long num;
						bool flag5 = !long.TryParse(fileName, NumberStyles.HexNumber, null, out num);
						if (!flag5)
						{
							string[] directories = Directory.GetDirectories(text2);
							foreach (string text3 in directories)
							{
								string fileName2 = Path.GetFileName(text3);
								bool flag6 = !fileName2.StartsWith("CUSA") || Directory.GetFiles(text3).Length == 0;
								if (!flag6)
								{
									list.AddRange(Directory.GetFiles(text3, "*.bin"));
								}
							}
						}
					}
					array = list.ToArray();
					Array.Sort<string>(array);
					foreach (string text4 in array)
					{
						bool flag7 = !File.Exists(text4.Substring(0, text4.Length - 4));
						if (!flag7)
						{
							string text5;
							int onlineSaveIndex = MainForm3.GetOnlineSaveIndex(this.m_games, text4, out text5);
							bool flag8 = onlineSaveIndex >= 0;
							if (flag8)
							{
								game game = game.Copy(this.m_games[onlineSaveIndex]);
								game.id = text5;
								game.LocalCheatExists = true;
								game.LocalSaveFolder = text4;
								game.UpdatedTime = this.GetSaveUpdateTime(text4);
								bool flag9 = game.GetTargetGameFolder() == null;
								if (flag9)
								{
									game.LocalCheatExists = false;
								}
								try
								{
									MainForm3.FillLocalCheats(ref game);
								}
								catch (Exception)
								{
								}
								bool flag10 = !this.m_dictLocalSaves.ContainsKey(game.id);
								if (flag10)
								{
									List<game> list2 = new List<game>();
									list2.Add(game);
									this.m_dictLocalSaves.Add(game.id, list2);
								}
								else
								{
									this.m_dictLocalSaves[game.id].Add(game);
								}
								bool flag11 = !this.m_dictAllLocalSaves.ContainsKey(game.id);
								if (flag11)
								{
									List<game> list3 = new List<game>();
									this.m_dictAllLocalSaves.Add(game.id, list3);
								}
								this.m_dictAllLocalSaves[game.id].Add(game);
							}
							else
							{
								string text6 = text4.Substring(0, text4.Length - 4);
								bool flag12 = File.Exists(text6);
								if (flag12)
								{
									string fileName3 = Path.GetFileName(Path.GetDirectoryName(text4));
									game game2 = new game
									{
										name = "",
										id = fileName3,
										containers = new containers
										{
											_containers = new List<container>
											{
												new container
												{
													pfs = Path.GetFileName(text6).Substring(0, Path.GetFileName(text6).Length)
												}
											}
										},
										LocalSaveFolder = text4
									};
									bool flag13 = !this.m_dictAllLocalSaves.ContainsKey(game2.id);
									if (flag13)
									{
										List<game> list4 = new List<game>();
										this.m_dictAllLocalSaves.Add(game2.id, list4);
									}
									this.m_dictAllLocalSaves[game2.id].Add(game2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00083828 File Offset: 0x00081A28
		private DateTime TimeForComapre(game item)
		{
			List<alias> allAliases = item.GetAllAliases(true, false);
			bool flag = allAliases.Count == 0 || allAliases[0] == null;
			DateTime dateTime;
			if (flag)
			{
				dateTime = item.UpdatedTime;
			}
			else
			{
				foreach (alias alias in allAliases)
				{
					bool flag2 = !this.m_dictLocalSaves.ContainsKey(alias.id);
					if (!flag2)
					{
						List<game> list = this.m_dictLocalSaves[alias.id];
						bool flag3 = list.Count == 0;
						if (flag3)
						{
							return item.UpdatedTime;
						}
						return this.GetSaveUpdateTime(list[0].LocalSaveFolder);
					}
				}
				dateTime = item.UpdatedTime;
			}
			return dateTime;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0008390C File Offset: 0x00081B0C
		private DateTime GetSaveUpdateTime(string save)
		{
			DateTime dateTime = new FileInfo(save).LastWriteTime;
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(save));
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				bool flag = fileInfo.LastWriteTime > dateTime;
				if (flag)
				{
					dateTime = fileInfo.LastWriteTime;
				}
			}
			return dateTime;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00083974 File Offset: 0x00081B74
		private void FillResignSaves(string expandGame, bool bSortedAsc)
		{
			bool flag = this.m_expandedGameResign == expandGame;
			if (flag)
			{
				expandGame = null;
				this.m_expandedGameResign = null;
			}
			((ISupportInitialize)this.dgResign).BeginInit();
			this.dgResign.Rows.Clear();
			List<string> list = new List<string>();
			using (Dictionary<string, List<game>>.KeyCollection.Enumerator enumerator = this.m_dictAllLocalSaves.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string id = enumerator.Current;
					game game = this.m_games.Find((game a) => a.id == id);
					bool flag2 = game == null;
					if (flag2)
					{
						game = this.m_dictAllLocalSaves[id][0];
					}
					foreach (alias alias in game.GetAllAliases(bSortedAsc, false))
					{
						string text = alias.name;
						text = text + " (" + alias.id + ")";
						string id2 = alias.id;
						bool flag3 = !this.m_dictAllLocalSaves.ContainsKey(alias.id);
						if (!flag3)
						{
							List<game> list2 = this.m_dictAllLocalSaves[id2];
							bool flag4 = list.IndexOf(id2) >= 0;
							if (!flag4)
							{
								list.Add(id2);
								int num = this.dgResign.Rows.Add();
								this.dgResign.Rows[num].Cells[1].Value = alias.name;
								bool flag5 = list2.Count == 0;
								if (flag5)
								{
									game game2 = list2[0];
									this.dgResign.Rows[num].Tag = game2;
									container targetGameFolder = game2.GetTargetGameFolder();
									bool flag6 = targetGameFolder != null;
									if (flag6)
									{
										this.dgResign.Rows[num].Cells[2].Value = targetGameFolder.GetCheatsCount();
									}
									else
									{
										this.dgResign.Rows[num].Cells[2].Value = "N/A";
									}
									this.dgResign.Rows[num].Cells[0].ToolTipText = "";
									this.dgResign.Rows[num].Cells[0].Tag = id2;
									this.dgResign.Rows[num].Cells[2].Value = this.GetPSNID(game2);
									bool flag7 = !this.IsValidForResign(game2);
									if (flag7)
									{
										this.dgResign.Rows[num].DefaultCellStyle = new DataGridViewCellStyle
										{
											ForeColor = Color.Gray
										};
										this.dgResign.Rows[num].Cells[1].Tag = "U";
									}
								}
								else
								{
									DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
									this.dgResign.Rows[num].Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
									{
										Font = new Font("Arial", Util.ScaleSize(7f))
									});
									this.dgResign.Rows[num].Cells[0].Value = "►";
									string text2 = this.dgResign.Rows[num].Cells[1].Value as string;
									this.dgResign.Rows[num].Cells[1].Value = (string.IsNullOrEmpty(text2) ? alias.id : (text2 + " (" + alias.id + ")"));
									dataGridViewCellStyle.BackColor = Color.White;
									this.dgResign.Rows[num].Cells[0].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgResign.Rows[num].Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgResign.Rows[num].Cells[2].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgResign.Rows[num].Tag = list2;
									bool flag8 = !this.IsValidForResign(game);
									if (flag8)
									{
										this.dgResign.Rows[num].DefaultCellStyle = new DataGridViewCellStyle
										{
											ForeColor = Color.Gray
										};
										this.dgResign.Rows[num].Cells[1].Tag = "U";
									}
									bool flag9 = text == expandGame || alias.id == expandGame;
									if (flag9)
									{
										this.dgResign.Rows[num].Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
										{
											Font = new Font("Arial", Util.ScaleSize(7f))
										});
										this.dgResign.Rows[num].Cells[0].Value = "▼";
										this.dgResign.Rows[num].Cells[0].ToolTipText = "";
										this.dgResign.Rows[num].Cells[1].Value = (string.IsNullOrEmpty(text2) ? alias.id : (text2 + " (" + alias.id + ")"));
										this.dgResign.Rows[num].Cells[0].Tag = id2;
										foreach (game game3 in list2)
										{
											container targetGameFolder2 = game3.GetTargetGameFolder();
											bool flag10 = targetGameFolder2 == null;
											if (!flag10)
											{
												int num2 = this.dgResign.Rows.Add();
												Match match = Regex.Match(Path.GetFileNameWithoutExtension(game3.LocalSaveFolder), targetGameFolder2.pfs);
												bool flag11 = targetGameFolder2.name != null && match.Groups != null && match.Groups.Count > 1;
												if (flag11)
												{
													this.dgResign.Rows[num2].Cells[1].Value = "    " + targetGameFolder2.name.Replace("${1}", match.Groups[1].Value);
												}
												else
												{
													this.dgResign.Rows[num2].Cells[1].Value = "    " + (targetGameFolder2.name ?? Path.GetFileNameWithoutExtension(game3.LocalSaveFolder));
												}
												this.dgResign.Rows[num2].Cells[0].Tag = id2;
												game3.name = alias.name;
												this.dgResign.Rows[num2].Tag = game3;
												this.dgResign.Rows[num2].Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game3.LocalSaveFolder);
												this.dgResign.Rows[num2].Cells[3].Value = this.GetPSNID(game3);
												string sysVer = MainForm3.GetSysVer(game3.LocalSaveFolder);
												this.dgResign.Rows[num2].Cells[2].Value = sysVer;
												string text3 = "";
												string text4 = sysVer;
												if (!(text4 == "?"))
												{
													if (!(text4 == "All"))
													{
														if (!(text4 == "4.50+"))
														{
															if (!(text4 == "4.70+"))
															{
																if (!(text4 == "5.00"))
																{
																	if (text4 == "5.50")
																	{
																		text3 = Resources.tooltipV5;
																	}
																}
																else
																{
																	text3 = Resources.tooltipV4;
																}
															}
															else
															{
																text3 = Resources.tooltipV3;
															}
														}
														else
														{
															text3 = Resources.tooltipV2;
														}
													}
													else
													{
														text3 = Resources.tooltipV1;
													}
												}
												else
												{
													text3 = Resources.msgUnknownSysVer;
												}
												this.dgResign.Rows[num2].Cells[2].ToolTipText = text3;
												bool flag12 = !this.IsValidForResign(game3);
												if (flag12)
												{
													this.dgResign.Rows[num2].DefaultCellStyle = new DataGridViewCellStyle
													{
														ForeColor = Color.Gray
													};
													this.dgResign.Rows[num2].Cells[1].Tag = "U";
												}
											}
										}
										this.m_expandedGameResign = expandGame;
									}
								}
							}
						}
					}
				}
			}
			bool flag13 = !Util.IsUnixOrMacOSX();
			if (flag13)
			{
				this.dgResign.Sort(this.dgResign.Columns[1], (!bSortedAsc) ? ListSortDirection.Descending : ListSortDirection.Ascending);
			}
			this.dgResign.ClearSelection();
			((ISupportInitialize)this.dgResign).EndInit();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000843C8 File Offset: 0x000825C8
		internal static string GetSysVer(string binFile)
		{
			byte[] array = File.ReadAllBytes(binFile);
			return MainForm3.GetSysVer(array);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000843E8 File Offset: 0x000825E8
		internal static string GetSysVer(byte[] buf)
		{
			bool flag = buf.Length > 8;
			string text;
			if (flag)
			{
				switch (buf[8])
				{
				case 1:
					text = "All";
					break;
				case 2:
					text = "4.50+";
					break;
				case 3:
					text = "4.70+";
					break;
				case 4:
					text = "5.00+";
					break;
				case 5:
					text = "5.50+";
					break;
				case 6:
					text = "6.00+";
					break;
				case 7:
					text = "6.50+";
					break;
				case 8:
					text = "7.00+";
					break;
				case 9:
					text = "7.50+";
					break;
				case 10:
					text = "8.00+";
					break;
				default:
					text = "?";
					break;
				}
			}
			else
			{
				text = "?";
			}
			return text;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0008449C File Offset: 0x0008269C
		private bool IsValidForResign(game item)
		{
			bool flag = this.m_rblist == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (rbgame rbgame in this.m_rblist._rbgames)
				{
					bool flag3 = rbgame.gamecode == item.id;
					if (flag3)
					{
						bool flag4 = rbgame.containers == null || rbgame.containers.container == null || rbgame.containers.container.Count == 0;
						if (flag4)
						{
							return false;
						}
						bool flag5 = item.LocalSaveFolder != null;
						if (flag5)
						{
							foreach (string text in rbgame.containers.container)
							{
								bool flag6 = Util.IsMatch(Path.GetFileNameWithoutExtension(item.LocalSaveFolder), text);
								if (flag6)
								{
									return false;
								}
							}
						}
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000845D8 File Offset: 0x000827D8
		private void FillLocalSaves(string expandGame, bool bSortedAsc)
		{
			bool flag = this.m_expandedGame == expandGame;
			if (flag)
			{
				expandGame = null;
				this.m_expandedGame = null;
			}
			this.dgServerGames.Rows.Clear();
			List<string> list = new List<string>();
			List<DataGridViewRow> list2 = new List<DataGridViewRow>();
			((ISupportInitialize)this.dgServerGames).BeginInit();
			foreach (game game in this.m_games)
			{
				foreach (alias alias in game.GetAllAliases(bSortedAsc, false))
				{
					string text = alias.name;
					text = text + " (" + alias.id + ")";
					string id = alias.id;
					bool flag2 = !this.m_dictLocalSaves.ContainsKey(alias.id);
					if (!flag2)
					{
						List<game> list3 = this.m_dictLocalSaves[id];
						bool flag3 = list.IndexOf(id) >= 0;
						if (!flag3)
						{
							list.Add(id);
							List<DataGridViewRow> list4 = new List<DataGridViewRow>();
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.dgServerGames);
							dataGridViewRow.Height = Util.ScaleSize(24);
							dataGridViewRow.Cells[1].Value = alias.name;
							bool flag4 = list3.Count == 0;
							if (flag4)
							{
								game game2 = list3[0];
								dataGridViewRow.Tag = game2;
								container targetGameFolder = game2.GetTargetGameFolder();
								dataGridViewRow.Cells[2].Value = ((targetGameFolder != null) ? targetGameFolder.GetCheatsCount().ToString() : "N/A");
								dataGridViewRow.Cells[0].ToolTipText = "";
								dataGridViewRow.Cells[0].Tag = id;
								dataGridViewRow.Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game2.LocalSaveFolder);
								dataGridViewRow.Cells[3].Value = id;
								dataGridViewRow.Cells[5].Value = true;
								dataGridViewRow.Cells[8].Value = game2.UpdatedTime;
								dataGridViewRow.Cells[4].Value = this.GetPSNID(game2);
								bool flag5 = !this.IsValidPSNID(game2.PSN_ID);
								if (flag5)
								{
									dataGridViewRow.DefaultCellStyle = new DataGridViewCellStyle
									{
										ForeColor = Color.Gray
									};
									dataGridViewRow.Cells[1].Tag = "U";
								}
							}
							else
							{
								DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
								dataGridViewRow.Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
								{
									Font = new Font("Arial", Util.ScaleSize(7f))
								});
								dataGridViewRow.Cells[0].Value = "►";
								dataGridViewRow.Cells[1].Value = string.Concat(new object[]
								{
									dataGridViewRow.Cells[1].Value,
									" (",
									alias.id,
									")"
								});
								dataGridViewCellStyle.BackColor = Color.White;
								dataGridViewRow.Cells[0].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[2].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[3].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[4].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[7].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[8].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Tag = list3;
								dataGridViewRow.Cells[5].Value = false;
								bool flag6 = text == expandGame;
								if (flag6)
								{
									dataGridViewRow.Cells[0].Tag = id;
									dataGridViewRow.Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
									{
										Font = new Font("Arial", Util.ScaleSize(7f))
									});
									dataGridViewRow.Cells[0].Value = "▼";
									dataGridViewRow.Cells[0].ToolTipText = "";
									dataGridViewRow.Cells[1].Value = alias.name + " (" + alias.id + ")";
									foreach (game game3 in list3)
									{
										container targetGameFolder2 = game3.GetTargetGameFolder();
										bool flag7 = targetGameFolder2 == null;
										if (!flag7)
										{
											DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
											dataGridViewRow2.CreateCells(this.dgServerGames);
											dataGridViewRow2.Height = Util.ScaleSize(24);
											Match match = Regex.Match(Path.GetFileNameWithoutExtension(game3.LocalSaveFolder), targetGameFolder2.pfs);
											bool flag8 = targetGameFolder2.name != null && match.Groups != null && match.Groups.Count > 1;
											if (flag8)
											{
												dataGridViewRow2.Cells[1].Value = "    " + targetGameFolder2.name.Replace("${1}", match.Groups[1].Value);
											}
											else
											{
												dataGridViewRow2.Cells[1].Value = "    " + (targetGameFolder2.name ?? Path.GetFileNameWithoutExtension(game3.LocalSaveFolder));
											}
											dataGridViewRow2.Cells[0].Tag = id;
											dataGridViewRow2.Tag = game3;
											dataGridViewRow2.Cells[2].Value = ((targetGameFolder2 != null) ? targetGameFolder2.GetCheatsCount().ToString() : "N/A");
											dataGridViewRow2.Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game3.LocalSaveFolder);
											dataGridViewRow2.Cells[3].Value = id;
											dataGridViewRow2.Cells[5].Value = true;
											dataGridViewRow2.Cells[8].Value = game3.UpdatedTime;
											dataGridViewRow2.Cells[4].Value = this.GetPSNID(game3);
											bool flag9 = !this.IsValidPSNID(game3.PSN_ID);
											if (flag9)
											{
												dataGridViewRow2.DefaultCellStyle = new DataGridViewCellStyle
												{
													ForeColor = Color.Gray
												};
												dataGridViewRow2.Cells[1].Tag = "U";
											}
											list4.Add(dataGridViewRow2);
										}
									}
									this.m_expandedGame = expandGame;
								}
							}
							list2.Add(dataGridViewRow);
							list2.AddRange(list4.ToArray());
						}
					}
				}
			}
			this.dgServerGames.Rows.AddRange(list2.ToArray());
			this.FillUnavailableGames();
			this.dgServerGames.ClearSelection();
			((ISupportInitialize)this.dgServerGames).EndInit();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00084E0C File Offset: 0x0008300C
		private object GetPSNID(game item)
		{
			bool flag = !this.IsValidPSNID(item.PSN_ID);
			object obj;
			if (flag)
			{
				obj = Resources.lblUnregistered + " " + item.PSN_ID;
			}
			else
			{
				Dictionary<string, object> dictionary = this.m_psnIDs[item.PSN_ID] as Dictionary<string, object>;
				obj = dictionary["friendly_name"];
			}
			return obj;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00084E6C File Offset: 0x0008306C
		private string GetProfileKey(string sfoPath, Dictionary<string, string> mapProfiles)
		{
			bool flag = File.Exists(sfoPath);
			if (flag)
			{
				int num;
				string text = Convert.ToBase64String(MainForm3.GetParamInfo(sfoPath, out num));
				string text2 = string.Concat(new string[]
				{
					num.ToString(),
					":",
					text,
					":",
					Convert.ToBase64String(Util.GetPSNId(Path.GetDirectoryName(sfoPath)))
				});
				bool flag2 = mapProfiles.ContainsKey(text2);
				if (flag2)
				{
					return mapProfiles[text2];
				}
				string text3 = num.ToString() + ":" + text;
				bool flag3 = mapProfiles.ContainsKey(text3);
				if (flag3)
				{
					return mapProfiles[text3];
				}
			}
			return "";
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00084F28 File Offset: 0x00083128
		private bool CheckSerial()
		{
			bool flag = Util.GetUserId() == null;
			if (flag)
			{
				SerialValidateGG serialValidateGG = new SerialValidateGG();
				bool flag2 = serialValidateGG.ShowDialog(this) != DialogResult.OK;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00084F68 File Offset: 0x00083168
		private void SetLabels()
		{
			this.picVersion.BackgroundImageLayout = ImageLayout.None;
			this.picVersion.Visible = false;
			this.pictureBox2.BackgroundImage = (Util.IsHyperkin() ? Resources.logo_swps4us : Resources.logo);
			this.panel1.BackgroundImage = Resources.sel_drive;
			this.lblNoSaves.Text = Resources.lblNoSaves;
			this.lblNoSaves2.Text = Resources.lblNoSaves;
			base.Icon = Resources.dp;
			this.panel3.BackColor = Color.FromArgb(102, 102, 102);
			this.btnGamesInServer.Text = Resources.btnViewAllCheats;
			this.btnApply.Text = Resources.btnApply;
			this.btnBrowse.Text = Resources.btnBrowse;
			this.chkBackup.Text = Resources.chkBackupSaves;
			this.lblBackup.Text = Resources.gbBackupLocation;
			this.dgServerGames.Columns[0].HeaderText = "";
			this.dgServerGames.Columns[1].HeaderText = Resources.colGameName;
			this.dgServerGames.Columns[2].HeaderText = Resources.colCheats;
			this.dgServerGames.Columns[3].HeaderText = Resources.colGameCode;
			this.dgServerGames.Columns[4].HeaderText = Resources.colProfile;
			this.dgServerGames.Columns[7].HeaderText = Resources.colAdded;
			this.dgServerGames.Columns[8].HeaderText = Resources.colUpdated;
			this.dgServerGames.Columns[3].Visible = false;
			this.btnRss.Text = Resources.btnRss;
			this.btnDeactivate.Text = Resources.btnDeactivate;
			this.btnDiagnostic.Text = Resources.btnDiagnostic;
			this.simpleToolStripMenuItem.Text = Resources.mnuSimple;
			this.advancedToolStripMenuItem.Text = Resources.mnuAdvanced;
			this.deleteSaveToolStripMenuItem.Text = Resources.mnuDeleteSave;
			this.resignToolStripMenuItem.Text = Resources.mnuResign;
			this.registerPSNIDToolStripMenuItem.Text = Resources.mnuRegisterPSN;
			this.restoreFromBackupToolStripMenuItem.Text = Resources.mnuRestore;
			this.Text = Util.PRODUCT_NAME;
			this.Text = this.Text + " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.btnOpenFolder.Text = Resources.btnOpenFolder;
			this.lblDeactivate.Text = Resources.lblDeactivate;
			this.lblRSSSection.Text = Resources.lblRSSSection;
			this.btnManageProfiles.Text = Resources.btnUserAccount;
			this.lblManageProfiles.Text = Resources.lblUserAccount;
			this.lblDiagnosticSection.Text = Resources.lblDiagnosticSection;
			bool flag = Util.IsHyperkin();
			if (flag)
			{
				this.lblLanguage.Visible = false;
				this.cbLanguage.Visible = false;
			}
			this.panel3.BackgroundImageLayout = ImageLayout.Tile;
			this.btnImport.Text = Resources.btnImport;
			this.btnCheats.Text = Resources.btnCheats;
			this.btnResign.Text = Resources.btnResign;
			this.chkShowAll.Text = Resources.chkShowAll;
			this.dgResign.Columns[1].HeaderText = Resources.colGameName;
			this.dgResign.Columns[2].HeaderText = Resources.colSysVer;
			this.dgResign.Columns[3].HeaderText = Resources.colProfile;
			this.lblLanguage.Text = Resources.lblLanguage;
			this.registerProfileToolStripMenuItem.Text = Resources.mnuRegisterPSN;
			this.deleteSaveToolStripMenuItem1.Text = Resources.mnuDeleteSave;
			bool flag2 = this.cbDrives.Items.Count > 0;
			if (flag2)
			{
				this.cbDrives.Items[this.cbDrives.Items.Count - 1] = Resources.colSelect;
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000853A8 File Offset: 0x000835A8
		private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			CultureInfo cultureInfo = this.cbLanguage.SelectedItem as CultureInfo;
			bool flag = cultureInfo == null;
			if (!flag)
			{
				Util.SetRegistryValue("Language", cultureInfo.Name);
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
				this.SetLabels();
				this.Refresh();
				this.btnHome.Invalidate();
				this.btnHelp.Invalidate();
				this.btnOptions.Invalidate();
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					this.btnHome.Image = Resources.home_gamelist_off;
					this.btnOptions.Image = Resources.home_settings_on;
					this.btnHelp.Image = Resources.home_help_off;
				}
				else
				{
					this.btnHome.BackgroundImage = Resources.home_gamelist_off;
					this.btnOptions.BackgroundImage = Resources.home_settings_on;
					this.btnHelp.BackgroundImage = Resources.home_help_off;
				}
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0008549C File Offset: 0x0008369C
		public static void FillLocalCheats(ref game item)
		{
			string text = Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm3.USER_CHEATS_FILE;
			bool flag = File.Exists(text);
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(text);
				}
				catch (Exception)
				{
					string text2 = File.ReadAllText(text);
					text2 = text2.Replace("&", "&amp;");
					try
					{
						xmlDocument.LoadXml(text2);
					}
					catch (Exception)
					{
					}
				}
				for (int i = 0; i < xmlDocument["usercheats"].ChildNodes.Count; i++)
				{
					container targetGameFolder = item.GetTargetGameFolder();
					bool flag2 = targetGameFolder != null && item.id + targetGameFolder.key == xmlDocument["usercheats"].ChildNodes[i].Attributes["id"].Value;
					if (flag2)
					{
						bool flag3 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes.Count > 0;
						if (flag3)
						{
							for (int j = 0; j < xmlDocument["usercheats"].ChildNodes[i].ChildNodes.Count; j++)
							{
								XmlNode xmlNode = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j];
								bool flag4 = (xmlNode as XmlElement).Name == "file";
								if (flag4)
								{
									XmlElement xmlElement = xmlNode as XmlElement;
									string attribute = xmlElement.GetAttribute("name");
									file gameFile = item.GetGameFile(targetGameFolder, attribute);
									bool flag5 = gameFile != null;
									if (flag5)
									{
										gameFile.ucfilename = attribute;
									}
									for (int k = 0; k < xmlElement.ChildNodes.Count; k++)
									{
										XmlNode xmlNode2 = xmlElement.ChildNodes[k];
										string value = xmlNode2.Attributes["desc"].Value;
										string value2 = xmlNode2.Attributes["comment"].Value;
										cheat cheat = new cheat("-1", value, value2);
										for (int l = 0; l < xmlNode2.ChildNodes.Count; l++)
										{
											string text3 = xmlNode2.ChildNodes[l].InnerText;
											text3 = text3.Replace("\r\n", " ").TrimEnd(new char[0]);
											text3 = text3.Replace("\n", " ").TrimEnd(new char[0]);
											string[] array = text3.Split(new char[] { ' ' });
											bool flag6 = array.Length % 2 == 0;
											if (flag6)
											{
												cheat.code = text3;
											}
										}
										bool flag7 = gameFile != null;
										if (flag7)
										{
											gameFile.Cheats.Add(cheat);
										}
									}
								}
								else
								{
									string value3 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].Attributes["desc"].Value;
									string value4 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].Attributes["comment"].Value;
									cheat cheat2 = new cheat("-1", value3, value4);
									for (int m = 0; m < xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].ChildNodes.Count; m++)
									{
										string text4 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].ChildNodes[m].InnerText;
										text4 = text4.Replace("\r\n", " ").TrimEnd(new char[0]);
										text4 = text4.Replace("\n", " ").TrimEnd(new char[0]);
										string[] array2 = text4.Split(new char[] { ' ' });
										bool flag8 = array2.Length == 2;
										if (flag8)
										{
											cheat2.code = text4;
										}
									}
									bool flag9 = !string.IsNullOrEmpty(cheat2.code);
									if (flag9)
									{
										bool flag10 = targetGameFolder != null;
										if (flag10)
										{
											targetGameFolder.files._files[0].Cheats.Add(cheat2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000859A8 File Offset: 0x00083BA8
		private void FillServerGamesList()
		{
			((ISupportInitialize)this.dgServerGames).BeginInit();
			this.dgServerGames.Rows.Clear();
			List<DataGridViewRow> list = new List<DataGridViewRow>();
			foreach (game game in this.m_games)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.dgServerGames);
				dataGridViewRow.Cells[1].Value = game.name;
				dataGridViewRow.Cells[2].Value = game.GetCheatCount();
				dataGridViewRow.Cells[3].Value = game.id;
				list.Add(dataGridViewRow);
			}
			this.dgServerGames.Rows.AddRange(list.ToArray());
			((ISupportInitialize)this.dgServerGames).EndInit();
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00085AA8 File Offset: 0x00083CA8
		private void FillUnavailableGames()
		{
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				string text = this.cbDrives.SelectedItem.ToString();
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					text = string.Format("/Volumes{0}", text);
				}
				else
				{
					bool flag3 = Util.CurrentPlatform == Util.Platform.Linux;
					if (flag3)
					{
						text = string.Format("/media/{0}{1}", Environment.UserName, text);
					}
				}
				string dataPath = Util.GetDataPath(text);
				bool flag4 = !Directory.Exists(dataPath);
				if (!flag4)
				{
					string[] directories = Directory.GetDirectories(dataPath);
					List<DataGridViewRow> list = new List<DataGridViewRow>();
					foreach (string text2 in directories)
					{
						string text3;
						bool flag5 = MainForm3.GetOnlineSaveIndex(this.m_games, text2, out text3) == -1;
						if (flag5)
						{
							string text4 = text2 + Path.DirectorySeparatorChar.ToString() + "PARAM.SFO";
							bool flag6 = File.Exists(text4);
							if (flag6)
							{
								DataGridViewRow dataGridViewRow = new DataGridViewRow();
								dataGridViewRow.CreateCells(this.dgServerGames);
								Color lightSlateGray = Color.LightSlateGray;
								dataGridViewRow.Cells[0].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[1].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[2].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[3].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[0].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[1].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[2].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[3].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[4].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[1].Value = this.GetSaveTitle(text4);
								dataGridViewRow.Cells[3].Value = Path.GetFileName(text2).Substring(0, 9);
								dataGridViewRow.Cells[0].Tag = dataGridViewRow.Cells[3].Value;
								dataGridViewRow.Cells[4].Value = "";
								dataGridViewRow.Tag = text2;
								list.Add(dataGridViewRow);
							}
						}
					}
					this.dgServerGames.Rows.AddRange(list.ToArray());
				}
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00085D5C File Offset: 0x00083F5C
		private void dgServerGames_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = e.Button == MouseButtons.Right;
				if (flag2)
				{
					this.dgServerGames.ClearSelection();
					this.dgServerGames.Rows[e.RowIndex].Selected = true;
				}
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00085DB8 File Offset: 0x00083FB8
		private void SortGames(int sortCol, bool bDesc)
		{
			bool flag = sortCol == 8;
			if (flag)
			{
				foreach (KeyValuePair<string, List<game>> keyValuePair in this.m_dictLocalSaves)
				{
					keyValuePair.Value.Sort((game item1, game item2) => item1.UpdatedTime.CompareTo(item2.UpdatedTime));
					if (bDesc)
					{
						keyValuePair.Value.Reverse();
					}
				}
			}
			this.m_games.Sort(delegate(game item1, game item2)
			{
				switch (sortCol)
				{
				case 2:
					return item1.GetCheatCount().CompareTo(item2.GetCheatCount());
				case 3:
					return item1.id.CompareTo(item2.id);
				case 7:
					return item1.acts.CompareTo(item2.acts);
				case 8:
					return this.TimeForComapre(item1).CompareTo(this.TimeForComapre(item2));
				}
				return (item1.name + item1.id).CompareTo(item2.name + item2.id);
			});
			if (bDesc)
			{
				this.m_games.Reverse();
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00085E98 File Offset: 0x00084098
		private void FillSavesList(string xml)
		{
			this.m_games = new List<game>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(games));
				using (StringReader stringReader = new StringReader(xml))
				{
					games games = (games)xmlSerializer.Deserialize(stringReader);
					this.m_games = games._games;
					this.m_rblist = games.rblist;
				}
			}
			catch (Exception)
			{
				try
				{
					xml = xml.Replace("&", "&amp;");
					XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(games));
					using (StringReader stringReader2 = new StringReader(xml))
					{
						games games2 = (games)xmlSerializer2.Deserialize(stringReader2);
						this.m_games = games2._games;
					}
				}
				catch (Exception)
				{
					return;
				}
			}
			this.m_games.Sort((game item1, game item2) => (item1.name + item1.LocalSaveFolder).CompareTo(item2.name + item1.LocalSaveFolder));
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00085FD4 File Offset: 0x000841D4
		private int GetPSNIDInfo()
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"PSNID_INFO\",\"userid\":\"{0}\"}}", Util.GetUserId())));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			int num;
			if (flag)
			{
				bool flag2 = dictionary.ContainsKey("psnid");
				if (flag2)
				{
					this.m_psnIDs = dictionary["psnid"] as Dictionary<string, object>;
				}
				else
				{
					this.m_psnIDs = new Dictionary<string, object>();
				}
				this.m_psn_quota = Convert.ToInt32(dictionary["psnid_quota"]);
				this.m_psn_remaining = Convert.ToInt32(dictionary["psnid_remaining"]);
				this.RefreshProfiles();
				num = this.m_psn_quota;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00086108 File Offset: 0x00084308
		private void RefreshProfiles()
		{
			this.gbProfiles.Controls.Clear();
			this.gbProfiles.Width = this.m_psn_quota * Util.ScaleSize(18) + Util.ScaleSize(35);
			for (int i = 0; i < this.m_psn_quota; i++)
			{
				PictureBox pictureBox = new PictureBox();
				bool flag = i < this.m_psn_quota - this.m_psn_remaining;
				if (flag)
				{
					pictureBox.Image = Resources.check;
				}
				else
				{
					pictureBox.Image = Resources.uncheck;
				}
				pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				pictureBox.Left = Util.ScaleSize(8) + i * Util.ScaleSize(18);
				pictureBox.Top = Util.ScaleSize(8);
				pictureBox.Size = Util.ScaleSize(new Size(13, 13));
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					pictureBox.MaximumSize = Util.ScaleSize(new Size(18, 35));
				}
				this.gbProfiles.Controls.Add(pictureBox);
			}
			TextBox textBox = new TextBox();
			textBox.Text = string.Format("{0}/{1}", this.m_psn_quota - this.m_psn_remaining, this.m_psn_quota);
			textBox.Left = this.m_psn_quota * Util.ScaleSize(18) + Util.ScaleSize(8);
			textBox.Top = Util.ScaleSize(9);
			textBox.Width = Util.ScaleSize(26);
			textBox.ForeColor = Color.White;
			textBox.BorderStyle = BorderStyle.None;
			textBox.BackColor = Color.FromArgb(102, 132, 162);
			this.gbProfiles.Controls.Add(textBox);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000862BC File Offset: 0x000844BC
		public bool IsValidPSNID(string psnId)
		{
			return this.m_psnIDs != null && this.m_psnIDs.ContainsKey(psnId);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000862F0 File Offset: 0x000844F0
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.chkShowAll.Checked || this.dgServerGames.SelectedCells.Count == 0 || this.cbDrives.Items.Count == 0;
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				this.advancedToolStripMenuItem.Visible = true;
				int rowIndex = this.dgServerGames.SelectedCells[1].RowIndex;
				bool flag2 = !(bool)this.dgServerGames.Rows[rowIndex].Cells[5].Value;
				if (flag2)
				{
					e.Cancel = true;
				}
				else
				{
					bool flag3 = (string)this.dgServerGames.Rows[rowIndex].Cells[1].Tag == "U";
					if (flag3)
					{
						this.registerPSNIDToolStripMenuItem.Visible = true;
						this.registerPSNIDToolStripMenuItem.Enabled = true;
						this.simpleToolStripMenuItem.Enabled = false;
						this.advancedToolStripMenuItem.Enabled = false;
						this.restoreFromBackupToolStripMenuItem.Enabled = false;
					}
					else
					{
						this.registerPSNIDToolStripMenuItem.Visible = false;
						this.registerPSNIDToolStripMenuItem.Enabled = false;
						this.restoreFromBackupToolStripMenuItem.Enabled = true;
						game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
						bool flag4 = game == null;
						if (flag4)
						{
							e.Cancel = true;
						}
						else
						{
							container targetGameFolder = game.GetTargetGameFolder();
							bool flag5 = targetGameFolder != null;
							if (flag5)
							{
								this.advancedToolStripMenuItem.Enabled = !(targetGameFolder.quickmode > 0);
								this.simpleToolStripMenuItem.Enabled = true;
							}
							else
							{
								this.simpleToolStripMenuItem.Enabled = false;
								this.advancedToolStripMenuItem.Enabled = false;
							}
							this.deleteSaveToolStripMenuItem.Visible = true;
							this.restoreFromBackupToolStripMenuItem.Visible = true;
							bool flag6 = MainForm3.GetSysVer(game.LocalSaveFolder) == "All";
							if (flag6)
							{
								this.advancedToolStripMenuItem.Enabled = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00086548 File Offset: 0x00084748
		private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
			bool flag = game == null || (game.PSN_ID != null && !this.IsValidPSNID(game.PSN_ID));
			if (!flag)
			{
				bool flag2 = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[2].Value as string == "N/A";
				if (!flag2)
				{
					List<string> list = null;
					bool flag3 = !this.chkShowAll.Checked;
					if (flag3)
					{
						list = game.GetContainerFiles();
						bool flag4 = list == null || list.Count < 2;
						if (flag4)
						{
							Util.ShowMessage(Resources.errNoFile, Resources.msgError);
							return;
						}
					}
					container targetGameFolder = game.GetTargetGameFolder();
					bool flag5 = targetGameFolder != null && targetGameFolder.locked > 0;
					if (flag5)
					{
						bool flag6 = Util.ShowMessage(Resources.errProfileLock, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
						if (flag6)
						{
							return;
						}
					}
					int rowIndex = this.dgServerGames.SelectedCells[0].RowIndex;
					List<string> list2 = new List<string>();
					bool flag7 = !this.chkShowAll.Checked;
					if (flag7)
					{
						string text = game.LocalSaveFolder.Substring(0, game.LocalSaveFolder.Length - 4);
						bool flag8 = !string.IsNullOrEmpty(game.notes);
						if (flag8)
						{
							Notes notes = new Notes(game.notes);
							notes.ShowDialog(this);
						}
						bool flag9 = targetGameFolder.preprocess == 1;
						if (flag9)
						{
							list.Remove(text);
							AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(list.ToArray(), game, null, "list");
							bool flag10 = advancedSaveUploaderForEncrypt.ShowDialog(this) != DialogResult.Abort && !string.IsNullOrEmpty(advancedSaveUploaderForEncrypt.ListResult);
							if (!flag10)
							{
								Util.ShowMessage(Resources.errInvalidSave);
								return;
							}
							ArrayList arrayList = new JavaScriptSerializer().Deserialize(advancedSaveUploaderForEncrypt.ListResult, typeof(ArrayList)) as ArrayList;
							foreach (object obj in arrayList)
							{
								list2.Add((string)obj);
							}
						}
					}
					bool flag11 = Util.IsUnixOrMacOSX();
					if (flag11)
					{
						SimpleEdit simpleEdit = new SimpleEdit(game, this.chkShowAll.Checked, list2);
						bool flag12 = simpleEdit.ShowDialog() == DialogResult.OK;
						if (flag12)
						{
							this.dgServerGames.Rows[rowIndex].Tag = simpleEdit.GameItem;
							this.dgServerGames.Rows[rowIndex].Cells[2].Value = simpleEdit.GameItem.GetCheatCount();
							this.PrepareLocalSavesMap();
							string expandedGame = this.m_expandedGame;
							this.m_expandedGame = null;
							int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.FillLocalSaves(expandedGame, this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
							this.dgServerGames.Rows[Math.Min(rowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
							try
							{
								this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
							}
							catch (Exception)
							{
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex2 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.cbDrives_SelectedIndexChanged(null, null);
							this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex2;
						}
					}
					else
					{
						SimpleTreeEdit simpleTreeEdit = new SimpleTreeEdit(game, this.chkShowAll.Checked, list2);
						bool flag13 = simpleTreeEdit.ShowDialog() == DialogResult.OK;
						if (flag13)
						{
							this.dgServerGames.Rows[rowIndex].Tag = simpleTreeEdit.GameItem;
							this.dgServerGames.Rows[rowIndex].Cells[2].Value = simpleTreeEdit.GameItem.GetCheatCount();
							this.PrepareLocalSavesMap();
							string expandedGame2 = this.m_expandedGame;
							this.m_expandedGame = null;
							int firstDisplayedScrollingRowIndex3 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.FillLocalSaves(expandedGame2, this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
							this.dgServerGames.Rows[Math.Min(rowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
							try
							{
								this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex3;
							}
							catch (Exception)
							{
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex4 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.cbDrives_SelectedIndexChanged(null, null);
							this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex4;
						}
					}
				}
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00086A98 File Offset: 0x00084C98
		private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedCells.Count == 0;
			if (!flag)
			{
				Util.ClearTemp();
				game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
				List<string> containerFiles = game.GetContainerFiles();
				bool flag2 = containerFiles.Count < 2;
				if (flag2)
				{
					Util.ShowMessage(Resources.errNoFile, Resources.msgError);
				}
				else
				{
					bool flag3 = game.GetTargetGameFolder().locked > 0;
					if (flag3)
					{
						bool flag4 = Util.ShowMessage(Resources.errProfileLock, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
						if (flag4)
						{
							return;
						}
					}
					string text = game.LocalSaveFolder.Substring(0, game.LocalSaveFolder.Length - 4);
					string text2 = game.ToString(new List<string>(), "decrypt");
					containerFiles.Remove(text);
					bool flag5 = !string.IsNullOrEmpty(game.notes);
					if (flag5)
					{
						Notes notes = new Notes(game.notes);
						notes.ShowDialog(this);
					}
					AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(containerFiles.ToArray(), game, null, "decrypt");
					bool flag6 = advancedSaveUploaderForEncrypt.ShowDialog() != DialogResult.Abort;
					if (flag6)
					{
						bool flag7 = advancedSaveUploaderForEncrypt.DecryptedSaveData != null && advancedSaveUploaderForEncrypt.DecryptedSaveData.Count > 0;
						if (flag7)
						{
							using (AdvancedEdit advancedEdit = new AdvancedEdit(game, advancedSaveUploaderForEncrypt.DecryptedSaveData))
							{
								bool flag8 = advancedEdit.ShowDialog(this) == DialogResult.OK;
								if (flag8)
								{
									this.cbDrives_SelectedIndexChanged(null, null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00086C74 File Offset: 0x00084E74
		private void ResizeColumns(bool showAllChecked)
		{
			int num = this.dgServerGames.Width;
			bool flag = num == 0;
			if (!flag)
			{
				this.dgServerGames.Columns[4].Visible = !showAllChecked;
				this.dgServerGames.Columns[8].Visible = !showAllChecked;
				if (showAllChecked)
				{
					this.dgServerGames.Columns[0].Visible = false;
					this.dgServerGames.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
				else
				{
					this.dgServerGames.Columns[0].Width = 25;
					num = (this.btnImport.Visible ? this.dgResign.Width : this.dgServerGames.Width) - 25;
					this.dgServerGames.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					this.dgServerGames.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
				this.dgResign.Columns[0].Width = 25;
				this.dgResign.Columns[1].Width = (((int)((float)num * 0.6f) >= 5) ? ((int)((float)num * 0.6f)) : 5);
				this.dgResign.Columns[2].Width = (((int)((float)num * 0.11f) >= 5) ? ((int)((float)num * 0.11f)) : 5);
				this.dgResign.Columns[3].Width = (((int)((float)num * 0.18f) >= 5) ? ((int)((float)num * 0.18f)) : 5);
				this.dgResign.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				this.dgServerGames.Columns[1].Width = (((int)((float)num * 0.6f) >= 5) ? ((int)((float)num * 0.6f)) : 5);
				this.dgServerGames.Columns[2].Width = (((int)((float)num * 0.11f) >= 5) ? ((int)((float)num * 0.11f)) : 5);
				this.dgServerGames.Columns[3].Visible = false;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00086EB8 File Offset: 0x000850B8
		private void cbDrives_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.dgResign.Rows.Clear();
				bool flag = this.cbDrives.SelectedItem == null;
				if (!flag)
				{
					this.ResizeColumns(this.chkShowAll.Checked);
					string text = string.Empty;
					string text2 = this.cbDrives.SelectedItem.ToString();
					bool flag2 = text2 == Resources.colSelect && !MainForm3.isFirstRunning && sender != null && ((ComboBox)sender).Focused;
					if (flag2)
					{
						FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
						bool flag3 = Util.IsUnixOrMacOSX();
						if (flag3)
						{
							folderBrowserDialog.Description = "Select cheats folder location";
						}
						else
						{
							folderBrowserDialog.Description = Resources.lblSelectCheatsFolder;
						}
						DialogResult dialogResult = folderBrowserDialog.ShowDialog();
						bool flag4 = dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes;
						if (!flag4)
						{
							bool flag5 = !this.chkShowAll.Checked && this.cbDrives.Items.Count < 2;
							if (flag5)
							{
								this.dgServerGames.Rows.Clear();
								this.ShowHideNoSavesPanels();
							}
							this.cbDrives.SelectedIndex = 0;
							return;
						}
						bool flag6 = string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath);
						if (flag6)
						{
							Util.ShowMessage(Resources.notSelected);
							this.cbDrives_SelectedIndexChanged(sender, e);
							return;
						}
						text = Path.GetFullPath(folderBrowserDialog.SelectedPath).Normalize();
						bool flag7 = !Util.IsPathToCheats(text);
						if (flag7)
						{
							Util.ShowMessage(Resources.msgWrongPath);
							this.cbDrives_SelectedIndexChanged(sender, e);
							return;
						}
						string shortPath = Util.GetShortPath(text);
						bool flag8 = !this.chkShowAll.Enabled;
						if (flag8)
						{
							this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = true));
							this.chkShowAll.Checked = false;
						}
						Util.SaveCheatsPathToRegistry(shortPath);
						this.drivesHelper.FillDrives();
						this.cbDrives.SelectedIndex = 0;
					}
					else
					{
						bool flag9 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text2);
						if (flag9)
						{
							text2 = string.Format("/Volumes{0}", text2);
						}
						else
						{
							bool flag10 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text2);
							if (flag10)
							{
								text2 = string.Format("/media/{0}{1}", Environment.UserName, text2);
							}
						}
						text = Util.GetDataPath(text2);
					}
					bool flag11 = !string.IsNullOrEmpty(text) && !text.StartsWith(Resources.colSelect);
					bool flag12 = (!Directory.Exists(text) || Directory.GetDirectories(text).Length == 0) && !flag11;
					if (flag12)
					{
						bool flag13 = !this.chkShowAll.Enabled || string.IsNullOrEmpty(text) || text.StartsWith(Resources.colSelect);
						if (flag13)
						{
							this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = true));
							this.chkShowAll.Checked = false;
						}
					}
					else
					{
						bool flag14 = !this.chkShowAll.Checked;
						if (flag14)
						{
							this.PrepareLocalSavesMap();
							this.FillLocalSaves(null, true);
							this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
						}
						else
						{
							this.chkShowAll_CheckedChanged(null, null);
						}
						this.FillResignSaves(null, true);
					}
					this.ShowHideNoSavesPanels();
				}
			}
			catch (Exception ex)
			{
				CustomMsgBox.Show(ex.Message);
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00087278 File Offset: 0x00085478
		private void ShowHideNoSavesPanels()
		{
			bool flag = this.dgServerGames.Rows.Count == 0 && !this.chkShowAll.Checked;
			if (flag)
			{
				this.pnlNoSaves.Visible = true;
				this.pnlNoSaves.Location = new Point(Util.ScaleSize(1), 0);
				this.pnlNoSaves.BringToFront();
			}
			else
			{
				this.pnlNoSaves.Visible = false;
				this.pnlNoSaves.Location = new Point(Util.ScaleSize(-9999), 0);
				this.pnlNoSaves.SendToBack();
			}
			bool flag2 = this.dgResign.Rows.Count == 0;
			if (flag2)
			{
				this.pnlNoSaves2.Visible = true;
				this.pnlNoSaves2.Location = new Point(Util.ScaleSize(1), 0);
				this.pnlNoSaves2.BringToFront();
			}
			else
			{
				this.pnlNoSaves2.Visible = false;
				this.pnlNoSaves2.Location = new Point(Util.ScaleSize(-9999), 0);
				this.pnlNoSaves2.SendToBack();
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0008739C File Offset: 0x0008559C
		private void FillResignSaves()
		{
			this.dgResign.Rows.Clear();
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				string text = this.cbDrives.SelectedItem.ToString();
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					text = string.Format("/Volumes{0}", text);
				}
				else
				{
					bool flag3 = Util.CurrentPlatform == Util.Platform.Linux;
					if (flag3)
					{
						text = string.Format("/media/{0}{1}", Environment.UserName, text);
					}
				}
				bool flag4 = !Directory.Exists(Util.GetDataPath(text));
				if (!flag4)
				{
					string[] directories = Directory.GetDirectories(Util.GetDataPath(text));
					foreach (string text2 in this.m_dictAllLocalSaves.Keys)
					{
					}
					((ISupportInitialize)this.dgResign).BeginInit();
					foreach (string text3 in directories)
					{
						string fileName = Path.GetFileName(text3);
						bool flag5 = this.IsValidPSNID(fileName);
						if (flag5)
						{
							string[] directories2 = Directory.GetDirectories(text3);
							foreach (string text4 in directories2)
							{
								string[] files = Directory.GetFiles(text4, "*.bin");
								foreach (string text5 in files)
								{
									bool flag6 = new FileInfo(text5).Length < 2048L;
									if (flag6)
									{
										string text6 = text5.Substring(0, text5.Length - 4);
										bool flag7 = File.Exists(text6);
										if (flag7)
										{
											string fileName2 = Path.GetFileName(text4);
											game game = new game
											{
												id = fileName2,
												containers = new containers
												{
													_containers = new List<container>
													{
														new container
														{
															pfs = Path.GetFileName(text6)
														}
													}
												},
												LocalSaveFolder = text5
											};
											string text7;
											int onlineSaveIndex = MainForm3.GetOnlineSaveIndex(this.m_games, text6, out text7);
											int num = this.dgResign.Rows.Add();
											this.dgResign.Rows[num].Tag = text4;
											this.dgResign.Rows[num].Cells[0].Tag = game;
											this.dgResign.Rows[num].Cells[0].Value = ((onlineSaveIndex >= 0) ? this.m_games[onlineSaveIndex].name : fileName2);
											this.dgResign.Rows[num].Cells[1].Value = this.GetPSNID(game);
										}
									}
								}
							}
						}
					}
					((ISupportInitialize)this.dgResign).EndInit();
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000876B0 File Offset: 0x000858B0
		public static int GetOnlineSaveIndex(List<game> games, string save, out string saveId)
		{
			string fileName = Path.GetFileName(Path.GetDirectoryName(save));
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(save);
			for (int i = 0; i < games.Count; i++)
			{
				saveId = games[i].id;
				bool flag = fileName.Equals(saveId) || games[i].IsAlias(fileName, out saveId);
				if (flag)
				{
					for (int j = 0; j < games[i].containers._containers.Count; j++)
					{
						bool flag2 = fileNameWithoutExtension == games[i].containers._containers[j].pfs || Util.IsMatch(fileNameWithoutExtension, games[i].containers._containers[j].pfs);
						if (flag2)
						{
							return i;
						}
					}
				}
			}
			saveId = null;
			return -1;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000877B0 File Offset: 0x000859B0
		private int GetOnlineSaveIndexByGameName(string gameName)
		{
			for (int i = 0; i < this.m_games.Count; i++)
			{
				bool flag = gameName.Equals(this.m_games[i].name);
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00087800 File Offset: 0x00085A00
		public static string GetParamInfo(string sfoFile, string item)
		{
			bool flag = !File.Exists(sfoFile);
			string text;
			if (flag)
			{
				text = "";
			}
			else
			{
				byte[] array = File.ReadAllBytes(sfoFile);
				int num = BitConverter.ToInt32(array, 8);
				int num2 = BitConverter.ToInt32(array, 12);
				int num3 = BitConverter.ToInt32(array, 16);
				int num4 = 16;
				for (int i = 0; i < num3; i++)
				{
					short num5 = BitConverter.ToInt16(array, i * num4 + 20);
					int num6 = BitConverter.ToInt32(array, i * num4 + 12 + 20);
					bool flag2 = Encoding.UTF8.GetString(array, num + (int)num5, item.Length) == item;
					if (flag2)
					{
						int j;
						for (j = 0; j < array.Length; j++)
						{
							bool flag3 = array[num2 + num6 + j] == 0;
							if (flag3)
							{
								break;
							}
						}
						return Encoding.UTF8.GetString(array, num2 + num6, j);
					}
				}
				text = "";
			}
			return text;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00087904 File Offset: 0x00085B04
		public static byte[] GetParamInfo(string sfoFile, out int profileId)
		{
			profileId = 0;
			bool flag = !File.Exists(sfoFile);
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				byte[] array2 = File.ReadAllBytes(sfoFile);
				int num = BitConverter.ToInt32(array2, 8);
				int num2 = BitConverter.ToInt32(array2, 12);
				int num3 = BitConverter.ToInt32(array2, 16);
				int num4 = 16;
				for (int i = 0; i < num3; i++)
				{
					short num5 = BitConverter.ToInt16(array2, i * num4 + 20);
					int num6 = BitConverter.ToInt32(array2, i * num4 + 12 + 20);
					bool flag2 = Encoding.UTF8.GetString(array2, num + (int)num5, 5) == "PARAM";
					if (flag2)
					{
						byte[] array3 = new byte[16];
						Array.Copy(array2, num2 + num6 + 28, array3, 0, 16);
						profileId = BitConverter.ToInt32(array2, num2 + num6 + 28 + 16);
						return array3;
					}
				}
				array = null;
			}
			return array;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000879F0 File Offset: 0x00085BF0
		private string GetSaveDescription(string sfoFile)
		{
			return MainForm3.GetParamInfo(sfoFile, "SUB_TITLE") + "\r\n" + MainForm3.GetParamInfo(sfoFile, "DETAIL");
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00087A24 File Offset: 0x00085C24
		private string GetSaveTitle(string sfoFile)
		{
			return MainForm3.GetParamInfo(sfoFile, "TITLE");
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00087A44 File Offset: 0x00085C44
		private void btnHome_Click(object sender, EventArgs e)
		{
			bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag)
			{
				this.pnlHome.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
				this.pnlBackup.Location = new Point(Util.ScaleSize(5000), Util.ScaleSize(5000));
			}
			else
			{
				this.pnlHome.Visible = true;
				this.pnlBackup.Visible = false;
				bool flag2 = this.cbDrives.SelectedItem == null;
				if (flag2)
				{
					this.ResizeColumns(this.chkShowAll.Checked);
				}
			}
			bool flag3 = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag3)
			{
				this.btnHome.Image = Resources.home_gamelist_on;
				this.btnOptions.Image = Resources.home_settings_off;
				this.btnHelp.Image = Resources.home_help_off;
			}
			else
			{
				this.btnHome.BackgroundImage = Resources.home_gamelist_on;
				this.btnOptions.BackgroundImage = Resources.home_settings_off;
				this.btnHelp.BackgroundImage = Resources.home_help_off;
			}
			bool flag4 = sender != null;
			if (flag4)
			{
				this.cbDrives_SelectedIndexChanged(null, null);
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00087B72 File Offset: 0x00085D72
		private void btnSaves_Click(object sender, EventArgs e)
		{
			this.pnlHome.Visible = false;
			this.pnlBackup.Visible = false;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00087B90 File Offset: 0x00085D90
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				folderBrowserDialog.Description = "Select Backup Folder Location";
			}
			else
			{
				folderBrowserDialog.Description = Resources.lblSelectFolder;
			}
			DialogResult dialogResult = folderBrowserDialog.ShowDialog();
			bool flag2 = dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes;
			if (flag2)
			{
				bool flag3 = string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath);
				if (flag3)
				{
					Util.ShowMessage(Resources.notSelected);
					this.btnBrowse_Click(sender, e);
				}
				else
				{
					string fullPath = Path.GetFullPath(folderBrowserDialog.SelectedPath);
					DriveInfo[] drives = DriveInfo.GetDrives();
					foreach (DriveInfo driveInfo in drives)
					{
						bool flag4 = !fullPath.Contains(driveInfo.Name);
						if (!flag4)
						{
							bool flag5 = Util.IsUnixOrMacOSX();
							bool flag6;
							if (flag5)
							{
								flag6 = driveInfo.Name.Contains("media") || driveInfo.Name.Contains("Volumes");
								flag6 = driveInfo.IsReady && (driveInfo.DriveType == DriveType.Removable || flag6);
							}
							else
							{
								flag6 = driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable;
							}
							bool flag7 = flag6;
							if (flag7)
							{
								Util.ShowMessage(Resources.doNotUseRemovable);
								this.btnBrowse_Click(sender, e);
								return;
							}
						}
					}
					this.txtBackupLocation.Text = folderBrowserDialog.SelectedPath;
					this.btnApply_Click(null, null);
				}
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00087D14 File Offset: 0x00085F14
		private void chkBackup_CheckedChanged(object sender, EventArgs e)
		{
			this.txtBackupLocation.Enabled = this.chkBackup.Checked;
			this.btnBrowse.Enabled = this.chkBackup.Checked;
			Util.SetRegistryValue("BackupSaves", this.chkBackup.Checked ? "true" : "false");
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00087D74 File Offset: 0x00085F74
		private void btnBackup_Click(object sender, EventArgs e)
		{
			bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag)
			{
				this.pnlBackup.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
				this.pnlHome.Location = new Point(5000, 5000);
			}
			else
			{
				this.pnlBackup.Visible = true;
				this.pnlHome.Visible = false;
			}
			bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag2)
			{
				this.btnOptions.Image = Resources.home_settings_on;
				this.btnHome.Image = Resources.home_gamelist_off;
				this.btnHelp.Image = Resources.home_help_off;
			}
			else
			{
				this.btnOptions.BackgroundImage = Resources.home_settings_on;
				this.btnHome.BackgroundImage = Resources.home_gamelist_off;
				this.btnHelp.BackgroundImage = Resources.home_help_off;
			}
			this.chkBackup.Checked = Util.GetRegistryValue("BackupSaves") != "false";
			this.txtBackupLocation.Text = Util.GetBackupLocation();
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00087E96 File Offset: 0x00086096
		private void btnApply_Click(object sender, EventArgs e)
		{
			Util.SetRegistryValue("Location", this.txtBackupLocation.Text);
			Util.SetRegistryValue("BackupSaves", this.chkBackup.Checked ? "true" : "false");
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00087ED4 File Offset: 0x000860D4
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				bool flag = this.evt == null;
				if (!flag)
				{
					this.evt.Set();
					this.evt2.Set();
					Directory.Delete(Util.GetTempFolder(), true);
					bool sessionInited = this.m_sessionInited;
					if (sessionInited)
					{
						try
						{
							WebClientEx webClientEx = new WebClientEx();
							webClientEx.Credentials = Util.GetNetworkCredential();
							webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
							webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth?token=" + Util.GetAuthToken(), Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"DESTROY_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), Util.GetUID(false, false))));
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00087FB4 File Offset: 0x000861B4
		private void SaveUserCheats()
		{
			string text = "<usercheats>";
			foreach (object obj in ((IEnumerable)this.dgServerGames.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = dataGridViewRow.Tag != null;
				if (flag)
				{
					game game = dataGridViewRow.Tag as game;
					bool flag2 = game == null;
					if (!flag2)
					{
						bool flag3 = game.GetTargetGameFolder() == null;
						if (!flag3)
						{
							text += string.Format("<game id=\"{0}\">", Path.GetFileName(game.LocalSaveFolder));
							foreach (cheat cheat in game.GetTargetGameFolder().files._files[0].Cheats)
							{
								bool flag4 = cheat.id == "-1";
								if (flag4)
								{
									text = string.Concat(new string[] { text, "<cheat desc=\"", cheat.name, "\" comment=\"", cheat.note, "\">" });
									text += cheat.ToString(false);
									text += "</cheat>";
								}
							}
							text += "</game>";
						}
					}
				}
			}
			text += "</usercheats>";
			File.WriteAllText(Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm3.USER_CHEATS_FILE, text);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000881A0 File Offset: 0x000863A0
		private bool CheckForVersion()
		{
			return true;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000881B4 File Offset: 0x000863B4
		private void btnRss_Click(object sender, EventArgs e)
		{
			try
			{
				string text = GameListDownloader.RSS_URL;
				bool flag = !Util.IsHyperkin();
				if (flag)
				{
					text = string.Format("{0}/ps4/rss?token={1}", Util.GetBaseUrl(), Util.GetAuthToken());
				}
				RssFeed rssFeed = RssFeed.Read(text);
				RssChannel rssChannel = rssFeed.Channels[0];
				RSSForm rssform = new RSSForm(rssChannel);
				rssform.ShowDialog();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0008822C File Offset: 0x0008642C
		private void btnDiagnostic_Click(object sender, EventArgs e)
		{
			try
			{
				using (DiagnosticForm diagnosticForm = new DiagnosticForm())
				{
					diagnosticForm.ShowDialog(this);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0008827C File Offset: 0x0008647C
		private void restoreFromBackupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedRows.Count == 1;
			if (flag)
			{
				game game = this.dgServerGames.SelectedRows[0].Tag as game;
				string text = string.Concat(new string[]
				{
					game.PSN_ID,
					"_",
					Path.GetFileName(Path.GetDirectoryName(game.LocalSaveFolder)),
					"_",
					Path.GetFileNameWithoutExtension(game.LocalSaveFolder),
					"_*"
				});
				string[] files = Directory.GetFiles(Util.GetBackupLocation(), text);
				bool flag2 = files.Length == 1;
				if (flag2)
				{
					RestoreBackup restoreBackup = new RestoreBackup(files[0], Path.GetDirectoryName(game.LocalSaveFolder));
					restoreBackup.ShowDialog();
					Util.ShowMessage(Resources.msgRestored);
				}
				else
				{
					bool flag3 = files.Length == 0;
					if (flag3)
					{
						Util.ShowMessage(Resources.errNoBackup);
					}
					else
					{
						ChooseBackup chooseBackup = new ChooseBackup(game.name, game.PSN_ID + "_" + Path.GetFileName(Path.GetDirectoryName(game.LocalSaveFolder)) + "_", game.LocalSaveFolder);
						chooseBackup.ShowDialog();
					}
				}
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000883B0 File Offset: 0x000865B0
		private void btnDeactivate_Click(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(string.Format(Resources.msgDeactivate, Util.PRODUCT_NAME), Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.DeactivateLicense();
				if (flag3)
				{
					Util.ShowMessage(string.Format(Resources.msgDeactivated, Util.PRODUCT_NAME), Resources.msgInfo);
					this.m_sessionInited = false;
					Application.Restart();
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00088420 File Offset: 0x00086620
		private bool DeactivateLicense()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
				byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"UNREGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), Util.GetUID(false, false))));
				string @string = Encoding.ASCII.GetString(array);
				Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag = (string)dictionary["status"] == "OK";
				if (flag)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase(), true);
					string[] valueNames = registryKey.GetValueNames();
					try
					{
						foreach (string text in valueNames)
						{
							bool flag2 = text == "Location";
							if (!flag2)
							{
								registryKey.DeleteValue(text);
							}
						}
					}
					catch (Exception)
					{
					}
					finally
					{
						registryKey.Close();
						currentUser.Close();
					}
					Util.DeleteRegistryValue("User");
					return true;
				}
				bool flag3 = dictionary.ContainsKey("code");
				if (flag3)
				{
					Util.ShowErrorMessage(dictionary, Resources.errOffline);
				}
				else
				{
					Util.ShowMessage(Resources.errOffline, Resources.msgError);
				}
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Resources.errConnection, Resources.msgError);
			}
			return false;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00088600 File Offset: 0x00086800
		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			bool flag = Util.CurrentPlatform == Util.Platform.Linux;
			if (!flag)
			{
				Process.Start("file://" + this.txtBackupLocation.Text);
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00088638 File Offset: 0x00086838
		private void btnHelp_Click(object sender, EventArgs e)
		{
			bool flag = Util.CurrentPlatform == Util.Platform.Linux;
			if (!flag)
			{
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				string text = (Util.IsHyperkin() ? "http://www.thesavewizard.com/manual.php" : "http://www.savewizard.net/manuals/swps4m/");
				ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					UseShellExecute = true,
					Verb = "open",
					FileName = text
				};
				Process.Start(processStartInfo);
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000021C5 File Offset: 0x000003C5
		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000886A4 File Offset: 0x000868A4
		private string FindGGUSB()
		{
			ManagementScope managementScope = new ManagementScope("root\\cimv2");
			WqlObjectQuery wqlObjectQuery = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive where Model = 'dpdev GameGenie USB Device'");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, wqlObjectQuery);
			ManagementBaseObject[] array = new ManagementBaseObject[1];
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			bool flag = managementObjectCollection.Count > 0;
			if (flag)
			{
				managementObjectCollection.CopyTo(array, 0);
				string text = (string)array[0].Properties["DeviceID"].Value;
				text = text.Replace("\\\\", "\\\\\\\\");
				text = text.Replace(".\\", ".\\\\");
				string text2 = array[0].Properties["PNPDeviceID"].Value.ToString();
				string[] array2 = text2.Split(new char[] { '\\', '&' });
				string text3 = "ASSOCIATORS OF {Win32_DiskDrive.DeviceID=\"" + text + "\"} WHERE AssocClass = Win32_DiskDriveToDiskPartition";
				WqlObjectQuery wqlObjectQuery2 = new WqlObjectQuery(text3);
				ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher(managementScope, wqlObjectQuery2);
				managementObjectCollection = managementObjectSearcher2.Get();
				bool flag2 = managementObjectCollection.Count == 1;
				if (flag2)
				{
					managementObjectCollection.CopyTo(array, 0);
					text = (string)array[0].Properties["DeviceID"].Value;
					WqlObjectQuery wqlObjectQuery3 = new WqlObjectQuery("ASSOCIATORS OF {Win32_DiskPartition.DeviceID=\"" + text + "\"} WHERE AssocClass = Win32_LogicalDiskToPartition");
					ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher(managementScope, wqlObjectQuery3);
					managementObjectCollection = managementObjectSearcher3.Get();
					bool flag3 = managementObjectCollection.Count == 1;
					if (flag3)
					{
						managementObjectCollection.CopyTo(array, 0);
						string text4 = (string)array[0].Properties["DeviceID"].Value;
						managementObjectSearcher3.Dispose();
						managementObjectSearcher2.Dispose();
						managementObjectSearcher.Dispose();
						return array2[5];
					}
					managementObjectSearcher3.Dispose();
				}
				managementObjectSearcher2.Dispose();
			}
			managementObjectSearcher.Dispose();
			return null;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00088888 File Offset: 0x00086A88
		private void deleteSaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].Value as string;
			string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
			game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
			bool flag = game == null;
			string text2;
			if (flag)
			{
				text2 = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as string;
			}
			else
			{
				text2 = game.LocalSaveFolder;
			}
			bool flag2 = text2 == null;
			if (!flag2)
			{
				bool flag3 = Util.ShowMessage(Resources.msgConfirmDeleteSave, this.Text, MessageBoxButtons.YesNo) == DialogResult.No;
				if (!flag3)
				{
					try
					{
						File.Delete(text2);
						File.Delete(text2.Substring(0, game.LocalSaveFolder.Length - 4));
						string directoryName = Path.GetDirectoryName(text2);
						bool flag4 = Directory.GetFiles(directoryName).Length == 0;
						if (flag4)
						{
							Directory.Delete(directoryName, true);
							string fullName = Directory.GetParent(directoryName).FullName;
							bool flag5 = Directory.GetFileSystemEntries(fullName).Length == 0;
							if (flag5)
							{
								Directory.Delete(fullName);
							}
						}
					}
					catch (Exception)
					{
						Util.ShowMessage(Resources.errDelete, Resources.msgError);
					}
					int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
					this.cbDrives_SelectedIndexChanged(null, null);
					bool flag6 = this.dgServerGames.Rows.Count > firstDisplayedScrollingRowIndex && firstDisplayedScrollingRowIndex >= 0;
					if (flag6)
					{
						this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00088A94 File Offset: 0x00086C94
		private void chkBackup_Click(object sender, EventArgs e)
		{
			bool flag = !this.chkBackup.Checked;
			if (flag)
			{
				bool flag2 = Util.ShowMessage(Resources.msgConfirmBackup, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
				bool flag3 = flag2;
				if (flag3)
				{
					this.chkBackup.Checked = true;
				}
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00088AE4 File Offset: 0x00086CE4
		private void btnManageProfiles_Click(object sender, EventArgs e)
		{
			ManageProfiles manageProfiles = new ManageProfiles("", this.m_psnIDs);
			manageProfiles.ShowDialog();
			bool flag = !string.IsNullOrEmpty(manageProfiles.PsnIDResponse);
			if (flag)
			{
				Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(manageProfiles.PsnIDResponse, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag2 = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
				if (flag2)
				{
					bool flag3 = dictionary.ContainsKey("psnid");
					if (flag3)
					{
						this.m_psnIDs = dictionary["psnid"] as Dictionary<string, object>;
					}
					else
					{
						this.m_psnIDs = new Dictionary<string, object>();
					}
					this.m_psn_quota = Convert.ToInt32(dictionary["psnid_quota"]);
					this.m_psn_remaining = Convert.ToInt32(dictionary["psnid_remaining"]);
				}
			}
			this.RefreshProfiles();
			this.cbDrives_SelectedIndexChanged(null, null);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00088BE4 File Offset: 0x00086DE4
		private void registerPSNIDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.m_psnIDs.Count >= this.m_psn_quota || this.m_psn_remaining <= 0;
			if (flag)
			{
				Util.ShowMessage(Resources.errMaxProfiles, Resources.msgInfo);
			}
			else
			{
				bool flag2 = this.dgServerGames.SelectedRows.Count == 1;
				if (flag2)
				{
					game game = this.dgServerGames.SelectedRows[0].Tag as game;
					ManageProfiles manageProfiles = new ManageProfiles(game.PSN_ID, this.m_psnIDs);
					bool flag3 = manageProfiles.ShowDialog() == DialogResult.OK;
					if (flag3)
					{
						bool flag4 = !string.IsNullOrEmpty(manageProfiles.PsnIDResponse);
						if (flag4)
						{
							Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(manageProfiles.PsnIDResponse, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
							bool flag5 = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
							if (flag5)
							{
								bool flag6 = dictionary.ContainsKey("psnid");
								if (flag6)
								{
									this.m_psnIDs = dictionary["psnid"] as Dictionary<string, object>;
								}
								else
								{
									this.m_psnIDs = new Dictionary<string, object>();
								}
								this.m_psn_quota = Convert.ToInt32(dictionary["psnid_quota"]);
								this.m_psn_remaining = Convert.ToInt32(dictionary["psnid_remaining"]);
							}
						}
						this.RefreshProfiles();
						this.cbDrives_SelectedIndexChanged(null, null);
					}
				}
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00088D70 File Offset: 0x00086F70
		private void resignToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedCells.Count == 0;
			if (!flag)
			{
				game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
				string text = ((game == null) ? (this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as string) : game.LocalSaveFolder);
				bool flag2 = text == null;
				if (!flag2)
				{
					this.cbDrives_SelectedIndexChanged(null, null);
				}
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00088E1C File Offset: 0x0008701C
		private bool RegisterSerial()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				string registryValue = Util.GetRegistryValue("Serial");
				string text = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(registryValue)));
				text = text.Replace("-", "");
				string uid = Util.GetUID(false, true);
				bool flag = string.IsNullOrEmpty(uid);
				if (flag)
				{
					Util.ShowMessage(Resources.errContactSupport);
					return false;
				}
				string text2 = string.Format("{0}/ps4auth", Util.GetBaseUrl(), uid, text);
				string text3 = webClientEx.DownloadString(new Uri(text2, UriKind.Absolute));
				bool flag2 = text3.IndexOf('#') > 0;
				if (flag2)
				{
					string[] array = text3.Split(new char[] { '#' });
					bool flag3 = array.Length > 1;
					if (flag3)
					{
						bool flag4 = array[0] == "4";
						if (flag4)
						{
							Util.ShowMessage(Resources.errInvalidSerial, Resources.msgError);
							return false;
						}
						bool flag5 = array[0] == "5";
						if (flag5)
						{
							Util.ShowMessage(Resources.errTooManyTimes, Resources.msgError);
							return false;
						}
					}
				}
				else
				{
					bool flag6 = text3 == null || text3.ToLower().Contains("error") || text3.ToLower().Contains("not found");
					if (flag6)
					{
						string text4 = text3.Replace("ERROR", "");
						bool flag7 = text4.Contains("1007");
						if (flag7)
						{
							Util.GetUID(true, true);
							return this.RegisterSerial();
						}
						bool flag8 = text4.Contains("1004");
						if (flag8)
						{
							Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME) + text4, Resources.msgError);
							return false;
						}
						bool flag9 = text4.Contains("1005");
						if (flag9)
						{
							Util.ShowMessage(Resources.errTooManyTimes + text4, Resources.msgError);
							return false;
						}
						Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME) + text4, Resources.msgError);
						return false;
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Util.ShowMessage(ex.Message, ex.StackTrace);
				Util.ShowMessage(Resources.errSerial, Resources.msgError);
			}
			Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME), Resources.msgError);
			return false;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000890C8 File Offset: 0x000872C8
		private void registerProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.m_psnIDs.Count >= this.m_psn_quota || this.m_psn_remaining <= 0;
			if (flag)
			{
				Util.ShowMessage(Resources.errMaxProfiles, Resources.msgInfo);
			}
			else
			{
				bool flag2 = this.dgResign.SelectedRows.Count == 1;
				if (flag2)
				{
					game game = this.dgResign.SelectedRows[0].Tag as game;
					ManageProfiles manageProfiles = new ManageProfiles(game.PSN_ID, this.m_psnIDs);
					bool flag3 = manageProfiles.ShowDialog() == DialogResult.OK;
					if (flag3)
					{
						bool flag4 = !string.IsNullOrEmpty(manageProfiles.PsnIDResponse);
						if (flag4)
						{
							Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(manageProfiles.PsnIDResponse, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
							bool flag5 = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
							if (flag5)
							{
								bool flag6 = dictionary.ContainsKey("psnid");
								if (flag6)
								{
									this.m_psnIDs = dictionary["psnid"] as Dictionary<string, object>;
								}
								else
								{
									this.m_psnIDs = new Dictionary<string, object>();
								}
								this.m_psn_quota = Convert.ToInt32(dictionary["psnid_quota"]);
								this.m_psn_remaining = Convert.ToInt32(dictionary["psnid_remaining"]);
							}
						}
						this.RefreshProfiles();
						this.cbDrives_SelectedIndexChanged(null, null);
					}
				}
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00089254 File Offset: 0x00087454
		private void deleteSaveToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			game game = this.dgResign.Rows[this.dgResign.SelectedCells[0].RowIndex].Tag as game;
			string text = ((game == null) ? (this.dgResign.Rows[this.dgResign.SelectedCells[0].RowIndex].Tag as string) : game.LocalSaveFolder);
			bool flag = text == null;
			if (!flag)
			{
				bool flag2 = Util.ShowMessage(Resources.msgConfirmDeleteSave, this.Text, MessageBoxButtons.YesNo) == DialogResult.No;
				if (!flag2)
				{
					try
					{
						File.Delete(text);
						File.Delete(text.Substring(0, game.LocalSaveFolder.Length - 4));
					}
					catch (Exception)
					{
						Util.ShowMessage(Resources.errDelete, Resources.msgError);
					}
					int firstDisplayedScrollingRowIndex = this.dgResign.FirstDisplayedScrollingRowIndex;
					this.cbDrives_SelectedIndexChanged(null, null);
					bool flag3 = this.dgResign.Rows.Count > firstDisplayedScrollingRowIndex && firstDisplayedScrollingRowIndex >= 0;
					if (flag3)
					{
						this.dgResign.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00089390 File Offset: 0x00087590
		private void dgServerGames_Click(object sender, EventArgs e)
		{
			this.dgServerGames.Focus();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0008939F File Offset: 0x0008759F
		private void dgResign_Click(object sender, EventArgs e)
		{
			this.dgResign.Focus();
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000893B0 File Offset: 0x000875B0
		public int DropDownWidth(ComboBox myCombo)
		{
			int num = 40;
			foreach (object obj in myCombo.Items)
			{
				int width = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
				bool flag = width > num;
				if (flag)
				{
					num = width;
				}
			}
			return num;
		}

		// Token: 0x04000B3F RID: 2879
		private Dictionary<string, List<game>> m_dictLocalSaves = new Dictionary<string, List<game>>();

		// Token: 0x04000B40 RID: 2880
		private Dictionary<string, List<game>> m_dictAllLocalSaves = new Dictionary<string, List<game>>();

		// Token: 0x04000B41 RID: 2881
		private string m_expandedGame = null;

		// Token: 0x04000B42 RID: 2882
		private string m_expandedGameResign = null;

		// Token: 0x04000B43 RID: 2883
		internal const int WM_DEVICECHANGE = 537;

		// Token: 0x04000B44 RID: 2884
		public const int WM_SYSCOMMAND = 274;

		// Token: 0x04000B45 RID: 2885
		public const int MF_SEPARATOR = 2048;

		// Token: 0x04000B46 RID: 2886
		public const int MF_BYPOSITION = 1024;

		// Token: 0x04000B47 RID: 2887
		public const int MF_STRING = 0;

		// Token: 0x04000B48 RID: 2888
		public const int IDM_ABOUT = 1000;

		// Token: 0x04000B49 RID: 2889
		private Dictionary<int, string> RegionMap;

		// Token: 0x04000B4A RID: 2890
		private const string UNREGISTER_UUID = "{{\"action\":\"UNREGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000B4B RID: 2891
		private const string DESTROY_SESSION = "{{\"action\":\"DESTROY_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000B4C RID: 2892
		private const string SESSION_INIT_URL = "{{\"action\":\"START_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000B4D RID: 2893
		private const string PSNID_INFO = "{{\"action\":\"PSNID_INFO\",\"userid\":\"{0}\"}}";

		// Token: 0x04000B4E RID: 2894
		private const string SESSION_CLOSAL = "{0}/?q=software_auth2/sessionclose&sessionid={1}";

		// Token: 0x04000B4F RID: 2895
		private const int INTERNAL_VERION_MAJOR = 1;

		// Token: 0x04000B50 RID: 2896
		private const int INTERNAL_VERION_MINOR = 0;

		// Token: 0x04000B51 RID: 2897
		private const int PID_JP_FULL_SINGLE = 2;

		// Token: 0x04000B52 RID: 2898
		private const int PID_JP_FULL_MULTI = 3;

		// Token: 0x04000B53 RID: 2899
		private const int PID_CYBER_ZERO_ENG = 4;

		// Token: 0x04000B54 RID: 2900
		private const int PID_SWPS4MAX = 7;

		// Token: 0x04000B55 RID: 2901
		private const int PID_SE_JP_AMAZON_MULTI = 8;

		// Token: 0x04000B56 RID: 2902
		private const int PID_SE_JP_AMAZON_SINGLE = 9;

		// Token: 0x04000B57 RID: 2903
		private const int PID_JP_FULL_SINGLE_DIRECT = 10;

		// Token: 0x04000B58 RID: 2904
		private const int PID_JP_FULL_MULTI_DIRECT = 11;

		// Token: 0x04000B59 RID: 2905
		private const int PID_AMAZON_SGE1 = 13;

		// Token: 0x04000B5A RID: 2906
		private const int PID_CYBER_SGE1 = 15;

		// Token: 0x04000B5B RID: 2907
		private const int PID_SWPS4US_AMAZON = 16;

		// Token: 0x04000B5C RID: 2908
		private const int PID_SWPS4US_PAYPAL = 18;

		// Token: 0x04000B5D RID: 2909
		private const int PID_CYBER_ZERO_JP = 20;

		// Token: 0x04000B5E RID: 2910
		private const int PID_SWPS4US_RETAIL = 22;

		// Token: 0x04000B5F RID: 2911
		private const int PID_CYBER_FULL_ENG = 24;

		// Token: 0x04000B60 RID: 2912
		private MainForm3.GetTrafficDelegate GetTrafficFunc;

		// Token: 0x04000B61 RID: 2913
		private List<game> m_games;

		// Token: 0x04000B62 RID: 2914
		private rblsit m_rblist;

		// Token: 0x04000B63 RID: 2915
		private DrivesHelper drivesHelper;

		// Token: 0x04000B64 RID: 2916
		private FormWindowState previousState = FormWindowState.Normal;

		// Token: 0x04000B65 RID: 2917
		public static string USER_CHEATS_FILE = "swusercheats.xml";

		// Token: 0x04000B66 RID: 2918
		private int previousDriveNum = 0;

		// Token: 0x04000B67 RID: 2919
		private bool isRunning = false;

		// Token: 0x04000B68 RID: 2920
		private bool m_bSerialChecked = false;

		// Token: 0x04000B69 RID: 2921
		private bool m_sessionInited = false;

		// Token: 0x04000B6A RID: 2922
		private AutoResetEvent evt;

		// Token: 0x04000B6B RID: 2923
		private AutoResetEvent evt2;

		// Token: 0x04000B6C RID: 2924
		private Dictionary<string, object> m_psnIDs = null;

		// Token: 0x04000B6D RID: 2925
		private int m_psn_quota = 0;

		// Token: 0x04000B6E RID: 2926
		private int m_psn_remaining = 0;

		// Token: 0x04000B6F RID: 2927
		private static bool isFirstRunning = true;

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06001E3A RID: 7738
		private delegate void GetTrafficDelegate();

		// Token: 0x0200029A RID: 666
		public struct DEV_BROADCAST_HDR
		{
			// Token: 0x04000FDD RID: 4061
			public uint dbch_Size;

			// Token: 0x04000FDE RID: 4062
			public uint dbch_DeviceType;

			// Token: 0x04000FDF RID: 4063
			public uint dbch_Reserved;
		}

		// Token: 0x0200029B RID: 667
		public struct DEV_BROADCAST_VOLUME
		{
			// Token: 0x04000FE0 RID: 4064
			public uint dbch_Size;

			// Token: 0x04000FE1 RID: 4065
			public uint dbch_DeviceType;

			// Token: 0x04000FE2 RID: 4066
			public uint dbch_Reserved;

			// Token: 0x04000FE3 RID: 4067
			public uint dbcv_unitmask;

			// Token: 0x04000FE4 RID: 4068
			public ushort dbcv_flags;
		}
	}
}
