namespace PS3SaveEditor
{
	// Token: 0x020001C5 RID: 453
	public partial class ChooseBackup : global::System.Windows.Forms.Form
	{
		// Token: 0x06001733 RID: 5939 RVA: 0x00072A0C File Offset: 0x00070C0C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00072A44 File Offset: 0x00070C44
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnRestore = new global::System.Windows.Forms.Button();
			this.lstBackups = new global::System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.lblGameName = new global::System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnRestore);
			this.panel1.Controls.Add(this.lstBackups);
			this.panel1.Controls.Add(this.lblGameName);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(262, 240));
			this.panel1.TabIndex = 0;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(140), global::PS3SaveEditor.Util.ScaleSize(201));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnRestore.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(39), global::PS3SaveEditor.Util.ScaleSize(201));
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnRestore.TabIndex = 2;
			this.btnRestore.Text = "Restore";
			this.btnRestore.UseVisualStyleBackColor = true;
			this.btnRestore.Click += new global::System.EventHandler(this.btnRestore_Click);
			this.lstBackups.ContextMenuStrip = this.contextMenuStrip1;
			this.lstBackups.FormattingEnabled = true;
			this.lstBackups.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(30));
			this.lstBackups.Name = "lstBackups";
			this.lstBackups.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(235, 160));
			this.lstBackups.TabIndex = 1;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.deleteToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(108, 26));
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(107, 22));
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new global::System.EventHandler(this.deleteToolStripMenuItem_Click);
			this.lblGameName.AutoSize = true;
			this.lblGameName.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.lblGameName.ForeColor = global::System.Drawing.Color.White;
			this.lblGameName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(9), global::PS3SaveEditor.Util.ScaleSize(9));
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblGameName.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(284, 262));
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChooseBackup";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ChooseBackup";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000AB1 RID: 2737
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000AB2 RID: 2738
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000AB3 RID: 2739
		private global::System.Windows.Forms.ListBox lstBackups;

		// Token: 0x04000AB4 RID: 2740
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000AB5 RID: 2741
		private global::System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

		// Token: 0x04000AB6 RID: 2742
		private global::System.Windows.Forms.Label lblGameName;

		// Token: 0x04000AB7 RID: 2743
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000AB8 RID: 2744
		private global::System.Windows.Forms.Button btnRestore;
	}
}
