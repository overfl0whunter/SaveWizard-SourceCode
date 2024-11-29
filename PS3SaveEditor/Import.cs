using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CSUST.Data;
using Ionic.Zip;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001CC RID: 460
	public partial class Import : Form
	{
		// Token: 0x06001753 RID: 5971 RVA: 0x00073E1C File Offset: 0x0007201C
		public Import(List<game> games, Dictionary<ZipEntry, ZipEntry> entries, ZipFile zipFile, Dictionary<string, object> accounts, string drive)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleImport;
			this.m_games = games;
			this.m_psnIDs = accounts;
			this.dgImport.Columns[1].HeaderText = Resources.colGameName;
			this.dgImport.Columns[2].HeaderText = Resources.colSysVer;
			this.dgImport.Columns[3].HeaderText = Resources.colProfile;
			this.btnImport.Text = Resources.btnImport;
			this.btnCancel.Text = Resources.btnCancel;
			this.m_accounts = accounts;
			this.m_drive = drive;
			this.m_zipFile = zipFile;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.PrepareMap(entries, games);
			this.FillSaves(null, false);
			this.dgImport.SelectionChanged += this.dgImport_SelectionChanged;
			this.btnImport.Click += this.btnImport_Click;
			this.btnCancel.Click += this.btnCancel_Click;
			this.dgImport.CellDoubleClick += this.dgImport_CellDoubleClick;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00073F94 File Offset: 0x00072194
		private void dgImport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgImport.SelectedCells.Count == 0 || this.dgImport.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					bool flag3 = this.dgImport.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending;
					this.FillSaves(this.dgImport.Rows[this.dgImport.SelectedCells[0].RowIndex].Cells[1].Value as string, flag3);
				}
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00074050 File Offset: 0x00072250
		private void FillSaves(string expandGame, bool bSortedAsc)
		{
			bool flag = this.m_expandedGame == expandGame;
			if (flag)
			{
				expandGame = null;
				this.m_expandedGame = null;
			}
			this.dgImport.Rows.Clear();
			List<string> list = new List<string>();
			using (Dictionary<string, List<game>>.KeyCollection.Enumerator enumerator = this.m_map.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string id = enumerator.Current;
					game game = this.m_games.Find((game a) => a.id == id);
					bool flag2 = game == null;
					if (flag2)
					{
						game = this.m_map[id][0];
					}
					foreach (alias alias in game.GetAllAliases(bSortedAsc, false))
					{
						string text = alias.name;
						text = text + " (" + alias.id + ")";
						string id2 = alias.id;
						bool flag3 = !this.m_map.ContainsKey(alias.id);
						if (!flag3)
						{
							List<game> list2 = this.m_map[id2];
							bool flag4 = list.IndexOf(id2) >= 0;
							if (!flag4)
							{
								list.Add(id2);
								int num = this.dgImport.Rows.Add();
								this.dgImport.Rows[num].Cells[1].Value = alias.name;
								bool flag5 = list2.Count == 0;
								if (flag5)
								{
									game game2 = list2[0];
									this.dgImport.Rows[num].Tag = game2;
									container targetGameFolder = game2.GetTargetGameFolder();
									bool flag6 = targetGameFolder != null;
									if (flag6)
									{
										this.dgImport.Rows[num].Cells[2].Value = targetGameFolder.GetCheatsCount();
									}
									else
									{
										this.dgImport.Rows[num].Cells[2].Value = "N/A";
									}
									this.dgImport.Rows[num].Cells[0].ToolTipText = "";
									this.dgImport.Rows[num].Cells[0].Tag = id2;
									string[] array = game2.PFSZipEntry.FileName.Split(new char[] { '/' });
									bool flag7 = array.Length >= 2;
									if (flag7)
									{
										this.dgImport.Rows[num].Cells[2].Value = array[array.Length - 2];
									}
								}
								else
								{
									DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
									this.dgImport.Rows[num].Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
									{
										Font = new Font("Arial", Util.ScaleSize(7f))
									});
									this.dgImport.Rows[num].Cells[0].Value = "►";
									string text2 = this.dgImport.Rows[num].Cells[1].Value as string;
									this.dgImport.Rows[num].Cells[1].Value = (string.IsNullOrEmpty(text2) ? alias.id : (text2 + " (" + alias.id + ")"));
									dataGridViewCellStyle.BackColor = Color.White;
									this.dgImport.Rows[num].Cells[0].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgImport.Rows[num].Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgImport.Rows[num].Cells[2].Style.ApplyStyle(dataGridViewCellStyle);
									this.dgImport.Rows[num].Tag = list2;
									bool flag8 = text == expandGame || alias.id == expandGame;
									if (flag8)
									{
										this.dgImport.Rows[num].Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
										{
											Font = new Font("Arial", Util.ScaleSize(7f))
										});
										this.dgImport.Rows[num].Cells[0].Value = "▼";
										this.dgImport.Rows[num].Cells[0].ToolTipText = "";
										this.dgImport.Rows[num].Cells[1].Value = (string.IsNullOrEmpty(text2) ? alias.id : (text2 + " (" + alias.id + ")"));
										this.dgImport.Rows[num].Cells[0].Tag = id2;
										foreach (game game3 in list2)
										{
											container container = game3.containers._containers[0];
											bool flag9 = container == null;
											if (!flag9)
											{
												int num2 = this.dgImport.Rows.Add();
												Match match = Regex.Match(Path.GetFileNameWithoutExtension(game3.LocalSaveFolder), container.pfs);
												bool flag10 = container.name != null && match.Groups != null && match.Groups.Count > 1;
												if (flag10)
												{
													this.dgImport.Rows[num2].Cells[1].Value = "    " + container.name.Replace("${1}", match.Groups[1].Value);
												}
												else
												{
													this.dgImport.Rows[num2].Cells[1].Value = "    " + (container.name ?? Path.GetFileNameWithoutExtension(game3.LocalSaveFolder));
												}
												this.dgImport.Rows[num2].Cells[0].Tag = id2;
												game3.name = alias.name;
												this.dgImport.Rows[num2].Tag = game3;
												this.dgImport.Rows[num2].Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game3.LocalSaveFolder);
												this.dgImport.Rows[num2].Cells[3].Value = this.GetPSNID(game3);
												MemoryStream memoryStream = new MemoryStream();
												game3.BinZipEntry.Extract(memoryStream);
												string sysVer = MainForm3.GetSysVer(memoryStream.GetBuffer());
												memoryStream.Close();
												memoryStream.Dispose();
												this.dgImport.Rows[num2].Cells[2].Value = sysVer;
												bool flag11 = sysVer == "?";
												if (flag11)
												{
													this.dgImport.Rows[num2].Cells[2].ToolTipText = Resources.msgUnknownSysVer;
												}
												else
												{
													bool flag12 = sysVer == "All";
													if (flag12)
													{
														this.dgImport.Rows[num2].Cells[2].ToolTipText = Resources.tooltipV1;
													}
													else
													{
														bool flag13 = sysVer == "4.50+";
														if (flag13)
														{
															this.dgImport.Rows[num2].Cells[2].ToolTipText = Resources.tooltipV2;
														}
														else
														{
															bool flag14 = sysVer == "4.70";
															if (flag14)
															{
																this.dgImport.Rows[num2].Cells[2].ToolTipText = Resources.tooltipV3;
															}
														}
													}
												}
											}
										}
										this.m_expandedGame = expandGame;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000749B8 File Offset: 0x00072BB8
		private object GetPSNID(game item)
		{
			bool flag = !this.IsValidPSNID(item.PSN_ID);
			object obj;
			if (flag)
			{
				obj = Resources.lblUnregistered + " " + item.PSN_ID;
			}
			else
			{
				Dictionary<string, object> dictionary = this.m_psnIDs[item.PSN_ID] as Dictionary<string, object>;
				obj = dictionary["friendly_name"];
			}
			return obj;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00074A18 File Offset: 0x00072C18
		public bool IsValidPSNID(string psnId)
		{
			return this.m_psnIDs != null && this.m_psnIDs.ContainsKey(psnId);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00074A4C File Offset: 0x00072C4C
		private void PrepareMap(Dictionary<ZipEntry, ZipEntry> entries, List<game> games)
		{
			this.m_map = new Dictionary<string, List<game>>();
			foreach (ZipEntry zipEntry in entries.Keys)
			{
				string[] array = zipEntry.FileName.Split(new char[] { '/' });
				bool flag = array.Length > 1 && array[array.Length - 2].StartsWith("CUSA");
				if (flag)
				{
					string[] array2 = zipEntry.FileName.Split(new char[] { '/' });
					string text;
					int onlineSaveIndex = MainForm3.GetOnlineSaveIndex(games, zipEntry.FileName, out text);
					bool flag2 = onlineSaveIndex < 0;
					if (flag2)
					{
						text = array2[array2.Length - 2];
					}
					bool flag3 = !this.m_map.ContainsKey(text);
					if (flag3)
					{
						this.m_map.Add(text, new List<game>());
					}
					string text2 = array2[array.Length - 2];
					ZipEntry zipEntry2 = zipEntry;
					ZipEntry zipEntry3 = entries[zipEntry];
					string directoryName = Path.GetDirectoryName(zipEntry2.FileName);
					game game = new game
					{
						id = text2,
						name = "",
						containers = new containers
						{
							_containers = new List<container>
							{
								new container
								{
									pfs = Path.GetFileName(zipEntry.FileName)
								}
							}
						},
						PFSZipEntry = zipEntry2,
						BinZipEntry = zipEntry3,
						ZipFile = this.m_zipFile,
						LocalSaveFolder = Path.Combine(directoryName, Path.GetFileName(zipEntry3.FileName))
					};
					bool flag4 = onlineSaveIndex >= 0;
					if (flag4)
					{
						string name = games[onlineSaveIndex].name;
						game = game.Copy(this.m_games[onlineSaveIndex]);
						game.id = text;
						game.LocalSaveFolder = Path.Combine(directoryName, Path.GetFileName(zipEntry3.FileName));
						game.PFSZipEntry = zipEntry2;
						game.BinZipEntry = zipEntry3;
						game.ZipFile = this.m_zipFile;
					}
					this.m_map[text].Add(game);
				}
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00074CA0 File Offset: 0x00072EA0
		private void btnImport_Click(object sender, EventArgs e)
		{
			bool flag = Util.GetRegistryValue("NoResignMessage") == null;
			if (flag)
			{
				ResignInfo resignInfo = new ResignInfo();
				resignInfo.ShowDialog(this);
			}
			ChooseProfile chooseProfile = new ChooseProfile(this.m_accounts, "");
			bool flag2 = chooseProfile.ShowDialog() == DialogResult.OK;
			if (flag2)
			{
				game game = this.dgImport.SelectedRows[0].Tag as game;
				ZipEntry pfszipEntry = game.PFSZipEntry;
				ZipEntry binZipEntry = game.BinZipEntry;
				string id = game.id;
				string text = Path.Combine(new string[] { this.m_drive, "PS4", "SAVEDATA", chooseProfile.SelectedAccount, id });
				game game2 = new game
				{
					id = id,
					name = "",
					containers = new containers
					{
						_containers = new List<container>
						{
							new container
							{
								pfs = (this.dgImport.SelectedRows[0].Cells[1].Value as string)
							}
						}
					},
					PFSZipEntry = pfszipEntry,
					BinZipEntry = binZipEntry,
					ZipFile = this.m_zipFile,
					LocalSaveFolder = Path.Combine(text, Path.GetFileName(pfszipEntry.FileName))
				};
				bool flag3 = File.Exists(game2.LocalSaveFolder);
				if (flag3)
				{
					bool flag4 = Util.ShowMessage(Resources.msgConfirmResignOverwrite, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.No;
					if (flag4)
					{
						return;
					}
				}
				bool flag5 = string.IsNullOrEmpty(this.m_drive) || !Directory.Exists(Path.GetPathRoot(game2.LocalSaveFolder));
				if (flag5)
				{
					Util.ShowMessage(Resources.errImportNoUSB);
				}
				else
				{
					ResignFilesUplaoder resignFilesUplaoder = new ResignFilesUplaoder(game2, text, chooseProfile.SelectedAccount, new List<string>());
					bool flag6 = resignFilesUplaoder.ShowDialog() == DialogResult.OK;
					if (flag6)
					{
						ResignMessage resignMessage = new ResignMessage(false);
						resignMessage.ShowDialog(this);
					}
					this.dgImport.ClearSelection();
					base.DialogResult = DialogResult.OK;
				}
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00074ED8 File Offset: 0x000730D8
		private void dgImport_SelectionChanged(object sender, EventArgs e)
		{
			this.btnImport.Enabled = this.dgImport.SelectedRows.Count == 1 && !string.IsNullOrEmpty(this.dgImport.SelectedRows[0].Cells[1].Value as string) && (this.dgImport.SelectedRows[0].Cells[1].Value as string).StartsWith("    ");
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00074F68 File Offset: 0x00073168
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

		// Token: 0x04000ACA RID: 2762
		private Dictionary<string, object> m_accounts;

		// Token: 0x04000ACB RID: 2763
		private ZipFile m_zipFile;

		// Token: 0x04000ACC RID: 2764
		private string m_drive;

		// Token: 0x04000ACD RID: 2765
		private List<game> m_games;

		// Token: 0x04000ACE RID: 2766
		private Dictionary<string, object> m_psnIDs = null;

		// Token: 0x04000ACF RID: 2767
		private string m_expandedGame;

		// Token: 0x04000AD0 RID: 2768
		private Dictionary<string, List<game>> m_map;
	}
}
