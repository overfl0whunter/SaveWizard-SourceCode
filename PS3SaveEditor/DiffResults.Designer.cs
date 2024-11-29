namespace PS3SaveEditor
{
	// Token: 0x020001CB RID: 459
	public partial class DiffResults : global::System.Windows.Forms.Form
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x00073A94 File Offset: 0x00071C94
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00073ACC File Offset: 0x00071CCC
		private void InitializeComponent()
		{
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.StartAddress = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EndAddress = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Bytes = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnClose = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.StartAddress, this.EndAddress, this.Bytes });
			this.dataGridView1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(322, 213));
			this.dataGridView1.TabIndex = 0;
			this.StartAddress.HeaderText = "Start Address";
			this.StartAddress.Name = "StartAddress";
			this.StartAddress.ReadOnly = true;
			this.EndAddress.HeaderText = "End Address";
			this.EndAddress.Name = "EndAddress";
			this.EndAddress.ReadOnly = true;
			this.EndAddress.Width = 120;
			this.Bytes.HeaderText = "Bytes";
			this.Bytes.Name = "Bytes";
			this.Bytes.ReadOnly = true;
			this.Bytes.Width = 90;
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(26), global::PS3SaveEditor.Util.ScaleSize(239));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(346, 274));
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DiffResults";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "DiffResults";
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000AC4 RID: 2756
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000AC5 RID: 2757
		private global::System.Windows.Forms.DataGridView dataGridView1;

		// Token: 0x04000AC6 RID: 2758
		private global::System.Windows.Forms.DataGridViewTextBoxColumn StartAddress;

		// Token: 0x04000AC7 RID: 2759
		private global::System.Windows.Forms.DataGridViewTextBoxColumn EndAddress;

		// Token: 0x04000AC8 RID: 2760
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Bytes;

		// Token: 0x04000AC9 RID: 2761
		private global::System.Windows.Forms.Button btnClose;
	}
}
