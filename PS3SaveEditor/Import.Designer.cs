namespace PS3SaveEditor
{
	// Token: 0x020001CC RID: 460
	public partial class Import : global::System.Windows.Forms.Form
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x00075000 File Offset: 0x00073200
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00075038 File Offset: 0x00073238
		private void InitializeComponent()
		{
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.dgImport = new global::CSUST.Data.CustomDataGridView();
			this.ColSelect = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GameName = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SysVer = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Account = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dgImport).BeginInit();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnImport);
			this.panel1.Controls.Add(this.dgImport);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(12));
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(580, 353));
			this.panel1.TabIndex = 1;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(277), global::PS3SaveEditor.Util.ScaleSize(322));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnImport.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(199), global::PS3SaveEditor.Util.ScaleSize(322));
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnImport.TabIndex = 2;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.dgImport.AllowUserToAddRows = false;
			this.dgImport.AllowUserToDeleteRows = false;
			this.dgImport.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgImport.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.ColSelect, this.GameName, this.SysVer, this.Account });
			this.dgImport.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.dgImport.MultiSelect = false;
			this.dgImport.Name = "dgImport";
			this.dgImport.ReadOnly = true;
			this.dgImport.RowHeadersVisible = false;
			this.dgImport.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgImport.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(555, 302));
			this.dgImport.TabIndex = 1;
			this.ColSelect.HeaderText = "";
			this.ColSelect.Name = "Select";
			this.ColSelect.ReadOnly = true;
			this.ColSelect.Width = global::PS3SaveEditor.Util.ScaleSize(20);
			this.GameName.HeaderText = "Game Name";
			this.GameName.Name = "GameName";
			this.GameName.ReadOnly = true;
			this.GameName.Resizable = global::System.Windows.Forms.DataGridViewTriState.True;
			this.GameName.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.GameName.Width = global::PS3SaveEditor.Util.ScaleSize(350);
			this.SysVer.HeaderText = "Sys. Ver";
			this.SysVer.Name = "SysVer";
			this.SysVer.ReadOnly = true;
			this.SysVer.Width = global::PS3SaveEditor.Util.ScaleSize(80);
			this.Account.HeaderText = "Profile/PSN ID";
			this.Account.Name = "Account";
			this.Account.ReadOnly = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(600, 373));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Import";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import";
			this.panel1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dgImport).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000AD1 RID: 2769
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000AD2 RID: 2770
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000AD3 RID: 2771
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000AD4 RID: 2772
		private global::System.Windows.Forms.Button btnImport;

		// Token: 0x04000AD5 RID: 2773
		private global::CSUST.Data.CustomDataGridView dgImport;

		// Token: 0x04000AD6 RID: 2774
		private global::System.Windows.Forms.DataGridViewTextBoxColumn ColSelect;

		// Token: 0x04000AD7 RID: 2775
		private global::System.Windows.Forms.DataGridViewTextBoxColumn GameName;

		// Token: 0x04000AD8 RID: 2776
		private global::System.Windows.Forms.DataGridViewTextBoxColumn SysVer;

		// Token: 0x04000AD9 RID: 2777
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Account;
	}
}
