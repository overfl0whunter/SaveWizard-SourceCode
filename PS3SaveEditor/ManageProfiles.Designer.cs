namespace PS3SaveEditor
{
	// Token: 0x020001DB RID: 475
	public partial class ManageProfiles : global::System.Windows.Forms.Form
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x00092E14 File Offset: 0x00091014
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00092E4C File Offset: 0x0009104C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			dataGridViewCellStyle.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.Font);
			this.dgProfiles = new global::System.Windows.Forms.DataGridView();
			this.dgProfiles.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.dgProfiles.Font);
			this.Removable = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this._Name = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this._Name.MaxInputLength = 32;
			this.ID = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStrip1.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.contextMenuStrip1.Font);
			this.deleteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			((global::System.ComponentModel.ISupportInitialize)this.dgProfiles).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.dgProfiles.AllowUserToAddRows = false;
			this.dgProfiles.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.Color.FromArgb(0, 175, 255);
			this.dgProfiles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgProfiles.RowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgProfiles.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
			this.dgProfiles.BackgroundColor = global::System.Drawing.Color.FromArgb(175, 175, 175);
			this.dgProfiles.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._Name.MaxInputLength = 32;
			this.dgProfiles.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this._Name, this.Removable, this.ID });
			this.dgProfiles.ColumnHeadersDefaultCellStyle.Font = global::PS3SaveEditor.Util.GetFontForPlatform(this.dgProfiles.ColumnHeadersDefaultCellStyle.Font);
			this.dgProfiles.ContextMenuStrip = this.contextMenuStrip1;
			this.dgProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.dgProfiles.Name = "dgProfiles";
			this.dgProfiles.RowHeadersVisible = false;
			this.dgProfiles.RowTemplate.Height = global::PS3SaveEditor.Util.ScaleSize(24);
			this.dgProfiles.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgProfiles.ShowEditingIcon = false;
			this.dgProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(356, 202));
			this.dgProfiles.TabIndex = 0;
			this._Name.HeaderText = "Name";
			this._Name.Name = "_Name";
			this._Name.Width = global::PS3SaveEditor.Util.ScaleSize(132);
			this._Name.MaxInputLength = 32;
			this.Removable.HeaderText = "Removable";
			this.Removable.Name = "Removable";
			this.Removable.Width = global::PS3SaveEditor.Util.ScaleSize(95);
			this.Removable.ReadOnly = true;
			this.ID.HeaderText = "PSN ID";
			this.ID.Name = "ID";
			this.ID.Width = global::PS3SaveEditor.Util.ScaleSize(126);
			this.ID.ReadOnly = true;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.deleteToolStripMenuItem, this.renameToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(118, 48));
			this.contextMenuStrip1.Opening += new global::System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(117, 22));
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new global::System.EventHandler(this.deleteToolStripMenuItem_Click);
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(117, 22));
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new global::System.EventHandler(this.renameToolStripMenuItem_Click);
			this.btnSave.ForeColor = global::System.Drawing.Color.Black;
			this.btnSave.BackColor = global::System.Drawing.SystemColors.ButtonFace;
			this.btnSave.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(118), global::PS3SaveEditor.Util.ScaleSize(217));
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = global::PS3SaveEditor.Resources.Resources.btnApply;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.btnClose.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.ForeColor = global::System.Drawing.Color.White;
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(197), global::PS3SaveEditor.Util.ScaleSize(217));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.dgProfiles);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(380, 244));
			this.panel1.TabIndex = 4;
			base.AcceptButton = this.btnSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.CancelButton = this.btnClose;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(400, 264));
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ManageProfiles";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Manage Profiles";
			base.Load += new global::System.EventHandler(this.ManageProfiles_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dgProfiles).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000C2E RID: 3118
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C2F RID: 3119
		private global::System.Windows.Forms.DataGridView dgProfiles;

		// Token: 0x04000C30 RID: 3120
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x04000C31 RID: 3121
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000C32 RID: 3122
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000C33 RID: 3123
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000C34 RID: 3124
		private global::System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

		// Token: 0x04000C35 RID: 3125
		private global::System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;

		// Token: 0x04000C36 RID: 3126
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Removable;

		// Token: 0x04000C37 RID: 3127
		private global::System.Windows.Forms.DataGridViewTextBoxColumn _Name;

		// Token: 0x04000C38 RID: 3128
		private global::System.Windows.Forms.DataGridViewTextBoxColumn ID;
	}
}
