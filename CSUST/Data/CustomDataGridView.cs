using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CSUST.Data
{
	// Token: 0x020000EA RID: 234
	public class CustomDataGridView : DataGridView
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0003E64C File Offset: 0x0003C84C
		public CustomDataGridView()
		{
			this.brSelection = new SolidBrush(Color.FromArgb(0, 175, 255));
			this.borderPen = new Pen(Color.FromArgb(168, 173, 179), 1f);
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000ABB RID: 2747 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		// (remove) Token: 0x06000ABC RID: 2748 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		[Description("Set cell background color, Colindex -1 denotes any col.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<CellBackColorEventArgs> SetCellBackColor;

		// Token: 0x06000ABD RID: 2749 RVA: 0x0003E710 File Offset: 0x0003C910
		private void DrawCellBackColor(DataGridViewCellPaintingEventArgs e)
		{
			bool flag = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
			if (flag)
			{
				base.OnCellPainting(e);
			}
			else
			{
				bool flag2 = this.SetCellBackColor == null;
				if (flag2)
				{
					base.OnCellPainting(e);
				}
				else
				{
					CellBackColorEventArgs cellBackColorEventArgs = new CellBackColorEventArgs(e.RowIndex, e.ColumnIndex);
					this.SetCellBackColor(this, cellBackColorEventArgs);
					bool flag3 = cellBackColorEventArgs.BackColor == Color.Empty;
					if (flag3)
					{
						base.OnCellPainting(e);
					}
					else
					{
						using (SolidBrush solidBrush = new SolidBrush(cellBackColorEventArgs.BackColor))
						{
							using (Pen pen = new Pen(base.GridColor))
							{
								Rectangle rectangle = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);
								Rectangle rectangle2 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);
								rectangle.X--;
								rectangle.Y--;
								rectangle2.Width--;
								rectangle2.Height--;
								e.Graphics.DrawRectangle(pen, rectangle);
								e.Graphics.FillRectangle(solidBrush, rectangle2);
							}
						}
						e.PaintContent(e.CellBounds);
						e.Handled = true;
					}
				}
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(this.borderPen, 0, 0, base.Width - 1, base.Height - 1);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0003E8E0 File Offset: 0x0003CAE0
		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			bool flag = e.RowIndex >= 0 && e.ColumnIndex >= 0;
			if (flag)
			{
				bool flag2 = base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null && (base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() == "GameFile" || base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() == "CheatGroup" || base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() == "NoCheats");
				if (flag2)
				{
					bool flag3 = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
					if (flag3)
					{
						e.Graphics.FillRectangle(this.brSelection, e.CellBounds);
						e.Graphics.DrawLine(Pens.Gray, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right, e.CellBounds.Top);
						e.Graphics.DrawLine(Pens.Gray, e.CellBounds.Left, e.CellBounds.Bottom, e.CellBounds.Right, e.CellBounds.Bottom);
						e.Handled = true;
					}
					else
					{
						bool flag4 = base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() == "NoCheats";
						if (flag4)
						{
							e.Graphics.DrawRectangle(Pens.White, new Rectangle(e.CellBounds.Left, e.CellBounds.Top + 1, e.CellBounds.Width, e.CellBounds.Height - 2));
							e.Graphics.FillRectangle(Brushes.White, new Rectangle(e.CellBounds.Left, e.CellBounds.Top + 1, e.CellBounds.Width, e.CellBounds.Height - 2));
						}
						else
						{
							bool flag5 = base.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() == "CheatGroup";
							if (flag5)
							{
								e.Graphics.FillRectangle(Brushes.White, e.CellBounds.Left, e.CellBounds.Top + 1, e.CellBounds.Width, e.CellBounds.Height - 1);
							}
							else
							{
								e.Graphics.DrawRectangle(Pens.Gray, e.CellBounds);
								e.Graphics.FillRectangle(Brushes.Gray, e.CellBounds);
							}
						}
						e.Handled = true;
					}
				}
				else
				{
					bool flag6 = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
					if (flag6)
					{
						e.Graphics.FillRectangle(this.brSelection, e.CellBounds);
					}
					else
					{
						Brush brush = new SolidBrush(e.CellStyle.BackColor);
						e.Graphics.FillRectangle(brush, e.CellBounds);
						brush.Dispose();
					}
					e.Graphics.DrawLine(Pens.Gray, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right, e.CellBounds.Top);
					e.Graphics.DrawLine(Pens.Gray, e.CellBounds.Left, e.CellBounds.Bottom, e.CellBounds.Right, e.CellBounds.Bottom);
					e.PaintContent(e.CellBounds);
					e.Handled = true;
				}
			}
			else
			{
				base.OnCellPainting(e);
			}
		}

		// Token: 0x040005F2 RID: 1522
		private Pen borderPen;

		// Token: 0x040005F3 RID: 1523
		private Brush brSelection;
	}
}
