using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B2 RID: 178
	public class RssItemCollection : CollectionBase
	{
		// Token: 0x170001D8 RID: 472
		public RssItem this[int index]
		{
			get
			{
				return (RssItem)base.List[index];
			}
			set
			{
				this.pubDateChanged = true;
				base.List[index] = value;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00030358 File Offset: 0x0002E558
		public int Add(RssItem item)
		{
			this.pubDateChanged = true;
			return base.List.Add(item);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00030380 File Offset: 0x0002E580
		public bool Contains(RssItem rssItem)
		{
			return base.List.Contains(rssItem);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssItem[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000303A0 File Offset: 0x0002E5A0
		public int IndexOf(RssItem rssItem)
		{
			return base.List.IndexOf(rssItem);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000303BE File Offset: 0x0002E5BE
		public void Insert(int index, RssItem item)
		{
			this.pubDateChanged = true;
			base.List.Insert(index, item);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000303D6 File Offset: 0x0002E5D6
		public void Remove(RssItem item)
		{
			this.pubDateChanged = true;
			base.List.Remove(item);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000303F0 File Offset: 0x0002E5F0
		public DateTime LatestPubDate()
		{
			this.CalculatePubDates();
			return this.latestPubDate;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00030410 File Offset: 0x0002E610
		public DateTime OldestPubDate()
		{
			this.CalculatePubDates();
			return this.oldestPubDate;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00030430 File Offset: 0x0002E630
		private void CalculatePubDates()
		{
			bool flag = this.pubDateChanged;
			if (flag)
			{
				this.pubDateChanged = false;
				this.latestPubDate = DateTime.MinValue;
				this.oldestPubDate = DateTime.MaxValue;
				foreach (object obj in base.List)
				{
					RssItem rssItem = (RssItem)obj;
					bool flag2 = (rssItem.PubDate != RssDefault.DateTime) & (rssItem.PubDate > this.latestPubDate);
					if (flag2)
					{
						this.latestPubDate = rssItem.PubDate;
					}
				}
				bool flag3 = this.latestPubDate == DateTime.MinValue;
				if (flag3)
				{
					this.latestPubDate = RssDefault.DateTime;
				}
				foreach (object obj2 in base.List)
				{
					RssItem rssItem2 = (RssItem)obj2;
					bool flag4 = (rssItem2.PubDate != RssDefault.DateTime) & (rssItem2.PubDate < this.oldestPubDate);
					if (flag4)
					{
						this.oldestPubDate = rssItem2.PubDate;
					}
				}
				bool flag5 = this.oldestPubDate == DateTime.MaxValue;
				if (flag5)
				{
					this.oldestPubDate = RssDefault.DateTime;
				}
			}
		}

		// Token: 0x040004BA RID: 1210
		private DateTime latestPubDate = RssDefault.DateTime;

		// Token: 0x040004BB RID: 1211
		private DateTime oldestPubDate = RssDefault.DateTime;

		// Token: 0x040004BC RID: 1212
		private bool pubDateChanged = true;
	}
}
