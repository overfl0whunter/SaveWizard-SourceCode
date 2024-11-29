using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D6 RID: 470
	public partial class ServerSaveDownloader : Form
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x0008FC7C File Offset: 0x0008DE7C
		public ServerSaveDownloader(string saveid, string saveFolder, game game)
		{
			this.m_saveFolder = saveFolder;
			this.m_game = game;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.saveUploadDownloder1.BackColor = Color.FromArgb(200, 100, 10);
			this.Text = Resources.titleResign;
			base.CenterToScreen();
			this.saveUploadDownloder1.SaveId = saveid;
			this.saveUploadDownloder1.Action = "download";
			this.saveUploadDownloder1.OutputFolder = this.m_saveFolder;
			this.CloseForm = new ServerSaveDownloader.CloseDelegate(this.CloseFormSafe);
			base.Load += this.SimpleSaveUploader_Load;
			this.saveUploadDownloder1.DownloadFinish += this.saveUploadDownloder1_DownloadFinish;
			this.saveUploadDownloder1.UploadFinish += this.saveUploadDownloder1_UploadFinish;
			this.saveUploadDownloder1.UploadStart += this.saveUploadDownloder1_UploadStart;
			this.saveUploadDownloder1.DownloadStart += this.saveUploadDownloder1_DownloadStart;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0008FDBC File Offset: 0x0008DFBC
		private List<string> PrepareZipFile(game game)
		{
			List<string> list = new List<string>();
			list.Add(Path.Combine(this.m_saveFolder, "PARAM.SFO"));
			list.Add(Path.Combine(this.m_saveFolder, "PARAM.PFD"));
			string tempFolder = Util.GetTempFolder();
			string text = Path.Combine(tempFolder, "ps3_files_list.xml");
			bool flag = game != null;
			if (flag)
			{
				File.WriteAllText(text, "<files><game>" + game.id + "</game><pfd>PARAM.PFD</pfd><sfo>PARAM.SFO</sfo></files>");
			}
			else
			{
				string text2 = MainForm.GetParamInfo(Path.Combine(this.m_saveFolder, "PARAM.SFO"), "SAVEDATA_DIRECTORY");
				bool flag2 = string.IsNullOrEmpty(text2) || text2.Length < 9;
				if (flag2)
				{
					text2 = Path.GetDirectoryName(this.m_saveFolder);
				}
				File.WriteAllText(text, "<files><game>" + text2.Substring(0, 9) + "</game><pfd>PARAM.PFD</pfd><sfo>PARAM.SFO</sfo></files>");
			}
			list.Add(text);
			return list;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008FEAD File Offset: 0x0008E0AD
		private void saveUploadDownloder1_DownloadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgDownloadPatch);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008FEC1 File Offset: 0x0008E0C1
		private void saveUploadDownloder1_UploadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgUploadPatch);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008FED5 File Offset: 0x0008E0D5
		private void saveUploadDownloder1_UploadFinish(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgWait);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008FEEC File Offset: 0x0008E0EC
		private void saveUploadDownloder1_DownloadFinish(object sender, DownloadFinishEventArgs e)
		{
			bool flag = !e.Status;
			if (flag)
			{
				bool flag2 = e.Error != null;
				if (flag2)
				{
					Util.ShowMessage(e.Error, Resources.msgError);
				}
				else
				{
					Util.ShowMessage(Resources.errServer, Resources.msgError);
				}
			}
			else
			{
				bool flag3 = Directory.Exists(this.m_game.LocalSaveFolder);
				if (flag3)
				{
				}
			}
			this.CloseThis(e.Status);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008FF64 File Offset: 0x0008E164
		private void SimpleSaveUploader_Load(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.Start();
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008FF74 File Offset: 0x0008E174
		private void CloseThis(bool status)
		{
			bool flag = !base.IsDisposed;
			if (flag)
			{
				base.Invoke(this.CloseForm, new object[] { status });
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008FFAC File Offset: 0x0008E1AC
		private void CloseFormSafe(bool bStatus)
		{
			if (bStatus)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Abort;
			}
			this.appClosing = true;
			base.Close();
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0008FFE0 File Offset: 0x0008E1E0
		private void SimpleSaveUploader_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = !this.appClosing;
			if (flag)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private string m_saveFolder;

		// Token: 0x04000BF5 RID: 3061
		private ServerSaveDownloader.CloseDelegate CloseForm;

		// Token: 0x04000BF6 RID: 3062
		private bool appClosing = false;

		// Token: 0x04000BF7 RID: 3063
		private game m_game;

		// Token: 0x020002A0 RID: 672
		// (Invoke) Token: 0x06001E4B RID: 7755
		private delegate void CloseDelegate(bool bStatus);
	}
}
