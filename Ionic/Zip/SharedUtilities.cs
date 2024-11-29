using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Ionic.Zip
{
	// Token: 0x02000041 RID: 65
	internal static class SharedUtilities
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000F790 File Offset: 0x0000D990
		public static long GetFileLength(string fileName)
		{
			bool flag = !File.Exists(fileName);
			if (flag)
			{
				throw new FileNotFoundException(fileName);
			}
			long num = 0L;
			FileShare fileShare = FileShare.ReadWrite;
			fileShare |= FileShare.Delete;
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, fileShare))
			{
				num = fileStream.Length;
			}
			return num;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		[Conditional("NETCF")]
		public static void Workaround_Ladybug318918(Stream s)
		{
			s.Flush();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F800 File Offset: 0x0000DA00
		private static string SimplifyFwdSlashPath(string path)
		{
			bool flag = path.StartsWith("./");
			if (flag)
			{
				path = path.Substring(2);
			}
			path = path.Replace("/./", "/");
			path = SharedUtilities.doubleDotRegex1.Replace(path, "$1$3");
			return path;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000F850 File Offset: 0x0000DA50
		public static string NormalizePathForUseInZipFile(string pathName)
		{
			bool flag = string.IsNullOrEmpty(pathName);
			string text;
			if (flag)
			{
				text = pathName;
			}
			else
			{
				bool flag2 = pathName.Length >= 2 && pathName[1] == ':' && pathName[2] == '\\';
				if (flag2)
				{
					pathName = pathName.Substring(3);
				}
				pathName = pathName.Replace('\\', '/');
				while (pathName.StartsWith("/"))
				{
					pathName = pathName.Substring(1);
				}
				text = SharedUtilities.SimplifyFwdSlashPath(pathName);
			}
			return text;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		internal static byte[] StringToByteArray(string value, Encoding encoding)
		{
			return encoding.GetBytes(value);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		internal static byte[] StringToByteArray(string value)
		{
			return SharedUtilities.StringToByteArray(value, SharedUtilities.ibm437);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000F90C File Offset: 0x0000DB0C
		internal static string Utf8StringFromBuffer(byte[] buf)
		{
			return SharedUtilities.StringFromBuffer(buf, SharedUtilities.utf8);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000F92C File Offset: 0x0000DB2C
		internal static string StringFromBuffer(byte[] buf, Encoding encoding)
		{
			return encoding.GetString(buf, 0, buf.Length);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000F94C File Offset: 0x0000DB4C
		internal static int ReadSignature(Stream s)
		{
			int num = 0;
			try
			{
				num = SharedUtilities._ReadFourBytes(s, "n/a");
			}
			catch (BadReadException)
			{
			}
			return num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000F988 File Offset: 0x0000DB88
		internal static int ReadEntrySignature(Stream s)
		{
			int num = 0;
			try
			{
				num = SharedUtilities._ReadFourBytes(s, "n/a");
				bool flag = num == 134695760;
				if (flag)
				{
					s.Seek(12L, SeekOrigin.Current);
					num = SharedUtilities._ReadFourBytes(s, "n/a");
					bool flag2 = num != 67324752;
					if (flag2)
					{
						s.Seek(8L, SeekOrigin.Current);
						num = SharedUtilities._ReadFourBytes(s, "n/a");
						bool flag3 = num != 67324752;
						if (flag3)
						{
							s.Seek(-24L, SeekOrigin.Current);
							num = SharedUtilities._ReadFourBytes(s, "n/a");
						}
					}
				}
			}
			catch (BadReadException)
			{
			}
			return num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000FA38 File Offset: 0x0000DC38
		internal static int ReadInt(Stream s)
		{
			return SharedUtilities._ReadFourBytes(s, "Could not read block - no data!  (position 0x{0:X8})");
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000FA58 File Offset: 0x0000DC58
		private static int _ReadFourBytes(Stream s, string message)
		{
			byte[] array = new byte[4];
			int num = s.Read(array, 0, array.Length);
			bool flag = num != array.Length;
			if (flag)
			{
				throw new BadReadException(string.Format(message, s.Position));
			}
			return (((int)array[3] * 256 + (int)array[2]) * 256 + (int)array[1]) * 256 + (int)array[0];
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		internal static long FindSignature(Stream stream, int SignatureToFind)
		{
			long position = stream.Position;
			int num = 65536;
			byte[] array = new byte[]
			{
				(byte)(SignatureToFind >> 24),
				(byte)((SignatureToFind & 16711680) >> 16),
				(byte)((SignatureToFind & 65280) >> 8),
				(byte)(SignatureToFind & 255)
			};
			byte[] array2 = new byte[num];
			bool flag = false;
			bool flag5;
			do
			{
				int num2 = stream.Read(array2, 0, array2.Length);
				bool flag2 = num2 != 0;
				if (!flag2)
				{
					break;
				}
				for (int i = 0; i < num2; i++)
				{
					bool flag3 = array2[i] == array[3];
					if (flag3)
					{
						long position2 = stream.Position;
						stream.Seek((long)(i - num2), SeekOrigin.Current);
						int num3 = SharedUtilities.ReadSignature(stream);
						flag = num3 == SignatureToFind;
						bool flag4 = !flag;
						if (!flag4)
						{
							break;
						}
						stream.Seek(position2, SeekOrigin.Begin);
					}
				}
				flag5 = flag;
			}
			while (!flag5);
			bool flag6 = !flag;
			long num4;
			if (flag6)
			{
				stream.Seek(position, SeekOrigin.Begin);
				num4 = -1L;
			}
			else
			{
				long num5 = stream.Position - position - 4L;
				num4 = num5;
			}
			return num4;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		internal static DateTime AdjustTime_Reverse(DateTime time)
		{
			bool flag = time.Kind == DateTimeKind.Utc;
			DateTime dateTime;
			if (flag)
			{
				dateTime = time;
			}
			else
			{
				DateTime dateTime2 = time;
				bool flag2 = DateTime.Now.IsDaylightSavingTime() && !time.IsDaylightSavingTime();
				if (flag2)
				{
					dateTime2 = time - new TimeSpan(1, 0, 0);
				}
				else
				{
					bool flag3 = !DateTime.Now.IsDaylightSavingTime() && time.IsDaylightSavingTime();
					if (flag3)
					{
						dateTime2 = time + new TimeSpan(1, 0, 0);
					}
				}
				dateTime = dateTime2;
			}
			return dateTime;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		internal static DateTime PackedToDateTime(int packedDateTime)
		{
			bool flag = packedDateTime == 65535 || packedDateTime == 0;
			DateTime dateTime;
			if (flag)
			{
				dateTime = new DateTime(1995, 1, 1, 0, 0, 0, 0);
			}
			else
			{
				short num = (short)(packedDateTime & 65535);
				short num2 = (short)(((long)packedDateTime & (long)((ulong)(-65536))) >> 16);
				int i = 1980 + (((int)num2 & 65024) >> 9);
				int j = (num2 & 480) >> 5;
				int k = (int)(num2 & 31);
				int num3 = ((int)num & 63488) >> 11;
				int l = (num & 2016) >> 5;
				int m = (int)((num & 31) * 2);
				bool flag2 = m >= 60;
				if (flag2)
				{
					l++;
					m = 0;
				}
				bool flag3 = l >= 60;
				if (flag3)
				{
					num3++;
					l = 0;
				}
				bool flag4 = num3 >= 24;
				if (flag4)
				{
					k++;
					num3 = 0;
				}
				DateTime dateTime2 = DateTime.Now;
				bool flag5 = false;
				try
				{
					dateTime2 = new DateTime(i, j, k, num3, l, m, 0);
					flag5 = true;
				}
				catch (ArgumentOutOfRangeException)
				{
					bool flag6 = i == 1980 && (j == 0 || k == 0);
					if (flag6)
					{
						try
						{
							dateTime2 = new DateTime(1980, 1, 1, num3, l, m, 0);
							flag5 = true;
						}
						catch (ArgumentOutOfRangeException)
						{
							try
							{
								dateTime2 = new DateTime(1980, 1, 1, 0, 0, 0, 0);
								flag5 = true;
							}
							catch (ArgumentOutOfRangeException)
							{
							}
						}
					}
					else
					{
						try
						{
							while (i < 1980)
							{
								i++;
							}
							while (i > 2030)
							{
								i--;
							}
							while (j < 1)
							{
								j++;
							}
							while (j > 12)
							{
								j--;
							}
							while (k < 1)
							{
								k++;
							}
							while (k > 28)
							{
								k--;
							}
							while (l < 0)
							{
								l++;
							}
							while (l > 59)
							{
								l--;
							}
							while (m < 0)
							{
								m++;
							}
							while (m > 59)
							{
								m--;
							}
							dateTime2 = new DateTime(i, j, k, num3, l, m, 0);
							flag5 = true;
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
				bool flag7 = !flag5;
				if (flag7)
				{
					string text = string.Format("y({0}) m({1}) d({2}) h({3}) m({4}) s({5})", new object[] { i, j, k, num3, l, m });
					throw new ZipException(string.Format("Bad date/time format in the zip file. ({0})", text));
				}
				dateTime2 = DateTime.SpecifyKind(dateTime2, DateTimeKind.Local);
				dateTime = dateTime2;
			}
			return dateTime;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		internal static int DateTimeToPacked(DateTime time)
		{
			time = time.ToLocalTime();
			ushort num = (ushort)((time.Day & 31) | ((time.Month << 5) & 480) | ((time.Year - 1980 << 9) & 65024));
			ushort num2 = (ushort)(((time.Second / 2) & 31) | ((time.Minute << 5) & 2016) | ((time.Hour << 11) & 63488));
			return ((int)num << 16) | (int)num2;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00010030 File Offset: 0x0000E230
		public static void CreateAndOpenUniqueTempFile(string dir, out Stream fs, out string filename)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					filename = Path.Combine(dir, SharedUtilities.InternalGetTempFileName());
					fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
					return;
				}
				catch (IOException)
				{
					bool flag = i == 2;
					if (flag)
					{
						throw;
					}
				}
			}
			throw new IOException();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010090 File Offset: 0x0000E290
		public static string InternalGetTempFileName()
		{
			return "DotNetZip-" + Path.GetRandomFileName().Substring(0, 8) + ".tmp";
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000100C0 File Offset: 0x0000E2C0
		internal static int ReadWithRetry(Stream s, byte[] buffer, int offset, int count, string FileName)
		{
			int num = 0;
			bool flag = false;
			int num2 = 0;
			do
			{
				try
				{
					num = s.Read(buffer, offset, count);
					flag = true;
				}
				catch (IOException ex)
				{
					SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
					bool flag2 = securityPermission.IsUnrestricted();
					if (!flag2)
					{
						throw;
					}
					uint num3 = SharedUtilities._HRForException(ex);
					bool flag3 = num3 != 2147942433U;
					if (flag3)
					{
						throw new IOException(string.Format("Cannot read file {0}", FileName), ex);
					}
					num2++;
					bool flag4 = num2 > 10;
					if (flag4)
					{
						throw new IOException(string.Format("Cannot read file {0}, at offset 0x{1:X8} after 10 retries", FileName, offset), ex);
					}
					Thread.Sleep(250 + num2 * 550);
				}
			}
			while (!flag);
			return num;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00010198 File Offset: 0x0000E398
		private static uint _HRForException(Exception ex1)
		{
			return (uint)Marshal.GetHRForException(ex1);
		}

		// Token: 0x040001A9 RID: 425
		private static Regex doubleDotRegex1 = new Regex("^(.*/)?([^/\\\\.]+/\\\\.\\\\./)(.+)$");

		// Token: 0x040001AA RID: 426
		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		// Token: 0x040001AB RID: 427
		private static Encoding utf8 = Encoding.GetEncoding("UTF-8");
	}
}
