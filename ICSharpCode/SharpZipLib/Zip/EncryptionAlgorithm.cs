using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200005F RID: 95
	public enum EncryptionAlgorithm
	{
		// Token: 0x040002EF RID: 751
		None,
		// Token: 0x040002F0 RID: 752
		PkzipClassic,
		// Token: 0x040002F1 RID: 753
		Des = 26113,
		// Token: 0x040002F2 RID: 754
		RC2,
		// Token: 0x040002F3 RID: 755
		TripleDes168,
		// Token: 0x040002F4 RID: 756
		TripleDes112 = 26121,
		// Token: 0x040002F5 RID: 757
		Aes128 = 26126,
		// Token: 0x040002F6 RID: 758
		Aes192,
		// Token: 0x040002F7 RID: 759
		Aes256,
		// Token: 0x040002F8 RID: 760
		RC2Corrected = 26370,
		// Token: 0x040002F9 RID: 761
		Blowfish = 26400,
		// Token: 0x040002FA RID: 762
		Twofish,
		// Token: 0x040002FB RID: 763
		RC4 = 26625,
		// Token: 0x040002FC RID: 764
		Unknown = 65535
	}
}
