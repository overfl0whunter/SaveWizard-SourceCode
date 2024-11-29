using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000021 RID: 33
	internal class SharedUtils
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000C90C File Offset: 0x0000AB0C
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			bool flag = target.Length == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				char[] array = new char[target.Length];
				int num2 = sourceTextReader.Read(array, start, count);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					for (int i = start; i < start + num2; i++)
					{
						target[i] = (byte)array[i];
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000C970 File Offset: 0x0000AB70
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000C990 File Offset: 0x0000AB90
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
