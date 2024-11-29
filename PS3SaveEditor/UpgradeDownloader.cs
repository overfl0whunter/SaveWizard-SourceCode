using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E7 RID: 487
	public partial class UpgradeDownloader : Form
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x000A2288 File Offset: 0x000A0488
		public UpgradeDownloader(string url)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.m_url = url;
			this.lblStatus.Text = Resources.lblDownloadStatus;
			this.Text = Resources.titleUpgrader;
			base.CenterToScreen();
			this.lblStatus.BackColor = Color.Transparent;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			base.Load += this.UpgradeDownloader_Load;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000A2330 File Offset: 0x000A0530
		private void UpgradeDownloader_Load(object sender, EventArgs e)
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
			this.tempFile = Path.GetTempFileName();
			webClientEx.DownloadProgressChanged += this.client_DownloadProgressChanged;
			webClientEx.DownloadFileCompleted += this.client_DownloadFileCompleted;
			webClientEx.DownloadFileAsync(new Uri(this.m_url, UriKind.Absolute), this.tempFile, this.tempFile);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000A23AC File Offset: 0x000A05AC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000A2418 File Offset: 0x000A0618
		private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			bool flag = e.Error != null;
			if (flag)
			{
				Util.ShowMessage(Resources.errUpgrade);
			}
			else
			{
				new Process
				{
					StartInfo = new ProcessStartInfo("msiexec", "/i \"" + this.tempFile + "\"")
				}.Start();
				base.Close();
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000A247C File Offset: 0x000A067C
		private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.pbProgress.Value = e.ProgressPercentage;
		}

		// Token: 0x04000CBF RID: 3263
		private string m_url;

		// Token: 0x04000CC0 RID: 3264
		private string tempFile;
	}
}
