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
	// Token: 0x020001E4 RID: 484
	public partial class SimpleSaveUploader : Form
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		public SimpleSaveUploader(game gameItem, string profile, List<string> files)
		{
			this.m_game = gameItem;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleSimpleEditUploader;
			base.CenterToScreen();
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.saveUploadDownloder1.BackColor = Color.FromArgb(200, 100, 10);
			this.saveUploadDownloder1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.saveUploadDownloder1.Files = files.ToArray();
			this.saveUploadDownloder1.Action = "patch";
			this.saveUploadDownloder1.OutputFolder = Path.GetDirectoryName(gameItem.LocalSaveFolder);
			this.saveUploadDownloder1.Game = gameItem;
			this.CloseForm = new SimpleSaveUploader.CloseDelegate(this.CloseFormSafe);
			base.Load += this.SimpleSaveUploader_Load;
			this.saveUploadDownloder1.DownloadFinish += this.saveUploadDownloder1_DownloadFinish;
			this.saveUploadDownloder1.UploadFinish += this.saveUploadDownloder1_UploadFinish;
			this.saveUploadDownloder1.UploadStart += this.saveUploadDownloder1_UploadStart;
			this.saveUploadDownloder1.DownloadStart += this.saveUploadDownloder1_DownloadStart;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0009E938 File Offset: 0x0009CB38
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0009E9A4 File Offset: 0x0009CBA4
		private List<string> PrepareZipFile(List<string> files)
		{
			List<string> list = new List<string>();
			List<string> containerFiles = this.m_game.GetContainerFiles();
			string text = this.m_game.LocalSaveFolder.Substring(0, this.m_game.LocalSaveFolder.Length - 4);
			string hash = Util.GetHash(text);
			bool cache = Util.GetCache(hash);
			string text2 = this.m_game.ToString(true, files);
			bool flag = cache;
			if (flag)
			{
				containerFiles.Remove(text);
				text2 = text2.Replace("<name>" + Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder) + "</name>", string.Concat(new string[]
				{
					"<name>",
					Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder),
					"</name><md5>",
					hash,
					"</md5>"
				}));
			}
			list.AddRange(containerFiles);
			string tempFolder = Util.GetTempFolder();
			string text3 = Path.Combine(tempFolder, "ps4_list.xml");
			File.WriteAllText(text3, text2);
			list.Add(text3);
			string asZipFile = ZipUtil.GetAsZipFile(containerFiles.ToArray(), null);
			return list;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0009EABF File Offset: 0x0009CCBF
		private void saveUploadDownloder1_DownloadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgDownloadPatch);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0009EAD3 File Offset: 0x0009CCD3
		private void saveUploadDownloder1_UploadStart(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgUploadPatch);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0009EAE7 File Offset: 0x0009CCE7
		private void saveUploadDownloder1_UploadFinish(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgWait);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0009EAFC File Offset: 0x0009CCFC
		private void saveUploadDownloder1_DownloadFinish(object sender, DownloadFinishEventArgs e)
		{
			bool flag = !e.Status && e.Error != "Abort";
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

		// Token: 0x06001957 RID: 6487 RVA: 0x0009EB61 File Offset: 0x0009CD61
		private void SimpleSaveUploader_Load(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.Start();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0009EB70 File Offset: 0x0009CD70
		private void CloseThis(bool status)
		{
			bool flag = !base.IsDisposed;
			if (flag)
			{
				base.Invoke(this.CloseForm, new object[] { status });
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
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

		// Token: 0x0600195A RID: 6490 RVA: 0x0009EBDC File Offset: 0x0009CDDC
		private void SimpleSaveUploader_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = !this.appClosing && e.CloseReason == CloseReason.UserClosing && base.DialogResult != DialogResult.OK;
			if (flag)
			{
				bool flag2 = Util.ShowMessage("Are you sure you want to abort?", Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
				bool flag3 = flag2;
				if (flag3)
				{
					this.saveUploadDownloder1.AbortEvent.Set();
					base.DialogResult = DialogResult.Abort;
					this.appClosing = true;
					e.Cancel = true;
					return;
				}
			}
			bool flag4 = !this.appClosing;
			if (flag4)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x04000CA3 RID: 3235
		private SimpleSaveUploader.CloseDelegate CloseForm;

		// Token: 0x04000CA4 RID: 3236
		private game m_game;

		// Token: 0x04000CA5 RID: 3237
		private bool appClosing = false;

		// Token: 0x020002CA RID: 714
		// (Invoke) Token: 0x06001EB8 RID: 7864
		private delegate void CloseDelegate(bool bStatus);
	}
}
