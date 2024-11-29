using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x0200008A RID: 138
	public class PendingBuffer
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x0002C297 File Offset: 0x0002A497
		public PendingBuffer()
			: this(4096)
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002C2A6 File Offset: 0x0002A4A6
		public PendingBuffer(int bufferSize)
		{
			this.buffer_ = new byte[bufferSize];
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		public void Reset()
		{
			this.start = (this.end = (this.bitCount = 0));
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0002C2E4 File Offset: 0x0002A4E4
		public void WriteByte(int value)
		{
			byte[] array = this.buffer_;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002C30C File Offset: 0x0002A50C
		public void WriteShort(int value)
		{
			byte[] array = this.buffer_;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer_;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002C350 File Offset: 0x0002A550
		public void WriteInt(int value)
		{
			byte[] array = this.buffer_;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer_;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer_;
			num = this.end;
			this.end = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer_;
			num = this.end;
			this.end = num + 1;
			array4[num] = (byte)(value >> 24);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002C3CE File Offset: 0x0002A5CE
		public void WriteBlock(byte[] block, int offset, int length)
		{
			Array.Copy(block, offset, this.buffer_, this.end, length);
			this.end += length;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
		public int BitCount
		{
			get
			{
				return this.bitCount;
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002C40C File Offset: 0x0002A60C
		public void AlignToByte()
		{
			bool flag = this.bitCount > 0;
			if (flag)
			{
				byte[] array = this.buffer_;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				bool flag2 = this.bitCount > 8;
				if (flag2)
				{
					byte[] array2 = this.buffer_;
					num = this.end;
					this.end = num + 1;
					array2[num] = (byte)(this.bits >> 8);
				}
			}
			this.bits = 0U;
			this.bitCount = 0;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002C488 File Offset: 0x0002A688
		public void WriteBits(int b, int count)
		{
			this.bits |= (uint)((uint)b << this.bitCount);
			this.bitCount += count;
			bool flag = this.bitCount >= 16;
			if (flag)
			{
				byte[] array = this.buffer_;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				byte[] array2 = this.buffer_;
				num = this.end;
				this.end = num + 1;
				array2[num] = (byte)(this.bits >> 8);
				this.bits >>= 16;
				this.bitCount -= 16;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002C530 File Offset: 0x0002A730
		public void WriteShortMSB(int s)
		{
			byte[] array = this.buffer_;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)(s >> 8);
			byte[] array2 = this.buffer_;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)s;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002C574 File Offset: 0x0002A774
		public bool IsFlushed
		{
			get
			{
				return this.end == 0;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0002C590 File Offset: 0x0002A790
		public int Flush(byte[] output, int offset, int length)
		{
			bool flag = this.bitCount >= 8;
			if (flag)
			{
				byte[] array = this.buffer_;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				this.bits >>= 8;
				this.bitCount -= 8;
			}
			bool flag2 = length > this.end - this.start;
			if (flag2)
			{
				length = this.end - this.start;
				Array.Copy(this.buffer_, this.start, output, offset, length);
				this.start = 0;
				this.end = 0;
			}
			else
			{
				Array.Copy(this.buffer_, this.start, output, offset, length);
				this.start += length;
			}
			return length;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0002C660 File Offset: 0x0002A860
		public byte[] ToByteArray()
		{
			byte[] array = new byte[this.end - this.start];
			Array.Copy(this.buffer_, this.start, array, 0, array.Length);
			this.start = 0;
			this.end = 0;
			return array;
		}

		// Token: 0x0400045A RID: 1114
		private byte[] buffer_;

		// Token: 0x0400045B RID: 1115
		private int start;

		// Token: 0x0400045C RID: 1116
		private int end;

		// Token: 0x0400045D RID: 1117
		private uint bits;

		// Token: 0x0400045E RID: 1118
		private int bitCount;
	}
}
