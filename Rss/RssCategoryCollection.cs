using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class RssCategoryCollection : CollectionBase
	{
		// Token: 0x170001D4 RID: 468
		public RssCategory this[int index]
		{
			get
			{
				return (RssCategory)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00030150 File Offset: 0x0002E350
		public int Add(RssCategory rssCategory)
		{
			return base.List.Add(rssCategory);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00030170 File Offset: 0x0002E370
		public bool Contains(RssCategory rssCategory)
		{
			return base.List.Contains(rssCategory);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssCategory[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00030190 File Offset: 0x0002E390
		public int IndexOf(RssCategory rssCategory)
		{
			return base.List.IndexOf(rssCategory);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssCategory rssCategory)
		{
			base.List.Insert(index, rssCategory);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssCategory rssCategory)
		{
			base.List.Remove(rssCategory);
		}
	}
}
