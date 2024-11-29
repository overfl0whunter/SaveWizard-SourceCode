using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PS3SaveEditor
{
	// Token: 0x020001C8 RID: 456
	public class CustomCheckedListBox : CheckedListBox
	{
		// Token: 0x06001742 RID: 5954 RVA: 0x00073570 File Offset: 0x00071770
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				bool flag2 = e.State == DrawItemState.Selected;
				if (flag2)
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
				}
				string text = base.Items[e.Index].ToString();
				CheckBoxState checkBoxState = (base.GetItemChecked(e.Index) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
				Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, checkBoxState);
				CheckBoxRenderer.DrawCheckBox(e.Graphics, e.Bounds.Location, new Rectangle(new Point(e.Bounds.X + glyphSize.Width, e.Bounds.Y), new Size(e.Bounds.Width - glyphSize.Width, e.Bounds.Height)), text, this.Font, false, checkBoxState);
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0007368C File Offset: 0x0007188C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000736C3 File Offset: 0x000718C3
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000AC1 RID: 2753
		private IContainer components = null;
	}
}
