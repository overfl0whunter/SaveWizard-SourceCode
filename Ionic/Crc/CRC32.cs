using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x0200002A RID: 42
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class CRC32
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000E768 File Offset: 0x0000C968
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000E780 File Offset: 0x0000C980
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000E79C File Offset: 0x0000C99C
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			bool flag = input == null;
			if (flag)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int num = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, num);
			bool flag2 = output != null;
			if (flag2)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, num);
				bool flag3 = output != null;
				if (flag3)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000E874 File Offset: 0x0000CA74
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000E890 File Offset: 0x0000CA90
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((W ^ (uint)B) & 255U)] ^ (W >> 8));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			bool flag = block == null;
			if (flag)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				bool flag2 = this.reverseBits;
				if (flag2)
				{
					uint num2 = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)num2];
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)num3];
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000E964 File Offset: 0x0000CB64
		public void UpdateCRC(byte b)
		{
			bool flag = this.reverseBits;
			if (flag)
			{
				uint num = (this._register >> 24) ^ (uint)b;
				this._register = (this._register << 8) ^ this.crc32Table[(int)num];
			}
			else
			{
				uint num2 = (this._register & 255U) ^ (uint)b;
				this._register = (this._register >> 8) ^ this.crc32Table[(int)num2];
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				bool flag = this.reverseBits;
				if (flag)
				{
					uint num = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((num >= 0U) ? num : (num + 256U))];
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((num2 >= 0U) ? num2 : (num2 + 256U))];
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000EA64 File Offset: 0x0000CC64
		private static uint ReverseBits(uint data)
		{
			uint num = ((data & 1431655765U) << 1) | ((data >> 1) & 1431655765U);
			num = ((num & 858993459U) << 2) | ((num >> 2) & 858993459U);
			num = ((num & 252645135U) << 4) | ((num >> 4) & 252645135U);
			return (num << 24) | ((num & 65280U) << 8) | ((num >> 8) & 65280U) | (num >> 24);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		private static byte ReverseBits(byte data)
		{
			uint num = (uint)data * 131586U;
			uint num2 = 17055760U;
			uint num3 = num & num2;
			uint num4 = (num << 2) & (num2 << 1);
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000EB10 File Offset: 0x0000CD10
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					bool flag = (num & 1U) == 1U;
					if (flag)
					{
						num = (num >> 1) ^ this.dwPolynomial;
					}
					else
					{
						num >>= 1;
					}
				}
				bool flag2 = this.reverseBits;
				if (flag2)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b > 0);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec > 0U)
			{
				bool flag = (vec & 1U) == 1U;
				if (flag)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000EC14 File Offset: 0x0000CE14
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			bool flag = length == 0;
			if (!flag)
			{
				uint num = ~this._register;
				array2[0] = this.dwPolynomial;
				uint num2 = 1U;
				for (int i = 1; i < 32; i++)
				{
					array2[i] = num2;
					num2 <<= 1;
				}
				this.gf2_matrix_square(array, array2);
				this.gf2_matrix_square(array2, array);
				uint num3 = (uint)length;
				do
				{
					this.gf2_matrix_square(array, array2);
					bool flag2 = (num3 & 1U) == 1U;
					if (flag2)
					{
						num = this.gf2_matrix_times(array, num);
					}
					num3 >>= 1;
					bool flag3 = num3 == 0U;
					if (flag3)
					{
						break;
					}
					this.gf2_matrix_square(array2, array);
					bool flag4 = (num3 & 1U) == 1U;
					if (flag4)
					{
						num = this.gf2_matrix_times(array2, num);
					}
					num3 >>= 1;
				}
				while (num3 > 0U);
				num ^= (uint)crc;
				this._register = ~num;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000ED03 File Offset: 0x0000CF03
		public CRC32()
			: this(false)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000ED0E File Offset: 0x0000CF0E
		public CRC32(bool reverseBits)
			: this(-306674912, reverseBits)
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000ED1E File Offset: 0x0000CF1E
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x0400016F RID: 367
		private uint dwPolynomial;

		// Token: 0x04000170 RID: 368
		private long _TotalBytesRead;

		// Token: 0x04000171 RID: 369
		private bool reverseBits;

		// Token: 0x04000172 RID: 370
		private uint[] crc32Table;

		// Token: 0x04000173 RID: 371
		private const int BUFFER_SIZE = 8192;

		// Token: 0x04000174 RID: 372
		private uint _register = uint.MaxValue;
	}
}
