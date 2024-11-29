using System;

namespace Rss
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class RssImage : RssElement
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00030D1C File Offset: 0x0002EF1C
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00030D34 File Offset: 0x0002EF34
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00030D44 File Offset: 0x0002EF44
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00030D5C File Offset: 0x0002EF5C
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

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00030D6C File Offset: 0x0002EF6C
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00030D84 File Offset: 0x0002EF84
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00030D94 File Offset: 0x0002EF94
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00030DAC File Offset: 0x0002EFAC
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00030DBC File Offset: 0x0002EFBC
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = RssDefault.Check(value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00030DFC File Offset: 0x0002EFFC
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = RssDefault.Check(value);
			}
		}

		// Token: 0x040004D7 RID: 1239
		private string title = "";

		// Token: 0x040004D8 RID: 1240
		private string description = "";

		// Token: 0x040004D9 RID: 1241
		private Uri uri = RssDefault.Uri;

		// Token: 0x040004DA RID: 1242
		private Uri link = RssDefault.Uri;

		// Token: 0x040004DB RID: 1243
		private int width = -1;

		// Token: 0x040004DC RID: 1244
		private int height = -1;
	}
}
