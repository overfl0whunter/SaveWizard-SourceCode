using System;
using System.Drawing;

namespace CSUST.Data
{
	// Token: 0x020000EB RID: 235
	public class CellBackColorEventArgs : EventArgs
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public CellBackColorEventArgs(int row, int col)
		{
			this.m_RowIndex = row;
			this.m_ColIndex = col;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
		public int RowIndex
		{
			get
			{
				return this.m_RowIndex;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0003EDB8 File Offset: 0x0003CFB8
		public int ColIndex
		{
			get
			{
				return this.m_ColIndex;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
		public Color BackColor
		{
			get
			{
				return this.m_BackColor;
			}
			set
			{
				this.m_BackColor = value;
			}
		}

		// Token: 0x040005F5 RID: 1525
		private int m_RowIndex;

		// Token: 0x040005F6 RID: 1526
		private int m_ColIndex;

		// Token: 0x040005F7 RID: 1527
		private Color m_BackColor = Color.Empty;
	}
}
