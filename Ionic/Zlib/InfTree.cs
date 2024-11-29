using System;

namespace Ionic.Zlib
{
	// Token: 0x02000018 RID: 24
	internal sealed class InfTree
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000AC4C File Offset: 0x00008E4C
		private int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
		{
			int num = 0;
			int i = n;
			do
			{
				this.c[b[bindex + num]]++;
				num++;
				i--;
			}
			while (i != 0);
			bool flag = this.c[0] == n;
			int num2;
			if (flag)
			{
				t[0] = -1;
				m[0] = 0;
				num2 = 0;
			}
			else
			{
				int num3 = m[0];
				int j;
				for (j = 1; j <= 15; j++)
				{
					bool flag2 = this.c[j] != 0;
					if (flag2)
					{
						break;
					}
				}
				int k = j;
				bool flag3 = num3 < j;
				if (flag3)
				{
					num3 = j;
				}
				for (i = 15; i != 0; i--)
				{
					bool flag4 = this.c[i] != 0;
					if (flag4)
					{
						break;
					}
				}
				int num4 = i;
				bool flag5 = num3 > i;
				if (flag5)
				{
					num3 = i;
				}
				m[0] = num3;
				int num5 = 1 << j;
				while (j < i)
				{
					bool flag6 = (num5 -= this.c[j]) < 0;
					if (flag6)
					{
						return -3;
					}
					j++;
					num5 <<= 1;
				}
				bool flag7 = (num5 -= this.c[i]) < 0;
				if (flag7)
				{
					num2 = -3;
				}
				else
				{
					this.c[i] += num5;
					j = (this.x[1] = 0);
					num = 1;
					int num6 = 2;
					while (--i != 0)
					{
						j = (this.x[num6] = j + this.c[num]);
						num6++;
						num++;
					}
					i = 0;
					num = 0;
					do
					{
						bool flag8 = (j = b[bindex + num]) != 0;
						if (flag8)
						{
							int[] array = this.x;
							int num7 = j;
							int num8 = array[num7];
							array[num7] = num8 + 1;
							v[num8] = i;
						}
						num++;
					}
					while (++i < n);
					n = this.x[num4];
					i = (this.x[0] = 0);
					num = 0;
					int num9 = -1;
					int num10 = -num3;
					this.u[0] = 0;
					int num11 = 0;
					int num12 = 0;
					while (k <= num4)
					{
						int num13 = this.c[k];
						while (num13-- != 0)
						{
							int num14;
							while (k > num10 + num3)
							{
								num9++;
								num10 += num3;
								num12 = num4 - num10;
								num12 = ((num12 > num3) ? num3 : num12);
								bool flag9 = (num14 = 1 << ((j = k - num10) & 31)) > num13 + 1;
								if (flag9)
								{
									num14 -= num13 + 1;
									num6 = k;
									bool flag10 = j < num12;
									if (flag10)
									{
										while (++j < num12)
										{
											bool flag11 = (num14 <<= 1) <= this.c[++num6];
											if (flag11)
											{
												break;
											}
											num14 -= this.c[num6];
										}
									}
								}
								num12 = 1 << j;
								bool flag12 = hn[0] + num12 > 1440;
								if (flag12)
								{
									return -3;
								}
								num11 = (this.u[num9] = hn[0]);
								hn[0] += num12;
								bool flag13 = num9 != 0;
								if (flag13)
								{
									this.x[num9] = i;
									this.r[0] = (int)((sbyte)j);
									this.r[1] = (int)((sbyte)num3);
									j = SharedUtils.URShift(i, num10 - num3);
									this.r[2] = num11 - this.u[num9 - 1] - j;
									Array.Copy(this.r, 0, hp, (this.u[num9 - 1] + j) * 3, 3);
								}
								else
								{
									t[0] = num11;
								}
							}
							this.r[1] = (int)((sbyte)(k - num10));
							bool flag14 = num >= n;
							if (flag14)
							{
								this.r[0] = 192;
							}
							else
							{
								bool flag15 = v[num] < s;
								if (flag15)
								{
									this.r[0] = (int)((v[num] < 256) ? 0 : 96);
									this.r[2] = v[num++];
								}
								else
								{
									this.r[0] = (int)((sbyte)(e[v[num] - s] + 16 + 64));
									this.r[2] = d[v[num++] - s];
								}
							}
							num14 = 1 << k - num10;
							for (j = SharedUtils.URShift(i, num10); j < num12; j += num14)
							{
								Array.Copy(this.r, 0, hp, (num11 + j) * 3, 3);
							}
							j = 1 << k - 1;
							while ((i & j) != 0)
							{
								i ^= j;
								j = SharedUtils.URShift(j, 1);
							}
							i ^= j;
							int num15 = (1 << num10) - 1;
							while ((i & num15) != this.x[num9])
							{
								num9--;
								num10 -= num3;
								num15 = (1 << num10) - 1;
							}
						}
						k++;
					}
					num2 = ((num5 != 0 && num4 != 1) ? (-5) : 0);
				}
			}
			return num2;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000B1A0 File Offset: 0x000093A0
		internal int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZlibCodec z)
		{
			this.initWorkArea(19);
			this.hn[0] = 0;
			int num = this.huft_build(c, 0, 19, 19, null, null, tb, bb, hp, this.hn, this.v);
			bool flag = num == -3;
			if (flag)
			{
				z.Message = "oversubscribed dynamic bit lengths tree";
			}
			else
			{
				bool flag2 = num == -5 || bb[0] == 0;
				if (flag2)
				{
					z.Message = "incomplete dynamic bit lengths tree";
					num = -3;
				}
			}
			return num;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000B220 File Offset: 0x00009420
		internal int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZlibCodec z)
		{
			this.initWorkArea(288);
			this.hn[0] = 0;
			int num = this.huft_build(c, 0, nl, 257, InfTree.cplens, InfTree.cplext, tl, bl, hp, this.hn, this.v);
			bool flag = num != 0 || bl[0] == 0;
			int num2;
			if (flag)
			{
				bool flag2 = num == -3;
				if (flag2)
				{
					z.Message = "oversubscribed literal/length tree";
				}
				else
				{
					bool flag3 = num != -4;
					if (flag3)
					{
						z.Message = "incomplete literal/length tree";
						num = -3;
					}
				}
				num2 = num;
			}
			else
			{
				this.initWorkArea(288);
				num = this.huft_build(c, nl, nd, 0, InfTree.cpdist, InfTree.cpdext, td, bd, hp, this.hn, this.v);
				bool flag4 = num != 0 || (bd[0] == 0 && nl > 257);
				if (flag4)
				{
					bool flag5 = num == -3;
					if (flag5)
					{
						z.Message = "oversubscribed distance tree";
					}
					else
					{
						bool flag6 = num == -5;
						if (flag6)
						{
							z.Message = "incomplete distance tree";
							num = -3;
						}
						else
						{
							bool flag7 = num != -4;
							if (flag7)
							{
								z.Message = "empty distance tree with lengths";
								num = -3;
							}
						}
					}
					num2 = num;
				}
				else
				{
					num2 = 0;
				}
			}
			return num2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000B374 File Offset: 0x00009574
		internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZlibCodec z)
		{
			bl[0] = 9;
			bd[0] = 5;
			tl[0] = InfTree.fixed_tl;
			td[0] = InfTree.fixed_td;
			return 0;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private void initWorkArea(int vsize)
		{
			bool flag = this.hn == null;
			if (flag)
			{
				this.hn = new int[1];
				this.v = new int[vsize];
				this.c = new int[16];
				this.r = new int[3];
				this.u = new int[15];
				this.x = new int[16];
			}
			else
			{
				bool flag2 = this.v.Length < vsize;
				if (flag2)
				{
					this.v = new int[vsize];
				}
				Array.Clear(this.v, 0, vsize);
				Array.Clear(this.c, 0, 16);
				this.r[0] = 0;
				this.r[1] = 0;
				this.r[2] = 0;
				Array.Clear(this.u, 0, 15);
				Array.Clear(this.x, 0, 16);
			}
		}

		// Token: 0x040000C2 RID: 194
		private const int MANY = 1440;

		// Token: 0x040000C3 RID: 195
		private const int Z_OK = 0;

		// Token: 0x040000C4 RID: 196
		private const int Z_STREAM_END = 1;

		// Token: 0x040000C5 RID: 197
		private const int Z_NEED_DICT = 2;

		// Token: 0x040000C6 RID: 198
		private const int Z_ERRNO = -1;

		// Token: 0x040000C7 RID: 199
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x040000C8 RID: 200
		private const int Z_DATA_ERROR = -3;

		// Token: 0x040000C9 RID: 201
		private const int Z_MEM_ERROR = -4;

		// Token: 0x040000CA RID: 202
		private const int Z_BUF_ERROR = -5;

		// Token: 0x040000CB RID: 203
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x040000CC RID: 204
		internal const int fixed_bl = 9;

		// Token: 0x040000CD RID: 205
		internal const int fixed_bd = 5;

		// Token: 0x040000CE RID: 206
		internal static readonly int[] fixed_tl = new int[]
		{
			96, 7, 256, 0, 8, 80, 0, 8, 16, 84,
			8, 115, 82, 7, 31, 0, 8, 112, 0, 8,
			48, 0, 9, 192, 80, 7, 10, 0, 8, 96,
			0, 8, 32, 0, 9, 160, 0, 8, 0, 0,
			8, 128, 0, 8, 64, 0, 9, 224, 80, 7,
			6, 0, 8, 88, 0, 8, 24, 0, 9, 144,
			83, 7, 59, 0, 8, 120, 0, 8, 56, 0,
			9, 208, 81, 7, 17, 0, 8, 104, 0, 8,
			40, 0, 9, 176, 0, 8, 8, 0, 8, 136,
			0, 8, 72, 0, 9, 240, 80, 7, 4, 0,
			8, 84, 0, 8, 20, 85, 8, 227, 83, 7,
			43, 0, 8, 116, 0, 8, 52, 0, 9, 200,
			81, 7, 13, 0, 8, 100, 0, 8, 36, 0,
			9, 168, 0, 8, 4, 0, 8, 132, 0, 8,
			68, 0, 9, 232, 80, 7, 8, 0, 8, 92,
			0, 8, 28, 0, 9, 152, 84, 7, 83, 0,
			8, 124, 0, 8, 60, 0, 9, 216, 82, 7,
			23, 0, 8, 108, 0, 8, 44, 0, 9, 184,
			0, 8, 12, 0, 8, 140, 0, 8, 76, 0,
			9, 248, 80, 7, 3, 0, 8, 82, 0, 8,
			18, 85, 8, 163, 83, 7, 35, 0, 8, 114,
			0, 8, 50, 0, 9, 196, 81, 7, 11, 0,
			8, 98, 0, 8, 34, 0, 9, 164, 0, 8,
			2, 0, 8, 130, 0, 8, 66, 0, 9, 228,
			80, 7, 7, 0, 8, 90, 0, 8, 26, 0,
			9, 148, 84, 7, 67, 0, 8, 122, 0, 8,
			58, 0, 9, 212, 82, 7, 19, 0, 8, 106,
			0, 8, 42, 0, 9, 180, 0, 8, 10, 0,
			8, 138, 0, 8, 74, 0, 9, 244, 80, 7,
			5, 0, 8, 86, 0, 8, 22, 192, 8, 0,
			83, 7, 51, 0, 8, 118, 0, 8, 54, 0,
			9, 204, 81, 7, 15, 0, 8, 102, 0, 8,
			38, 0, 9, 172, 0, 8, 6, 0, 8, 134,
			0, 8, 70, 0, 9, 236, 80, 7, 9, 0,
			8, 94, 0, 8, 30, 0, 9, 156, 84, 7,
			99, 0, 8, 126, 0, 8, 62, 0, 9, 220,
			82, 7, 27, 0, 8, 110, 0, 8, 46, 0,
			9, 188, 0, 8, 14, 0, 8, 142, 0, 8,
			78, 0, 9, 252, 96, 7, 256, 0, 8, 81,
			0, 8, 17, 85, 8, 131, 82, 7, 31, 0,
			8, 113, 0, 8, 49, 0, 9, 194, 80, 7,
			10, 0, 8, 97, 0, 8, 33, 0, 9, 162,
			0, 8, 1, 0, 8, 129, 0, 8, 65, 0,
			9, 226, 80, 7, 6, 0, 8, 89, 0, 8,
			25, 0, 9, 146, 83, 7, 59, 0, 8, 121,
			0, 8, 57, 0, 9, 210, 81, 7, 17, 0,
			8, 105, 0, 8, 41, 0, 9, 178, 0, 8,
			9, 0, 8, 137, 0, 8, 73, 0, 9, 242,
			80, 7, 4, 0, 8, 85, 0, 8, 21, 80,
			8, 258, 83, 7, 43, 0, 8, 117, 0, 8,
			53, 0, 9, 202, 81, 7, 13, 0, 8, 101,
			0, 8, 37, 0, 9, 170, 0, 8, 5, 0,
			8, 133, 0, 8, 69, 0, 9, 234, 80, 7,
			8, 0, 8, 93, 0, 8, 29, 0, 9, 154,
			84, 7, 83, 0, 8, 125, 0, 8, 61, 0,
			9, 218, 82, 7, 23, 0, 8, 109, 0, 8,
			45, 0, 9, 186, 0, 8, 13, 0, 8, 141,
			0, 8, 77, 0, 9, 250, 80, 7, 3, 0,
			8, 83, 0, 8, 19, 85, 8, 195, 83, 7,
			35, 0, 8, 115, 0, 8, 51, 0, 9, 198,
			81, 7, 11, 0, 8, 99, 0, 8, 35, 0,
			9, 166, 0, 8, 3, 0, 8, 131, 0, 8,
			67, 0, 9, 230, 80, 7, 7, 0, 8, 91,
			0, 8, 27, 0, 9, 150, 84, 7, 67, 0,
			8, 123, 0, 8, 59, 0, 9, 214, 82, 7,
			19, 0, 8, 107, 0, 8, 43, 0, 9, 182,
			0, 8, 11, 0, 8, 139, 0, 8, 75, 0,
			9, 246, 80, 7, 5, 0, 8, 87, 0, 8,
			23, 192, 8, 0, 83, 7, 51, 0, 8, 119,
			0, 8, 55, 0, 9, 206, 81, 7, 15, 0,
			8, 103, 0, 8, 39, 0, 9, 174, 0, 8,
			7, 0, 8, 135, 0, 8, 71, 0, 9, 238,
			80, 7, 9, 0, 8, 95, 0, 8, 31, 0,
			9, 158, 84, 7, 99, 0, 8, 127, 0, 8,
			63, 0, 9, 222, 82, 7, 27, 0, 8, 111,
			0, 8, 47, 0, 9, 190, 0, 8, 15, 0,
			8, 143, 0, 8, 79, 0, 9, 254, 96, 7,
			256, 0, 8, 80, 0, 8, 16, 84, 8, 115,
			82, 7, 31, 0, 8, 112, 0, 8, 48, 0,
			9, 193, 80, 7, 10, 0, 8, 96, 0, 8,
			32, 0, 9, 161, 0, 8, 0, 0, 8, 128,
			0, 8, 64, 0, 9, 225, 80, 7, 6, 0,
			8, 88, 0, 8, 24, 0, 9, 145, 83, 7,
			59, 0, 8, 120, 0, 8, 56, 0, 9, 209,
			81, 7, 17, 0, 8, 104, 0, 8, 40, 0,
			9, 177, 0, 8, 8, 0, 8, 136, 0, 8,
			72, 0, 9, 241, 80, 7, 4, 0, 8, 84,
			0, 8, 20, 85, 8, 227, 83, 7, 43, 0,
			8, 116, 0, 8, 52, 0, 9, 201, 81, 7,
			13, 0, 8, 100, 0, 8, 36, 0, 9, 169,
			0, 8, 4, 0, 8, 132, 0, 8, 68, 0,
			9, 233, 80, 7, 8, 0, 8, 92, 0, 8,
			28, 0, 9, 153, 84, 7, 83, 0, 8, 124,
			0, 8, 60, 0, 9, 217, 82, 7, 23, 0,
			8, 108, 0, 8, 44, 0, 9, 185, 0, 8,
			12, 0, 8, 140, 0, 8, 76, 0, 9, 249,
			80, 7, 3, 0, 8, 82, 0, 8, 18, 85,
			8, 163, 83, 7, 35, 0, 8, 114, 0, 8,
			50, 0, 9, 197, 81, 7, 11, 0, 8, 98,
			0, 8, 34, 0, 9, 165, 0, 8, 2, 0,
			8, 130, 0, 8, 66, 0, 9, 229, 80, 7,
			7, 0, 8, 90, 0, 8, 26, 0, 9, 149,
			84, 7, 67, 0, 8, 122, 0, 8, 58, 0,
			9, 213, 82, 7, 19, 0, 8, 106, 0, 8,
			42, 0, 9, 181, 0, 8, 10, 0, 8, 138,
			0, 8, 74, 0, 9, 245, 80, 7, 5, 0,
			8, 86, 0, 8, 22, 192, 8, 0, 83, 7,
			51, 0, 8, 118, 0, 8, 54, 0, 9, 205,
			81, 7, 15, 0, 8, 102, 0, 8, 38, 0,
			9, 173, 0, 8, 6, 0, 8, 134, 0, 8,
			70, 0, 9, 237, 80, 7, 9, 0, 8, 94,
			0, 8, 30, 0, 9, 157, 84, 7, 99, 0,
			8, 126, 0, 8, 62, 0, 9, 221, 82, 7,
			27, 0, 8, 110, 0, 8, 46, 0, 9, 189,
			0, 8, 14, 0, 8, 142, 0, 8, 78, 0,
			9, 253, 96, 7, 256, 0, 8, 81, 0, 8,
			17, 85, 8, 131, 82, 7, 31, 0, 8, 113,
			0, 8, 49, 0, 9, 195, 80, 7, 10, 0,
			8, 97, 0, 8, 33, 0, 9, 163, 0, 8,
			1, 0, 8, 129, 0, 8, 65, 0, 9, 227,
			80, 7, 6, 0, 8, 89, 0, 8, 25, 0,
			9, 147, 83, 7, 59, 0, 8, 121, 0, 8,
			57, 0, 9, 211, 81, 7, 17, 0, 8, 105,
			0, 8, 41, 0, 9, 179, 0, 8, 9, 0,
			8, 137, 0, 8, 73, 0, 9, 243, 80, 7,
			4, 0, 8, 85, 0, 8, 21, 80, 8, 258,
			83, 7, 43, 0, 8, 117, 0, 8, 53, 0,
			9, 203, 81, 7, 13, 0, 8, 101, 0, 8,
			37, 0, 9, 171, 0, 8, 5, 0, 8, 133,
			0, 8, 69, 0, 9, 235, 80, 7, 8, 0,
			8, 93, 0, 8, 29, 0, 9, 155, 84, 7,
			83, 0, 8, 125, 0, 8, 61, 0, 9, 219,
			82, 7, 23, 0, 8, 109, 0, 8, 45, 0,
			9, 187, 0, 8, 13, 0, 8, 141, 0, 8,
			77, 0, 9, 251, 80, 7, 3, 0, 8, 83,
			0, 8, 19, 85, 8, 195, 83, 7, 35, 0,
			8, 115, 0, 8, 51, 0, 9, 199, 81, 7,
			11, 0, 8, 99, 0, 8, 35, 0, 9, 167,
			0, 8, 3, 0, 8, 131, 0, 8, 67, 0,
			9, 231, 80, 7, 7, 0, 8, 91, 0, 8,
			27, 0, 9, 151, 84, 7, 67, 0, 8, 123,
			0, 8, 59, 0, 9, 215, 82, 7, 19, 0,
			8, 107, 0, 8, 43, 0, 9, 183, 0, 8,
			11, 0, 8, 139, 0, 8, 75, 0, 9, 247,
			80, 7, 5, 0, 8, 87, 0, 8, 23, 192,
			8, 0, 83, 7, 51, 0, 8, 119, 0, 8,
			55, 0, 9, 207, 81, 7, 15, 0, 8, 103,
			0, 8, 39, 0, 9, 175, 0, 8, 7, 0,
			8, 135, 0, 8, 71, 0, 9, 239, 80, 7,
			9, 0, 8, 95, 0, 8, 31, 0, 9, 159,
			84, 7, 99, 0, 8, 127, 0, 8, 63, 0,
			9, 223, 82, 7, 27, 0, 8, 111, 0, 8,
			47, 0, 9, 191, 0, 8, 15, 0, 8, 143,
			0, 8, 79, 0, 9, 255
		};

		// Token: 0x040000CF RID: 207
		internal static readonly int[] fixed_td = new int[]
		{
			80, 5, 1, 87, 5, 257, 83, 5, 17, 91,
			5, 4097, 81, 5, 5, 89, 5, 1025, 85, 5,
			65, 93, 5, 16385, 80, 5, 3, 88, 5, 513,
			84, 5, 33, 92, 5, 8193, 82, 5, 9, 90,
			5, 2049, 86, 5, 129, 192, 5, 24577, 80, 5,
			2, 87, 5, 385, 83, 5, 25, 91, 5, 6145,
			81, 5, 7, 89, 5, 1537, 85, 5, 97, 93,
			5, 24577, 80, 5, 4, 88, 5, 769, 84, 5,
			49, 92, 5, 12289, 82, 5, 13, 90, 5, 3073,
			86, 5, 193, 192, 5, 24577
		};

		// Token: 0x040000D0 RID: 208
		internal static readonly int[] cplens = new int[]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258, 0,
			0
		};

		// Token: 0x040000D1 RID: 209
		internal static readonly int[] cplext = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0, 112,
			112
		};

		// Token: 0x040000D2 RID: 210
		internal static readonly int[] cpdist = new int[]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577
		};

		// Token: 0x040000D3 RID: 211
		internal static readonly int[] cpdext = new int[]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		// Token: 0x040000D4 RID: 212
		internal const int BMAX = 15;

		// Token: 0x040000D5 RID: 213
		internal int[] hn = null;

		// Token: 0x040000D6 RID: 214
		internal int[] v = null;

		// Token: 0x040000D7 RID: 215
		internal int[] c = null;

		// Token: 0x040000D8 RID: 216
		internal int[] r = null;

		// Token: 0x040000D9 RID: 217
		internal int[] u = null;

		// Token: 0x040000DA RID: 218
		internal int[] x = null;
	}
}
