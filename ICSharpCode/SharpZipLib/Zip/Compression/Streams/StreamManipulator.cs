using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200008F RID: 143
	public class StreamManipulator
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0002D92C File Offset: 0x0002BB2C
		public int PeekBits(int bitCount)
		{
			bool flag = this.bitsInBuffer_ < bitCount;
			if (flag)
			{
				bool flag2 = this.windowStart_ == this.windowEnd_;
				if (flag2)
				{
					return -1;
				}
				uint num = this.buffer_;
				byte[] array = this.window_;
				int num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				uint num3 = array[num2] & 255U;
				byte[] array2 = this.window_;
				num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				this.buffer_ = num | ((num3 | ((array2[num2] & 255U) << 8)) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 16;
			}
			return (int)((ulong)this.buffer_ & (ulong)((long)((1 << bitCount) - 1)));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0002D9DB File Offset: 0x0002BBDB
		public void DropBits(int bitCount)
		{
			this.buffer_ >>= bitCount;
			this.bitsInBuffer_ -= bitCount;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0002DA00 File Offset: 0x0002BC00
		public int GetBits(int bitCount)
		{
			int num = this.PeekBits(bitCount);
			bool flag = num >= 0;
			if (flag)
			{
				this.DropBits(bitCount);
			}
			return num;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer_;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0002DA48 File Offset: 0x0002BC48
		public int AvailableBytes
		{
			get
			{
				return this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0002DA70 File Offset: 0x0002BC70
		public void SkipToByteBoundary()
		{
			this.buffer_ >>= this.bitsInBuffer_ & 7;
			this.bitsInBuffer_ &= -8;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0002DA9C File Offset: 0x0002BC9C
		public bool IsNeedingInput
		{
			get
			{
				return this.windowStart_ == this.windowEnd_;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0002DABC File Offset: 0x0002BCBC
		public int CopyBytes(byte[] output, int offset, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			bool flag2 = (this.bitsInBuffer_ & 7) != 0;
			if (flag2)
			{
				throw new InvalidOperationException("Bit buffer is not byte aligned!");
			}
			int num = 0;
			while (this.bitsInBuffer_ > 0 && length > 0)
			{
				output[offset++] = (byte)this.buffer_;
				this.buffer_ >>= 8;
				this.bitsInBuffer_ -= 8;
				length--;
				num++;
			}
			bool flag3 = length == 0;
			int num2;
			if (flag3)
			{
				num2 = num;
			}
			else
			{
				int num3 = this.windowEnd_ - this.windowStart_;
				bool flag4 = length > num3;
				if (flag4)
				{
					length = num3;
				}
				Array.Copy(this.window_, this.windowStart_, output, offset, length);
				this.windowStart_ += length;
				bool flag5 = ((this.windowStart_ - this.windowEnd_) & 1) != 0;
				if (flag5)
				{
					byte[] array = this.window_;
					int num4 = this.windowStart_;
					this.windowStart_ = num4 + 1;
					this.buffer_ = array[num4] & 255U;
					this.bitsInBuffer_ = 8;
				}
				num2 = num + length;
			}
			return num2;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		public void Reset()
		{
			this.buffer_ = 0U;
			this.windowStart_ = (this.windowEnd_ = (this.bitsInBuffer_ = 0));
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0002DC20 File Offset: 0x0002BE20
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
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			bool flag4 = this.windowStart_ < this.windowEnd_;
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
			bool flag6 = (count & 1) != 0;
			if (flag6)
			{
				this.buffer_ |= (uint)((uint)(buffer[offset++] & byte.MaxValue) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 8;
			}
			this.window_ = buffer;
			this.windowStart_ = offset;
			this.windowEnd_ = num;
		}

		// Token: 0x0400047B RID: 1147
		private byte[] window_;

		// Token: 0x0400047C RID: 1148
		private int windowStart_;

		// Token: 0x0400047D RID: 1149
		private int windowEnd_;

		// Token: 0x0400047E RID: 1150
		private uint buffer_;

		// Token: 0x0400047F RID: 1151
		private int bitsInBuffer_;
	}
}
