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
	// Token: 0x020001AF RID: 431
	public partial class AdvancedSaveUploaderForEncrypt : Form
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0006E599 File Offset: 0x0006C799
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0006E5A1 File Offset: 0x0006C7A1
		public Dictionary<string, byte[]> DecryptedSaveData { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0006E5AA File Offset: 0x0006C7AA
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0006E5B2 File Offset: 0x0006C7B2
		public byte[] DependentSaveData { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0006E5BB File Offset: 0x0006C7BB
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x0006E5C3 File Offset: 0x0006C7C3
		public string ListResult { get; set; }

		// Token: 0x06001647 RID: 5703 RVA: 0x0006E5CC File Offset: 0x0006C7CC
		public AdvancedSaveUploaderForEncrypt(string[] files, game gameItem, string profile, string action)
		{
			this.m_files = files;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.DecryptedSaveData = new Dictionary<string, byte[]>();
			this.saveUploadDownloder1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.SetLabels();
			this.m_action = action;
			bool flag = action == "list";
			if (flag)
			{
				this.Text = Resources.titleSimpleEdit;
			}
			base.CenterToScreen();
			this.saveUploadDownloder1.Files = files;
			this.saveUploadDownloder1.Action = action;
			bool flag2 = this.m_action == "encrypt";
			if (flag2)
			{
				this.saveUploadDownloder1.OutputFolder = Path.GetDirectoryName(gameItem.LocalSaveFolder);
			}
			else
			{
				this.saveUploadDownloder1.OutputFolder = ZipUtil.GetPs3SeTempFolder();
			}
			this.saveUploadDownloder1.Game = gameItem;
			this.CloseForm = new AdvancedSaveUploaderForEncrypt.CloseDelegate(this.CloseFormSafe);
			base.Load += this.AdvancedSaveUploaderForEncrypt_Load;
			this.saveUploadDownloder1.DownloadFinish += this.saveUploadDownloder1_DownloadFinish;
			this.saveUploadDownloder1.UploadFinish += this.saveUploadDownloder1_UploadFinish;
			this.saveUploadDownloder1.UploadStart += this.saveUploadDownloder1_UploadStart;
			this.saveUploadDownloder1.DownloadStart += this.saveUploadDownloder1_DownloadStart;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0006E760 File Offset: 0x0006C960
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0006E7CC File Offset: 0x0006C9CC
		private void SetLabels()
		{
			this.Text = Resources.titleAdvDownloader;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		private void saveUploadDownloder1_DownloadStart(object sender, EventArgs e)
		{
			bool flag = this.m_action == "encrypt";
			if (flag)
			{
				this.saveUploadDownloder1.SetStatus(Resources.msgDownloadEnc);
			}
			else
			{
				this.saveUploadDownloder1.SetStatus(Resources.msgDownloadDec);
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0006E824 File Offset: 0x0006CA24
		private void saveUploadDownloder1_UploadStart(object sender, EventArgs e)
		{
			bool flag = this.m_action == "encrypt";
			if (flag)
			{
				this.saveUploadDownloder1.SetStatus(Resources.msgUploadEnc);
			}
			else
			{
				this.saveUploadDownloder1.SetStatus(Resources.msgUploadDec);
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0006E86A File Offset: 0x0006CA6A
		private void saveUploadDownloder1_UploadFinish(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.SetStatus(Resources.msgWait);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0006E880 File Offset: 0x0006CA80
		private void saveUploadDownloder1_DownloadFinish(object sender, DownloadFinishEventArgs e)
		{
			bool flag = this.m_action == "list" && !string.IsNullOrEmpty(this.saveUploadDownloder1.ListResult);
			if (flag)
			{
				this.ListResult = this.saveUploadDownloder1.ListResult;
				bool flag2 = !base.IsDisposed;
				if (flag2)
				{
					this.CloseThis(e.Status);
				}
			}
			else
			{
				List<string> saveFiles = this.saveUploadDownloder1.Game.GetSaveFiles();
				string[] files = Directory.GetFiles(this.saveUploadDownloder1.OutputFolder, "*");
				foreach (string text in saveFiles)
				{
					container targetGameFolder = this.saveUploadDownloder1.Game.GetTargetGameFolder();
					bool status = e.Status;
					if (!status)
					{
						bool flag3 = e.Error == "Abort";
						if (!flag3)
						{
							bool flag4 = !string.IsNullOrEmpty(e.Error);
							if (flag4)
							{
								SaveUploadDownloder.ErrorMessage(this, e.Error, Resources.msgError);
							}
						}
						break;
					}
					bool flag5 = this.m_action == "decrypt";
					if (flag5)
					{
						foreach (string text2 in this.saveUploadDownloder1.OrderedEntries)
						{
							string text3 = Path.Combine(Util.GetTempFolder(), text2);
							bool flag6 = Array.IndexOf<string>(files, text3) < 0;
							if (!flag6)
							{
								bool flag7 = Path.GetFileName(text3).Equals("param.sfo", StringComparison.CurrentCultureIgnoreCase);
								if (!flag7)
								{
									bool flag8 = Path.GetFileName(text3).Equals("param.pfd", StringComparison.CurrentCultureIgnoreCase);
									if (!flag8)
									{
										bool flag9 = Path.GetFileName(text3).Equals("devlog.txt", StringComparison.CurrentCultureIgnoreCase);
										if (!flag9)
										{
											bool flag10 = Path.GetFileName(text3).Equals("ps4_list.xml", StringComparison.CurrentCultureIgnoreCase);
											if (!flag10)
											{
												bool flag11 = !this.DecryptedSaveData.ContainsKey(Path.GetFileName(text3)) && (text == Path.GetFileName(text3) || Util.IsMatch(Path.GetFileName(text3), text));
												if (flag11)
												{
													this.DecryptedSaveData.Add(Path.GetFileName(text3), File.ReadAllBytes(text3));
												}
											}
										}
									}
								}
							}
						}
					}
				}
				bool flag12 = !base.IsDisposed;
				if (flag12)
				{
					this.CloseThis(e.Status);
				}
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0006EB50 File Offset: 0x0006CD50
		private void CloseThis(bool bStatus)
		{
			try
			{
				base.Invoke(this.CloseForm, new object[] { bStatus });
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0006EB94 File Offset: 0x0006CD94
		private void CloseFormSafe(bool bStatus)
		{
			base.DialogResult = (bStatus ? DialogResult.OK : DialogResult.Abort);
			this.appClosing = true;
			base.Close();
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006EBB3 File Offset: 0x0006CDB3
		private void AdvancedSaveUploaderForEncrypt_Load(object sender, EventArgs e)
		{
			this.saveUploadDownloder1.Start();
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0006EBC4 File Offset: 0x0006CDC4
		private void AdvancedSaveUploaderForEncrypt_FormClosing(object sender, FormClosingEventArgs e)
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

		// Token: 0x04000A4E RID: 2638
		private AdvancedSaveUploaderForEncrypt.CloseDelegate CloseForm;

		// Token: 0x04000A51 RID: 2641
		private string m_action;

		// Token: 0x04000A52 RID: 2642
		private string[] m_files;

		// Token: 0x04000A53 RID: 2643
		private bool appClosing = false;

		// Token: 0x02000291 RID: 657
		// (Invoke) Token: 0x06001E28 RID: 7720
		private delegate void CloseDelegate(bool bStatus);
	}
}
