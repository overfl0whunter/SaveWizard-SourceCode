using System;

namespace Rss
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public class RssGuid : RssElement
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000315E8 File Offset: 0x0002F7E8
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00031600 File Offset: 0x0002F800
		public DBBool PermaLink
		{
			get
			{
				return this.permaLink;
			}
			set
			{
				this.permaLink = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0003160C File Offset: 0x0002F80C
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00031624 File Offset: 0x0002F824
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

		// Token: 0x040004ED RID: 1261
		private DBBool permaLink = DBBool.Null;

		// Token: 0x040004EE RID: 1262
		private string name = "";
	}
}
