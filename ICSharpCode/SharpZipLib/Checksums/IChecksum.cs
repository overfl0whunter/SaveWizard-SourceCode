using System;

namespace ICSharpCode.SharpZipLib.Checksums
{
	// Token: 0x020000AC RID: 172
	public interface IChecksum
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000773 RID: 1907
		long Value { get; }

		// Token: 0x06000774 RID: 1908
		void Reset();

		// Token: 0x06000775 RID: 1909
		void Update(int value);

		// Token: 0x06000776 RID: 1910
		void Update(byte[] buffer);

		// Token: 0x06000777 RID: 1911
		void Update(byte[] buffer, int offset, int count);
	}
}
