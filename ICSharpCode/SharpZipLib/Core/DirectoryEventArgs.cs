using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000099 RID: 153
	public class DirectoryEventArgs : ScanEventArgs
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0002E988 File Offset: 0x0002CB88
		public DirectoryEventArgs(string name, bool hasMatchingFiles)
			: base(name)
		{
			this.hasMatchingFiles_ = hasMatchingFiles;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0002E99C File Offset: 0x0002CB9C
		public bool HasMatchingFiles
		{
			get
			{
				return this.hasMatchingFiles_;
			}
		}

		// Token: 0x0400049C RID: 1180
		private bool hasMatchingFiles_;
	}
}
