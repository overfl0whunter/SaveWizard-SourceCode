using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B4 RID: 180
	public class RssModuleItemCollection : CollectionBase
	{
		// Token: 0x170001DA RID: 474
		public RssModuleItem this[int index]
		{
			get
			{
				return (RssModuleItem)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003067C File Offset: 0x0002E87C
		public int Add(RssModuleItem rssModuleItem)
		{
			return base.List.Add(rssModuleItem);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003069C File Offset: 0x0002E89C
		public bool Contains(RssModuleItem rssModuleItem)
		{
			return base.List.Contains(rssModuleItem);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssModuleItem[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000306BC File Offset: 0x0002E8BC
		public int IndexOf(RssModuleItem rssModuleItem)
		{
			return base.List.IndexOf(rssModuleItem);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssModuleItem rssModuleItem)
		{
			base.List.Insert(index, rssModuleItem);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssModuleItem rssModuleItem)
		{
			base.List.Remove(rssModuleItem);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000306DA File Offset: 0x0002E8DA
		public void BindTo(int itemHashCode)
		{
			this._alBindTo.Add(itemHashCode);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000306F0 File Offset: 0x0002E8F0
		public bool IsBoundTo(int itemHashCode)
		{
			return this._alBindTo.BinarySearch(0, this._alBindTo.Count, itemHashCode, null) >= 0;
		}

		// Token: 0x040004BD RID: 1213
		private ArrayList _alBindTo = new ArrayList();
	}
}
