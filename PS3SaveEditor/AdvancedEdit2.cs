using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Be.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001ED RID: 493
	public partial class AdvancedEdit2 : Form
	{
		// Token: 0x060019FD RID: 6653 RVA: 0x000A55D4 File Offset: 0x000A37D4
		public AdvancedEdit2(game game, Dictionary<string, byte[]> data)
		{
			this.InitializeComponent();
			base.CenterToScreen();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnApply.ForeColor = Color.Black;
			this.btnClose.BackColor = SystemColors.ButtonFace;
			this.btnClose.ForeColor = Color.Black;
			this.m_DirtyFilesLeft = new List<string>();
			this.m_DirtyFilesRight = new List<string>();
			this.m_saveFilesDataLeft = data;
			this.m_saveFilesDataRight = new Dictionary<string, byte[]>();
			this.tableMain.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.tableTop.BackColor = Color.Transparent;
			this.tableRight.BackColor = Color.Transparent;
			this.lblGameName.BackColor = Color.Transparent;
			this.panel1.BackColor = Color.Transparent;
			this.DoubleBuffered = true;
			this.lblOffset.BackColor = Color.Transparent;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.SetLabels();
			this.lblGameName.Text = game.name;
			this.m_game = game;
			this.FillCache();
			this.btnApply.Enabled = false;
			this.m_bTextMode = false;
			this.cbSearchMode.SelectedIndex = 0;
			this.txtSaveDataLeft.TextChanged += this.txtSaveData_TextChanged;
			this.txtSaveDataRight.TextChanged += this.txtSaveData_TextChanged;
			this.btnCompare.Click += this.btnCompare_Click;
			this.cbSaveFiles.SelectedIndexChanged += this.cbSaveFiles_SelectedIndexChanged;
			foreach (string text in data.Keys)
			{
				this.cbSaveFiles.Items.Add(text);
			}
			bool flag = this.cbSaveFiles.Items.Count > 0;
			if (flag)
			{
				this.cbSaveFiles.SelectedIndex = 0;
			}
			bool flag2 = this.cbSaveFiles.Items.Count == 1;
			if (flag2)
			{
				this.cbSaveFiles.Enabled = false;
			}
			this.FillCheats();
			this.btnApply.Click += this.btnApply_Click;
			this.btnClose.Click += this.btnClose_Click;
			this.btnPush.Click += this.btnPush_Click;
			this.btnPop.Click += this.btnPop_Click;
			this.hexBoxLeft.SelectionBackColor = Color.FromArgb(0, 175, 255);
			this.hexBoxLeft.ShadowSelectionColor = Color.FromArgb(204, 240, 255);
			this.hexBoxRight.SelectionBackColor = Color.FromArgb(0, 175, 255);
			this.hexBoxRight.ShadowSelectionColor = Color.FromArgb(204, 240, 255);
			this.hexBoxLeft.GotFocus += this.hexBox1_GotFocus;
			this.hexBoxRight.GotFocus += this.hexBox2_GotFocus;
			this.txtSaveDataLeft.GotFocus += this.txtSaveDataLeft_GotFocus;
			this.txtSaveDataRight.GotFocus += this.txtSaveDataRight_GotFocus;
			this.hexBoxLeft.Focus();
			this.lstCache.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstCache.DrawItem += this.lstCache_DrawItem;
			this.lstCheatCodes.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstCheatCodes.DrawItem += this.lstCheatCodes_DrawItem;
			this.lstSearchVal.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstSearchVal.DrawItem += this.lstSearchVal_DrawItem;
			this.lstSearchAddresses.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstSearchAddresses.DrawItem += this.lstSearchAddresses_DrawItem;
			this.activeHexBox = this.hexBoxLeft;
			this.lstCache.KeyDown += this.lstCache_KeyDown;
			this.panelLeft.Paint += this.panelLeft_Paint;
			this.listViewCheats.SelectedIndexChanged += this.listViewCheats_SelectedIndexChanged;
			this.listViewCheats.ItemCheck += this.listViewCheats_ItemCheck;
			this.listViewCheats.KeyDown += this.listViewCheats_KeyDown;
			this.txtSearchValue.KeyDown += this.txtSearchValue_KeyDown;
			this.btnFindPrev.Click += this.btnFindPrev_Click;
			this.btnFind.Click += this.btnFind_Click;
			this.tableLayoutMiddle.CellPaint += this.tableLayoutPanel1_CellPaint;
			this.hexBoxLeft.SelectionStartChanged += this.hexBox1_SelectionStartChanged;
			this.hexBoxRight.SelectionStartChanged += this.hexBox2_SelectionStartChanged;
			this.hexBoxLeft.VScroll += this.hexBox1_Scroll;
			this.hexBoxRight.VScroll += this.hexBox2_Scroll;
			this.hexBoxLeft.HScroll += this.hexBoxLeft_HScroll;
			this.hexBoxRight.HScroll += this.hexBoxRight_HScroll;
			this.diffResults.OnDiffRowSelected += this.diffResults_OnDiffRowSelected;
			this.lstCheatCodes.SelectedIndexChanged += this.lstCheatCodes_SelectedIndexChanged;
			this.panelLeft.Visible = false;
			this.panelRight.Visible = false;
			base.ResizeBegin += delegate(object s, EventArgs e)
			{
				base.SuspendLayout();
			};
			base.ResizeEnd += delegate(object s, EventArgs e)
			{
				base.ResumeLayout(true);
			};
			this.MinimumSize = Util.ScaleSize(new Size(856, 522));
			base.ResizeRedraw = false;
			base.SizeChanged += delegate(object s, EventArgs e)
			{
				bool flag4 = base.WindowState == FormWindowState.Maximized;
				if (flag4)
				{
					this._resizeInProgress = false;
					this.tableMain.BackColor = Color.FromArgb(127, 204, 204, 204);
					this.tableLayoutMiddle.BackColor = Color.Transparent;
					this.tableRight.BackColor = Color.Transparent;
					this.tableTop.BackColor = Color.Transparent;
					base.Invalidate(true);
				}
			};
			this.cbSaveFiles.Width = Math.Min(200, this.ComboBoxWidth(this.cbSaveFiles));
			this.btnCompare.Width = this.cbSaveFiles.Width;
			this.panelRight.BackColor = Color.FromArgb(102, 164, 201);
			this.btnStackAddress.Click += this.btnStackAddress_Click;
			this.btnStackSearch.Click += this.btnStackSearch_Click;
			this.cbSearchMode.SelectedIndexChanged += this.cbSearchMode_SelectedIndexChanged;
			this.btnGo.Click += this.btnGo_Click;
			this.lstSearchAddresses.KeyDown += this.lstSearchAddresses_KeyDown;
			this.lstSearchAddresses.SelectedIndexChanged += this.lstSearchAddresses_SelectedIndexChanged;
			this.lstSearchAddresses.KeyDown += this.lstSearchAddresses_KeyDown;
			this.lstSearchVal.KeyDown += this.lstSearchVal_KeyDown;
			this.lstSearchVal.MouseClick += this.lstSearchVal_MouseClick;
			this.lstSearchVal.SelectedIndexChanged += this.lstSearchVal_SelectedIndexChanged;
			this.hexBoxLeft.KeyDown += this.hexBox1_KeyDown;
			this.hexBoxRight.KeyDown += this.hexBox2_KeyDown;
			bool flag3 = Util.IsUnixOrMacOSX();
			if (flag3)
			{
				this.lblOffset.Location = new Point(Util.ScaleSize(467), 0);
				this.lblOffsetValue.Location = new Point(Util.ScaleSize(517), 0);
			}
			this.chkEnableRight_CheckedChanged(null, null);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000A5E28 File Offset: 0x000A4028
		private void lstSearchAddresses_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
					e.Graphics.DrawString((string)this.lstSearchAddresses.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstSearchAddresses.Items[e.Index], e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000A5F24 File Offset: 0x000A4124
		private void lstSearchVal_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
					e.Graphics.DrawString((string)this.lstSearchVal.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstSearchVal.Items[e.Index], e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000A6020 File Offset: 0x000A4220
		private void lstCheatCodes_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(72, 187, 97)), e.Bounds);
					e.Graphics.DrawString((string)this.lstCheatCodes.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstCheatCodes.Items[e.Index], e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000A6118 File Offset: 0x000A4318
		private void lstCache_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(204, 240, 255)), e.Bounds);
					e.Graphics.DrawString((string)this.lstCache.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstCache.Items[e.Index], e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000A6216 File Offset: 0x000A4416
		private void txtSaveDataRight_GotFocus(object sender, EventArgs e)
		{
			this.activeHexBox = null;
			this.activeTextBox = this.txtSaveDataRight;
			this.activeTextBox.BorderStyle = BorderStyle.Fixed3D;
			this.txtSaveDataLeft.BorderStyle = BorderStyle.None;
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000A6252 File Offset: 0x000A4452
		private void txtSaveDataLeft_GotFocus(object sender, EventArgs e)
		{
			this.activeHexBox = null;
			this.activeTextBox = this.txtSaveDataLeft;
			this.activeTextBox.BorderStyle = BorderStyle.Fixed3D;
			this.txtSaveDataRight.BorderStyle = BorderStyle.None;
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000A6290 File Offset: 0x000A4490
		private int ComboBoxWidth(ComboBox myCombo)
		{
			int num = 0;
			foreach (object obj in myCombo.Items)
			{
				int width = TextRenderer.MeasureText(myCombo.GetItemText(obj), myCombo.Font).Width;
				bool flag = width > num;
				if (flag)
				{
					num = width;
				}
			}
			return num + SystemInformation.VerticalScrollBarWidth;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000A6320 File Offset: 0x000A4520
		private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			bool flag = e.Row == 0;
			if (flag)
			{
				int num = 88;
				int num2 = 24;
				int num3 = 500;
				bool flag2 = Util.IsUnixOrMacOSX();
				if (flag2)
				{
					num = 80;
					num2 = 21;
					num3 = 450;
				}
				bool flag3 = e.Column == 0;
				if (flag3)
				{
					using (Brush brush = new SolidBrush(this.panelLeft.BackColor))
					{
						e.Graphics.FillRectangle(brush, 10, 0, Math.Min(this.hexBoxLeft.Width, Math.Min(e.CellBounds.Width, 610)), this.panelLeft.ClientRectangle.Height);
					}
					using (Brush brush2 = new SolidBrush(this.panelLeft.ForeColor))
					{
						e.Graphics.DrawString("Address", this.panelLeft.Font, brush2, new Point(-this.hexBoxLeft.HScrollBar.Value + 10, 0));
						for (int i = 0; i < 16; i++)
						{
							e.Graphics.DrawString(i.ToString("X") + "+", this.panelLeft.Font, brush2, new Point(-this.hexBoxLeft.HScrollBar.Value + num + i * num2, 0));
						}
						e.Graphics.DrawString("ASCII", this.panelLeft.Font, brush2, new Point(-this.hexBoxLeft.HScrollBar.Value + num3, 0));
					}
				}
				else
				{
					Rectangle rectangle = new Rectangle(new Point(e.CellBounds.Location.X + 8, 0), new Size(Math.Min(e.CellBounds.Width, 610), 20));
					e.Graphics.Clip = new Region(rectangle);
					using (Brush brush3 = new SolidBrush(this.panelRight.BackColor))
					{
						e.Graphics.FillRectangle(brush3, rectangle);
					}
					using (Brush brush4 = new SolidBrush(this.panelRight.ForeColor))
					{
						e.Graphics.DrawString("Address", this.panelRight.Font, brush4, new Point(-this.hexBoxRight.HScrollBar.Value + e.CellBounds.Location.X + 10, 0));
						for (int j = 0; j < 16; j++)
						{
							e.Graphics.DrawString(j.ToString("X") + "+", this.panelRight.Font, brush4, new Point(-this.hexBoxRight.HScrollBar.Value + e.CellBounds.Location.X + num + j * num2, 0));
						}
						e.Graphics.DrawString("ASCII", this.panelRight.Font, brush4, new Point(-this.hexBoxRight.HScrollBar.Value + e.CellBounds.Location.X + num3, 0));
					}
				}
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000A6738 File Offset: 0x000A4938
		private void panelLeft_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.TranslateTransform((float)this.hexBoxLeft.HScrollBar.Value, 0f);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000A675D File Offset: 0x000A495D
		private void hexBoxRight_HScroll(object sender, EventArgs e)
		{
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000A675D File Offset: 0x000A495D
		private void hexBoxLeft_HScroll(object sender, EventArgs e)
		{
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000A676C File Offset: 0x000A496C
		private void listViewCheats_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			bool flag = e.NewValue == CheckState.Checked;
			if (flag)
			{
				this.btnApply.Enabled = true;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000A6794 File Offset: 0x000A4994
		private void lstCheatCodes_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.lstCheatCodes.SelectedItems.Count == 1;
			if (flag)
			{
				string text = (string)this.lstCheatCodes.SelectedItems[0];
				string[] array = text.Split(new char[] { ' ' });
				try
				{
					long num = long.Parse(array[0], NumberStyles.HexNumber);
					num &= 268435455L;
					bool flag2 = num <= this.hexBoxLeft.ByteProvider.Length;
					if (flag2)
					{
						this.hexBoxLeft.ScrollByteIntoView(num);
						bool flag3 = this.hexBoxRight.ByteProvider != null;
						if (flag3)
						{
							this.hexBoxRight.ScrollByteIntoView(num);
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000A6864 File Offset: 0x000A4A64
		private void diffResults_OnDiffRowSelected(object sender, EventArgs e)
		{
			DataGridView dataGridView = sender as DataGridView;
			bool flag = dataGridView.SelectedRows.Count == 1;
			if (flag)
			{
				bool flag2 = !string.IsNullOrEmpty((string)dataGridView.SelectedRows[0].Cells[0].Value);
				if (flag2)
				{
					long num = long.Parse((string)dataGridView.SelectedRows[0].Cells[0].Value, NumberStyles.HexNumber);
					this.hexBoxLeft.ScrollByteIntoView(num);
					bool flag3 = this.hexBoxRight.ByteProvider != null;
					if (flag3)
					{
						this.hexBoxRight.ScrollByteIntoView(num);
					}
				}
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000A691C File Offset: 0x000A4B1C
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 274;
			if (flag)
			{
				bool flag2 = m.WParam == new IntPtr(61488);
				if (flag2)
				{
					this.tableMain.BackColor = Color.FromArgb(0, 138, 213);
					this.tableLayoutMiddle.BackColor = Color.FromArgb(0, 138, 213);
					this.tableRight.BackColor = Color.FromArgb(0, 138, 213);
					this.tableTop.BackColor = Color.FromArgb(0, 138, 213);
					this._resizeInProgress = true;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000021C5 File Offset: 0x000003C5
		private void listViewCheats_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000A69DA File Offset: 0x000A4BDA
		private void btnFindPrev_Click(object sender, EventArgs e)
		{
			this.Search(true, false);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000A69E8 File Offset: 0x000A4BE8
		private void lstCache_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Delete;
			if (flag)
			{
				bool flag2 = this.lstCache.SelectedItem == null;
				if (!flag2)
				{
					bool flag3 = Util.ShowMessage(Resources.warnDeleteCache, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
					if (flag3)
					{
						Directory.Delete(Util.GetCacheFolder(this.m_game, (string)this.lstCache.SelectedItem), true);
						this.lstCache.Items.Remove(this.lstCache.SelectedItem);
					}
				}
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000A6A71 File Offset: 0x000A4C71
		private void cbSearchMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.txtSearchValue.Text = "";
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000A6A88 File Offset: 0x000A4C88
		private void txtAddress_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.btnGo_Click(null, null);
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
		private void listViewCheats_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			bool @checked = e.Item.Checked;
			if (@checked)
			{
				this.btnApply.Enabled = true;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000A6ADC File Offset: 0x000A4CDC
		private void listViewCheats_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.listViewCheats.SelectedItems.Count == 1;
			if (flag)
			{
				string text = (this.listViewCheats.Tag as List<string>)[this.listViewCheats.SelectedIndex];
				this.lstCheatCodes.Items.Clear();
				string[] array = text.Split(new char[] { '\n' });
				foreach (string text2 in array)
				{
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						this.lstCheatCodes.Items.Add(text2);
					}
				}
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000A6B8C File Offset: 0x000A4D8C
		private void hexBox2_GotFocus(object sender, EventArgs e)
		{
			this.activeTextBox = null;
			this.activeHexBox = this.hexBoxRight;
			this.panelRight.BackColor = Color.FromArgb(0, 175, 255);
			this.panelRight.ForeColor = Color.White;
			this.panelLeft.BackColor = Color.FromArgb(102, 164, 201);
			this.panelLeft.ForeColor = Color.Black;
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000A6C14 File Offset: 0x000A4E14
		private void hexBox1_GotFocus(object sender, EventArgs e)
		{
			this.activeTextBox = null;
			this.activeHexBox = this.hexBoxLeft;
			this.panelLeft.BackColor = Color.FromArgb(0, 175, 255);
			this.panelLeft.ForeColor = Color.White;
			this.panelRight.BackColor = Color.FromArgb(102, 164, 201);
			this.panelRight.ForeColor = Color.Black;
			this.tableLayoutMiddle.Invalidate();
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000A6C9C File Offset: 0x000A4E9C
		private void txtSaveData_TextChanged(object sender, EventArgs e)
		{
			this.btnApply.Enabled = true;
			bool flag = sender as RichTextBox == this.txtSaveDataLeft;
			if (flag)
			{
				bool flag2 = this.m_DirtyFilesLeft.IndexOf(this.m_cursaveFile) < 0;
				if (flag2)
				{
					this.m_DirtyFilesLeft.Add(this.m_cursaveFile);
				}
			}
			else
			{
				bool flag3 = this.m_DirtyFilesRight.IndexOf(this.m_cursaveFile) < 0;
				if (flag3)
				{
					this.m_DirtyFilesRight.Add(this.m_cursaveFile);
				}
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000A6D24 File Offset: 0x000A4F24
		private void SetLabels()
		{
			this.lblOffset.Text = Resources.lblOffset;
			this.lblCheatCodes.Text = Resources.lblCodes;
			this.lblCheats.Text = Resources.lblCheats;
			this.btnApply.Text = Resources.btnApplyDownload;
			this.btnClose.Text = Resources.btnClose;
			this.Text = Resources.titleAdvEdit;
			this.cbSearchMode.Items.Add(Resources.itmHex);
			this.cbSearchMode.Items.Add(Resources.itmDec);
			this.btnFind.Text = Resources.btnFind;
			this.btnFindPrev.Text = Resources.btnFindPrev;
			this.btnPop.Text = Resources.btnPop;
			this.btnPush.Text = Resources.btnPush;
			this.btnStackAddress.Text = Resources.btnStack;
			this.btnStackSearch.Text = Resources.btnStack;
			this.btnCompare.Text = Resources.btnCompare;
			this.chkSyncScroll.Text = Resources.chkSyncScroll;
			this.chkEnableRight.Text = Resources.chkEnableRight;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000A6E58 File Offset: 0x000A5058
		private void hexBox1_SelectionStartChanged(object sender, EventArgs e)
		{
			long num = (long)this.hexBoxLeft.BytesPerLine * (this.hexBoxLeft.CurrentLine - 1L) + (this.hexBoxLeft.CurrentPositionInLine - 1L);
			this.lblOffsetValue.Text = "0x" + string.Format("{0:X}", num).PadLeft(8, '0');
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000A6EBF File Offset: 0x000A50BF
		protected override void OnClosed(EventArgs e)
		{
			this.hexBoxLeft.Dispose();
			this.hexBoxRight.Dispose();
			base.OnClosed(e);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000A6EE4 File Offset: 0x000A50E4
		private void provider_left_Changed(object sender, EventArgs e)
		{
			this.btnApply.Enabled = true;
			bool flag = this.m_DirtyFilesLeft.IndexOf(this.m_cursaveFile) < 0;
			if (flag)
			{
				this.m_DirtyFilesLeft.Add(this.m_cursaveFile);
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000A6F2C File Offset: 0x000A512C
		protected override void OnPaint(PaintEventArgs e)
		{
			bool resizeInProgress = this._resizeInProgress;
			if (!resizeInProgress)
			{
				base.OnPaint(e);
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000A6F50 File Offset: 0x000A5150
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool resizeInProgress = this._resizeInProgress;
			if (!resizeInProgress)
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
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000A6FF4 File Offset: 0x000A51F4
		private string ApplyUserCheats()
		{
			string text = "";
			file gameFile = this.m_game.GetGameFile(this.m_game.GetTargetGameFolder(), (string)this.cbSaveFiles.SelectedItem);
			bool flag = gameFile != null;
			if (flag)
			{
				List<string> list = this.listViewCheats.Tag as List<string>;
				foreach (object obj in this.listViewCheats.CheckedIndices)
				{
					int num = (int)obj;
					string text2 = list[num];
					text = string.Concat(new string[] { text, "<file><id>", gameFile.id, "</id><filename>", gameFile.filename, "</filename><cheats><code>", text2, "</code></cheats></file>" });
				}
			}
			return text;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000A7100 File Offset: 0x000A5300
		private void txtSaveDataLeft_TextChanged(object sender, EventArgs e)
		{
			this.btnApply.Enabled = true;
			bool flag = this.m_DirtyFilesLeft.IndexOf(this.m_cursaveFile) < 0;
			if (flag)
			{
				this.m_DirtyFilesLeft.Add(this.m_cursaveFile);
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000A7148 File Offset: 0x000A5348
		private Dictionary<long, int> GetDiffs()
		{
			Dictionary<long, int> dictionary = new Dictionary<long, int>();
			byte[] bytes = (this.hexBoxLeft.ByteProvider as DynamicByteProvider).Bytes.GetBytes();
			byte[] array = this.m_saveFilesDataLeft[this.m_cursaveFile];
			for (int i = 0; i < Math.Min(array.Length, bytes.Length); i++)
			{
				bool flag = bytes[i] != array[i];
				if (flag)
				{
					dictionary.Add((long)i, 0);
					long num = (long)i;
					int j = i;
					while (j < Math.Min(array.Length, bytes.Length))
					{
						bool flag2 = bytes[i] != array[i];
						if (!flag2)
						{
							break;
						}
						dictionary[num] = (int)((byte)(dictionary[num] + 1));
						j++;
						i++;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000A7230 File Offset: 0x000A5430
		private string DiffsToCheatCodes(Dictionary<long, int> diffs)
		{
			string text = "";
			byte[] array = (this.hexBoxLeft.ByteProvider as DynamicByteProvider).Bytes.ToArray();
			foreach (long num in diffs.Keys)
			{
				int num2 = diffs[num];
				for (int i = 0; i < (int)Math.Ceiling((double)num2 / 4.0); i++)
				{
					long num3 = num + (long)(i * 4);
					text += string.Format("20{0:6X} {1:8X}\r\n", num3, BitConverter.ToInt32(array, (int)num3));
				}
			}
			return text;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000A730C File Offset: 0x000A550C
		private void btnApply_Click(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(Resources.warnOverwriteAdv, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
			if (!flag)
			{
				string text = this.ApplyUserCheats();
				bool flag2 = !this.m_bTextMode;
				if (flag2)
				{
					this.provider_left.ApplyChanges();
					bool flag3 = this.provider_right != null;
					if (flag3)
					{
						this.provider_right.ApplyChanges();
					}
					bool flag4 = this.m_cursaveFile == null;
					if (flag4)
					{
						this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
					}
					this.m_saveFilesDataLeft[this.m_cursaveFile] = this.provider_left.Bytes.ToArray();
					bool flag5 = this.provider_right != null && this.m_saveFilesDataRight.ContainsKey(this.m_cursaveFile);
					if (flag5)
					{
						this.m_saveFilesDataRight[this.m_cursaveFile] = this.provider_right.Bytes.ToArray();
					}
				}
				else
				{
					container targetGameFolder = this.m_game.GetTargetGameFolder();
					file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
					bool flag6 = gameFile.TextMode == 1;
					if (flag6)
					{
						this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.UTF8.GetBytes(this.txtSaveDataLeft.Text);
					}
					else
					{
						bool flag7 = gameFile.TextMode == 3;
						if (flag7)
						{
							this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.Unicode.GetBytes(this.txtSaveDataLeft.Text);
						}
						else
						{
							this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.ASCII.GetBytes(this.txtSaveDataLeft.Text);
						}
					}
				}
				bool flag8 = this.m_game.GetTargetGameFolder() == null;
				if (flag8)
				{
					Util.ShowMessage(Resources.errSaveData, Resources.msgError);
				}
				else
				{
					container targetGameFolder2 = this.m_game.GetTargetGameFolder();
					List<string> dirtyFilesLeft = this.m_DirtyFilesLeft;
					List<string> list = new List<string>();
					foreach (string text2 in dirtyFilesLeft)
					{
						string text3 = Path.Combine(ZipUtil.GetPs3SeTempFolder(), "_file_" + Path.GetFileName(text2));
						File.WriteAllBytes(text3, this.m_saveFilesDataLeft[Path.GetFileName(text2)]);
						bool flag9 = list.IndexOf(text3) < 0;
						if (flag9)
						{
							list.Add(text3);
						}
					}
					List<string> containerFiles = this.m_game.GetContainerFiles();
					string text4 = this.m_game.LocalSaveFolder.Substring(0, this.m_game.LocalSaveFolder.Length - 4);
					string hash = Util.GetHash(text4);
					bool cache = Util.GetCache(hash);
					string text5 = this.m_game.ToString(list, "encrypt");
					bool flag10 = cache;
					if (flag10)
					{
						containerFiles.Remove(text4);
						text5 = text5.Replace("<pfs><name>" + Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder) + "</name></pfs>", string.Concat(new string[]
						{
							"<pfs><name>",
							Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder),
							"</name><md5>",
							hash,
							"</md5></pfs>"
						}));
					}
					list.AddRange(containerFiles);
					string tempFolder = Util.GetTempFolder();
					string text6 = tempFolder + "ps4_list.xml";
					File.WriteAllText(text6, text5);
					list.Add(text6);
					AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(list.ToArray(), this.m_game, null, "encrypt");
					bool flag11 = advancedSaveUploaderForEncrypt.ShowDialog() == DialogResult.OK;
					if (flag11)
					{
					}
					File.Delete(text6);
					Directory.Delete(ZipUtil.GetPs3SeTempFolder(), true);
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000A76F0 File Offset: 0x000A58F0
		private void btnApply_Click2(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(Resources.warnOverwriteAdv, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
			if (!flag)
			{
				string text = this.ApplyUserCheats();
				bool flag2 = Util.GetRegistryValue("BackupSaves") == "true";
				if (flag2)
				{
				}
				this.provider_left.ApplyChanges();
				bool flag3 = this.provider_right != null;
				if (flag3)
				{
					this.provider_right.ApplyChanges();
				}
				bool flag4 = this.m_cursaveFile == null;
				if (flag4)
				{
					this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
				}
				this.m_saveFilesDataLeft[this.m_cursaveFile] = this.provider_left.Bytes.ToArray();
				bool flag5 = this.provider_right != null && this.m_saveFilesDataRight.ContainsKey(this.m_cursaveFile);
				if (flag5)
				{
					this.m_saveFilesDataRight[this.m_cursaveFile] = this.provider_right.Bytes.ToArray();
				}
				string text2 = Path.Combine(Util.GetTempFolder(), "root");
				List<string> list = new List<string>();
				foreach (string text3 in this.m_saveFilesDataLeft.Keys)
				{
					string text4 = Path.Combine(text2, text3);
					list.Add(text4);
					File.WriteAllBytes(text4, this.m_saveFilesDataLeft[text3]);
				}
				list.Add(Path.Combine(Util.GetTempFolder(), this.m_game.id + ".sav"));
				AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(list.ToArray(), this.m_game, "", "encrypt");
				bool flag6 = advancedSaveUploaderForEncrypt.ShowDialog() == DialogResult.OK;
				if (flag6)
				{
				}
				try
				{
					Directory.Delete(Util.GetTempFolder(), true);
				}
				catch (Exception)
				{
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00063981 File Offset: 0x00061B81
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000021C5 File Offset: 0x000003C5
		private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000A7904 File Offset: 0x000A5B04
		private void hexBox1_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.F3;
			if (flag)
			{
				this.Search(e.Shift, false);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000A7930 File Offset: 0x000A5B30
		private void hexBox2_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.F3;
			if (flag)
			{
				this.Search(e.Shift, false);
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000A795C File Offset: 0x000A5B5C
		private void txtSearchValue_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.Search(false, false);
				this.txtSearchValue.Focus();
				e.SuppressKeyPress = true;
			}
			else
			{
				bool bTextMode = this.m_bTextMode;
				if (!bTextMode)
				{
					bool flag2 = e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.Control || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right;
					if (!flag2)
					{
						bool flag3 = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 && !e.Shift) || (this.cbSearchMode.SelectedIndex == 0 && e.KeyCode >= Keys.A && e.KeyCode <= Keys.F);
						if (!flag3)
						{
							e.SuppressKeyPress = true;
						}
					}
				}
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000A7A6C File Offset: 0x000A5C6C
		private void Search(bool bBackward, bool bStart)
		{
			bool bTextMode = this.m_bTextMode;
			if (bTextMode)
			{
				this.SerachText(bBackward, bStart);
			}
			else
			{
				byte[] bytes = (this.activeHexBox.ByteProvider as DynamicByteProvider).Bytes.GetBytes();
				MemoryStream memoryStream = new MemoryStream(bytes);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				if (bStart)
				{
					binaryReader.BaseStream.Position = 0L;
					this.activeHexBox.SelectionStart = 0L;
					this.activeHexBox.SelectionLength = 0L;
				}
				else
				{
					bool flag = this.activeHexBox.SelectionStart >= 0L;
					if (flag)
					{
						binaryReader.BaseStream.Position = this.activeHexBox.SelectionStart + this.activeHexBox.SelectionLength;
					}
				}
				long num = binaryReader.BaseStream.Position;
				uint num2;
				uint num3;
				int searchValues = this.GetSearchValues(out num2, out num3);
				bool flag2 = searchValues == 0;
				if (flag2)
				{
					Util.ShowMessage(Resources.errInvalidHex, Resources.msgError);
				}
				else
				{
					bool flag3 = searchValues < 0;
					if (flag3)
					{
						Util.ShowMessage(Resources.errIncorrectValue, Resources.msgError);
					}
					else
					{
						while (binaryReader.BaseStream.Position >= 0L && binaryReader.BaseStream.Position < binaryReader.BaseStream.Length + (long)(bBackward ? searchValues : (1 - searchValues)))
						{
							uint num4 = this.ReadValue(binaryReader, searchValues, bBackward);
							bool flag4 = num4 == num2 || num4 == num3;
							if (flag4)
							{
								this.activeHexBox.Select(binaryReader.BaseStream.Position - (long)searchValues, (long)searchValues);
								this.activeHexBox.ScrollByteIntoView(binaryReader.BaseStream.Position);
								this.activeHexBox.Focus();
								break;
							}
							if (bBackward)
							{
								num -= 1L;
								bool flag5 = num < 0L;
								if (flag5)
								{
									break;
								}
							}
							else
							{
								num += 1L;
								bool flag6 = num > binaryReader.BaseStream.Length;
								if (flag6)
								{
									break;
								}
							}
							binaryReader.BaseStream.Position = num;
						}
						binaryReader.Close();
						memoryStream.Close();
						memoryStream.Dispose();
					}
				}
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000A7C98 File Offset: 0x000A5E98
		public int FindMyText(string text, int start, bool bReverse)
		{
			int num = -1;
			bool flag = text.Length > 0 && start >= 0;
			if (flag)
			{
				RichTextBoxFinds richTextBoxFinds = RichTextBoxFinds.None;
				int num2 = this.activeTextBox.Text.Length;
				if (bReverse)
				{
					richTextBoxFinds |= RichTextBoxFinds.Reverse;
					num2 = start - text.Length;
					bool flag2 = num2 < 0;
					if (flag2)
					{
						num2 = this.activeTextBox.Text.Length;
					}
					start = 0;
				}
				int num3 = this.activeTextBox.Find(text, start, num2, richTextBoxFinds);
				bool flag3 = num3 >= 0;
				if (flag3)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000A7D38 File Offset: 0x000A5F38
		private void SerachText(bool bBackward, bool bStart)
		{
			bool flag = this.activeTextBox == null;
			if (flag)
			{
				this.activeTextBox = this.txtSaveDataLeft;
			}
			int num = 0;
			bool flag2 = !bStart;
			if (flag2)
			{
				num = this.activeTextBox.SelectionStart + this.activeTextBox.SelectionLength;
			}
			int num2 = this.FindMyText(this.txtSearchValue.Text, num, bBackward);
			bool flag3 = num2 < 0;
			if (flag3)
			{
				this.activeTextBox.Select(0, 0);
			}
			else
			{
				this.activeTextBox.Focus();
				this.activeTextBox.Select(num2, this.txtSearchValue.Text.Length);
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000A7DDC File Offset: 0x000A5FDC
		private uint ReadValue(BinaryReader reader, int size, bool bBackward)
		{
			if (bBackward)
			{
				bool flag = reader.BaseStream.Position < (long)(2 * size);
				if (flag)
				{
					reader.BaseStream.Position = reader.BaseStream.Length - 1L;
				}
				reader.BaseStream.Position -= (long)(2 * size);
			}
			bool flag2 = size == 1;
			uint num;
			if (flag2)
			{
				num = (uint)reader.ReadByte();
			}
			else
			{
				bool flag3 = size == 2;
				if (flag3)
				{
					num = (uint)reader.ReadUInt16();
				}
				else
				{
					bool flag4 = size == 3;
					if (flag4)
					{
						num = (uint)(((int)reader.ReadUInt16() << 8) | (int)reader.ReadByte());
					}
					else
					{
						num = reader.ReadUInt32();
					}
				}
			}
			return num;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000A7E84 File Offset: 0x000A6084
		private int GetSearchValues(out uint val1, out uint val2)
		{
			uint num;
			int num2;
			try
			{
				string text = this.txtSearchValue.Text;
				bool flag = this.cbSearchMode.SelectedIndex == 0;
				if (flag)
				{
					num = uint.Parse(text, NumberStyles.HexNumber);
					num2 = text.Length;
					bool flag2 = num2 != 1 && num2 != 2 && num2 != 4 && num2 != 6 && num2 != 8;
					if (flag2)
					{
						val1 = (val2 = 0U);
						return 0;
					}
				}
				else
				{
					num = uint.Parse(text);
					num2 = num.ToString("X").Length;
				}
			}
			catch (Exception)
			{
				val1 = 0U;
				val2 = 0U;
				return -1;
			}
			int num3;
			switch (num2)
			{
			case 1:
			case 2:
				num3 = 1;
				break;
			case 3:
			case 4:
				num3 = 2;
				break;
			case 5:
			case 6:
				num3 = 3;
				break;
			case 7:
			case 8:
				num3 = 4;
				break;
			default:
				num3 = 4;
				break;
			}
			val1 = num;
			switch (num3)
			{
			case 2:
				val2 = ((num & 255U) << 8) | ((num & 65280U) >> 8);
				break;
			case 3:
				val2 = ((num & 65280U) << 8) | ((num & 16711680U) >> 8) | (num & 255U);
				break;
			case 4:
				val2 = ((num & 255U) << 24) | ((num & 65280U) << 8) | ((num & 16711680U) >> 8) | ((num & 4278190080U) >> 24);
				break;
			default:
				val2 = num;
				break;
			}
			return num3;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000A800C File Offset: 0x000A620C
		private void btnFind_Click(object sender, EventArgs e)
		{
			this.Search(false, false);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000A69DA File Offset: 0x000A4BDA
		private void button1_Click(object sender, EventArgs e)
		{
			this.Search(true, false);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000A8018 File Offset: 0x000A6218
		private void AdvancedEdit_KeyDown(object sender, KeyEventArgs e)
		{
			bool bTextMode = this.m_bTextMode;
			if (!bTextMode)
			{
				bool flag = e.KeyCode == Keys.G && e.Modifiers == Keys.Control;
				if (flag)
				{
					Goto @goto = new Goto(this.provider_left.Length);
					bool flag2 = @goto.ShowDialog() == DialogResult.OK;
					if (flag2)
					{
						bool flag3 = @goto.AddressLocation < this.provider_left.Length;
						if (flag3)
						{
							this.activeHexBox.ScrollByteIntoView(@goto.AddressLocation);
							this.activeHexBox.Select(@goto.AddressLocation, 1L);
							this.activeHexBox.Invalidate();
						}
						else
						{
							Util.ShowMessage(Resources.errInvalidAddress);
						}
					}
				}
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000A80D4 File Offset: 0x000A62D4
		private void cbSaveFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = !this.m_bTextMode && this.provider_left != null && this.provider_left.Length > 0L;
			if (flag)
			{
				this.provider_left.ApplyChanges();
			}
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			bool flag2 = !string.IsNullOrEmpty(this.m_cursaveFile) && this.m_saveFilesDataLeft.ContainsKey(this.m_cursaveFile);
			if (flag2)
			{
				file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
				bool flag3 = gameFile.TextMode == 0 && this.provider_left != null;
				if (flag3)
				{
					this.m_saveFilesDataLeft[this.m_cursaveFile] = this.provider_left.Bytes.ToArray();
				}
				else
				{
					bool flag4 = gameFile.TextMode == 2;
					if (flag4)
					{
						this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.ASCII.GetBytes(this.txtSaveDataLeft.Text);
					}
					else
					{
						bool flag5 = gameFile.TextMode == 3;
						if (flag5)
						{
							this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.Unicode.GetBytes(this.txtSaveDataLeft.Text);
						}
						else
						{
							this.m_saveFilesDataLeft[this.m_cursaveFile] = Encoding.UTF8.GetBytes(this.txtSaveDataLeft.Text);
						}
					}
				}
				bool flag6 = this.provider_right != null && this.m_saveFilesDataRight.ContainsKey(this.m_cursaveFile);
				if (flag6)
				{
					this.m_saveFilesDataRight[this.m_cursaveFile] = this.provider_right.Bytes.ToArray();
				}
			}
			this.FillCheats();
			this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
			this.m_bTextMode = false;
			file gameFile2 = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
			bool flag7 = gameFile2.TextMode == 0;
			if (flag7)
			{
				this.hexBoxLeft.Visible = true;
				this.hexBoxRight.Visible = true;
				this.txtSaveDataLeft.Visible = false;
				this.txtSaveDataRight.Visible = false;
				this.RefreshHexBoxes();
				this.tableLayoutMiddle.RowStyles[0].Height = 20f;
				this.lblOffset.Visible = true;
				this.lblOffsetValue.Visible = true;
				this.lstCheatCodes.Enabled = true;
				this.lstSearchAddresses.Enabled = true;
				this.lstSearchVal.Enabled = true;
				this.cbSearchMode.Enabled = true;
				this.btnCompare.Enabled = true;
				this.txtAddress.Enabled = true;
				this.btnStackAddress.Enabled = true;
				this.btnStackSearch.Enabled = true;
				this.txtAddress.Enabled = true;
				this.btnGo.Enabled = true;
			}
			else
			{
				this.tableLayoutMiddle.RowStyles[0].Height = 0f;
				this.txtSaveDataLeft.Visible = true;
				this.txtSaveDataRight.Visible = true;
				this.hexBoxLeft.Visible = false;
				this.hexBoxRight.Visible = false;
				this.lstCheatCodes.Enabled = false;
				this.lstSearchAddresses.Enabled = false;
				this.lstSearchVal.Enabled = false;
				this.cbSearchMode.Enabled = false;
				this.btnCompare.Enabled = false;
				this.txtAddress.Enabled = false;
				this.btnStackSearch.Enabled = false;
				this.btnStackAddress.Enabled = false;
				this.txtAddress.Enabled = false;
				this.btnGo.Enabled = false;
				bool flag8 = gameFile2.TextMode == 1;
				if (flag8)
				{
					this.txtSaveDataLeft.Text = Encoding.UTF8.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
				}
				else
				{
					bool flag9 = gameFile2.TextMode == 3;
					if (flag9)
					{
						this.txtSaveDataLeft.Text = Encoding.Unicode.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
					}
					else
					{
						this.txtSaveDataLeft.Text = Encoding.ASCII.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
					}
				}
				this.m_bTextMode = true;
				this.lblOffset.Visible = false;
				this.lblOffsetValue.Visible = false;
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000A8550 File Offset: 0x000A6750
		private void hexBox2_SelectionStartChanged(object sender, EventArgs e)
		{
			long num = (long)this.hexBoxRight.BytesPerLine * (this.hexBoxRight.CurrentLine - 1L) + (this.hexBoxRight.CurrentPositionInLine - 1L);
			this.lblOffsetValue.Text = "0x" + string.Format("{0:X}", num).PadLeft(8, '0');
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000A85B8 File Offset: 0x000A67B8
		private void hexBox2_Scroll(object sender, EventArgs e)
		{
			bool @checked = this.chkSyncScroll.Checked;
			if (@checked)
			{
				this.hexBoxLeft.PerformScrollToLine((long)this.hexBoxRight.VScrollBar.Value);
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000A85F4 File Offset: 0x000A67F4
		private void hexBox1_Scroll(object sender, EventArgs e)
		{
			bool @checked = this.chkSyncScroll.Checked;
			if (@checked)
			{
				this.hexBoxRight.PerformScrollToLine((long)this.hexBoxLeft.VScrollBar.Value);
				this._lastTsLeft = Environment.TickCount;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000A8640 File Offset: 0x000A6840
		private void btnPush_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			string cacheFolder = Util.GetCacheFolder(this.m_game, text);
			Directory.CreateDirectory(cacheFolder);
			foreach (string text2 in this.m_saveFilesDataLeft.Keys)
			{
				File.WriteAllBytes(Path.Combine(cacheFolder, text2), this.m_saveFilesDataLeft[text2]);
			}
			this.lstCache.Items.Add(text);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000A86EC File Offset: 0x000A68EC
		private void FillCheats()
		{
			this.listViewCheats.Tag = new List<string>();
			this.listViewCheats.Items.Clear();
			this.lstCheatCodes.Items.Clear();
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			foreach (file file in targetGameFolder.files._files)
			{
				bool flag = Util.IsMatch((string)this.cbSaveFiles.SelectedItem, file.filename);
				if (flag)
				{
					foreach (cheat cheat in file.GetAllCheats())
					{
						bool flag2 = cheat.id != "-1";
						if (!flag2)
						{
							ListViewItem listViewItem = new ListViewItem(cheat.name);
							this.listViewCheats.Items.Add(cheat.name);
							(this.listViewCheats.Tag as List<string>).Add(cheat.ToEditableString());
						}
					}
				}
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000A8848 File Offset: 0x000A6A48
		private void FillCache()
		{
			string cacheFolder = Util.GetCacheFolder(this.m_game, null);
			bool flag = Directory.Exists(cacheFolder);
			if (flag)
			{
				string[] directories = Directory.GetDirectories(cacheFolder);
				foreach (string text in directories)
				{
					this.lstCache.Items.Add(Path.GetFileName(text));
				}
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000A88AC File Offset: 0x000A6AAC
		private void btnPop_Click(object sender, EventArgs e)
		{
			bool flag = string.IsNullOrEmpty((string)this.lstCache.SelectedItem);
			if (flag)
			{
				Util.ShowMessage(Resources.msgChooseCache);
			}
			else
			{
				string text = (string)this.lstCache.SelectedItem;
				string cacheFolder = Util.GetCacheFolder(this.m_game, (string)this.lstCache.SelectedItem);
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
				file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
				this.LoadCache(cacheFolder);
				bool flag2 = gameFile.TextMode == 0;
				if (flag2)
				{
					this.RefreshHexBoxes();
				}
				else
				{
					this.RefreshTextBoxes(gameFile.TextMode);
				}
				this.cbSaveFiles_SelectedIndexChanged(null, null);
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000A8984 File Offset: 0x000A6B84
		private void RefreshTextBoxes(int textMode)
		{
			string text = (string)this.cbSaveFiles.SelectedItem;
			bool flag = this.m_saveFilesDataLeft.ContainsKey(text);
			if (flag)
			{
				bool flag2 = textMode == 1;
				if (flag2)
				{
					this.txtSaveDataLeft.Text = Encoding.UTF8.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
				}
				bool flag3 = textMode == 3;
				if (flag3)
				{
					this.txtSaveDataLeft.Text = Encoding.Unicode.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
				}
				else
				{
					this.txtSaveDataLeft.Text = Encoding.ASCII.GetString(this.m_saveFilesDataLeft[this.m_cursaveFile]);
				}
			}
			bool flag4 = this.m_saveFilesDataRight.ContainsKey(text);
			if (flag4)
			{
				bool flag5 = textMode == 1;
				if (flag5)
				{
					this.txtSaveDataRight.Text = Encoding.UTF8.GetString(this.m_saveFilesDataRight[this.m_cursaveFile]);
				}
				bool flag6 = textMode == 3;
				if (flag6)
				{
					this.txtSaveDataRight.Text = Encoding.Unicode.GetString(this.m_saveFilesDataRight[this.m_cursaveFile]);
				}
				else
				{
					this.txtSaveDataRight.Text = Encoding.ASCII.GetString(this.m_saveFilesDataRight[this.m_cursaveFile]);
				}
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000A8AE4 File Offset: 0x000A6CE4
		private void RefreshHexBoxes()
		{
			string text = (string)this.cbSaveFiles.SelectedItem;
			bool flag = this.m_saveFilesDataLeft.ContainsKey(text);
			if (flag)
			{
				this.provider_left = new DynamicByteProvider(this.m_saveFilesDataLeft[text]);
				this.provider_left.Changed += new EventHandler<ByteProviderChanged>(this.provider_left_Changed);
				this.hexBoxLeft.ByteProvider = this.provider_left;
			}
			bool flag2 = this.m_saveFilesDataRight.ContainsKey(text);
			if (flag2)
			{
				this.provider_right = new DynamicByteProvider(this.m_saveFilesDataRight[text]);
				this.provider_right.Changed += new EventHandler<ByteProviderChanged>(this.provider_right_Changed);
				this.hexBoxRight.ByteProvider = this.provider_right;
			}
			this.hexBoxLeft.HexCasing = HexCasing.Upper;
			this.hexBoxRight.HexCasing = HexCasing.Upper;
			this.hexBoxLeft.LineInfoVisible = true;
			this.hexBoxRight.LineInfoVisible = true;
			this.hexBoxLeft.StringViewVisible = true;
			this.hexBoxRight.StringViewVisible = true;
			this.hexBoxLeft.BytesPerLine = 16;
			this.hexBoxRight.BytesPerLine = 16;
			this.hexBoxLeft.UseFixedBytesPerLine = true;
			this.hexBoxRight.UseFixedBytesPerLine = true;
			this.hexBoxLeft.VScrollBarVisible = true;
			this.hexBoxRight.VScrollBarVisible = true;
			this.hexBoxLeft.HScrollBarVisible = true;
			this.hexBoxRight.HScrollBarVisible = true;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000A8C64 File Offset: 0x000A6E64
		private void provider_right_Changed(object sender, EventArgs e)
		{
			this.btnApply.Enabled = true;
			bool flag = this.m_DirtyFilesRight.IndexOf(this.m_cursaveFile) < 0;
			if (flag)
			{
				this.m_DirtyFilesRight.Add(this.m_cursaveFile);
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000A8CAC File Offset: 0x000A6EAC
		private void LoadCache(string folder)
		{
			string[] files = Directory.GetFiles(folder);
			bool flag = this.activeHexBox == this.hexBoxLeft || this.activeTextBox == this.txtSaveDataLeft;
			if (flag)
			{
				this.m_saveFilesDataLeft.Clear();
				foreach (string text in files)
				{
					this.m_saveFilesDataLeft.Add(text.Replace(folder + Path.DirectorySeparatorChar.ToString(), ""), File.ReadAllBytes(text));
				}
				this.hexBoxLeft.Tag = folder;
			}
			else
			{
				this.m_saveFilesDataRight.Clear();
				foreach (string text2 in files)
				{
					this.m_saveFilesDataRight.Add(text2.Replace(folder + Path.DirectorySeparatorChar.ToString(), ""), File.ReadAllBytes(text2));
				}
				this.hexBoxRight.Tag = folder;
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000A8DBC File Offset: 0x000A6FBC
		private void btnStackSearch_Click(object sender, EventArgs e)
		{
			bool flag = this.cbSearchMode.SelectedIndex == 0;
			if (flag)
			{
				this.lstSearchVal.Items.Add("0x" + this.txtSearchValue.Text);
			}
			else
			{
				this.lstSearchVal.Items.Add(string.Concat(long.Parse(this.txtSearchValue.Text)));
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000A8E34 File Offset: 0x000A7034
		private void btnStackAddress_Click(object sender, EventArgs e)
		{
			try
			{
				this.lstSearchAddresses.Items.Add(long.Parse(this.txtAddress.Text, NumberStyles.HexNumber).ToString("X"));
			}
			catch (Exception)
			{
				Util.ShowMessage("Please enter valid hexadecimal");
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000021C5 File Offset: 0x000003C5
		private void lstSearchVal_MouseClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000A8E9C File Offset: 0x000A709C
		private void lstSearchVal_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.lstSearchVal.SelectedItem != null;
			if (flag)
			{
				string text = (string)this.lstSearchVal.SelectedItem;
				bool flag2 = text.StartsWith("0x");
				if (flag2)
				{
					this.cbSearchMode.SelectedIndex = 0;
					this.txtSearchValue.Text = text.Substring(2);
				}
				else
				{
					this.txtSearchValue.Text = this.lstSearchVal.SelectedItem as string;
				}
				this.Search(false, this.activeHexBox.SelectionLength == 0L);
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000A8F38 File Offset: 0x000A7138
		private void lstSearchAddresses_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.lstSearchAddresses.SelectedItem != null;
			if (flag)
			{
				long num = long.Parse((string)this.lstSearchAddresses.SelectedItem, NumberStyles.HexNumber);
				this.txtAddress.Text = this.lstSearchAddresses.SelectedItem as string;
				this.Goto(num);
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000A8F9C File Offset: 0x000A719C
		private void btnNew_Click(object sender, EventArgs e)
		{
			ListViewItem listViewItem = new ListViewItem("");
			this.listViewCheats.Items.Add(listViewItem);
			listViewItem.BeginEdit();
			listViewItem.Selected = true;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000A8FD8 File Offset: 0x000A71D8
		private void btnApplyCodes_Click(object sender, EventArgs e)
		{
			bool flag = this.listViewCheats.SelectedItems.Count == 1;
			if (flag)
			{
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000A800C File Offset: 0x000A620C
		private void btnFind_Click_1(object sender, EventArgs e)
		{
			this.Search(false, false);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000A9000 File Offset: 0x000A7200
		private void Goto(long address)
		{
			bool flag = address < this.provider_left.Length;
			if (flag)
			{
				this.activeHexBox.ScrollByteIntoView(address);
				this.activeHexBox.Select(address, 1L);
				this.activeHexBox.Invalidate();
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000A904C File Offset: 0x000A724C
		private void btnGo_Click(object sender, EventArgs e)
		{
			try
			{
				this.Goto(long.Parse(this.txtAddress.Text, NumberStyles.HexNumber));
			}
			catch
			{
				Util.ShowMessage("Please enter valid address.");
			}
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000A909C File Offset: 0x000A729C
		private void lstSearchVal_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Delete;
			if (flag)
			{
				bool flag2 = this.lstSearchVal.SelectedItems.Count == 1;
				if (flag2)
				{
					this.lstSearchVal.Items.Remove(this.lstSearchVal.SelectedItems[0]);
				}
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000A90F8 File Offset: 0x000A72F8
		private void lstSearchAddresses_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Delete;
			if (flag)
			{
				bool flag2 = this.lstSearchAddresses.SelectedItems.Count == 1;
				if (flag2)
				{
					this.lstSearchAddresses.Items.Remove(this.lstSearchAddresses.SelectedItems[0]);
				}
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000A9154 File Offset: 0x000A7354
		private void SaveUserCheats()
		{
			string text = "<usercheats></usercheats>";
			string text2 = Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm.USER_CHEATS_FILE;
			bool flag = File.Exists(text2);
			if (flag)
			{
				text = File.ReadAllText(text2);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(text);
			bool flag2 = false;
			for (int i = 0; i < xmlDocument["usercheats"].ChildNodes.Count; i++)
			{
				bool flag3 = this.m_game.id == xmlDocument["usercheats"].ChildNodes[i].Attributes["id"].Value;
				if (flag3)
				{
					flag2 = true;
				}
			}
			bool flag4 = !flag2;
			if (flag4)
			{
				XmlElement xmlElement = xmlDocument.CreateElement("game");
				xmlElement.SetAttribute("id", this.m_game.id);
				xmlDocument["usercheats"].AppendChild(xmlElement);
			}
			for (int j = 0; j < xmlDocument["usercheats"].ChildNodes.Count; j++)
			{
				bool flag5 = this.m_game.id == xmlDocument["usercheats"].ChildNodes[j].Attributes["id"].Value;
				if (flag5)
				{
					XmlElement xmlElement2 = xmlDocument["usercheats"].ChildNodes[j] as XmlElement;
					xmlElement2.InnerXml = "";
					foreach (file file in this.m_game.GetTargetGameFolder().files._files)
					{
						XmlElement xmlElement3 = xmlDocument.CreateElement("file");
						xmlElement3.SetAttribute("name", Path.GetFileName(file.filename));
						xmlElement2.AppendChild(xmlElement3);
						foreach (cheat cheat in file.GetAllCheats())
						{
							bool flag6 = cheat.id == "-1";
							if (flag6)
							{
								XmlElement xmlElement4 = xmlDocument.CreateElement("cheat");
								xmlElement4.SetAttribute("desc", cheat.name);
								xmlElement4.SetAttribute("comment", cheat.note);
								xmlElement3.AppendChild(xmlElement4);
								XmlElement xmlElement5 = xmlDocument.CreateElement("code");
								xmlElement5.InnerText = cheat.code;
								xmlElement4.AppendChild(xmlElement5);
							}
						}
					}
				}
			}
			xmlDocument.Save(text2);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000A9478 File Offset: 0x000A7678
		private void btnSaveCodes_Click(object sender, EventArgs e)
		{
			file gameFile = this.m_game.GetGameFile(this.m_game.GetTargetGameFolder(), (string)this.cbSaveFiles.SelectedItem);
			List<cheat> list = new List<cheat>();
			foreach (cheat cheat in gameFile.GetAllCheats())
			{
				bool flag = cheat.id == "-1";
				if (flag)
				{
					list.Add(cheat);
				}
			}
			foreach (cheat cheat2 in list)
			{
				gameFile.Cheats.Remove(cheat2);
			}
			foreach (object obj in this.listViewCheats.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				bool flag2 = string.IsNullOrEmpty((string)listViewItem.Tag);
				if (!flag2)
				{
					cheat cheat3 = new cheat("-1", listViewItem.Text, "");
					cheat3.code = (string)listViewItem.Tag;
					gameFile.Cheats.Add(cheat3);
				}
			}
			this.SaveUserCheats();
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000A9608 File Offset: 0x000A7808
		private void btnCompare_Click(object sender, EventArgs e)
		{
			bool visible = this.diffResults.Visible;
			if (!visible)
			{
				bool flag = this.hexBoxLeft.ByteProvider == null || this.hexBoxRight.ByteProvider == null || (this.hexBoxLeft.ByteProvider as DynamicByteProvider).Bytes == null || (this.hexBoxRight.ByteProvider as DynamicByteProvider).Bytes == null;
				if (!flag)
				{
					byte[] bytes = (this.hexBoxLeft.ByteProvider as DynamicByteProvider).Bytes.GetBytes();
					byte[] bytes2 = (this.hexBoxRight.ByteProvider as DynamicByteProvider).Bytes.GetBytes();
					Dictionary<long, byte> dictionary = new Dictionary<long, byte>();
					for (int i = 0; i < Math.Min(bytes2.Length, bytes.Length); i++)
					{
						bool flag2 = bytes[i] != bytes2[i];
						if (flag2)
						{
							dictionary.Add((long)i, 0);
							long num = (long)i;
							int j = i;
							while (j < Math.Min(bytes2.Length, bytes.Length))
							{
								bool flag3 = bytes[i] != bytes2[i];
								if (!flag3)
								{
									break;
								}
								dictionary[num] += 1;
								j++;
								i++;
							}
						}
					}
					foreach (long num2 in dictionary.Keys)
					{
						this.hexBoxLeft.SelectAddresses = dictionary;
						this.hexBoxRight.SelectAddresses = dictionary;
					}
					bool flag4 = this.diffResults != null;
					if (flag4)
					{
						this.diffResults.Differences = dictionary;
						this.diffResults.Show(this);
						this.hexBoxLeft.SelectAddresses = dictionary;
						this.hexBoxRight.SelectAddresses = dictionary;
					}
				}
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000A9804 File Offset: 0x000A7A04
		private void chkEnableRight_CheckedChanged(object sender, EventArgs e)
		{
			this.tableMain.BackColor = Color.FromArgb(0, 138, 213);
			this.tableLayoutMiddle.BackColor = Color.FromArgb(0, 138, 213);
			this.tableRight.BackColor = Color.FromArgb(0, 138, 213);
			this.tableTop.BackColor = Color.FromArgb(0, 138, 213);
			this._resizeInProgress = true;
			this.tableMain.SuspendLayout();
			bool @checked = this.chkEnableRight.Checked;
			if (@checked)
			{
				this.MinimumSize = new Size(Math.Min(1230, Screen.PrimaryScreen.WorkingArea.Width), this.MinimumSize.Height);
				base.Width = Math.Min(1230, Screen.PrimaryScreen.WorkingArea.Width);
				this.tableLayoutMiddle.ColumnStyles[0].SizeType = SizeType.Percent;
				this.tableLayoutMiddle.ColumnStyles[1].SizeType = SizeType.Percent;
				this.tableLayoutMiddle.ColumnStyles[0].Width = 50f;
				this.tableLayoutMiddle.ColumnStyles[1].Width = 50f;
				this.panelRight.BringToFront();
			}
			else
			{
				this.MinimumSize = new Size(800, this.MinimumSize.Height);
				base.Width = Math.Min(890, Screen.PrimaryScreen.WorkingArea.Width);
				this.tableLayoutMiddle.ColumnStyles[0].SizeType = SizeType.Percent;
				this.tableLayoutMiddle.ColumnStyles[1].SizeType = SizeType.Percent;
				this.tableLayoutMiddle.ColumnStyles[0].Width = 100f;
				this.tableLayoutMiddle.ColumnStyles[1].Width = 0f;
			}
			base.CenterToScreen();
			this.tableMain.ResumeLayout();
			this.tableMain.Refresh();
			this.tableMain.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.tableLayoutMiddle.BackColor = Color.Transparent;
			this.tableRight.BackColor = Color.Transparent;
			this.tableTop.BackColor = Color.Transparent;
			this._resizeInProgress = false;
			base.Invalidate(true);
		}

		// Token: 0x04000CDE RID: 3294
		private DynamicByteProvider provider_left;

		// Token: 0x04000CDF RID: 3295
		private DynamicByteProvider provider_right;

		// Token: 0x04000CE0 RID: 3296
		private game m_game;

		// Token: 0x04000CE1 RID: 3297
		private bool m_bTextMode;

		// Token: 0x04000CE2 RID: 3298
		private Dictionary<string, byte[]> m_saveFilesDataLeft;

		// Token: 0x04000CE3 RID: 3299
		private Dictionary<string, byte[]> m_saveFilesDataRight;

		// Token: 0x04000CE4 RID: 3300
		private const int MAX_CHEAT_CODES = 128;

		// Token: 0x04000CE5 RID: 3301
		private List<string> m_DirtyFilesLeft;

		// Token: 0x04000CE6 RID: 3302
		private List<string> m_DirtyFilesRight;

		// Token: 0x04000CE7 RID: 3303
		private string m_cursaveFile;

		// Token: 0x04000CE8 RID: 3304
		private bool _resizeInProgress = false;

		// Token: 0x04000CE9 RID: 3305
		private HexBox activeHexBox;

		// Token: 0x04000CEA RID: 3306
		private RichTextBox activeTextBox;

		// Token: 0x04000CEB RID: 3307
		private int _lastTsLeft = 0;

		// Token: 0x04000CEC RID: 3308
		private DiffResults diffResults = new DiffResults();
	}
}
