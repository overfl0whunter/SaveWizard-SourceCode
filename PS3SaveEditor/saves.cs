using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001BA RID: 442
	[XmlRoot("saves")]
	public class saves
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0006F81C File Offset: 0x0006DA1C
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0006F824 File Offset: 0x0006DA24
		[XmlElement("save")]
		public List<save> _saves { get; set; }
	}
}
