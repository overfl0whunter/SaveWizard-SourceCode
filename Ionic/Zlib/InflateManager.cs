using System;

namespace Ionic.Zlib
{
	// Token: 0x02000017 RID: 23
	internal sealed class InflateManager
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000A0C0 File Offset: 0x000082C0
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000A0D8 File Offset: 0x000082D8
		internal bool HandleRfc1950HeaderBytes
		{
			get
			{
				return this._handleRfc1950HeaderBytes;
			}
			set
			{
				this._handleRfc1950HeaderBytes = value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000A0E2 File Offset: 0x000082E2
		public InflateManager()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000A0F3 File Offset: 0x000082F3
		public InflateManager(bool expectRfc1950HeaderBytes)
		{
			this._handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000A10C File Offset: 0x0000830C
		internal int Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.mode = (this.HandleRfc1950HeaderBytes ? InflateManager.InflateManagerMode.METHOD : InflateManager.InflateManagerMode.BLOCKS);
			this.blocks.Reset();
			return 0;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000A164 File Offset: 0x00008364
		internal int End()
		{
			bool flag = this.blocks != null;
			if (flag)
			{
				this.blocks.Free();
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A198 File Offset: 0x00008398
		internal int Initialize(ZlibCodec codec, int w)
		{
			this._codec = codec;
			this._codec.Message = null;
			this.blocks = null;
			bool flag = w < 8 || w > 15;
			if (flag)
			{
				this.End();
				throw new ZlibException("Bad window size.");
			}
			this.wbits = w;
			this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? this : null, 1 << w);
			this.Reset();
			return 0;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A214 File Offset: 0x00008414
		internal int Inflate(FlushType flush)
		{
			bool flag = this._codec.InputBuffer == null;
			if (flag)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			int num3;
			for (;;)
			{
				switch (this.mode)
				{
				case InflateManager.InflateManagerMode.METHOD:
				{
					bool flag2 = this._codec.AvailableBytesIn == 0;
					if (flag2)
					{
						goto Block_3;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer = this._codec.InputBuffer;
					ZlibCodec codec = this._codec;
					num3 = codec.NextIn;
					codec.NextIn = num3 + 1;
					bool flag3 = ((this.method = inputBuffer[num3]) & 15) != 8;
					if (flag3)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("unknown compression method (0x{0:X2})", this.method);
						this.marker = 5;
						continue;
					}
					bool flag4 = (this.method >> 4) + 8 > this.wbits;
					if (flag4)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("invalid window size ({0})", (this.method >> 4) + 8);
						this.marker = 5;
						continue;
					}
					this.mode = InflateManager.InflateManagerMode.FLAG;
					continue;
				}
				case InflateManager.InflateManagerMode.FLAG:
				{
					bool flag5 = this._codec.AvailableBytesIn == 0;
					if (flag5)
					{
						goto Block_6;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer2 = this._codec.InputBuffer;
					ZlibCodec codec2 = this._codec;
					num3 = codec2.NextIn;
					codec2.NextIn = num3 + 1;
					int num4 = inputBuffer2[num3] & 255;
					bool flag6 = ((this.method << 8) + num4) % 31 != 0;
					if (flag6)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect header check";
						this.marker = 5;
						continue;
					}
					this.mode = (((num4 & 32) == 0) ? InflateManager.InflateManagerMode.BLOCKS : InflateManager.InflateManagerMode.DICT4);
					continue;
				}
				case InflateManager.InflateManagerMode.DICT4:
				{
					bool flag7 = this._codec.AvailableBytesIn == 0;
					if (flag7)
					{
						goto Block_9;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer3 = this._codec.InputBuffer;
					ZlibCodec codec3 = this._codec;
					num3 = codec3.NextIn;
					codec3.NextIn = num3 + 1;
					this.expectedCheck = (uint)((inputBuffer3[num3] << 24) & (long)((ulong)(-16777216)));
					this.mode = InflateManager.InflateManagerMode.DICT3;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT3:
				{
					bool flag8 = this._codec.AvailableBytesIn == 0;
					if (flag8)
					{
						goto Block_10;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num5 = this.expectedCheck;
					byte[] inputBuffer4 = this._codec.InputBuffer;
					ZlibCodec codec4 = this._codec;
					num3 = codec4.NextIn;
					codec4.NextIn = num3 + 1;
					this.expectedCheck = num5 + ((inputBuffer4[num3] << 16) & 16711680U);
					this.mode = InflateManager.InflateManagerMode.DICT2;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT2:
				{
					bool flag9 = this._codec.AvailableBytesIn == 0;
					if (flag9)
					{
						goto Block_11;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num6 = this.expectedCheck;
					byte[] inputBuffer5 = this._codec.InputBuffer;
					ZlibCodec codec5 = this._codec;
					num3 = codec5.NextIn;
					codec5.NextIn = num3 + 1;
					this.expectedCheck = num6 + ((inputBuffer5[num3] << 8) & 65280U);
					this.mode = InflateManager.InflateManagerMode.DICT1;
					continue;
				}
				case InflateManager.InflateManagerMode.DICT1:
					goto IL_03EB;
				case InflateManager.InflateManagerMode.DICT0:
					goto IL_0488;
				case InflateManager.InflateManagerMode.BLOCKS:
				{
					num2 = this.blocks.Process(num2);
					bool flag10 = num2 == -3;
					if (flag10)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this.marker = 0;
						continue;
					}
					bool flag11 = num2 == 0;
					if (flag11)
					{
						num2 = num;
					}
					bool flag12 = num2 != 1;
					if (flag12)
					{
						goto Block_15;
					}
					num2 = num;
					this.computedCheck = this.blocks.Reset();
					bool flag13 = !this.HandleRfc1950HeaderBytes;
					if (flag13)
					{
						goto Block_16;
					}
					this.mode = InflateManager.InflateManagerMode.CHECK4;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK4:
				{
					bool flag14 = this._codec.AvailableBytesIn == 0;
					if (flag14)
					{
						goto Block_17;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					byte[] inputBuffer6 = this._codec.InputBuffer;
					ZlibCodec codec6 = this._codec;
					num3 = codec6.NextIn;
					codec6.NextIn = num3 + 1;
					this.expectedCheck = (uint)((inputBuffer6[num3] << 24) & (long)((ulong)(-16777216)));
					this.mode = InflateManager.InflateManagerMode.CHECK3;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK3:
				{
					bool flag15 = this._codec.AvailableBytesIn == 0;
					if (flag15)
					{
						goto Block_18;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num7 = this.expectedCheck;
					byte[] inputBuffer7 = this._codec.InputBuffer;
					ZlibCodec codec7 = this._codec;
					num3 = codec7.NextIn;
					codec7.NextIn = num3 + 1;
					this.expectedCheck = num7 + ((inputBuffer7[num3] << 16) & 16711680U);
					this.mode = InflateManager.InflateManagerMode.CHECK2;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK2:
				{
					bool flag16 = this._codec.AvailableBytesIn == 0;
					if (flag16)
					{
						goto Block_19;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num8 = this.expectedCheck;
					byte[] inputBuffer8 = this._codec.InputBuffer;
					ZlibCodec codec8 = this._codec;
					num3 = codec8.NextIn;
					codec8.NextIn = num3 + 1;
					this.expectedCheck = num8 + ((inputBuffer8[num3] << 8) & 65280U);
					this.mode = InflateManager.InflateManagerMode.CHECK1;
					continue;
				}
				case InflateManager.InflateManagerMode.CHECK1:
				{
					bool flag17 = this._codec.AvailableBytesIn == 0;
					if (flag17)
					{
						goto Block_20;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					uint num9 = this.expectedCheck;
					byte[] inputBuffer9 = this._codec.InputBuffer;
					ZlibCodec codec9 = this._codec;
					num3 = codec9.NextIn;
					codec9.NextIn = num3 + 1;
					this.expectedCheck = num9 + (inputBuffer9[num3] & 255U);
					bool flag18 = this.computedCheck != this.expectedCheck;
					if (flag18)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect data check";
						this.marker = 5;
						continue;
					}
					goto IL_0795;
				}
				case InflateManager.InflateManagerMode.DONE:
					goto IL_07A2;
				case InflateManager.InflateManagerMode.BAD:
					goto IL_07A7;
				}
				break;
			}
			throw new ZlibException("Stream error.");
			Block_3:
			return num2;
			Block_6:
			return num2;
			Block_9:
			return num2;
			Block_10:
			return num2;
			Block_11:
			return num2;
			IL_03EB:
			bool flag19 = this._codec.AvailableBytesIn == 0;
			if (flag19)
			{
				return num2;
			}
			this._codec.AvailableBytesIn--;
			this._codec.TotalBytesIn += 1L;
			uint num10 = this.expectedCheck;
			byte[] inputBuffer10 = this._codec.InputBuffer;
			ZlibCodec codec10 = this._codec;
			num3 = codec10.NextIn;
			codec10.NextIn = num3 + 1;
			this.expectedCheck = num10 + (inputBuffer10[num3] & 255U);
			this._codec._Adler32 = this.expectedCheck;
			this.mode = InflateManager.InflateManagerMode.DICT0;
			return 2;
			IL_0488:
			this.mode = InflateManager.InflateManagerMode.BAD;
			this._codec.Message = "need dictionary";
			this.marker = 0;
			return -2;
			Block_15:
			return num2;
			Block_16:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			Block_17:
			return num2;
			Block_18:
			return num2;
			Block_19:
			return num2;
			Block_20:
			return num2;
			IL_0795:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			IL_07A2:
			return 1;
			IL_07A7:
			throw new ZlibException(string.Format("Bad state ({0})", this._codec.Message));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A9FC File Offset: 0x00008BFC
		internal int SetDictionary(byte[] dictionary)
		{
			int num = 0;
			int num2 = dictionary.Length;
			bool flag = this.mode != InflateManager.InflateManagerMode.DICT0;
			if (flag)
			{
				throw new ZlibException("Stream error.");
			}
			bool flag2 = Adler.Adler32(1U, dictionary, 0, dictionary.Length) != this._codec._Adler32;
			int num3;
			if (flag2)
			{
				num3 = -3;
			}
			else
			{
				this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
				bool flag3 = num2 >= 1 << this.wbits;
				if (flag3)
				{
					num2 = (1 << this.wbits) - 1;
					num = dictionary.Length - num2;
				}
				this.blocks.SetDictionary(dictionary, num, num2);
				this.mode = InflateManager.InflateManagerMode.BLOCKS;
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		internal int Sync()
		{
			bool flag = this.mode != InflateManager.InflateManagerMode.BAD;
			if (flag)
			{
				this.mode = InflateManager.InflateManagerMode.BAD;
				this.marker = 0;
			}
			int num;
			bool flag2 = (num = this._codec.AvailableBytesIn) == 0;
			int num2;
			if (flag2)
			{
				num2 = -5;
			}
			else
			{
				int num3 = this._codec.NextIn;
				int num4 = this.marker;
				while (num != 0 && num4 < 4)
				{
					bool flag3 = this._codec.InputBuffer[num3] == InflateManager.mark[num4];
					if (flag3)
					{
						num4++;
					}
					else
					{
						bool flag4 = this._codec.InputBuffer[num3] > 0;
						if (flag4)
						{
							num4 = 0;
						}
						else
						{
							num4 = 4 - num4;
						}
					}
					num3++;
					num--;
				}
				this._codec.TotalBytesIn += (long)(num3 - this._codec.NextIn);
				this._codec.NextIn = num3;
				this._codec.AvailableBytesIn = num;
				this.marker = num4;
				bool flag5 = num4 != 4;
				if (flag5)
				{
					num2 = -3;
				}
				else
				{
					long totalBytesIn = this._codec.TotalBytesIn;
					long totalBytesOut = this._codec.TotalBytesOut;
					this.Reset();
					this._codec.TotalBytesIn = totalBytesIn;
					this._codec.TotalBytesOut = totalBytesOut;
					this.mode = InflateManager.InflateManagerMode.BLOCKS;
					num2 = 0;
				}
			}
			return num2;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000AC10 File Offset: 0x00008E10
		internal int SyncPoint(ZlibCodec z)
		{
			return this.blocks.SyncPoint();
		}

		// Token: 0x040000B6 RID: 182
		private const int PRESET_DICT = 32;

		// Token: 0x040000B7 RID: 183
		private const int Z_DEFLATED = 8;

		// Token: 0x040000B8 RID: 184
		private InflateManager.InflateManagerMode mode;

		// Token: 0x040000B9 RID: 185
		internal ZlibCodec _codec;

		// Token: 0x040000BA RID: 186
		internal int method;

		// Token: 0x040000BB RID: 187
		internal uint computedCheck;

		// Token: 0x040000BC RID: 188
		internal uint expectedCheck;

		// Token: 0x040000BD RID: 189
		internal int marker;

		// Token: 0x040000BE RID: 190
		private bool _handleRfc1950HeaderBytes = true;

		// Token: 0x040000BF RID: 191
		internal int wbits;

		// Token: 0x040000C0 RID: 192
		internal InflateBlocks blocks;

		// Token: 0x040000C1 RID: 193
		private static readonly byte[] mark = new byte[] { 0, 0, byte.MaxValue, byte.MaxValue };

		// Token: 0x020001FD RID: 509
		private enum InflateManagerMode
		{
			// Token: 0x04000D6B RID: 3435
			METHOD,
			// Token: 0x04000D6C RID: 3436
			FLAG,
			// Token: 0x04000D6D RID: 3437
			DICT4,
			// Token: 0x04000D6E RID: 3438
			DICT3,
			// Token: 0x04000D6F RID: 3439
			DICT2,
			// Token: 0x04000D70 RID: 3440
			DICT1,
			// Token: 0x04000D71 RID: 3441
			DICT0,
			// Token: 0x04000D72 RID: 3442
			BLOCKS,
			// Token: 0x04000D73 RID: 3443
			CHECK4,
			// Token: 0x04000D74 RID: 3444
			CHECK3,
			// Token: 0x04000D75 RID: 3445
			CHECK2,
			// Token: 0x04000D76 RID: 3446
			CHECK1,
			// Token: 0x04000D77 RID: 3447
			DONE,
			// Token: 0x04000D78 RID: 3448
			BAD
		}
	}
}
