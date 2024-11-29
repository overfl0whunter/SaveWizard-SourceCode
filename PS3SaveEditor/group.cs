using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001BF RID: 447
	public class group
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x000713A4 File Offset: 0x0006F5A4
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x000713AC File Offset: 0x0006F5AC
		public string name { get; set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x000713B5 File Offset: 0x0006F5B5
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x000713BD File Offset: 0x0006F5BD
		public string note { get; set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000713C6 File Offset: 0x0006F5C6
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x000713CE File Offset: 0x0006F5CE
		public string options { get; set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000713D7 File Offset: 0x0006F5D7
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x000713DF File Offset: 0x0006F5DF
		public string type { get; set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000713E8 File Offset: 0x0006F5E8
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x000713F0 File Offset: 0x0006F5F0
		[XmlElement("cheat")]
		public List<cheat> cheats { get; set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x000713F9 File Offset: 0x0006F5F9
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00071401 File Offset: 0x0006F601
		[XmlElement(ElementName = "group")]
		public List<group> _group { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0007140C File Offset: 0x0006F60C
		public int TotalCheats
		{
			get
			{
				int num = 0;
				bool flag = this.cheats != null;
				if (flag)
				{
					num = this.cheats.Count;
				}
				bool flag2 = this._group != null;
				if (flag2)
				{
					foreach (group group in this._group)
					{
						num += group.TotalCheats;
					}
				}
				return num;
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00071498 File Offset: 0x0006F698
		public List<cheat> GetAllCheats()
		{
			List<cheat> list = new List<cheat>();
			bool flag = this._group != null;
			if (flag)
			{
				foreach (group group in this._group)
				{
					list.AddRange(group.cheats);
				}
			}
			list.AddRange(this.cheats);
			return list;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00071520 File Offset: 0x0006F720
		public group()
		{
			this.cheats = new List<cheat>();
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00071538 File Offset: 0x0006F738
		internal static group Copy(group g)
		{
			group group = new group();
			group.name = g.name;
			group.note = g.note;
			group.options = g.options;
			group.type = g.type;
			bool flag = g._group != null;
			if (flag)
			{
				group._group = new List<group>();
				foreach (group group2 in g._group)
				{
					group._group.Add(group.Copy(group2));
				}
			}
			foreach (cheat cheat in g.cheats)
			{
				group.cheats.Add(cheat.Copy(cheat));
			}
			return group;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00071648 File Offset: 0x0006F848
		public cheat GetCheat(string cd)
		{
			foreach (cheat cheat in this.cheats)
			{
				bool flag = cd == cheat.name;
				if (flag)
				{
					return cheat;
				}
			}
			bool flag2 = this._group != null;
			if (flag2)
			{
				foreach (group group in this._group)
				{
					cheat cheat2 = group.GetCheat(cd);
					bool flag3 = cheat2 != null;
					if (flag3)
					{
						return cheat2;
					}
				}
			}
			return null;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00071720 File Offset: 0x0006F920
		internal int GetCheatsCount()
		{
			int count = this.cheats.Count;
			bool flag = this._group != null;
			if (flag)
			{
				using (List<group>.Enumerator enumerator = this._group.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						group group = enumerator.Current;
						return count + group.GetCheatsCount();
					}
				}
			}
			return count;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000717A0 File Offset: 0x0006F9A0
		internal List<cheat> GetCheats()
		{
			List<cheat> list = new List<cheat>();
			list.AddRange(list);
			bool flag = this._group != null;
			if (flag)
			{
				foreach (group group in this._group)
				{
					list.AddRange(group.GetCheats());
				}
			}
			return list;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00071820 File Offset: 0x0006FA20
		public bool CheatsSelected
		{
			get
			{
				foreach (cheat cheat in this.cheats)
				{
					bool selected = cheat.Selected;
					if (selected)
					{
						return true;
					}
				}
				bool flag = this._group != null;
				if (flag)
				{
					foreach (group group in this._group)
					{
						bool cheatsSelected = group.CheatsSelected;
						if (cheatsSelected)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000718E8 File Offset: 0x0006FAE8
		public string SelectedCheats
		{
			get
			{
				string text = "";
				foreach (cheat cheat in this.cheats)
				{
					bool selected = cheat.Selected;
					if (selected)
					{
						text = text + "<id>" + cheat.id + "</id>";
					}
				}
				bool flag = this._group != null;
				if (flag)
				{
					foreach (group group in this._group)
					{
						text += group.SelectedCheats;
					}
				}
				return text;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000719C8 File Offset: 0x0006FBC8
		internal List<cheat> GetGroupCheats()
		{
			List<cheat> list = new List<cheat>();
			list.AddRange(this.cheats);
			bool flag = this._group != null;
			if (flag)
			{
				foreach (group group in this._group)
				{
					list.AddRange(group.GetGroupCheats());
				}
			}
			return list;
		}
	}
}
