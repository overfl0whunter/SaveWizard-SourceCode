using System;

namespace Ionic.Zlib
{
	// Token: 0x02000028 RID: 40
	public static class ZlibConstants
	{
		// Token: 0x04000163 RID: 355
		public const int WindowBitsMax = 15;

		// Token: 0x04000164 RID: 356
		public const int WindowBitsDefault = 15;

		// Token: 0x04000165 RID: 357
		public const int Z_OK = 0;

		// Token: 0x04000166 RID: 358
		public const int Z_STREAM_END = 1;

		// Token: 0x04000167 RID: 359
		public const int Z_NEED_DICT = 2;

		// Token: 0x04000168 RID: 360
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04000169 RID: 361
		public const int Z_DATA_ERROR = -3;

		// Token: 0x0400016A RID: 362
		public const int Z_BUF_ERROR = -5;

		// Token: 0x0400016B RID: 363
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x0400016C RID: 364
		public const int WorkingBufferSizeMin = 1024;
	}
}
