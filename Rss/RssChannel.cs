using System;

namespace Rss
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class RssChannel : RssElement
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x000308A4 File Offset: 0x0002EAA4
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
					text = "RssChannel";
				}
			}
			return text;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000308E8 File Offset: 0x0002EAE8
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00030900 File Offset: 0x0002EB00
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00030910 File Offset: 0x0002EB10
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00030928 File Offset: 0x0002EB28
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

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00030938 File Offset: 0x0002EB38
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00030950 File Offset: 0x0002EB50
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00030960 File Offset: 0x0002EB60
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00030978 File Offset: 0x0002EB78
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00030988 File Offset: 0x0002EB88
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x000309A0 File Offset: 0x0002EBA0
		public RssImage Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x000309AC File Offset: 0x0002EBAC
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x000309C4 File Offset: 0x0002EBC4
		public string Copyright
		{
			get
			{
				return this.copyright;
			}
			set
			{
				this.copyright = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000309D4 File Offset: 0x0002EBD4
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x000309EC File Offset: 0x0002EBEC
		public string ManagingEditor
		{
			get
			{
				return this.managingEditor;
			}
			set
			{
				this.managingEditor = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000309FC File Offset: 0x0002EBFC
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00030A14 File Offset: 0x0002EC14
		public string WebMaster
		{
			get
			{
				return this.webMaster;
			}
			set
			{
				this.webMaster = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00030A24 File Offset: 0x0002EC24
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00030A3C File Offset: 0x0002EC3C
		public string Rating
		{
			get
			{
				return this.rating;
			}
			set
			{
				this.rating = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00030A4C File Offset: 0x0002EC4C
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00030A64 File Offset: 0x0002EC64
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

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00030A70 File Offset: 0x0002EC70
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00030A88 File Offset: 0x0002EC88
		public DateTime LastBuildDate
		{
			get
			{
				return this.lastBuildDate;
			}
			set
			{
				this.lastBuildDate = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00030A94 File Offset: 0x0002EC94
		public RssCategoryCollection Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00030AAC File Offset: 0x0002ECAC
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		public string Generator
		{
			get
			{
				return this.generator;
			}
			set
			{
				this.generator = RssDefault.Check(value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00030AD4 File Offset: 0x0002ECD4
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00030AEC File Offset: 0x0002ECEC
		public string Docs
		{
			get
			{
				return this.docs;
			}
			set
			{
				this.docs = RssDefault.Check(value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00030AFC File Offset: 0x0002ECFC
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x00030B14 File Offset: 0x0002ED14
		public RssTextInput TextInput
		{
			get
			{
				return this.textInput;
			}
			set
			{
				this.textInput = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00030B20 File Offset: 0x0002ED20
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00030B38 File Offset: 0x0002ED38
		public bool[] SkipDays
		{
			get
			{
				return this.skipDays;
			}
			set
			{
				this.skipDays = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00030B44 File Offset: 0x0002ED44
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00030B5C File Offset: 0x0002ED5C
		public bool[] SkipHours
		{
			get
			{
				return this.skipHours;
			}
			set
			{
				this.skipHours = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00030B68 File Offset: 0x0002ED68
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00030B80 File Offset: 0x0002ED80
		public RssCloud Cloud
		{
			get
			{
				return this.cloud;
			}
			set
			{
				this.cloud = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00030B8C File Offset: 0x0002ED8C
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		public int TimeToLive
		{
			get
			{
				return this.timeToLive;
			}
			set
			{
				this.timeToLive = RssDefault.Check(value);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		public RssItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x040004BE RID: 1214
		private string title = "";

		// Token: 0x040004BF RID: 1215
		private Uri link = RssDefault.Uri;

		// Token: 0x040004C0 RID: 1216
		private string description = "";

		// Token: 0x040004C1 RID: 1217
		private string language = "";

		// Token: 0x040004C2 RID: 1218
		private string copyright = "";

		// Token: 0x040004C3 RID: 1219
		private string managingEditor = "";

		// Token: 0x040004C4 RID: 1220
		private string webMaster = "";

		// Token: 0x040004C5 RID: 1221
		private DateTime pubDate = RssDefault.DateTime;

		// Token: 0x040004C6 RID: 1222
		private DateTime lastBuildDate = RssDefault.DateTime;

		// Token: 0x040004C7 RID: 1223
		private RssCategoryCollection categories = new RssCategoryCollection();

		// Token: 0x040004C8 RID: 1224
		private string generator = "";

		// Token: 0x040004C9 RID: 1225
		private string docs = "";

		// Token: 0x040004CA RID: 1226
		private RssCloud cloud = null;

		// Token: 0x040004CB RID: 1227
		private int timeToLive = -1;

		// Token: 0x040004CC RID: 1228
		private RssImage image = null;

		// Token: 0x040004CD RID: 1229
		private RssTextInput textInput = null;

		// Token: 0x040004CE RID: 1230
		private bool[] skipHours = new bool[24];

		// Token: 0x040004CF RID: 1231
		private bool[] skipDays = new bool[7];

		// Token: 0x040004D0 RID: 1232
		private string rating = "";

		// Token: 0x040004D1 RID: 1233
		private RssItemCollection items = new RssItemCollection();
	}
}
