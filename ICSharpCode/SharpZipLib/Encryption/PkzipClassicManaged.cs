using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000094 RID: 148
	public sealed class PkzipClassicManaged : PkzipClassic
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0002E18C File Offset: 0x0002C38C
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		public override int BlockSize
		{
			get
			{
				return 8;
			}
			set
			{
				bool flag = value != 8;
				if (flag)
				{
					throw new CryptographicException("Block size is invalid");
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
		public override KeySizes[] LegalKeySizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(96, 96, 0)
				};
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000021C5 File Offset: 0x000003C5
		public override void GenerateIV()
		{
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		public override KeySizes[] LegalBlockSizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(8, 8, 0)
				};
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002E218 File Offset: 0x0002C418
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0002E250 File Offset: 0x0002C450
		public override byte[] Key
		{
			get
			{
				bool flag = this.key_ == null;
				if (flag)
				{
					this.GenerateKey();
				}
				return (byte[])this.key_.Clone();
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				bool flag2 = value.Length != 12;
				if (flag2)
				{
					throw new CryptographicException("Key size is illegal");
				}
				this.key_ = (byte[])value.Clone();
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public override void GenerateKey()
		{
			this.key_ = new byte[12];
			Random random = new Random();
			random.NextBytes(this.key_);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicEncryptCryptoTransform(this.Key);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicDecryptCryptoTransform(this.Key);
		}

		// Token: 0x04000481 RID: 1153
		private byte[] key_;
	}
}
