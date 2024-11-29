namespace PS3SaveEditor
{
	// Token: 0x020001AB RID: 427
	public partial class AddCode : global::System.Windows.Forms.Form
	{
		// Token: 0x06001609 RID: 5641 RVA: 0x000693E8 File Offset: 0x000675E8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00069420 File Offset: 0x00067620
		private void InitializeComponent()
		{
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			this.txtDescription = new global::System.Windows.Forms.TextBox();
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.ColLocation = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnSave = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.txtCode = new global::System.Windows.Forms.TextBox();
			this.lblCodes = new global::System.Windows.Forms.Label();
			this.txtComment = new global::System.Windows.Forms.TextBox();
			this.lblComment = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lblDescription = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.txtDescription.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(28));
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 20));
			this.txtDescription.TabIndex = 0;
			bool flag = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag)
			{
				this.txtDescription.ContextMenu = new global::PS3SaveEditor.SubControls.MacContextMenu(this.txtDescription).GetMenu();
			}
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.ColLocation, this.Value });
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(265), global::PS3SaveEditor.Util.ScaleSize(52));
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font(global::PS3SaveEditor.Util.GetFontFamily(), global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(12, 10));
			this.dataGridView1.TabIndex = 2;
			this.dataGridView1.Visible = false;
			this.ColLocation.HeaderText = "Location";
			this.ColLocation.Name = "Location";
			this.Value.HeaderText = "Value";
			this.Value.Name = "Value";
			this.btnSave.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(25), global::PS3SaveEditor.Util.ScaleSize(282));
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new global::System.EventHandler(this.btnSave_Click);
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(104), global::PS3SaveEditor.Util.ScaleSize(282));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.txtCode.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txtCode.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtCode.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(112));
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtCode.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 165));
			this.txtCode.TabIndex = 2;
			this.txtCode.TextChanged += new global::System.EventHandler(this.txtCheatCode_TextChanged);
			this.txtCode.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.txtCheatCode_KeyDown);
			this.txtCode.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
			bool flag2 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag2)
			{
				this.txtCode.ContextMenu = new global::PS3SaveEditor.SubControls.MacContextMenu(this.txtCode).GetMenu();
			}
			this.lblCodes.BackColor = global::System.Drawing.Color.Transparent;
			this.lblCodes.ForeColor = global::System.Drawing.Color.White;
			this.lblCodes.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(97));
			this.lblCodes.Name = "lblCodes";
			this.lblCodes.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(111, 13));
			this.lblCodes.TabIndex = 6;
			this.lblCodes.Text = "Cheat Codes:";
			this.txtComment.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(70));
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 20));
			this.txtComment.TabIndex = 1;
			bool flag3 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag3)
			{
				this.txtComment.ContextMenu = new global::PS3SaveEditor.SubControls.MacContextMenu(this.txtComment).GetMenu();
			}
			this.lblComment.BackColor = global::System.Drawing.Color.Transparent;
			this.lblComment.ForeColor = global::System.Drawing.Color.White;
			this.lblComment.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(55));
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(93, 13));
			this.lblComment.TabIndex = 7;
			this.lblComment.Text = "Comment:";
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(127, 204, 204, 204);
			this.panel1.Controls.Add(this.lblDescription);
			this.panel1.Controls.Add(this.txtComment);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.lblComment);
			this.panel1.Controls.Add(this.lblCodes);
			this.panel1.Controls.Add(this.txtCode);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.txtDescription);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(11));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(205, 315));
			this.panel1.TabIndex = 8;
			this.lblDescription.BackColor = global::System.Drawing.Color.Transparent;
			this.lblDescription.ForeColor = global::System.Drawing.Color.White;
			this.lblDescription.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(13));
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(93, 13));
			this.lblDescription.TabIndex = 8;
			this.lblDescription.Text = "Description:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(225, 337));
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddCode";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Add Cheat";
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000A0E RID: 2574
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000A0F RID: 2575
		private global::System.Windows.Forms.TextBox txtDescription;

		// Token: 0x04000A10 RID: 2576
		private global::System.Windows.Forms.DataGridView dataGridView1;

		// Token: 0x04000A11 RID: 2577
		private global::System.Windows.Forms.DataGridViewTextBoxColumn ColLocation;

		// Token: 0x04000A12 RID: 2578
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Value;

		// Token: 0x04000A13 RID: 2579
		private global::System.Windows.Forms.Button btnSave;

		// Token: 0x04000A14 RID: 2580
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000A15 RID: 2581
		private global::System.Windows.Forms.TextBox txtCode;

		// Token: 0x04000A16 RID: 2582
		private global::System.Windows.Forms.Label lblCodes;

		// Token: 0x04000A17 RID: 2583
		private global::System.Windows.Forms.TextBox txtComment;

		// Token: 0x04000A18 RID: 2584
		private global::System.Windows.Forms.Label lblComment;

		// Token: 0x04000A19 RID: 2585
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000A1A RID: 2586
		private global::System.Windows.Forms.Label lblDescription;
	}
}
