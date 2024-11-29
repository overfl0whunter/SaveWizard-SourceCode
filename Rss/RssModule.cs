using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public abstract class RssModule
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x000318D0 File Offset: 0x0002FAD0
		public RssModule()
		{
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0003191C File Offset: 0x0002FB1C
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00031934 File Offset: 0x0002FB34
		internal RssModuleItemCollection ChannelExtensions
		{
			get
			{
				return this._rssChannelExtensions;
			}
			set
			{
				this._rssChannelExtensions = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00031940 File Offset: 0x0002FB40
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00031958 File Offset: 0x0002FB58
		internal RssModuleItemCollectionCollection ItemExtensions
		{
			get
			{
				return this._rssItemExtensions;
			}
			set
			{
				this._rssItemExtensions = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00031964 File Offset: 0x0002FB64
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0003197C File Offset: 0x0002FB7C
		public string NamespacePrefix
		{
			get
			{
				return this._sNamespacePrefix;
			}
			set
			{
				this._sNamespacePrefix = RssDefault.Check(value);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0003198C File Offset: 0x0002FB8C
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000319A4 File Offset: 0x0002FBA4
		public Uri NamespaceURL
		{
			get
			{
				return this._uriNamespaceURL;
			}
			set
			{
				this._uriNamespaceURL = RssDefault.Check(value);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000319B3 File Offset: 0x0002FBB3
		public void BindTo(int channelHashCode)
		{
			this._alBindTo.Add(channelHashCode);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000319C8 File Offset: 0x0002FBC8
		public bool IsBoundTo(int channelHashCode)
		{
			return this._alBindTo.BinarySearch(0, this._alBindTo.Count, channelHashCode, null) >= 0;
		}

		// Token: 0x040004FB RID: 1275
		private ArrayList _alBindTo = new ArrayList();

		// Token: 0x040004FC RID: 1276
		private RssModuleItemCollection _rssChannelExtensions = new RssModuleItemCollection();

		// Token: 0x040004FD RID: 1277
		private RssModuleItemCollectionCollection _rssItemExtensions = new RssModuleItemCollectionCollection();

		// Token: 0x040004FE RID: 1278
		private string _sNamespacePrefix = "";

		// Token: 0x040004FF RID: 1279
		private Uri _uriNamespaceURL = RssDefault.Uri;
	}
}
