using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001C1 RID: 449
	[XmlRoot("files")]
	public class files
	{
		// Token: 0x060016F0 RID: 5872 RVA: 0x00071AE4 File Offset: 0x0006FCE4
		public files()
		{
			this._files = new List<file>();
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00071AFA File Offset: 0x0006FCFA
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00071B02 File Offset: 0x0006FD02
		[XmlElement("file")]
		public List<file> _files { get; set; }
	}
}
