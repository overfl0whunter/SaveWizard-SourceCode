using System;

namespace Ionic.Zip
{
	// Token: 0x0200004A RID: 74
	public enum ZipEntrySource
	{
		// Token: 0x0400021F RID: 543
		None,
		// Token: 0x04000220 RID: 544
		FileSystem,
		// Token: 0x04000221 RID: 545
		Stream,
		// Token: 0x04000222 RID: 546
		ZipFile,
		// Token: 0x04000223 RID: 547
		WriteDelegate,
		// Token: 0x04000224 RID: 548
		JitStream,
		// Token: 0x04000225 RID: 549
		ZipOutputStream
	}
}
