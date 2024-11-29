namespace PS3SaveEditor
{
	// Token: 0x020001AF RID: 431
	public partial class AdvancedSaveUploaderForEncrypt : global::System.Windows.Forms.Form
	{
		// Token: 0x06001652 RID: 5714 RVA: 0x0006EC54 File Offset: 0x0006CE54
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		private void InitializeComponent()
		{
			this.saveUploadDownloder1 = new global::PS3SaveEditor.SaveUploadDownloder();
			base.SuspendLayout();
			this.saveUploadDownloder1.Action = null;
			this.saveUploadDownloder1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.saveUploadDownloder1.FilePath = null;
			this.saveUploadDownloder1.Files = null;
			this.saveUploadDownloder1.Game = null;
			this.saveUploadDownloder1.IsUpload = false;
			this.saveUploadDownloder1.ListResult = null;
			this.saveUploadDownloder1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(12));
			this.saveUploadDownloder1.Name = "saveUploadDownloder1";
			this.saveUploadDownloder1.OrderedEntries = null;
			this.saveUploadDownloder1.OutputFolder = null;
			this.saveUploadDownloder1.Profile = null;
			this.saveUploadDownloder1.ProgressBar = null;
			this.saveUploadDownloder1.SaveId = null;
			this.saveUploadDownloder1.StatusLabel = null;
			this.saveUploadDownloder1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(472, 175));
			base.Controls.Add(this.saveUploadDownloder1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AdvancedSaveUploaderForEncrypt";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Save Downloader";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.AdvancedSaveUploaderForEncrypt_FormClosing);
			base.ResumeLayout(false);
		}

		// Token: 0x04000A55 RID: 2645
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000A56 RID: 2646
		private global::PS3SaveEditor.SaveUploadDownloder saveUploadDownloder1;
	}
}
