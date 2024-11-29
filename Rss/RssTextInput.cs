using System;

namespace Rss
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class RssTextInput : RssElement
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00030E44 File Offset: 0x0002F044
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00030E5C File Offset: 0x0002F05C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = RssDefault.Check(value);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00030E6C File Offset: 0x0002F06C
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00030E84 File Offset: 0x0002F084
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = RssDefault.Check(value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00030E94 File Offset: 0x0002F094
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00030EAC File Offset: 0x0002F0AC
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00030EBC File Offset: 0x0002F0BC
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		public Uri Link
		{
			get
			{
				return this.link;
			}
			set
			{
				this.link = RssDefault.Check(value);
			}
		}

		// Token: 0x040004DD RID: 1245
		private string title = "";

		// Token: 0x040004DE RID: 1246
		private string description = "";

		// Token: 0x040004DF RID: 1247
		private string name = "";

		// Token: 0x040004E0 RID: 1248
		private Uri link = RssDefault.Uri;
	}
}
