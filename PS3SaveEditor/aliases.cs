using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001B9 RID: 441
	public class aliases
	{
		// Token: 0x06001675 RID: 5749 RVA: 0x0006F78C File Offset: 0x0006D98C
		public static aliases Copy(aliases a)
		{
			aliases aliases = new aliases();
			bool flag = a != null && a._aliases != null;
			if (flag)
			{
				aliases._aliases = new List<alias>();
				foreach (alias alias in a._aliases)
				{
					aliases._aliases.Add(alias.Copy(alias));
				}
			}
			return aliases;
		}

		// Token: 0x04000A6C RID: 2668
		[XmlElement("alias")]
		public List<alias> _aliases;
	}
}
