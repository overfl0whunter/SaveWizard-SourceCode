using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200005E RID: 94
	public enum CompressionMethod
	{
		// Token: 0x040002E9 RID: 745
		Stored,
		// Token: 0x040002EA RID: 746
		Deflated = 8,
		// Token: 0x040002EB RID: 747
		Deflate64,
		// Token: 0x040002EC RID: 748
		BZip2 = 11,
		// Token: 0x040002ED RID: 749
		WinZipAES = 99
	}
}
