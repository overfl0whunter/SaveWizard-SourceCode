using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B2 RID: 434
	[XmlRoot("games", Namespace = "")]
	public class games
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0006F70F File Offset: 0x0006D90F
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x0006F717 File Offset: 0x0006D917
		[XmlElement("rblist")]
		public rblsit rblist { get; set; }

		// Token: 0x04000A60 RID: 2656
		[XmlElement("regions")]
		public regions regions;

		// Token: 0x04000A61 RID: 2657
		[XmlElement("game")]
		public List<game> _games;

		// Token: 0x04000A62 RID: 2658
		[XmlElement("saves")]
		public saves _saves;
	}
}
