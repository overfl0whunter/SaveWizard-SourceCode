using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000076 RID: 118
	public class DynamicDiskDataSource : IDynamicDataSource
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00025C60 File Offset: 0x00023E60
		public Stream GetSource(ZipEntry entry, string name)
		{
			Stream stream = null;
			bool flag = name != null;
			if (flag)
			{
				stream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			return stream;
		}
	}
}
