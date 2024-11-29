namespace PS3SaveEditor.Diagnostic
{
	// Token: 0x020001F6 RID: 502
	public partial class DiagnosticForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001BE7 RID: 7143 RVA: 0x000B13D4 File Offset: 0x000AF5D4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000B140C File Offset: 0x000AF60C
		private void InitializeComponent()
		{
			this.infoBox = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.infoBox.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.infoBox.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.infoBox.Multiline = true;
			this.infoBox.Name = "infoBox";
			this.infoBox.ReadOnly = true;
			this.infoBox.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(352, 150));
			this.infoBox.TabIndex = 0;
			this.infoBox.Text = "test";
			bool flag = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag)
			{
				this.infoBox.ContextMenu = new global::PS3SaveEditor.SubControls.MacContextMenu(this.infoBox).GetMenu();
			}
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(376, 174));
			base.Controls.Add(this.infoBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.Name = "DiagnosticForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Diagnostic info";
			base.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000D2D RID: 3373
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000D2E RID: 3374
		private global::System.Windows.Forms.TextBox infoBox;
	}
}
