using System;

namespace Ionic.Zip
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum ZipEntryTimestamp
	{
		// Token: 0x04000216 RID: 534
		None = 0,
		// Token: 0x04000217 RID: 535
		DOS = 1,
		// Token: 0x04000218 RID: 536
		Windows = 2,
		// Token: 0x04000219 RID: 537
		Unix = 4,
		// Token: 0x0400021A RID: 538
		InfoZip1 = 8
	}
}
