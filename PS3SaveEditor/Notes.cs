using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3SaveEditor.Resources;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace PS3SaveEditor
{
	// Token: 0x020001D0 RID: 464
	public partial class Notes : Form
	{
		// Token: 0x06001819 RID: 6169 RVA: 0x0008CCAC File Offset: 0x0008AEAC
		public Notes(string notes)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.CenterToScreen();
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			string text = Util.ScaleSize(12) + "px";
			string text2 = string.Concat(new string[]
			{
				"<style>*{font:'",
				Util.GetFontFamily(),
				"';font-size:",
				text,
				";color:#000;} p,div{padding-bottom:4px;} </style>"
			});
			this.htmlPanel1.Text = text2 + "<body>" + notes + "</body>";
			this.btnOk.Text = Resources.btnOK;
			this.btnOk.Click += this.btnOk_Click;
			bool flag = Util.CurrentPlatform == Util.Platform.Linux;
			if (flag)
			{
				this.htmlPanel1.Scroll += this.HtmlPanel_Scroll;
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnOk_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0008CDBC File Offset: 0x0008AFBC
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

		// Token: 0x0600181C RID: 6172 RVA: 0x0008CE54 File Offset: 0x0008B054
		private async void HtmlPanel_Scroll(object sender, ScrollEventArgs e)
		{
			await Task.Delay(20);
			this.htmlPanel1.ClearSelection();
		}
	}
}
