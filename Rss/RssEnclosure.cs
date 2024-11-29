using System;

namespace Rss
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public class RssEnclosure : RssElement
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00031550 File Offset: 0x0002F750
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00031568 File Offset: 0x0002F768
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00031578 File Offset: 0x0002F778
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00031590 File Offset: 0x0002F790
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = RssDefault.Check(value);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000315A0 File Offset: 0x0002F7A0
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x000315B8 File Offset: 0x0002F7B8
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = RssDefault.Check(value);
			}
		}

		// Token: 0x040004EA RID: 1258
		private Uri uri = RssDefault.Uri;

		// Token: 0x040004EB RID: 1259
		private int length = -1;

		// Token: 0x040004EC RID: 1260
		private string type = "";
	}
}
