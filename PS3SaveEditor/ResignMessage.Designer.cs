namespace PS3SaveEditor
{
	// Token: 0x020001D4 RID: 468
	public partial class ResignMessage : global::System.Windows.Forms.Form
	{
		// Token: 0x06001838 RID: 6200 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0008EF10 File Offset: 0x0008D110
		private void InitializeComponent()
		{
			this.chkDeleteExisting = new global::System.Windows.Forms.CheckBox();
			this.lblResignSuccess = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.chkDeleteExisting.AutoSize = true;
			this.chkDeleteExisting.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(71), global::PS3SaveEditor.Util.ScaleSize(50));
			this.chkDeleteExisting.Name = "chkDeleteExisting";
			this.chkDeleteExisting.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(137, 17));
			this.chkDeleteExisting.TabIndex = 0;
			this.chkDeleteExisting.Text = "Delete the original save";
			this.chkDeleteExisting.UseVisualStyleBackColor = true;
			this.lblResignSuccess.AutoSize = true;
			this.lblResignSuccess.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(86), global::PS3SaveEditor.Util.ScaleSize(17));
			this.lblResignSuccess.Name = "lblResignSuccess";
			this.lblResignSuccess.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(110, 13));
			this.lblResignSuccess.TabIndex = 1;
			this.lblResignSuccess.Text = "Re-signing successful";
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.chkDeleteExisting);
			this.panel1.Controls.Add(this.lblResignSuccess);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(277, 115));
			this.panel1.TabIndex = 2;
			this.btnOK.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(102), global::PS3SaveEditor.Util.ScaleSize(83));
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(297, 135));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ResignMessage";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "ResignMessage";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000BE6 RID: 3046
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BE7 RID: 3047
		private global::System.Windows.Forms.CheckBox chkDeleteExisting;

		// Token: 0x04000BE8 RID: 3048
		private global::System.Windows.Forms.Label lblResignSuccess;

		// Token: 0x04000BE9 RID: 3049
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000BEA RID: 3050
		private global::System.Windows.Forms.Button btnOK;
	}
}
