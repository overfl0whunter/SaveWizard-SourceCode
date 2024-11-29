using System;

namespace ICSharpCode.SharpZipLib.Checksums
{
	// Token: 0x020000AA RID: 170
	public sealed class Adler32 : IChecksum
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		public long Value
		{
			get
			{
				return (long)((ulong)this.checksum);
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0002FB15 File Offset: 0x0002DD15
		public Adler32()
		{
			this.Reset();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0002FB26 File Offset: 0x0002DD26
		public void Reset()
		{
			this.checksum = 1U;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0002FB30 File Offset: 0x0002DD30
		public void Update(int value)
		{
			uint num = this.checksum & 65535U;
			uint num2 = this.checksum >> 16;
			num = (num + (uint)(value & 255)) % 65521U;
			num2 = (num + num2) % 65521U;
			this.checksum = (num2 << 16) + num;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002FB7C File Offset: 0x0002DD7C
		public void Update(byte[] buffer)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002FBAC File Offset: 0x0002DDAC
		public void Update(byte[] buffer, int offset, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "cannot be negative");
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "cannot be negative");
			}
			bool flag4 = offset >= buffer.Length;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("offset", "not a valid index into buffer");
			}
			bool flag5 = offset + count > buffer.Length;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count", "exceeds buffer size");
			}
			uint num = this.checksum & 65535U;
			uint num2 = this.checksum >> 16;
			while (count > 0)
			{
				int num3 = 3800;
				bool flag6 = num3 > count;
				if (flag6)
				{
					num3 = count;
				}
				count -= num3;
				while (--num3 >= 0)
				{
					num += (uint)(buffer[offset++] & byte.MaxValue);
					num2 += num;
				}
				num %= 65521U;
				num2 %= 65521U;
			}
			this.checksum = (num2 << 16) | num;
		}

		// Token: 0x040004B2 RID: 1202
		private const uint BASE = 65521U;

		// Token: 0x040004B3 RID: 1203
		private uint checksum;
	}
}
