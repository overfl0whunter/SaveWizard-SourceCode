using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000066 RID: 102
	public interface ITaggedData
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004E0 RID: 1248
		short TagID { get; }

		// Token: 0x060004E1 RID: 1249
		void SetData(byte[] data, int offset, int count);

		// Token: 0x060004E2 RID: 1250
		byte[] GetData();
	}
}
