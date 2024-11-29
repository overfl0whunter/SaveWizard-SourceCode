namespace PS3SaveEditor
{
	// Token: 0x020001D3 RID: 467
	public partial class ResignInfo : global::System.Windows.Forms.Form
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x0008EA48 File Offset: 0x0008CC48
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0008EA80 File Offset: 0x0008CC80
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.ResignInfo));
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.chkDontShow = new global::System.Windows.Forms.CheckBox();
			this.textBox1 = new global::System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.chkDontShow);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(566, 184));
			this.panel1.TabIndex = 0;
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(243), global::PS3SaveEditor.Util.ScaleSize(153));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.chkDontShow.AutoSize = true;
			this.chkDontShow.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(154));
			this.chkDontShow.Name = "chkDontShow";
			this.chkDontShow.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(179, 17));
			this.chkDontShow.TabIndex = 1;
			this.chkDontShow.Text = "Do not show this message again";
			this.chkDontShow.UseVisualStyleBackColor = true;
			this.textBox1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(10));
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(539, 135));
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = componentResourceManager.GetString("textBox1.Text");
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(586, 204));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ResignInfo";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Important Information";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000BE0 RID: 3040
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BE1 RID: 3041
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000BE2 RID: 3042
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000BE3 RID: 3043
		private global::System.Windows.Forms.CheckBox chkDontShow;

		// Token: 0x04000BE4 RID: 3044
		private global::System.Windows.Forms.Label textBox1;
	}
}
