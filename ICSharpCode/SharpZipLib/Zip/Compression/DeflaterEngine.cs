﻿using System;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000084 RID: 132
	public class DeflaterEngine : DeflaterConstants
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x00029498 File Offset: 0x00027698
		public DeflaterEngine(DeflaterPending pending)
		{
			this.pending = pending;
			this.huffman = new DeflaterHuffman(pending);
			this.adler = new Adler32();
			this.window = new byte[65536];
			this.head = new short[32768];
			this.prev = new short[32768];
			this.blockStart = (this.strstart = 1);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002950C File Offset: 0x0002770C
		public bool Deflate(bool flush, bool finish)
		{
			for (;;)
			{
				this.FillWindow();
				bool flag = flush && this.inputOff == this.inputEnd;
				bool flag2;
				switch (this.compressionFunction)
				{
				case 0:
					flag2 = this.DeflateStored(flag, finish);
					goto IL_0065;
				case 1:
					flag2 = this.DeflateFast(flag, finish);
					goto IL_0065;
				case 2:
					flag2 = this.DeflateSlow(flag, finish);
					goto IL_0065;
				}
				break;
				IL_0065:
				if (!this.pending.IsFlushed || !flag2)
				{
					return flag2;
				}
			}
			throw new InvalidOperationException("unknown compressionFunction");
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00029598 File Offset: 0x00027798
		public void SetInput(byte[] buffer, int offset, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool flag4 = this.inputOff < this.inputEnd;
			if (flag4)
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			int num = offset + count;
			bool flag5 = offset > num || num > buffer.Length;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.inputBuf = buffer;
			this.inputOff = offset;
			this.inputEnd = num;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002963C File Offset: 0x0002783C
		public bool NeedsInput()
		{
			return this.inputEnd == this.inputOff;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002965C File Offset: 0x0002785C
		public void SetDictionary(byte[] buffer, int offset, int length)
		{
			this.adler.Update(buffer, offset, length);
			bool flag = length < 3;
			if (!flag)
			{
				bool flag2 = length > 32506;
				if (flag2)
				{
					offset += length - 32506;
					length = 32506;
				}
				Array.Copy(buffer, offset, this.window, this.strstart, length);
				this.UpdateHash();
				length--;
				while (--length > 0)
				{
					this.InsertString();
					this.strstart++;
				}
				this.strstart += 2;
				this.blockStart = this.strstart;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00029708 File Offset: 0x00027908
		public void Reset()
		{
			this.huffman.Reset();
			this.adler.Reset();
			this.blockStart = (this.strstart = 1);
			this.lookahead = 0;
			this.totalIn = 0L;
			this.prevAvailable = false;
			this.matchLen = 2;
			for (int i = 0; i < 32768; i++)
			{
				this.head[i] = 0;
			}
			for (int j = 0; j < 32768; j++)
			{
				this.prev[j] = 0;
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002979B File Offset: 0x0002799B
		public void ResetAdler()
		{
			this.adler.Reset();
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000297AC File Offset: 0x000279AC
		public int Adler
		{
			get
			{
				return (int)this.adler.Value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000297CC File Offset: 0x000279CC
		public long TotalIn
		{
			get
			{
				return this.totalIn;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000297E4 File Offset: 0x000279E4
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x000297FC File Offset: 0x000279FC
		public DeflateStrategy Strategy
		{
			get
			{
				return this.strategy;
			}
			set
			{
				this.strategy = value;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00029808 File Offset: 0x00027A08
		public void SetLevel(int level)
		{
			bool flag = level < 0 || level > 9;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.goodLength = DeflaterConstants.GOOD_LENGTH[level];
			this.max_lazy = DeflaterConstants.MAX_LAZY[level];
			this.niceLength = DeflaterConstants.NICE_LENGTH[level];
			this.max_chain = DeflaterConstants.MAX_CHAIN[level];
			bool flag2 = DeflaterConstants.COMPR_FUNC[level] != this.compressionFunction;
			if (flag2)
			{
				switch (this.compressionFunction)
				{
				case 0:
				{
					bool flag3 = this.strstart > this.blockStart;
					if (flag3)
					{
						this.huffman.FlushStoredBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.UpdateHash();
					break;
				}
				case 1:
				{
					bool flag4 = this.strstart > this.blockStart;
					if (flag4)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					break;
				}
				case 2:
				{
					bool flag5 = this.prevAvailable;
					if (flag5)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					bool flag6 = this.strstart > this.blockStart;
					if (flag6)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.prevAvailable = false;
					this.matchLen = 2;
					break;
				}
				}
				this.compressionFunction = DeflaterConstants.COMPR_FUNC[level];
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000299D4 File Offset: 0x00027BD4
		public void FillWindow()
		{
			bool flag = this.strstart >= 65274;
			if (flag)
			{
				this.SlideWindow();
			}
			while (this.lookahead < 262 && this.inputOff < this.inputEnd)
			{
				int num = 65536 - this.lookahead - this.strstart;
				bool flag2 = num > this.inputEnd - this.inputOff;
				if (flag2)
				{
					num = this.inputEnd - this.inputOff;
				}
				Array.Copy(this.inputBuf, this.inputOff, this.window, this.strstart + this.lookahead, num);
				this.adler.Update(this.inputBuf, this.inputOff, num);
				this.inputOff += num;
				this.totalIn += (long)num;
				this.lookahead += num;
			}
			bool flag3 = this.lookahead >= 3;
			if (flag3)
			{
				this.UpdateHash();
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00029AE8 File Offset: 0x00027CE8
		private void UpdateHash()
		{
			this.ins_h = ((int)this.window[this.strstart] << 5) ^ (int)this.window[this.strstart + 1];
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00029B10 File Offset: 0x00027D10
		private int InsertString()
		{
			int num = ((this.ins_h << 5) ^ (int)this.window[this.strstart + 2]) & 32767;
			short num2 = (this.prev[this.strstart & 32767] = this.head[num]);
			this.head[num] = (short)this.strstart;
			this.ins_h = num;
			return (int)num2 & 65535;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00029B7C File Offset: 0x00027D7C
		private void SlideWindow()
		{
			Array.Copy(this.window, 32768, this.window, 0, 32768);
			this.matchStart -= 32768;
			this.strstart -= 32768;
			this.blockStart -= 32768;
			for (int i = 0; i < 32768; i++)
			{
				int num = (int)this.head[i] & 65535;
				this.head[i] = (short)((num >= 32768) ? (num - 32768) : 0);
			}
			for (int j = 0; j < 32768; j++)
			{
				int num2 = (int)this.prev[j] & 65535;
				this.prev[j] = (short)((num2 >= 32768) ? (num2 - 32768) : 0);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00029C64 File Offset: 0x00027E64
		private bool FindLongestMatch(int curMatch)
		{
			int num = this.max_chain;
			int num2 = this.niceLength;
			short[] array = this.prev;
			int num3 = this.strstart;
			int num4 = this.strstart + this.matchLen;
			int num5 = Math.Max(this.matchLen, 2);
			int num6 = Math.Max(this.strstart - 32506, 0);
			int num7 = this.strstart + 258 - 1;
			byte b = this.window[num4 - 1];
			byte b2 = this.window[num4];
			bool flag = num5 >= this.goodLength;
			if (flag)
			{
				num >>= 2;
			}
			bool flag2 = num2 > this.lookahead;
			if (flag2)
			{
				num2 = this.lookahead;
			}
			do
			{
				bool flag3 = this.window[curMatch + num5] != b2 || this.window[curMatch + num5 - 1] != b || this.window[curMatch] != this.window[num3] || this.window[curMatch + 1] != this.window[num3 + 1];
				if (!flag3)
				{
					int num8 = curMatch + 2;
					num3 += 2;
					while (this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && this.window[++num3] == this.window[++num8] && num3 < num7)
					{
					}
					bool flag4 = num3 > num4;
					if (flag4)
					{
						this.matchStart = curMatch;
						num4 = num3;
						num5 = num3 - this.strstart;
						bool flag5 = num5 >= num2;
						if (flag5)
						{
							break;
						}
						b = this.window[num4 - 1];
						b2 = this.window[num4];
					}
					num3 = this.strstart;
				}
			}
			while ((curMatch = (int)array[curMatch & 32767] & 65535) > num6 && --num != 0);
			this.matchLen = Math.Min(num5, this.lookahead);
			return this.matchLen >= 3;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00029F1C File Offset: 0x0002811C
		private bool DeflateStored(bool flush, bool finish)
		{
			bool flag = !flush && this.lookahead == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num = this.strstart - this.blockStart;
				bool flag3 = num >= DeflaterConstants.MAX_BLOCK_SIZE || (this.blockStart < 32768 && num >= 32506) || flush;
				if (flag3)
				{
					bool flag4 = finish;
					bool flag5 = num > DeflaterConstants.MAX_BLOCK_SIZE;
					if (flag5)
					{
						num = DeflaterConstants.MAX_BLOCK_SIZE;
						flag4 = false;
					}
					this.huffman.FlushStoredBlock(this.window, this.blockStart, num, flag4);
					this.blockStart += num;
					flag2 = !flag4;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00029FEC File Offset: 0x000281EC
		private bool DeflateFast(bool flush, bool finish)
		{
			bool flag = this.lookahead < 262 && !flush;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				while (this.lookahead >= 262 || flush)
				{
					bool flag3 = this.lookahead == 0;
					if (flag3)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
						this.blockStart = this.strstart;
						return false;
					}
					bool flag4 = this.strstart > 65274;
					if (flag4)
					{
						this.SlideWindow();
					}
					int num;
					bool flag5 = this.lookahead >= 3 && (num = this.InsertString()) != 0 && this.strategy != DeflateStrategy.HuffmanOnly && this.strstart - num <= 32506 && this.FindLongestMatch(num);
					if (flag5)
					{
						bool flag6 = this.huffman.TallyDist(this.strstart - this.matchStart, this.matchLen);
						this.lookahead -= this.matchLen;
						bool flag7 = this.matchLen <= this.max_lazy && this.lookahead >= 3;
						if (flag7)
						{
							for (;;)
							{
								int num2 = this.matchLen - 1;
								this.matchLen = num2;
								if (num2 <= 0)
								{
									break;
								}
								this.strstart++;
								this.InsertString();
							}
							this.strstart++;
						}
						else
						{
							this.strstart += this.matchLen;
							bool flag8 = this.lookahead >= 2;
							if (flag8)
							{
								this.UpdateHash();
							}
						}
						this.matchLen = 2;
						bool flag9 = !flag6;
						if (flag9)
						{
							continue;
						}
					}
					else
					{
						this.huffman.TallyLit((int)(this.window[this.strstart] & byte.MaxValue));
						this.strstart++;
						this.lookahead--;
					}
					bool flag10 = this.huffman.IsFull();
					if (flag10)
					{
						bool flag11 = finish && this.lookahead == 0;
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, flag11);
						this.blockStart = this.strstart;
						return !flag11;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002A268 File Offset: 0x00028468
		private bool DeflateSlow(bool flush, bool finish)
		{
			bool flag = this.lookahead < 262 && !flush;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				while (this.lookahead >= 262 || flush)
				{
					bool flag3 = this.lookahead == 0;
					if (flag3)
					{
						bool flag4 = this.prevAvailable;
						if (flag4)
						{
							this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
						}
						this.prevAvailable = false;
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
						this.blockStart = this.strstart;
						return false;
					}
					bool flag5 = this.strstart >= 65274;
					if (flag5)
					{
						this.SlideWindow();
					}
					int num = this.matchStart;
					int num2 = this.matchLen;
					bool flag6 = this.lookahead >= 3;
					if (flag6)
					{
						int num3 = this.InsertString();
						bool flag7 = this.strategy != DeflateStrategy.HuffmanOnly && num3 != 0 && this.strstart - num3 <= 32506 && this.FindLongestMatch(num3);
						if (flag7)
						{
							bool flag8 = this.matchLen <= 5 && (this.strategy == DeflateStrategy.Filtered || (this.matchLen == 3 && this.strstart - this.matchStart > 4096));
							if (flag8)
							{
								this.matchLen = 2;
							}
						}
					}
					bool flag9 = num2 >= 3 && this.matchLen <= num2;
					if (flag9)
					{
						this.huffman.TallyDist(this.strstart - 1 - num, num2);
						num2 -= 2;
						do
						{
							this.strstart++;
							this.lookahead--;
							bool flag10 = this.lookahead >= 3;
							if (flag10)
							{
								this.InsertString();
							}
						}
						while (--num2 > 0);
						this.strstart++;
						this.lookahead--;
						this.prevAvailable = false;
						this.matchLen = 2;
					}
					else
					{
						bool flag11 = this.prevAvailable;
						if (flag11)
						{
							this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
						}
						this.prevAvailable = true;
						this.strstart++;
						this.lookahead--;
					}
					bool flag12 = this.huffman.IsFull();
					if (flag12)
					{
						int num4 = this.strstart - this.blockStart;
						bool flag13 = this.prevAvailable;
						if (flag13)
						{
							num4--;
						}
						bool flag14 = finish && this.lookahead == 0 && !this.prevAvailable;
						this.huffman.FlushBlock(this.window, this.blockStart, num4, flag14);
						this.blockStart += num4;
						return !flag14;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x040003F3 RID: 1011
		private const int TooFar = 4096;

		// Token: 0x040003F4 RID: 1012
		private int ins_h;

		// Token: 0x040003F5 RID: 1013
		private short[] head;

		// Token: 0x040003F6 RID: 1014
		private short[] prev;

		// Token: 0x040003F7 RID: 1015
		private int matchStart;

		// Token: 0x040003F8 RID: 1016
		private int matchLen;

		// Token: 0x040003F9 RID: 1017
		private bool prevAvailable;

		// Token: 0x040003FA RID: 1018
		private int blockStart;

		// Token: 0x040003FB RID: 1019
		private int strstart;

		// Token: 0x040003FC RID: 1020
		private int lookahead;

		// Token: 0x040003FD RID: 1021
		private byte[] window;

		// Token: 0x040003FE RID: 1022
		private DeflateStrategy strategy;

		// Token: 0x040003FF RID: 1023
		private int max_chain;

		// Token: 0x04000400 RID: 1024
		private int max_lazy;

		// Token: 0x04000401 RID: 1025
		private int niceLength;

		// Token: 0x04000402 RID: 1026
		private int goodLength;

		// Token: 0x04000403 RID: 1027
		private int compressionFunction;

		// Token: 0x04000404 RID: 1028
		private byte[] inputBuf;

		// Token: 0x04000405 RID: 1029
		private long totalIn;

		// Token: 0x04000406 RID: 1030
		private int inputOff;

		// Token: 0x04000407 RID: 1031
		private int inputEnd;

		// Token: 0x04000408 RID: 1032
		private DeflaterPending pending;

		// Token: 0x04000409 RID: 1033
		private DeflaterHuffman huffman;

		// Token: 0x0400040A RID: 1034
		private Adler32 adler;
	}
}
