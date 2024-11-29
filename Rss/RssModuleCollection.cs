using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B3 RID: 179
	public class RssModuleCollection : CollectionBase
	{
		// Token: 0x170001D9 RID: 473
		public RssModule this[int index]
		{
			get
			{
				return (RssModule)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000305F8 File Offset: 0x0002E7F8
		public int Add(RssModule rssModule)
		{
			return base.List.Add(rssModule);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00030618 File Offset: 0x0002E818
		public bool Contains(RssModule rssModule)
		{
			return base.List.Contains(rssModule);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssModule[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00030638 File Offset: 0x0002E838
		public int IndexOf(RssModule rssModule)
		{
			return base.List.IndexOf(rssModule);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssModule rssModule)
		{
			base.List.Insert(index, rssModule);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssModule rssModule)
		{
			base.List.Remove(rssModule);
		}
	}
}
