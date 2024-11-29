namespace PS3SaveEditor.SubControls
{
	// Token: 0x020001F1 RID: 497
	public partial class WaitingForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x000ACA8C File Offset: 0x000AAC8C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000ACAC4 File Offset: 0x000AACC4
		private void InitializeComponent()
		{
			this.waitLabel = new global::System.Windows.Forms.Label();
			this.prBar = new global::PS3SaveEditor.PS4ProgressBar();
			base.SuspendLayout();
			this.waitLabel.AutoSize = true;
			this.waitLabel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(9), global::PS3SaveEditor.Util.ScaleSize(9));
			this.waitLabel.Name = "waitLabel";
			this.waitLabel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(187, 13));
			this.waitLabel.TabIndex = 0;
			this.waitLabel.Text = "Please wait. File opening in progress...";
			this.prBar.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(43));
			this.prBar.Name = "prBar";
			this.prBar.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(346, 23));
			this.prBar.Style = global::System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prBar.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(370, 89));
			base.ControlBox = false;
			base.Controls.Add(this.prBar);
			base.Controls.Add(this.waitLabel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WaitingForm";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Please wait. File opening in progress...";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000D20 RID: 3360
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000D21 RID: 3361
		private global::System.Windows.Forms.Label waitLabel;

		// Token: 0x04000D22 RID: 3362
		private global::PS3SaveEditor.PS4ProgressBar prBar;
	}
}
