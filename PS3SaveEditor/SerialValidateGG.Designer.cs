namespace PS3SaveEditor
{
	// Token: 0x020001E2 RID: 482
	public partial class SerialValidateGG : global::System.Windows.Forms.Form
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x0009A348 File Offset: 0x00098548
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0009A380 File Offset: 0x00098580
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.txtSerial4 = new global::System.Windows.Forms.TextBox();
			this.txtSerial3 = new global::System.Windows.Forms.TextBox();
			this.txtSerial2 = new global::System.Windows.Forms.TextBox();
			this.txtSerial1 = new global::System.Windows.Forms.TextBox();
			this.lblInstruction2 = new global::System.Windows.Forms.Label();
			this.lblInstruction = new global::System.Windows.Forms.Label();
			this.lblLicHelp = new global::System.Windows.Forms.Label();
			this.lnkLicSupport = new global::System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(65), global::PS3SaveEditor.Util.ScaleSize(78));
			this.label1.Name = "label1";
			this.label1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(299, 15));
			this.label1.TabIndex = 0;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.panel1.Controls.Add(this.lnkLicSupport);
			this.panel1.Controls.Add(this.lblLicHelp);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.txtSerial4);
			this.panel1.Controls.Add(this.txtSerial3);
			this.panel1.Controls.Add(this.txtSerial2);
			this.panel1.Controls.Add(this.txtSerial1);
			this.panel1.Controls.Add(this.lblInstruction2);
			this.panel1.Controls.Add(this.lblInstruction);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(11));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(439, 120));
			this.panel1.TabIndex = 1;
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(218), global::PS3SaveEditor.Util.ScaleSize(54));
			this.label4.Name = "label4";
			this.label4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label4.TabIndex = 12;
			this.label4.Text = "-";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(163), global::PS3SaveEditor.Util.ScaleSize(54));
			this.label3.Name = "label3";
			this.label3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label3.TabIndex = 11;
			this.label3.Text = "-";
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(107), global::PS3SaveEditor.Util.ScaleSize(54));
			this.label2.Name = "label2";
			this.label2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(11, 13));
			this.label2.TabIndex = 10;
			this.label2.Text = "-";
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ForeColor = global::System.Drawing.Color.Black;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(54));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(55, 23));
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Visible = false;
			this.btnOk.ForeColor = global::System.Drawing.Color.Black;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(293), global::PS3SaveEditor.Util.ScaleSize(51));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 18));
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.txtSerial4.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(232), global::PS3SaveEditor.Util.ScaleSize(51));
			this.txtSerial4.MaxLength = 4;
			this.txtSerial4.Name = "txtSerial4";
			this.txtSerial4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial4.TabIndex = 7;
			this.txtSerial3.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(176), global::PS3SaveEditor.Util.ScaleSize(51));
			this.txtSerial3.MaxLength = 4;
			this.txtSerial3.Name = "txtSerial3";
			this.txtSerial3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial3.TabIndex = 6;
			this.txtSerial2.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(120), global::PS3SaveEditor.Util.ScaleSize(51));
			this.txtSerial2.MaxLength = 4;
			this.txtSerial2.Name = "txtSerial2";
			this.txtSerial2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial2.TabIndex = 5;
			this.txtSerial1.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtSerial1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(64), global::PS3SaveEditor.Util.ScaleSize(51));
			this.txtSerial1.MaxLength = 4;
			this.txtSerial1.Name = "txtSerial1";
			this.txtSerial1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(40, 21));
			this.txtSerial1.TabIndex = 4;
			this.lblInstruction2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(5), global::PS3SaveEditor.Util.ScaleSize(25));
			this.lblInstruction2.Name = "lblInstruction2";
			this.lblInstruction2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(430, 15));
			this.lblInstruction2.TabIndex = 2;
			this.lblInstruction2.Text = "Sample Text";
			this.lblInstruction2.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lblInstruction.AutoSize = true;
			this.lblInstruction.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(8));
			this.lblInstruction.Name = "lblInstruction";
			this.lblInstruction.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 15));
			this.lblInstruction.TabIndex = 1;
			this.lblInstruction.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lblLicHelp.ForeColor = global::System.Drawing.Color.White;
			this.lblLicHelp.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(2), global::PS3SaveEditor.Util.ScaleSize(98));
			this.lblLicHelp.Name = "lblLicHelp";
			this.lblLicHelp.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(290, 15));
			this.lblLicHelp.TabIndex = 13;
			this.lblLicHelp.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.lnkLicSupport.ForeColor = global::System.Drawing.Color.White;
			this.lnkLicSupport.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(295), global::PS3SaveEditor.Util.ScaleSize(98));
			this.lnkLicSupport.Name = "lnkLicSupport";
			this.lnkLicSupport.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(120, 15));
			this.lnkLicSupport.TabIndex = 14;
			this.lnkLicSupport.TabStop = true;
			this.lnkLicSupport.Text = "www.savewizard.net";
			this.lnkLicSupport.LinkColor = global::System.Drawing.Color.White;
			this.lnkLicSupport.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(459, 142));
			base.Controls.Add(this.panel1);
			this.ForeColor = global::System.Drawing.Color.White;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SerialValidateGG";
			base.ShowIcon = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Registering Game Genie";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000C7B RID: 3195
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000C7C RID: 3196
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000C7D RID: 3197
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000C7E RID: 3198
		private global::System.Windows.Forms.Label lblInstruction;

		// Token: 0x04000C7F RID: 3199
		private global::System.Windows.Forms.Label lblInstruction2;

		// Token: 0x04000C80 RID: 3200
		private global::System.Windows.Forms.TextBox txtSerial4;

		// Token: 0x04000C81 RID: 3201
		private global::System.Windows.Forms.TextBox txtSerial3;

		// Token: 0x04000C82 RID: 3202
		private global::System.Windows.Forms.TextBox txtSerial2;

		// Token: 0x04000C83 RID: 3203
		private global::System.Windows.Forms.TextBox txtSerial1;

		// Token: 0x04000C84 RID: 3204
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000C85 RID: 3205
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000C86 RID: 3206
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000C87 RID: 3207
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000C88 RID: 3208
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000C89 RID: 3209
		private global::System.Windows.Forms.Label lblLicHelp;

		// Token: 0x04000C8A RID: 3210
		private global::System.Windows.Forms.LinkLabel lnkLicSupport;
	}
}
