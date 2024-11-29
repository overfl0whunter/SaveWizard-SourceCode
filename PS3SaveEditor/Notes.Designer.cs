namespace PS3SaveEditor
{
	// Token: 0x020001D0 RID: 464
	public partial class Notes : global::System.Windows.Forms.Form
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x0008CEA0 File Offset: 0x0008B0A0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0008CED8 File Offset: 0x0008B0D8
		private void InitializeComponent()
		{
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.htmlPanel1 = new global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.htmlPanel1);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(12));
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(548, 256));
			this.panel1.TabIndex = 0;
			this.htmlPanel1.BackColor = global::System.Drawing.Color.White;
			this.htmlPanel1.BaseStylesheet = null;
			this.htmlPanel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.htmlPanel1.Name = "htmlPanel1";
			this.htmlPanel1.Text = "<p> </p>";
			this.htmlPanel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(524, 206));
			this.htmlPanel1.TabIndex = 2;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(236), global::PS3SaveEditor.Util.ScaleSize(225));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(568, 276));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Notes";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Notes";
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000BBE RID: 3006
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BBF RID: 3007
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000BC0 RID: 3008
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000BC1 RID: 3009
		private global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
	}
}
