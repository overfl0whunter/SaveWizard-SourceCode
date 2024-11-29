using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A8 RID: 168
	public sealed class StreamUtils
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0002F69E File Offset: 0x0002D89E
		public static void ReadFully(Stream stream, byte[] buffer)
		{
			StreamUtils.ReadFully(stream, buffer, 0, buffer.Length);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag3 = offset < 0 || offset > buffer.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			bool flag4 = count < 0 || offset + count > buffer.Length;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				int num = stream.Read(buffer, offset, count);
				bool flag5 = num <= 0;
				if (flag5)
				{
					throw new EndOfStreamException();
				}
				offset += num;
				count -= num;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002F75C File Offset: 0x0002D95C
		public static void Copy(Stream source, Stream destination, byte[] buffer)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = destination == null;
			if (flag2)
			{
				throw new ArgumentNullException("destination");
			}
			bool flag3 = buffer == null;
			if (flag3)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag4 = buffer.Length < 128;
			if (flag4)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			bool flag5 = true;
			while (flag5)
			{
				int num = source.Read(buffer, 0, buffer.Length);
				bool flag6 = num > 0;
				if (flag6)
				{
					destination.Write(buffer, 0, num);
				}
				else
				{
					destination.Flush();
					flag5 = false;
				}
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002F806 File Offset: 0x0002DA06
		public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name)
		{
			StreamUtils.Copy(source, destination, buffer, progressHandler, updateInterval, sender, name, -1L);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002F81C File Offset: 0x0002DA1C
		public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name, long fixedTarget)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = destination == null;
			if (flag2)
			{
				throw new ArgumentNullException("destination");
			}
			bool flag3 = buffer == null;
			if (flag3)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag4 = buffer.Length < 128;
			if (flag4)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			bool flag5 = progressHandler == null;
			if (flag5)
			{
				throw new ArgumentNullException("progressHandler");
			}
			bool flag6 = true;
			DateTime dateTime = DateTime.Now;
			long num = 0L;
			long num2 = 0L;
			bool flag7 = fixedTarget >= 0L;
			if (flag7)
			{
				num2 = fixedTarget;
			}
			else
			{
				bool canSeek = source.CanSeek;
				if (canSeek)
				{
					num2 = source.Length - source.Position;
				}
			}
			ProgressEventArgs progressEventArgs = new ProgressEventArgs(name, num, num2);
			progressHandler(sender, progressEventArgs);
			bool flag8 = true;
			while (flag6)
			{
				int num3 = source.Read(buffer, 0, buffer.Length);
				bool flag9 = num3 > 0;
				if (flag9)
				{
					num += (long)num3;
					flag8 = false;
					destination.Write(buffer, 0, num3);
				}
				else
				{
					destination.Flush();
					flag6 = false;
				}
				bool flag10 = DateTime.Now - dateTime > updateInterval;
				if (flag10)
				{
					flag8 = true;
					dateTime = DateTime.Now;
					progressEventArgs = new ProgressEventArgs(name, num, num2);
					progressHandler(sender, progressEventArgs);
					flag6 = progressEventArgs.ContinueRunning;
				}
			}
			bool flag11 = !flag8;
			if (flag11)
			{
				progressEventArgs = new ProgressEventArgs(name, num, num2);
				progressHandler(sender, progressEventArgs);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00003ED8 File Offset: 0x000020D8
		private StreamUtils()
		{
		}
	}
}
