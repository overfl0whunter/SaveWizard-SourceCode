using System;
using System.Drawing;
using System.Windows.Forms;

namespace PS3SaveEditor
{
	// Token: 0x020001C9 RID: 457
	public class CustomGroupBox : GroupBox
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x000736EC File Offset: 0x000718EC
		protected override void OnPaint(PaintEventArgs e)
		{
			bool flag = Util.IsUnixOrMacOSX();
			int num;
			if (flag)
			{
				num = base.ClientRectangle.Height - 5;
			}
			else
			{
				num = base.ClientRectangle.Height - 6;
			}
			e.Graphics.DrawRectangle(Pens.White, new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top + 4, base.ClientRectangle.Width - 1, num));
		}
	}
}
