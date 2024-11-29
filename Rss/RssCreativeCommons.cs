using System;

namespace Rss
{
	// Token: 0x020000C2 RID: 194
	public sealed class RssCreativeCommons : RssModule
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00031C64 File Offset: 0x0002FE64
		public RssCreativeCommons(Uri license, bool isChannelSubElement)
		{
			base.NamespacePrefix = "creativeCommons";
			base.NamespaceURL = new Uri("http://backend.userland.com/creativeCommonsRssModule");
			if (isChannelSubElement)
			{
				base.ChannelExtensions.Add(new RssModuleItem("license", true, RssDefault.Check(license.ToString())));
			}
			else
			{
				RssModuleItemCollection rssModuleItemCollection = new RssModuleItemCollection();
				rssModuleItemCollection.Add(new RssModuleItem("license", true, RssDefault.Check(license.ToString())));
				base.ItemExtensions.Add(rssModuleItemCollection);
			}
		}
	}
}
