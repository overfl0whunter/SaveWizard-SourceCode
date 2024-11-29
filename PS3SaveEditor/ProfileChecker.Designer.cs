namespace PS3SaveEditor
{
	// Token: 0x020001D1 RID: 465
	public partial class ProfileChecker : global::System.Windows.Forms.Form
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x0008D940 File Offset: 0x0008BB40
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0008D978 File Offset: 0x0008BB78
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.ProfileChecker));
			this.panelInstructions = new global::System.Windows.Forms.Panel();
			this.lblInstructionPage1 = new global::System.Windows.Forms.Label();
			this.lblInstruciton3 = new global::System.Windows.Forms.Label();
			this.lblInstruction2 = new global::System.Windows.Forms.Label();
			this.lblInstrucionRed = new global::System.Windows.Forms.Label();
			this.lblInstruction1 = new global::System.Windows.Forms.Label();
			this.lblInstructions = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lblTitle1 = new global::System.Windows.Forms.Label();
			this.btnNext = new global::System.Windows.Forms.Button();
			this.panelProfileName = new global::System.Windows.Forms.Panel();
			this.lblFooter2 = new global::System.Windows.Forms.Label();
			this.lblUserName = new global::System.Windows.Forms.Label();
			this.lblInstruction2Page2 = new global::System.Windows.Forms.Label();
			this.lblDriveLetter = new global::System.Windows.Forms.Label();
			this.txtProfileName = new global::System.Windows.Forms.TextBox();
			this.lblInstructionPage2 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.lblPageTitle = new global::System.Windows.Forms.Label();
			this.panelFinish = new global::System.Windows.Forms.Panel();
			this.lblFinish = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.lblTitleFinish = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panelInstructions.SuspendLayout();
			this.panelProfileName.SuspendLayout();
			this.panelFinish.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panelInstructions.BackColor = global::System.Drawing.Color.White;
			this.panelInstructions.Controls.Add(this.lblInstructionPage1);
			this.panelInstructions.Controls.Add(this.lblInstruciton3);
			this.panelInstructions.Controls.Add(this.lblInstruction2);
			this.panelInstructions.Controls.Add(this.lblInstrucionRed);
			this.panelInstructions.Controls.Add(this.lblInstruction1);
			this.panelInstructions.Controls.Add(this.lblInstructions);
			this.panelInstructions.Controls.Add(this.label1);
			this.panelInstructions.Controls.Add(this.lblTitle1);
			this.panelInstructions.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.panelInstructions.Name = "panelInstructions";
			this.panelInstructions.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(570, 340));
			this.panelInstructions.TabIndex = 0;
			this.lblInstructionPage1.ForeColor = global::System.Drawing.Color.Black;
			this.lblInstructionPage1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(290));
			this.lblInstructionPage1.Name = "lblInstructionPage1";
			this.lblInstructionPage1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 23));
			this.lblInstructionPage1.TabIndex = 7;
			this.lblInstruciton3.ForeColor = global::System.Drawing.Color.Black;
			this.lblInstruciton3.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(243));
			this.lblInstruciton3.Name = "lblInstruciton3";
			this.lblInstruciton3.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 28));
			this.lblInstruciton3.TabIndex = 6;
			this.lblInstruction2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(170));
			this.lblInstruction2.Name = "lblInstruction2";
			this.lblInstruction2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 61));
			this.lblInstruction2.TabIndex = 5;
			this.lblInstrucionRed.ForeColor = global::System.Drawing.Color.Red;
			this.lblInstrucionRed.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(110));
			this.lblInstrucionRed.Name = "lblInstrucionRed";
			this.lblInstrucionRed.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 35));
			this.lblInstrucionRed.TabIndex = 4;
			this.lblInstruction1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(93));
			this.lblInstruction1.Name = "lblInstruction1";
			this.lblInstruction1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 26));
			this.lblInstruction1.TabIndex = 3;
			this.lblInstructions.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(63));
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(541, 26));
			this.lblInstructions.TabIndex = 2;
			this.label1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(48));
			this.label1.Name = "label1";
			this.label1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(537, 1));
			this.label1.TabIndex = 1;
			this.lblTitle1.AutoSize = true;
			this.lblTitle1.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(16f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblTitle1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(13));
			this.lblTitle1.Name = "lblTitle1";
			this.lblTitle1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 26));
			this.lblTitle1.TabIndex = 0;
			this.btnNext.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(259), global::PS3SaveEditor.Util.ScaleSize(379));
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = "Next";
			this.btnNext.UseVisualStyleBackColor = true;
			this.panelProfileName.BackColor = global::System.Drawing.Color.White;
			this.panelProfileName.Controls.Add(this.lblFooter2);
			this.panelProfileName.Controls.Add(this.lblUserName);
			this.panelProfileName.Controls.Add(this.lblInstruction2Page2);
			this.panelProfileName.Controls.Add(this.lblDriveLetter);
			this.panelProfileName.Controls.Add(this.txtProfileName);
			this.panelProfileName.Controls.Add(this.lblInstructionPage2);
			this.panelProfileName.Controls.Add(this.label4);
			this.panelProfileName.Controls.Add(this.lblPageTitle);
			this.panelProfileName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.panelProfileName.Name = "panelProfileName";
			this.panelProfileName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(570, 340));
			this.panelProfileName.TabIndex = 2;
			this.lblFooter2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(20), global::PS3SaveEditor.Util.ScaleSize(283));
			this.lblFooter2.Name = "lblFooter2";
			this.lblFooter2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(532, 20));
			this.lblFooter2.TabIndex = 7;
			this.lblUserName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(63), global::PS3SaveEditor.Util.ScaleSize(190));
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(193, 20));
			this.lblUserName.TabIndex = 6;
			this.lblInstruction2Page2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(20), global::PS3SaveEditor.Util.ScaleSize(155));
			this.lblInstruction2Page2.Name = "lblInstruction2Page2";
			this.lblInstruction2Page2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(532, 20));
			this.lblInstruction2Page2.TabIndex = 5;
			this.lblDriveLetter.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(63), global::PS3SaveEditor.Util.ScaleSize(86));
			this.lblDriveLetter.Name = "lblDriveLetter";
			this.lblDriveLetter.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(300, 13));
			this.lblDriveLetter.TabIndex = 4;
			this.txtProfileName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(63), global::PS3SaveEditor.Util.ScaleSize(213));
			this.txtProfileName.Name = "txtProfileName";
			this.txtProfileName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(485, 20));
			this.txtProfileName.TabIndex = 3;
			this.lblInstructionPage2.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(20), global::PS3SaveEditor.Util.ScaleSize(61));
			this.lblInstructionPage2.Name = "lblInstructionPage2";
			this.lblInstructionPage2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(532, 20));
			this.lblInstructionPage2.TabIndex = 2;
			this.label4.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(48));
			this.label4.Name = "label4";
			this.label4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(538, 1));
			this.label4.TabIndex = 1;
			this.lblPageTitle.AutoSize = true;
			this.lblPageTitle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(16f), global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblPageTitle.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(16), global::PS3SaveEditor.Util.ScaleSize(13));
			this.lblPageTitle.Name = "lblPageTitle";
			this.lblPageTitle.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 26));
			this.lblPageTitle.TabIndex = 0;
			this.panelFinish.BackColor = global::System.Drawing.Color.White;
			this.panelFinish.Controls.Add(this.lblFinish);
			this.panelFinish.Controls.Add(this.label8);
			this.panelFinish.Controls.Add(this.lblTitleFinish);
			this.panelFinish.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(12));
			this.panelFinish.Name = "panelFinish";
			this.panelFinish.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(570, 340));
			this.panelFinish.TabIndex = 3;
			this.lblFinish.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(18), global::PS3SaveEditor.Util.ScaleSize(61));
			this.lblFinish.Name = "lblFinish";
			this.lblFinish.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(532, 25));
			this.lblFinish.TabIndex = 2;
			this.label8.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(48));
			this.label8.Name = "label8";
			this.label8.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(537, 1));
			this.label8.TabIndex = 1;
			this.lblTitleFinish.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(16f), global::System.Drawing.FontStyle.Bold);
			this.lblTitleFinish.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(13));
			this.lblTitleFinish.Name = "lblTitleFinish";
			this.lblTitleFinish.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(537, 26));
			this.lblTitleFinish.TabIndex = 0;
			this.panel1.Controls.Add(this.panelProfileName);
			this.panel1.Controls.Add(this.panelFinish);
			this.panel1.Controls.Add(this.panelInstructions);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(594, 363));
			this.panel1.TabIndex = 4;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(614, 410));
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnNext);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProfileChecker";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ProfileChecker";
			this.panelInstructions.ResumeLayout(false);
			this.panelInstructions.PerformLayout();
			this.panelProfileName.ResumeLayout(false);
			this.panelProfileName.PerformLayout();
			this.panelFinish.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000BC6 RID: 3014
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BC7 RID: 3015
		private global::System.Windows.Forms.Panel panelInstructions;

		// Token: 0x04000BC8 RID: 3016
		private global::System.Windows.Forms.Button btnNext;

		// Token: 0x04000BC9 RID: 3017
		private global::System.Windows.Forms.Label lblInstructions;

		// Token: 0x04000BCA RID: 3018
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000BCB RID: 3019
		private global::System.Windows.Forms.Label lblTitle1;

		// Token: 0x04000BCC RID: 3020
		private global::System.Windows.Forms.Panel panelProfileName;

		// Token: 0x04000BCD RID: 3021
		private global::System.Windows.Forms.Label lblDriveLetter;

		// Token: 0x04000BCE RID: 3022
		private global::System.Windows.Forms.TextBox txtProfileName;

		// Token: 0x04000BCF RID: 3023
		private global::System.Windows.Forms.Label lblInstructionPage2;

		// Token: 0x04000BD0 RID: 3024
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000BD1 RID: 3025
		private global::System.Windows.Forms.Label lblPageTitle;

		// Token: 0x04000BD2 RID: 3026
		private global::System.Windows.Forms.Panel panelFinish;

		// Token: 0x04000BD3 RID: 3027
		private global::System.Windows.Forms.Label lblFinish;

		// Token: 0x04000BD4 RID: 3028
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000BD5 RID: 3029
		private global::System.Windows.Forms.Label lblTitleFinish;

		// Token: 0x04000BD6 RID: 3030
		private global::System.Windows.Forms.Label lblInstructionPage1;

		// Token: 0x04000BD7 RID: 3031
		private global::System.Windows.Forms.Label lblInstruciton3;

		// Token: 0x04000BD8 RID: 3032
		private global::System.Windows.Forms.Label lblInstruction2;

		// Token: 0x04000BD9 RID: 3033
		private global::System.Windows.Forms.Label lblInstrucionRed;

		// Token: 0x04000BDA RID: 3034
		private global::System.Windows.Forms.Label lblInstruction1;

		// Token: 0x04000BDB RID: 3035
		private global::System.Windows.Forms.Label lblFooter2;

		// Token: 0x04000BDC RID: 3036
		private global::System.Windows.Forms.Label lblUserName;

		// Token: 0x04000BDD RID: 3037
		private global::System.Windows.Forms.Label lblInstruction2Page2;

		// Token: 0x04000BDE RID: 3038
		private global::System.Windows.Forms.Panel panel1;
	}
}
