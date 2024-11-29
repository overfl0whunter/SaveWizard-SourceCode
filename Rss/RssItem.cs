using System;

namespace Rss
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public class RssItem : RssElement
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x000316AC File Offset: 0x0002F8AC
		public override string ToString()
		{
			bool flag = this.title != null;
			string text;
			if (flag)
			{
				text = this.title;
			}
			else
			{
				bool flag2 = this.description != null;
				if (flag2)
				{
					text = this.description;
				}
				else
				{
					text = "RssItem";
				}
			}
			return text;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000316F0 File Offset: 0x0002F8F0
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00031708 File Offset: 0x0002F908
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00031718 File Offset: 0x0002F918
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00031730 File Offset: 0x0002F930
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

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00031740 File Offset: 0x0002F940
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00031758 File Offset: 0x0002F958
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00031768 File Offset: 0x0002F968
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00031780 File Offset: 0x0002F980
		public string Author
		{
			get
			{
				return this.author;
			}
			set
			{
				this.author = RssDefault.Check(value);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00031790 File Offset: 0x0002F990
		public RssCategoryCollection Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000317A8 File Offset: 0x0002F9A8
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x000317C0 File Offset: 0x0002F9C0
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				this.comments = RssDefault.Check(value);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000317D0 File Offset: 0x0002F9D0
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000317E8 File Offset: 0x0002F9E8
		public RssSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000317F4 File Offset: 0x0002F9F4
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0003180C File Offset: 0x0002FA0C
		public RssEnclosure Enclosure
		{
			get
			{
				return this.enclosure;
			}
			set
			{
				this.enclosure = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00031818 File Offset: 0x0002FA18
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00031830 File Offset: 0x0002FA30
		public RssGuid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0003183C File Offset: 0x0002FA3C
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00031854 File Offset: 0x0002FA54
		public DateTime PubDate
		{
			get
			{
				return this.pubDate;
			}
			set
			{
				this.pubDate = value;
			}
		}

		// Token: 0x040004EF RID: 1263
		private string title = "";

		// Token: 0x040004F0 RID: 1264
		private Uri link = RssDefault.Uri;

		// Token: 0x040004F1 RID: 1265
		private string description = "";

		// Token: 0x040004F2 RID: 1266
		private string author = "";

		// Token: 0x040004F3 RID: 1267
		private RssCategoryCollection categories = new RssCategoryCollection();

		// Token: 0x040004F4 RID: 1268
		private string comments = "";

		// Token: 0x040004F5 RID: 1269
		private RssEnclosure enclosure = null;

		// Token: 0x040004F6 RID: 1270
		private RssGuid guid = null;

		// Token: 0x040004F7 RID: 1271
		private DateTime pubDate = RssDefault.DateTime;

		// Token: 0x040004F8 RID: 1272
		private RssSource source = null;
	}
}
