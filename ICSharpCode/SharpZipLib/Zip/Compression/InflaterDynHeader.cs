using System;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000088 RID: 136
	internal class InflaterDynHeader
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0002BA14 File Offset: 0x00029C14
		public bool Decode(StreamManipulator input)
		{
			for (;;)
			{
				for (;;)
				{
					switch (this.mode)
					{
					case 0:
						goto IL_0032;
					case 1:
						goto IL_0077;
					case 2:
						goto IL_00DC;
					case 3:
						goto IL_0133;
					case 4:
						goto IL_01BB;
					case 5:
						goto IL_027A;
					}
				}
				IL_027A:
				int num = InflaterDynHeader.repBits[this.repSymbol];
				int num2 = input.PeekBits(num);
				bool flag = num2 < 0;
				if (flag)
				{
					goto Block_11;
				}
				input.DropBits(num);
				num2 += InflaterDynHeader.repMin[this.repSymbol];
				bool flag2 = this.ptr + num2 > this.num;
				if (flag2)
				{
					goto Block_12;
				}
				while (num2-- > 0)
				{
					byte[] array = this.litdistLens;
					int num3 = this.ptr;
					this.ptr = num3 + 1;
					array[num3] = this.lastLen;
				}
				bool flag3 = this.ptr == this.num;
				if (flag3)
				{
					goto Block_14;
				}
				this.mode = 4;
				continue;
				IL_01BB:
				int symbol;
				while (((symbol = this.blTree.GetSymbol(input)) & -16) == 0)
				{
					byte[] array2 = this.litdistLens;
					int num3 = this.ptr;
					this.ptr = num3 + 1;
					array2[num3] = (this.lastLen = (byte)symbol);
					bool flag4 = this.ptr == this.num;
					if (flag4)
					{
						goto Block_6;
					}
				}
				bool flag5 = symbol < 0;
				if (flag5)
				{
					goto Block_8;
				}
				bool flag6 = symbol >= 17;
				if (flag6)
				{
					this.lastLen = 0;
				}
				else
				{
					bool flag7 = this.ptr == 0;
					if (flag7)
					{
						goto Block_10;
					}
				}
				this.repSymbol = symbol - 16;
				this.mode = 5;
				goto IL_027A;
				IL_0133:
				while (this.ptr < this.blnum)
				{
					int num4 = input.PeekBits(3);
					bool flag8 = num4 < 0;
					if (flag8)
					{
						goto Block_4;
					}
					input.DropBits(3);
					this.blLens[InflaterDynHeader.BL_ORDER[this.ptr]] = (byte)num4;
					this.ptr++;
				}
				this.blTree = new InflaterHuffmanTree(this.blLens);
				this.blLens = null;
				this.ptr = 0;
				this.mode = 4;
				goto IL_01BB;
				IL_00DC:
				this.blnum = input.PeekBits(4);
				bool flag9 = this.blnum < 0;
				if (flag9)
				{
					goto Block_3;
				}
				this.blnum += 4;
				input.DropBits(4);
				this.blLens = new byte[19];
				this.ptr = 0;
				this.mode = 3;
				goto IL_0133;
				IL_0077:
				this.dnum = input.PeekBits(5);
				bool flag10 = this.dnum < 0;
				if (flag10)
				{
					goto Block_2;
				}
				this.dnum++;
				input.DropBits(5);
				this.num = this.lnum + this.dnum;
				this.litdistLens = new byte[this.num];
				this.mode = 2;
				goto IL_00DC;
				IL_0032:
				this.lnum = input.PeekBits(5);
				bool flag11 = this.lnum < 0;
				if (flag11)
				{
					break;
				}
				this.lnum += 257;
				input.DropBits(5);
				this.mode = 1;
				goto IL_0077;
			}
			return false;
			Block_2:
			return false;
			Block_3:
			return false;
			Block_4:
			return false;
			Block_6:
			return true;
			Block_8:
			return false;
			Block_10:
			throw new SharpZipBaseException();
			Block_11:
			return false;
			Block_12:
			throw new SharpZipBaseException();
			Block_14:
			return true;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002BD64 File Offset: 0x00029F64
		public InflaterHuffmanTree BuildLitLenTree()
		{
			byte[] array = new byte[this.lnum];
			Array.Copy(this.litdistLens, 0, array, 0, this.lnum);
			return new InflaterHuffmanTree(array);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		public InflaterHuffmanTree BuildDistTree()
		{
			byte[] array = new byte[this.dnum];
			Array.Copy(this.litdistLens, this.lnum, array, 0, this.dnum);
			return new InflaterHuffmanTree(array);
		}

		// Token: 0x04000442 RID: 1090
		private const int LNUM = 0;

		// Token: 0x04000443 RID: 1091
		private const int DNUM = 1;

		// Token: 0x04000444 RID: 1092
		private const int BLNUM = 2;

		// Token: 0x04000445 RID: 1093
		private const int BLLENS = 3;

		// Token: 0x04000446 RID: 1094
		private const int LENS = 4;

		// Token: 0x04000447 RID: 1095
		private const int REPS = 5;

		// Token: 0x04000448 RID: 1096
		private static readonly int[] repMin = new int[] { 3, 3, 11 };

		// Token: 0x04000449 RID: 1097
		private static readonly int[] repBits = new int[] { 2, 3, 7 };

		// Token: 0x0400044A RID: 1098
		private static readonly int[] BL_ORDER = new int[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x0400044B RID: 1099
		private byte[] blLens;

		// Token: 0x0400044C RID: 1100
		private byte[] litdistLens;

		// Token: 0x0400044D RID: 1101
		private InflaterHuffmanTree blTree;

		// Token: 0x0400044E RID: 1102
		private int mode;

		// Token: 0x0400044F RID: 1103
		private int lnum;

		// Token: 0x04000450 RID: 1104
		private int dnum;

		// Token: 0x04000451 RID: 1105
		private int blnum;

		// Token: 0x04000452 RID: 1106
		private int num;

		// Token: 0x04000453 RID: 1107
		private int repSymbol;

		// Token: 0x04000454 RID: 1108
		private byte lastLen;

		// Token: 0x04000455 RID: 1109
		private int ptr;
	}
}
