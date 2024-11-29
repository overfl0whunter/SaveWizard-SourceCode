using System;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000091 RID: 145
	internal class PkzipClassicCryptoBase
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x0002DE84 File Offset: 0x0002C084
		protected byte TransformByte()
		{
			uint num = (this.keys[2] & 65535U) | 2U;
			return (byte)(num * (num ^ 1U) >> 8);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		protected void SetKeys(byte[] keyData)
		{
			bool flag = keyData == null;
			if (flag)
			{
				throw new ArgumentNullException("keyData");
			}
			bool flag2 = keyData.Length != 12;
			if (flag2)
			{
				throw new InvalidOperationException("Key length is not valid");
			}
			this.keys = new uint[3];
			this.keys[0] = (uint)(((int)keyData[3] << 24) | ((int)keyData[2] << 16) | ((int)keyData[1] << 8) | (int)keyData[0]);
			this.keys[1] = (uint)(((int)keyData[7] << 24) | ((int)keyData[6] << 16) | ((int)keyData[5] << 8) | (int)keyData[4]);
			this.keys[2] = (uint)(((int)keyData[11] << 24) | ((int)keyData[10] << 16) | ((int)keyData[9] << 8) | (int)keyData[8]);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0002DF58 File Offset: 0x0002C158
		protected void UpdateKeys(byte ch)
		{
			this.keys[0] = Crc32.ComputeCrc32(this.keys[0], ch);
			this.keys[1] = this.keys[1] + (uint)((byte)this.keys[0]);
			this.keys[1] = this.keys[1] * 134775813U + 1U;
			this.keys[2] = Crc32.ComputeCrc32(this.keys[2], (byte)(this.keys[1] >> 24));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0002DFCF File Offset: 0x0002C1CF
		protected void Reset()
		{
			this.keys[0] = 0U;
			this.keys[1] = 0U;
			this.keys[2] = 0U;
		}

		// Token: 0x04000480 RID: 1152
		private uint[] keys;
	}
}
