using System;

namespace Rss
{
	// Token: 0x020000C8 RID: 200
	public sealed class RssPhotoAlbum : RssModule
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x000322F4 File Offset: 0x000304F4
		public RssPhotoAlbum(Uri link, RssPhotoAlbumCategory photoAlbumCategory)
		{
			base.NamespacePrefix = "photoAlbum";
			base.NamespaceURL = new Uri("http://xml.innothinx.com/photoAlbum");
			base.ChannelExtensions.Add(new RssModuleItem("link", true, RssDefault.Check(link).ToString()));
			base.ItemExtensions.Add(photoAlbumCategory);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00032358 File Offset: 0x00030558
		public RssPhotoAlbum(Uri link, RssPhotoAlbumCategories photoAlbumCategories)
		{
			base.NamespacePrefix = "photoAlbum";
			base.NamespaceURL = new Uri("http://xml.innothinx.com/photoAlbum");
			base.ChannelExtensions.Add(new RssModuleItem("link", true, RssDefault.Check(link).ToString()));
			foreach (object obj in photoAlbumCategories)
			{
				RssModuleItemCollection rssModuleItemCollection = (RssModuleItemCollection)obj;
				base.ItemExtensions.Add(rssModuleItemCollection);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00032400 File Offset: 0x00030600
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0003244D File Offset: 0x0003064D
		public Uri Link
		{
			get
			{
				return (RssDefault.Check(base.ChannelExtensions[0].Text) == "") ? null : new Uri(base.ChannelExtensions[0].Text);
			}
			set
			{
				base.ChannelExtensions[0].Text = ((RssDefault.Check(value) == RssDefault.Uri) ? "" : value.ToString());
			}
		}
	}
}
