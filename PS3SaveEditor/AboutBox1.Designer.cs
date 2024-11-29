namespace PS3SaveEditor
{
	// Token: 0x020001AA RID: 426
	internal partial class AboutBox1 : global::System.Windows.Forms.Form
	{
		// Token: 0x060015F4 RID: 5620 RVA: 0x00067A7C File Offset: 0x00065C7C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00067AB4 File Offset: 0x00065CB4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.AboutBox1));
			this.lblVersion = new global::System.Windows.Forms.Label();
			this.lblDesc = new global::System.Windows.Forms.Label();
			this.lblCopyright = new global::System.Windows.Forms.Label();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.osLabel = new global::System.Windows.Forms.Label();
			this.frameworkVersion = new global::System.Windows.Forms.Label();
			this.frameworkLabel = new global::System.Windows.Forms.Label();
			this.osVersion = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(11));
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblVersion.TabIndex = 2;
			this.lblDesc.AutoSize = true;
			this.lblDesc.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(30));
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(124, 13));
			this.lblDesc.TabIndex = 3;
			this.lblDesc.Text = "CYBER PS4 Save Editor";
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(112));
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(232, 13));
			this.lblCopyright.TabIndex = 4;
			this.lblCopyright.Text = "Copyright © CYBER Gadget. All rights reserved.";
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(133));
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(123, 13));
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://cybergadget.co.jp";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(339), global::PS3SaveEditor.Util.ScaleSize(129));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(11));
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(32, 32));
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.osLabel.AutoSize = true;
			this.osLabel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(53));
			this.osLabel.Name = "osLabel";
			this.osLabel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(25, 13));
			this.osLabel.TabIndex = 7;
			this.osLabel.Text = "OS:";
			this.frameworkVersion.AutoSize = true;
			this.frameworkVersion.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(131), global::PS3SaveEditor.Util.ScaleSize(76));
			this.frameworkVersion.Name = "frameworkVersion";
			this.frameworkVersion.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.frameworkVersion.TabIndex = 9;
			this.frameworkLabel.AutoSize = true;
			this.frameworkLabel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(59), global::PS3SaveEditor.Util.ScaleSize(76));
			this.frameworkLabel.Name = "frameworkLabel";
			this.frameworkLabel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(62, 13));
			this.frameworkLabel.TabIndex = 10;
			this.frameworkLabel.Text = "Framework:";
			this.osVersion.AutoSize = true;
			this.osVersion.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(131), global::PS3SaveEditor.Util.ScaleSize(53));
			this.osVersion.Name = "osVersion";
			this.osVersion.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.osVersion.TabIndex = 11;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(426, 164));
			base.Controls.Add(this.osVersion);
			base.Controls.Add(this.frameworkLabel);
			base.Controls.Add(this.frameworkVersion);
			base.Controls.Add(this.osLabel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.lblCopyright);
			base.Controls.Add(this.lblDesc);
			base.Controls.Add(this.lblVersion);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutBox1";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(9));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About PS4 Save Editor";
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040009FD RID: 2557
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040009FE RID: 2558
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x040009FF RID: 2559
		private global::System.Windows.Forms.Label lblVersion;

		// Token: 0x04000A00 RID: 2560
		private global::System.Windows.Forms.Label lblDesc;

		// Token: 0x04000A01 RID: 2561
		private global::System.Windows.Forms.Label lblCopyright;

		// Token: 0x04000A02 RID: 2562
		private global::System.Windows.Forms.LinkLabel linkLabel1;

		// Token: 0x04000A03 RID: 2563
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000A04 RID: 2564
		private global::System.Windows.Forms.Label osLabel;

		// Token: 0x04000A05 RID: 2565
		private global::System.Windows.Forms.Label frameworkVersion;

		// Token: 0x04000A06 RID: 2566
		private global::System.Windows.Forms.Label frameworkLabel;

		// Token: 0x04000A07 RID: 2567
		private global::System.Windows.Forms.Label osVersion;
	}
}
