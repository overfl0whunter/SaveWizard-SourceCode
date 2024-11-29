using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000051 RID: 81
	internal static class ZipOutput
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0001C2DC File Offset: 0x0001A4DC
		public static bool WriteCentralDirectoryStructure(Stream s, ICollection<ZipEntry> entries, uint numSegments, Zip64Option zip64, string comment, ZipContainer container)
		{
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = zipSegmentedStream != null;
			if (flag)
			{
				zipSegmentedStream.ContiguousWrite = true;
			}
			long num = 0L;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				foreach (ZipEntry zipEntry in entries)
				{
					bool includedInMostRecentSave = zipEntry.IncludedInMostRecentSave;
					if (includedInMostRecentSave)
					{
						zipEntry.WriteCentralDirectoryEntry(memoryStream);
					}
				}
				byte[] array = memoryStream.ToArray();
				s.Write(array, 0, array.Length);
				num = (long)array.Length;
			}
			CountingStream countingStream = s as CountingStream;
			long num2 = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
			long num3 = num2 - num;
			uint num4 = ((zipSegmentedStream != null) ? zipSegmentedStream.CurrentSegment : 0U);
			long num5 = num2 - num3;
			int num6 = ZipOutput.CountEntries(entries);
			bool flag2 = zip64 == Zip64Option.Always || num6 >= 65535 || num5 > (long)((ulong)(-1)) || num3 > (long)((ulong)(-1));
			bool flag3 = flag2;
			byte[] array3;
			if (flag3)
			{
				bool flag4 = zip64 == Zip64Option.Default;
				if (flag4)
				{
					StackFrame stackFrame = new StackFrame(1);
					bool flag5 = stackFrame.GetMethod().DeclaringType == typeof(ZipFile);
					if (flag5)
					{
						throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipFile.UseZip64WhenSaving property.");
					}
					throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipOutputStream.EnableZip64 property.");
				}
				else
				{
					byte[] array2 = ZipOutput.GenZip64EndOfCentralDirectory(num3, num2, num6, numSegments);
					array3 = ZipOutput.GenCentralDirectoryFooter(num3, num2, zip64, num6, comment, container);
					bool flag6 = num4 > 0U;
					if (flag6)
					{
						uint num7 = zipSegmentedStream.ComputeSegment(array2.Length + array3.Length);
						int num8 = 16;
						Array.Copy(BitConverter.GetBytes(num7), 0, array2, num8, 4);
						num8 += 4;
						Array.Copy(BitConverter.GetBytes(num7), 0, array2, num8, 4);
						num8 = 60;
						Array.Copy(BitConverter.GetBytes(num7), 0, array2, num8, 4);
						num8 += 4;
						num8 += 8;
						Array.Copy(BitConverter.GetBytes(num7), 0, array2, num8, 4);
					}
					s.Write(array2, 0, array2.Length);
				}
			}
			else
			{
				array3 = ZipOutput.GenCentralDirectoryFooter(num3, num2, zip64, num6, comment, container);
			}
			bool flag7 = num4 > 0U;
			if (flag7)
			{
				ushort num9 = (ushort)zipSegmentedStream.ComputeSegment(array3.Length);
				int num10 = 4;
				Array.Copy(BitConverter.GetBytes(num9), 0, array3, num10, 2);
				num10 += 2;
				Array.Copy(BitConverter.GetBytes(num9), 0, array3, num10, 2);
				num10 += 2;
			}
			s.Write(array3, 0, array3.Length);
			bool flag8 = zipSegmentedStream != null;
			if (flag8)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			return flag2;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001C58C File Offset: 0x0001A78C
		private static Encoding GetEncoding(ZipContainer container, string t)
		{
			ZipOption alternateEncodingUsage = container.AlternateEncodingUsage;
			Encoding encoding;
			if (alternateEncodingUsage != ZipOption.Default)
			{
				if (alternateEncodingUsage != ZipOption.Always)
				{
					Encoding defaultEncoding = container.DefaultEncoding;
					bool flag = t == null;
					if (flag)
					{
						encoding = defaultEncoding;
					}
					else
					{
						byte[] bytes = defaultEncoding.GetBytes(t);
						string @string = defaultEncoding.GetString(bytes, 0, bytes.Length);
						bool flag2 = @string.Equals(t);
						if (flag2)
						{
							encoding = defaultEncoding;
						}
						else
						{
							encoding = container.AlternateEncoding;
						}
					}
				}
				else
				{
					encoding = container.AlternateEncoding;
				}
			}
			else
			{
				encoding = container.DefaultEncoding;
			}
			return encoding;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001C608 File Offset: 0x0001A808
		private static byte[] GenCentralDirectoryFooter(long StartOfCentralDirectory, long EndOfCentralDirectory, Zip64Option zip64, int entryCount, string comment, ZipContainer container)
		{
			Encoding encoding = ZipOutput.GetEncoding(container, comment);
			int num = 22;
			byte[] array = null;
			short num2 = 0;
			bool flag = comment != null && comment.Length != 0;
			if (flag)
			{
				array = encoding.GetBytes(comment);
				num2 = (short)array.Length;
			}
			num += (int)num2;
			byte[] array2 = new byte[num];
			int num3 = 0;
			byte[] bytes = BitConverter.GetBytes(101010256U);
			Array.Copy(bytes, 0, array2, num3, 4);
			num3 += 4;
			array2[num3++] = 0;
			array2[num3++] = 0;
			array2[num3++] = 0;
			array2[num3++] = 0;
			bool flag2 = entryCount >= 65535 || zip64 == Zip64Option.Always;
			if (flag2)
			{
				for (int i = 0; i < 4; i++)
				{
					array2[num3++] = byte.MaxValue;
				}
			}
			else
			{
				array2[num3++] = (byte)(entryCount & 255);
				array2[num3++] = (byte)((entryCount & 65280) >> 8);
				array2[num3++] = (byte)(entryCount & 255);
				array2[num3++] = (byte)((entryCount & 65280) >> 8);
			}
			long num4 = EndOfCentralDirectory - StartOfCentralDirectory;
			bool flag3 = num4 >= (long)((ulong)(-1)) || StartOfCentralDirectory >= (long)((ulong)(-1));
			if (flag3)
			{
				for (int i = 0; i < 8; i++)
				{
					array2[num3++] = byte.MaxValue;
				}
			}
			else
			{
				array2[num3++] = (byte)(num4 & 255L);
				array2[num3++] = (byte)((num4 & 65280L) >> 8);
				array2[num3++] = (byte)((num4 & 16711680L) >> 16);
				array2[num3++] = (byte)((num4 & (long)((ulong)(-16777216))) >> 24);
				array2[num3++] = (byte)(StartOfCentralDirectory & 255L);
				array2[num3++] = (byte)((StartOfCentralDirectory & 65280L) >> 8);
				array2[num3++] = (byte)((StartOfCentralDirectory & 16711680L) >> 16);
				array2[num3++] = (byte)((StartOfCentralDirectory & (long)((ulong)(-16777216))) >> 24);
			}
			bool flag4 = comment == null || comment.Length == 0;
			if (flag4)
			{
				array2[num3++] = 0;
				array2[num3++] = 0;
			}
			else
			{
				bool flag5 = (int)num2 + num3 + 2 > array2.Length;
				if (flag5)
				{
					num2 = (short)(array2.Length - num3 - 2);
				}
				array2[num3++] = (byte)(num2 & 255);
				array2[num3++] = (byte)(((int)num2 & 65280) >> 8);
				bool flag6 = num2 != 0;
				if (flag6)
				{
					int i = 0;
					while (i < (int)num2 && num3 + i < array2.Length)
					{
						array2[num3 + i] = array[i];
						i++;
					}
					num3 += i;
				}
			}
			return array2;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		private static byte[] GenZip64EndOfCentralDirectory(long StartOfCentralDirectory, long EndOfCentralDirectory, int entryCount, uint numSegments)
		{
			byte[] array = new byte[76];
			int num = 0;
			byte[] array2 = BitConverter.GetBytes(101075792U);
			Array.Copy(array2, 0, array, num, 4);
			num += 4;
			long num2 = 44L;
			Array.Copy(BitConverter.GetBytes(num2), 0, array, num, 8);
			num += 8;
			array[num++] = 45;
			array[num++] = 0;
			array[num++] = 45;
			array[num++] = 0;
			for (int i = 0; i < 8; i++)
			{
				array[num++] = 0;
			}
			long num3 = (long)entryCount;
			Array.Copy(BitConverter.GetBytes(num3), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(num3), 0, array, num, 8);
			num += 8;
			long num4 = EndOfCentralDirectory - StartOfCentralDirectory;
			Array.Copy(BitConverter.GetBytes(num4), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(StartOfCentralDirectory), 0, array, num, 8);
			num += 8;
			array2 = BitConverter.GetBytes(117853008U);
			Array.Copy(array2, 0, array, num, 4);
			num += 4;
			uint num5 = ((numSegments == 0U) ? 0U : (numSegments - 1U));
			Array.Copy(BitConverter.GetBytes(num5), 0, array, num, 4);
			num += 4;
			Array.Copy(BitConverter.GetBytes(EndOfCentralDirectory), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(numSegments), 0, array, num, 4);
			num += 4;
			return array;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		private static int CountEntries(ICollection<ZipEntry> _entries)
		{
			int num = 0;
			foreach (ZipEntry zipEntry in _entries)
			{
				bool includedInMostRecentSave = zipEntry.IncludedInMostRecentSave;
				if (includedInMostRecentSave)
				{
					num++;
				}
			}
			return num;
		}
	}
}
