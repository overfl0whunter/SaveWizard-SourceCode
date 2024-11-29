namespace PS3SaveEditor
{
	// Token: 0x020001E0 RID: 480
	public partial class RSSForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060018D1 RID: 6353 RVA: 0x00094858 File Offset: 0x00092A58
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00094890 File Offset: 0x00092A90
		private void InitializeComponent()
		{
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.lblTitle = new global::System.Windows.Forms.Label();
			this.lnkTitle = new global::System.Windows.Forms.LinkLabel();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.lstRSSFeeds = new global::System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.lstRSSFeeds);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(604, 420));
			this.panel1.TabIndex = 0;
			this.panel2.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.panel2.BackColor = global::System.Drawing.Color.White;
			this.panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.lblTitle);
			this.panel2.Controls.Add(this.lnkTitle);
			this.panel2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(97));
			this.panel2.Name = "panel2";
			this.panel2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(581, 275));
			this.panel2.TabIndex = 2;
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(12f), global::System.Drawing.FontStyle.Bold);
			this.lblTitle.ForeColor = global::System.Drawing.Color.Black;
			this.lblTitle.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(9), global::PS3SaveEditor.Util.ScaleSize(10));
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(46, 24));
			this.lblTitle.TabIndex = 4;
			this.lblTitle.Text = "      ";
			this.lnkTitle.AutoSize = true;
			this.lnkTitle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(12f), global::System.Drawing.FontStyle.Bold);
			this.lnkTitle.ForeColor = global::System.Drawing.Color.White;
			this.lnkTitle.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(9), global::PS3SaveEditor.Util.ScaleSize(10));
			this.lnkTitle.Name = "lnkTitle";
			this.lnkTitle.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 24));
			this.lnkTitle.TabIndex = 2;
			this.lnkTitle.TabStop = true;
			this.lnkTitle.Text = "     ";
			this.btnOk.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(263), global::PS3SaveEditor.Util.ScaleSize(389));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = false;
			this.lstRSSFeeds.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.lstRSSFeeds.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 255);
			this.lstRSSFeeds.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular);
			this.lstRSSFeeds.FormattingEnabled = true;
			this.lstRSSFeeds.IntegralHeight = false;
			this.lstRSSFeeds.ItemHeight = global::PS3SaveEditor.Util.ScaleSize(16);
			this.lstRSSFeeds.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.lstRSSFeeds.Name = "lstRSSFeeds";
			this.lstRSSFeeds.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(581, 82));
			this.lstRSSFeeds.ScrollAlwaysVisible = true;
			this.lstRSSFeeds.TabIndex = 0;
			this.htmlPanel1 = new global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
			this.htmlPanel1.BackColor = global::System.Drawing.Color.White;
			this.htmlPanel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(-1), global::PS3SaveEditor.Util.ScaleSize(40));
			this.htmlPanel1.Name = "htmlPanel1";
			this.htmlPanel1.Text = "<p>Rss Feed</p>";
			this.htmlPanel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(581, 234));
			this.panel2.Controls.Add(this.htmlPanel1);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(624, 442));
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RSSForm";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "RSSForm";
			base.ResizeEnd += new global::System.EventHandler(this.RSSForm_ResizeEnd);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000C4B RID: 3147
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C4C RID: 3148
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000C4D RID: 3149
		private global::System.Windows.Forms.ListBox lstRSSFeeds;

		// Token: 0x04000C4E RID: 3150
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000C4F RID: 3151
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000C50 RID: 3152
		private global::System.Windows.Forms.LinkLabel lnkTitle;

		// Token: 0x04000C51 RID: 3153
		private global::System.Windows.Forms.Label lblTitle;

		// Token: 0x04000C52 RID: 3154
		private global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
	}
}
