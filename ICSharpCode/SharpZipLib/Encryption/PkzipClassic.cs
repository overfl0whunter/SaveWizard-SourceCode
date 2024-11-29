using System;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000090 RID: 144
	public abstract class PkzipClassic : SymmetricAlgorithm
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0002DD10 File Offset: 0x0002BF10
		public static byte[] GenerateKeys(byte[] seed)
		{
			bool flag = seed == null;
			if (flag)
			{
				throw new ArgumentNullException("seed");
			}
			bool flag2 = seed.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Length is zero", "seed");
			}
			uint[] array = new uint[] { 305419896U, 591751049U, 878082192U };
			for (int i = 0; i < seed.Length; i++)
			{
				array[0] = Crc32.ComputeCrc32(array[0], seed[i]);
				array[1] = array[1] + (uint)((byte)array[0]);
				array[1] = array[1] * 134775813U + 1U;
				array[2] = Crc32.ComputeCrc32(array[2], (byte)(array[1] >> 24));
			}
			return new byte[]
			{
				(byte)(array[0] & 255U),
				(byte)((array[0] >> 8) & 255U),
				(byte)((array[0] >> 16) & 255U),
				(byte)((array[0] >> 24) & 255U),
				(byte)(array[1] & 255U),
				(byte)((array[1] >> 8) & 255U),
				(byte)((array[1] >> 16) & 255U),
				(byte)((array[1] >> 24) & 255U),
				(byte)(array[2] & 255U),
				(byte)((array[2] >> 8) & 255U),
				(byte)((array[2] >> 16) & 255U),
				(byte)((array[2] >> 24) & 255U)
			};
		}
	}
}
