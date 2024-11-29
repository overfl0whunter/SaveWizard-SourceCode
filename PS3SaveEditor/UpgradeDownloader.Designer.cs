namespace PS3SaveEditor
{
	// Token: 0x020001E7 RID: 487
	public partial class UpgradeDownloader : global::System.Windows.Forms.Form
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x000A2494 File Offset: 0x000A0694
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000A24CC File Offset: 0x000A06CC
		private void InitializeComponent()
		{
			this.pbProgress = new global::PS3SaveEditor.PS4ProgressBar();
			this.lblStatus = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.pbProgress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(8), global::PS3SaveEditor.Util.ScaleSize(56));
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(409, 23));
			this.pbProgress.TabIndex = 0;
			this.lblStatus.ForeColor = global::System.Drawing.Color.White;
			this.lblStatus.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(39));
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(143, 13));
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Downloading latest version...";
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.pbProgress);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(432, 131));
			this.panel1.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(452, 155));
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpgradeDownloader";
			base.ShowIcon = false;
			this.Text = "Downloading Latest Version";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000CC1 RID: 3265
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000CC2 RID: 3266
		private global::PS3SaveEditor.PS4ProgressBar pbProgress;

		// Token: 0x04000CC3 RID: 3267
		private global::System.Windows.Forms.Label lblStatus;

		// Token: 0x04000CC4 RID: 3268
		private global::System.Windows.Forms.Panel panel1;
	}
}
