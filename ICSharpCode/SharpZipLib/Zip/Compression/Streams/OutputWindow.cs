using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200008E RID: 142
	public class OutputWindow
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0002D530 File Offset: 0x0002B730
		public void Write(int value)
		{
			int num = this.windowFilled;
			this.windowFilled = num + 1;
			bool flag = num == 32768;
			if (flag)
			{
				throw new InvalidOperationException("Window full");
			}
			byte[] array = this.window;
			num = this.windowEnd;
			this.windowEnd = num + 1;
			array[num] = (byte)value;
			this.windowEnd &= 32767;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0002D594 File Offset: 0x0002B794
		private void SlowRepeat(int repStart, int length, int distance)
		{
			while (length-- > 0)
			{
				byte[] array = this.window;
				int num = this.windowEnd;
				this.windowEnd = num + 1;
				array[num] = this.window[repStart++];
				this.windowEnd &= 32767;
				repStart &= 32767;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		public void Repeat(int length, int distance)
		{
			bool flag = (this.windowFilled += length) > 32768;
			if (flag)
			{
				throw new InvalidOperationException("Window full");
			}
			int num = (this.windowEnd - distance) & 32767;
			int num2 = 32768 - length;
			bool flag2 = num <= num2 && this.windowEnd < num2;
			if (flag2)
			{
				bool flag3 = length <= distance;
				if (flag3)
				{
					Array.Copy(this.window, num, this.window, this.windowEnd, length);
					this.windowEnd += length;
				}
				else
				{
					while (length-- > 0)
					{
						byte[] array = this.window;
						int num3 = this.windowEnd;
						this.windowEnd = num3 + 1;
						array[num3] = this.window[num++];
					}
				}
			}
			else
			{
				this.SlowRepeat(num, length, distance);
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0002D6DC File Offset: 0x0002B8DC
		public int CopyStored(StreamManipulator input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
			int num = 32768 - this.windowEnd;
			bool flag = length > num;
			int num2;
			if (flag)
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, num);
				bool flag2 = num2 == num;
				if (flag2)
				{
					num2 += input.CopyBytes(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, length);
			}
			this.windowEnd = (this.windowEnd + num2) & 32767;
			this.windowFilled += num2;
			return num2;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0002D794 File Offset: 0x0002B994
		public void CopyDict(byte[] dictionary, int offset, int length)
		{
			bool flag = dictionary == null;
			if (flag)
			{
				throw new ArgumentNullException("dictionary");
			}
			bool flag2 = this.windowFilled > 0;
			if (flag2)
			{
				throw new InvalidOperationException();
			}
			bool flag3 = length > 32768;
			if (flag3)
			{
				offset += length - 32768;
				length = 32768;
			}
			Array.Copy(dictionary, offset, this.window, 0, length);
			this.windowEnd = length & 32767;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002D808 File Offset: 0x0002BA08
		public int GetFreeSpace()
		{
			return 32768 - this.windowFilled;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002D828 File Offset: 0x0002BA28
		public int GetAvailable()
		{
			return this.windowFilled;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0002D840 File Offset: 0x0002BA40
		public int CopyOutput(byte[] output, int offset, int len)
		{
			int num = this.windowEnd;
			bool flag = len > this.windowFilled;
			if (flag)
			{
				len = this.windowFilled;
			}
			else
			{
				num = (this.windowEnd - this.windowFilled + len) & 32767;
			}
			int num2 = len;
			int num3 = len - num;
			bool flag2 = num3 > 0;
			if (flag2)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				len = num;
			}
			Array.Copy(this.window, num - len, output, offset, len);
			this.windowFilled -= num2;
			bool flag3 = this.windowFilled < 0;
			if (flag3)
			{
				throw new InvalidOperationException();
			}
			return num2;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0002D8F4 File Offset: 0x0002BAF4
		public void Reset()
		{
			this.windowFilled = (this.windowEnd = 0);
		}

		// Token: 0x04000476 RID: 1142
		private const int WindowSize = 32768;

		// Token: 0x04000477 RID: 1143
		private const int WindowMask = 32767;

		// Token: 0x04000478 RID: 1144
		private byte[] window = new byte[32768];

		// Token: 0x04000479 RID: 1145
		private int windowEnd;

		// Token: 0x0400047A RID: 1146
		private int windowFilled;
	}
}
