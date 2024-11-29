using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B4 RID: 436
	public class rbcontainers
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0006F742 File Offset: 0x0006D942
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x0006F74A File Offset: 0x0006D94A
		[XmlElement("container")]
		public List<string> container { get; set; }
	}
}
