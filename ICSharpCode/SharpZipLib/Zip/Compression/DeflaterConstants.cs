using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000082 RID: 130
	public class DeflaterConstants
	{
		// Token: 0x040003D4 RID: 980
		public const bool DEBUGGING = false;

		// Token: 0x040003D5 RID: 981
		public const int STORED_BLOCK = 0;

		// Token: 0x040003D6 RID: 982
		public const int STATIC_TREES = 1;

		// Token: 0x040003D7 RID: 983
		public const int DYN_TREES = 2;

		// Token: 0x040003D8 RID: 984
		public const int PRESET_DICT = 32;

		// Token: 0x040003D9 RID: 985
		public const int DEFAULT_MEM_LEVEL = 8;

		// Token: 0x040003DA RID: 986
		public const int MAX_MATCH = 258;

		// Token: 0x040003DB RID: 987
		public const int MIN_MATCH = 3;

		// Token: 0x040003DC RID: 988
		public const int MAX_WBITS = 15;

		// Token: 0x040003DD RID: 989
		public const int WSIZE = 32768;

		// Token: 0x040003DE RID: 990
		public const int WMASK = 32767;

		// Token: 0x040003DF RID: 991
		public const int HASH_BITS = 15;

		// Token: 0x040003E0 RID: 992
		public const int HASH_SIZE = 32768;

		// Token: 0x040003E1 RID: 993
		public const int HASH_MASK = 32767;

		// Token: 0x040003E2 RID: 994
		public const int HASH_SHIFT = 5;

		// Token: 0x040003E3 RID: 995
		public const int MIN_LOOKAHEAD = 262;

		// Token: 0x040003E4 RID: 996
		public const int MAX_DIST = 32506;

		// Token: 0x040003E5 RID: 997
		public const int PENDING_BUF_SIZE = 65536;

		// Token: 0x040003E6 RID: 998
		public static int MAX_BLOCK_SIZE = Math.Min(65535, 65531);

		// Token: 0x040003E7 RID: 999
		public const int DEFLATE_STORED = 0;

		// Token: 0x040003E8 RID: 1000
		public const int DEFLATE_FAST = 1;

		// Token: 0x040003E9 RID: 1001
		public const int DEFLATE_SLOW = 2;

		// Token: 0x040003EA RID: 1002
		public static int[] GOOD_LENGTH = new int[] { 0, 4, 4, 4, 4, 8, 8, 8, 32, 32 };

		// Token: 0x040003EB RID: 1003
		public static int[] MAX_LAZY = new int[] { 0, 4, 5, 6, 4, 16, 16, 32, 128, 258 };

		// Token: 0x040003EC RID: 1004
		public static int[] NICE_LENGTH = new int[] { 0, 8, 16, 32, 16, 32, 128, 128, 258, 258 };

		// Token: 0x040003ED RID: 1005
		public static int[] MAX_CHAIN = new int[] { 0, 4, 8, 32, 16, 32, 128, 256, 1024, 4096 };

		// Token: 0x040003EE RID: 1006
		public static int[] COMPR_FUNC = new int[] { 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
	}
}
