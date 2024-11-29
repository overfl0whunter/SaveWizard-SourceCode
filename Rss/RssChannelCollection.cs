using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class RssChannelCollection : CollectionBase
	{
		// Token: 0x170001D5 RID: 469
		public RssChannel this[int index]
		{
			get
			{
				return (RssChannel)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000301DC File Offset: 0x0002E3DC
		public int Add(RssChannel channel)
		{
			return base.List.Add(channel);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000301FC File Offset: 0x0002E3FC
		public bool Contains(RssChannel rssChannel)
		{
			return base.List.Contains(rssChannel);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(RssChannel[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003021C File Offset: 0x0002E41C
		public int IndexOf(RssChannel rssChannel)
		{
			return base.List.IndexOf(rssChannel);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, RssChannel channel)
		{
			base.List.Insert(index, channel);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(RssChannel channel)
		{
			base.List.Remove(channel);
		}
	}
}
