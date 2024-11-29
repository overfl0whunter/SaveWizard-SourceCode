using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PS3SaveEditor
{
	// Token: 0x020001CA RID: 458
	public class CustomTableLayoutPanel : TableLayoutPanel
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x0007376E File Offset: 0x0007196E
		public CustomTableLayoutPanel()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0007378C File Offset: 0x0007198C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000AC2 RID: 2754
		private IContainer components = null;
	}
}
