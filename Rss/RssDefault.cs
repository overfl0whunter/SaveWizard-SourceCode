using System;

namespace Rss
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	public class RssDefault
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00035860 File Offset: 0x00033A60
		public static string Check(string input)
		{
			return (input == null) ? "" : input;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00035880 File Offset: 0x00033A80
		public static int Check(int input)
		{
			return (input < -1) ? (-1) : input;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0003589C File Offset: 0x00033A9C
		public static Uri Check(Uri input)
		{
			return (input == null) ? RssDefault.Uri : input;
		}

		// Token: 0x04000524 RID: 1316
		public const string String = "";

		// Token: 0x04000525 RID: 1317
		public const int Int = -1;

		// Token: 0x04000526 RID: 1318
		public static readonly DateTime DateTime = DateTime.MinValue;

		// Token: 0x04000527 RID: 1319
		public static readonly Uri Uri = null;
	}
}
