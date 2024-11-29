using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B8 RID: 440
	public class containers
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x0006F775 File Offset: 0x0006D975
		public containers()
		{
			this._containers = new List<container>();
		}

		// Token: 0x04000A6B RID: 2667
		[XmlElement("container")]
		public List<container> _containers;
	}
}
