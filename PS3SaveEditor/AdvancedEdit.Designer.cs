namespace PS3SaveEditor
{
	// Token: 0x020001AC RID: 428
	public partial class AdvancedEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x0006C898 File Offset: 0x0006AA98
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0006C8D0 File Offset: 0x0006AAD0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PS3SaveEditor.AdvancedEdit));
			this.lblCheatCodes = new global::System.Windows.Forms.Label();
			this.lblCheats = new global::System.Windows.Forms.Label();
			this.btnApply = new global::System.Windows.Forms.Button();
			this.lblOffset = new global::System.Windows.Forms.Label();
			this.lblOffsetValue = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lblLengthVal = new global::System.Windows.Forms.Label();
			this.lblLength = new global::System.Windows.Forms.Label();
			this.lblCurrentFile = new global::System.Windows.Forms.Label();
			this.cbSaveFiles = new global::System.Windows.Forms.ComboBox();
			this.txtSaveData = new global::System.Windows.Forms.RichTextBox();
			this.lblProfile = new global::System.Windows.Forms.Label();
			this.cbProfile = new global::System.Windows.Forms.ComboBox();
			this.btnFindPrev = new global::System.Windows.Forms.Button();
			this.btnFind = new global::System.Windows.Forms.Button();
			this.lblAddress = new global::System.Windows.Forms.Label();
			this.lblDataHex = new global::System.Windows.Forms.Label();
			this.lblDataAscii = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lstCheats = new global::System.Windows.Forms.ListBox();
			this.lstValues = new global::System.Windows.Forms.ListBox();
			this.lblGameName = new global::System.Windows.Forms.Label();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.hexBox1 = new global::Be.Windows.Forms.HexBox();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripButtonSearch = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonUndo = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRedo = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonGoto = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonExport = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonImportFile = new global::System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.lblCheatCodes.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblCheatCodes.AutoSize = true;
			this.lblCheatCodes.ForeColor = global::System.Drawing.Color.White;
			bool flag = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag)
			{
				this.lblCheatCodes.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(654), global::PS3SaveEditor.Util.ScaleSize(146));
			}
			else
			{
				this.lblCheatCodes.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(146));
			}
			this.lblCheatCodes.Name = "lblCheatCodes";
			this.lblCheatCodes.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(71, 13));
			this.lblCheatCodes.TabIndex = 4;
			this.lblCheatCodes.Text = "Cheat Codes:";
			this.lblCheats.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblCheats.AutoSize = true;
			this.lblCheats.ForeColor = global::System.Drawing.Color.White;
			this.lblCheats.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(43));
			this.lblCheats.Name = "lblCheats";
			this.lblCheats.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(43, 13));
			this.lblCheats.TabIndex = 5;
			this.lblCheats.Text = "Cheats:";
			this.btnApply.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnApply.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnApply.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnApply.ForeColor = global::System.Drawing.Color.White;
			this.btnApply.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(725), global::PS3SaveEditor.Util.ScaleSize(317));
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(57, 23));
			this.btnApply.TabIndex = 6;
			this.btnApply.Text = "Apply && Download";
			this.btnApply.UseVisualStyleBackColor = false;
			this.lblOffset.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblOffset.AutoSize = true;
			this.lblOffset.ForeColor = global::System.Drawing.Color.White;
			bool flag2 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag2)
			{
				this.lblOffset.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(410), global::PS3SaveEditor.Util.ScaleSize(322));
			}
			else
			{
				this.lblOffset.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(480), global::PS3SaveEditor.Util.ScaleSize(322));
			}
			this.lblOffset.Name = "lblOffset";
			this.lblOffset.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(38, 13));
			this.lblOffset.TabIndex = 8;
			this.lblOffset.Text = "Offset:";
			this.lblOffsetValue.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblOffsetValue.AutoSize = true;
			this.lblOffsetValue.ForeColor = global::System.Drawing.Color.White;
			this.lblOffsetValue.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(518), global::PS3SaveEditor.Util.ScaleSize(322));
			this.lblOffsetValue.Name = "lblOffsetValue";
			this.lblOffsetValue.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblOffsetValue.TabIndex = 9;
			bool flag3 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag3)
			{
				this.lblOffsetValue.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, global::PS3SaveEditor.Util.ScaleSize(12));
			}
			this.lblLengthVal.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblLengthVal.AutoSize = true;
			this.lblLengthVal.BackColor = global::System.Drawing.Color.Transparent;
			this.lblLengthVal.ForeColor = global::System.Drawing.Color.White;
			this.lblLengthVal.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(631), global::PS3SaveEditor.Util.ScaleSize(322));
			this.lblLengthVal.Name = "lblLengthVal";
			this.lblLengthVal.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(0, 13));
			this.lblLengthVal.TabIndex = 29;
			bool flag4 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag4)
			{
				this.lblLengthVal.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, global::PS3SaveEditor.Util.ScaleSize(12));
			}
			this.panel1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.panel1.Controls.Add(this.lblLengthVal);
			this.panel1.Controls.Add(this.lblLength);
			this.panel1.Controls.Add(this.lblCurrentFile);
			this.panel1.Controls.Add(this.cbSaveFiles);
			this.panel1.Controls.Add(this.txtSaveData);
			this.panel1.Controls.Add(this.lblProfile);
			this.panel1.Controls.Add(this.cbProfile);
			this.panel1.Controls.Add(this.btnFindPrev);
			this.panel1.Controls.Add(this.btnFind);
			this.panel1.Controls.Add(this.lblAddress);
			this.panel1.Controls.Add(this.lblDataHex);
			this.panel1.Controls.Add(this.lblDataAscii);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.lstCheats);
			this.panel1.Controls.Add(this.lstValues);
			this.panel1.Controls.Add(this.lblGameName);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.lblOffsetValue);
			this.panel1.Controls.Add(this.lblOffset);
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Controls.Add(this.lblCheats);
			this.panel1.Controls.Add(this.lblCheatCodes);
			this.panel1.Controls.Add(this.hexBox1);
			this.panel1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(11));
			this.panel1.Name = "panel1";
			this.panel1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(856, 348));
			this.panel1.TabIndex = 10;
			this.lblLength.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblLength.AutoSize = true;
			this.lblLength.BackColor = global::System.Drawing.Color.Transparent;
			this.lblLength.ForeColor = global::System.Drawing.Color.White;
			bool flag5 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag5)
			{
				this.lblLength.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(558), global::PS3SaveEditor.Util.ScaleSize(322));
			}
			else
			{
				this.lblLength.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(588), global::PS3SaveEditor.Util.ScaleSize(322));
			}
			this.lblLength.Name = "lblLength";
			this.lblLength.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(43, 13));
			this.lblLength.TabIndex = 27;
			this.lblLength.Text = "Length:";
			this.lblCurrentFile.AutoSize = true;
			this.lblCurrentFile.BackColor = global::System.Drawing.Color.Transparent;
			this.lblCurrentFile.ForeColor = global::System.Drawing.Color.White;
			this.lblCurrentFile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(25));
			this.lblCurrentFile.Name = "lblCurrentFile";
			this.lblCurrentFile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(60, 13));
			this.lblCurrentFile.TabIndex = 26;
			this.lblCurrentFile.Text = "Current file:";
			this.cbSaveFiles.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSaveFiles.FormattingEnabled = true;
			this.cbSaveFiles.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(94), global::PS3SaveEditor.Util.ScaleSize(22));
			this.cbSaveFiles.Name = "cbSaveFiles";
			this.cbSaveFiles.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(121, 21));
			this.cbSaveFiles.Sorted = true;
			this.cbSaveFiles.TabIndex = 25;
			this.txtSaveData.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.txtSaveData.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(63));
			this.txtSaveData.Name = "txtSaveData";
			this.txtSaveData.ScrollBars = global::System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtSaveData.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(666, 244));
			this.txtSaveData.TabIndex = 24;
			this.txtSaveData.Text = "";
			this.txtSaveData.Visible = false;
			this.lblProfile.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.lblProfile.AutoSize = true;
			this.lblProfile.ForeColor = global::System.Drawing.Color.White;
			this.lblProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(307), global::PS3SaveEditor.Util.ScaleSize(321));
			this.lblProfile.Name = "lblProfile";
			this.lblProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(39, 13));
			this.lblProfile.TabIndex = 23;
			this.lblProfile.Text = "Profile:";
			this.cbProfile.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.cbProfile.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProfile.FormattingEnabled = true;
			this.cbProfile.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(347), global::PS3SaveEditor.Util.ScaleSize(317));
			this.cbProfile.Name = "cbProfile";
			this.cbProfile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(112, 21));
			this.cbProfile.TabIndex = 22;
			this.btnFindPrev.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnFindPrev.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnFindPrev.ForeColor = global::System.Drawing.Color.White;
			this.btnFindPrev.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(221), global::PS3SaveEditor.Util.ScaleSize(316));
			this.btnFindPrev.Name = "btnFindPrev";
			this.btnFindPrev.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(81, 23));
			this.btnFindPrev.TabIndex = 21;
			this.btnFindPrev.Text = "Find Previous";
			this.btnFindPrev.UseVisualStyleBackColor = false;
			this.btnFindPrev.Visible = false;
			this.btnFind.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnFind.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnFind.ForeColor = global::System.Drawing.Color.White;
			this.btnFind.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(152), global::PS3SaveEditor.Util.ScaleSize(316));
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(63, 23));
			this.btnFind.TabIndex = 20;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = false;
			this.btnFind.Visible = false;
			this.lblAddress.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(7.8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblAddress.ForeColor = global::System.Drawing.Color.White;
			this.lblAddress.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(14), global::PS3SaveEditor.Util.ScaleSize(45));
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(68, 15));
			this.lblAddress.TabIndex = 17;
			this.lblAddress.Text = "Address";
			this.lblDataHex.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(7.8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblDataHex.ForeColor = global::System.Drawing.Color.White;
			bool flag6 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.Linux;
			if (flag6)
			{
				switch (global::PS3SaveEditor.Util.ScaleIndex)
				{
				case 0:
					this.lblDataHex.Location = new global::System.Drawing.Point(66, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 1:
					this.lblDataHex.Location = new global::System.Drawing.Point(90, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 2:
					this.lblDataHex.Location = new global::System.Drawing.Point(113, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 3:
					this.lblDataHex.Location = new global::System.Drawing.Point(127, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 4:
					this.lblDataHex.Location = new global::System.Drawing.Point(150, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 5:
					this.lblDataHex.Location = new global::System.Drawing.Point(175, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				}
			}
			else
			{
				switch (global::PS3SaveEditor.Util.ScaleIndex)
				{
				case 0:
					this.lblDataHex.Location = new global::System.Drawing.Point(66, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 1:
					this.lblDataHex.Location = new global::System.Drawing.Point(100, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 2:
					this.lblDataHex.Location = new global::System.Drawing.Point(113, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 3:
					this.lblDataHex.Location = new global::System.Drawing.Point(137, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 4:
					this.lblDataHex.Location = new global::System.Drawing.Point(160, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 5:
					this.lblDataHex.Location = new global::System.Drawing.Point(185, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				}
			}
			this.lblDataHex.Name = "lblAddress";
			this.lblDataHex.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(80, 15));
			this.lblDataHex.TabIndex = 18;
			this.lblDataHex.Text = "Data (Hex)";
			this.lblDataAscii.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(7.8f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblDataAscii.ForeColor = global::System.Drawing.Color.White;
			bool flag7 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.Linux;
			if (flag7)
			{
				switch (global::PS3SaveEditor.Util.ScaleIndex)
				{
				case 0:
					this.lblDataAscii.Location = new global::System.Drawing.Point(314, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 1:
					this.lblDataAscii.Location = new global::System.Drawing.Point(440, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 2:
					this.lblDataAscii.Location = new global::System.Drawing.Point(562, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 3:
					this.lblDataAscii.Location = new global::System.Drawing.Point(625, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 4:
					this.lblDataAscii.Location = new global::System.Drawing.Point(748, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 5:
					this.lblDataAscii.Location = new global::System.Drawing.Point(872, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				}
			}
			else
			{
				switch (global::PS3SaveEditor.Util.ScaleIndex)
				{
				case 0:
					this.lblDataAscii.Location = new global::System.Drawing.Point(314, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 1:
					this.lblDataAscii.Location = new global::System.Drawing.Point(497, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 2:
					this.lblDataAscii.Location = new global::System.Drawing.Point(562, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 3:
					this.lblDataAscii.Location = new global::System.Drawing.Point(685, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 4:
					this.lblDataAscii.Location = new global::System.Drawing.Point(808, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				case 5:
					this.lblDataAscii.Location = new global::System.Drawing.Point(932, global::PS3SaveEditor.Util.ScaleSize(45));
					break;
				}
			}
			this.lblDataAscii.Name = "lblAddress";
			this.lblDataAscii.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(85, 15));
			this.lblDataAscii.TabIndex = 19;
			this.lblDataAscii.Text = "Data (Ascii)";
			this.label1.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.White;
			this.label1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(10), global::PS3SaveEditor.Util.ScaleSize(321));
			this.label1.Name = "label1";
			this.label1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(41, 13));
			this.label1.TabIndex = 15;
			this.label1.Text = "Search";
			this.label1.Visible = false;
			this.lstCheats.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lstCheats.FormattingEnabled = true;
			this.lstCheats.IntegralHeight = false;
			this.lstCheats.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(63));
			this.lstCheats.Name = "lstCheats";
			this.lstCheats.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(160, 74));
			this.lstCheats.TabIndex = 14;
			this.lstValues.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lstValues.FormattingEnabled = true;
			bool flag8 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag8)
			{
				this.lstValues.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(169));
			}
			else
			{
				this.lstValues.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(684), global::PS3SaveEditor.Util.ScaleSize(162));
			}
			this.lstValues.MultiColumn = true;
			this.lstValues.Name = "lstValues";
			this.lstValues.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(160, 147));
			this.lstValues.TabIndex = 13;
			this.lblGameName.AutoSize = true;
			this.lblGameName.ForeColor = global::System.Drawing.Color.White;
			this.lblGameName.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(4));
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(28, 13));
			this.lblGameName.TabIndex = 12;
			this.lblGameName.Text = "Test";
			this.btnClose.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnClose.BackColor = global::System.Drawing.Color.FromArgb(246, 128, 31);
			this.btnClose.ForeColor = global::System.Drawing.Color.White;
			this.btnClose.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(787), global::PS3SaveEditor.Util.ScaleSize(317));
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(57, 23));
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.hexBox1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.hexBox1.Font = new global::System.Drawing.Font("Courier New", global::PS3SaveEditor.Util.ScaleSize(9f), global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.hexBox1.HScrollBarVisible = false;
			this.hexBox1.LineInfoForeColor = global::System.Drawing.Color.Empty;
			this.hexBox1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(13), global::PS3SaveEditor.Util.ScaleSize(63));
			this.hexBox1.Name = "hexBox1";
			bool flag9 = global::PS3SaveEditor.Util.CurrentPlatform == global::PS3SaveEditor.Util.Platform.MacOS;
			if (flag9)
			{
				this.hexBox1.ForeColor = global::System.Drawing.Color.Black;
			}
			this.hexBox1.ShadowSelectionColor = global::System.Drawing.Color.FromArgb(100, 60, 188, 255);
			this.hexBox1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(666, 244));
			this.hexBox1.TabIndex = 0;
			this.toolStrip1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.toolStripButtonSearch, this.toolStripButtonUndo, this.toolStripButtonRedo, this.toolStripButtonGoto, this.toolStripButtonExport, this.toolStripButtonImportFile });
			this.toolStrip1.Location = new global::System.Drawing.Point(global::PS3SaveEditor.Util.ScaleSize(682), global::PS3SaveEditor.Util.ScaleSize(13));
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(181, 25));
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripButtonSearch.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSearch.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonSearch.Image");
			this.toolStripButtonSearch.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonSearch.Name = "toolStripButtonSearch";
			this.toolStripButtonSearch.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonSearch.Text = "Search";
			this.toolStripButtonUndo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonUndo.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonUndo.Image");
			this.toolStripButtonUndo.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonUndo.Name = "toolStripButtonUndo";
			this.toolStripButtonUndo.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonUndo.Text = "Undo";
			this.toolStripButtonRedo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRedo.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonRedo.Image");
			this.toolStripButtonRedo.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonRedo.Name = "toolStripButtonRedo";
			this.toolStripButtonRedo.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonRedo.Text = "Redo";
			this.toolStripButtonGoto.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonGoto.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonGoto.Image");
			this.toolStripButtonGoto.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonGoto.Name = "toolStripButtonGoto";
			this.toolStripButtonGoto.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonGoto.Text = "Go to location";
			this.toolStripButtonExport.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonExport.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonExport.Image");
			this.toolStripButtonExport.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonExport.Name = "toolStripButtonExport";
			this.toolStripButtonExport.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonExport.Text = "Export to file";
			this.toolStripButtonImportFile.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonImportFile.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonImportFile.Image");
			this.toolStripButtonImportFile.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonImportFile.Name = "toolStripButtonImportFile";
			this.toolStripButtonImportFile.Size = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(23, 22));
			this.toolStripButtonImportFile.Text = "Import File";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(global::PS3SaveEditor.Util.ScaleSize(6f), global::PS3SaveEditor.Util.ScaleSize(13f));
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(876, 369));
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.panel1);
			base.Icon = global::PS3SaveEditor.Resources.Resources.dp;
			base.KeyPreview = true;
			this.MinimumSize = global::PS3SaveEditor.Util.ScaleSize(new global::System.Drawing.Size(856, 362));
			base.Name = "AdvancedEdit";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Advanced Edit";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000A26 RID: 2598
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000A27 RID: 2599
		private global::Be.Windows.Forms.HexBox hexBox1;

		// Token: 0x04000A28 RID: 2600
		private global::System.Windows.Forms.Label lblCheatCodes;

		// Token: 0x04000A29 RID: 2601
		private global::System.Windows.Forms.Label lblCheats;

		// Token: 0x04000A2A RID: 2602
		private global::System.Windows.Forms.Button btnApply;

		// Token: 0x04000A2B RID: 2603
		private global::System.Windows.Forms.Label lblOffset;

		// Token: 0x04000A2C RID: 2604
		private global::System.Windows.Forms.Label lblOffsetValue;

		// Token: 0x04000A2D RID: 2605
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000A2E RID: 2606
		private global::System.Windows.Forms.ListBox lstCheats;

		// Token: 0x04000A2F RID: 2607
		private global::System.Windows.Forms.ListBox lstValues;

		// Token: 0x04000A30 RID: 2608
		private global::System.Windows.Forms.Label lblGameName;

		// Token: 0x04000A31 RID: 2609
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000A32 RID: 2610
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000A33 RID: 2611
		private global::System.Windows.Forms.Button btnFindPrev;

		// Token: 0x04000A34 RID: 2612
		private global::System.Windows.Forms.Button btnFind;

		// Token: 0x04000A35 RID: 2613
		private global::System.Windows.Forms.Label lblAddress;

		// Token: 0x04000A36 RID: 2614
		private global::System.Windows.Forms.Label lblDataHex;

		// Token: 0x04000A37 RID: 2615
		private global::System.Windows.Forms.Label lblDataAscii;

		// Token: 0x04000A38 RID: 2616
		private global::System.Windows.Forms.ComboBox cbProfile;

		// Token: 0x04000A39 RID: 2617
		private global::System.Windows.Forms.Label lblProfile;

		// Token: 0x04000A3A RID: 2618
		private global::System.Windows.Forms.RichTextBox txtSaveData;

		// Token: 0x04000A3B RID: 2619
		private global::System.Windows.Forms.ComboBox cbSaveFiles;

		// Token: 0x04000A3C RID: 2620
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000A3D RID: 2621
		private global::System.Windows.Forms.ToolStripButton toolStripButtonSearch;

		// Token: 0x04000A3E RID: 2622
		private global::System.Windows.Forms.ToolStripButton toolStripButtonUndo;

		// Token: 0x04000A3F RID: 2623
		private global::System.Windows.Forms.ToolStripButton toolStripButtonRedo;

		// Token: 0x04000A40 RID: 2624
		private global::System.Windows.Forms.ToolStripButton toolStripButtonGoto;

		// Token: 0x04000A41 RID: 2625
		private global::System.Windows.Forms.ToolStripButton toolStripButtonExport;

		// Token: 0x04000A42 RID: 2626
		private global::System.Windows.Forms.Label lblCurrentFile;

		// Token: 0x04000A43 RID: 2627
		private global::System.Windows.Forms.Label lblLengthVal;

		// Token: 0x04000A44 RID: 2628
		private global::System.Windows.Forms.Label lblLength;

		// Token: 0x04000A45 RID: 2629
		private global::System.Windows.Forms.ToolStripButton toolStripButtonImportFile;
	}
}
