using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001C0 RID: 448
	public class internals
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00071A50 File Offset: 0x0006FC50
		public internals()
		{
			this.files = new List<file>();
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00071A66 File Offset: 0x0006FC66
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x00071A6E File Offset: 0x0006FC6E
		[XmlElement("file")]
		public List<file> files { get; set; }

		// Token: 0x060016EF RID: 5871 RVA: 0x00071A78 File Offset: 0x0006FC78
		public static internals Copy(internals i)
		{
			internals internals = new internals();
			foreach (file file in i.files)
			{
				internals.files.Add(file.Copy(file));
			}
			return internals;
		}
	}
}
