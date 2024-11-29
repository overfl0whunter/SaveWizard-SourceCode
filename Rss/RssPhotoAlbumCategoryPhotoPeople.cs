using System;

namespace Rss
{
	// Token: 0x020000C3 RID: 195
	public sealed class RssPhotoAlbumCategoryPhotoPeople : RssModuleItemCollection
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00031CF3 File Offset: 0x0002FEF3
		public RssPhotoAlbumCategoryPhotoPeople()
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00031CFD File Offset: 0x0002FEFD
		public RssPhotoAlbumCategoryPhotoPeople(string value)
		{
			this.Add(value);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00031D10 File Offset: 0x0002FF10
		public int Add(string value)
		{
			return base.Add(new RssModuleItem("person", true, value));
		}
	}
}
