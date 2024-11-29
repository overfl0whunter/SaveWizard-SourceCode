using System;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B3 RID: 435
	public class rbgame
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0006F720 File Offset: 0x0006D920
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0006F728 File Offset: 0x0006D928
		public string gamecode { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0006F731 File Offset: 0x0006D931
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0006F739 File Offset: 0x0006D939
		[XmlElement("containers")]
		public rbcontainers containers { get; set; }
	}
}
