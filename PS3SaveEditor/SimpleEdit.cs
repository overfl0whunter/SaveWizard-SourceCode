using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using CSUST.Data;
using Microsoft.Win32;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E3 RID: 483
	public partial class SimpleEdit : Form
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0009ADEC File Offset: 0x00098FEC
		public game GameItem
		{
			get
			{
				return this.m_game;
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0009AE04 File Offset: 0x00099004
		public SimpleEdit(game gameItem, bool bShowOnly, List<string> files = null)
		{
			this.m_bShowOnly = bShowOnly;
			this.m_game = game.Copy(gameItem);
			this.m_gameFiles = files;
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.btnApply.Enabled = false;
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnApply.ForeColor = Color.Black;
			this.btnClose.BackColor = SystemColors.ButtonFace;
			this.btnClose.ForeColor = Color.Black;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblProfile.Visible = false;
			this.cbProfile.Visible = false;
			this.label1.Visible = false;
			this.dgCheatCodes.Visible = false;
			this.lblGameName.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblGameName.ForeColor = Color.White;
			this.lblGameName.Visible = false;
			this.SetLabels();
			base.CenterToScreen();
			this.FillProfiles();
			this.lblProfile.Text = Resources.lblProfile;
			this.lblGameName.Text = gameItem.name;
			this.dgCheats.CellMouseClick += this.dgCheats_CellMouseClick;
			this.dgCheats.CellMouseDown += this.dgCheats_CellMouseDown;
			this.dgCheats.CellValidated += this.dgCheats_CellValidated;
			this.dgCheats.CellValueChanged += this.dgCheats_CellValueChanged;
			this.dgCheats.CurrentCellDirtyStateChanged += this.dgCheats_CurrentCellDirtyStateChanged;
			this.dgCheats.CellMouseUp += this.dgCheats_CellMouseUp;
			this.dgCheats.MouseDown += this.dgCheats_MouseDown;
			this.btnApply.Click += this.btnApply_Click;
			this.btnApplyCodes.Click += this.btnApplyCodes_Click;
			this.btnClose.Click += this.btnClose_Click;
			base.Resize += this.SimpleEdit_ResizeEnd;
			this.SimpleEdit_ResizeEnd(null, null);
			this.btnApplyCodes.Text = Resources.btnApply;
			this.label1.Text = Resources.lblCheatCodes;
			this.FillCheats(null);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0009B0CB File Offset: 0x000992CB
		protected override void OnResizeBegin(EventArgs e)
		{
			base.SuspendLayout();
			base.OnResizeBegin(e);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0009B0DD File Offset: 0x000992DD
		protected override void OnResizeEnd(EventArgs e)
		{
			base.OnResizeEnd(e);
			base.ResumeLayout();
			this.SimpleEdit_ResizeEnd(null, null);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0009B0F8 File Offset: 0x000992F8
		private void dgCheats_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.ColumnIndex == 0;
			if (flag)
			{
				this.dgCheats.EndEdit();
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0009B124 File Offset: 0x00099324
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0009B190 File Offset: 0x00099390
		private void dgCheats_MouseDown(object sender, MouseEventArgs e)
		{
			Point location = e.Location;
			DataGridView.HitTestInfo hitTestInfo = this.dgCheats.HitTest(location.X, location.Y);
			bool flag = hitTestInfo.Type != DataGridViewHitTestType.Cell;
			if (flag)
			{
				this.dgCheats.ClearSelection();
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0009B1E0 File Offset: 0x000993E0
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

		// Token: 0x06001935 RID: 6453 RVA: 0x0009B300 File Offset: 0x00099500
		private void dgCheats_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			bool isCurrentCellDirty = this.dgCheats.IsCurrentCellDirty;
			if (isCurrentCellDirty)
			{
				this.dgCheats.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0009B330 File Offset: 0x00099530
		private bool ValidateOneGroup(string curChecked)
		{
			foreach (object obj in ((IEnumerable)this.dgCheats.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = "one".Equals(dataGridViewRow.Tag) && dataGridViewRow.Cells[0].Value != null && (bool)dataGridViewRow.Cells[0].Value;
				if (flag)
				{
					bool flag2 = (string)dataGridViewRow.Cells[1].Tag != curChecked;
					if (flag2)
					{
						dataGridViewRow.Cells[0].Value = false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0009B41C File Offset: 0x0009961C
		private void dgCheats_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = false;
			bool flag2 = e.ColumnIndex == 0;
			if (flag2)
			{
				foreach (object obj in ((IEnumerable)this.dgCheats.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					bool flag3 = dataGridViewRow.Cells[0].Value != null && (bool)dataGridViewRow.Cells[0].Value && (string)dataGridViewRow.Cells[0].Tag != "GameFile" && (string)dataGridViewRow.Cells[0].Tag != "CheatGroup";
					if (flag3)
					{
						flag = true;
						break;
					}
				}
			}
			this.btnApply.Enabled = flag;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0009B520 File Offset: 0x00099720
		private void dgCheats_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex == 0;
			if (flag)
			{
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				bool flag2 = targetGameFolder == null;
				if (flag2)
				{
					Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
				}
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0009B564 File Offset: 0x00099764
		private void SimpleEdit_ResizeEnd(object sender, EventArgs e)
		{
			this.btnApply.Left = base.Width / 2 - this.btnApply.Width - 2;
			this.btnClose.Left = base.Width / 2 + 2;
			this.lblProfile.Left = this.btnApply.Left - this.cbProfile.Width - this.lblProfile.Width - 30;
			this.cbProfile.Left = this.lblProfile.Left + this.lblProfile.Width + 5;
			this.dgCheats.Columns[1].Width = (this.dgCheats.Width - 2 - this.dgCheats.Columns[0].Width) / 2;
			this.dgCheats.Columns[2].Width = (this.dgCheats.Width - 2 - this.dgCheats.Columns[0].Width) / 2;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0009B67C File Offset: 0x0009987C
		protected override void OnClosing(CancelEventArgs e)
		{
			bool bCheatsModified = this.m_bCheatsModified;
			if (bCheatsModified)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
			base.OnClosing(e);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0009B6B0 File Offset: 0x000998B0
		private void SetLabels()
		{
			this.Text = Resources.titleSimpleEdit;
			this.btnApply.Text = Resources.btnApplyPatch;
			this.btnClose.Text = Resources.btnClose;
			this.dgCheats.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgCheats.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			this.dgCheats.RowTemplate.Height = Util.ScaleSize(24);
			this.dgCheats.Columns[0].HeaderText = "";
			this.dgCheats.Columns[1].HeaderText = Resources.colDesc;
			this.dgCheats.Columns[2].HeaderText = Resources.colComment;
			this.addCodeToolStripMenuItem.Text = Resources.mnuAddCheatCode;
			this.editCodeToolStripMenuItem.Text = Resources.mnuEditCheatCode;
			this.deleteCodeToolStripMenuItem.Text = Resources.mnuDeleteCheatCode;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0009B7B8 File Offset: 0x000999B8
		private void FillCheats(string highlight)
		{
			this.dgCheats.Rows.Clear();
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			bool flag = targetGameFolder != null;
			if (flag)
			{
				this.ColSelect.Visible = true;
				List<cheat> allCheats = this.m_game.GetAllCheats();
				bool flag2 = allCheats.Count == 0;
				if (flag2)
				{
					int num = this.dgCheats.Rows.Add(new DataGridViewRow());
					this.dgCheats.Rows[num].Height = Util.ScaleSize(24);
					DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.ForeColor = Color.Gray;
					this.dgCheats.Rows[num].Cells[0].Tag = "NoCheats";
					dataGridViewCellStyle.Font = new Font(this.dgCheats.Font, FontStyle.Italic);
					this.dgCheats.Rows[num].Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
					this.dgCheats.Rows[num].Cells[1].Value = Resources.lblNoCheats;
				}
				bool flag3 = targetGameFolder.preprocess == 1 && this.m_gameFiles != null && this.m_gameFiles.Count > 0;
				if (flag3)
				{
					container container = container.Copy(targetGameFolder);
					List<file> files = container.files._files;
					targetGameFolder.files._files = new List<file>();
					this.m_gameFiles.Sort();
					foreach (string text in this.m_gameFiles)
					{
						file file = file.GetGameFile(container, this.m_game.LocalSaveFolder, text);
						bool flag4 = file == null;
						if (!flag4)
						{
							file = file.Copy(file);
							file.original_filename = file.filename;
							file.filename = text;
							targetGameFolder.files._files.Add(file);
						}
					}
					targetGameFolder.files._files.Sort((file f1, file f2) => f1.VisibleFileName.CompareTo(f2.VisibleFileName));
				}
				MainForm.FillLocalCheats(ref this.m_game);
				foreach (file file2 in targetGameFolder.files._files)
				{
					bool flag5 = targetGameFolder.files._files.Count > 1;
					if (flag5)
					{
						int num2 = this.dgCheats.Rows.Add(new DataGridViewRow());
						this.dgCheats.Rows[num2].Height = Util.ScaleSize(24);
						this.dgCheats.Rows[num2].Cells[1].Value = file2.VisibleFileName;
						this.dgCheats.Rows[num2].Cells[2].Value = "";
						this.dgCheats.Rows[num2].Cells[1].Tag = file2.id;
						this.dgCheats.Rows[num2].Cells[0].Tag = "GameFile";
						this.dgCheats.Rows[num2].Tag = file2.filename;
					}
					foreach (cheat cheat in file2.cheats._cheats)
					{
						int num2 = this.dgCheats.Rows.Add(new DataGridViewRow());
						this.dgCheats.Rows[num2].Height = Util.ScaleSize(24);
						this.dgCheats.Rows[num2].Cells[1].Value = cheat.name;
						this.dgCheats.Rows[num2].Cells[2].Value = cheat.note;
						this.dgCheats.Rows[num2].Cells[1].Tag = cheat.id;
						this.dgCheats.Rows[num2].Cells[0].Tag = file2.filename;
						bool flag6 = cheat.id == "-1";
						if (flag6)
						{
							this.dgCheats.Rows[num2].Tag = "UserCheat";
							this.dgCheats.Rows[num2].Cells[1].Tag = cheat.code;
						}
					}
					foreach (group group in file2.groups)
					{
						this.FillGroupCheats(file2, group, file2.filename, 0);
					}
				}
			}
			else
			{
				bool bShowOnly = this.m_bShowOnly;
				if (bShowOnly)
				{
					this.ColSelect.Visible = false;
					this.btnApply.Enabled = false;
					foreach (container container2 in this.m_game.containers._containers)
					{
						foreach (file file3 in container2.files._files)
						{
							foreach (cheat cheat2 in file3.cheats._cheats)
							{
								int num3 = this.dgCheats.Rows.Add();
								this.dgCheats.Rows[num3].Height = Util.ScaleSize(24);
								this.dgCheats.Rows[num3].Cells[1].Value = cheat2.name;
								this.dgCheats.Rows[num3].Cells[2].Value = cheat2.note;
							}
							foreach (group group2 in file3.groups)
							{
								this.FillGroupCheats(file3, group2, file3.filename, 0);
							}
						}
					}
				}
			}
			this.RefreshValue();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0009BFD8 File Offset: 0x0009A1D8
		private void FillFileCheats(container target, file file, string saveFile)
		{
			for (int i = 0; i < file.Cheats.Count; i++)
			{
				cheat cheat = file.Cheats[i];
				int num = this.dgCheats.Rows.Add(new DataGridViewRow());
				this.dgCheats.Rows[num].Height = Util.ScaleSize(24);
				this.dgCheats.Rows[num].Cells[1].Value = cheat.name;
				this.dgCheats.Rows[num].Cells[2].Value = cheat.note;
				bool flag = cheat.id == "-1";
				if (flag)
				{
					DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.ForeColor = Color.Blue;
					this.dgCheats.Rows[num].Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
					this.dgCheats.Rows[num].Cells[0].Tag = "UserCheat";
					this.dgCheats.Rows[num].Cells[1].Tag = Path.GetFileName(saveFile);
					this.dgCheats.Rows[num].Tag = file.GetParent(target);
				}
				else
				{
					this.dgCheats.Rows[num].Cells[0].Tag = saveFile;
					this.dgCheats.Rows[num].Cells[1].Tag = cheat.id;
				}
			}
			bool flag2 = file.groups.Count > 0;
			if (flag2)
			{
				foreach (group group in file.groups)
				{
					this.FillGroupCheats(file, group, saveFile, 0);
				}
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0009C214 File Offset: 0x0009A414
		private void FillGroupCheats(file file, group g, string saveFile, int level)
		{
			int num = this.dgCheats.Rows.Add(new DataGridViewRow());
			this.dgCheats.Rows[num].Height = Util.ScaleSize(24);
			this.dgCheats.Rows[num].Cells[0].Tag = "CheatGroup";
			bool flag = level > 0;
			if (flag)
			{
				this.dgCheats.Rows[num].Cells[1].Value = new string(' ', level * 4) + g.name;
			}
			else
			{
				this.dgCheats.Rows[num].Cells[1].Value = g.name;
			}
			this.dgCheats.Rows[num].Cells[2].Value = g.note;
			this.dgCheats.Rows[num].Cells[2].Value = "";
			foreach (cheat cheat in g.cheats)
			{
				num = this.dgCheats.Rows.Add(new DataGridViewRow());
				this.dgCheats.Rows[num].Height = Util.ScaleSize(24);
				this.dgCheats.Rows[num].Cells[1].Value = new string(' ', (level + 1) * 4) + cheat.name;
				this.dgCheats.Rows[num].Cells[0].Tag = saveFile;
				this.dgCheats.Rows[num].Cells[1].Tag = cheat.id;
				this.dgCheats.Rows[num].Tag = g.options;
			}
			bool flag2 = g._group != null;
			if (flag2)
			{
				foreach (group group in g._group)
				{
					this.FillGroupCheats(file, group, saveFile, level + 1);
				}
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0009C4B8 File Offset: 0x0009A6B8
		private bool ContainsGameFile(List<file> allGameFile, file @internal)
		{
			foreach (file file in allGameFile)
			{
				bool flag = file.id == @internal.id;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0009C524 File Offset: 0x0009A724
		private void dgCheats_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.RefreshValue();
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = e.ColumnIndex == 2;
				if (flag2)
				{
					string text = this.dgCheats.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
					bool flag3 = text != null && text.IndexOf("http://") >= 0;
					if (flag3)
					{
						int num = text.IndexOf("http://");
						int num2 = text.IndexOf(' ', num);
						bool flag4 = num2 > 0;
						if (flag4)
						{
							Process.Start(text.Substring(text.IndexOf("http"), num2 - num));
						}
						else
						{
							Process.Start(text.Substring(text.IndexOf("http")));
						}
					}
				}
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0009C604 File Offset: 0x0009A804
		private void RefreshValue()
		{
			this.dgCheatCodes.Rows.Clear();
			int num = -1;
			bool flag = this.dgCheats.SelectedCells.Count == 1;
			if (flag)
			{
				num = this.dgCheats.SelectedCells[0].RowIndex;
			}
			bool flag2 = this.dgCheats.SelectedRows.Count == 1;
			if (flag2)
			{
				num = this.dgCheats.SelectedRows[0].Index;
			}
			bool flag3 = num < 0 && this.dgCheats.Rows.Count > 0;
			if (flag3)
			{
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0009C6A4 File Offset: 0x0009A8A4
		private void btnApply_Click(object sender, EventArgs e)
		{
			bool flag = false;
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			bool flag2 = targetGameFolder == null;
			if (flag2)
			{
				Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
			}
			else
			{
				List<string> list = new List<string>();
				for (int i = 0; i < this.dgCheats.Rows.Count; i++)
				{
					bool flag3 = this.dgCheats.Rows[i].Cells[0].Value != null && (bool)this.dgCheats.Rows[i].Cells[0].Value;
					if (flag3)
					{
						List<file> list2 = new List<file>(targetGameFolder.files._files);
						foreach (file file in list2)
						{
							foreach (cheat cheat in file.GetAllCheats())
							{
								bool flag4 = (string)this.dgCheats.Rows[i].Cells[1].Tag == cheat.id || ((string)this.dgCheats.Rows[i].Tag == "UserCheat" && cheat.id == "-1" && cheat.name == (string)this.dgCheats.Rows[i].Cells[1].Value);
								if (flag4)
								{
									bool flag5 = this.m_gameFiles != null && (string)this.dgCheats.Rows[i].Cells[0].Tag != file.filename;
									if (!flag5)
									{
										cheat.Selected = true;
										bool flag6 = list.IndexOf(file.filename) < 0;
										if (flag6)
										{
											list.Add(file.filename);
										}
									}
								}
							}
						}
						flag = true;
					}
				}
				bool flag7 = !flag;
				if (flag7)
				{
					Util.ShowMessage(Resources.msgSelectCheat, Resources.msgError);
				}
				else
				{
					bool flag8 = Util.ShowMessage(Resources.warnOverwrite, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
					if (!flag8)
					{
						string text = (string)this.cbProfile.SelectedItem;
						SimpleSaveUploader simpleSaveUploader = new SimpleSaveUploader(this.m_game, text, list);
						bool flag9 = simpleSaveUploader.ShowDialog() == DialogResult.OK;
						if (flag9)
						{
							Util.ShowMessage(Resources.msgQuickModeFinish, Resources.msgInfo);
						}
						base.Close();
					}
				}
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0006396F File Offset: 0x00061B6F
		private void button1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0009C9D8 File Offset: 0x0009ABD8
		private void dgCheats_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			this.RefreshValue();
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0009C9E4 File Offset: 0x0009ABE4
		private void btnApplyCodes_Click(object sender, EventArgs e)
		{
			bool flag = this.dgCheatCodes.Tag == null;
			if (flag)
			{
				Util.ShowMessage(Resources.msgNoCheats, Resources.msgError);
			}
			else
			{
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				int num = (int)this.dgCheatCodes.Tag;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0009CA34 File Offset: 0x0009AC34
		private void addCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			foreach (file file in targetGameFolder.files._files)
			{
				foreach (cheat cheat in file.Cheats)
				{
					list.Add(cheat.name);
				}
			}
			AddCode addCode = new AddCode(list);
			bool flag = addCode.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				cheat cheat2 = new cheat("-1", addCode.Description, addCode.Comment);
				cheat2.code = addCode.Code;
				bool flag2 = this.m_game.GetTargetGameFolder() == null;
				if (flag2)
				{
					Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
					return;
				}
				string selectedSaveFile = this.GetSelectedSaveFile();
				container targetGameFolder2 = this.m_game.GetTargetGameFolder();
				file gameFile = this.m_game.GetGameFile(targetGameFolder2, selectedSaveFile);
				gameFile.Cheats.Add(cheat2);
				this.SaveUserCheats();
				this.m_bCheatsModified = true;
			}
			this.FillCheats(addCode.Description);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
		private string GetSelectedSaveFile()
		{
			int index = this.dgCheats.SelectedRows[0].Index;
			bool flag = this.dgCheats.Rows[index].Cells[0].Tag != null && (string)this.dgCheats.Rows[index].Cells[0].Tag == "GameFile";
			string text;
			if (flag)
			{
				text = this.dgCheats.Rows[index].Cells[1].Value.ToString();
			}
			else
			{
				for (int i = index; i >= 0; i--)
				{
					bool flag2 = (string)this.dgCheats.Rows[i].Cells[0].Tag == "GameFile";
					if (flag2)
					{
						return this.dgCheats.Rows[i].Tag.ToString();
					}
				}
				text = null;
			}
			return text;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0009CCC4 File Offset: 0x0009AEC4
		private void SaveUserCheats()
		{
			bool flag = this.m_game.GetTargetGameFolder() == null;
			if (flag)
			{
				Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
			}
			else
			{
				string text = "<usercheats></usercheats>";
				string text2 = Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm.USER_CHEATS_FILE;
				bool flag2 = File.Exists(text2);
				if (flag2)
				{
					text = File.ReadAllText(text2);
				}
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.LoadXml(text);
				}
				catch (Exception)
				{
					try
					{
						text = text.Replace("&", "&amp;");
						xmlDocument.LoadXml(text);
					}
					catch (Exception)
					{
					}
				}
				bool flag3 = false;
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				for (int i = 0; i < xmlDocument["usercheats"].ChildNodes.Count; i++)
				{
					bool flag4 = this.m_game.id + targetGameFolder.key == xmlDocument["usercheats"].ChildNodes[i].Attributes["id"].Value;
					if (flag4)
					{
						flag3 = true;
					}
				}
				bool flag5 = !flag3;
				if (flag5)
				{
					XmlElement xmlElement = xmlDocument.CreateElement("game");
					xmlElement.SetAttribute("id", this.m_game.id + targetGameFolder.key);
					xmlDocument["usercheats"].AppendChild(xmlElement);
				}
				for (int j = 0; j < xmlDocument["usercheats"].ChildNodes.Count; j++)
				{
					bool flag6 = this.m_game.id + targetGameFolder.key == xmlDocument["usercheats"].ChildNodes[j].Attributes["id"].Value;
					if (flag6)
					{
						XmlElement xmlElement2 = xmlDocument["usercheats"].ChildNodes[j] as XmlElement;
						xmlElement2.InnerXml = "";
						List<file> list = new List<file>(targetGameFolder.files._files);
						foreach (file file in targetGameFolder.files._files)
						{
							bool flag7 = file.internals != null && file.internals.files.Count > 0;
							if (flag7)
							{
								list.AddRange(file.internals.files);
							}
						}
						foreach (file file2 in list)
						{
							XmlElement xmlElement3 = xmlDocument.CreateElement("file");
							xmlElement3.SetAttribute("name", file2.filename);
							xmlElement2.AppendChild(xmlElement3);
							foreach (cheat cheat in file2.Cheats)
							{
								bool flag8 = cheat.id == "-1";
								if (flag8)
								{
									XmlElement xmlElement4 = xmlDocument.CreateElement("cheat");
									xmlElement4.SetAttribute("desc", cheat.name);
									xmlElement4.SetAttribute("comment", cheat.note);
									xmlElement3.AppendChild(xmlElement4);
									XmlElement xmlElement5 = xmlDocument.CreateElement("code");
									xmlElement5.InnerText = cheat.code;
									xmlElement4.AppendChild(xmlElement5);
								}
							}
						}
					}
				}
				xmlDocument.Save(text2);
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0009D124 File Offset: 0x0009B324
		private void editCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = this.dgCheats.SelectedRows[0].Index;
			file gameFile = this.m_game.GetGameFile(this.m_game.GetTargetGameFolder(), this.dgCheats.Rows[index].Cells[0].Tag.ToString());
			cheat cheat = null;
			foreach (cheat cheat2 in gameFile.Cheats)
			{
				bool flag = cheat2.name == this.dgCheats.Rows[index].Cells[1].Value.ToString();
				if (flag)
				{
					cheat = cheat2;
					break;
				}
			}
			bool flag2 = cheat == null;
			if (!flag2)
			{
				List<string> list = new List<string>();
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				foreach (file file in targetGameFolder.files._files)
				{
					foreach (cheat cheat3 in file.Cheats)
					{
						bool flag3 = cheat3.name != this.dgCheats.Rows[index].Cells[1].Value.ToString();
						if (flag3)
						{
							list.Add(cheat3.name);
						}
					}
				}
				AddCode addCode = new AddCode(cheat, list);
				bool flag4 = addCode.ShowDialog() == DialogResult.OK;
				if (flag4)
				{
					cheat cheat4 = new cheat("-1", addCode.Description, addCode.Comment);
					cheat4.code = addCode.Code;
					for (int i = 0; i < gameFile.Cheats.Count; i++)
					{
						bool flag5 = gameFile.Cheats[i].name == this.dgCheats.Rows[index].Cells[1].Value.ToString();
						if (flag5)
						{
							gameFile.Cheats[i] = cheat4;
						}
					}
					this.SaveUserCheats();
					this.m_bCheatsModified = true;
				}
				this.FillCheats(addCode.Description);
			}
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.m_game.GetTargetGameFolder() == null;
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				bool flag2 = this.dgCheats.SelectedRows.Count == 1;
				if (flag2)
				{
					container targetGameFolder = this.m_game.GetTargetGameFolder();
					bool flag3 = targetGameFolder.quickmode > 0;
					if (flag3)
					{
						e.Cancel = true;
					}
					else
					{
						int index = this.dgCheats.SelectedRows[0].Index;
						bool flag4 = this.dgCheats.Rows[index].Cells[0].Tag != null && (this.dgCheats.Rows[index].Cells[0].Tag.ToString() == "NoCheats" || this.dgCheats.Rows[index].Cells[0].Tag.ToString() == "GameFile");
						if (flag4)
						{
							e.Cancel = false;
						}
						else
						{
							bool flag5 = targetGameFolder.files._files.Count == 1;
							if (flag5)
							{
							}
						}
						this.editCodeToolStripMenuItem.Enabled = (string)this.dgCheats.Rows[index].Tag == "UserCheat";
						this.deleteCodeToolStripMenuItem.Enabled = (string)this.dgCheats.Rows[index].Tag == "UserCheat";
					}
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0009D5AC File Offset: 0x0009B7AC
		private void dgCheats_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = e.Button == MouseButtons.Right;
				if (flag2)
				{
					this.dgCheats.ClearSelection();
					this.dgCheats.Rows[e.RowIndex].Selected = true;
				}
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0009D608 File Offset: 0x0009B808
		private void deleteCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = this.dgCheats.SelectedRows[0].Index;
			bool flag = index >= 0;
			if (flag)
			{
				bool flag2 = Util.ShowMessage(Resources.msgConfirmDelete, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
				bool flag3 = flag2;
				if (flag3)
				{
					container targetGameFolder = this.m_game.GetTargetGameFolder();
					file gameFile = this.m_game.GetGameFile(targetGameFolder, this.dgCheats.Rows[index].Cells[0].Tag.ToString());
					for (int i = 0; i < gameFile.Cheats.Count; i++)
					{
						bool flag4 = gameFile.Cheats[i].name == this.dgCheats.Rows[index].Cells[1].Value.ToString();
						if (flag4)
						{
							gameFile.Cheats.RemoveAt(i);
							break;
						}
					}
					this.SaveUserCheats();
					this.FillCheats(null);
					this.m_bCheatsModified = true;
				}
			}
		}

		// Token: 0x04000C8B RID: 3211
		private game m_game;

		// Token: 0x04000C8C RID: 3212
		private bool m_bCheatsModified = false;

		// Token: 0x04000C8D RID: 3213
		private bool m_bShowOnly = false;

		// Token: 0x04000C8E RID: 3214
		private List<string> m_gameFiles;
	}
}
