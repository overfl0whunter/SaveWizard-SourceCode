namespace PS3SaveEditor
{
	// Token: 0x020001ED RID: 493
	public partial class AdvancedEdit2 : global::System.Windows.Forms.Form
	{
		// Token: 0x06001A4C RID: 6732 RVA: 0x000A9AAC File Offset: 0x000A7CAC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000A9AE4 File Offset: 0x000A7CE4
		private void InitializeComponent()
		{
			this.tableMain = new global::PS3SaveEditor.CustomTableLayoutPanel();
			this.tableLayoutMiddle = new global::PS3SaveEditor.CustomTableLayoutPanel();
			this.hexBoxLeft = new global::Be.Windows.Forms.HexBox();
			this.panelRight = new global::System.Windows.Forms.Panel();
			this.hexBoxRight = new global::Be.Windows.Forms.HexBox();
			this.txtSaveDataLeft = new global::System.Windows.Forms.RichTextBox();
			this.txtSaveDataRight = new global::System.Windows.Forms.RichTextBox();
			this.panelLeft = new global::System.Windows.Forms.Panel();
			this.lblGameName = new global::System.Windows.Forms.Label();
			this.tableTop = new global::PS3SaveEditor.CustomTableLayoutPanel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtSearchValue = new global::System.Windows.Forms.TextBox();
			this.btnStackSearch = new global::System.Windows.Forms.Button();
			this.btnFind = new global::System.Windows.Forms.Button();
			this.btnFindPrev = new global::System.Windows.Forms.Button();
			this.cbSearchMode = new global::System.Windows.Forms.ComboBox();
			this.lstSearchVal = new global::System.Windows.Forms.ListBox();
			this.lblAddress = new global::System.Windows.Forms.Label();
			this.txtAddress = new global::System.Windows.Forms.TextBox();
			this.btnStackAddress = new global::System.Windows.Forms.Button();
			this.btnGo = new global::System.Windows.Forms.Button();
			this.lstSearchAddresses = new global::System.Windows.Forms.ListBox();
			this.tableRight = new global::PS3SaveEditor.CustomTableLayoutPanel();
			this.chkEnableRight = new global::System.Windows.Forms.CheckBox();
			this.chkSyncScroll = new global::System.Windows.Forms.CheckBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.lstCache = new global::System.Windows.Forms.ListBox();
			this.btnPush = new global::System.Windows.Forms.Button();
			this.btnPop = new global::System.Windows.Forms.Button();
			this.lblCheats = new global::System.Windows.Forms.Label();
			this.listViewCheats = new global::System.Windows.Forms.CheckedListBox();
			this.lblCheatCodes = new global::System.Windows.Forms.Label();
			this.lstCheatCodes = new global::System.Windows.Forms.ListBox();
			this.cbSaveFiles = new global::System.Windows.Forms.ComboBox();
			this.btnApply = new global::System.Windows.Forms.Button();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.btnCompare = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lblOffsetValue = new global::System.Windows.Forms.Label();
			this.lblOffset = new global::System.Windows.Forms.Label();
			this.tableMain.SuspendLayout();
			this.tableLayoutMiddle.SuspendLayout();
			this.tableTop.SuspendLayout();
			this.tableRight.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.tableMain.BackColor = global::System.Drawing.Color.FromArgb(204, 204, 204);
			this.tableMain.ColumnCount = 2;
			this.tableMain.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableMain.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 189f));
			this.tableMain.Controls.Add(this.lblGameName, 0, 0);
			this.tableMain.Controls.Add(this.tableTop, 0, 1);
			this.tableMain.Controls.Add(this.tableLayoutMiddle, 0, 2);
			this.tableMain.Controls.Add(this.tableRight, 1, 0);
			this.tableMain.Controls.Add(this.panel1, 0, 3);
			this.tableMain.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableMain.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(10));
			this.tableMain.Name = "tableMain";
			this.tableMain.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(8));
			this.tableMain.RowCount = 4;
			this.tableMain.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 24f));
			this.tableMain.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 60f));
			this.tableMain.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableMain.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 30f));
			this.tableMain.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableMain.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(878, 517));
			this.tableMain.TabIndex = 0;
			this.tableLayoutMiddle.BackColor = global::System.Drawing.Color.Transparent;
			this.tableLayoutMiddle.ColumnCount = 2;
			this.tableLayoutMiddle.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutMiddle.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 0f));
			this.tableLayoutMiddle.Controls.Add(this.hexBoxLeft, 0, 1);
			this.tableLayoutMiddle.Controls.Add(this.panelRight, 1, 0);
			this.tableLayoutMiddle.Controls.Add(this.hexBoxRight, 1, 1);
			this.tableLayoutMiddle.Controls.Add(this.txtSaveDataLeft, 0, 1);
			this.tableLayoutMiddle.Controls.Add(this.txtSaveDataRight, 1, 1);
			this.tableLayoutMiddle.Controls.Add(this.panelLeft, 0, 0);
			this.tableLayoutMiddle.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutMiddle.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(8), global::PS3SaveEditor.Util.ScaleSize(92));
			this.tableLayoutMiddle.Margin = new global::System.Windows.Forms.Padding(0);
			this.tableLayoutMiddle.MaximumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(1280, 1000));
			this.tableLayoutMiddle.Name = "tableLayoutMiddle";
			this.tableLayoutMiddle.RowCount = 3;
			this.tableLayoutMiddle.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutMiddle.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutMiddle.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 0f));
			this.tableLayoutMiddle.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(673, 387));
			this.tableLayoutMiddle.TabIndex = 31;
			this.hexBoxLeft.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.hexBoxLeft.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.hexBoxLeft.HScrollBarVisible = false;
			this.hexBoxLeft.LineInfoForeColor = global::System.Drawing.Color.Empty;
			this.hexBoxLeft.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(676), global::PS3SaveEditor.Util.ScaleSize(23));
			this.hexBoxLeft.Name = "hexBoxLeft";
			this.hexBoxLeft.ShadowSelectionColor = global::System.Drawing.Color.FromArgb(100, 60, 188, 255);
			this.hexBoxLeft.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(1, 361));
			this.hexBoxLeft.TabIndex = 0;
			this.panelRight.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panelRight.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f));
			this.panelRight.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(673), 0);
			this.panelRight.Margin = new global::System.Windows.Forms.Padding(0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(1, 20));
			this.panelRight.TabIndex = 29;
			this.hexBoxRight.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.hexBoxRight.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.hexBoxRight.HScrollBarVisible = false;
			this.hexBoxRight.LineInfoForeColor = global::System.Drawing.Color.Empty;
			this.hexBoxRight.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(390));
			this.hexBoxRight.Name = "hexBoxRight";
			this.hexBoxRight.ShadowSelectionColor = global::System.Drawing.Color.FromArgb(100, 60, 188, 255);
			this.hexBoxRight.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(667, 1));
			this.hexBoxRight.TabIndex = 27;
			this.txtSaveDataLeft.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txtSaveDataLeft.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(23));
			this.txtSaveDataLeft.Name = "txtSaveDataLeft";
			this.txtSaveDataLeft.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(667, 361));
			this.txtSaveDataLeft.TabIndex = 31;
			this.txtSaveDataLeft.Text = "";
			this.txtSaveDataLeft.Visible = false;
			this.txtSaveDataRight.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txtSaveDataRight.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(676), global::PS3SaveEditor.Util.ScaleSize(390));
			this.txtSaveDataRight.Name = "txtSaveDataRight";
			this.txtSaveDataRight.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(1, 1));
			this.txtSaveDataRight.TabIndex = 30;
			this.txtSaveDataRight.Text = "";
			this.txtSaveDataRight.Visible = false;
			this.panelLeft.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panelLeft.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f));
			this.panelLeft.Location = new global::System.Drawing.Point(0, 0);
			this.panelLeft.Margin = new global::System.Windows.Forms.Padding(0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(673, 20));
			this.panelLeft.TabIndex = 32;
			this.lblGameName.AutoSize = true;
			this.lblGameName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(8));
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(66, 13));
			this.lblGameName.TabIndex = 0;
			this.lblGameName.Text = "Game Name";
			this.tableTop.ColumnCount = 11;
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 45f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 70f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 2f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 70f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 70f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 50f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 70f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 40f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableTop.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableTop.Controls.Add(this.label2, 0, 2);
			this.tableTop.Controls.Add(this.txtSearchValue, 1, 2);
			this.tableTop.Controls.Add(this.btnStackSearch, 1, 0);
			this.tableTop.Controls.Add(this.btnFind, 3, 2);
			this.tableTop.Controls.Add(this.btnFindPrev, 4, 2);
			this.tableTop.Controls.Add(this.cbSearchMode, 3, 0);
			this.tableTop.Controls.Add(this.lstSearchVal, 5, 0);
			this.tableTop.Controls.Add(this.lblAddress, 6, 2);
			this.tableTop.Controls.Add(this.txtAddress, 7, 2);
			this.tableTop.Controls.Add(this.btnStackAddress, 7, 0);
			this.tableTop.Controls.Add(this.btnGo, 8, 2);
			this.tableTop.Controls.Add(this.lstSearchAddresses, 9, 0);
			this.tableTop.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableTop.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(34));
			this.tableTop.Margin = new global::System.Windows.Forms.Padding(2);
			this.tableTop.Name = "tableTop";
			this.tableTop.RowCount = 4;
			this.tableTop.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 23f));
			this.tableTop.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 2f));
			this.tableTop.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22f));
			this.tableTop.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 2f));
			this.tableTop.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(669, 56));
			this.tableTop.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new global::System.Drawing.Point(0, global::PS3SaveEditor.Util.ScaleSize(25));
			this.label2.Margin = new global::System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(45, 22));
			this.label2.TabIndex = 0;
			this.label2.Text = "Search";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.txtSearchValue.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txtSearchValue.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(46), global::PS3SaveEditor.Util.ScaleSize(26));
			this.txtSearchValue.Margin = new global::System.Windows.Forms.Padding(1, 1, 1, 2);
			this.txtSearchValue.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 21));
			this.txtSearchValue.Name = "txtSearchValue";
			this.txtSearchValue.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 20));
			this.txtSearchValue.TabIndex = 1;
			this.btnStackSearch.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnStackSearch.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(45), 0);
			this.btnStackSearch.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnStackSearch.Name = "btnStackSearch";
			this.btnStackSearch.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(70, 23));
			this.btnStackSearch.TabIndex = 2;
			this.btnStackSearch.Text = "Stack";
			this.btnStackSearch.UseVisualStyleBackColor = true;
			this.btnFind.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnFind.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(117), global::PS3SaveEditor.Util.ScaleSize(25));
			this.btnFind.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(70, 22));
			this.btnFind.TabIndex = 3;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFindPrev.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnFindPrev.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(189), global::PS3SaveEditor.Util.ScaleSize(25));
			this.btnFindPrev.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(2), 0, 0, 0);
			this.btnFindPrev.Name = "btnFindPrev";
			this.btnFindPrev.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 22));
			this.btnFindPrev.TabIndex = 4;
			this.btnFindPrev.Text = "Find Prev.";
			this.btnFindPrev.UseVisualStyleBackColor = true;
			this.cbSearchMode.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.cbSearchMode.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSearchMode.IntegralHeight = false;
			this.cbSearchMode.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(118), global::PS3SaveEditor.Util.ScaleSize(1));
			this.cbSearchMode.Margin = new global::System.Windows.Forms.Padding(1, 1, 0, 1);
			this.cbSearchMode.MaximumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 0));
			this.cbSearchMode.Name = "cbSearchMode";
			this.cbSearchMode.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 21));
			this.cbSearchMode.TabIndex = 5;
			this.lstSearchVal.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lstSearchVal.FormattingEnabled = true;
			this.lstSearchVal.IntegralHeight = false;
			this.lstSearchVal.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(260), global::PS3SaveEditor.Util.ScaleSize(1));
			this.lstSearchVal.Margin = new global::System.Windows.Forms.Padding(3, 1, 3, 1);
			this.lstSearchVal.Name = "lstSearchVal";
			this.tableTop.SetRowSpan(this.lstSearchVal, 3);
			this.lstSearchVal.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(74, 45));
			this.lstSearchVal.TabIndex = 6;
			this.lblAddress.AutoSize = true;
			this.lblAddress.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lblAddress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(337), global::PS3SaveEditor.Util.ScaleSize(25));
			this.lblAddress.Margin = new global::System.Windows.Forms.Padding(0);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(50, 22));
			this.lblAddress.TabIndex = 7;
			this.lblAddress.Text = "Address";
			this.lblAddress.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.txtAddress.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txtAddress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(388), global::PS3SaveEditor.Util.ScaleSize(26));
			this.txtAddress.Margin = new global::System.Windows.Forms.Padding(1);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 20));
			this.txtAddress.TabIndex = 8;
			this.btnStackAddress.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnStackAddress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(387), 0);
			this.btnStackAddress.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnStackAddress.Name = "btnStackAddress";
			this.btnStackAddress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(70, 23));
			this.btnStackAddress.TabIndex = 9;
			this.btnStackAddress.Text = "Stack";
			this.btnStackAddress.UseVisualStyleBackColor = true;
			this.btnGo.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnGo.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(459), global::PS3SaveEditor.Util.ScaleSize(25));
			this.btnGo.Margin = new global::System.Windows.Forms.Padding(2, 0, 0, 0);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(38, 22));
			this.btnGo.TabIndex = 10;
			this.btnGo.Text = "OK";
			this.btnGo.UseVisualStyleBackColor = true;
			this.lstSearchAddresses.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lstSearchAddresses.FormattingEnabled = true;
			this.lstSearchAddresses.IntegralHeight = false;
			this.lstSearchAddresses.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(500), global::PS3SaveEditor.Util.ScaleSize(1));
			this.lstSearchAddresses.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(1));
			this.lstSearchAddresses.Name = "lstSearchAddresses";
			this.tableTop.SetRowSpan(this.lstSearchAddresses, global::PS3SaveEditor.Util.ScaleSize(3));
			this.lstSearchAddresses.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(74, 45));
			this.lstSearchAddresses.TabIndex = 11;
			this.tableRight.ColumnCount = 2;
			this.tableRight.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, global::PS3SaveEditor.Util.ScaleSize(50f)));
			this.tableRight.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, global::PS3SaveEditor.Util.ScaleSize(50f)));
			this.tableRight.Controls.Add(this.chkEnableRight, 0, 2);
			this.tableRight.Controls.Add(this.chkSyncScroll, 0, 3);
			this.tableRight.Controls.Add(this.label4, 0, 4);
			this.tableRight.Controls.Add(this.lstCache, 0, 5);
			this.tableRight.Controls.Add(this.btnPush, 0, 6);
			this.tableRight.Controls.Add(this.btnPop, 1, 6);
			this.tableRight.Controls.Add(this.lblCheats, 0, 7);
			this.tableRight.Controls.Add(this.listViewCheats, 0, 8);
			this.tableRight.Controls.Add(this.lblCheatCodes, 0, 9);
			this.tableRight.Controls.Add(this.lstCheatCodes, 0, 10);
			this.tableRight.Controls.Add(this.cbSaveFiles, 0, 0);
			this.tableRight.Controls.Add(this.btnApply, 0, 12);
			this.tableRight.Controls.Add(this.btnClose, 1, 12);
			this.tableRight.Controls.Add(this.btnCompare, 0, 1);
			this.tableRight.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableRight.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(11));
			this.tableRight.Margin = new global::System.Windows.Forms.Padding(3, 3, 2, 3);
			this.tableRight.Name = "tableRight";
			this.tableRight.RowCount = 13;
			this.tableMain.SetRowSpan(this.tableRight, 4);
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 24f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 19f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 19f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 24f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 30f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 80f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableRight.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 24f));
			this.tableRight.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(184, 495));
			this.tableRight.TabIndex = 3;
			this.chkEnableRight.AutoSize = true;
			this.tableRight.SetColumnSpan(this.chkEnableRight, 2);
			this.chkEnableRight.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(47));
			this.chkEnableRight.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(3), 0);
			this.chkEnableRight.Name = "chkEnableRight";
			this.chkEnableRight.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(87, 16));
			this.chkEnableRight.TabIndex = 0;
			this.chkEnableRight.Text = "Enable Right";
			this.chkEnableRight.UseVisualStyleBackColor = true;
			this.chkEnableRight.CheckedChanged += new global::System.EventHandler(this.chkEnableRight_CheckedChanged);
			this.chkSyncScroll.AutoSize = true;
			this.tableRight.SetColumnSpan(this.chkSyncScroll, 2);
			this.chkSyncScroll.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(66));
			this.chkSyncScroll.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(3), 0);
			this.chkSyncScroll.Name = "chkSyncScroll";
			this.chkSyncScroll.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(79, 16));
			this.chkSyncScroll.TabIndex = 1;
			this.chkSyncScroll.Text = "Sync Scroll";
			this.chkSyncScroll.UseVisualStyleBackColor = true;
			this.label4.AutoSize = true;
			this.tableRight.SetColumnSpan(this.label4, 2);
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(82));
			this.label4.Name = "label4";
			this.label4.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(178, 20));
			this.label4.TabIndex = 2;
			this.label4.Text = "Savedata Cache:";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.tableRight.SetColumnSpan(this.lstCache, 2);
			this.lstCache.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lstCache.FormattingEnabled = true;
			this.lstCache.IntegralHeight = false;
			this.lstCache.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(2), global::PS3SaveEditor.Util.ScaleSize(104));
			this.lstCache.Margin = new global::System.Windows.Forms.Padding(2);
			this.lstCache.Name = "lstCache";
			this.lstCache.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(180, 76));
			this.lstCache.TabIndex = 3;
			this.btnPush.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnPush.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(183));
			this.btnPush.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(1));
			this.btnPush.Name = "btnPush";
			this.btnPush.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(90, 22));
			this.btnPush.TabIndex = 4;
			this.btnPush.Text = "Push";
			this.btnPush.UseVisualStyleBackColor = true;
			this.btnPop.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnPop.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(93), global::PS3SaveEditor.Util.ScaleSize(183));
			this.btnPop.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(1));
			this.btnPop.Name = "btnPop";
			this.btnPop.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(90, 22));
			this.btnPop.TabIndex = 5;
			this.btnPop.Text = "Pop";
			this.btnPop.UseVisualStyleBackColor = true;
			this.lblCheats.AutoSize = true;
			this.tableRight.SetColumnSpan(this.lblCheats, 2);
			this.lblCheats.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lblCheats.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(206));
			this.lblCheats.Name = "lblCheats";
			this.lblCheats.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(178, 22));
			this.lblCheats.TabIndex = 6;
			this.lblCheats.Text = "Cheats";
			this.lblCheats.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.tableRight.SetColumnSpan(this.listViewCheats, 2);
			this.listViewCheats.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listViewCheats.FormattingEnabled = true;
			this.listViewCheats.IntegralHeight = false;
			this.listViewCheats.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(2), global::PS3SaveEditor.Util.ScaleSize(230));
			this.listViewCheats.Margin = new global::System.Windows.Forms.Padding(2);
			this.listViewCheats.Name = "listViewCheats";
			this.listViewCheats.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(180, 76));
			this.listViewCheats.TabIndex = 7;
			this.lblCheatCodes.AutoSize = true;
			this.tableRight.SetColumnSpan(this.lblCheatCodes, 2);
			this.lblCheatCodes.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lblCheatCodes.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(308));
			this.lblCheatCodes.Name = "lblCheatCodes";
			this.lblCheatCodes.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(178, 30));
			this.lblCheatCodes.TabIndex = 8;
			this.lblCheatCodes.Text = "Cheat Codes";
			this.lblCheatCodes.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.tableRight.SetColumnSpan(this.lstCheatCodes, 2);
			this.lstCheatCodes.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lstCheatCodes.FormattingEnabled = true;
			this.lstCheatCodes.IntegralHeight = false;
			this.lstCheatCodes.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(2), global::PS3SaveEditor.Util.ScaleSize(340));
			this.lstCheatCodes.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(2));
			this.lstCheatCodes.Name = "lstCheatCodes";
			this.lstCheatCodes.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(180, 76));
			this.lstCheatCodes.TabIndex = 9;
			this.tableRight.SetColumnSpan(this.cbSaveFiles, 2);
			this.cbSaveFiles.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSaveFiles.FormattingEnabled = true;
			this.cbSaveFiles.IntegralHeight = false;
			this.cbSaveFiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(2), 0);
			this.cbSaveFiles.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(2), 0, global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(3));
			this.cbSaveFiles.Name = "cbSaveFiles";
			this.cbSaveFiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(121, 21));
			this.cbSaveFiles.TabIndex = 10;
			this.btnApply.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnApply.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(472));
			this.btnApply.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(1));
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(90, 22));
			this.btnApply.TabIndex = 11;
			this.btnApply.Text = "OK";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnClose.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(93), global::PS3SaveEditor.Util.ScaleSize(472));
			this.btnClose.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(1));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(90, 22));
			this.btnClose.TabIndex = 12;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.tableRight.SetColumnSpan(this.btnCompare, 2);
			this.btnCompare.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(23));
			this.btnCompare.Margin = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(3), global::PS3SaveEditor.Util.ScaleSize(1), global::PS3SaveEditor.Util.ScaleSize(1));
			this.btnCompare.Name = "btnCompare";
			this.btnCompare.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(122, 20));
			this.btnCompare.TabIndex = 13;
			this.btnCompare.Text = "Compare";
			this.btnCompare.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.lblOffsetValue);
			this.panel1.Controls.Add(this.lblOffset);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(11), global::PS3SaveEditor.Util.ScaleSize(482));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(667, 24));
			this.panel1.TabIndex = 30;
			this.lblOffsetValue.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblOffsetValue.AutoSize = true;
			this.lblOffsetValue.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(517), global::PS3SaveEditor.Util.ScaleSize(10));
			this.lblOffsetValue.Name = "lblOffsetValue";
			this.lblOffsetValue.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(66, 13));
			this.lblOffsetValue.TabIndex = 1;
			this.lblOffsetValue.Text = "0x00000000";
			this.lblOffset.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblOffset.AutoSize = true;
			this.lblOffset.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(467), global::PS3SaveEditor.Util.ScaleSize(10));
			this.lblOffset.Name = "lblOffset";
			this.lblOffset.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(38, 13));
			this.lblOffset.TabIndex = 0;
			this.lblOffset.Text = "Offset:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.FromArgb(0, 138, 213);
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(898, 537));
			base.Controls.Add(this.tableMain);
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.Name = "AdvancedEdit2";
			base.Padding = new global::System.Windows.Forms.Padding(global::PS3SaveEditor.Util.ScaleSize(10));
			this.Text = "Advanced Edit";
			this.tableMain.ResumeLayout(false);
			this.tableMain.PerformLayout();
			this.tableLayoutMiddle.ResumeLayout(false);
			this.tableTop.ResumeLayout(false);
			this.tableTop.PerformLayout();
			this.tableRight.ResumeLayout(false);
			this.tableRight.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000CED RID: 3309
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000CEE RID: 3310
		private global::System.Windows.Forms.Label lblGameName;

		// Token: 0x04000CEF RID: 3311
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000CF0 RID: 3312
		private global::System.Windows.Forms.TextBox txtSearchValue;

		// Token: 0x04000CF1 RID: 3313
		private global::System.Windows.Forms.Button btnStackSearch;

		// Token: 0x04000CF2 RID: 3314
		private global::System.Windows.Forms.Button btnFind;

		// Token: 0x04000CF3 RID: 3315
		private global::System.Windows.Forms.Button btnFindPrev;

		// Token: 0x04000CF4 RID: 3316
		private global::System.Windows.Forms.ComboBox cbSearchMode;

		// Token: 0x04000CF5 RID: 3317
		private global::System.Windows.Forms.ListBox lstSearchVal;

		// Token: 0x04000CF6 RID: 3318
		private global::System.Windows.Forms.Label lblAddress;

		// Token: 0x04000CF7 RID: 3319
		private global::System.Windows.Forms.TextBox txtAddress;

		// Token: 0x04000CF8 RID: 3320
		private global::System.Windows.Forms.Button btnStackAddress;

		// Token: 0x04000CF9 RID: 3321
		private global::System.Windows.Forms.Button btnGo;

		// Token: 0x04000CFA RID: 3322
		private global::System.Windows.Forms.ListBox lstSearchAddresses;

		// Token: 0x04000CFB RID: 3323
		private global::System.Windows.Forms.CheckBox chkEnableRight;

		// Token: 0x04000CFC RID: 3324
		private global::System.Windows.Forms.CheckBox chkSyncScroll;

		// Token: 0x04000CFD RID: 3325
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000CFE RID: 3326
		private global::System.Windows.Forms.ListBox lstCache;

		// Token: 0x04000CFF RID: 3327
		private global::System.Windows.Forms.Button btnPush;

		// Token: 0x04000D00 RID: 3328
		private global::System.Windows.Forms.Button btnPop;

		// Token: 0x04000D01 RID: 3329
		private global::System.Windows.Forms.Label lblCheats;

		// Token: 0x04000D02 RID: 3330
		private global::System.Windows.Forms.CheckedListBox listViewCheats;

		// Token: 0x04000D03 RID: 3331
		private global::System.Windows.Forms.Label lblCheatCodes;

		// Token: 0x04000D04 RID: 3332
		private global::System.Windows.Forms.ListBox lstCheatCodes;

		// Token: 0x04000D05 RID: 3333
		private global::System.Windows.Forms.ComboBox cbSaveFiles;

		// Token: 0x04000D06 RID: 3334
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x04000D07 RID: 3335
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000D08 RID: 3336
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000D09 RID: 3337
		private global::System.Windows.Forms.Label lblOffsetValue;

		// Token: 0x04000D0A RID: 3338
		private global::System.Windows.Forms.Label lblOffset;

		// Token: 0x04000D0B RID: 3339
		private global::System.Windows.Forms.Button btnCompare;

		// Token: 0x04000D0C RID: 3340
		private global::Be.Windows.Forms.HexBox hexBoxLeft;

		// Token: 0x04000D0D RID: 3341
		private global::System.Windows.Forms.Panel panelRight;

		// Token: 0x04000D0E RID: 3342
		private global::Be.Windows.Forms.HexBox hexBoxRight;

		// Token: 0x04000D0F RID: 3343
		private global::System.Windows.Forms.RichTextBox txtSaveDataLeft;

		// Token: 0x04000D10 RID: 3344
		private global::System.Windows.Forms.RichTextBox txtSaveDataRight;

		// Token: 0x04000D11 RID: 3345
		private global::System.Windows.Forms.Panel panelLeft;

		// Token: 0x04000D12 RID: 3346
		private global::PS3SaveEditor.CustomTableLayoutPanel tableMain;

		// Token: 0x04000D13 RID: 3347
		private global::PS3SaveEditor.CustomTableLayoutPanel tableTop;

		// Token: 0x04000D14 RID: 3348
		private global::PS3SaveEditor.CustomTableLayoutPanel tableRight;

		// Token: 0x04000D15 RID: 3349
		private global::PS3SaveEditor.CustomTableLayoutPanel tableLayoutMiddle;
	}
}
