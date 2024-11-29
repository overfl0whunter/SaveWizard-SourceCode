using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000092 RID: 146
	internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0002DFED File Offset: 0x0002C1ED
		internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0002E000 File Offset: 0x0002C200
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0002E028 File Offset: 0x0002C228
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i];
				outputBuffer[outputOffset++] = inputBuffer[i] ^ base.TransformByte();
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0002E070 File Offset: 0x0002C270
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0002E084 File Offset: 0x0002C284
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0002E098 File Offset: 0x0002C298
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0002E0BF File Offset: 0x0002C2BF
		public void Dispose()
		{
			base.Reset();
		}
	}
}
