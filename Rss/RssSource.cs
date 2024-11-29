using System;

namespace Rss
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	public class RssSource : RssElement
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00031880 File Offset: 0x0002FA80
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00031898 File Offset: 0x0002FA98
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = RssDefault.Check(value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000318A8 File Offset: 0x0002FAA8
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x000318C0 File Offset: 0x0002FAC0
		public Uri Url
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = RssDefault.Check(value);
			}
		}

		// Token: 0x040004F9 RID: 1273
		private string name = "";

		// Token: 0x040004FA RID: 1274
		private Uri uri = RssDefault.Uri;
	}
}
