using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000060 RID: 96
	[Flags]
	public enum GeneralBitFlags
	{
		// Token: 0x040002FE RID: 766
		Encrypted = 1,
		// Token: 0x040002FF RID: 767
		Method = 6,
		// Token: 0x04000300 RID: 768
		Descriptor = 8,
		// Token: 0x04000301 RID: 769
		ReservedPKware4 = 16,
		// Token: 0x04000302 RID: 770
		Patched = 32,
		// Token: 0x04000303 RID: 771
		StrongEncryption = 64,
		// Token: 0x04000304 RID: 772
		Unused7 = 128,
		// Token: 0x04000305 RID: 773
		Unused8 = 256,
		// Token: 0x04000306 RID: 774
		Unused9 = 512,
		// Token: 0x04000307 RID: 775
		Unused10 = 1024,
		// Token: 0x04000308 RID: 776
		UnicodeText = 2048,
		// Token: 0x04000309 RID: 777
		EnhancedCompress = 4096,
		// Token: 0x0400030A RID: 778
		HeaderMasked = 8192,
		// Token: 0x0400030B RID: 779
		ReservedPkware14 = 16384,
		// Token: 0x0400030C RID: 780
		ReservedPkware15 = 32768
	}
}
