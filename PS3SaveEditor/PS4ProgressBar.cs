using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PS3SaveEditor
{
	// Token: 0x020001D2 RID: 466
	public class PS4ProgressBar : ProgressBar
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x0008E7D0 File Offset: 0x0008C9D0
		public PS4ProgressBar()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0008E824 File Offset: 0x0008CA24
		protected override void OnPaint(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 181, 255), Color.FromArgb(0, 62, 207), 90f))
			{
				bool flag = base.Value > 0;
				if (flag)
				{
					e.Graphics.FillRectangle(linearGradientBrush, 0f, 0f, (float)base.ClientRectangle.Width * (float)(base.Value + 1) / (float)base.Maximum, (float)base.ClientRectangle.Height);
				}
				else
				{
					e.Graphics.FillRectangle(linearGradientBrush, 0f, 0f, (float)base.ClientRectangle.Width * (float)base.Value / (float)base.Maximum, (float)base.ClientRectangle.Height);
				}
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0008E91C File Offset: 0x0008CB1C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0008E953 File Offset: 0x0008CB53
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Style = ProgressBarStyle.Continuous;
			base.ResumeLayout(false);
		}

		// Token: 0x04000BDF RID: 3039
		private IContainer components = null;
	}
}
