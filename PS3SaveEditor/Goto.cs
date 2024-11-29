using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D9 RID: 473
	public partial class Goto : Form
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00090B16 File Offset: 0x0008ED16
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x00090B1E File Offset: 0x0008ED1E
		public long AddressLocation { get; set; }

		// Token: 0x06001873 RID: 6259 RVA: 0x00090B28 File Offset: 0x0008ED28
		public Goto(long maxLength)
		{
			this.m_maxLength = maxLength;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleGoto;
			this.lblEnterLoc.Text = Resources.lblEnterLoc;
			this.btnCancel.Text = Resources.btnCancel;
			this.btnOk.Text = Resources.btnOK;
			base.CenterToScreen();
			this.btnOk.Enabled = false;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00090BB8 File Offset: 0x0008EDB8
		private void btnOk_Click(object sender, EventArgs e)
		{
			bool flag = this.txtLocation.Text.StartsWith("0x");
			if (flag)
			{
				this.AddressLocation = long.Parse(this.txtLocation.Text.Substring(2), NumberStyles.HexNumber);
			}
			else
			{
				this.AddressLocation = long.Parse(this.txtLocation.Text);
			}
			base.Close();
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00090C28 File Offset: 0x0008EE28
		private void txtLocation_TextChanged(object sender, EventArgs e)
		{
			bool flag = string.IsNullOrEmpty(this.txtLocation.Text);
			if (flag)
			{
				this.btnOk.Enabled = false;
			}
			else
			{
				bool flag2 = this.txtLocation.Text.StartsWith("0x");
				if (flag2)
				{
					bool flag3 = this.txtLocation.Text.Length > 2;
					if (!flag3)
					{
						this.btnOk.Enabled = false;
						return;
					}
					long num = long.Parse(this.txtLocation.Text.Substring(2), NumberStyles.HexNumber);
					bool flag4 = num > this.m_maxLength;
					if (flag4)
					{
						this.btnOk.Enabled = false;
						return;
					}
				}
				else
				{
					long num2;
					bool flag5 = long.TryParse(this.txtLocation.Text.Trim(), out num2);
					if (flag5)
					{
						bool flag6 = num2 > this.m_maxLength;
						if (flag6)
						{
							this.btnOk.Enabled = false;
							return;
						}
					}
					else
					{
						bool flag7 = long.TryParse(this.txtLocation.Text.Trim(), NumberStyles.HexNumber, null, out num2);
						if (flag7)
						{
							this.txtLocation.Text = "0x" + this.txtLocation.Text.Trim();
							bool flag8 = num2 > this.m_maxLength;
							if (flag8)
							{
								this.btnOk.Enabled = false;
								return;
							}
						}
						else
						{
							this.txtLocation.Text = "";
						}
					}
				}
				this.btnOk.Enabled = true;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		private void txtLocation_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.Control || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right;
			if (!flag)
			{
				bool flag2 = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 && !e.Shift) || (this.txtLocation.SelectionStart == 1 && e.KeyCode == Keys.X && this.txtLocation.Text[0] == '0') || (this.txtLocation.Text.StartsWith("0x") && e.KeyCode >= Keys.A && e.KeyCode <= Keys.F);
				if (!flag2)
				{
					e.SuppressKeyPress = true;
				}
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000021C5 File Offset: 0x000003C5
		private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x04000C17 RID: 3095
		private long m_maxLength;
	}
}
