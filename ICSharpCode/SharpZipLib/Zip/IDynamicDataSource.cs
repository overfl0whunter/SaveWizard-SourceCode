using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000074 RID: 116
	public interface IDynamicDataSource
	{
		// Token: 0x0600059A RID: 1434
		Stream GetSource(ZipEntry entry, string name);
	}
}
