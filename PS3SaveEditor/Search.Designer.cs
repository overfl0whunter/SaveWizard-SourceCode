namespace PS3SaveEditor
{
	// Token: 0x020001D5 RID: 469
	public partial class Search : global::System.Windows.Forms.Form
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x0008F674 File Offset: 0x0008D874
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008F6AC File Offset: 0x0008D8AC
		private void InitializeComponent()
		{
			this.lblEnterLoc = new global::System.Windows.Forms.Label();
			this.txtSearch = new global::System.Windows.Forms.TextBox();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnFindPrev = new global::System.Windows.Forms.Button();
			this.btnFind = new global::System.Windows.Forms.Button();
			this.cbSearchType = new global::System.Windows.Forms.ComboBox();
			base.SuspendLayout();
			this.lblEnterLoc.AutoSize = true;
			this.lblEnterLoc.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(11));
			this.lblEnterLoc.Name = "lblEnterLoc";
			this.lblEnterLoc.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(56, 13));
			this.lblEnterLoc.TabIndex = 0;
			this.lblEnterLoc.Text = "Find what:";
			this.txtSearch.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(70), global::PS3SaveEditor.Util.ScaleSize(9));
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(97, 20));
			this.txtSearch.TabIndex = 1;
			this.txtSearch.TextChanged += new global::System.EventHandler(this.txtSearch_TextChanged);
			this.txtSearch.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			this.txtSearch.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(121), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Visible = false;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(193), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(75, 23));
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnFindPrev.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnFindPrev.BackColor = global::System.Drawing.SystemColors.Control;
			this.btnFindPrev.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btnFindPrev.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(86), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnFindPrev.Name = "btnFindPrev";
			this.btnFindPrev.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(81, 23));
			this.btnFindPrev.TabIndex = 23;
			this.btnFindPrev.Text = "Find Previous";
			this.btnFindPrev.UseVisualStyleBackColor = false;
			this.btnFind.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnFind.BackColor = global::System.Drawing.SystemColors.Control;
			this.btnFind.ForeColor = global::System.Drawing.Color.Black;
			this.btnFind.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(12), global::PS3SaveEditor.Util.ScaleSize(35));
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(63, 23));
			this.btnFind.TabIndex = 22;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = false;
			this.cbSearchType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSearchType.FormattingEnabled = true;
			this.cbSearchType.Items.AddRange(new object[] { "Hex", "Text", "Decimal", "Float" });
			this.cbSearchType.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(184), global::PS3SaveEditor.Util.ScaleSize(10));
			this.cbSearchType.Name = "cbSearchType";
			this.cbSearchType.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(84, 21));
			this.cbSearchType.TabIndex = 24;
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.CancelButton = this.btnCancel;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(280, 69));
			base.Controls.Add(this.cbSearchType);
			base.Controls.Add(this.btnFindPrev);
			base.Controls.Add(this.btnFind);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtSearch);
			base.Controls.Add(this.lblEnterLoc);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Search";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Find";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000BEC RID: 3052
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000BED RID: 3053
		private global::System.Windows.Forms.Label lblEnterLoc;

		// Token: 0x04000BEE RID: 3054
		private global::System.Windows.Forms.TextBox txtSearch;

		// Token: 0x04000BEF RID: 3055
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04000BF0 RID: 3056
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x04000BF1 RID: 3057
		private global::System.Windows.Forms.Button btnFindPrev;

		// Token: 0x04000BF2 RID: 3058
		private global::System.Windows.Forms.Button btnFind;

		// Token: 0x04000BF3 RID: 3059
		private global::System.Windows.Forms.ComboBox cbSearchType;
	}
}
