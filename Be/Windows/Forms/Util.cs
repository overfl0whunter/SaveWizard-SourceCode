using System;
using System.Diagnostics;

namespace Be.Windows.Forms
{
	// Token: 0x020000E7 RID: 231
	internal static class Util
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003C9D4 File Offset: 0x0003ABD4
		public static bool DesignMode
		{
			get
			{
				return Util._designMode;
			}
		}

		// Token: 0x040005C7 RID: 1479
		private static bool _designMode = Process.GetCurrentProcess().ProcessName.ToLower() == "devenv";
	}
}
