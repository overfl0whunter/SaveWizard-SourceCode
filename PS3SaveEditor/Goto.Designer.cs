namespace PS3SaveEditor
{
	// Token: 0x020001D9 RID: 473
	public partial class Goto : global::System.Windows.Forms.Form
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x00090ECC File Offset: 0x0008F0CC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00090F04 File Offset: 0x0008F104
		private void InitializeComponent()
		{
			this.lblEnterLoc = new global::System.Windows.Forms.Label();
			this.txtLocation = new global::System.Windows.Forms.TextBox();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.lblEnterLoc.AutoSize = true;
			this.lblEnterLoc.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(11));
			this.lblEnterLoc.Name = "lblEnterLoc";
			this.lblEnterLoc.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(79, 13));
			this.lblEnterLoc.TabIndex = 0;
			this.lblEnterLoc.Text = "Enter Location:";
			this.txtLocation.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(99), global::PS3SaveEditor.Util.ScaleSize(9));
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(97, 20));
			this.txtLocation.TabIndex = 1;
			this.txtLocation.TextChanged += new global::System.EventHandler(this.txtLocation_TextChanged);
			this.txtLocation.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
			this.txtLocation.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtLocation_KeyPress);
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(121), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(202), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.CancelButton = this.btnCancel;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(284, 69));
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtLocation);
			base.Controls.Add(this.lblEnterLoc);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Goto";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Go To Location";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000C18 RID: 3096
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C19 RID: 3097
		private global::System.Windows.Forms.Label lblEnterLoc;

		// Token: 0x04000C1A RID: 3098
		private global::System.Windows.Forms.TextBox txtLocation;

		// Token: 0x04000C1B RID: 3099
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000C1C RID: 3100
		private global::System.Windows.Forms.Button btnCancel;
	}
}
