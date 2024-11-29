using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001C3 RID: 451
	[XmlRoot("file")]
	public class file
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00071BBC File Offset: 0x0006FDBC
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00071BC4 File Offset: 0x0006FDC4
		public string filename { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00071BCD File Offset: 0x0006FDCD
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00071BD5 File Offset: 0x0006FDD5
		[XmlIgnore]
		public string original_filename { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00071BDE File Offset: 0x0006FDDE
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x00071BE6 File Offset: 0x0006FDE6
		public string id { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00071BEF File Offset: 0x0006FDEF
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x00071BF7 File Offset: 0x0006FDF7
		public string title { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00071C00 File Offset: 0x0006FE00
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x00071C08 File Offset: 0x0006FE08
		public string dependency { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00071C11 File Offset: 0x0006FE11
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x00071C19 File Offset: 0x0006FE19
		public string Option { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00071C22 File Offset: 0x0006FE22
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x00071C2A File Offset: 0x0006FE2A
		public string altname { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00071C33 File Offset: 0x0006FE33
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x00071C3B File Offset: 0x0006FE3B
		public string ucfilename { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00071C44 File Offset: 0x0006FE44
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00071C4C File Offset: 0x0006FE4C
		public cheats cheats { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00071C58 File Offset: 0x0006FE58
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x00071C75 File Offset: 0x0006FE75
		[XmlIgnore]
		public List<cheat> Cheats
		{
			get
			{
				return this.cheats._cheats;
			}
			set
			{
				this.cheats._cheats = value;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00071C88 File Offset: 0x0006FE88
		public List<cheat> GetAllCheats()
		{
			List<cheat> list = new List<cheat>();
			list.AddRange(this.Cheats);
			foreach (group group in this.groups)
			{
				list.AddRange(group.GetGroupCheats());
			}
			return list;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00071D00 File Offset: 0x0006FF00
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x00071D1D File Offset: 0x0006FF1D
		[XmlIgnore]
		public List<group> groups
		{
			get
			{
				return this.cheats.groups;
			}
			set
			{
				this.cheats.groups = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00071D30 File Offset: 0x0006FF30
		public int TotalCheats
		{
			get
			{
				return this.cheats.TotalCheats;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00071D50 File Offset: 0x0006FF50
		public string VisibleFileName
		{
			get
			{
				string text = null;
				bool flag = !string.IsNullOrEmpty(this.altname);
				if (flag)
				{
					text = this.altname;
				}
				bool flag2 = !string.IsNullOrEmpty(this.altname) && this.original_filename != null && this.filename != this.original_filename;
				if (flag2)
				{
					bool flag3 = Util.IsMatch(this.filename, this.original_filename);
					if (flag3)
					{
						Match match = Regex.Match(this.filename, this.original_filename);
						bool flag4 = match.Groups != null;
						if (flag4)
						{
							bool flag5 = match.Groups.Count > 1;
							if (flag5)
							{
								text = text.Replace("${1}", match.Groups[1].Value);
							}
							bool flag6 = match.Groups.Count > 2;
							if (flag6)
							{
								text = text.Replace("${2}", match.Groups[2].Value);
							}
						}
					}
				}
				bool flag7 = !string.IsNullOrEmpty(text);
				string text2;
				if (flag7)
				{
					text2 = string.Format("{0} ({1})", text, this.filename);
				}
				else
				{
					text2 = this.filename;
				}
				return text2;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00071E88 File Offset: 0x00070088
		public int TextMode
		{
			get
			{
				string text = this.textmode;
				int num;
				if (text != null)
				{
					if (text == null || text.Length != 0)
					{
						if (!(text == "utf-8"))
						{
							if (!(text == "ascii"))
							{
								if (!(text == "utf-16"))
								{
									num = 0;
								}
								else
								{
									num = 3;
								}
							}
							else
							{
								num = 2;
							}
						}
						else
						{
							num = 1;
						}
					}
					else
					{
						num = 0;
					}
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00071EF0 File Offset: 0x000700F0
		public bool IsHidden
		{
			get
			{
				return this.type == "hidden";
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00071F12 File Offset: 0x00070112
		public file()
		{
			this.cheats = new cheats();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00071F28 File Offset: 0x00070128
		public string GetDependencyFile(container gameFolder, string folder)
		{
			bool flag = string.IsNullOrEmpty(this.dependency);
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				foreach (file file in gameFolder.files._files)
				{
					bool flag2 = file.id == this.dependency;
					if (flag2)
					{
						string text2 = file.GetSaveFile(folder);
						bool flag3 = text2 == null;
						if (flag3)
						{
							foreach (file file2 in gameFolder.files._files)
							{
								bool flag4 = file2.id == file.dependency;
								if (flag4)
								{
									text2 = file2.filename;
								}
							}
						}
						bool flag5 = text2 != null;
						if (flag5)
						{
							return Path.Combine(folder, text2);
						}
					}
				}
				text = null;
			}
			return text;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00072050 File Offset: 0x00070250
		internal static file Copy(file gameFile)
		{
			file file = new file();
			file.original_filename = gameFile.original_filename;
			file.filename = gameFile.filename;
			file.dependency = gameFile.dependency;
			file.title = gameFile.title;
			file.id = gameFile.id;
			file.Option = gameFile.Option;
			file.altname = gameFile.altname;
			bool flag = gameFile.internals != null;
			if (flag)
			{
				file.internals = new internals();
				foreach (file file2 in gameFile.internals.files)
				{
					file.internals.files.Add(file.Copy(file2));
				}
			}
			file.cheats = new cheats();
			foreach (group group in gameFile.groups)
			{
				file.groups.Add(group.Copy(group));
			}
			file.textmode = gameFile.textmode;
			file.type = gameFile.type;
			file.ucfilename = gameFile.ucfilename;
			foreach (cheat cheat in gameFile.Cheats)
			{
				file.Cheats.Add(cheat.Copy(cheat));
			}
			return file;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00072214 File Offset: 0x00070414
		internal string GetSaveFile(string saveFolder)
		{
			string[] files = Directory.GetFiles(saveFolder, this.filename);
			bool flag = files.Length != 0;
			string text;
			if (flag)
			{
				text = Path.GetFileName(files[0]);
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00072248 File Offset: 0x00070448
		internal List<string> GetSaveFiles(string saveFolder)
		{
			string[] files = Directory.GetFiles(saveFolder, this.filename);
			bool flag = files.Length != 0;
			List<string> list2;
			if (flag)
			{
				List<string> list = new List<string>(files);
				list.Sort();
				list2 = list;
			}
			else
			{
				list2 = null;
			}
			return list2;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00072284 File Offset: 0x00070484
		internal static file GetGameFile(container gameFolder, string folder, string file)
		{
			foreach (file file2 in gameFolder.files._files)
			{
				bool flag = file2.filename == file || Util.IsMatch(file, file2.filename);
				if (flag)
				{
					return file2;
				}
			}
			return null;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00072304 File Offset: 0x00070504
		internal cheat GetCheat(string cd)
		{
			foreach (cheat cheat in this.Cheats)
			{
				bool flag = cd == cheat.name;
				if (flag)
				{
					return cheat;
				}
			}
			foreach (group group in this.groups)
			{
				cheat cheat2 = group.GetCheat(cd);
				bool flag2 = cheat2 != null;
				if (flag2)
				{
					return cheat2;
				}
			}
			return null;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000723CC File Offset: 0x000705CC
		public file GetParent(container gamefolder)
		{
			foreach (file file in gamefolder.files._files)
			{
				bool flag = file.id == this.id;
				if (flag)
				{
					return null;
				}
				bool flag2 = file.internals != null;
				if (flag2)
				{
					foreach (file file2 in file.internals.files)
					{
						bool flag3 = file2.id == this.id;
						if (flag3)
						{
							return file;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000AA7 RID: 2727
		public internals internals;

		// Token: 0x04000AA8 RID: 2728
		public string type;

		// Token: 0x04000AA9 RID: 2729
		public string textmode;
	}
}
