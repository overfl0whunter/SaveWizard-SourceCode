using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class RssFeedCollection : CollectionBase
	{
		// Token: 0x170001D6 RID: 470
		public RssFeed this[int index]
		{
			get
			{
				return (RssFeed)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		public RssFeed this[string url]
		{
			get
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					bool flag = ((RssFeed)base.List[i]).Url == url;
					if (flag)
					{
						return this[i];
					}
				}
				return null;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000302BC File Offset: 0x0002E4BC
		public int Add(RssFeed feed)
		{
			return base.List.Add(feed);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000302DC File Offset: 0x0002E4DC
		public bool Contains(RssFeed rssFeed)
		{
			return base.List.Contains(rssFeed);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssFeed[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000302FC File Offset: 0x0002E4FC
		public int IndexOf(RssFeed rssFeed)
		{
			return base.List.IndexOf(rssFeed);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssFeed feed)
		{
			base.List.Insert(index, feed);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssFeed feed)
		{
			base.List.Remove(feed);
		}
	}
}
