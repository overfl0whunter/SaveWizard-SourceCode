using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor.Utilities
{
	// Token: 0x020001EF RID: 495
	public class DrivesHelper
	{
		// Token: 0x06001A53 RID: 6739 RVA: 0x000AC17C File Offset: 0x000AA37C
		public DrivesHelper(ComboBox cdDrives, List<game> m_games, CheckBox showAll, Panel noSaves, Button resign, Button import)
		{
			this.driveBox = cdDrives;
			this.gameList = m_games;
			this.chkShowAll = showAll;
			this.pnlNoSaves = noSaves;
			this.btnResign = resign;
			this.btnImport = import;
			this.ClearDrivesFunc = new DrivesHelper.ClearDrivesDelegate(this.ClearDrives);
			this.AddItemFunc = new DrivesHelper.AddItemDelegate(this.AddItem);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000AC1E2 File Offset: 0x000AA3E2
		public DrivesHelper(ComboBox cdDrives, List<game> m_games, CheckBox showAll, Panel noSaves)
			: this(cdDrives, m_games, showAll, noSaves, new Button(), new Button())
		{
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000AC1FB File Offset: 0x000AA3FB
		public void HandleDrive(object drive)
		{
			this.FillDrives();
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000AC208 File Offset: 0x000AA408
		public void FillDrives()
		{
			this.driveBox.Invoke(this.ClearDrivesFunc);
			string cheatsLocationFromRegistry = Util.GetCheatsLocationFromRegistry();
			bool flag = !string.IsNullOrEmpty(cheatsLocationFromRegistry);
			if (flag)
			{
				this.driveBox.Invoke(this.AddItemFunc, new object[] { cheatsLocationFromRegistry });
			}
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo driveInfo in drives)
			{
				bool flag2 = Util.IsUnixOrMacOSX();
				bool flag3;
				if (flag2)
				{
					flag3 = (driveInfo.Name.Contains("media") || driveInfo.Name.Contains("Volumes")) && Directory.Exists(driveInfo + "/PS4");
					flag3 = driveInfo.IsReady && (driveInfo.DriveType == DriveType.Removable || flag3);
				}
				else
				{
					flag3 = driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable;
				}
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = driveInfo != null;
					if (flag5)
					{
						bool flag6 = Util.CurrentPlatform == Util.Platform.Windows;
						string text;
						if (flag6)
						{
							text = driveInfo.RootDirectory.FullName;
						}
						else
						{
							text = string.Format("/{0}", Path.GetFileName(driveInfo.RootDirectory.FullName));
						}
						this.driveBox.Invoke(this.AddItemFunc, new object[] { text });
					}
					else
					{
						this.driveBox.Invoke(this.AddItemFunc, new object[1]);
					}
				}
			}
			this.driveBox.Invoke(this.AddItemFunc, new object[] { Resources.colSelect });
			this.SetCbDrivesBoxSize(true);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000AC3C0 File Offset: 0x000AA5C0
		private void SetCbDrivesBoxSize(bool useLongSize)
		{
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				this.driveBox.Location = new Point(Util.ScaleSize(65), Util.ScaleSize(2));
				this.driveBox.Width = Util.ScaleSize(165);
			}
			else if (useLongSize)
			{
				this.driveBox.Location = new Point(Util.ScaleSize(65), Util.ScaleSize(5));
				this.driveBox.Width = Util.ScaleSize(165);
			}
			else
			{
				this.driveBox.Location = new Point(Util.ScaleSize(185), Util.ScaleSize(5));
				this.driveBox.Width = Util.ScaleSize(45);
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000AC484 File Offset: 0x000AA684
		private void AddItem(string item)
		{
			bool flag = item != null;
			if (flag)
			{
				int num = this.driveBox.Items.Add(item);
				string text = item;
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
				bool flag4 = Directory.Exists(Util.GetDataPath(text)) && Directory.GetDirectories(Util.GetDataPath(text)).Length != 0;
				if (flag4)
				{
					this.pnlNoSaves.Visible = false;
					this.pnlNoSaves.SendToBack();
					bool flag5 = this.driveBox.SelectedIndex < 0;
					if (flag5)
					{
						this.driveBox.SelectedIndex = num;
					}
					else
					{
						string text2 = this.driveBox.SelectedItem as string;
						bool flag6 = !string.IsNullOrEmpty(text2);
						if (flag6)
						{
							bool flag7 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(text2);
							if (flag7)
							{
								text2 = string.Format("/Volumes{0}", text2);
							}
							else
							{
								bool flag8 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(text2);
								if (flag8)
								{
									text2 = string.Format("/media/{0}{1}", Environment.UserName, text2);
								}
							}
							bool flag9 = Directory.Exists(Util.GetDataPath(text2)) && Directory.GetDirectories(Util.GetDataPath(text2)).Length != 0;
							if (!flag9)
							{
								this.driveBox.SelectedIndex = num;
							}
						}
					}
				}
				else
				{
					bool flag10 = this.driveBox.SelectedIndex < 0;
					if (flag10)
					{
						this.pnlNoSaves.Visible = true;
						this.pnlNoSaves.BringToFront();
						this.driveBox.SelectedIndex = num;
					}
				}
				bool flag11 = !this.chkShowAll.Enabled;
				if (flag11)
				{
					this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = true));
					this.chkShowAll.Checked = false;
				}
			}
			bool flag12 = string.IsNullOrEmpty(item);
			if (flag12)
			{
				bool flag13 = this.gameList.Count > 0;
				if (flag13)
				{
					this.chkShowAll.Checked = true;
					this.btnResign.Enabled = (this.btnImport.Enabled = (this.chkShowAll.Enabled = false));
				}
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000AC724 File Offset: 0x000AA924
		private void ClearDrives()
		{
			this.driveBox.Items.Clear();
			bool flag = this.driveBox.Items.Count > 0;
			if (flag)
			{
				this.driveBox.SelectedIndex = 0;
			}
			else
			{
				bool flag2 = this.gameList.Count > 0;
				if (flag2)
				{
					this.chkShowAll.Checked = true;
					this.chkShowAll.Enabled = false;
				}
			}
		}

		// Token: 0x04000D16 RID: 3350
		private ComboBox driveBox;

		// Token: 0x04000D17 RID: 3351
		private List<game> gameList;

		// Token: 0x04000D18 RID: 3352
		private CheckBox chkShowAll;

		// Token: 0x04000D19 RID: 3353
		private Panel pnlNoSaves;

		// Token: 0x04000D1A RID: 3354
		private Button btnResign;

		// Token: 0x04000D1B RID: 3355
		private Button btnImport;

		// Token: 0x04000D1C RID: 3356
		private DrivesHelper.ClearDrivesDelegate ClearDrivesFunc;

		// Token: 0x04000D1D RID: 3357
		private DrivesHelper.AddItemDelegate AddItemFunc;

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x06001ECC RID: 7884
		private delegate void ClearDrivesDelegate();

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x06001ED0 RID: 7888
		private delegate void AddItemDelegate(string item);
	}
}
