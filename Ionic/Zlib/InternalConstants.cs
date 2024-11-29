﻿using System;

namespace Ionic.Zlib
{
	// Token: 0x02000022 RID: 34
	internal static class InternalConstants
	{
		// Token: 0x04000128 RID: 296
		internal static readonly int MAX_BITS = 15;

		// Token: 0x04000129 RID: 297
		internal static readonly int BL_CODES = 19;

		// Token: 0x0400012A RID: 298
		internal static readonly int D_CODES = 30;

		// Token: 0x0400012B RID: 299
		internal static readonly int LITERALS = 256;

		// Token: 0x0400012C RID: 300
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x0400012D RID: 301
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x0400012E RID: 302
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x0400012F RID: 303
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04000130 RID: 304
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x04000131 RID: 305
		internal static readonly int REPZ_11_138 = 18;
	}
}
