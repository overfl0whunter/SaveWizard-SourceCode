using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B5 RID: 437
	[XmlRoot("rblist", Namespace = "")]
	public class rblsit
	{
		// Token: 0x04000A67 RID: 2663
		[XmlElement("game")]
		public List<rbgame> _rbgames;
	}
}
