using System;
using System.IO;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x02000044 RID: 68
	internal class ZipCrypto
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00010443 File Offset: 0x0000E643
		private ZipCrypto()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00010470 File Offset: 0x0000E670
		public static ZipCrypto ForWrite(string password)
		{
			ZipCrypto zipCrypto = new ZipCrypto();
			bool flag = password == null;
			if (flag)
			{
				throw new BadPasswordException("This entry requires a password.");
			}
			zipCrypto.InitCipher(password);
			return zipCrypto;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000104A4 File Offset: 0x0000E6A4
		public static ZipCrypto ForRead(string password, ZipEntry e)
		{
			Stream archiveStream = e._archiveStream;
			e._WeakEncryptionHeader = new byte[12];
			byte[] weakEncryptionHeader = e._WeakEncryptionHeader;
			ZipCrypto zipCrypto = new ZipCrypto();
			bool flag = password == null;
			if (flag)
			{
				throw new BadPasswordException("This entry requires a password.");
			}
			zipCrypto.InitCipher(password);
			ZipEntry.ReadWeakEncryptionHeader(archiveStream, weakEncryptionHeader);
			byte[] array = zipCrypto.DecryptMessage(weakEncryptionHeader, weakEncryptionHeader.Length);
			bool flag2 = array[11] != (byte)((e._Crc32 >> 24) & 255);
			if (flag2)
			{
				bool flag3 = (e._BitField & 8) != 8;
				if (flag3)
				{
					throw new BadPasswordException("The password did not match.");
				}
				bool flag4 = array[11] != (byte)((e._TimeBlob >> 8) & 255);
				if (flag4)
				{
					throw new BadPasswordException("The password did not match.");
				}
			}
			return zipCrypto;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0001057C File Offset: 0x0000E77C
		private byte MagicByte
		{
			get
			{
				ushort num = (ushort)(this._Keys[2] & 65535U) | 2;
				return (byte)(num * (num ^ 1) >> 8);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000105AC File Offset: 0x0000E7AC
		public byte[] DecryptMessage(byte[] cipherText, int length)
		{
			bool flag = cipherText == null;
			if (flag)
			{
				throw new ArgumentNullException("cipherText");
			}
			bool flag2 = length > cipherText.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", "Bad length during Decryption: the length parameter must be smaller than or equal to the size of the destination array.");
			}
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				byte b = cipherText[i] ^ this.MagicByte;
				this.UpdateKeys(b);
				array[i] = b;
			}
			return array;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00010624 File Offset: 0x0000E824
		public byte[] EncryptMessage(byte[] plainText, int length)
		{
			bool flag = plainText == null;
			if (flag)
			{
				throw new ArgumentNullException("plaintext");
			}
			bool flag2 = length > plainText.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", "Bad length during Encryption: The length parameter must be smaller than or equal to the size of the destination array.");
			}
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				byte b = plainText[i];
				array[i] = plainText[i] ^ this.MagicByte;
				this.UpdateKeys(b);
			}
			return array;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000106A0 File Offset: 0x0000E8A0
		public void InitCipher(string passphrase)
		{
			byte[] array = SharedUtilities.StringToByteArray(passphrase);
			for (int i = 0; i < passphrase.Length; i++)
			{
				this.UpdateKeys(array[i]);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000106D4 File Offset: 0x0000E8D4
		private void UpdateKeys(byte byteValue)
		{
			this._Keys[0] = (uint)this.crc32.ComputeCrc32((int)this._Keys[0], byteValue);
			this._Keys[1] = this._Keys[1] + (uint)((byte)this._Keys[0]);
			this._Keys[1] = this._Keys[1] * 134775813U + 1U;
			this._Keys[2] = (uint)this.crc32.ComputeCrc32((int)this._Keys[2], (byte)(this._Keys[1] >> 24));
		}

		// Token: 0x040001BD RID: 445
		private uint[] _Keys = new uint[] { 305419896U, 591751049U, 878082192U };

		// Token: 0x040001BE RID: 446
		private CRC32 crc32 = new CRC32();
	}
}
