using System;

namespace Rss
{
	// Token: 0x020000C7 RID: 199
	public sealed class RssPhotoAlbumCategory : RssModuleItemCollection
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00031F0D File Offset: 0x0003010D
		public RssPhotoAlbumCategory(string categoryName, string categoryDescription, DateTime categoryDateFrom, DateTime categoryDateTo, RssPhotoAlbumCategoryPhoto categoryPhoto)
		{
			this.Add(categoryName, categoryDescription, categoryDateFrom, categoryDateTo, categoryPhoto);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00031F28 File Offset: 0x00030128
		private int Add(string categoryName, string categoryDescription, DateTime categoryDateFrom, DateTime categoryDateTo, RssPhotoAlbumCategoryPhoto categoryPhoto)
		{
			RssModuleItemCollection rssModuleItemCollection = new RssModuleItemCollection();
			rssModuleItemCollection.Add(new RssModuleItem("from", true, RssDefault.Check(categoryDateFrom.ToUniversalTime().ToString("r"))));
			rssModuleItemCollection.Add(new RssModuleItem("to", true, RssDefault.Check(categoryDateTo.ToUniversalTime().ToString("r"))));
			base.Add(new RssModuleItem("categoryName", true, RssDefault.Check(categoryName)));
			base.Add(new RssModuleItem("categoryDescription", true, RssDefault.Check(categoryDescription)));
			base.Add(new RssModuleItem("categoryDateRange", true, "", rssModuleItemCollection));
			base.Add(new RssModuleItem("categoryPhoto", true, "", categoryPhoto));
			return -1;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00031FF8 File Offset: 0x000301F8
		public RssPhotoAlbumCategory(string categoryName, string categoryDescription, string categoryDateFrom, string categoryDateTo, RssPhotoAlbumCategoryPhoto categoryPhoto)
		{
			this.Add(categoryName, categoryDescription, categoryDateFrom, categoryDateTo, categoryPhoto);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00032010 File Offset: 0x00030210
		private int Add(string categoryName, string categoryDescription, string categoryDateFrom, string categoryDateTo, RssPhotoAlbumCategoryPhoto categoryPhoto)
		{
			RssModuleItemCollection rssModuleItemCollection = new RssModuleItemCollection();
			rssModuleItemCollection.Add(new RssModuleItem("from", true, RssDefault.Check(categoryDateFrom)));
			rssModuleItemCollection.Add(new RssModuleItem("to", true, RssDefault.Check(categoryDateTo)));
			base.Add(new RssModuleItem("categoryName", true, RssDefault.Check(categoryName)));
			base.Add(new RssModuleItem("categoryDescription", true, RssDefault.Check(categoryDescription)));
			base.Add(new RssModuleItem("categoryDateRange", true, "", rssModuleItemCollection));
			base.Add(new RssModuleItem("categoryPhoto", true, "", categoryPhoto));
			return -1;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000320BB File Offset: 0x000302BB
		public RssPhotoAlbumCategory(string categoryName, string categoryDescription, DateTime categoryDateFrom, DateTime categoryDateTo, RssPhotoAlbumCategoryPhotos categoryPhotos)
		{
			this.Add(categoryName, categoryDescription, categoryDateFrom, categoryDateTo, categoryPhotos);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000320D4 File Offset: 0x000302D4
		private int Add(string categoryName, string categoryDescription, DateTime categoryDateFrom, DateTime categoryDateTo, RssPhotoAlbumCategoryPhotos categoryPhotos)
		{
			RssModuleItemCollection rssModuleItemCollection = new RssModuleItemCollection();
			rssModuleItemCollection.Add(new RssModuleItem("from", true, RssDefault.Check(categoryDateFrom.ToUniversalTime().ToString("r"))));
			rssModuleItemCollection.Add(new RssModuleItem("to", true, RssDefault.Check(categoryDateTo.ToUniversalTime().ToString("r"))));
			base.Add(new RssModuleItem("categoryName", true, RssDefault.Check(categoryName)));
			base.Add(new RssModuleItem("categoryDescription", true, RssDefault.Check(categoryDescription)));
			base.Add(new RssModuleItem("categoryDateRange", true, "", rssModuleItemCollection));
			foreach (object obj in categoryPhotos)
			{
				RssPhotoAlbumCategoryPhoto rssPhotoAlbumCategoryPhoto = (RssPhotoAlbumCategoryPhoto)obj;
				base.Add(new RssModuleItem("categoryPhoto", true, "", rssPhotoAlbumCategoryPhoto));
			}
			return -1;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000321EC File Offset: 0x000303EC
		public RssPhotoAlbumCategory(string categoryName, string categoryDescription, string categoryDateFrom, string categoryDateTo, RssPhotoAlbumCategoryPhotos categoryPhotos)
		{
			this.Add(categoryName, categoryDescription, categoryDateFrom, categoryDateTo, categoryPhotos);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00032204 File Offset: 0x00030404
		private int Add(string categoryName, string categoryDescription, string categoryDateFrom, string categoryDateTo, RssPhotoAlbumCategoryPhotos categoryPhotos)
		{
			RssModuleItemCollection rssModuleItemCollection = new RssModuleItemCollection();
			rssModuleItemCollection.Add(new RssModuleItem("from", true, RssDefault.Check(categoryDateFrom)));
			rssModuleItemCollection.Add(new RssModuleItem("to", true, RssDefault.Check(categoryDateTo)));
			base.Add(new RssModuleItem("categoryName", true, RssDefault.Check(categoryName)));
			base.Add(new RssModuleItem("categoryDescription", true, RssDefault.Check(categoryDescription)));
			base.Add(new RssModuleItem("categoryDateRange", true, "", rssModuleItemCollection));
			foreach (object obj in categoryPhotos)
			{
				RssPhotoAlbumCategoryPhoto rssPhotoAlbumCategoryPhoto = (RssPhotoAlbumCategoryPhoto)obj;
				base.Add(new RssModuleItem("categoryPhoto", true, "", rssPhotoAlbumCategoryPhoto));
			}
			return -1;
		}
	}
}
