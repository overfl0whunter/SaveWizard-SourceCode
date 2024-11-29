using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001CB RID: 459
	public partial class DiffResults : Form
	{
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600174A RID: 5962 RVA: 0x000737C4 File Offset: 0x000719C4
		// (remove) Token: 0x0600174B RID: 5963 RVA: 0x000737FC File Offset: 0x000719FC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler OnDiffRowSelected;

		// Token: 0x17000606 RID: 1542
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x00073834 File Offset: 0x00071A34
		public Dictionary<long, byte> Differences
		{
			set
			{
				this.dataGridView1.Rows.Clear();
				foreach (long num in value.Keys)
				{
					int num2 = this.dataGridView1.Rows.Add();
					this.dataGridView1.Rows[num2].Cells[0].Value = num.ToString("X8");
					bool flag = value[num] != 1;
					if (flag)
					{
						this.dataGridView1.Rows[num2].Cells[1].Value = (num + (long)((ulong)value[num])).ToString("X8");
					}
					this.dataGridView1.Rows[num2].Cells[2].Value = value[num].ToString("X2");
				}
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00073960 File Offset: 0x00071B60
		public DiffResults()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleDiffResults;
			this.dataGridView1.Columns[0].HeaderText = Resources.colStartAddr;
			this.dataGridView1.Columns[1].HeaderText = Resources.colEndAddr;
			this.dataGridView1.Columns[2].HeaderText = Resources.colBytes;
			base.CenterToScreen();
			this.dataGridView1.RowStateChanged += this.dataGridView1_RowStateChanged;
			base.FormClosing += this.DiffResults_FormClosing;
			this.btnClose.Text = Resources.btnClose;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00073A38 File Offset: 0x00071C38
		private void DiffResults_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			base.Hide();
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00073A4C File Offset: 0x00071C4C
		private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			bool flag = e.StateChanged == DataGridViewElementStates.Selected;
			if (flag)
			{
				bool flag2 = this.OnDiffRowSelected != null;
				if (flag2)
				{
					this.OnDiffRowSelected(sender, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00073A89 File Offset: 0x00071C89
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Hide();
		}
	}
}
