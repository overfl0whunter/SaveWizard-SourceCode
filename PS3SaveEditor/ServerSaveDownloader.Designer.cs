namespace PS3SaveEditor
{
	// Token: 0x020001D6 RID: 470
	public partial class ServerSaveDownloader : global::System.Windows.Forms.Form
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x00090004 File Offset: 0x0008E204
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0009003C File Offset: 0x0008E23C
		private void InitializeComponent()
		{
			this.saveUploadDownloder1 = new global::PS3SaveEditor.SaveUploadDownloder();
			base.SuspendLayout();
			this.saveUploadDownloder1.Action = null;
			this.saveUploadDownloder1.BackColor = global::System.Drawing.Color.FromArgb(200, 100, 10);
			this.saveUploadDownloder1.FilePath = null;
			this.saveUploadDownloder1.Files = null;
			this.saveUploadDownloder1.Game = null;
			this.saveUploadDownloder1.IsUpload = false;
			this.saveUploadDownloder1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(12));
			this.saveUploadDownloder1.Name = "saveUploadDownloder1";
			this.saveUploadDownloder1.OutputFolder = null;
			this.saveUploadDownloder1.Profile = null;
			this.saveUploadDownloder1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(473, 175));
			base.Controls.Add(this.saveUploadDownloder1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ServerSaveDownloader";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "ServerSaveDownloader";
			base.ResumeLayout(false);
		}

		// Token: 0x04000BF8 RID: 3064
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BF9 RID: 3065
		private global::PS3SaveEditor.SaveUploadDownloder saveUploadDownloder1;
	}
}
