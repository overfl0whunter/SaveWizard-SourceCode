using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B5 RID: 181
	public class RssModuleItemCollectionCollection : CollectionBase
	{
		// Token: 0x170001DB RID: 475
		public RssModuleItemCollection this[int index]
		{
			get
			{
				return (RssModuleItemCollection)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00030760 File Offset: 0x0002E960
		public int Add(RssModuleItemCollection rssModuleItemCollection)
		{
			return base.List.Add(rssModuleItemCollection);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00030780 File Offset: 0x0002E980
		public bool Contains(RssModuleItemCollection rssModuleItemCollection)
		{
			return base.List.Contains(rssModuleItemCollection);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssModuleItemCollection[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000307A0 File Offset: 0x0002E9A0
		public int IndexOf(RssModuleItemCollection rssModuleItemCollection)
		{
			return base.List.IndexOf(rssModuleItemCollection);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssModuleItemCollection rssModuleItemCollection)
		{
			base.List.Insert(index, rssModuleItemCollection);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssModuleItemCollection rssModuleItemCollection)
		{
			base.List.Remove(rssModuleItemCollection);
		}
	}
}
