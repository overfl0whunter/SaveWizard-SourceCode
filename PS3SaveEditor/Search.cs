using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D5 RID: 469
	public partial class Search : Form
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0008F23C File Offset: 0x0008D43C
		public TextBox SearchText
		{
			get
			{
				return this.txtSearch;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x0008F254 File Offset: 0x0008D454
		public bool TextMode
		{
			set
			{
				if (value)
				{
					this.cbSearchType.SelectedIndex = 1;
					this.cbSearchType.Enabled = false;
				}
				else
				{
					this.cbSearchType.Enabled = true;
					this.cbSearchType.SelectedIndex = 0;
				}
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008F2A4 File Offset: 0x0008D4A4
		public Search(AdvancedEdit editForm)
		{
			this.m_editForm = editForm;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.btnFind.Click += this.btnFind_Click;
			this.btnFindPrev.Click += this.btnFindPrev_Click;
			base.CenterToScreen();
			this.btnOk.Enabled = false;
			this.btnOk.Text = Resources.btnOK;
			this.btnCancel.Text = Resources.btnCancel;
			this.btnFindPrev.Text = Resources.btnFindPrev;
			this.btnFind.Text = Resources.btnFind;
			this.cbSearchType.SelectedIndex = 0;
			base.FormClosed += this.Search_FormClosed;
			base.FormClosing += this.Search_FormClosing;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00073A38 File Offset: 0x00071C38
		private void Search_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			base.Hide();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000021C5 File Offset: 0x000003C5
		private void Search_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008F39C File Offset: 0x0008D59C
		private void btnFindPrev_Click(object sender, EventArgs e)
		{
			SearchMode searchMode = this.GetSearchMode();
			this.m_editForm.Search(true, false, searchMode);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008F3C0 File Offset: 0x0008D5C0
		public SearchMode GetSearchMode()
		{
			SearchMode searchMode;
			switch (this.cbSearchType.SelectedIndex)
			{
			case 0:
				searchMode = SearchMode.Hex;
				break;
			case 1:
				searchMode = SearchMode.Text;
				break;
			case 2:
				searchMode = SearchMode.Decimal;
				break;
			case 3:
				searchMode = SearchMode.Float;
				break;
			default:
				searchMode = SearchMode.Hex;
				break;
			}
			return searchMode;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008F407 File Offset: 0x0008D607
		private void btnFind_Click(object sender, EventArgs e)
		{
			this.m_editForm.Search(false, false, this.GetSearchMode());
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00073A89 File Offset: 0x00071C89
		private void btnOk_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00073A89 File Offset: 0x00071C89
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0008F420 File Offset: 0x0008D620
		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			bool textMode = this.m_editForm.TextMode;
			if (!textMode)
			{
				bool flag = this.GetSearchMode() == SearchMode.Decimal;
				if (flag)
				{
					try
					{
						int.Parse(this.txtSearch.Text);
					}
					catch (OverflowException)
					{
						this.txtSearch.Text = this.txtSearch.Text.Substring(0, this.txtSearch.Text.Length - 1);
						this.txtSearch.SelectionStart = this.txtSearch.Text.Length;
					}
					catch (Exception)
					{
					}
				}
				bool flag2 = this.txtSearch.Text.Length > 0;
				if (flag2)
				{
					this.btnFind.Enabled = true;
					this.btnFindPrev.Enabled = true;
				}
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008F50C File Offset: 0x0008D70C
		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.m_editForm.Search(false, true, this.GetSearchMode());
			}
			bool textMode = this.m_editForm.TextMode;
			if (!textMode)
			{
				bool flag2 = e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.Control || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right;
				if (!flag2)
				{
					bool flag3 = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 && !e.Shift) || (this.txtSearch.SelectionStart == 1 && e.KeyCode == Keys.X && this.txtSearch.Text[0] == '0') || (this.GetSearchMode() == SearchMode.Hex && e.KeyCode >= Keys.A && e.KeyCode <= Keys.F);
					if (!flag3)
					{
						bool flag4 = this.GetSearchMode() == SearchMode.Text;
						if (!flag4)
						{
							bool flag5 = this.GetSearchMode() == SearchMode.Float;
							if (flag5)
							{
								bool flag6 = e.KeyCode == Keys.Decimal;
								if (flag6)
								{
									return;
								}
							}
							e.SuppressKeyPress = true;
						}
					}
				}
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000021C5 File Offset: 0x000003C5
		private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x04000BEB RID: 3051
		private AdvancedEdit m_editForm;
	}
}
