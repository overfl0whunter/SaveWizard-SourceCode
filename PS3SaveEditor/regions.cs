using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B6 RID: 438
	public class regions
	{
		// Token: 0x04000A68 RID: 2664
		[XmlElement("region")]
		public List<region> _regions;
	}
}
