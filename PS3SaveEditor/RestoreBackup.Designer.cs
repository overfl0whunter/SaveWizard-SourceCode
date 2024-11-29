namespace PS3SaveEditor
{
	// Token: 0x020001DF RID: 479
	public partial class RestoreBackup : global::System.Windows.Forms.Form
	{
		// Token: 0x060018C5 RID: 6341 RVA: 0x0009409C File Offset: 0x0009229C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000940D4 File Offset: 0x000922D4
		private void InitializeComponent()
		{
			this.pbProgress = new global::PS3SaveEditor.PS4ProgressBar();
			this.lblProgress = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.pbProgress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(27));
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(257, 20));
			this.pbProgress.TabIndex = 0;
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(5), global::PS3SaveEditor.Util.ScaleSize(9));
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblProgress.TabIndex = 1;
			this.panel1.Controls.Add(this.lblProgress);
			this.panel1.Controls.Add(this.pbProgress);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(9), global::PS3SaveEditor.Util.ScaleSize(9));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(263, 73));
			this.panel1.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(284, 94));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RestoreBackup";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Restore Backup";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000C47 RID: 3143
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C48 RID: 3144
		private global::PS3SaveEditor.PS4ProgressBar pbProgress;

		// Token: 0x04000C49 RID: 3145
		private global::System.Windows.Forms.Label lblProgress;

		// Token: 0x04000C4A RID: 3146
		private global::System.Windows.Forms.Panel panel1;
	}
}
