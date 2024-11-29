using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000093 RID: 147
	internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x0002DFED File Offset: 0x0002C1ED
		internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0002E0F4 File Offset: 0x0002C2F4
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i] ^ base.TransformByte();
				outputBuffer[outputOffset++] = b;
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0002E13C File Offset: 0x0002C33C
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0002E150 File Offset: 0x0002C350
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0002E164 File Offset: 0x0002C364
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0002E178 File Offset: 0x0002C378
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0002E0BF File Offset: 0x0002C2BF
		public void Dispose()
		{
			base.Reset();
		}
	}
}
