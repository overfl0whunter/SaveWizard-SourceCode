using System;

namespace Rss
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public class RssCategory : RssElement
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000357FC File Offset: 0x000339FC
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00035814 File Offset: 0x00033A14
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = RssDefault.Check(value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00035824 File Offset: 0x00033A24
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0003583C File Offset: 0x00033A3C
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = RssDefault.Check(value);
			}
		}

		// Token: 0x0400051F RID: 1311
		private string name = "";

		// Token: 0x04000520 RID: 1312
		private string domain = "";
	}
}
