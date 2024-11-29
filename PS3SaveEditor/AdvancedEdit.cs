using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using Microsoft.Win32;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001AC RID: 428
	public partial class AdvancedEdit : Form
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00069E10 File Offset: 0x00068010
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00069E28 File Offset: 0x00068028
		public bool TextMode
		{
			get
			{
				return this.m_bTextMode;
			}
			set
			{
				this.m_searchForm.TextMode = value;
				this.m_bTextMode = value;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00069E40 File Offset: 0x00068040
		public AdvancedEdit(game game, Dictionary<string, byte[]> data)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.KeyDown += this.AdvancedEdit_KeyDown;
			this.btnFindPrev.Click += this.button1_Click;
			this.btnFind.Click += this.btnFind_Click;
			this.hexBox1.KeyDown += this.hexBox1_KeyDown;
			this.hexBox1.SelectionBackColor = Color.FromArgb(0, 175, 255);
			this.hexBox1.ShadowSelectionColor = Color.FromArgb(204, 240, 255);
			this.lstCheats.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstCheats.DrawItem += this.lstCheats_DrawItem;
			this.lstValues.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstValues.DrawItem += this.lstValues_DrawItem;
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.m_searchForm = new Search(this);
			this.TextMode = false;
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnApply.ForeColor = Color.Black;
			this.btnClose.BackColor = SystemColors.ButtonFace;
			this.btnClose.ForeColor = Color.Black;
			this.btnFind.BackColor = SystemColors.ButtonFace;
			this.btnFind.ForeColor = Color.Black;
			this.btnFindPrev.BackColor = SystemColors.ButtonFace;
			this.btnFindPrev.ForeColor = Color.Black;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.label1.BackColor = (this.lblAddress.BackColor = (this.lblCheatCodes.BackColor = (this.lblCheats.BackColor = (this.lblGameName.BackColor = (this.lblOffset.BackColor = (this.lblOffsetValue.BackColor = (this.lblProfile.BackColor = Color.Transparent)))))));
			this.lblDataHex.BackColor = (this.lblDataAscii.BackColor = Color.Transparent);
			this.lblProfile.Visible = false;
			this.cbProfile.Visible = false;
			this.m_DirtyFiles = new List<string>();
			this.m_saveFilesData = data;
			this.btnFind.Text = Resources.btnFind;
			this.btnFindPrev.Text = Resources.btnFindPrev;
			this.lblProfile.Text = Resources.lblProfile;
			this.label1.Text = Resources.lblSearch;
			this.lblAddress.Text = Resources.lblAddressExtra;
			this.lblDataHex.Text = Resources.lblDataHex;
			this.lblDataAscii.Text = Resources.lblDataAscii;
			this.lblCurrentFile.Text = Resources.lblCurrentFile;
			this.lblLength.Text = Resources.lblLength;
			this.SetLabels();
			this.FillProfiles();
			this.lblGameName.Text = game.name;
			this.m_game = game;
			base.CenterToScreen();
			this.btnApply.Enabled = false;
			this.lstValues.SelectedIndexChanged += this.lstValues_SelectedIndexChanged;
			this.lstCheats.SelectedIndexChanged += this.lstCheats_SelectedIndexChanged;
			this.cbSaveFiles.SelectedIndexChanged += this.cbSaveFiles_SelectedIndexChanged;
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			string sysVer = MainForm3.GetSysVer(this.m_game.LocalSaveFolder);
			bool flag = sysVer == "All";
			if (flag)
			{
				this.toolStripButtonImportFile.Visible = false;
			}
			bool flag2 = targetGameFolder != null;
			if (flag2)
			{
				this.cbSaveFiles.Sorted = true;
				foreach (string text in data.Keys)
				{
					this.cbSaveFiles.Items.Add(text);
				}
				bool flag3 = this.cbSaveFiles.Items.Count > 0;
				if (flag3)
				{
					this.cbSaveFiles.SelectedIndex = 0;
				}
			}
			bool flag4 = this.cbSaveFiles.Items.Count == 1;
			if (flag4)
			{
				this.cbSaveFiles.Enabled = false;
			}
			this.btnApply.Click += this.btnApply_Click;
			this.btnClose.Click += this.btnClose_Click;
			bool flag5 = this.lstCheats.Items.Count > 0;
			if (flag5)
			{
				this.lstCheats.SelectedIndex = 0;
			}
			base.ResizeBegin += delegate(object s, EventArgs e)
			{
				base.SuspendLayout();
				this.panel1.BackColor = Color.FromArgb(0, 138, 213);
				this._resizeInProgress = true;
			};
			base.ResizeEnd += delegate(object s, EventArgs e)
			{
				base.ResumeLayout(true);
				this._resizeInProgress = false;
				this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
				base.Invalidate(true);
			};
			base.SizeChanged += delegate(object s, EventArgs e)
			{
				bool flag6 = base.WindowState == FormWindowState.Maximized;
				if (flag6)
				{
					this._resizeInProgress = false;
					this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
					base.Invalidate(true);
				}
			};
			base.Disposed += this.AdvancedEdit_Disposed;
			this.toolStripButtonExport.Click += this.toolStripButtonExport_Click;
			this.toolStripButtonGoto.Click += this.toolStripButtonGoto_Click;
			this.toolStripButtonImportFile.Click += this.toolStripButtonImportFile_Click;
			this.toolStripButtonUndo.Click += this.toolStripButtonUndo_Click;
			this.toolStripButtonRedo.Click += this.toolStripButtonRedo_Click;
			this.toolStripButtonSearch.Click += this.toolStripButtonSearch_Click;
			this.cbSaveFiles.Width = Math.Min(200, this.ComboBoxWidth(this.cbSaveFiles));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0006A488 File Offset: 0x00068688
		private void toolStripButtonImportFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			bool flag = openFileDialog.ShowDialog(this) == DialogResult.OK;
			if (flag)
			{
				byte[] array = File.ReadAllBytes(openFileDialog.FileName);
				bool flag2 = array.Length < Util.GetMinFileSize() || array.Length > Util.GetMaxFileSize();
				if (flag2)
				{
					Util.ShowMessage(Resources.errFileSizeOutOfRange);
				}
				else
				{
					bool flag3 = this.m_DirtyFiles.IndexOf(this.m_cursaveFile) < 0;
					if (flag3)
					{
						this.m_DirtyFiles.Add(this.m_cursaveFile);
					}
					this.m_undoList[this.m_cursaveFile].Clear();
					this.hexBox1.DifferDict.Clear();
					this.provider = new DynamicByteProvider(array);
					this.provider.Changed += this.provider_Changed;
					this.hexBox1.ByteProvider = this.provider;
					this.hexBox1.Refresh();
					this.btnApply.Enabled = true;
				}
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0006A58C File Offset: 0x0006878C
		private void AdvancedEdit_Disposed(object sender, EventArgs e)
		{
			bool flag = !this.m_searchForm.IsDisposed;
			if (flag)
			{
				this.m_searchForm.Dispose();
			}
			bool flag2 = !this.hexBox1.IsDisposed;
			if (flag2)
			{
				this.hexBox1.Dispose();
			}
			bool flag3 = this.provider != null && this.provider.Bytes != null;
			if (flag3)
			{
				this.provider.Bytes.Clear();
			}
			this.m_saveFilesData.Clear();
			GC.Collect();
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0006A616 File Offset: 0x00068816
		private void toolStripButtonSearch_Click(object sender, EventArgs e)
		{
			this.m_searchForm.Hide();
			this.m_searchForm.Show(this);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0006A634 File Offset: 0x00068834
		private void toolStripButtonRedo_Click(object sender, EventArgs e)
		{
			bool flag = this.m_redoList[this.m_cursaveFile].Count > 0;
			if (flag)
			{
				ActionItem actionItem = this.m_redoList[this.m_cursaveFile].Pop();
				this.m_undoList[this.m_cursaveFile].Push(actionItem);
				this.hexBox1.ScrollByteIntoView(actionItem.Location);
				this.hexBox1.ByteProvider.WriteByte(actionItem.Location, actionItem.NewValue, true);
				this.hexBox1.Refresh();
			}
			this.toolStripButtonUndo.Enabled = this.m_undoList[this.m_cursaveFile].Count != 0;
			this.toolStripButtonRedo.Enabled = this.m_redoList[this.m_cursaveFile].Count != 0;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0006A71C File Offset: 0x0006891C
		private void toolStripButtonUndo_Click(object sender, EventArgs e)
		{
			bool flag = this.m_undoList[this.m_cursaveFile].Count > 0;
			if (flag)
			{
				ActionItem actionItem = this.m_undoList[this.m_cursaveFile].Pop();
				this.m_redoList[this.m_cursaveFile].Push(actionItem);
				this.hexBox1.ScrollByteIntoView(actionItem.Location);
				this.hexBox1.ByteProvider.WriteByte(actionItem.Location, actionItem.Value, true);
				this.hexBox1.Refresh();
			}
			this.toolStripButtonUndo.Enabled = this.m_undoList[this.m_cursaveFile].Count != 0;
			this.toolStripButtonRedo.Enabled = this.m_redoList[this.m_cursaveFile].Count != 0;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0006A804 File Offset: 0x00068A04
		private void toolStripButtonGoto_Click(object sender, EventArgs e)
		{
			this.DoGoTo();
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0006A810 File Offset: 0x00068A10
		private void toolStripButtonExport_Click(object sender, EventArgs e)
		{
			byte[] array = this.m_saveFilesData[this.m_cursaveFile];
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = this.m_cursaveFile;
			bool flag = saveFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				File.WriteAllBytes(saveFileDialog.FileName, array);
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0006A860 File Offset: 0x00068A60
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 274;
			if (flag)
			{
				bool flag2 = m.WParam == new IntPtr(61488);
				if (flag2)
				{
					this.panel1.BackColor = Color.FromArgb(0, 138, 213);
					this._resizeInProgress = true;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0006A8C8 File Offset: 0x00068AC8
		private int ComboBoxWidth(ComboBox myCombo)
		{
			int num = 0;
			foreach (object obj in myCombo.Items)
			{
				int width = TextRenderer.MeasureText(myCombo.GetItemText(obj), myCombo.Font).Width;
				bool flag = width > num;
				if (flag)
				{
					num = width;
				}
			}
			return num + SystemInformation.VerticalScrollBarWidth;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0006A958 File Offset: 0x00068B58
		private void lstValues_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(72, 187, 97)), new Rectangle(e.Bounds.Left, e.Bounds.Top, this.lstValues.Width, e.Bounds.Height));
					e.Graphics.DrawString((string)this.lstValues.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstValues.Items[e.Index], e.Font, new SolidBrush(Color.Black), new Rectangle(e.Bounds.Left, e.Bounds.Top, this.lstValues.Width, e.Bounds.Height), StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0006AABC File Offset: 0x00068CBC
		private void lstCheats_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
					e.Graphics.DrawString((string)this.lstCheats.Items[e.Index], e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString((string)this.lstCheats.Items[e.Index], e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0006ABB8 File Offset: 0x00068DB8
		protected override void OnPaint(PaintEventArgs e)
		{
			bool resizeInProgress = this._resizeInProgress;
			if (!resizeInProgress)
			{
				base.OnPaint(e);
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0006ABDC File Offset: 0x00068DDC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool resizeInProgress = this._resizeInProgress;
			if (!resizeInProgress)
			{
				bool flag = base.ClientRectangle.Width == 0 || base.ClientRectangle.Height == 0;
				if (!flag)
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
					{
						e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
					}
				}
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006AC80 File Offset: 0x00068E80
		private void txtSaveData_TextChanged(object sender, EventArgs e)
		{
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
			string text = "";
			bool flag = gameFile != null && gameFile.TextMode > 0;
			if (flag)
			{
				bool flag2 = gameFile.TextMode == 1;
				if (flag2)
				{
					text = Encoding.UTF8.GetString(this.m_saveFilesData[this.m_cursaveFile]);
				}
				else
				{
					bool flag3 = gameFile.TextMode == 3;
					if (flag3)
					{
						text = Encoding.Unicode.GetString(this.m_saveFilesData[this.m_cursaveFile]);
					}
					else
					{
						text = Encoding.ASCII.GetString(this.m_saveFilesData[this.m_cursaveFile]);
					}
				}
			}
			string[] array = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = this.txtSaveData.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			bool flag4 = array.Length == array2.Length;
			if (flag4)
			{
				bool flag5 = false;
				for (int i = 0; i < array.Length; i++)
				{
					bool flag6 = array[i] != array2[i];
					if (flag6)
					{
						flag5 = true;
						break;
					}
				}
				bool flag7 = !flag5;
				if (flag7)
				{
					return;
				}
			}
			this.btnApply.Enabled = true;
			bool flag8 = this.m_DirtyFiles.IndexOf(this.m_cursaveFile) < 0;
			if (flag8)
			{
				this.m_DirtyFiles.Add(this.m_cursaveFile);
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0006AE0C File Offset: 0x0006900C
		private void SetLabels()
		{
			this.lblOffset.Text = Resources.lblOffset;
			this.lblCheatCodes.Text = Resources.lblCodes;
			this.lblCheats.Text = Resources.lblCheats;
			this.btnApply.Text = Resources.btnApplyDownload;
			this.btnClose.Text = Resources.btnClose;
			this.Text = Resources.titleAdvEdit;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0006AE7C File Offset: 0x0006907C
		private void hexBox1_SelectionStartChanged(object sender, EventArgs e)
		{
			long num = (long)this.hexBox1.BytesPerLine * (this.hexBox1.CurrentLine - 1L) + (this.hexBox1.CurrentPositionInLine - 1L);
			this.lblOffsetValue.Text = "0x" + string.Format("{0:X}", num).PadLeft(8, '0');
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0006AEE4 File Offset: 0x000690E4
		protected override void OnClosed(EventArgs e)
		{
			bool flag = this.provider != null && this.provider.Bytes != null;
			if (flag)
			{
				this.provider.Bytes.Clear();
			}
			bool flag2 = !this.hexBox1.IsDisposed;
			if (flag2)
			{
				this.hexBox1.Dispose();
			}
			base.OnClosed(e);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000021C5 File Offset: 0x000003C5
		private void provider_LengthChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0006AF48 File Offset: 0x00069148
		private void provider_Changed(object sender, ByteProviderChanged e)
		{
			this.btnApply.Enabled = true;
			bool flag = !this.hexBox1.DifferDict.ContainsKey(e.Index);
			if (flag)
			{
				this.hexBox1.DifferDict[e.Index] = e.OldValue;
			}
			bool flag2 = this.m_DirtyFiles.IndexOf(this.m_cursaveFile) < 0;
			if (flag2)
			{
				this.m_DirtyFiles.Add(this.m_cursaveFile);
			}
			bool flag3 = !this.m_undoList.ContainsKey(this.m_cursaveFile);
			if (flag3)
			{
				this.m_undoList.Add(this.m_cursaveFile, new Stack<ActionItem>());
			}
			this.m_undoList[this.m_cursaveFile].Push(new ActionItem
			{
				Location = e.Index,
				Value = e.OldValue,
				NewValue = e.NewValue
			});
			this.toolStripButtonUndo.Enabled = true;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0006B048 File Offset: 0x00069248
		private void lstCheats_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool bTextMode = this.m_bTextMode;
			if (!bTextMode)
			{
				this.lstValues.Items.Clear();
				int selectedIndex = this.lstCheats.SelectedIndex;
				string text = this.cbSaveFiles.SelectedItem.ToString();
				bool flag = selectedIndex >= 0;
				if (flag)
				{
					container targetGameFolder = this.m_game.GetTargetGameFolder();
					bool flag2 = targetGameFolder != null;
					if (flag2)
					{
						foreach (file file in targetGameFolder.files._files)
						{
							List<string> saveFiles = this.m_game.GetSaveFiles();
							bool flag3 = saveFiles != null;
							if (flag3)
							{
								foreach (string text2 in saveFiles)
								{
									bool flag4 = Path.GetFileName(text2) == text || Util.IsMatch(text, file.filename);
									if (flag4)
									{
										cheat cheat = file.GetCheat(this.lstCheats.Items[selectedIndex].ToString());
										bool flag5 = cheat != null;
										if (flag5)
										{
											string code = cheat.code;
											bool flag6 = string.IsNullOrEmpty(code);
											if (!flag6)
											{
												string[] array = code.Trim().Split(new char[] { ' ', '\r', '\n' });
												for (int i = 0; i < array.Length - 1; i += 2)
												{
													this.lstValues.Items.Add(array[i] + " " + array[i + 1]);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0006B25C File Offset: 0x0006945C
		private void lstValues_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.lstValues.SelectedIndex < 0 || this.m_bTextMode;
			if (!flag)
			{
				bool flag2 = this.lstValues.Items[0].ToString()[0] == 'F';
				if (!flag2)
				{
					this.hexBox1.SelectAddresses.Clear();
					string text = this.lstValues.Items[this.lstValues.SelectedIndex].ToString();
					string[] array = text.Split(new char[] { ' ' });
					int num;
					long memLocation = cheat.GetMemLocation(array[0], out num);
					bool flag3 = this.provider.Length > memLocation;
					if (flag3)
					{
						this.hexBox1.SelectAddresses.Add(memLocation, cheat.GetBitCodeBytes(num));
						this.hexBox1.ScrollByteIntoView(memLocation);
						this.hexBox1.Invalidate();
					}
				}
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0006B350 File Offset: 0x00069550
		private void btnApply_Click(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(Resources.warnOverwriteAdv, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
			if (!flag)
			{
				bool flag2 = !this.m_bTextMode;
				if (flag2)
				{
					this.provider.ApplyChanges();
					bool flag3 = this.m_cursaveFile == null;
					if (flag3)
					{
						this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
					}
					this.m_saveFilesData[this.m_cursaveFile] = this.provider.Bytes.ToArray();
				}
				else
				{
					container targetGameFolder = this.m_game.GetTargetGameFolder();
					file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
					bool flag4 = gameFile.TextMode == 1;
					if (flag4)
					{
						this.m_saveFilesData[this.m_cursaveFile] = Encoding.UTF8.GetBytes(this.txtSaveData.Text);
					}
					else
					{
						bool flag5 = gameFile.TextMode == 3;
						if (flag5)
						{
							this.m_saveFilesData[this.m_cursaveFile] = Encoding.Unicode.GetBytes(this.txtSaveData.Text);
						}
						else
						{
							this.m_saveFilesData[this.m_cursaveFile] = Encoding.ASCII.GetBytes(this.txtSaveData.Text);
						}
					}
				}
				bool flag6 = this.m_game.GetTargetGameFolder() == null;
				if (flag6)
				{
					Util.ShowMessage(Resources.errSaveData, Resources.msgError);
				}
				else
				{
					container targetGameFolder2 = this.m_game.GetTargetGameFolder();
					List<string> dirtyFiles = this.m_DirtyFiles;
					List<string> list = new List<string>();
					foreach (string text in dirtyFiles)
					{
						string text2 = Path.Combine(ZipUtil.GetPs3SeTempFolder(), "_file_" + Path.GetFileName(text));
						File.WriteAllBytes(text2, this.m_saveFilesData[Path.GetFileName(text)]);
						bool flag7 = list.IndexOf(text2) < 0;
						if (flag7)
						{
							list.Add(text2);
						}
					}
					List<string> containerFiles = this.m_game.GetContainerFiles();
					string text3 = this.m_game.LocalSaveFolder.Substring(0, this.m_game.LocalSaveFolder.Length - 4);
					string hash = Util.GetHash(text3);
					bool cache = Util.GetCache(hash);
					string text4 = this.m_game.ToString(list, "encrypt");
					bool flag8 = cache;
					if (flag8)
					{
						containerFiles.Remove(text3);
						text4 = text4.Replace("<pfs><name>" + Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder) + "</name></pfs>", string.Concat(new string[]
						{
							"<pfs><name>",
							Path.GetFileNameWithoutExtension(this.m_game.LocalSaveFolder),
							"</name><md5>",
							hash,
							"</md5></pfs>"
						}));
					}
					list.AddRange(containerFiles);
					string tempFolder = Util.GetTempFolder();
					string text5 = tempFolder + "ps4_list.xml";
					File.WriteAllText(text5, text4);
					list.Add(text5);
					string text6 = (string)this.cbProfile.SelectedItem;
					AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(list.ToArray(), this.m_game, text6, "encrypt");
					bool flag9 = advancedSaveUploaderForEncrypt.ShowDialog() == DialogResult.OK;
					if (flag9)
					{
						Util.ShowMessage(Resources.msgAdvModeFinish, Resources.msgInfo);
					}
					File.Delete(text5);
					Directory.Delete(ZipUtil.GetPs3SeTempFolder(), true);
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0006B6EC File Offset: 0x000698EC
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
			base.Dispose();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000021C5 File Offset: 0x000003C5
		private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0006B708 File Offset: 0x00069908
		private void hexBox1_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.F3;
			if (flag)
			{
				this.Search(e.Shift, false, this.m_searchForm.GetSearchMode());
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0006B740 File Offset: 0x00069940
		private void txtSearchValue_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.Search(false, true, this.m_searchForm.GetSearchMode());
			}
			bool bTextMode = this.m_bTextMode;
			if (!bTextMode)
			{
				bool flag2 = e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.Control || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right;
				if (!flag2)
				{
					bool flag3 = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 && !e.Shift) || (this.m_searchForm.SearchText.SelectionStart == 1 && e.KeyCode == Keys.X && this.m_searchForm.SearchText.Text[0] == '0') || (this.m_searchForm.SearchText.Text.StartsWith("0x") && e.KeyCode >= Keys.A && e.KeyCode <= Keys.F);
					if (!flag3)
					{
						e.SuppressKeyPress = true;
					}
				}
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0006B890 File Offset: 0x00069A90
		public void Search(bool bBackward, bool bStart, SearchMode mode)
		{
			bool bTextMode = this.m_bTextMode;
			if (bTextMode)
			{
				this.SerachText(bBackward, bStart);
			}
			else
			{
				byte[] bytes = (this.hexBox1.ByteProvider as DynamicByteProvider).Bytes.GetBytes();
				MemoryStream memoryStream = new MemoryStream(bytes);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				if (bStart)
				{
					binaryReader.BaseStream.Position = 0L;
					this.hexBox1.SelectionStart = 0L;
					this.hexBox1.SelectionLength = 0L;
				}
				else
				{
					bool flag = this.hexBox1.SelectionStart >= 0L;
					if (flag)
					{
						binaryReader.BaseStream.Position = this.hexBox1.SelectionStart + this.hexBox1.SelectionLength;
					}
				}
				long num = binaryReader.BaseStream.Position;
				uint num2;
				uint num3;
				byte[] array;
				int searchValues = this.GetSearchValues(mode, out num2, out num3, out array);
				this.lblLengthVal.Text = "0x" + string.Format("{0:X}", searchValues).PadLeft(8, '0');
				bool flag2 = searchValues == 0;
				if (flag2)
				{
					Util.ShowMessage(Resources.errInvalidHex, Resources.msgError);
					this.m_searchForm.SearchText.Focus();
				}
				else
				{
					bool flag3 = searchValues < 0;
					if (flag3)
					{
						Util.ShowMessage(Resources.errIncorrectValue, Resources.msgError);
						this.m_searchForm.SearchText.Focus();
					}
					else
					{
						while (binaryReader.BaseStream.Position >= 0L && binaryReader.BaseStream.Position < binaryReader.BaseStream.Length + (long)(bBackward ? searchValues : (1 - searchValues)))
						{
							bool flag4 = true;
							uint num4 = 0U;
							bool flag5 = mode == SearchMode.Text;
							if (flag5)
							{
								byte[] array2 = new byte[searchValues];
								binaryReader.BaseStream.Read(array2, 0, searchValues);
								for (int i = 0; i < searchValues; i++)
								{
									bool flag6 = array2[i] != array[i];
									if (flag6)
									{
										flag4 = false;
										break;
									}
								}
							}
							else
							{
								flag4 = false;
								num4 = this.ReadValue(binaryReader, searchValues, bBackward);
							}
							bool flag7 = (mode != SearchMode.Text && (num4 == num2 || num4 == num3)) || flag4;
							if (flag7)
							{
								this.hexBox1.Select(binaryReader.BaseStream.Position - (long)searchValues, (long)searchValues);
								this.hexBox1.ScrollByteIntoView(binaryReader.BaseStream.Position);
								this.hexBox1.Focus();
								break;
							}
							if (bBackward)
							{
								num -= 1L;
								bool flag8 = num < 0L;
								if (flag8)
								{
									break;
								}
							}
							else
							{
								num += 1L;
								bool flag9 = num > binaryReader.BaseStream.Length;
								if (flag9)
								{
									break;
								}
							}
							binaryReader.BaseStream.Position = num;
						}
						binaryReader.Close();
						memoryStream.Close();
						memoryStream.Dispose();
					}
				}
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0006BB80 File Offset: 0x00069D80
		public int FindMyText(string text, int start, bool bReverse)
		{
			int num = -1;
			bool flag = text.Length > 0 && start >= 0;
			if (flag)
			{
				RichTextBoxFinds richTextBoxFinds = RichTextBoxFinds.None;
				int num2 = this.txtSaveData.Text.Length;
				if (bReverse)
				{
					richTextBoxFinds |= RichTextBoxFinds.Reverse;
					num2 = start - text.Length;
					start = 0;
					bool flag2 = num2 < 0;
					if (flag2)
					{
						num2 = this.txtSaveData.Text.Length - 1;
					}
				}
				int num3 = this.txtSaveData.Find(text, start, num2, richTextBoxFinds);
				bool flag3 = num3 >= 0;
				if (flag3)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0006BC20 File Offset: 0x00069E20
		private void SerachText(bool bBackward, bool bStart)
		{
			int num = 0;
			bool flag = !bStart;
			if (flag)
			{
				num = this.txtSaveData.SelectionStart + this.txtSaveData.SelectionLength;
			}
			this.lblLengthVal.Text = string.Concat(this.m_searchForm.Text.Length);
			int num2 = this.FindMyText(this.m_searchForm.SearchText.Text, num, bBackward);
			bool flag2 = num2 < 0;
			if (flag2)
			{
				this.txtSaveData.Select(0, 0);
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0006BCA8 File Offset: 0x00069EA8
		private uint ReadValue(BinaryReader reader, int size, bool bBackward)
		{
			if (bBackward)
			{
				bool flag = reader.BaseStream.Position < (long)(2 * size);
				if (flag)
				{
					reader.BaseStream.Position = reader.BaseStream.Length - 1L;
				}
				reader.BaseStream.Position -= (long)(2 * size);
			}
			bool flag2 = size == 1;
			uint num;
			if (flag2)
			{
				num = (uint)reader.ReadByte();
			}
			else
			{
				bool flag3 = size == 2;
				if (flag3)
				{
					num = (uint)reader.ReadUInt16();
				}
				else
				{
					bool flag4 = size == 3;
					if (flag4)
					{
						num = (uint)(((int)reader.ReadUInt16() << 8) | (int)reader.ReadByte());
					}
					else
					{
						num = reader.ReadUInt32();
					}
				}
			}
			return num;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0006BD50 File Offset: 0x00069F50
		private int GetSearchValues(SearchMode mode, out uint val1, out uint val2, out byte[] val3)
		{
			uint num = 0U;
			int num2 = 0;
			val3 = Encoding.ASCII.GetBytes(this.m_searchForm.SearchText.Text);
			try
			{
				bool flag = mode == SearchMode.Hex;
				if (flag)
				{
					num = uint.Parse(this.m_searchForm.SearchText.Text, NumberStyles.HexNumber);
					num2 = this.m_searchForm.SearchText.Text.Length;
					bool flag2 = num2 != 1 && num2 != 2 && num2 != 4 && num2 != 6 && num2 != 8;
					if (flag2)
					{
						val1 = (val2 = 0U);
						return 0;
					}
				}
				else
				{
					bool flag3 = mode == SearchMode.Decimal;
					if (flag3)
					{
						num = uint.Parse(this.m_searchForm.SearchText.Text);
						num2 = num.ToString("X").Length;
					}
					else
					{
						bool flag4 = mode == SearchMode.Float;
						if (flag4)
						{
							num = BitConverter.ToUInt32(BitConverter.GetBytes(float.Parse(this.m_searchForm.SearchText.Text)), 0);
							num2 = 8;
						}
					}
				}
			}
			catch (Exception)
			{
				val1 = 0U;
				val2 = 0U;
				return -1;
			}
			int num3;
			switch (num2)
			{
			case 1:
			case 2:
				num3 = 1;
				break;
			case 3:
			case 4:
				num3 = 2;
				break;
			case 5:
			case 6:
				num3 = 3;
				break;
			case 7:
			case 8:
				num3 = 4;
				break;
			default:
				num3 = 4;
				break;
			}
			val1 = num;
			switch (num3)
			{
			case 2:
				val2 = ((num & 255U) << 8) | ((num & 65280U) >> 8);
				break;
			case 3:
				val2 = ((num & 65280U) << 8) | ((num & 16711680U) >> 8) | (num & 255U);
				break;
			case 4:
				val2 = ((num & 255U) << 24) | ((num & 65280U) << 8) | ((num & 16711680U) >> 8) | ((num & 4278190080U) >> 24);
				break;
			default:
				val2 = num;
				break;
			}
			bool flag5 = mode == SearchMode.Text;
			if (flag5)
			{
				num3 = val3.Length;
			}
			return num3;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0006BF58 File Offset: 0x0006A158
		private void txtSearchValue_TextChanged(object sender, EventArgs e)
		{
			bool bTextMode = this.m_bTextMode;
			if (!bTextMode)
			{
				bool flag = !this.m_searchForm.SearchText.Text.StartsWith("0x");
				if (flag)
				{
					try
					{
						int.Parse(this.m_searchForm.SearchText.Text);
					}
					catch (OverflowException)
					{
						this.m_searchForm.SearchText.Text = this.m_searchForm.SearchText.Text.Substring(0, this.m_searchForm.SearchText.Text.Length - 1);
						this.m_searchForm.SearchText.SelectionStart = this.m_searchForm.SearchText.Text.Length;
					}
					catch (Exception)
					{
						this.m_searchForm.SearchText.Text = "";
					}
				}
				bool flag2 = this.m_searchForm.SearchText.Text.Length > 0;
				if (flag2)
				{
					this.btnFind.Enabled = true;
					this.btnFindPrev.Enabled = true;
				}
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0006C090 File Offset: 0x0006A290
		private void btnFind_Click(object sender, EventArgs e)
		{
			this.Search(false, false, this.m_searchForm.GetSearchMode());
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0006C0A7 File Offset: 0x0006A2A7
		private void button1_Click(object sender, EventArgs e)
		{
			this.Search(true, false, this.m_searchForm.GetSearchMode());
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
		private void AdvancedEdit_KeyDown(object sender, KeyEventArgs e)
		{
			bool bTextMode = this.m_bTextMode;
			if (!bTextMode)
			{
				bool flag = e.KeyCode == Keys.G && e.Modifiers == Keys.Control;
				if (flag)
				{
					this.DoGoTo();
				}
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0006C104 File Offset: 0x0006A304
		private void DoGoTo()
		{
			Goto @goto = new Goto(this.provider.Length);
			bool flag = @goto.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				bool flag2 = @goto.AddressLocation < this.provider.Length;
				if (flag2)
				{
					this.hexBox1.ScrollByteIntoView(@goto.AddressLocation);
					this.hexBox1.Select(@goto.AddressLocation, 1L);
					this.hexBox1.Invalidate();
				}
				else
				{
					Util.ShowMessage(Resources.errInvalidAddress);
				}
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0006C18C File Offset: 0x0006A38C
		private void FillProfiles()
		{
			this.cbProfile.Items.Add("None");
			using (RegistryKey currentUser = Registry.CurrentUser)
			{
				using (RegistryKey registryKey = currentUser.CreateSubKey(Util.GetRegistryBase() + "\\Profiles"))
				{
					string text = (string)registryKey.GetValue(null);
					string[] valueNames = registryKey.GetValueNames();
					foreach (string text2 in valueNames)
					{
						bool flag = !string.IsNullOrEmpty(text2);
						if (flag)
						{
							int num = this.cbProfile.Items.Add(text2);
							bool flag2 = (string)registryKey.GetValue(text2) == text;
							if (flag2)
							{
								this.cbProfile.SelectedIndex = num;
							}
						}
					}
				}
			}
			bool flag3 = this.cbProfile.SelectedIndex < 0;
			if (flag3)
			{
				this.cbProfile.SelectedIndex = 0;
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0006C2AC File Offset: 0x0006A4AC
		private void cbSaveFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = !this.m_bTextMode && this.provider != null && this.provider.Length > 0L;
			if (flag)
			{
				this.provider.ApplyChanges();
			}
			bool flag2 = this.cbSaveFiles.SelectedIndex == this._previousSelectionIndex;
			if (!flag2)
			{
				this._previousSelectionIndex = this.cbSaveFiles.SelectedIndex;
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				bool flag3 = !string.IsNullOrEmpty(this.m_cursaveFile) && this.m_saveFilesData.ContainsKey(this.m_cursaveFile);
				if (flag3)
				{
					file gameFile = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
					bool flag4 = gameFile.TextMode == 0;
					if (flag4)
					{
						this.m_saveFilesData[this.m_cursaveFile] = this.provider.Bytes.ToArray();
					}
					else
					{
						bool flag5 = gameFile.TextMode == 2;
						if (flag5)
						{
							this.m_saveFilesData[this.m_cursaveFile] = Encoding.ASCII.GetBytes(this.txtSaveData.Text);
						}
						else
						{
							bool flag6 = gameFile.TextMode == 3;
							if (flag6)
							{
								this.m_saveFilesData[this.m_cursaveFile] = Encoding.Unicode.GetBytes(this.txtSaveData.Text);
							}
							else
							{
								this.m_saveFilesData[this.m_cursaveFile] = Encoding.UTF8.GetBytes(this.txtSaveData.Text);
							}
						}
					}
				}
				this.lstCheats.Items.Clear();
				this.lstValues.Items.Clear();
				this.m_cursaveFile = this.cbSaveFiles.SelectedItem.ToString();
				List<cheat> cheats = this.m_game.GetCheats(this.m_game.LocalSaveFolder.Substring(0, this.m_game.LocalSaveFolder.Length - 4), this.m_cursaveFile);
				bool flag7 = cheats != null;
				if (flag7)
				{
					foreach (cheat cheat in cheats)
					{
						bool flag8 = cheat.id == "-1";
						if (flag8)
						{
							this.lstCheats.Items.Add(cheat.name);
						}
					}
				}
				bool flag9 = this.lstCheats.Items.Count > 0;
				if (flag9)
				{
					this.lstCheats.SelectedIndex = 0;
				}
				file gameFile2 = this.m_game.GetGameFile(targetGameFolder, this.m_cursaveFile);
				bool flag10 = gameFile2 != null && gameFile2.TextMode > 0;
				if (flag10)
				{
					this.txtSaveData.Visible = true;
					this.hexBox1.Visible = false;
					bool flag11 = gameFile2.TextMode == 1;
					if (flag11)
					{
						this.txtSaveData.Text = Encoding.UTF8.GetString(this.m_saveFilesData[this.m_cursaveFile]);
					}
					else
					{
						bool flag12 = gameFile2.TextMode == 3;
						if (flag12)
						{
							this.txtSaveData.Text = Encoding.Unicode.GetString(this.m_saveFilesData[this.m_cursaveFile]);
						}
						else
						{
							this.txtSaveData.Text = Encoding.ASCII.GetString(this.m_saveFilesData[this.m_cursaveFile]);
						}
					}
					this.TextMode = true;
					this.txtSaveData.TextChanged += this.txtSaveData_TextChanged;
					this.lblAddress.Visible = false;
					this.lblDataHex.Visible = false;
					this.lblDataAscii.Visible = false;
					this.lblOffset.Visible = false;
					this.txtSaveData.HideSelection = false;
				}
				else
				{
					this.TextMode = false;
					this.hexBox1.Visible = true;
					this.lblAddress.Visible = true;
					this.lblDataHex.Visible = true;
					this.lblDataAscii.Visible = true;
					this.lblOffset.Visible = true;
					this.txtSaveData.HideSelection = true;
					this.txtSaveData.Visible = false;
					this.provider = new DynamicByteProvider(this.m_saveFilesData[this.m_cursaveFile]);
					this.provider.Changed += this.provider_Changed;
					bool flag13 = !this.m_undoList.ContainsKey(this.m_cursaveFile);
					if (flag13)
					{
						this.m_undoList.Add(this.m_cursaveFile, new Stack<ActionItem>());
					}
					bool flag14 = !this.m_redoList.ContainsKey(this.m_cursaveFile);
					if (flag14)
					{
						this.m_redoList.Add(this.m_cursaveFile, new Stack<ActionItem>());
					}
					this.toolStripButtonUndo.Enabled = this.m_undoList[this.m_cursaveFile].Count != 0;
					this.toolStripButtonRedo.Enabled = this.m_redoList[this.m_cursaveFile].Count != 0;
					this.provider.LengthChanged += this.provider_LengthChanged;
					this.hexBox1.ByteProvider = this.provider;
					this.hexBox1.BytesPerLine = 16;
					this.hexBox1.UseFixedBytesPerLine = true;
					this.hexBox1.VScrollBarVisible = true;
					this.hexBox1.LineInfoVisible = true;
					this.hexBox1.StringViewVisible = true;
					this.hexBox1.SelectionStartChanged += this.hexBox1_SelectionStartChanged;
					this.hexBox1.SelectionLengthChanged += this.hexBox1_SelectionLengthChanged;
				}
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0006C874 File Offset: 0x0006AA74
		private void hexBox1_SelectionLengthChanged(object sender, EventArgs e)
		{
			this.lblLengthVal.Text = string.Concat(this.hexBox1.SelectionLength);
		}

		// Token: 0x04000A1B RID: 2587
		private DynamicByteProvider provider;

		// Token: 0x04000A1C RID: 2588
		private game m_game;

		// Token: 0x04000A1D RID: 2589
		private bool m_bTextMode;

		// Token: 0x04000A1E RID: 2590
		private Dictionary<string, byte[]> m_saveFilesData;

		// Token: 0x04000A1F RID: 2591
		private List<string> m_DirtyFiles;

		// Token: 0x04000A20 RID: 2592
		private string m_cursaveFile;

		// Token: 0x04000A21 RID: 2593
		private bool _resizeInProgress = false;

		// Token: 0x04000A22 RID: 2594
		private int _previousSelectionIndex = -1;

		// Token: 0x04000A23 RID: 2595
		private Dictionary<string, Stack<ActionItem>> m_undoList = new Dictionary<string, Stack<ActionItem>>();

		// Token: 0x04000A24 RID: 2596
		private Dictionary<string, Stack<ActionItem>> m_redoList = new Dictionary<string, Stack<ActionItem>>();

		// Token: 0x04000A25 RID: 2597
		private Search m_searchForm;
	}
}
