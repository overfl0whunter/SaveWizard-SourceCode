using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001C5 RID: 453
	public partial class ChooseBackup : Form
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x000726F8 File Offset: 0x000708F8
		public ChooseBackup(string gameName, string save, string saveFolder)
		{
			this.m_save = save;
			this.m_saveFolder = saveFolder;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblGameName.BackColor = Color.Transparent;
			this.lblGameName.ForeColor = Color.White;
			base.CenterToScreen();
			this.deleteToolStripMenuItem.Text = Resources.mnuDelete;
			this.lblGameName.Text = gameName;
			this.btnRestore.Text = Resources.btnRestore;
			this.btnCancel.Text = Resources.btnCancel;
			this.Text = Resources.titleChooseBackup;
			this.LoadBackups();
			this.lstBackups.DisplayMember = "Timestamp";
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000727EC File Offset: 0x000709EC
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

		// Token: 0x0600172F RID: 5935 RVA: 0x00072884 File Offset: 0x00070A84
		private void LoadBackups()
		{
			List<BackupItem> list = new List<BackupItem>();
			string backupLocation = Util.GetBackupLocation();
			string[] files = Directory.GetFiles(backupLocation, this.m_save + "*");
			foreach (string text in files)
			{
				string fileName = Path.GetFileName(text);
				int num = fileName.LastIndexOf('.');
				bool flag = num > 19;
				if (flag)
				{
					list.Add(new BackupItem
					{
						BackupFile = text,
						Timestamp = fileName.Substring(num - 19, 19)
					});
				}
			}
			this.lstBackups.DataSource = list;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00072934 File Offset: 0x00070B34
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.lstBackups.SelectedItem == null;
			if (!flag)
			{
				string backupFile = (this.lstBackups.SelectedItem as BackupItem).BackupFile;
				File.Delete(backupFile);
				this.LoadBackups();
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0007297C File Offset: 0x00070B7C
		private void btnRestore_Click(object sender, EventArgs e)
		{
			bool flag = this.lstBackups.SelectedItem == null;
			if (!flag)
			{
				string backupFile = (this.lstBackups.SelectedItem as BackupItem).BackupFile;
				bool flag2 = Util.ShowMessage(Resources.warnRestore, this.Text, MessageBoxButtons.YesNo) == DialogResult.No;
				if (!flag2)
				{
					try
					{
						RestoreBackup restoreBackup = new RestoreBackup(backupFile, Path.GetDirectoryName(this.m_saveFolder));
						restoreBackup.ShowDialog();
						Util.ShowMessage(Resources.msgRestored);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000AAF RID: 2735
		private string m_saveFolder;

		// Token: 0x04000AB0 RID: 2736
		private string m_save;
	}
}
