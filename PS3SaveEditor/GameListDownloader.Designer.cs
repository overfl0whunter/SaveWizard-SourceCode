namespace PS3SaveEditor
{
	// Token: 0x020001DA RID: 474
	public partial class GameListDownloader : global::System.Windows.Forms.Form
	{
		// Token: 0x06001888 RID: 6280 RVA: 0x00091ABC File Offset: 0x0008FCBC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00091AF4 File Offset: 0x0008FCF4
		private void InitializeComponent()
		{
			this.lblStatus = new global::System.Windows.Forms.Label();
			this.pbProgress = new global::PS3SaveEditor.PS4ProgressBar();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.lblStatus.ForeColor = global::System.Drawing.Color.White;
			this.lblStatus.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(26));
			this.lblStatus.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(4), 0, global::PS3SaveEditor.Util.ScaleSize(4), 0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(325, 17));
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "Please wait while the game list being downloaded..";
			this.pbProgress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(57));
			this.pbProgress.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4));
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(536, 23));
			this.pbProgress.TabIndex = 1;
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.pbProgress);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(15));
			this.panel1.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(569, 128));
			this.panel1.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(604, 160));
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4), global::PS3SaveEditor.Util.ScaleSize(4));
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GameListDownloader";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Downloading Games List from Server";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.GameListDownloader_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000C24 RID: 3108
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C25 RID: 3109
		private global::System.Windows.Forms.Label lblStatus;

		// Token: 0x04000C26 RID: 3110
		private global::PS3SaveEditor.PS4ProgressBar pbProgress;

		// Token: 0x04000C27 RID: 3111
		private global::System.Windows.Forms.Panel panel1;
	}
}
