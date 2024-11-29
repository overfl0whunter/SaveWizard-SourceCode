using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Ionic.Zip;

namespace PS3SaveEditor
{
	// Token: 0x020001BC RID: 444
	[XmlRoot("game", Namespace = "")]
	public class game
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0006F939 File Offset: 0x0006DB39
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x0006F941 File Offset: 0x0006DB41
		public string id { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0006F94A File Offset: 0x0006DB4A
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x0006F952 File Offset: 0x0006DB52
		public int acts { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0006F95B File Offset: 0x0006DB5B
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x0006F963 File Offset: 0x0006DB63
		public string notes { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0006F96C File Offset: 0x0006DB6C
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x0006F974 File Offset: 0x0006DB74
		public string diskcode { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0006F97D File Offset: 0x0006DB7D
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x0006F985 File Offset: 0x0006DB85
		public string aliasid { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0006F98E File Offset: 0x0006DB8E
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x0006F996 File Offset: 0x0006DB96
		public string name { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0006F99F File Offset: 0x0006DB9F
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x0006F9A7 File Offset: 0x0006DBA7
		public string version { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0006F9B0 File Offset: 0x0006DBB0
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0006F9B8 File Offset: 0x0006DBB8
		public aliases aliases { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0006F9C1 File Offset: 0x0006DBC1
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0006F9C9 File Offset: 0x0006DBC9
		public containers containers { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0006F9D2 File Offset: 0x0006DBD2
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x0006F9DA File Offset: 0x0006DBDA
		public int region { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0006F9E3 File Offset: 0x0006DBE3
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0006F9EB File Offset: 0x0006DBEB
		public string Client { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0006F9F4 File Offset: 0x0006DBF4
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x0006F9FC File Offset: 0x0006DBFC
		public long updated { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0006FA05 File Offset: 0x0006DC05
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x0006FA0D File Offset: 0x0006DC0D
		public string LocalSaveFolder { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0006FA16 File Offset: 0x0006DC16
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x0006FA1E File Offset: 0x0006DC1E
		[XmlIgnore]
		public ZipEntry PFSZipEntry { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0006FA27 File Offset: 0x0006DC27
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0006FA2F File Offset: 0x0006DC2F
		[XmlIgnore]
		public ZipEntry BinZipEntry { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0006FA38 File Offset: 0x0006DC38
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0006FA40 File Offset: 0x0006DC40
		[XmlIgnore]
		public ZipFile ZipFile { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0006FA49 File Offset: 0x0006DC49
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x0006FA51 File Offset: 0x0006DC51
		[XmlIgnore]
		public DateTime UpdatedTime { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0006FA5C File Offset: 0x0006DC5C
		public string PSN_ID
		{
			get
			{
				try
				{
					bool flag = this.LocalSaveFolder != null;
					if (flag)
					{
						return Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(this.LocalSaveFolder)));
					}
				}
				catch
				{
				}
				return null;
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
		public game()
		{
			this.containers = new containers();
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0006FAC8 File Offset: 0x0006DCC8
		public override string ToString()
		{
			return this.ToString(false, this.GetSaveFiles());
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0006FAE8 File Offset: 0x0006DCE8
		public int GetCheatsCount()
		{
			int num = 0;
			foreach (container container in this.containers._containers)
			{
				num += container.GetCheatsCount();
			}
			return num;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0006FB50 File Offset: 0x0006DD50
		public string ToString(List<string> selectedSaveFiles, string mode = "decrypt")
		{
			container targetGameFolder = this.GetTargetGameFolder();
			List<string> containerFiles = this.GetContainerFiles();
			string text = string.Format("<game id=\"{0}\" mode=\"{1}\"><key><name>{2}</name></key><pfs><name>{3}</name></pfs><files>", new object[]
			{
				this.id,
				mode,
				Path.GetFileName(containerFiles[0]),
				Path.GetFileName(containerFiles[1])
			});
			List<string> list = new List<string>();
			foreach (string text2 in selectedSaveFiles)
			{
				list.Add(Path.GetFileName(text2));
				bool flag = mode == "encrypt";
				if (flag)
				{
					text = text + "<file><name>" + Path.GetFileName(text2).Replace("_file_", "") + "</name></file>";
				}
				else
				{
					text = text + "<file><name>" + Path.GetFileName(text2) + "</name></file>";
				}
			}
			bool flag2 = targetGameFolder != null;
			if (flag2)
			{
			}
			return text += "</files></game>";
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0006FC74 File Offset: 0x0006DE74
		public string ToString(bool bSelectedCheatFilesOnly, List<string> lstSaveFiles)
		{
			container targetGameFolder = this.GetTargetGameFolder();
			List<string> containerFiles = this.GetContainerFiles();
			string text = string.Format("<game id=\"{0}\" mode=\"{1}\"><key><name>{2}</name></key><pfs><name>{3}</name></pfs><files>", new object[]
			{
				this.id,
				"patch",
				Path.GetFileName(containerFiles[0]),
				Path.GetFileName(containerFiles[1])
			});
			List<string> saveFiles = this.GetSaveFiles();
			bool flag = targetGameFolder != null;
			if (flag)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (string text2 in lstSaveFiles)
				{
					file gameFile = file.GetGameFile(targetGameFolder, this.LocalSaveFolder, text2);
					bool flag2 = gameFile == null;
					if (!flag2)
					{
						bool flag3 = false;
						bool flag4 = !bSelectedCheatFilesOnly;
						if (flag4)
						{
							flag3 = true;
						}
						else
						{
							for (int i = 0; i < gameFile.Cheats.Count; i++)
							{
								bool selected = gameFile.Cheats[i].Selected;
								if (selected)
								{
									flag3 = true;
								}
							}
							bool flag5 = gameFile.groups != null;
							if (flag5)
							{
								foreach (group group in gameFile.groups)
								{
									bool cheatsSelected = group.CheatsSelected;
									if (cheatsSelected)
									{
										flag3 = true;
									}
								}
							}
						}
						bool flag6 = flag3;
						if (flag6)
						{
							string text3 = text2;
							bool flag7 = dictionary.ContainsKey(text3);
							if (flag7)
							{
								text = text.Replace(string.Concat(new string[] { "<file><fileid>", gameFile.id, "</fileid><name>", text3, "</name></file>" }), "");
								dictionary.Remove(text3);
							}
							bool flag8 = !dictionary.ContainsKey(text3) && gameFile.GetParent(targetGameFolder) == null;
							if (flag8)
							{
								text += "<file>";
								text = text + "<name>" + text3 + "</name>";
								dictionary.Add(text3, gameFile.id);
								bool flag9 = gameFile.GetAllCheats().Count > 0;
								if (flag9)
								{
									text += "<cheats>";
									foreach (cheat cheat in gameFile.Cheats)
									{
										bool selected2 = cheat.Selected;
										if (selected2)
										{
											text += cheat.ToString(targetGameFolder.quickmode > 0);
										}
									}
									bool flag10 = gameFile.groups != null;
									if (flag10)
									{
										foreach (group group2 in gameFile.groups)
										{
											text += group2.SelectedCheats;
										}
									}
									text += "</cheats>";
								}
								text += "</file>";
							}
							bool flag11 = gameFile.GetParent(targetGameFolder) != null;
							if (flag11)
							{
								file parent = gameFile.GetParent(targetGameFolder);
								bool flag12 = parent.internals != null;
								if (flag12)
								{
									foreach (file file in parent.internals.files)
									{
										bool flag13 = !dictionary.ContainsValue(file.id);
										if (flag13)
										{
											bool flag14 = text2.IndexOf(file.filename) > 0;
											if (flag14)
											{
												text += "<file>";
												text = text + "<fileid>" + gameFile.id + "</fileid>";
												text = text + "<name>" + Path.GetFileName(text3) + "</name>";
												dictionary.Add(Path.GetFileName(text3), gameFile.id);
												bool flag15 = gameFile.Cheats.Count > 0;
												if (flag15)
												{
													text += "<cheats>";
													foreach (cheat cheat2 in gameFile.Cheats)
													{
														text += cheat2.ToString(targetGameFolder.quickmode > 0);
													}
													text += "</cheats>";
												}
												text += "</file>";
											}
											else
											{
												string text4 = Path.Combine(this.LocalSaveFolder, file.filename);
												text = text + "<file><fileid>" + file.id + "</fileid>";
												text = text + "<name>" + file.filename + "</name></file>";
												dictionary.Add(Path.GetFileName(text4), file.id);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			text = text.Replace("<cheats></cheats>", "");
			return text += "</files></game>";
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0007026C File Offset: 0x0006E46C
		public List<string> GetContainerFiles()
		{
			bool flag = !Directory.Exists(Path.GetDirectoryName(this.LocalSaveFolder));
			List<string> list;
			if (flag)
			{
				list = null;
			}
			else
			{
				List<string> list2 = new List<string>();
				container targetGameFolder = this.GetTargetGameFolder();
				list2.Add(this.LocalSaveFolder);
				list2.Add(this.LocalSaveFolder.Substring(0, this.LocalSaveFolder.Length - 4));
				list = list2;
			}
			return list;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000702D4 File Offset: 0x0006E4D4
		public container GetTargetGameFolder()
		{
			container container = null;
			bool flag = !Directory.Exists(Path.GetDirectoryName(this.LocalSaveFolder));
			container container2;
			if (flag)
			{
				container2 = null;
			}
			else
			{
				foreach (container container3 in this.containers._containers)
				{
					bool flag2 = Path.GetFileNameWithoutExtension(this.LocalSaveFolder) == container3.pfs || Util.IsMatch(Path.GetFileNameWithoutExtension(this.LocalSaveFolder), container3.pfs);
					if (flag2)
					{
						bool flag3 = File.Exists(this.LocalSaveFolder);
						bool flag4 = flag3;
						if (flag4)
						{
							container = container3;
							break;
						}
					}
				}
				container2 = container;
			}
			return container2;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000703A8 File Offset: 0x0006E5A8
		internal static game Copy(game gameItem)
		{
			game game = new game();
			game.id = gameItem.id;
			game.notes = gameItem.notes;
			game.name = gameItem.name;
			game.acts = gameItem.acts;
			game.diskcode = gameItem.diskcode;
			game.aliasid = gameItem.aliasid;
			game.updated = gameItem.updated;
			game.version = gameItem.version;
			game.region = gameItem.region;
			bool flag = gameItem.aliases != null;
			if (flag)
			{
				game.aliases = aliases.Copy(gameItem.aliases);
			}
			foreach (container container in gameItem.containers._containers)
			{
				game.containers._containers.Add(container.Copy(container));
			}
			game.Client = gameItem.Client;
			game.LocalCheatExists = gameItem.LocalCheatExists;
			game.LocalSaveFolder = gameItem.LocalSaveFolder;
			return game;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000704DC File Offset: 0x0006E6DC
		internal int GetCheatCount()
		{
			int num = 0;
			foreach (container container in this.containers._containers)
			{
				bool flag = container != null;
				if (flag)
				{
					foreach (file file in container.files._files)
					{
						num += file.Cheats.Count;
						bool flag2 = file.internals != null;
						if (flag2)
						{
							foreach (file file2 in file.internals.files)
							{
								num += file2.TotalCheats;
							}
						}
						foreach (group group in file.groups)
						{
							num += group.TotalCheats;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00070688 File Offset: 0x0006E888
		internal List<cheat> GetAllCheats()
		{
			List<cheat> list = new List<cheat>();
			foreach (container container in this.containers._containers)
			{
				foreach (file file in container.files._files)
				{
					list.AddRange(file.Cheats);
					bool flag = file.internals != null;
					if (flag)
					{
						foreach (file file2 in file.internals.files)
						{
							list.AddRange(file2.Cheats);
						}
					}
					foreach (group group in file.groups)
					{
						list.AddRange(group.GetAllCheats());
					}
				}
			}
			return list;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00070830 File Offset: 0x0006EA30
		internal List<string> GetSaveFiles()
		{
			return this.GetSaveFiles(false);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0007084C File Offset: 0x0006EA4C
		internal List<string> GetSaveFiles(bool bOnlySelectedCheats)
		{
			List<string> list = new List<string>();
			container targetGameFolder = this.GetTargetGameFolder();
			bool flag = false;
			bool flag2 = targetGameFolder != null;
			if (flag2)
			{
				foreach (file file in targetGameFolder.files._files)
				{
					list.Add(file.filename);
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000708E0 File Offset: 0x0006EAE0
		internal List<cheat> GetCheats(string saveFolder, string savefile)
		{
			List<cheat> list = new List<cheat>();
			foreach (container container in this.containers._containers)
			{
				string[] files = Directory.GetFiles(Path.GetDirectoryName(saveFolder));
				List<string> list2 = new List<string>();
				foreach (string text in files)
				{
					bool flag = Path.GetFileName(text) == container.pfs || Util.IsMatch(Path.GetFileName(text), container.pfs);
					if (flag)
					{
						list2.Add(text);
					}
				}
				bool flag2 = files.Length != 0 && list2.IndexOf(saveFolder) >= 0;
				if (flag2)
				{
					foreach (file file in container.files._files)
					{
						string[] array2 = Directory.GetFiles(Util.GetTempFolder(), "*");
						List<string> list3 = new List<string>();
						foreach (string text2 in array2)
						{
							bool flag3 = text2 == file.filename || Util.IsMatch(text2, file.filename);
							if (flag3)
							{
								list3.Add(text2);
							}
						}
						array2 = list3.ToArray();
						foreach (string text3 in array2)
						{
							bool flag4 = savefile == Path.GetFileName(text3) && (file.filename == Path.GetFileName(text3) || Util.IsMatch(Path.GetFileName(savefile), file.filename));
							if (flag4)
							{
								list.AddRange(file.Cheats);
								foreach (group group in file.groups)
								{
									List<cheat> cheats = group.GetCheats();
									bool flag5 = cheats != null;
									if (flag5)
									{
										list.AddRange(cheats);
									}
								}
								return list;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00070B98 File Offset: 0x0006ED98
		internal file GetGameFile(container folder, string savefile)
		{
			bool flag = savefile == null;
			file file;
			if (flag)
			{
				file = folder.files._files[0];
			}
			else
			{
				foreach (file file2 in folder.files._files)
				{
					bool flag2 = savefile == file2.filename || Util.IsMatch(savefile, file2.filename);
					if (flag2)
					{
						return file2;
					}
				}
				foreach (file file3 in folder.files._files)
				{
					string[] files = Directory.GetFiles(Util.GetTempFolder(), "*");
					foreach (string text in files)
					{
						bool flag3 = Path.GetFileName(text) == file3.filename || Util.IsMatch(Path.GetFileName(text), file3.filename);
						if (flag3)
						{
							return file3;
						}
					}
				}
				file = null;
			}
			return file;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00070CF0 File Offset: 0x0006EEF0
		internal bool IsAlias(string gameCode, out string saveId)
		{
			bool flag = this.aliases != null;
			if (flag)
			{
				foreach (alias alias in this.aliases._aliases)
				{
					bool flag2 = gameCode.IndexOf(alias.id) >= 0;
					if (flag2)
					{
						saveId = alias.id;
						return true;
					}
				}
			}
			saveId = null;
			return false;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00070D84 File Offset: 0x0006EF84
		internal bool IsSupported(Dictionary<string, List<game>> m_dictLocalSaves, out string saveID)
		{
			bool flag = this.aliases != null;
			if (flag)
			{
				foreach (alias alias in this.aliases._aliases)
				{
					bool flag2 = m_dictLocalSaves.ContainsKey(alias.id);
					if (flag2)
					{
						saveID = alias.id;
						return true;
					}
				}
			}
			saveID = null;
			return false;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00070E10 File Offset: 0x0006F010
		internal List<alias> GetAllAliases(bool bAsc = true, bool distinct = false)
		{
			List<alias> list = new List<alias>();
			list.Add(new alias
			{
				id = this.id,
				name = this.name,
				region = this.region,
				acts = this.acts,
				diskcode = this.diskcode
			});
			List<string> list2 = new List<string>();
			list2.Add(this.name);
			bool flag = this.aliases != null && this.aliases._aliases != null && this.aliases._aliases.Count > 0;
			if (flag)
			{
				bool flag2 = !distinct;
				if (flag2)
				{
					list.AddRange(this.aliases._aliases);
				}
				else
				{
					foreach (alias alias in this.aliases._aliases)
					{
						bool flag3 = list2.IndexOf(alias.name) < 0;
						if (flag3)
						{
							list.Add(alias);
							list2.Add(alias.name);
						}
					}
				}
			}
			list.Sort((alias a1, alias a2) => a1.id.CompareTo(a2.id));
			bool flag4 = !bAsc;
			if (flag4)
			{
				list.Reverse();
			}
			return list;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00070F90 File Offset: 0x0006F190
		internal cheat GetCheat(string id, string title)
		{
			foreach (container container in this.containers._containers)
			{
				foreach (file file in container.files._files)
				{
					foreach (cheat cheat in file.GetAllCheats())
					{
						bool flag = cheat.id == id && cheat.name == title;
						if (flag)
						{
							return cheat;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000A82 RID: 2690
		public bool LocalCheatExists;
	}
}
