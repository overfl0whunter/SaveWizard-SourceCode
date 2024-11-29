using System;

namespace PS3SaveEditor
{
	// Token: 0x020001BD RID: 445
	public class alias
	{
		// Token: 0x060016C1 RID: 5825 RVA: 0x000710A4 File Offset: 0x0006F2A4
		public static alias Copy(alias alias)
		{
			return new alias
			{
				id = alias.id,
				region = alias.region,
				name = alias.name,
				diskcode = alias.diskcode
			};
		}

		// Token: 0x04000A88 RID: 2696
		public string id;

		// Token: 0x04000A89 RID: 2697
		public string name;

		// Token: 0x04000A8A RID: 2698
		public int acts;

		// Token: 0x04000A8B RID: 2699
		public string diskcode;

		// Token: 0x04000A8C RID: 2700
		public int region;
	}
}
