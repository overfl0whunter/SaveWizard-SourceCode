using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A5 RID: 165
	public class PathFilter : IScanFilter
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x0002F2A2 File Offset: 0x0002D4A2
		public PathFilter(string filter)
		{
			this.nameFilter_ = new NameFilter(filter);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		public virtual bool IsMatch(string name)
		{
			bool flag = false;
			bool flag2 = name != null;
			if (flag2)
			{
				string text = ((name.Length > 0) ? Path.GetFullPath(name) : "");
				flag = this.nameFilter_.IsMatch(text);
			}
			return flag;
		}

		// Token: 0x040004AB RID: 1195
		private NameFilter nameFilter_;
	}
}
