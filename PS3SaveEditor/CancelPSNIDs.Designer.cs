namespace PS3SaveEditor
{
	// Token: 0x020001B1 RID: 433
	public partial class CancelPSNIDs : global::System.Windows.Forms.Form
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006F300 File Offset: 0x0006D500
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.CancelPSNIDs));
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.UserName = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.dataGridView1);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(260, 179));
			this.panel1.TabIndex = 0;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = global::System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.UserName });
			this.dataGridView1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(15));
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(237, 150));
			this.dataGridView1.TabIndex = 0;
			this.UserName.HeaderText = "UserName";
			this.UserName.Name = "UserName";
			this.UserName.ReadOnly = true;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(62), global::PS3SaveEditor.Util.ScaleSize(195));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(143), global::PS3SaveEditor.Util.ScaleSize(195));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(284, 228));
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CancelPSNIDs";
			base.ShowInTaskbar = false;
			this.Text = "CancelPSNIDs";
			base.Load += new global::System.EventHandler(this.CancelPSNIDs_Load);
			this.panel1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000A5A RID: 2650
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000A5B RID: 2651
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000A5C RID: 2652
		private global::System.Windows.Forms.DataGridView dataGridView1;

		// Token: 0x04000A5D RID: 2653
		private global::System.Windows.Forms.DataGridViewTextBoxColumn UserName;

		// Token: 0x04000A5E RID: 2654
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000A5F RID: 2655
		private global::System.Windows.Forms.Button btnClose;
	}
}
