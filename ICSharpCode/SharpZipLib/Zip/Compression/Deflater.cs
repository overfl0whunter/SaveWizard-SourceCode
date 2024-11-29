using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000081 RID: 129
	public class Deflater
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x00028ECC File Offset: 0x000270CC
		public Deflater()
			: this(-1, false)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00028ED8 File Offset: 0x000270D8
		public Deflater(int level)
			: this(level, false)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00028EE4 File Offset: 0x000270E4
		public Deflater(int level, bool noZlibHeaderOrFooter)
		{
			bool flag = level == -1;
			if (flag)
			{
				level = 6;
			}
			else
			{
				bool flag2 = level < 0 || level > 9;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("level");
				}
			}
			this.pending = new DeflaterPending();
			this.engine = new DeflaterEngine(this.pending);
			this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
			this.SetStrategy(DeflateStrategy.Default);
			this.SetLevel(level);
			this.Reset();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00028F5E File Offset: 0x0002715E
		public void Reset()
		{
			this.state = (this.noZlibHeaderOrFooter ? 16 : 0);
			this.totalOut = 0L;
			this.pending.Reset();
			this.engine.Reset();
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00028F94 File Offset: 0x00027194
		public int Adler
		{
			get
			{
				return this.engine.Adler;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00028FB4 File Offset: 0x000271B4
		public long TotalIn
		{
			get
			{
				return this.engine.TotalIn;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00028FD4 File Offset: 0x000271D4
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00028FEC File Offset: 0x000271EC
		public void Flush()
		{
			this.state |= 4;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00028FFD File Offset: 0x000271FD
		public void Finish()
		{
			this.state |= 12;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00029010 File Offset: 0x00027210
		public bool IsFinished
		{
			get
			{
				return this.state == 30 && this.pending.IsFlushed;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0002903C File Offset: 0x0002723C
		public bool IsNeedingInput
		{
			get
			{
				return this.engine.NeedsInput();
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00029059 File Offset: 0x00027259
		public void SetInput(byte[] input)
		{
			this.SetInput(input, 0, input.Length);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00029068 File Offset: 0x00027268
		public void SetInput(byte[] input, int offset, int count)
		{
			bool flag = (this.state & 8) != 0;
			if (flag)
			{
				throw new InvalidOperationException("Finish() already called");
			}
			this.engine.SetInput(input, offset, count);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000290A0 File Offset: 0x000272A0
		public void SetLevel(int level)
		{
			bool flag = level == -1;
			if (flag)
			{
				level = 6;
			}
			else
			{
				bool flag2 = level < 0 || level > 9;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("level");
				}
			}
			bool flag3 = this.level != level;
			if (flag3)
			{
				this.level = level;
				this.engine.SetLevel(level);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00029100 File Offset: 0x00027300
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00029118 File Offset: 0x00027318
		public void SetStrategy(DeflateStrategy strategy)
		{
			this.engine.Strategy = strategy;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00029128 File Offset: 0x00027328
		public int Deflate(byte[] output)
		{
			return this.Deflate(output, 0, output.Length);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00029148 File Offset: 0x00027348
		public int Deflate(byte[] output, int offset, int length)
		{
			int num = length;
			bool flag = this.state == 127;
			if (flag)
			{
				throw new InvalidOperationException("Deflater closed");
			}
			bool flag2 = this.state < 16;
			if (flag2)
			{
				int num2 = 30720;
				int num3 = this.level - 1 >> 1;
				bool flag3 = num3 < 0 || num3 > 3;
				if (flag3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				bool flag4 = (this.state & 1) != 0;
				if (flag4)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.pending.WriteShortMSB(num2);
				bool flag5 = (this.state & 1) != 0;
				if (flag5)
				{
					int adler = this.engine.Adler;
					this.engine.ResetAdler();
					this.pending.WriteShortMSB(adler >> 16);
					this.pending.WriteShortMSB(adler & 65535);
				}
				this.state = 16 | (this.state & 12);
			}
			for (;;)
			{
				int num4 = this.pending.Flush(output, offset, length);
				offset += num4;
				this.totalOut += (long)num4;
				length -= num4;
				bool flag6 = length == 0 || this.state == 30;
				if (flag6)
				{
					break;
				}
				bool flag7 = !this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0);
				if (flag7)
				{
					bool flag8 = this.state == 16;
					if (flag8)
					{
						goto Block_10;
					}
					bool flag9 = this.state == 20;
					if (flag9)
					{
						bool flag10 = this.level != 0;
						if (flag10)
						{
							for (int i = 8 + (-this.pending.BitCount & 7); i > 0; i -= 10)
							{
								this.pending.WriteBits(2, 10);
							}
						}
						this.state = 16;
					}
					else
					{
						bool flag11 = this.state == 28;
						if (flag11)
						{
							this.pending.AlignToByte();
							bool flag12 = !this.noZlibHeaderOrFooter;
							if (flag12)
							{
								int adler2 = this.engine.Adler;
								this.pending.WriteShortMSB(adler2 >> 16);
								this.pending.WriteShortMSB(adler2 & 65535);
							}
							this.state = 30;
						}
					}
				}
			}
			return num - length;
			Block_10:
			return num - length;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000293BC File Offset: 0x000275BC
		public void SetDictionary(byte[] dictionary)
		{
			this.SetDictionary(dictionary, 0, dictionary.Length);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000293CC File Offset: 0x000275CC
		public void SetDictionary(byte[] dictionary, int index, int count)
		{
			bool flag = this.state != 0;
			if (flag)
			{
				throw new InvalidOperationException();
			}
			this.state = 1;
			this.engine.SetDictionary(dictionary, index, count);
		}

		// Token: 0x040003BF RID: 959
		public const int BEST_COMPRESSION = 9;

		// Token: 0x040003C0 RID: 960
		public const int BEST_SPEED = 1;

		// Token: 0x040003C1 RID: 961
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x040003C2 RID: 962
		public const int NO_COMPRESSION = 0;

		// Token: 0x040003C3 RID: 963
		public const int DEFLATED = 8;

		// Token: 0x040003C4 RID: 964
		private const int IS_SETDICT = 1;

		// Token: 0x040003C5 RID: 965
		private const int IS_FLUSHING = 4;

		// Token: 0x040003C6 RID: 966
		private const int IS_FINISHING = 8;

		// Token: 0x040003C7 RID: 967
		private const int INIT_STATE = 0;

		// Token: 0x040003C8 RID: 968
		private const int SETDICT_STATE = 1;

		// Token: 0x040003C9 RID: 969
		private const int BUSY_STATE = 16;

		// Token: 0x040003CA RID: 970
		private const int FLUSHING_STATE = 20;

		// Token: 0x040003CB RID: 971
		private const int FINISHING_STATE = 28;

		// Token: 0x040003CC RID: 972
		private const int FINISHED_STATE = 30;

		// Token: 0x040003CD RID: 973
		private const int CLOSED_STATE = 127;

		// Token: 0x040003CE RID: 974
		private int level;

		// Token: 0x040003CF RID: 975
		private bool noZlibHeaderOrFooter;

		// Token: 0x040003D0 RID: 976
		private int state;

		// Token: 0x040003D1 RID: 977
		private long totalOut;

		// Token: 0x040003D2 RID: 978
		private DeflaterPending pending;

		// Token: 0x040003D3 RID: 979
		private DeflaterEngine engine;
	}
}
