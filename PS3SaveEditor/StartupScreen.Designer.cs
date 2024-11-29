namespace PS3SaveEditor
{
	// Token: 0x020001E6 RID: 486
	public partial class StartupScreen : global::System.Windows.Forms.Form
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x000A1F6C File Offset: 0x000A016C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000A1FA4 File Offset: 0x000A01A4
		private void InitializeComponent()
		{
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnAccept = new global::System.Windows.Forms.Button();
			this.htmlPanel1 = new global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
			base.SuspendLayout();
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(270), global::PS3SaveEditor.Util.ScaleSize(190));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(124, 26));
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "I DO NOT ACCEPT";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnAccept.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(169), global::PS3SaveEditor.Util.ScaleSize(190));
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 26));
			this.btnAccept.TabIndex = 3;
			this.btnAccept.Text = "I ACCEPT";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new global::System.EventHandler(this.btnAccept_Click);
			this.htmlPanel1.BackColor = global::System.Drawing.Color.Transparent;
			this.htmlPanel1.BaseStylesheet = null;
			this.htmlPanel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(8));
			this.htmlPanel1.Name = "htmlPanel1";
			this.htmlPanel1.Text = "<p> </p>";
			bool flag = this.hasUpdate;
			if (flag)
			{
				this.htmlPanel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(530, 250));
			}
			else
			{
				this.htmlPanel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(530, 180));
			}
			this.htmlPanel1.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(554, 222));
			bool flag2 = !this.hasUpdate;
			if (flag2)
			{
				base.Controls.Add(this.btnCancel);
				base.Controls.Add(this.btnAccept);
			}
			base.Controls.Add(this.htmlPanel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "StartupScreen";
			base.ShowIcon = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "StartupScreen";
			base.ResumeLayout(false);
		}

		// Token: 0x04000CBA RID: 3258
		private bool hasUpdate = false;

		// Token: 0x04000CBB RID: 3259
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000CBC RID: 3260
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000CBD RID: 3261
		private global::System.Windows.Forms.Button btnAccept;

		// Token: 0x04000CBE RID: 3262
		private global::TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
	}
}
