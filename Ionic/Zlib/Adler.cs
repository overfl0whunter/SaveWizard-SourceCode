using System;

namespace Ionic.Zlib
{
	// Token: 0x02000024 RID: 36
	public sealed class Adler
	{
		// Token: 0x06000102 RID: 258 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			bool flag = buf == null;
			uint num;
			if (flag)
			{
				num = 1U;
			}
			else
			{
				uint num2 = adler & 65535U;
				uint num3 = (adler >> 16) & 65535U;
				while (len > 0)
				{
					int i = ((len < Adler.NMAX) ? len : Adler.NMAX);
					len -= i;
					while (i >= 16)
					{
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						num2 += (uint)buf[index++];
						num3 += num2;
						i -= 16;
					}
					bool flag2 = i != 0;
					if (flag2)
					{
						do
						{
							num2 += (uint)buf[index++];
							num3 += num2;
						}
						while (--i != 0);
					}
					num2 %= Adler.BASE;
					num3 %= Adler.BASE;
				}
				num = (num3 << 16) | num2;
			}
			return num;
		}

		// Token: 0x0400013C RID: 316
		private static readonly uint BASE = 65521U;

		// Token: 0x0400013D RID: 317
		private static readonly int NMAX = 5552;
	}
}
