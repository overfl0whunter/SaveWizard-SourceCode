using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000075 RID: 117
	public class StaticDiskDataSource : IStaticDataSource
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x00025C2D File Offset: 0x00023E2D
		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00025C40 File Offset: 0x00023E40
		public Stream GetSource()
		{
			return File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x0400039B RID: 923
		private string fileName_;
	}
}
