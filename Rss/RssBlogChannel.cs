using System;

namespace Rss
{
	// Token: 0x020000C1 RID: 193
	public sealed class RssBlogChannel : RssModule
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00031BA8 File Offset: 0x0002FDA8
		public RssBlogChannel(Uri blogRoll, Uri mySubscriptions, Uri blink, Uri changes)
		{
			base.NamespacePrefix = "blogChannel";
			base.NamespaceURL = new Uri("http://backend.userland.com/blogChannelModule");
			base.ChannelExtensions.Add(new RssModuleItem("blogRoll", true, RssDefault.Check(blogRoll.ToString())));
			base.ChannelExtensions.Add(new RssModuleItem("mySubscriptions", true, RssDefault.Check(mySubscriptions.ToString())));
			base.ChannelExtensions.Add(new RssModuleItem("blink", true, RssDefault.Check(blink.ToString())));
			base.ChannelExtensions.Add(new RssModuleItem("changes", true, RssDefault.Check(changes.ToString())));
		}
	}
}
