namespace PS3SaveEditor
{
	// Token: 0x020001E4 RID: 484
	public partial class SimpleSaveUploader : global::System.Windows.Forms.Form
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x0009EC70 File Offset: 0x0009CE70
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0009ECA8 File Offset: 0x0009CEA8
		private void InitializeComponent()
		{
			this.saveUploadDownloder1 = new global::PS3SaveEditor.SaveUploadDownloder();
			base.SuspendLayout();
			this.saveUploadDownloder1.Action = null;
			this.saveUploadDownloder1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
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
			this.saveUploadDownloder1.SaveId = null;
			this.saveUploadDownloder1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(472, 175));
			base.Controls.Add(this.saveUploadDownloder1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SimpleSaveUploader";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Simple Save Patcher";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.SimpleSaveUploader_FormClosing);
			base.ResumeLayout(false);
		}

		// Token: 0x04000CA6 RID: 3238
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000CA7 RID: 3239
		private global::PS3SaveEditor.SaveUploadDownloder saveUploadDownloder1;
	}
}
