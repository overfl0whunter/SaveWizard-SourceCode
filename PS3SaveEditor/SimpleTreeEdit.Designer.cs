namespace PS3SaveEditor
{
	// Token: 0x020001E5 RID: 485
	public partial class SimpleTreeEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x000A1564 File Offset: 0x0009F764
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000A159C File Offset: 0x0009F79C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.SimpleTreeEdit));
			this.lblGameName = new global::System.Windows.Forms.Label();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.addCodeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editCodeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.deleteCodeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.lblProfile = new global::System.Windows.Forms.Label();
			this.cbProfile = new global::System.Windows.Forms.ComboBox();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.btnApply = new global::System.Windows.Forms.Button();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.lblGameName.AutoSize = true;
			this.lblGameName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(17), global::PS3SaveEditor.Util.ScaleSize(9));
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblGameName.TabIndex = 0;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.addCodeToolStripMenuItem, this.editCodeToolStripMenuItem, this.deleteCodeToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(139, 70));
			this.contextMenuStrip1.Opening += new global::System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			this.addCodeToolStripMenuItem.Name = "addCodeToolStripMenuItem";
			this.addCodeToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(138, 22));
			this.addCodeToolStripMenuItem.Text = "Add Code";
			this.editCodeToolStripMenuItem.Name = "editCodeToolStripMenuItem";
			this.editCodeToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(138, 22));
			this.editCodeToolStripMenuItem.Text = "Edit Code";
			this.editCodeToolStripMenuItem.Click += new global::System.EventHandler(this.editCodeToolStripMenuItem_Click);
			this.deleteCodeToolStripMenuItem.Name = "deleteCodeToolStripMenuItem";
			this.deleteCodeToolStripMenuItem.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(138, 22));
			this.deleteCodeToolStripMenuItem.Text = "Delete Code";
			this.deleteCodeToolStripMenuItem.Click += new global::System.EventHandler(this.deleteCodeToolStripMenuItem_Click);
			this.panel1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.lblProfile);
			this.panel1.Controls.Add(this.cbProfile);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(11));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(633, 276));
			this.panel1.TabIndex = 1;
			this.panel2.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.panel2.BackColor = global::System.Drawing.SystemColors.AppWorkspace;
			this.panel2.ContextMenuStrip = this.contextMenuStrip1;
			this.panel2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(13));
			this.panel2.Name = "panel2";
			this.panel2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(609, 230));
			this.panel2.TabIndex = 18;
			this.lblProfile.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.lblProfile.AutoSize = true;
			this.lblProfile.ForeColor = global::System.Drawing.Color.White;
			this.lblProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(71), global::PS3SaveEditor.Util.ScaleSize(249));
			this.lblProfile.Name = "lblProfile";
			this.lblProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(39, 13));
			this.lblProfile.TabIndex = 17;
			this.lblProfile.Text = "Profile:";
			this.cbProfile.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.cbProfile.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProfile.FormattingEnabled = true;
			this.cbProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(117), global::PS3SaveEditor.Util.ScaleSize(245));
			this.cbProfile.Name = "cbProfile";
			this.cbProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(112, 21));
			this.cbProfile.TabIndex = 16;
			this.btnClose.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnClose.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnClose.ForeColor = global::System.Drawing.Color.White;
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(315), global::PS3SaveEditor.Util.ScaleSize(248));
			this.btnClose.MaximumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnClose.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnApply.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnApply.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnApply.ForeColor = global::System.Drawing.Color.White;
			this.btnApply.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(255), global::PS3SaveEditor.Util.ScaleSize(248));
			this.btnApply.MaximumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnApply.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 23));
			this.btnApply.TabIndex = 10;
			this.btnApply.Text = "Patch && Download Save";
			this.btnApply.UseVisualStyleBackColor = false;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "file.png");
			this.imageList1.Images.SetKeyName(1, "group.png");
			this.imageList1.Images.SetKeyName(2, "cheat.png");
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(654, 294));
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.lblGameName);
			this.ForeColor = global::System.Drawing.Color.Black;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			this.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(550, 330));
			base.Name = "SimpleTreeEdit";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Simple Edit";
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000CAD RID: 3245
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000CAE RID: 3246
		private global::System.Windows.Forms.Label lblGameName;

		// Token: 0x04000CAF RID: 3247
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000CB0 RID: 3248
		private global::System.Windows.Forms.ToolStripMenuItem editCodeToolStripMenuItem;

		// Token: 0x04000CB1 RID: 3249
		private global::System.Windows.Forms.ToolStripMenuItem deleteCodeToolStripMenuItem;

		// Token: 0x04000CB2 RID: 3250
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000CB3 RID: 3251
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000CB4 RID: 3252
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x04000CB5 RID: 3253
		private global::System.Windows.Forms.ComboBox cbProfile;

		// Token: 0x04000CB6 RID: 3254
		private global::System.Windows.Forms.Label lblProfile;

		// Token: 0x04000CB7 RID: 3255
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000CB8 RID: 3256
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000CB9 RID: 3257
		private global::System.Windows.Forms.ToolStripMenuItem addCodeToolStripMenuItem;
	}
}
