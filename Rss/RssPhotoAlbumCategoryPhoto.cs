using System;

namespace Rss
{
	// Token: 0x020000C5 RID: 197
	public sealed class RssPhotoAlbumCategoryPhoto : RssModuleItemCollection
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x00031D59 File Offset: 0x0002FF59
		public RssPhotoAlbumCategoryPhoto(DateTime photoDate, string photoDescription, Uri photoLink)
		{
			this.Add(photoDate, photoDescription, photoLink);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00031D6D File Offset: 0x0002FF6D
		public RssPhotoAlbumCategoryPhoto(DateTime photoDate, string photoDescription, Uri photoLink, RssPhotoAlbumCategoryPhotoPeople photoPeople)
		{
			this.Add(photoDate, photoDescription, photoLink, photoPeople);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00031D84 File Offset: 0x0002FF84
		private int Add(DateTime photoDate, string photoDescription, Uri photoLink, RssPhotoAlbumCategoryPhotoPeople photoPeople)
		{
			this.Add(photoDate, photoDescription, photoLink);
			base.Add(new RssModuleItem("photoPeople", true, "", photoPeople));
			return -1;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00031DBC File Offset: 0x0002FFBC
		private int Add(DateTime photoDate, string photoDescription, Uri photoLink)
		{
			base.Add(new RssModuleItem("photoDate", true, RssDefault.Check(photoDate.ToUniversalTime().ToString("r"))));
			base.Add(new RssModuleItem("photoDescription", false, RssDefault.Check(photoDescription)));
			base.Add(new RssModuleItem("photoLink", true, RssDefault.Check(photoLink).ToString()));
			return -1;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00031E2F File Offset: 0x0003002F
		public RssPhotoAlbumCategoryPhoto(string photoDate, string photoDescription, Uri photoLink)
		{
			this.Add(photoDate, photoDescription, photoLink);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00031E43 File Offset: 0x00030043
		public RssPhotoAlbumCategoryPhoto(string photoDate, string photoDescription, Uri photoLink, RssPhotoAlbumCategoryPhotoPeople photoPeople)
		{
			this.Add(photoDate, photoDescription, photoLink, photoPeople);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00031E5C File Offset: 0x0003005C
		private int Add(string photoDate, string photoDescription, Uri photoLink, RssPhotoAlbumCategoryPhotoPeople photoPeople)
		{
			this.Add(photoDate, photoDescription, photoLink);
			base.Add(new RssModuleItem("photoPeople", true, "", photoPeople));
			return -1;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00031E94 File Offset: 0x00030094
		private int Add(string photoDate, string photoDescription, Uri photoLink)
		{
			base.Add(new RssModuleItem("photoDate", true, RssDefault.Check(photoDate)));
			base.Add(new RssModuleItem("photoDescription", false, RssDefault.Check(photoDescription)));
			base.Add(new RssModuleItem("photoLink", true, RssDefault.Check(photoLink).ToString()));
			return -1;
		}
	}
}
