using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001DF RID: 479
	public partial class RestoreBackup : Form
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x00093DE0 File Offset: 0x00091FE0
		public RestoreBackup(string backupFile, string destFolder)
		{
			this.m_backupFile = backupFile;
			this.m_destFolder = destFolder;
			this.UpdateProgress = new RestoreBackup.UpdateProgressDelegate(this.UpdateProgressSafe);
			this.CloseForm = new RestoreBackup.CloseDelegate(this.CloseFormSafe);
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblProgress.BackColor = Color.Transparent;
			this.lblProgress.ForeColor = Color.White;
			this.lblProgress.Text = Resources.lblRestoring;
			base.CenterToScreen();
			base.Load += this.RestoreBackup_Load;
			base.Activated += this.RestoreBackup_Activated;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00093ED0 File Offset: 0x000920D0
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00093F3C File Offset: 0x0009213C
		private void RestoreBackup_Activated(object sender, EventArgs e)
		{
			bool flag = !this.m_bActivated;
			if (flag)
			{
				this.m_bActivated = true;
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00093F60 File Offset: 0x00092160
		private void RestoreBackup_Load(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.ExtractBackup));
			thread.Start();
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00093F87 File Offset: 0x00092187
		private void UpdateProgressSafe(int val)
		{
			this.pbProgress.Value = val;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00093F98 File Offset: 0x00092198
		private void ExtractBackup()
		{
			ZipFile zipFile = ZipFile.Read(this.m_backupFile);
			zipFile.ExtractProgress += this.zipFile_ExtractProgress;
			zipFile.ExtractAll(this.m_destFolder, ExtractExistingFileAction.InvokeExtractProgressEvent);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00093FD4 File Offset: 0x000921D4
		private void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e)
		{
			bool flag = e.EventType == ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite;
			if (flag)
			{
				e.CurrentEntry.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
			}
			bool flag2 = e.TotalBytesToTransfer > 100L;
			if (flag2)
			{
				this.pbProgress.Invoke(this.UpdateProgress, new object[] { (int)(e.BytesTransferred * 100L / e.TotalBytesToTransfer) });
			}
			bool flag3 = e.EventType == ZipProgressEventType.Extracting_AfterExtractAll;
			if (flag3)
			{
				base.Invoke(this.CloseForm, new object[] { true });
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0009406C File Offset: 0x0009226C
		private void CloseFormSafe(bool bSuccess)
		{
			bool flag = !bSuccess;
			if (flag)
			{
				base.DialogResult = DialogResult.Abort;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}

		// Token: 0x04000C42 RID: 3138
		private string m_backupFile;

		// Token: 0x04000C43 RID: 3139
		private string m_destFolder;

		// Token: 0x04000C44 RID: 3140
		private bool m_bActivated = false;

		// Token: 0x04000C45 RID: 3141
		private RestoreBackup.UpdateProgressDelegate UpdateProgress;

		// Token: 0x04000C46 RID: 3142
		private RestoreBackup.CloseDelegate CloseForm;

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x06001E83 RID: 7811
		private delegate void UpdateProgressDelegate(int value);

		// Token: 0x020002BD RID: 701
		// (Invoke) Token: 0x06001E87 RID: 7815
		private delegate void CloseDelegate(bool bSuccess);
	}
}
