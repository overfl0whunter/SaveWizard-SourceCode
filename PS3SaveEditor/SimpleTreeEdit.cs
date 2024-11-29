using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using BrightIdeasSoftware;
using Microsoft.Win32;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E5 RID: 485
	public partial class SimpleTreeEdit : Form
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0009EE70 File Offset: 0x0009D070
		public game GameItem
		{
			get
			{
				return this.m_game;
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0009EE88 File Offset: 0x0009D088
		public SimpleTreeEdit(game gameItem, bool bShowOnly, List<string> files = null)
		{
			this.m_bShowOnly = bShowOnly;
			this.m_gameFiles = files;
			this.m_game = game.Copy(gameItem);
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.btnApply.Enabled = false;
			this.SetLabels();
			base.CenterToScreen();
			this.lblProfile.Visible = false;
			this.cbProfile.Visible = false;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblProfile.Visible = false;
			this.cbProfile.Visible = false;
			this.lblGameName.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblGameName.ForeColor = Color.White;
			this.lblGameName.Visible = false;
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnApply.ForeColor = Color.Black;
			this.btnClose.BackColor = SystemColors.ButtonFace;
			this.btnClose.ForeColor = Color.Black;
			this.lblProfile.Text = Resources.lblProfile;
			this.lblGameName.Text = gameItem.name;
			this.btnApply.Click += this.btnApply_Click;
			this.btnClose.Click += this.btnClose_Click;
			this.treeListView = new TreeListView();
			this.panel2.Controls.Add(this.treeListView);
			this.treeListView.Dock = DockStyle.Fill;
			this.treeListView.ItemChecked += this.treeListView_ItemChecked;
			this.treeListView.OwnerDraw = true;
			this.treeListView.DrawSubItem += this.treeListView_DrawSubItem;
			this.treeListView.RowHeight = 20;
			this.treeListView.BorderStyle = BorderStyle.FixedSingle;
			this.treeListView.GridLines = false;
			base.Resize += this.SimpleTreeEdit_Resize;
			this.contextMenuStrip1.Opening += this.contextMenuStrip1_Opening;
			this.addCodeToolStripMenuItem.Click += this.addCodeToolStripMenuItem_Click;
			this.m_bShowOnly = bShowOnly;
			this.FillTree(bShowOnly);
			this.SimpleTreeEdit_Resize(null, null);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000021C5 File Offset: 0x000003C5
		private void treeListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0009F11C File Offset: 0x0009D31C
		protected override void OnPaintBackground(PaintEventArgs e)
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

		// Token: 0x06001961 RID: 6497 RVA: 0x0009F1B4 File Offset: 0x0009D3B4
		private void SimpleTreeEdit_Resize(object sender, EventArgs e)
		{
			this.btnApply.Left = this.panel1.Width / 2 - this.btnApply.Width - 1;
			this.btnClose.Left = this.panel1.Width / 2 + 1;
			bool flag = this.treeListView.Items.Count > this.treeListView.Height / this.treeListView.RowHeight;
			if (flag)
			{
				this.treeListView.Columns[0].Width = ((this.treeListView.Columns[0] as OLVColumn).MinimumWidth = (this.treeListView.Width - SystemInformation.VerticalScrollBarWidth - 2) / 2);
				this.treeListView.Columns[1].Width = ((this.treeListView.Columns[1] as OLVColumn).MinimumWidth = (this.treeListView.Width - SystemInformation.VerticalScrollBarWidth - 2) / 2);
			}
			else
			{
				this.treeListView.Columns[0].Width = ((this.treeListView.Columns[0] as OLVColumn).MinimumWidth = (this.treeListView.Width - 2) / 2);
				this.treeListView.Columns[1].Width = ((this.treeListView.Columns[1] as OLVColumn).MinimumWidth = (this.treeListView.Width - 2) / 2);
			}
			base.Invalidate(true);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0009F368 File Offset: 0x0009D568
		private void treeListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			bool flag = e.Item.Text == Resources.lblNoCheats || !(this.treeListView.GetModelObject(e.Item.Index) is cheat);
			if (flag)
			{
				e.Item.Checked = false;
			}
			else
			{
				this.btnApply.Enabled = false;
				foreach (object obj in this.treeListView.CheckedObjects)
				{
					bool flag2 = obj is cheat;
					if (flag2)
					{
						this.btnApply.Enabled = true;
						break;
					}
				}
				bool flag3 = !e.Item.Checked;
				if (!flag3)
				{
					bool flag4 = this.treeListView.CheckedObjects.Count > 0;
					if (flag4)
					{
						group group = this.treeListView.GetParent(this.treeListView.GetModelObject(e.Item.Index)) as group;
						bool flag5 = group != null && group.type == "1";
						if (flag5)
						{
							foreach (object obj2 in this.treeListView.GetChildren(group))
							{
								bool flag6 = obj2 != this.treeListView.GetModelObject(e.Item.Index);
								if (flag6)
								{
									this.treeListView.ModelToItem(obj2).Checked = false;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0009F548 File Offset: 0x0009D748
		private void FillTree(bool showOnly)
		{
			this.treeListView.CanExpandGetter = delegate(object x)
			{
				file file6 = x as file;
				bool flag9 = file6 != null;
				bool flag10;
				if (flag9)
				{
					flag10 = true;
				}
				else
				{
					bool flag11 = x is group;
					if (flag11)
					{
						group group = x as group;
						flag10 = group.cheats.Count > 0 || group._group != null;
					}
					else
					{
						flag10 = false;
					}
				}
				return flag10;
			};
			this.treeListView.CheckBoxes = !showOnly;
			this.BackColor = Color.White;
			this.treeListView.UseCustomSelectionColors = true;
			this.treeListView.HighlightBackgroundColor = Color.FromArgb(0, 175, 255);
			this.treeListView.RowFormatter = delegate(OLVListItem olvItem)
			{
				bool selected = olvItem.Selected;
				if (selected)
				{
					olvItem.BackColor = Color.FromArgb(0, 175, 255);
				}
				else
				{
					olvItem.UseItemStyleForSubItems = true;
					bool flag12 = olvItem.RowObject is file;
					if (flag12)
					{
						olvItem.BackColor = Color.White;
					}
					bool flag13 = olvItem.RowObject is string;
					if (flag13)
					{
						olvItem.Font = new Font(olvItem.Font, FontStyle.Italic);
					}
				}
			};
			this.treeListView.PrimarySortOrder = SortOrder.None;
			this.treeListView.SecondarySortOrder = SortOrder.None;
			this.treeListView.UseCellFormatEvents = true;
			this.treeListView.FormatCell += this.treeListView_FormatCell;
			this.treeListView.ChildrenGetter = delegate(object x)
			{
				ArrayList arrayList = new ArrayList();
				file file7 = x as file;
				bool flag14 = file7 != null;
				if (flag14)
				{
					bool flag15 = file7.TotalCheats == 0;
					if (flag15)
					{
						arrayList.Add(Resources.lblNoCheats);
					}
					bool flag16 = file7.Cheats.Count > 0;
					if (flag16)
					{
						foreach (cheat cheat in file7.Cheats)
						{
							bool flag17 = cheat.id != "-1";
							if (flag17)
							{
								arrayList.Add(cheat);
							}
						}
					}
					bool flag18 = file7.groups.Count > 0;
					if (flag18)
					{
						arrayList.AddRange(file7.groups);
					}
					bool flag19 = file7.ucfilename == file7.filename || file7.ucfilename == null || Util.IsMatch(file7.ucfilename, file7.filename);
					if (flag19)
					{
						foreach (cheat cheat2 in file7.Cheats)
						{
							bool flag20 = cheat2.id == "-1";
							if (flag20)
							{
								arrayList.Add(cheat2);
							}
						}
					}
				}
				else
				{
					bool flag21 = x is group;
					if (flag21)
					{
						group group2 = x as group;
						arrayList.AddRange(group2.cheats);
						bool flag22 = group2._group != null;
						if (flag22)
						{
							arrayList.AddRange(group2._group);
						}
					}
				}
				return arrayList;
			};
			OLVColumn olvcolumn = new OLVColumn(Resources.colName, "Name");
			olvcolumn.AspectGetter = delegate(object x)
			{
				bool flag23 = x is file;
				object obj;
				if (flag23)
				{
					obj = (x as file).VisibleFileName;
				}
				else
				{
					bool flag24 = x is group;
					if (flag24)
					{
						obj = (x as group).name;
					}
					else
					{
						bool flag25 = x is cheat;
						if (flag25)
						{
							obj = (x as cheat).name;
						}
						else
						{
							bool flag26 = x is string;
							if (flag26)
							{
								obj = x;
							}
							else
							{
								obj = "!Missing!" + x;
							}
						}
					}
				}
				return obj;
			};
			this.treeListView.FullRowSelect = true;
			olvcolumn.Width = 300;
			OLVColumn olvcolumn2 = new OLVColumn(Resources.colComment, "Description");
			olvcolumn2.AspectGetter = delegate(object x)
			{
				bool flag27 = x is cheat;
				object obj2;
				if (flag27)
				{
					obj2 = (x as cheat).note;
				}
				else
				{
					bool flag28 = x is group;
					if (flag28)
					{
						obj2 = (x as group).note;
					}
					else
					{
						obj2 = "";
					}
				}
				return obj2;
			};
			olvcolumn2.Width = 300;
			this.treeListView.Columns.Add(olvcolumn);
			this.treeListView.Columns.Add(olvcolumn2);
			if (showOnly)
			{
				List<file> list = new List<file>();
				for (int i = 0; i < this.m_game.containers._containers.Count; i++)
				{
					bool flag = this.m_game.containers._containers[i].GetAllCheats().Count > 0;
					if (flag)
					{
						list.AddRange(this.m_game.containers._containers[i].files._files);
					}
				}
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					this.treeListView.Roots = list;
				}
				else
				{
					this.treeListView.Roots = this.m_game.containers._containers[0].files._files;
				}
				for (int j = 0; j < this.m_game.containers._containers.Count; j++)
				{
					foreach (file file in this.m_game.containers._containers[j].files._files)
					{
						this.treeListView.Expand(file);
					}
				}
			}
			else
			{
				List<file> list2 = new List<file>();
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				bool flag3 = targetGameFolder.preprocess == 1 && this.m_gameFiles != null && this.m_gameFiles.Count > 0;
				if (flag3)
				{
					container container = container.Copy(targetGameFolder);
					List<file> files = container.files._files;
					targetGameFolder.files._files = new List<file>();
					this.m_gameFiles.Sort();
					foreach (string text in this.m_gameFiles)
					{
						file file2 = file.GetGameFile(container, this.m_game.LocalSaveFolder, text);
						bool flag4 = file2 == null;
						if (!flag4)
						{
							file2 = file.Copy(file2);
							file2.original_filename = file2.filename;
							file2.filename = text;
							targetGameFolder.files._files.Add(file2);
						}
					}
					targetGameFolder.files._files.Sort((file f1, file f2) => f1.VisibleFileName.CompareTo(f2.VisibleFileName));
				}
				MainForm.FillLocalCheats(ref this.m_game);
				foreach (file file3 in targetGameFolder.files._files)
				{
					bool flag5 = file3.IsHidden && (file3.internals == null || file3.internals.files == null || file3.internals.files.Count == 0);
					if (!flag5)
					{
						bool flag6 = file3.internals != null;
						if (flag6)
						{
							foreach (file file4 in file3.internals.files)
							{
								list2.Add(file4);
							}
						}
						bool flag7 = !file3.IsHidden;
						if (flag7)
						{
							list2.Add(file3);
						}
					}
				}
				this.treeListView.Roots = list2;
				list2.Reverse();
				foreach (file file5 in list2)
				{
					this.treeListView.Expand(file5);
				}
				bool flag8 = this.treeListView.Items.Count > 0;
				if (flag8)
				{
					OLVListItem item = this.treeListView.GetItem(0);
					item.EnsureVisible();
				}
			}
			this.treeListView.BeforeSorting += this.treeListView_BeforeSorting;
			this.treeListView.CellClick += this.treeListView_CellClick;
			this.treeListView.Expanded += new EventHandler<TreeBranchExpandedEventArgs>(this.treeListView_ExpandedCollapsed);
			this.treeListView.Collapsed += new EventHandler<TreeBranchCollapsedEventArgs>(this.treeListView_ExpandedCollapsed);
			this.treeListView.MultiSelect = false;
			this.treeListView.ShowItemToolTips = true;
			this.treeListView.AutoSizeColumns();
			this.treeListView.LowLevelScroll(0, 0);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0009FC14 File Offset: 0x0009DE14
		private void treeListView_ExpandedCollapsed(object sender, EventArgs e)
		{
			bool flag = this.treeListView.Items.Count > this.treeListView.Height / this.treeListView.RowHeight;
			if (flag)
			{
				this.treeListView.Columns[0].Width = ((this.treeListView.Columns[0] as OLVColumn).MinimumWidth = (this.treeListView.Width - SystemInformation.VerticalScrollBarWidth - 2) / 2);
				this.treeListView.Columns[1].Width = ((this.treeListView.Columns[1] as OLVColumn).MinimumWidth = (this.treeListView.Width - SystemInformation.VerticalScrollBarWidth - 2) / 2);
			}
			else
			{
				this.treeListView.Columns[0].Width = ((this.treeListView.Columns[0] as OLVColumn).MinimumWidth = (this.treeListView.Width - 2) / 2);
				this.treeListView.Columns[1].Width = ((this.treeListView.Columns[1] as OLVColumn).MinimumWidth = (this.treeListView.Width - 2) / 2);
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0009FD7C File Offset: 0x0009DF7C
		private void treeListView_CellClick(object sender, CellClickEventArgs e)
		{
			bool flag = e.Item != null && e.Item.Text == "Add Cheat...";
			if (flag)
			{
				this.addCodeToolStripMenuItem_Click(null, null);
			}
			else
			{
				bool flag2 = e.Model is group;
				if (flag2)
				{
					bool flag3 = this.treeListView.IsExpanded(e.Model);
					if (flag3)
					{
						this.treeListView.Collapse(e.Model);
					}
					else
					{
						this.treeListView.Expand(e.Model);
					}
				}
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0009FE0C File Offset: 0x0009E00C
		private void treeListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
		{
			e.Canceled = true;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0009FE18 File Offset: 0x0009E018
		private void treeListView_FormatCell(object sender, FormatCellEventArgs e)
		{
			bool flag = e.Model is cheat;
			if (flag)
			{
				cheat cheat = e.Model as cheat;
				bool flag2 = cheat.id == "-1";
				if (flag2)
				{
					e.Item.ForeColor = Color.Blue;
				}
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0009FE70 File Offset: 0x0009E070
		private void dgCheats_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.ColumnIndex == 0;
			if (flag)
			{
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0009FE90 File Offset: 0x0009E090
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 274 && ((int)m.WParam == 61728 || (int)m.WParam == 61488);
			if (flag)
			{
				base.Invalidate(true);
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgCheats_MouseDown(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0009FEEC File Offset: 0x0009E0EC
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

		// Token: 0x0600196C RID: 6508 RVA: 0x000A000C File Offset: 0x0009E20C
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

		// Token: 0x0600196D RID: 6509 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000A0040 File Offset: 0x0009E240
		private void SetLabels()
		{
			this.Text = Resources.titleSimpleEdit;
			this.btnApply.Text = Resources.btnApplyPatch;
			this.btnClose.Text = Resources.btnClose;
			this.addCodeToolStripMenuItem.Text = Resources.mnuAddCheatCode;
			this.editCodeToolStripMenuItem.Text = Resources.mnuEditCheatCode;
			this.deleteCodeToolStripMenuItem.Text = Resources.mnuDeleteCheatCode;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000A00B0 File Offset: 0x0009E2B0
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

		// Token: 0x06001970 RID: 6512 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgCheats_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000021C5 File Offset: 0x000003C5
		private void RefreshValue()
		{
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000A011C File Offset: 0x0009E31C
		private void AddDependencies(file f, List<string> saveFiles)
		{
			bool flag = f.dependency != null;
			if (flag)
			{
				saveFiles.Add(f.GetDependencyFile(this.m_game.GetTargetGameFolder(), this.m_game.LocalSaveFolder));
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000A015C File Offset: 0x0009E35C
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
				foreach (object obj in this.treeListView.CheckedObjects)
				{
					flag = true;
					cheat cheat = obj as cheat;
					bool flag3 = cheat == null;
					if (!flag3)
					{
						cheat.Selected = true;
						object obj2 = obj;
						file file;
						bool flag4;
						do
						{
							obj2 = this.treeListView.GetParent(obj2);
							file = obj2 as file;
							flag4 = file != null;
						}
						while (!flag4);
						bool flag5 = list.IndexOf(file.filename) < 0;
						if (flag5)
						{
							list.Add(file.filename);
						}
					}
				}
				bool flag6 = !flag;
				if (flag6)
				{
					Util.ShowMessage(Resources.msgSelectCheat, Resources.msgError);
				}
				else
				{
					bool flag7 = Util.ShowMessage(Resources.warnOverwrite, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
					if (!flag7)
					{
						string text = (string)this.cbProfile.SelectedItem;
						SimpleSaveUploader simpleSaveUploader = new SimpleSaveUploader(this.m_game, text, list);
						bool flag8 = simpleSaveUploader.ShowDialog() == DialogResult.OK;
						if (flag8)
						{
							Util.ShowMessage(Resources.msgQuickModeFinish, Resources.msgInfo);
						}
						base.Close();
					}
				}
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000A02F8 File Offset: 0x0009E4F8
		private void btnApply_Click1(object sender, EventArgs e)
		{
			container targetGameFolder = this.m_game.GetTargetGameFolder();
			bool flag = targetGameFolder == null;
			if (flag)
			{
				Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
			}
			else
			{
				bool flag2 = false;
				List<string> list = new List<string>();
				list.Add(Path.Combine(this.m_game.LocalSaveFolder, "PARAM.PFD"));
				list.Add(Path.Combine(this.m_game.LocalSaveFolder, "PARAM.SFO"));
				foreach (object obj in this.treeListView.CheckedObjects)
				{
					flag2 = true;
					cheat cheat = obj as cheat;
					this.m_game.GetCheat(cheat.id, cheat.name).Selected = true;
					object obj2 = cheat;
					bool flag3;
					do
					{
						obj2 = this.treeListView.GetParent(obj2);
						flag3 = obj2 is file;
					}
					while (!flag3);
					file file = obj2 as file;
					bool flag4 = file.GetParent(targetGameFolder) != null;
					string text;
					if (flag4)
					{
						text = file.GetParent(targetGameFolder).GetSaveFile(this.m_game.LocalSaveFolder);
						bool flag5 = text == null;
						if (!flag5)
						{
							text = Path.Combine(this.m_game.LocalSaveFolder, text);
							bool flag6 = list.IndexOf(text) < 0;
							if (flag6)
							{
								list.Add(text);
								this.AddDependencies(file.GetParent(targetGameFolder), list);
							}
							text = Path.Combine(this.m_game.LocalSaveFolder, file.filename);
							goto IL_01C5;
						}
					}
					else
					{
						text = file.GetSaveFile(this.m_game.LocalSaveFolder);
						bool flag7 = text == null;
						if (!flag7)
						{
							text = Path.Combine(this.m_game.LocalSaveFolder, text);
							goto IL_01C5;
						}
					}
					continue;
					IL_01C5:
					bool flag8 = list.IndexOf(text) < 0;
					if (flag8)
					{
						list.Add(text);
						bool flag9 = file.internals != null;
						if (flag9)
						{
							foreach (file file2 in file.internals.files)
							{
								text = file2.GetSaveFile(this.m_game.LocalSaveFolder);
								bool flag10 = list.IndexOf(text) < 0;
								if (flag10)
								{
									list.Add(text);
								}
							}
						}
					}
				}
				bool flag11 = !flag2;
				if (flag11)
				{
					Util.ShowMessage(Resources.msgSelectCheat, Resources.msgError);
				}
				else
				{
					bool flag12 = Util.ShowMessage(Resources.warnOverwrite, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
					if (!flag12)
					{
						string text2 = (string)this.cbProfile.SelectedItem;
						SimpleSaveUploader simpleSaveUploader = new SimpleSaveUploader(this.m_game, text2, list);
						bool flag13 = simpleSaveUploader.ShowDialog() == DialogResult.OK;
						if (flag13)
						{
							Util.ShowMessage(Resources.msgQuickModeFinish, Resources.msgInfo);
						}
						base.Close();
					}
				}
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0006396F File Offset: 0x00061B6F
		private void button1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000A0650 File Offset: 0x0009E850
		private void dgCheats_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			this.RefreshValue();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000A065C File Offset: 0x0009E85C
		private void addCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.treeListView.SelectedObjects.Count == 0;
			if (!flag)
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
				bool flag2 = addCode.ShowDialog() == DialogResult.OK;
				if (flag2)
				{
					this.m_bCheatsModified = true;
					cheat cheat2 = new cheat("-1", addCode.Description, addCode.Comment);
					cheat2.code = addCode.Code;
					bool flag3 = this.m_game.GetTargetGameFolder() == null;
					if (flag3)
					{
						Util.ShowMessage(Resources.errNoSavedata, Resources.msgError);
					}
					else
					{
						file file2 = this.treeListView.SelectedObjects[0] as file;
						bool flag4 = file2 == null;
						if (flag4)
						{
							int num = this.treeListView.SelectedIndex;
							for (;;)
							{
								bool flag5 = this.treeListView.GetModelObject(num) is file;
								if (flag5)
								{
									break;
								}
								num--;
								bool flag6 = num < 0;
								if (flag6)
								{
									goto Block_7;
								}
							}
							file2 = this.treeListView.GetModelObject(num) as file;
							Block_7:;
						}
						bool flag7 = file2 != null;
						if (flag7)
						{
							file2.Cheats.Add(cheat2);
							this.treeListView.RefreshObject(file2);
							this.SaveUserCheats();
						}
					}
				}
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000A0858 File Offset: 0x0009EA58
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

		// Token: 0x06001979 RID: 6521 RVA: 0x000A0CB8 File Offset: 0x0009EEB8
		private void SaveUserCheats2()
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
				xmlDocument.LoadXml(text);
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
							bool flag8 = file2.GetParent(targetGameFolder) != null;
							if (flag8)
							{
								xmlElement3.SetAttribute("name", file2.filename);
							}
							else
							{
								xmlElement3.SetAttribute("name", Path.GetFileName(file2.GetSaveFile(this.m_game.LocalSaveFolder)));
							}
							xmlElement2.AppendChild(xmlElement3);
							foreach (cheat cheat in file2.Cheats)
							{
								bool flag9 = cheat.id == "-1";
								if (flag9)
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

		// Token: 0x0600197A RID: 6522 RVA: 0x000A10F4 File Offset: 0x0009F2F4
		private void editCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cheat cheat = this.treeListView.SelectedObjects[0] as cheat;
			bool flag = cheat != null;
			if (flag)
			{
				List<string> list = new List<string>();
				container targetGameFolder = this.m_game.GetTargetGameFolder();
				foreach (file file in targetGameFolder.files._files)
				{
					foreach (cheat cheat2 in file.Cheats)
					{
						bool flag2 = cheat2.name != cheat.name;
						if (flag2)
						{
							list.Add(cheat2.name);
						}
					}
				}
				AddCode addCode = new AddCode(cheat, list);
				bool flag3 = addCode.ShowDialog() == DialogResult.OK;
				if (flag3)
				{
					cheat.code = addCode.Code;
					cheat.name = addCode.Description;
					cheat.note = addCode.Comment;
					this.treeListView.RefreshObject(cheat);
					container targetGameFolder2 = this.m_game.GetTargetGameFolder();
					foreach (file file2 in targetGameFolder2.files._files)
					{
						foreach (cheat cheat3 in file2.Cheats)
						{
							bool flag4 = cheat3.name == cheat.name && cheat3.id == "-1";
							if (flag4)
							{
								cheat3.code = cheat.code;
								break;
							}
						}
					}
					this.SaveUserCheats();
					this.m_bCheatsModified = true;
				}
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000A132C File Offset: 0x0009F52C
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.m_game.GetTargetGameFolder() == null;
			if (flag)
			{
				this.editCodeToolStripMenuItem.Enabled = false;
				this.deleteCodeToolStripMenuItem.Enabled = false;
				this.addCodeToolStripMenuItem.Enabled = false;
			}
			else
			{
				this.addCodeToolStripMenuItem.Enabled = false;
				this.editCodeToolStripMenuItem.Enabled = false;
				this.deleteCodeToolStripMenuItem.Enabled = false;
				bool flag2 = this.treeListView.SelectedObjects.Count == 1;
				if (flag2)
				{
					bool flag3 = this.m_game.GetTargetGameFolder().quickmode > 0;
					if (flag3)
					{
						e.Cancel = true;
					}
					else
					{
						int selectedIndex = this.treeListView.SelectedIndex;
						object obj = this.treeListView.SelectedObjects[0];
						bool flag4 = obj is cheat && (obj as cheat).id == "-1";
						if (flag4)
						{
							cheat cheat = obj as cheat;
							this.editCodeToolStripMenuItem.Enabled = cheat.id == "-1";
							this.deleteCodeToolStripMenuItem.Enabled = cheat.id == "-1";
						}
						bool flag5 = obj is file || obj is cheat;
						if (flag5)
						{
							this.addCodeToolStripMenuItem.Enabled = true;
						}
					}
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgCheats_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000A14C4 File Offset: 0x0009F6C4
		private void deleteCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(Resources.msgConfirmDelete, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
			bool flag2 = flag;
			if (flag2)
			{
				cheat cheat = this.treeListView.SelectedObjects[0] as cheat;
				bool flag3 = cheat != null;
				if (flag3)
				{
					object parent = this.treeListView.GetParent(cheat);
					bool flag4 = parent is file;
					if (flag4)
					{
						file file = parent as file;
						file.Cheats.Remove(cheat);
						this.treeListView.RefreshObject(file);
						this.SaveUserCheats();
						this.m_bCheatsModified = true;
					}
				}
			}
		}

		// Token: 0x04000CA8 RID: 3240
		private game m_game;

		// Token: 0x04000CA9 RID: 3241
		private bool m_bCheatsModified = false;

		// Token: 0x04000CAA RID: 3242
		private bool m_bShowOnly = false;

		// Token: 0x04000CAB RID: 3243
		private TreeListView treeListView;

		// Token: 0x04000CAC RID: 3244
		private List<string> m_gameFiles;
	}
}
