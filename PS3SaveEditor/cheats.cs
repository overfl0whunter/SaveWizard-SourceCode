using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001C2 RID: 450
	[XmlRoot("cheats")]
	public class cheats
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00071B0B File Offset: 0x0006FD0B
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00071B13 File Offset: 0x0006FD13
		[XmlElement("cheat")]
		public List<cheat> _cheats { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00071B1C File Offset: 0x0006FD1C
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00071B24 File Offset: 0x0006FD24
		[XmlElement("group")]
		public List<group> groups { get; set; }

		// Token: 0x060016F7 RID: 5879 RVA: 0x00071B2D File Offset: 0x0006FD2D
		public cheats()
		{
			this._cheats = new List<cheat>();
			this.groups = new List<group>();
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00071B50 File Offset: 0x0006FD50
		public int TotalCheats
		{
			get
			{
				int num = this._cheats.Count;
				foreach (group group in this.groups)
				{
					num += group.TotalCheats;
				}
				return num;
			}
		}
	}
}
