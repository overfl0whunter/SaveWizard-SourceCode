using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200005B RID: 91
	public interface IEntryFactory
	{
		// Token: 0x0600046E RID: 1134
		ZipEntry MakeFileEntry(string fileName);

		// Token: 0x0600046F RID: 1135
		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		// Token: 0x06000470 RID: 1136
		ZipEntry MakeDirectoryEntry(string directoryName);

		// Token: 0x06000471 RID: 1137
		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000472 RID: 1138
		// (set) Token: 0x06000473 RID: 1139
		INameTransform NameTransform { get; set; }
	}
}
