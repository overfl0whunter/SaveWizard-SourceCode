using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CSUST.Data;
using Microsoft.Win32;
using PS3SaveEditor.Resources;
using PS3SaveEditor.Utilities;
using Rss;

namespace PS3SaveEditor
{
	// Token: 0x020001CE RID: 462
	public partial class MainForm : Form
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x00075950 File Offset: 0x00073B50
		public MainForm()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ja");
			this.m_games = new List<game>();
			this.RegionMap = new Dictionary<int, string>();
			this.lblLanguage.Text = Resources.lblLanguage;
			this.label1.Text = Resources.lblPackageSerial;
			this.groupBox3.Visible = false;
			this.chkShowAll.CheckedChanged += this.chkShowAll_CheckedChanged;
			this.chkShowAll.EnabledChanged += this.chkShowAll_EnabledChanged;
			this.picTraffic.Visible = false;
			base.ResizeBegin += delegate(object s, EventArgs e)
			{
				base.SuspendLayout();
			};
			base.ResizeEnd += delegate(object s, EventArgs e)
			{
				base.ResumeLayout(true);
				this.chkShowAll_CheckedChanged(null, null);
				base.Invalidate(true);
			};
			base.SizeChanged += delegate(object s, EventArgs e)
			{
				bool flag5 = base.WindowState == FormWindowState.Maximized;
				if (flag5)
				{
					this.chkShowAll_CheckedChanged(null, null);
					base.Invalidate(true);
				}
			};
			this.txtBackupLocation.ReadOnly = true;
			this.dgServerGames.Columns[0].ReadOnly = true;
			this.MinimumSize = base.Size;
			this.dgServerGames.CellClick += this.dgServerGames_CellClick;
			this.dgServerGames.SelectionChanged += this.dgServerGames_SelectionChanged;
			this.dgServerGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.btnGamesInServer.Visible = false;
			this.btnRss.BackColor = SystemColors.ButtonFace;
			this.btnOpenFolder.BackColor = SystemColors.ButtonFace;
			this.btnBrowse.BackColor = SystemColors.ButtonFace;
			this.btnDeactivate.BackColor = SystemColors.ButtonFace;
			this.btnManageProfiles.BackColor = SystemColors.ButtonFace;
			this.btnApply.BackColor = SystemColors.ButtonFace;
			this.btnRss.ForeColor = Color.Black;
			this.btnOpenFolder.ForeColor = Color.Black;
			this.btnBrowse.ForeColor = Color.Black;
			this.btnDeactivate.ForeColor = Color.Black;
			this.btnManageProfiles.ForeColor = Color.Black;
			this.btnApply.ForeColor = Color.Black;
			this.btnApply.ForeColor = Color.Black;
			this.pnlBackup.BackColor = (this.pnlHome.BackColor = (this.pnlHome.BackColor = (this.pnlNoSaves.BackColor = Color.FromArgb(127, 204, 204, 204))));
			this.gbBackupLocation.BackColor = (this.gbManageProfile.BackColor = (this.groupBox1.BackColor = (this.groupBox2.BackColor = Color.Transparent)));
			this.chkShowAll.BackColor = Color.FromArgb(0, 204, 204, 204);
			this.chkShowAll.ForeColor = Color.White;
			this.panel2.Visible = false;
			this.registerPSNIDToolStripMenuItem.Visible = false;
			this.resignToolStripMenuItem.Visible = true;
			this.toolStripSeparator1.Visible = false;
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				this.groupBox2.Size = Util.ScaleSize(new Size(234, 65));
				bool flag2 = base.WindowState == FormWindowState.Minimized;
				if (flag2)
				{
					base.WindowState = FormWindowState.Normal;
				}
				base.Activate();
			}
			else
			{
				Util.SetForegroundWindow(base.Handle);
			}
			base.CenterToScreen();
			this.SetLabels();
			bool flag3 = !Util.IsUnixOrMacOSX();
			if (flag3)
			{
				Util.SetForegroundWindow(base.Handle);
			}
			this.cbDrives.SelectedIndexChanged += this.cbDrives_SelectedIndexChanged;
			this.dgServerGames.CellMouseDown += this.dgServerGames_CellMouseDown;
			this.dgServerGames.CellDoubleClick += this.dgServerGames_CellDoubleClick;
			this.dgServerGames.ColumnHeaderMouseClick += this.dgServerGames_ColumnHeaderMouseClick;
			this.dgServerGames.ShowCellToolTips = true;
			this.panel2.BackgroundImage = null;
			string[] directories = Directory.GetDirectories(Path.GetDirectoryName(Application.ExecutablePath));
			string registryValue = Util.GetRegistryValue("Language");
			this.cbLanguage.DisplayMember = "NativeName";
			this.cbLanguage.ValueMember = "Name";
			List<CultureInfo> list = new List<CultureInfo>();
			list.Add(new CultureInfo("en"));
			this.cbLanguage.SelectedValueChanged += this.cbLanguage_SelectedIndexChanged;
			foreach (string text in directories)
			{
				try
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					CultureInfo cultureInfo = new CultureInfo(fileNameWithoutExtension);
					list.Add(cultureInfo);
				}
				catch
				{
				}
			}
			this.cbLanguage.DataSource = list;
			bool flag4 = registryValue != null;
			if (flag4)
			{
				this.cbLanguage.SelectedValue = registryValue;
			}
			else
			{
				this.cbLanguage.SelectedIndex = 0;
			}
			this.cbDrives.DrawMode = DrawMode.OwnerDrawFixed;
			this.cbDrives.DrawItem += this.cbDrives_DrawItem;
			this.drivesHelper = new DrivesHelper(this.cbDrives, this.m_games, this.chkShowAll, this.pnlNoSaves);
			this.drivesHelper.FillDrives();
			base.Load += this.MainForm_Load;
			this.btnHome.ChangeUICues += this.btnHome_ChangeUICues;
			this.dgServerGames.BackgroundColor = Color.White;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00075F70 File Offset: 0x00074170
		private void picContact_Click(object sender, EventArgs e)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				UseShellExecute = true,
				Verb = "open",
				FileName = "http://www.cybergadget.co.jp/contact/inquiry.html"
			};
			Process.Start(processStartInfo);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000021C5 File Offset: 0x000003C5
		private void picContact_MouseLeave(object sender, EventArgs e)
		{
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000021C5 File Offset: 0x000003C5
		private void picContact_MouseHover(object sender, EventArgs e)
		{
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00075FAC File Offset: 0x000741AC
		private void btnHome_ChangeUICues(object sender, UICuesEventArgs e)
		{
			bool changeFocus = e.ChangeFocus;
			if (changeFocus)
			{
				this.btnHome.Focus();
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00075FD0 File Offset: 0x000741D0
		private void chkShowAll_EnabledChanged(object sender, EventArgs e)
		{
			bool enabled = this.chkShowAll.Enabled;
			if (enabled)
			{
				this.chkShowAll.ForeColor = Color.White;
				this.chkShowAll.FlatStyle = FlatStyle.Standard;
			}
			else
			{
				this.chkShowAll.ForeColor = Color.FromArgb(190, 190, 190);
				this.chkShowAll.FlatStyle = FlatStyle.Flat;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00076040 File Offset: 0x00074240
		private void cbDrives_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = this.cbDrives.SelectedIndex < 0;
			if (!flag)
			{
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 175, 255)), e.Bounds);
					e.Graphics.DrawString(this.cbDrives.Items[e.Index].ToString(), e.Font, Brushes.White, new Point(e.Bounds.X, e.Bounds.Y));
				}
				else
				{
					e.Graphics.FillRectangle(Brushes.White, e.Bounds);
					e.Graphics.DrawString(this.cbDrives.Items[e.Index].ToString(), e.Font, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
				}
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0007616C File Offset: 0x0007436C
		private void dgServerGames_SelectionChanged(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedRows != null && this.dgServerGames.SelectedRows.Count > 0;
			if (flag)
			{
				int index = this.dgServerGames.SelectedRows[0].Index;
				bool flag2 = Util.IsUnixOrMacOSX();
				if (flag2)
				{
					this.dgServerGames.SelectionChanged -= this.dgServerGames_SelectionChanged;
				}
				bool @checked = this.chkShowAll.Checked;
				if (@checked)
				{
					this.dgServerGames.CurrentCell = this.dgServerGames.SelectedRows[0].Cells[1];
				}
				else
				{
					this.dgServerGames.CurrentCell = this.dgServerGames.SelectedRows[0].Cells[0];
				}
				bool flag3 = Util.IsUnixOrMacOSX();
				if (flag3)
				{
					this.dgServerGames.SelectionChanged += this.dgServerGames_SelectionChanged;
				}
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00076262 File Offset: 0x00074462
		private void MainForm_Resize(object sender, EventArgs e)
		{
			this.chkShowAll_CheckedChanged(null, null);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00076270 File Offset: 0x00074470
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

		// Token: 0x0600176E RID: 5998 RVA: 0x00076308 File Offset: 0x00074508
		private void dgServerGames_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			bool flag = e.Column.Index == 1;
			if (flag)
			{
				SortOrder sortGlyphDirection = this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection;
				bool flag2 = sortGlyphDirection == SortOrder.Descending;
				if (flag2)
				{
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					bool flag3 = e.SortResult == 0;
					if (flag3)
					{
						bool flag4 = this.dgServerGames.Rows[e.RowIndex1].Cells[1].Value.ToString().StartsWith("    ");
						if (flag4)
						{
							e.SortResult = -1;
						}
						bool flag5 = this.dgServerGames.Rows[e.RowIndex2].Cells[1].Value.ToString().StartsWith("    ");
						if (flag5)
						{
							e.SortResult = 1;
						}
					}
				}
				else
				{
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					e.SortResult = this.dgServerGames.Rows[e.RowIndex1].Cells[0].Tag.ToString().CompareTo(this.dgServerGames.Rows[e.RowIndex2].Cells[0].Tag.ToString());
					bool flag6 = e.SortResult == 0;
					if (flag6)
					{
						bool flag7 = this.dgServerGames.Rows[e.RowIndex1].Cells[1].Value.ToString().StartsWith("    ");
						if (flag7)
						{
							e.SortResult = 1;
						}
						bool flag8 = this.dgServerGames.Rows[e.RowIndex2].Cells[1].Value.ToString().StartsWith("    ");
						if (flag8)
						{
							e.SortResult = -1;
						}
					}
				}
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000765CC File Offset: 0x000747CC
		private void dgServerGames_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgServerGames.SelectedCells.Count == 0 || this.dgServerGames.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
					bool flag3 = toolTipText == Resources.msgUnsupported;
					if (flag3)
					{
						Util.ShowMessage(toolTipText);
					}
				}
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00076670 File Offset: 0x00074870
		private void chkShowAll_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.chkShowAll.Checked;
			if (@checked)
			{
				this.pnlNoSaves.Visible = false;
				this.pnlNoSaves.SendToBack();
				this.dgServerGames.Columns[3].Visible = false;
				this.ShowAllGames();
			}
			else
			{
				this.dgServerGames.Columns[0].Visible = true;
				this.dgServerGames.Columns[3].Visible = true;
				this.dgServerGames.Columns[3].HeaderText = Resources.colGameCode;
				this.cbDrives_SelectedIndexChanged(null, null);
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00076720 File Offset: 0x00074920
		private void ShowAllGames()
		{
			this.dgServerGames.Rows.Clear();
			this.dgServerGames.Columns[4].Visible = false;
			this.dgServerGames.Columns[5].Visible = false;
			this.dgServerGames.Columns[7].Visible = false;
			int width = this.dgServerGames.Width;
			bool flag = width == 0;
			if (!flag)
			{
				this.dgServerGames.Columns[3].Visible = false;
				this.dgServerGames.Columns[0].Visible = false;
				this.dgServerGames.Columns[1].Width = (int)((float)width * 0.8f);
				this.dgServerGames.Columns[2].Width = (int)((float)width * 0.2f);
				List<DataGridViewRow> list = new List<DataGridViewRow>();
				((ISupportInitialize)this.dgServerGames).BeginInit();
				foreach (game game in this.m_games)
				{
					foreach (alias alias in game.GetAllAliases(true, false))
					{
						bool flag2 = game.name == alias.name && game.id != alias.id;
						if (!flag2)
						{
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.dgServerGames);
							try
							{
								dataGridViewRow.Tag = game;
								dataGridViewRow.Cells[1].Value = alias.name;
								dataGridViewRow.Cells[2].Value = game.GetCheatCount();
								string text = "";
								text = Util.GetRegion(this.RegionMap, game.region, text);
								List<string> list2 = new List<string>();
								list2.Add(game.id);
								bool flag3 = game.aliases != null && game.aliases._aliases.Count > 0;
								if (flag3)
								{
									foreach (alias alias2 in game.aliases._aliases)
									{
										string region = Util.GetRegion(this.RegionMap, alias2.region, text);
										bool flag4 = text.IndexOf(region) < 0;
										if (flag4)
										{
											text += region;
										}
										list2.Add(alias2.id);
									}
								}
								list2.Sort();
								dataGridViewRow.Cells[3].Value = text;
								dataGridViewRow.Cells[1].ToolTipText = "Supported List: " + string.Join(",", list2.ToArray());
							}
							catch (Exception ex)
							{
								IDictionary data = ex.Data;
							}
							list.Add(dataGridViewRow);
						}
					}
				}
				this.dgServerGames.Rows.AddRange(list.ToArray());
				((ISupportInitialize)this.dgServerGames).EndInit();
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00076AF0 File Offset: 0x00074CF0
		private void dgServerGames_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.chkShowAll.Checked && e.ColumnIndex == 2;
			if (!flag)
			{
				this.SortGames(e.ColumnIndex, this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending);
				bool @checked = this.chkShowAll.Checked;
				if (@checked)
				{
					this.ShowAllGames();
				}
				else
				{
					this.FillLocalSaves(null, this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
				}
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00076B90 File Offset: 0x00074D90
		private void dgServerGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = this.dgServerGames.SelectedCells.Count == 0 || this.dgServerGames.SelectedCells[0].RowIndex < 0;
				if (!flag2)
				{
					string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
					game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
					bool flag3 = game == null;
					if (flag3)
					{
						List<game> list = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as List<game>;
						bool flag4 = list == null;
						if (flag4)
						{
							bool flag5 = toolTipText == Resources.msgUnsupported;
							if (flag5)
							{
								Util.ShowMessage(toolTipText);
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							bool flag6 = this.dgServerGames.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending;
							this.FillLocalSaves(this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].Value as string, flag6);
							bool flag7 = this.dgServerGames.Rows.Count > e.RowIndex + 1;
							if (flag7)
							{
								this.dgServerGames.Rows[e.RowIndex + 1].Selected = true;
								this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
							}
							else
							{
								this.dgServerGames.Rows[Math.Min(e.RowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
								try
								{
									this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
								}
								catch (Exception)
								{
								}
							}
						}
					}
					else
					{
						this.simpleToolStripMenuItem_Click(null, null);
					}
				}
			}
		}

		// Token: 0x06001774 RID: 6004
		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		// Token: 0x06001775 RID: 6005
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern bool InsertMenu(IntPtr hMenu, int wPosition, int wFlags, int wIDNewItem, string lpNewItem);

		// Token: 0x06001776 RID: 6006 RVA: 0x00076DF0 File Offset: 0x00074FF0
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 274;
			if (flag)
			{
				int num = m.WParam.ToInt32();
				if (num == 1000)
				{
					AboutBox1 aboutBox = new AboutBox1();
					aboutBox.ShowDialog();
					return;
				}
			}
			else
			{
				bool flag2 = m.Msg == 537 && this.m_bSerialChecked;
				if (flag2)
				{
					bool flag3 = m.WParam.ToInt32() == 32768;
					if (flag3)
					{
						bool flag4 = m.LParam != IntPtr.Zero;
						if (flag4)
						{
							MainForm.DEV_BROADCAST_HDR dev_BROADCAST_HDR = (MainForm.DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(MainForm.DEV_BROADCAST_HDR));
							bool flag5 = dev_BROADCAST_HDR.dbch_DeviceType == 2U;
							if (flag5)
							{
								MainForm.DEV_BROADCAST_VOLUME dev_BROADCAST_VOLUME = (MainForm.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(MainForm.DEV_BROADCAST_VOLUME));
								for (int i = 0; i < 26; i++)
								{
									bool flag6 = ((dev_BROADCAST_VOLUME.dbcv_unitmask >> i) & 1U) == 1U;
									if (flag6)
									{
										this.drivesHelper.FillDrives();
									}
								}
							}
						}
					}
					else
					{
						bool flag7 = m.WParam.ToInt32() == 32772;
						if (flag7)
						{
							bool flag8 = m.LParam != IntPtr.Zero;
							if (flag8)
							{
								MainForm.DEV_BROADCAST_HDR dev_BROADCAST_HDR2 = (MainForm.DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(MainForm.DEV_BROADCAST_HDR));
								bool flag9 = dev_BROADCAST_HDR2.dbch_DeviceType == 2U;
								if (flag9)
								{
									MainForm.DEV_BROADCAST_VOLUME dev_BROADCAST_VOLUME2 = (MainForm.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(MainForm.DEV_BROADCAST_VOLUME));
									for (int j = 0; j < 26; j++)
									{
										bool flag10 = ((dev_BROADCAST_VOLUME2.dbcv_unitmask >> j) & 1U) == 1U;
										if (flag10)
										{
											for (int k = 0; k < this.cbDrives.Items.Count; k++)
											{
												bool flag11 = this.cbDrives.Items[k].ToString() == string.Format("{0}:\\", (char)(65 + j));
												if (flag11)
												{
													this.cbDrives.Items.RemoveAt(k);
												}
											}
										}
									}
									bool flag12 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
									if (flag12)
									{
										this.chkShowAll.Checked = true;
										this.chkShowAll.Enabled = false;
										this.drivesHelper.FillDrives();
									}
									else
									{
										this.cbDrives.SelectedIndex = 0;
									}
								}
							}
						}
					}
				}
			}
			bool flag13 = m.Msg == 274 && (int)m.WParam == 61728;
			if (flag13)
			{
				base.Invalidate(true);
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00077110 File Offset: 0x00075310
		private int InitSession()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				string uid = Util.GetUID(false, false);
				bool flag = string.IsNullOrEmpty(uid);
				if (flag)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase(), true);
					try
					{
						registryKey.DeleteValue("Hash");
					}
					catch
					{
					}
					Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME), Resources.msgError);
					base.Close();
					return 0;
				}
				byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"START_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), uid)));
				string @string = Encoding.ASCII.GetString(array);
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag2 = dictionary.ContainsKey("update");
				if (flag2)
				{
					Dictionary<string, object> dictionary2 = dictionary["update"] as Dictionary<string, object>;
					foreach (string text in dictionary2.Keys)
					{
						string text2 = (string)dictionary2[text];
						bool flag3 = text2.IndexOf("msi", StringComparison.CurrentCultureIgnoreCase) > 0;
						if (flag3)
						{
							UpgradeDownloader upgradeDownloader = new UpgradeDownloader(text2);
							upgradeDownloader.ShowDialog();
							base.Close();
							return 0;
						}
					}
				}
				bool flag4 = dictionary.ContainsKey("token");
				if (flag4)
				{
					Util.SetAuthToken(dictionary["token"] as string);
					Thread thread = new Thread(new ParameterizedThreadStart(this.Pinger));
					thread.Start(Convert.ToInt32(dictionary["expiry_ts"]) - Convert.ToInt32(dictionary["current_ts"]));
					Thread thread2 = new Thread(new ParameterizedThreadStart(this.TrafficPoller));
					thread2.Start();
					this.GetPSNIDInfo();
					this.m_sessionInited = true;
					return 1;
				}
				bool flag5 = dictionary.ContainsKey("code") && (dictionary["code"].ToString() == "10009" || dictionary["code"].ToString() == "4071");
				if (flag5)
				{
					return -1;
				}
				Util.DeleteRegistryValue("User");
				bool flag6 = dictionary.ContainsKey("code");
				if (flag6)
				{
					Util.ShowErrorMessage(dictionary, string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME));
				}
				else
				{
					Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME));
				}
				base.Close();
				return 0;
			}
			catch (Exception ex)
			{
				bool flag7 = ex is WebException;
				if (flag7)
				{
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00077468 File Offset: 0x00075668
		private void MainForm_Load(object sender, EventArgs e)
		{
			bool flag = !this.CheckForVersion();
			if (!flag)
			{
				bool flag2 = !this.CheckSerial();
				if (flag2)
				{
					base.Close();
				}
				else
				{
					this.m_bSerialChecked = true;
					int num = this.InitSession();
					bool flag3 = num < 0;
					if (flag3)
					{
						Util.ChangeServer();
						num = this.InitSession();
					}
					bool flag4 = num == 0;
					if (!flag4)
					{
						bool flag5 = num < 0;
						if (flag5)
						{
							Util.ShowMessage(Resources.errServerConnection);
							base.Close();
						}
						else
						{
							GameListDownloader gameListDownloader = new GameListDownloader();
							bool flag6 = gameListDownloader.ShowDialog() == DialogResult.OK;
							if (flag6)
							{
								bool flag7 = this.m_psnIDs.Count == 0;
								if (flag7)
								{
								}
								try
								{
									this.FillSavesList(gameListDownloader.GameListXml);
								}
								catch (Exception)
								{
									Util.ShowMessage(Resources.errInternal, Resources.msgError);
									base.Close();
									return;
								}
								bool flag8 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
								if (flag8)
								{
									this.chkShowAll.Checked = true;
									this.chkShowAll.Enabled = false;
									this.btnHome_Click(null, null);
								}
								else
								{
									this.PrepareLocalSavesMap();
									this.FillLocalSaves(null, true);
									this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
									this.btnHome_Click(string.Empty, null);
								}
							}
							else
							{
								base.Close();
							}
							bool flag9 = !this.isRunning && Util.IsUnixOrMacOSX();
							if (flag9)
							{
								global::System.Timers.Timer timer = new global::System.Timers.Timer();
								this.previousDriveNum = DriveInfo.GetDrives().Length;
								timer.Elapsed += delegate(object s, ElapsedEventArgs e2)
								{
									DriveInfo[] drives = DriveInfo.GetDrives();
									bool flag10 = this.previousDriveNum != drives.Length;
									if (flag10)
									{
										this.drivesHelper.FillDrives();
										this.previousDriveNum = drives.Length;
										bool flag11 = this.cbDrives.Items.Count == 0 || this.cbDrives.Items[0].ToString() == "";
										if (flag11)
										{
											this.chkShowAll.Checked = true;
											this.chkShowAll.Enabled = false;
										}
									}
								};
								timer.Interval = 10000.0;
								timer.Enabled = true;
								this.isRunning = true;
							}
							this.isFirstRunning = false;
						}
					}
				}
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0007767C File Offset: 0x0007587C
		private void TrafficPoller(object ob)
		{
			this.evt2 = new AutoResetEvent(false);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0007768C File Offset: 0x0007588C
		private void Pinger(object tim)
		{
			int num = (int)tim;
			this.evt = new AutoResetEvent(false);
			string text = "{{\"action\":\"SESSION_REFRESH\",\"userid\":\"{0}\",\"token\":\"{1}\"}}";
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			while (!this.evt.WaitOne((num - 10) * 1000))
			{
				for (;;)
				{
					try
					{
						byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format(text, Util.GetUserId(), Util.GetAuthToken())));
						string @string = Encoding.ASCII.GetString(array);
						bool flag = @string.Contains("ERROR");
						if (flag)
						{
							return;
						}
						Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
						bool flag2 = dictionary.ContainsKey("token");
						if (flag2)
						{
							Util.SetAuthToken(dictionary["token"] as string);
						}
						break;
					}
					catch (Exception ex)
					{
						Thread.Sleep(3000);
					}
				}
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000777C0 File Offset: 0x000759C0
		private void PrepareLocalSavesMap()
		{
			this.m_dictLocalSaves.Clear();
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				string text = this.cbDrives.SelectedItem.ToString();
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text);
				if (flag2)
				{
					text = string.Format("/Volumes{0}", text);
				}
				else
				{
					bool flag3 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text);
					if (flag3)
					{
						text = string.Format("/media/{0}{1}", Environment.UserName, text);
					}
				}
				string dataPath = Util.GetDataPath(text);
				bool flag4 = !Directory.Exists(dataPath);
				if (!flag4)
				{
					string[] array = Directory.GetDirectories(dataPath);
					List<string> list = new List<string>();
					foreach (string text2 in array)
					{
						string fileName = Path.GetFileName(text2);
						long num;
						bool flag5 = !long.TryParse(fileName, NumberStyles.HexNumber, null, out num);
						if (!flag5)
						{
							string[] directories = Directory.GetDirectories(text2);
							foreach (string text3 in directories)
							{
								string fileName2 = Path.GetFileName(text3);
								bool flag6 = !fileName2.StartsWith("CUSA") || Directory.GetFiles(text3).Length == 0;
								if (!flag6)
								{
									list.AddRange(Directory.GetFiles(text3, "*.bin"));
								}
							}
						}
					}
					array = list.ToArray();
					Array.Sort<string>(array);
					foreach (string text4 in array)
					{
						string text5;
						int onlineSaveIndex = this.GetOnlineSaveIndex(text4, out text5);
						bool flag7 = onlineSaveIndex >= 0;
						if (flag7)
						{
							int num2 = this.dgServerGames.Rows.Add();
							game game = game.Copy(this.m_games[onlineSaveIndex]);
							game.id = text5;
							game.LocalCheatExists = true;
							game.LocalSaveFolder = text4;
							bool flag8 = game.GetTargetGameFolder() == null;
							if (flag8)
							{
								game.LocalCheatExists = false;
							}
							try
							{
								MainForm.FillLocalCheats(ref game);
							}
							catch (Exception)
							{
							}
							bool flag9 = !this.m_dictLocalSaves.ContainsKey(game.id);
							if (flag9)
							{
								List<game> list2 = new List<game>();
								list2.Add(game);
								this.m_dictLocalSaves.Add(game.id, list2);
							}
							else
							{
								this.m_dictLocalSaves[game.id].Add(game);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00077A68 File Offset: 0x00075C68
		private void FillLocalSaves(string expandGame, bool bSortedAsc)
		{
			bool flag = this.m_expandedGame == expandGame;
			if (flag)
			{
				expandGame = null;
				this.m_expandedGame = null;
			}
			((ISupportInitialize)this.dgServerGames).BeginInit();
			this.dgServerGames.Rows.Clear();
			List<string> list = new List<string>();
			List<DataGridViewRow> list2 = new List<DataGridViewRow>();
			foreach (game game in this.m_games)
			{
				foreach (alias alias in game.GetAllAliases(bSortedAsc, false))
				{
					string text = alias.name;
					text = text + " (" + alias.id + ")";
					string id = alias.id;
					bool flag2 = !this.m_dictLocalSaves.ContainsKey(alias.id);
					if (!flag2)
					{
						List<game> list3 = this.m_dictLocalSaves[id];
						bool flag3 = list.IndexOf(id) >= 0;
						if (!flag3)
						{
							list.Add(id);
							List<DataGridViewRow> list4 = new List<DataGridViewRow>();
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.dgServerGames);
							dataGridViewRow.Cells[1].Value = alias.name;
							bool flag4 = list3.Count == 0;
							if (flag4)
							{
								game game2 = list3[0];
								game2.diskcode = alias.diskcode;
								dataGridViewRow.Tag = game2;
								container targetGameFolder = game2.GetTargetGameFolder();
								dataGridViewRow.Cells[2].Value = ((targetGameFolder != null) ? targetGameFolder.GetCheatsCount().ToString() : "N/A");
								dataGridViewRow.Cells[0].ToolTipText = "";
								dataGridViewRow.Cells[0].Tag = id;
								dataGridViewRow.Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game2.LocalSaveFolder);
								dataGridViewRow.Cells[3].Value = id;
								dataGridViewRow.Cells[6].Value = true;
								dataGridViewRow.Cells[4].Value = this.GetPSNID(game2);
								bool flag5 = !this.IsValidPSNID(game2.PSN_ID);
								if (flag5)
								{
									dataGridViewRow.DefaultCellStyle = new DataGridViewCellStyle
									{
										ForeColor = Color.Gray
									};
									dataGridViewRow.Cells[1].Tag = "U";
								}
							}
							else
							{
								DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
								dataGridViewRow.Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
								{
									Font = new Font("Arial", Util.ScaleSize(7f))
								});
								dataGridViewRow.Cells[0].Value = "►";
								dataGridViewRow.Cells[1].Value = string.Concat(new object[]
								{
									dataGridViewRow.Cells[1].Value,
									" (",
									alias.id,
									")"
								});
								dataGridViewCellStyle.BackColor = Color.White;
								dataGridViewRow.Cells[0].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[1].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[2].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[3].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Cells[4].Style.ApplyStyle(dataGridViewCellStyle);
								dataGridViewRow.Tag = list3;
								dataGridViewRow.Cells[6].Value = false;
								bool flag6 = text == expandGame;
								if (flag6)
								{
									dataGridViewRow.Cells[0].Style.ApplyStyle(new DataGridViewCellStyle
									{
										Font = new Font("Arial", Util.ScaleSize(7f))
									});
									dataGridViewRow.Cells[0].Value = "▼";
									dataGridViewRow.Cells[0].ToolTipText = "";
									dataGridViewRow.Cells[1].Value = alias.name + " (" + alias.id + ")";
									dataGridViewRow.Cells[0].Tag = id;
									foreach (game game3 in list3)
									{
										container targetGameFolder2 = game3.GetTargetGameFolder();
										bool flag7 = targetGameFolder2 == null;
										if (!flag7)
										{
											DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
											dataGridViewRow2.CreateCells(this.dgServerGames);
											Match match = Regex.Match(Path.GetFileNameWithoutExtension(game3.LocalSaveFolder), targetGameFolder2.pfs);
											bool flag8 = match.Groups != null && match.Groups.Count > 1;
											if (flag8)
											{
												dataGridViewRow2.Cells[1].Value = "    " + targetGameFolder2.name.Replace("${1}", match.Groups[1].Value);
											}
											else
											{
												dataGridViewRow2.Cells[1].Value = "    " + (targetGameFolder2.name ?? Path.GetFileNameWithoutExtension(game3.LocalSaveFolder));
											}
											game3.diskcode = alias.diskcode;
											dataGridViewRow2.Cells[0].Tag = id;
											dataGridViewRow2.Tag = game3;
											dataGridViewRow2.Cells[2].Value = ((targetGameFolder2 != null) ? targetGameFolder2.GetCheatsCount().ToString() : "N/A");
											dataGridViewRow2.Cells[1].ToolTipText = Path.GetFileNameWithoutExtension(game3.LocalSaveFolder);
											dataGridViewRow2.Cells[3].Value = id;
											dataGridViewRow2.Cells[6].Value = true;
											dataGridViewRow2.Cells[4].Value = this.GetPSNID(game3);
											bool flag9 = !this.IsValidPSNID(game3.PSN_ID);
											if (flag9)
											{
												dataGridViewRow2.DefaultCellStyle = new DataGridViewCellStyle
												{
													ForeColor = Color.Gray
												};
												dataGridViewRow2.Cells[1].Tag = "U";
											}
											list4.Add(dataGridViewRow2);
										}
									}
									this.m_expandedGame = expandGame;
								}
							}
							list2.Add(dataGridViewRow);
							list2.AddRange(list4.ToArray());
						}
					}
				}
			}
			this.dgServerGames.Rows.AddRange(list2.ToArray());
			this.FillUnavailableGames();
			this.dgServerGames.ClearSelection();
			((ISupportInitialize)this.dgServerGames).EndInit();
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00078224 File Offset: 0x00076424
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

		// Token: 0x0600177E RID: 6014 RVA: 0x00078284 File Offset: 0x00076484
		private string GetProfileKey(string sfoPath, Dictionary<string, string> mapProfiles)
		{
			bool flag = File.Exists(sfoPath);
			if (flag)
			{
				int num;
				string text = Convert.ToBase64String(MainForm.GetParamInfo(sfoPath, out num));
				string text2 = string.Concat(new string[]
				{
					num.ToString(),
					":",
					text,
					":",
					Convert.ToBase64String(Util.GetPSNId(Path.GetDirectoryName(sfoPath)))
				});
				bool flag2 = mapProfiles.ContainsKey(text2);
				if (flag2)
				{
					return mapProfiles[text2];
				}
				string text3 = num.ToString() + ":" + text;
				bool flag3 = mapProfiles.ContainsKey(text3);
				if (flag3)
				{
					return mapProfiles[text3];
				}
			}
			return "";
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00078340 File Offset: 0x00076540
		private bool CheckSerial()
		{
			bool flag = Util.GetRegistryValue("User") == null;
			if (flag)
			{
				SerialValidateGG serialValidateGG = new SerialValidateGG();
				bool flag2 = serialValidateGG.ShowDialog(this) != DialogResult.OK;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00078384 File Offset: 0x00076584
		private void SetLabels()
		{
			this.picTraffic.BackgroundImageLayout = ImageLayout.None;
			this.picVersion.BackgroundImageLayout = ImageLayout.None;
			this.picVersion.Visible = false;
			this.pictureBox2.BackgroundImage = Resources.company;
			this.pictureBox2.BackgroundImageLayout = ImageLayout.None;
			this.panel1.BackgroundImage = Resources.sel_drive;
			this.lblNoSaves.Text = Resources.lblNoSaves;
			base.Icon = Resources.dp;
			this.btnGamesInServer.Text = Resources.btnViewAllCheats;
			this.btnApply.Text = Resources.btnApply;
			this.btnBrowse.Text = Resources.btnBrowse;
			this.chkBackup.Text = Resources.chkBackupSaves;
			this.lblBackup.Text = Resources.gbBackupLocation;
			this.dgServerGames.Columns[0].HeaderText = "";
			this.dgServerGames.Columns[1].HeaderText = Resources.colGameName;
			this.dgServerGames.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			this.dgServerGames.Columns[2].HeaderText = Resources.colCheats;
			this.dgServerGames.Columns[3].HeaderText = Resources.colGameCode;
			this.dgServerGames.Columns[4].HeaderText = Resources.colProfile;
			this.dgServerGames.Columns[3].Visible = false;
			this.btnRss.Text = Resources.btnRss;
			this.btnDeactivate.Text = Resources.btnDeactivate;
			this.simpleToolStripMenuItem.Text = Resources.mnuSimple;
			this.advancedToolStripMenuItem.Text = Resources.mnuAdvanced;
			this.deleteSaveToolStripMenuItem.Text = Resources.mnuDeleteSave;
			this.resignToolStripMenuItem.Text = Resources.mnuResign;
			this.registerPSNIDToolStripMenuItem.Text = Resources.mnuRegisterPSN;
			this.restoreFromBackupToolStripMenuItem.Text = Resources.mnuRestore;
			this.Text = Util.PRODUCT_NAME;
			this.btnOpenFolder.Text = Resources.btnOpenFolder;
			this.lblDeactivate.Text = Resources.lblDeactivate;
			this.lblRSSSection.Text = Resources.lblRSSSection;
			this.btnManageProfiles.Text = Resources.btnUserAccount;
			this.lblManageProfiles.Text = Resources.lblUserAccount;
			this.panel3.BackgroundImageLayout = ImageLayout.Tile;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0007860C File Offset: 0x0007680C
		private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			CultureInfo cultureInfo = this.cbLanguage.SelectedItem as CultureInfo;
			Util.SetRegistryValue("Language", cultureInfo.Name);
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			this.SetLabels();
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00078650 File Offset: 0x00076850
		internal static void FillLocalCheats(ref game item)
		{
			string text = Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm.USER_CHEATS_FILE;
			bool flag = File.Exists(text);
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text);
				for (int i = 0; i < xmlDocument["usercheats"].ChildNodes.Count; i++)
				{
					container targetGameFolder = item.GetTargetGameFolder();
					bool flag2 = targetGameFolder != null && item.id + targetGameFolder.key == xmlDocument["usercheats"].ChildNodes[i].Attributes["id"].Value;
					if (flag2)
					{
						bool flag3 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes.Count > 0;
						if (flag3)
						{
							for (int j = 0; j < xmlDocument["usercheats"].ChildNodes[i].ChildNodes.Count; j++)
							{
								XmlNode xmlNode = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j];
								bool flag4 = (xmlNode as XmlElement).Name == "file";
								if (flag4)
								{
									XmlElement xmlElement = xmlNode as XmlElement;
									string attribute = xmlElement.GetAttribute("name");
									file gameFile = item.GetGameFile(targetGameFolder, attribute);
									bool flag5 = gameFile != null;
									if (flag5)
									{
										gameFile.ucfilename = attribute;
										for (int k = gameFile.Cheats.Count - 1; k >= 0; k--)
										{
											bool flag6 = gameFile.Cheats[k].id == "-1";
											if (flag6)
											{
												gameFile.Cheats.Remove(gameFile.Cheats[k]);
											}
										}
										for (int l = 0; l < xmlElement.ChildNodes.Count; l++)
										{
											XmlNode xmlNode2 = xmlElement.ChildNodes[l];
											string value = xmlNode2.Attributes["desc"].Value;
											string value2 = xmlNode2.Attributes["comment"].Value;
											cheat cheat = new cheat("-1", value, value2);
											for (int m = 0; m < xmlNode2.ChildNodes.Count; m++)
											{
												string text2 = xmlNode2.ChildNodes[m].InnerText;
												text2 = text2.Replace("\r\n", " ").TrimEnd(new char[0]);
												text2 = text2.Replace("\n", " ").TrimEnd(new char[0]);
												string[] array = text2.Split(new char[] { ' ' });
												bool flag7 = array.Length % 2 == 0;
												if (flag7)
												{
													cheat.code = text2;
												}
											}
											bool flag8 = gameFile != null;
											if (flag8)
											{
												gameFile.Cheats.Add(cheat);
											}
										}
									}
								}
								else
								{
									string value3 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].Attributes["desc"].Value;
									string value4 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].Attributes["comment"].Value;
									cheat cheat2 = new cheat("-1", value3, value4);
									for (int n = 0; n < xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].ChildNodes.Count; n++)
									{
										string text3 = xmlDocument["usercheats"].ChildNodes[i].ChildNodes[j].ChildNodes[n].InnerText;
										text3 = text3.Replace("\r\n", " ").TrimEnd(new char[0]);
										text3 = text3.Replace("\n", " ").TrimEnd(new char[0]);
										string[] array2 = text3.Split(new char[] { ' ' });
										bool flag9 = array2.Length == 2;
										if (flag9)
										{
											cheat2.code = text3;
										}
									}
									bool flag10 = !string.IsNullOrEmpty(cheat2.code);
									if (flag10)
									{
										bool flag11 = targetGameFolder != null;
										if (flag11)
										{
											targetGameFolder.files._files[0].Cheats.Add(cheat2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00078B74 File Offset: 0x00076D74
		private void FillServerGamesList()
		{
			this.dgServerGames.Rows.Clear();
			foreach (game game in this.m_games)
			{
				int num = this.dgServerGames.Rows.Add(new DataGridViewRow());
				this.dgServerGames.Rows[num].Cells[1].Value = game.name;
				this.dgServerGames.Rows[num].Cells[2].Value = game.GetCheatCount();
				this.dgServerGames.Rows[num].Cells[3].Value = game.id;
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00078C70 File Offset: 0x00076E70
		private void FillUnavailableGames()
		{
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				string text = this.cbDrives.SelectedItem.ToString();
				bool flag2 = !Directory.Exists(text + "PS4\\SAVEDATA");
				if (!flag2)
				{
					string[] directories = Directory.GetDirectories(text + "PS4\\SAVEDATA");
					List<DataGridViewRow> list = new List<DataGridViewRow>();
					foreach (string text2 in directories)
					{
						string text3;
						bool flag3 = this.GetOnlineSaveIndex(text2, out text3) == -1;
						if (flag3)
						{
							string text4 = text2 + Path.DirectorySeparatorChar.ToString() + "PARAM.SFO";
							bool flag4 = File.Exists(text4);
							if (flag4)
							{
								DataGridViewRow dataGridViewRow = new DataGridViewRow();
								dataGridViewRow.CreateCells(this.dgServerGames);
								Color lightSlateGray = Color.LightSlateGray;
								dataGridViewRow.Cells[0].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[1].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[2].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[3].ToolTipText = Resources.msgUnsupported;
								dataGridViewRow.Cells[0].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[1].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[2].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[3].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[4].Style.BackColor = lightSlateGray;
								dataGridViewRow.Cells[1].Value = this.GetSaveTitle(text4);
								dataGridViewRow.Cells[3].Value = Path.GetFileName(text2).Substring(0, 9);
								dataGridViewRow.Cells[0].Tag = dataGridViewRow.Cells[3].Value;
								dataGridViewRow.Cells[4].Value = "";
								dataGridViewRow.Tag = text2;
								list.Add(dataGridViewRow);
							}
						}
					}
					this.dgServerGames.Rows.AddRange(list.ToArray());
				}
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00078EEC File Offset: 0x000770EC
		private void dgServerGames_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.RowIndex < 0;
			if (!flag)
			{
				bool flag2 = e.Button == MouseButtons.Right;
				if (flag2)
				{
					this.dgServerGames.ClearSelection();
					this.dgServerGames.Rows[e.RowIndex].Selected = true;
				}
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00078F48 File Offset: 0x00077148
		private void SortGames(int sortCol, bool bDesc)
		{
			this.m_games.Sort(delegate(game item1, game item2)
			{
				int sortCol2 = sortCol;
				int num;
				if (sortCol2 != 2)
				{
					if (sortCol2 != 3)
					{
						if (sortCol2 != 7)
						{
							num = (item1.name + item1.id).CompareTo(item2.name + item2.id);
						}
						else
						{
							num = string.Compare(item1.diskcode, item2.diskcode);
						}
					}
					else
					{
						num = item1.id.CompareTo(item2.id);
					}
				}
				else
				{
					num = item1.GetCheatCount().CompareTo(item2.GetCheatCount());
				}
				return num;
			});
			if (bDesc)
			{
				this.m_games.Reverse();
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00078F8C File Offset: 0x0007718C
		private void FillSavesList(string xml)
		{
			this.m_games = new List<game>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(games));
				using (StringReader stringReader = new StringReader(xml))
				{
					games games = (games)xmlSerializer.Deserialize(stringReader);
					this.m_games = games._games;
				}
			}
			catch (Exception)
			{
				try
				{
					xml = xml.Replace("&", "&amp;");
					XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(games));
					using (StringReader stringReader2 = new StringReader(xml))
					{
						games games2 = (games)xmlSerializer2.Deserialize(stringReader2);
						this.m_games = games2._games;
					}
				}
				catch (Exception)
				{
					return;
				}
			}
			this.m_games.Sort((game item1, game item2) => (item1.name + item1.LocalSaveFolder).CompareTo(item2.name + item1.LocalSaveFolder));
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000790BC File Offset: 0x000772BC
		private int GetPSNIDInfo()
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"PSNID_INFO\",\"userid\":\"{0}\"}}", Util.GetUserId())));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			int num;
			if (flag)
			{
				bool flag2 = dictionary.ContainsKey("psnid");
				if (flag2)
				{
					this.m_psnIDs = dictionary["psnid"] as Dictionary<string, object>;
				}
				else
				{
					this.m_psnIDs = new Dictionary<string, object>();
				}
				this.m_psn_quota = Convert.ToInt32(dictionary["psnid_quota"]);
				this.m_psn_remaining = Convert.ToInt32(dictionary["psnid_remaining"]);
				this.gbProfiles.Controls.Clear();
				this.gbProfiles.Width = this.m_psn_quota * 18 + 35;
				for (int i = 0; i < this.m_psn_quota; i++)
				{
					PictureBox pictureBox = new PictureBox();
					bool flag3 = i < this.m_psn_quota - this.m_psn_remaining;
					if (flag3)
					{
						pictureBox.Image = Resources.check;
					}
					else
					{
						pictureBox.Image = Resources.uncheck;
					}
					pictureBox.Left = 8 + i * 18;
					pictureBox.Top = 8;
					pictureBox.Width = 18;
					this.gbProfiles.Controls.Add(pictureBox);
				}
				TextBox textBox = new TextBox();
				textBox.Text = string.Format("{0}/{1}", this.m_psn_quota - this.m_psn_remaining, this.m_psn_quota);
				textBox.Left = this.m_psn_quota * 18 + 8;
				textBox.Top = 9;
				textBox.Width = 26;
				textBox.ForeColor = Color.White;
				textBox.BorderStyle = BorderStyle.None;
				textBox.BackColor = Color.FromArgb(102, 132, 162);
				this.gbProfiles.Controls.Add(textBox);
				num = this.m_psn_quota;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00079344 File Offset: 0x00077544
		public bool IsValidPSNID(string psnId)
		{
			return this.m_psnIDs != null && this.m_psnIDs.ContainsKey(psnId);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00079378 File Offset: 0x00077578
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.chkShowAll.Checked || this.dgServerGames.SelectedCells.Count == 0 || this.cbDrives.Items.Count == 0;
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				this.simpleToolStripMenuItem.Visible = true;
				this.advancedToolStripMenuItem.Visible = true;
				int rowIndex = this.dgServerGames.SelectedCells[1].RowIndex;
				bool flag2 = !(bool)this.dgServerGames.Rows[rowIndex].Cells[6].Value;
				if (flag2)
				{
					e.Cancel = true;
				}
				bool flag3 = (string)this.dgServerGames.Rows[rowIndex].Cells[1].Tag == "U";
				if (flag3)
				{
					this.registerPSNIDToolStripMenuItem.Visible = true;
					this.registerPSNIDToolStripMenuItem.Enabled = true;
					this.simpleToolStripMenuItem.Enabled = false;
					this.advancedToolStripMenuItem.Enabled = false;
					this.restoreFromBackupToolStripMenuItem.Enabled = false;
				}
				else
				{
					this.registerPSNIDToolStripMenuItem.Visible = false;
					this.registerPSNIDToolStripMenuItem.Enabled = false;
					this.restoreFromBackupToolStripMenuItem.Enabled = true;
					game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
					bool flag4 = game == null;
					if (flag4)
					{
						e.Cancel = true;
					}
					else
					{
						container targetGameFolder = game.GetTargetGameFolder();
						bool flag5 = targetGameFolder != null;
						if (flag5)
						{
							this.advancedToolStripMenuItem.Enabled = !(targetGameFolder.quickmode > 0);
							this.simpleToolStripMenuItem.Enabled = true;
						}
						else
						{
							this.simpleToolStripMenuItem.Enabled = false;
							this.advancedToolStripMenuItem.Enabled = false;
						}
						this.deleteSaveToolStripMenuItem.Visible = true;
						this.restoreFromBackupToolStripMenuItem.Visible = true;
					}
				}
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000795B0 File Offset: 0x000777B0
		private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
			bool flag = game == null || (game.PSN_ID != null && !this.IsValidPSNID(game.PSN_ID));
			if (!flag)
			{
				bool flag2 = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[2].Value as string == "N/A";
				if (!flag2)
				{
					List<string> list = null;
					bool flag3 = !this.chkShowAll.Checked;
					if (flag3)
					{
						list = game.GetContainerFiles();
						bool flag4 = list == null || list.Count < 2;
						if (flag4)
						{
							Util.ShowMessage(Resources.errNoFile, Resources.msgError);
							return;
						}
					}
					container targetGameFolder = game.GetTargetGameFolder();
					bool flag5 = targetGameFolder != null && targetGameFolder.locked > 0;
					if (flag5)
					{
						bool flag6 = Util.ShowMessage(Resources.errProfileLock, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
						if (flag6)
						{
							return;
						}
					}
					int rowIndex = this.dgServerGames.SelectedCells[0].RowIndex;
					List<string> list2 = new List<string>();
					bool flag7 = !this.chkShowAll.Checked;
					if (flag7)
					{
						string text = game.LocalSaveFolder.Substring(0, game.LocalSaveFolder.Length - 4);
						string text2 = game.ToString(new List<string>(), "decrypt");
						string tempFolder = Util.GetTempFolder();
						bool flag8 = targetGameFolder.preprocess == 1;
						if (flag8)
						{
							list.Remove(text);
							AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(list.ToArray(), game, null, "list");
							bool flag9 = advancedSaveUploaderForEncrypt.ShowDialog(this) != DialogResult.Abort && !string.IsNullOrEmpty(advancedSaveUploaderForEncrypt.ListResult);
							if (!flag9)
							{
								Util.ShowMessage(Resources.errInvalidSave);
								return;
							}
							ArrayList arrayList = new JavaScriptSerializer().Deserialize(advancedSaveUploaderForEncrypt.ListResult, typeof(ArrayList)) as ArrayList;
							foreach (object obj in arrayList)
							{
								list2.Add((string)obj);
							}
						}
					}
					bool flag10 = Util.IsUnixOrMacOSX();
					if (flag10)
					{
						SimpleEdit simpleEdit = new SimpleEdit(game, this.chkShowAll.Checked, list2);
						bool flag11 = simpleEdit.ShowDialog() == DialogResult.OK;
						if (flag11)
						{
							this.dgServerGames.Rows[rowIndex].Tag = simpleEdit.GameItem;
							this.dgServerGames.Rows[rowIndex].Cells[2].Value = simpleEdit.GameItem.GetCheatCount();
							this.PrepareLocalSavesMap();
							string expandedGame = this.m_expandedGame;
							this.m_expandedGame = null;
							int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.FillLocalSaves(expandedGame, this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
							this.dgServerGames.Rows[Math.Min(rowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
							try
							{
								this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
							}
							catch (Exception)
							{
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex2 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.cbDrives_SelectedIndexChanged(null, null);
							this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex2;
						}
					}
					else
					{
						SimpleTreeEdit simpleTreeEdit = new SimpleTreeEdit(game, this.chkShowAll.Checked, list2);
						bool flag12 = simpleTreeEdit.ShowDialog() == DialogResult.OK;
						if (flag12)
						{
							this.dgServerGames.Rows[rowIndex].Tag = simpleTreeEdit.GameItem;
							this.dgServerGames.Rows[rowIndex].Cells[2].Value = simpleTreeEdit.GameItem.GetCheatCount();
							this.PrepareLocalSavesMap();
							string expandedGame2 = this.m_expandedGame;
							this.m_expandedGame = null;
							int firstDisplayedScrollingRowIndex3 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.FillLocalSaves(expandedGame2, this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection == SortOrder.Ascending);
							this.dgServerGames.Rows[Math.Min(rowIndex, this.dgServerGames.Rows.Count - 1)].Selected = true;
							try
							{
								this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex3;
							}
							catch (Exception)
							{
							}
						}
						else
						{
							int firstDisplayedScrollingRowIndex4 = this.dgServerGames.FirstDisplayedScrollingRowIndex;
							this.cbDrives_SelectedIndexChanged(null, null);
							this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex4;
						}
					}
				}
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00079AF0 File Offset: 0x00077CF0
		private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedCells.Count == 0;
			if (!flag)
			{
				Util.ClearTemp();
				string text = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].Value as string;
				string toolTipText = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Cells[1].ToolTipText;
				game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
				List<string> containerFiles = game.GetContainerFiles();
				bool flag2 = containerFiles.Count < 2;
				if (flag2)
				{
					Util.ShowMessage(Resources.errNoFile, Resources.msgError);
				}
				else
				{
					bool flag3 = game.GetTargetGameFolder().locked > 0;
					if (flag3)
					{
						bool flag4 = Util.ShowMessage(Resources.errProfileLock, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
						if (flag4)
						{
							return;
						}
					}
					string text2 = game.LocalSaveFolder.Substring(0, game.LocalSaveFolder.Length - 4);
					string text3 = game.ToString(new List<string>(), "decrypt");
					containerFiles.Remove(text2);
					AdvancedSaveUploaderForEncrypt advancedSaveUploaderForEncrypt = new AdvancedSaveUploaderForEncrypt(containerFiles.ToArray(), game, null, "decrypt");
					bool flag5 = advancedSaveUploaderForEncrypt.ShowDialog() != DialogResult.Abort;
					if (flag5)
					{
						bool flag6 = advancedSaveUploaderForEncrypt.DecryptedSaveData != null && advancedSaveUploaderForEncrypt.DecryptedSaveData.Count > 0;
						if (flag6)
						{
							AdvancedEdit advancedEdit = new AdvancedEdit(game, advancedSaveUploaderForEncrypt.DecryptedSaveData);
							bool flag7 = advancedEdit.ShowDialog(this) == DialogResult.OK;
							if (flag7)
							{
								this.cbDrives_SelectedIndexChanged(null, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00079CF4 File Offset: 0x00077EF4
		private void cbDrives_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.cbDrives.SelectedItem == null;
			if (!flag)
			{
				this.dgServerGames.Columns[0].Width = 25;
				int width = this.dgServerGames.Width;
				this.dgServerGames.Columns[1].Width = (int)((float)(width - 25) * 0.5f);
				this.dgServerGames.Columns[2].Width = (int)((float)(width - 25) * 0.25f);
				this.dgServerGames.Columns[3].Visible = false;
				this.dgServerGames.Columns[4].Width = (int)((float)(width - 25) * 0.25f);
				this.dgServerGames.Columns[4].Visible = true;
				string text = string.Empty;
				string text2 = this.cbDrives.SelectedItem.ToString();
				bool flag2 = text2 == Resources.colSelect && !this.isFirstRunning && sender != null && ((ComboBox)sender).Focused;
				if (flag2)
				{
					FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
					bool flag3 = Util.IsUnixOrMacOSX();
					if (flag3)
					{
						folderBrowserDialog.Description = "Select cheats folder location";
					}
					else
					{
						folderBrowserDialog.Description = Resources.lblSelectCheatsFolder;
					}
					DialogResult dialogResult = folderBrowserDialog.ShowDialog();
					bool flag4 = dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes;
					if (!flag4)
					{
						return;
					}
					bool flag5 = string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath);
					if (flag5)
					{
						Util.ShowMessage(Resources.notSelected);
						this.cbDrives_SelectedIndexChanged(sender, e);
						return;
					}
					text = Path.GetFullPath(folderBrowserDialog.SelectedPath).Normalize();
					bool flag6 = !Util.IsPathToCheats(text);
					if (flag6)
					{
						Util.ShowMessage(Resources.msgWrongPath);
						this.cbDrives_SelectedIndexChanged(sender, e);
						return;
					}
					this.cbDrives.Items.Clear();
					this.cbDrives.Items.Add(Resources.colSelect);
					string shortPath = Util.GetShortPath(text);
					int num = this.cbDrives.Items.Add(shortPath);
					this.cbDrives.SelectedIndex = num;
					bool flag7 = !this.chkShowAll.Enabled;
					if (flag7)
					{
						this.chkShowAll.Enabled = true;
						this.chkShowAll.Checked = false;
					}
					Util.SaveCheatsPathToRegistry(shortPath);
				}
				else
				{
					bool flag8 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text2);
					if (flag8)
					{
						text2 = string.Format("/Volumes{0}", text2);
					}
					else
					{
						bool flag9 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text2);
						if (flag9)
						{
							text2 = string.Format("/media/{0}{1}", Environment.UserName, text2);
						}
					}
					text = Util.GetDataPath(text2);
				}
				bool flag10 = !string.IsNullOrEmpty(text) && !text.StartsWith(Resources.colSelect);
				bool flag11 = (!Directory.Exists(text) || Directory.GetDirectories(text).Length == 0) && !flag10;
				if (flag11)
				{
					bool flag12 = !this.chkShowAll.Enabled || string.IsNullOrEmpty(text) || text.StartsWith(Resources.colSelect);
					if (flag12)
					{
						this.chkShowAll.Enabled = true;
						this.chkShowAll.Checked = false;
					}
					bool flag13 = !this.chkShowAll.Checked;
					if (flag13)
					{
						this.pnlNoSaves.Visible = true;
						this.pnlNoSaves.BringToFront();
					}
				}
				else
				{
					bool flag14 = !this.chkShowAll.Checked;
					if (flag14)
					{
						this.pnlNoSaves.Visible = false;
						this.pnlNoSaves.SendToBack();
						this.PrepareLocalSavesMap();
						this.FillLocalSaves(null, true);
						this.dgServerGames.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
					}
					else
					{
						this.chkShowAll_CheckedChanged(null, null);
					}
				}
				bool flag15 = this.dgServerGames.Rows.Count == 0 && !this.chkShowAll.Checked;
				if (flag15)
				{
					this.pnlNoSaves.Visible = true;
					this.pnlNoSaves.Location = new Point(Util.ScaleSize(12), Util.ScaleSize(12));
					this.pnlNoSaves.BringToFront();
				}
				else
				{
					this.pnlNoSaves.Visible = false;
					this.pnlNoSaves.Location = new Point(Util.ScaleSize(-9999), Util.ScaleSize(12));
					this.pnlNoSaves.SendToBack();
				}
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0007A194 File Offset: 0x00078394
		private int GetOnlineSaveIndex(string save, out string saveId)
		{
			string fileName = Path.GetFileName(Path.GetDirectoryName(save));
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(save);
			for (int i = 0; i < this.m_games.Count; i++)
			{
				saveId = this.m_games[i].id;
				bool flag = fileName.Equals(this.m_games[i].id) || this.m_games[i].IsAlias(fileName, out saveId);
				if (flag)
				{
					for (int j = 0; j < this.m_games[i].containers._containers.Count; j++)
					{
						bool flag2 = fileNameWithoutExtension == this.m_games[i].containers._containers[j].pfs || Util.IsMatch(fileNameWithoutExtension, this.m_games[i].containers._containers[j].pfs);
						if (flag2)
						{
							return i;
						}
					}
				}
			}
			saveId = null;
			return -1;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0007A2C4 File Offset: 0x000784C4
		private int GetOnlineSaveIndexByGameName(string gameName)
		{
			for (int i = 0; i < this.m_games.Count; i++)
			{
				bool flag = gameName.Equals(this.m_games[i].name);
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0007A314 File Offset: 0x00078514
		public static string GetParamInfo(string sfoFile, string item)
		{
			bool flag = !File.Exists(sfoFile);
			string text;
			if (flag)
			{
				text = "";
			}
			else
			{
				byte[] array = File.ReadAllBytes(sfoFile);
				int num = BitConverter.ToInt32(array, 8);
				int num2 = BitConverter.ToInt32(array, 12);
				int num3 = BitConverter.ToInt32(array, 16);
				int num4 = 16;
				for (int i = 0; i < num3; i++)
				{
					short num5 = BitConverter.ToInt16(array, i * num4 + 20);
					int num6 = BitConverter.ToInt32(array, i * num4 + 12 + 20);
					bool flag2 = Encoding.UTF8.GetString(array, num + (int)num5, item.Length) == item;
					if (flag2)
					{
						int j;
						for (j = 0; j < array.Length; j++)
						{
							bool flag3 = array[num2 + num6 + j] == 0;
							if (flag3)
							{
								break;
							}
						}
						return Encoding.UTF8.GetString(array, num2 + num6, j);
					}
				}
				text = "";
			}
			return text;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0007A418 File Offset: 0x00078618
		public static byte[] GetParamInfo(string sfoFile, out int profileId)
		{
			profileId = 0;
			bool flag = !File.Exists(sfoFile);
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				byte[] array2 = File.ReadAllBytes(sfoFile);
				int num = BitConverter.ToInt32(array2, 8);
				int num2 = BitConverter.ToInt32(array2, 12);
				int num3 = BitConverter.ToInt32(array2, 16);
				int num4 = 16;
				for (int i = 0; i < num3; i++)
				{
					short num5 = BitConverter.ToInt16(array2, i * num4 + 20);
					int num6 = BitConverter.ToInt32(array2, i * num4 + 12 + 20);
					bool flag2 = Encoding.UTF8.GetString(array2, num + (int)num5, 5) == "PARAM";
					if (flag2)
					{
						byte[] array3 = new byte[16];
						Array.Copy(array2, num2 + num6 + 28, array3, 0, 16);
						profileId = BitConverter.ToInt32(array2, num2 + num6 + 28 + 16);
						return array3;
					}
				}
				array = null;
			}
			return array;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0007A504 File Offset: 0x00078704
		private string GetSaveDescription(string sfoFile)
		{
			return MainForm.GetParamInfo(sfoFile, "SUB_TITLE") + "\r\n" + MainForm.GetParamInfo(sfoFile, "DETAIL");
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0007A538 File Offset: 0x00078738
		private string GetSaveTitle(string sfoFile)
		{
			return MainForm.GetParamInfo(sfoFile, "TITLE");
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0007A558 File Offset: 0x00078758
		private void btnHome_Click(object sender, EventArgs e)
		{
			this.pnlHome.Visible = true;
			this.pnlBackup.Visible = false;
			this.pnlHome.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
			this.pnlBackup.Location = new Point(Util.ScaleSize(-9999), Util.ScaleSize(15));
			bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag)
			{
				this.btnHome.Image = Resources.home_gamelist_on;
				this.btnOptions.Image = Resources.home_settings_off;
				this.btnHelp.Image = Resources.home_help_off;
			}
			else
			{
				this.btnHome.BackgroundImage = Resources.home_gamelist_on;
				this.btnOptions.BackgroundImage = Resources.home_settings_off;
				this.btnHelp.BackgroundImage = Resources.home_help_off;
			}
			bool flag2 = sender != null;
			if (flag2)
			{
				this.cbDrives_SelectedIndexChanged(null, null);
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0007A64D File Offset: 0x0007884D
		private void btnSaves_Click(object sender, EventArgs e)
		{
			this.pnlHome.Visible = false;
			this.pnlBackup.Visible = false;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0007A66C File Offset: 0x0007886C
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				folderBrowserDialog.Description = "Select Backup Folder Location";
			}
			else
			{
				folderBrowserDialog.Description = Resources.lblSelectFolder;
			}
			DialogResult dialogResult = folderBrowserDialog.ShowDialog();
			bool flag2 = dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes;
			if (flag2)
			{
				bool flag3 = string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath);
				if (flag3)
				{
					Util.ShowMessage(Resources.notSelected);
					this.btnBrowse_Click(sender, e);
				}
				else
				{
					string fullPath = Path.GetFullPath(folderBrowserDialog.SelectedPath);
					DriveInfo[] drives = DriveInfo.GetDrives();
					foreach (DriveInfo driveInfo in drives)
					{
						bool flag4 = !fullPath.Contains(driveInfo.Name);
						if (!flag4)
						{
							bool flag5 = Util.IsUnixOrMacOSX();
							bool flag6;
							if (flag5)
							{
								flag6 = driveInfo.Name.Contains("media") || driveInfo.Name.Contains("Volumes");
								flag6 = driveInfo.IsReady && (driveInfo.DriveType == DriveType.Removable || flag6);
							}
							else
							{
								flag6 = driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable;
							}
							bool flag7 = flag6;
							if (flag7)
							{
								Util.ShowMessage(Resources.doNotUseRemovable);
								this.btnBrowse_Click(sender, e);
								return;
							}
						}
					}
					this.txtBackupLocation.Text = folderBrowserDialog.SelectedPath;
					this.btnApply_Click(null, null);
				}
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0007A7F0 File Offset: 0x000789F0
		private void chkBackup_CheckedChanged(object sender, EventArgs e)
		{
			this.txtBackupLocation.Enabled = this.chkBackup.Checked;
			this.btnBrowse.Enabled = this.chkBackup.Checked;
			Util.SetRegistryValue("BackupSaves", this.chkBackup.Checked ? "true" : "false");
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0007A850 File Offset: 0x00078A50
		private void btnBackup_Click(object sender, EventArgs e)
		{
			this.pnlHome.Visible = false;
			this.pnlBackup.Visible = true;
			this.pnlHome.Location = new Point(Util.ScaleSize(-9999), Util.ScaleSize(15));
			this.pnlBackup.Location = new Point(Util.ScaleSize(257), Util.ScaleSize(15));
			bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
			if (flag)
			{
				this.btnHome.Image = Resources.home_gamelist_off;
				this.btnOptions.Image = Resources.home_settings_on;
				this.btnHelp.Image = Resources.home_help_off;
			}
			else
			{
				this.btnHome.BackgroundImage = Resources.home_gamelist_off;
				this.btnOptions.BackgroundImage = Resources.home_settings_on;
				this.btnHelp.BackgroundImage = Resources.home_help_off;
			}
			this.chkBackup.Checked = Util.GetRegistryValue("BackupSaves") != "false";
			this.txtBackupLocation.Text = Util.GetBackupLocation();
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0007A967 File Offset: 0x00078B67
		private void btnApply_Click(object sender, EventArgs e)
		{
			Util.SetRegistryValue("Location", this.txtBackupLocation.Text);
			Util.SetRegistryValue("BackupSaves", this.chkBackup.Checked ? "true" : "false");
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0007A9A4 File Offset: 0x00078BA4
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				bool flag = this.evt == null;
				if (!flag)
				{
					this.evt.Set();
					this.evt2.Set();
					Directory.Delete(Util.GetTempFolder(), true);
					bool sessionInited = this.m_sessionInited;
					if (sessionInited)
					{
						try
						{
							WebClientEx webClientEx = new WebClientEx();
							webClientEx.Credentials = Util.GetNetworkCredential();
							webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
							webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth?token=" + Util.GetAuthToken(), Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"DESTROY_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), Util.GetUID(false, false))));
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007AA84 File Offset: 0x00078C84
		private void SaveUserCheats()
		{
			string text = "<usercheats>";
			foreach (object obj in ((IEnumerable)this.dgServerGames.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = dataGridViewRow.Tag != null;
				if (flag)
				{
					game game = dataGridViewRow.Tag as game;
					bool flag2 = game == null || game.GetTargetGameFolder() == null;
					if (!flag2)
					{
						text += string.Format("<game id=\"{0}\">", Path.GetFileName(game.LocalSaveFolder));
						foreach (cheat cheat in game.GetTargetGameFolder().files._files[0].Cheats)
						{
							bool flag3 = cheat.id == "-1";
							if (flag3)
							{
								text = string.Concat(new string[] { text, "<cheat desc=\"", cheat.name, "\" comment=\"", cheat.note, "\">" });
								text += cheat.ToString(false);
								text += "</cheat>";
							}
						}
						text += "</game>";
					}
				}
			}
			text += "</usercheats>";
			File.WriteAllText(Util.GetBackupLocation() + Path.DirectorySeparatorChar.ToString() + MainForm.USER_CHEATS_FILE, text);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0007AC68 File Offset: 0x00078E68
		private bool CheckForVersion()
		{
			return true;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0007AC7C File Offset: 0x00078E7C
		private void btnRss_Click(object sender, EventArgs e)
		{
			try
			{
				RssFeed rssFeed = RssFeed.Read(string.Format("{0}/ps4/rss?token={1}", Util.GetBaseUrl(), Util.GetAuthToken()));
				RssChannel rssChannel = rssFeed.Channels[0];
				RSSForm rssform = new RSSForm(rssChannel);
				rssform.ShowDialog();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0007ACD8 File Offset: 0x00078ED8
		private void restoreFromBackupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedRows.Count == 1;
			if (flag)
			{
				game game = this.dgServerGames.SelectedRows[0].Tag as game;
				string text = string.Concat(new string[]
				{
					game.PSN_ID,
					"_",
					Path.GetFileName(Path.GetDirectoryName(game.LocalSaveFolder)),
					"_",
					Path.GetFileNameWithoutExtension(game.LocalSaveFolder),
					"_*"
				});
				string[] files = Directory.GetFiles(Util.GetBackupLocation(), text);
				bool flag2 = files.Length == 1;
				if (flag2)
				{
					RestoreBackup restoreBackup = new RestoreBackup(files[0], Path.GetDirectoryName(game.LocalSaveFolder));
					restoreBackup.ShowDialog();
					Util.ShowMessage(Resources.msgRestored);
				}
				else
				{
					bool flag3 = files.Length == 0;
					if (flag3)
					{
						Util.ShowMessage(Resources.errNoBackup);
					}
					else
					{
						ChooseBackup chooseBackup = new ChooseBackup(game.name, game.PSN_ID + "_" + Path.GetFileName(Path.GetDirectoryName(game.LocalSaveFolder)) + "_", game.LocalSaveFolder);
						chooseBackup.ShowDialog();
					}
				}
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0007AE0C File Offset: 0x0007900C
		private void btnDeactivate_Click(object sender, EventArgs e)
		{
			bool flag = Util.IsUnixOrMacOSX() && !this.btnDeactivate.Enabled;
			if (!flag)
			{
				bool flag2 = Util.ShowMessage(string.Format(Resources.msgDeactivate, Util.PRODUCT_NAME), Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
				bool flag3 = flag2;
				if (flag3)
				{
					bool flag4 = this.DeactivateLicense();
					if (flag4)
					{
						Util.ShowMessage(string.Format(Resources.msgDeactivated, Util.PRODUCT_NAME), Resources.msgInfo);
						this.m_sessionInited = false;
						Application.Restart();
					}
				}
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0007AE9C File Offset: 0x0007909C
		private bool DeactivateLicense()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
				byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"UNREGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", Util.GetUserId(), Util.GetUID(false, false))));
				string @string = Encoding.ASCII.GetString(array);
				Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag = (string)dictionary["status"] == "OK";
				if (flag)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase(), true);
					string[] valueNames = registryKey.GetValueNames();
					foreach (string text in valueNames)
					{
						bool flag2 = text != "Location";
						if (flag2)
						{
							registryKey.DeleteValue(text);
						}
					}
					return true;
				}
				bool flag3 = dictionary.ContainsKey("code");
				if (flag3)
				{
					Util.ShowErrorMessage(dictionary, Resources.errOffline);
				}
				else
				{
					Util.ShowMessage(Resources.errOffline, Resources.msgError);
				}
			}
			catch (Exception)
			{
				Util.ShowMessage(Resources.errConnection, Resources.msgError);
			}
			return false;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0007B01C File Offset: 0x0007921C
		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			Process.Start("file://" + this.txtBackupLocation.Text);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0007B03C File Offset: 0x0007923C
		private void btnHelp_Click(object sender, EventArgs e)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string text = "http://www.savewizard.net/manuals/swps4m/";
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				UseShellExecute = true,
				Verb = "open",
				FileName = text
			};
			Process.Start(processStartInfo);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000021C5 File Offset: 0x000003C5
		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0007B08C File Offset: 0x0007928C
		private string FindGGUSB()
		{
			ManagementScope managementScope = new ManagementScope("root\\cimv2");
			WqlObjectQuery wqlObjectQuery = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive where Model = 'dpdev GameGenie USB Device'");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, wqlObjectQuery);
			ManagementBaseObject[] array = new ManagementBaseObject[1];
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			bool flag = managementObjectCollection.Count > 0;
			if (flag)
			{
				managementObjectCollection.CopyTo(array, 0);
				string text = (string)array[0].Properties["DeviceID"].Value;
				text = text.Replace("\\\\", "\\\\\\\\");
				text = text.Replace(".\\", ".\\\\");
				string text2 = array[0].Properties["PNPDeviceID"].Value.ToString();
				string[] array2 = text2.Split(new char[] { '\\', '&' });
				string text3 = "ASSOCIATORS OF {Win32_DiskDrive.DeviceID=\"" + text + "\"} WHERE AssocClass = Win32_DiskDriveToDiskPartition";
				WqlObjectQuery wqlObjectQuery2 = new WqlObjectQuery(text3);
				ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher(managementScope, wqlObjectQuery2);
				managementObjectCollection = managementObjectSearcher2.Get();
				bool flag2 = managementObjectCollection.Count == 1;
				if (flag2)
				{
					managementObjectCollection.CopyTo(array, 0);
					text = (string)array[0].Properties["DeviceID"].Value;
					WqlObjectQuery wqlObjectQuery3 = new WqlObjectQuery("ASSOCIATORS OF {Win32_DiskPartition.DeviceID=\"" + text + "\"} WHERE AssocClass = Win32_LogicalDiskToPartition");
					ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher(managementScope, wqlObjectQuery3);
					managementObjectCollection = managementObjectSearcher3.Get();
					bool flag3 = managementObjectCollection.Count == 1;
					if (flag3)
					{
						managementObjectCollection.CopyTo(array, 0);
						string text4 = (string)array[0].Properties["DeviceID"].Value;
						managementObjectSearcher3.Dispose();
						managementObjectSearcher2.Dispose();
						managementObjectSearcher.Dispose();
						return array2[5];
					}
					managementObjectSearcher3.Dispose();
				}
				managementObjectSearcher2.Dispose();
			}
			managementObjectSearcher.Dispose();
			return null;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0007B270 File Offset: 0x00079470
		private void deleteSaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
			bool flag = game == null;
			string text;
			if (flag)
			{
				text = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as string;
			}
			else
			{
				text = game.LocalSaveFolder;
			}
			bool flag2 = text == null;
			if (!flag2)
			{
				bool flag3 = Util.ShowMessage(Resources.msgConfirmDeleteSave, this.Text, MessageBoxButtons.YesNo) == DialogResult.No;
				if (!flag3)
				{
					try
					{
						File.Delete(text);
						File.Delete(text.Substring(0, game.LocalSaveFolder.Length - 4));
						string directoryName = Path.GetDirectoryName(text);
						bool flag4 = Directory.GetFiles(directoryName).Length == 0;
						if (flag4)
						{
							Directory.Delete(directoryName, true);
							string fullName = Directory.GetParent(directoryName).FullName;
							bool flag5 = Directory.GetFileSystemEntries(fullName).Length == 0;
							if (flag5)
							{
								Directory.Delete(fullName);
							}
						}
					}
					catch (Exception)
					{
						Util.ShowMessage(Resources.errDelete, Resources.msgError);
					}
					int firstDisplayedScrollingRowIndex = this.dgServerGames.FirstDisplayedScrollingRowIndex;
					this.cbDrives_SelectedIndexChanged(null, null);
					bool flag6 = this.dgServerGames.Rows.Count > firstDisplayedScrollingRowIndex && firstDisplayedScrollingRowIndex >= 0;
					if (flag6)
					{
						this.dgServerGames.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
					}
				}
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000021C5 File Offset: 0x000003C5
		private void btnGamesInServer_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0007B404 File Offset: 0x00079604
		private void chkBackup_Click(object sender, EventArgs e)
		{
			bool flag = !this.chkBackup.Checked;
			if (flag)
			{
				bool flag2 = Util.ShowMessage(Resources.msgConfirmBackup, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
				bool flag3 = flag2;
				if (flag3)
				{
					this.chkBackup.Checked = true;
				}
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0007B454 File Offset: 0x00079654
		private void btnManageProfiles_Click(object sender, EventArgs e)
		{
			bool flag = Util.IsUnixOrMacOSX() && !this.btnManageProfiles.Enabled;
			if (!flag)
			{
				ManageProfiles manageProfiles = new ManageProfiles("", this.m_psnIDs);
				manageProfiles.ShowDialog();
				this.GetPSNIDInfo();
				this.cbDrives_SelectedIndexChanged(null, null);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007B4A8 File Offset: 0x000796A8
		private void registerPSNIDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.m_psnIDs.Count >= this.m_psn_quota || this.m_psn_remaining <= 0;
			if (flag)
			{
				Util.ShowMessage(Resources.errMaxProfiles, Resources.msgInfo);
			}
			else
			{
				bool flag2 = this.dgServerGames.SelectedRows.Count == 1;
				if (flag2)
				{
					game game = this.dgServerGames.SelectedRows[0].Tag as game;
					ManageProfiles manageProfiles = new ManageProfiles(game.PSN_ID, this.m_psnIDs);
					bool flag3 = manageProfiles.ShowDialog() == DialogResult.OK;
					if (flag3)
					{
						this.GetPSNIDInfo();
						this.cbDrives_SelectedIndexChanged(null, null);
					}
				}
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0007B55C File Offset: 0x0007975C
		private void resignToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgServerGames.SelectedCells.Count == 0;
			if (!flag)
			{
				game game = this.dgServerGames.Rows[this.dgServerGames.SelectedCells[0].RowIndex].Tag as game;
				bool flag2 = this.m_psnIDs.Count == 0;
				if (flag2)
				{
					Util.ShowMessage(Resources.msgNoProfiles);
				}
				else
				{
					ChooseProfile chooseProfile = new ChooseProfile(this.m_psnIDs, game.PSN_ID);
					bool flag3 = chooseProfile.ShowDialog(this) == DialogResult.OK;
					if (flag3)
					{
						string text = game.LocalSaveFolder.Replace(game.PSN_ID, chooseProfile.SelectedAccount);
						bool flag4 = File.Exists(text);
						if (flag4)
						{
						}
						ResignFilesUplaoder resignFilesUplaoder = new ResignFilesUplaoder(game, Path.GetDirectoryName(game.LocalSaveFolder), chooseProfile.SelectedAccount, new List<string>());
						bool flag5 = resignFilesUplaoder.ShowDialog(this) == DialogResult.OK;
						if (flag5)
						{
							this.cbDrives_SelectedIndexChanged(null, null);
						}
					}
				}
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0007B660 File Offset: 0x00079860
		private bool RegisterSerial()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				string registryValue = Util.GetRegistryValue("Serial");
				string text = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(registryValue)));
				text = text.Replace("-", "");
				string uid = Util.GetUID(false, true);
				bool flag = string.IsNullOrEmpty(uid);
				if (flag)
				{
					Util.ShowMessage(Resources.errContactSupport);
					return false;
				}
				string text2 = string.Format("{0}/ps4auth", Util.GetAuthBaseUrl(), uid, text);
				string text3 = webClientEx.DownloadString(new Uri(text2, UriKind.Absolute));
				bool flag2 = text3.IndexOf('#') > 0;
				if (flag2)
				{
					string[] array = text3.Split(new char[] { '#' });
					bool flag3 = array.Length > 1;
					if (flag3)
					{
						bool flag4 = array[0] == "4";
						if (flag4)
						{
							Util.ShowMessage(Resources.errInvalidSerial, Resources.msgError);
							return false;
						}
						bool flag5 = array[0] == "5";
						if (flag5)
						{
							Util.ShowMessage(Resources.errTooManyTimes, Resources.msgError);
							return false;
						}
					}
				}
				else
				{
					bool flag6 = text3 == null || text3.ToLower().Contains("error") || text3.ToLower().Contains("not found");
					if (flag6)
					{
						string text4 = text3.Replace("ERROR", "");
						bool flag7 = text4.Contains("1007");
						if (flag7)
						{
							Util.GetUID(true, true);
							return this.RegisterSerial();
						}
						bool flag8 = text4.Contains("1004");
						if (flag8)
						{
							Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME) + text4, Resources.msgError);
							return false;
						}
						bool flag9 = text4.Contains("1005");
						if (flag9)
						{
							Util.ShowMessage(Resources.errTooManyTimes + text4, Resources.msgError);
							return false;
						}
						Util.ShowMessage(Resources.errNotRegistered, Resources.msgError);
						return false;
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Util.ShowMessage(ex.Message, ex.StackTrace);
				Util.ShowMessage(Resources.errSerial, Resources.msgError);
			}
			Util.ShowMessage(string.Format(Resources.errNotRegistered, Util.PRODUCT_NAME), Resources.msgError);
			return false;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0007B8F8 File Offset: 0x00079AF8
		private void btnActivatePackage_Click(object sender, EventArgs e)
		{
			byte[] array = new WebClientEx
			{
				Credentials = Util.GetNetworkCredential()
			}.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"ADD_PACKAGE\",\"license\":\"{0}-{1}-{2}-{3}\",\"userid\":\"{4}\"}}", new object[]
			{
				this.txtSerial1.Text,
				this.txtSerial2.Text,
				this.txtSerial3.Text,
				this.txtSerial4.Text,
				Util.GetUserId()
			})));
			string @string = Encoding.ASCII.GetString(array);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize<Dictionary<string, object>>(@string);
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			if (flag)
			{
				Util.ShowMessage("Successfully activated the package. Application will restart now.");
				Application.Restart();
			}
		}

		// Token: 0x04000ADF RID: 2783
		private Dictionary<string, List<game>> m_dictLocalSaves = new Dictionary<string, List<game>>();

		// Token: 0x04000AE0 RID: 2784
		private string m_expandedGame = null;

		// Token: 0x04000AE1 RID: 2785
		internal const int WM_DEVICECHANGE = 537;

		// Token: 0x04000AE2 RID: 2786
		public const int WM_SYSCOMMAND = 274;

		// Token: 0x04000AE3 RID: 2787
		public const int MF_SEPARATOR = 2048;

		// Token: 0x04000AE4 RID: 2788
		public const int MF_BYPOSITION = 1024;

		// Token: 0x04000AE5 RID: 2789
		public const int MF_STRING = 0;

		// Token: 0x04000AE6 RID: 2790
		public const int IDM_ABOUT = 1000;

		// Token: 0x04000AE7 RID: 2791
		private Dictionary<int, string> RegionMap;

		// Token: 0x04000AE8 RID: 2792
		private const string UNREGISTER_UUID = "{{\"action\":\"UNREGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000AE9 RID: 2793
		private const string DESTROY_SESSION = "{{\"action\":\"DESTROY_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000AEA RID: 2794
		private const string SESSION_INIT_URL = "{{\"action\":\"START_SESSION\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000AEB RID: 2795
		private const string PSNID_INFO = "{{\"action\":\"PSNID_INFO\",\"userid\":\"{0}\"}}";

		// Token: 0x04000AEC RID: 2796
		private const string ACTIVATE_PACKAGE = "{{\"action\":\"ADD_PACKAGE\",\"license\":\"{0}-{1}-{2}-{3}\",\"userid\":\"{4}\"}}";

		// Token: 0x04000AED RID: 2797
		private const string SESSION_CLOSAL = "{0}/?q=software_auth2/sessionclose&sessionid={1}";

		// Token: 0x04000AEE RID: 2798
		private const int INTERNAL_VERION_MAJOR = 1;

		// Token: 0x04000AEF RID: 2799
		private const int INTERNAL_VERION_MINOR = 0;

		// Token: 0x04000AF0 RID: 2800
		private int previousDriveNum = 0;

		// Token: 0x04000AF1 RID: 2801
		private bool isRunning = false;

		// Token: 0x04000AF2 RID: 2802
		private MainForm.GetTrafficDelegate GetTrafficFunc;

		// Token: 0x04000AF3 RID: 2803
		private List<game> m_games;

		// Token: 0x04000AF4 RID: 2804
		private DrivesHelper drivesHelper;

		// Token: 0x04000AF5 RID: 2805
		public static string USER_CHEATS_FILE = "swusercheats.xml";

		// Token: 0x04000AF6 RID: 2806
		private bool m_bSerialChecked = false;

		// Token: 0x04000AF7 RID: 2807
		private bool m_sessionInited = false;

		// Token: 0x04000AF8 RID: 2808
		private AutoResetEvent evt;

		// Token: 0x04000AF9 RID: 2809
		private AutoResetEvent evt2;

		// Token: 0x04000AFA RID: 2810
		private Dictionary<string, object> m_psnIDs = null;

		// Token: 0x04000AFB RID: 2811
		private int m_psn_quota = 0;

		// Token: 0x04000AFC RID: 2812
		private int m_psn_remaining = 0;

		// Token: 0x04000AFD RID: 2813
		private bool isFirstRunning = true;

		// Token: 0x02000294 RID: 660
		// (Invoke) Token: 0x06001E31 RID: 7729
		private delegate void GetTrafficDelegate();

		// Token: 0x02000295 RID: 661
		public struct DEV_BROADCAST_HDR
		{
			// Token: 0x04000FD2 RID: 4050
			public uint dbch_Size;

			// Token: 0x04000FD3 RID: 4051
			public uint dbch_DeviceType;

			// Token: 0x04000FD4 RID: 4052
			public uint dbch_Reserved;
		}

		// Token: 0x02000296 RID: 662
		public struct DEV_BROADCAST_VOLUME
		{
			// Token: 0x04000FD5 RID: 4053
			public uint dbch_Size;

			// Token: 0x04000FD6 RID: 4054
			public uint dbch_DeviceType;

			// Token: 0x04000FD7 RID: 4055
			public uint dbch_Reserved;

			// Token: 0x04000FD8 RID: 4056
			public uint dbcv_unitmask;

			// Token: 0x04000FD9 RID: 4057
			public ushort dbcv_flags;
		}
	}
}
