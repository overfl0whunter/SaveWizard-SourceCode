namespace PS3SaveEditor
{
	// Token: 0x020001DE RID: 478
	public partial class ResignFilesUplaoder : global::System.Windows.Forms.Form
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x00093C1C File Offset: 0x00091E1C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00093C54 File Offset: 0x00091E54
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
			this.saveUploadDownloder1.ListResult = null;
			this.saveUploadDownloder1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(12));
			this.saveUploadDownloder1.Name = "saveUploadDownloder1";
			this.saveUploadDownloder1.OrderedEntries = null;
			this.saveUploadDownloder1.OutputFolder = null;
			this.saveUploadDownloder1.Profile = null;
			this.saveUploadDownloder1.SaveId = null;
			this.saveUploadDownloder1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(473, 175));
			base.Controls.Add(this.saveUploadDownloder1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ResignFilesUplaoder";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ResignFilesUplaoder";
			base.ResumeLayout(false);
		}

		// Token: 0x04000C40 RID: 3136
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C41 RID: 3137
		private global::PS3SaveEditor.SaveUploadDownloder saveUploadDownloder1;
	}
}
