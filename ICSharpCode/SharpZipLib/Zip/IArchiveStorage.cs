using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000077 RID: 119
	public interface IArchiveStorage
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600059F RID: 1439
		FileUpdateMode UpdateMode { get; }

		// Token: 0x060005A0 RID: 1440
		Stream GetTemporaryOutput();

		// Token: 0x060005A1 RID: 1441
		Stream ConvertTemporaryToFinal();

		// Token: 0x060005A2 RID: 1442
		Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060005A3 RID: 1443
		Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060005A4 RID: 1444
		void Dispose();
	}
}
