using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace PS3SaveEditor
{
	// Token: 0x020001E6 RID: 486
	public partial class StartupScreen : Form
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x000A1E30 File Offset: 0x000A0030
		public StartupScreen(bool hasUpdate = false)
		{
			this.hasUpdate = hasUpdate;
			this.InitializeComponent();
			this.Text = Util.PRODUCT_NAME;
			this.Font = Util.GetFontForPlatform(this.Font);
			string text = "Please note this is a BETA version of Save Wizard for PS4 MAX which may have issues.<br/>We recommend as a precaution that any valuable saves are backed up manually by hand before using this BETA version.<br/>There is no auto-update feature in this BETA version, so please visit and bookmark <a href='http://www.savewizard.net/downloads/beta.php'>http://www.savewizard.net/downloads/beta.php</a> for future updates.<br/>Any issues should be emailed to <a href='mailto:beta@savewizard.net'>beta@savewizard.net</a>. Where possible, please include the OS you are using, the error message and error code shown and where possible how you caused the problem. Screenshots are acceptable.<br/>By using Save Wizard for PS4 MAX BETA, you agree that you have read both the EULA contained with this product AND that you fully understand that this is BETA grade product AND that its use may be withdrawn at any time.";
			if (hasUpdate)
			{
				text = text + "<h2>New version available - " + Util.AvailableVersion + "</h2>";
			}
			string text2 = Util.ScaleSize(11) + "px";
			string text3 = "<style>*{font:Arial;font-size:" + text2 + ";color:#fff;} body,p,div{padding:0px;margin:0px;} a{color:#fff;}h2{color:red;}</style>";
			this.htmlPanel1.Text = text3 + "<body>" + text + "</body>";
			base.CenterToScreen();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000A1EEC File Offset: 0x000A00EC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006396F File Offset: 0x00061B6F
		private void btnAccept_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000A1F58 File Offset: 0x000A0158
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Abort;
			base.Close();
		}
	}
}
