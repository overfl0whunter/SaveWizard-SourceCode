namespace PS3SaveEditor
{
	// Token: 0x020001C6 RID: 454
	public partial class ChooseProfile : global::System.Windows.Forms.Form
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x000731C8 File Offset: 0x000713C8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00073200 File Offset: 0x00071400
		private void InitializeComponent()
		{
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnSelect = new global::System.Windows.Forms.Button();
			this.cbProfiles = new global::System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnSelect);
			this.panel1.Controls.Add(this.cbProfiles);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(246, 68));
			this.panel1.TabIndex = 3;
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(135), global::PS3SaveEditor.Util.ScaleSize(39));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnSelect.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnSelect.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(37), global::PS3SaveEditor.Util.ScaleSize(38));
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnSelect.TabIndex = 4;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.cbProfiles.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProfiles.FormattingEnabled = true;
			this.cbProfiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(37), global::PS3SaveEditor.Util.ScaleSize(10));
			this.cbProfiles.Name = "cbProfiles";
			this.cbProfiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(173, 21));
			this.cbProfiles.TabIndex = 3;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(266, 88));
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChooseProfile";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose PSN Account";
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000ABA RID: 2746
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000ABB RID: 2747
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000ABC RID: 2748
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000ABD RID: 2749
		private global::System.Windows.Forms.Button btnSelect;

		// Token: 0x04000ABE RID: 2750
		private global::System.Windows.Forms.ComboBox cbProfiles;
	}
}
