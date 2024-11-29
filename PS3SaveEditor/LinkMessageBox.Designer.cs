namespace PS3SaveEditor
{
	// Token: 0x020001CD RID: 461
	public partial class LinkMessageBox : global::System.Windows.Forms.Form
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x00075644 File Offset: 0x00073844
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007567C File Offset: 0x0007387C
		private void InitializeComponent()
		{
			this.lblMessage = new global::System.Windows.Forms.Label();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			this.btnOK = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.lblMessage.AutoSize = false;
			this.lblMessage.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(11.25f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblMessage.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(16));
			this.lblMessage.MaximumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(520, 140));
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(520, 180));
			this.lblMessage.TabIndex = 0;
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(15), global::PS3SaveEditor.Util.ScaleSize(108));
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(84, 13));
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Contact Support";
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(214), global::PS3SaveEditor.Util.ScaleSize(108));
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.CancelButton = this.btnOK;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(503, 143));
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.lblMessage);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LinkMessageBox";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LinkMessageBox";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000ADB RID: 2779
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000ADC RID: 2780
		private global::System.Windows.Forms.Label lblMessage;

		// Token: 0x04000ADD RID: 2781
		private global::System.Windows.Forms.LinkLabel linkLabel1;

		// Token: 0x04000ADE RID: 2782
		private global::System.Windows.Forms.Button btnOK;
	}
}
