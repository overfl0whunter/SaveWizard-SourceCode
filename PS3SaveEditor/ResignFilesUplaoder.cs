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
	// Token: 0x020001DE RID: 478
	public partial class ResignFilesUplaoder : Form
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x000937CC File Offset: 0x000919CC
		public ResignFilesUplaoder(game game, string saveFolder, string profile, List<string> files)
		{
			this.m_saveFolder = saveFolder;
			this.m_profile = profile;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleResign;
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.saveUploadDownloder1.BackColor = Color.FromArgb(200, 100, 10);
			this.saveUploadDownloder1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.Text = Resources.titleResign;
			base.CenterToScreen();
			this.saveUploadDownloder1.Game = game;
			this.saveUploadDownloder1.Profile = profile;
			this.saveUploadDownloder1.Files = files.ToArray();
			this.saveUploadDownloder1.Action = "resign";
			this.saveUploadDownloder1.OutputFolder = this.m_saveFolder.Replace(game.PSN_ID, profile);
			bool flag = !Directory.Exists(this.saveUploadDownloder1.OutputFolder);
			if (flag)
			{
				Directory.CreateDirectory(this.saveUploadDownloder1.OutputFolder);
			}
			this.CloseForm = new ResignFilesUplaoder.CloseDelegate(this.CloseFormSafe);
			base.Load += this.SimpleSaveUploader_Load;
			this.saveUploadDownloder1.DownloadFinish += this.saveUploadDownloder1_DownloadFinish;
			this.saveUploadDownloder1.UploadFinish += this.saveUploadDownloder1_UploadFinish;
			this.saveUploadDownloder1.UploadStart += this.saveUploadDownloder1_UploadStart;
			this.saveUploadDownloder1.DownloadStart += this.saveUploadDownloder1_DownloadStart;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0009398C File Offset: 0x00091B8C
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000939F8 File Offset: 0x00091BF8
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

		// Token: 0x060018B3 RID: 6323 RVA: 0x00093AE9 File Offset: 0x00091CE9
		private void saveUploadDownloder1_DownloadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgDownloadPatch);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00093AFD File Offset: 0x00091CFD
		private void saveUploadDownloder1_UploadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgUploadPatch);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00093B11 File Offset: 0x00091D11
		private void saveUploadDownloder1_UploadFinish(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgWait);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00093B28 File Offset: 0x00091D28
		private void saveUploadDownloder1_DownloadFinish(object sender, DownloadFinishEventArgs e)
		{
			bool flag = !e.Status;
			if (flag)
			{
				bool flag2 = !string.IsNullOrEmpty(e.Error);
				if (flag2)
				{
					Util.ShowMessage(e.Error, Resources.msgError);
				}
			}
			this.CloseThis(e.Status);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00093B7B File Offset: 0x00091D7B
		private void SimpleSaveUploader_Load(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.Start();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00093B8C File Offset: 0x00091D8C
		private void CloseThis(bool status)
		{
			bool flag = !base.IsDisposed;
			if (flag)
			{
				base.Invoke(this.CloseForm, new object[] { status });
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00093BC4 File Offset: 0x00091DC4
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

		// Token: 0x060018BA RID: 6330 RVA: 0x00093BF8 File Offset: 0x00091DF8
		private void SimpleSaveUploader_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = !this.appClosing;
			if (flag)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private string m_saveFolder;

		// Token: 0x04000C3D RID: 3133
		private ResignFilesUplaoder.CloseDelegate CloseForm;

		// Token: 0x04000C3E RID: 3134
		private string m_profile;

		// Token: 0x04000C3F RID: 3135
		private bool appClosing = false;

		// Token: 0x020002BB RID: 699
		// (Invoke) Token: 0x06001E7F RID: 7807
		private delegate void CloseDelegate(bool bStatus);
	}
}
