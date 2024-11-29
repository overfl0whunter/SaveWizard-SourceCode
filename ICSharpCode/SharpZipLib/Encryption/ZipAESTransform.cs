using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000096 RID: 150
	internal class ZipAESTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0002E560 File Offset: 0x0002C760
		public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
		{
			bool flag = blockSize != 16 && blockSize != 32;
			if (flag)
			{
				throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
			}
			bool flag2 = saltBytes.Length != blockSize / 2;
			if (flag2)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Invalid salt len. Must be ",
					blockSize / 2,
					" for blocksize ",
					blockSize
				}));
			}
			this._blockSize = blockSize;
			this._encryptBuffer = new byte[this._blockSize];
			this._encrPos = 16;
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, saltBytes, 1000);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Mode = CipherMode.ECB;
			this._counterNonce = new byte[this._blockSize];
			byte[] bytes = rfc2898DeriveBytes.GetBytes(this._blockSize);
			byte[] bytes2 = rfc2898DeriveBytes.GetBytes(this._blockSize);
			this._encryptor = rijndaelManaged.CreateEncryptor(bytes, bytes2);
			this._pwdVerifier = rfc2898DeriveBytes.GetBytes(2);
			this._hmacsha1 = new HMACSHA1(bytes2);
			this._writeMode = writeMode;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0002E684 File Offset: 0x0002C884
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			bool flag = !this._writeMode;
			if (flag)
			{
				this._hmacsha1.TransformBlock(inputBuffer, inputOffset, inputCount, inputBuffer, inputOffset);
			}
			for (int i = 0; i < inputCount; i++)
			{
				bool flag2 = this._encrPos == 16;
				if (flag2)
				{
					int num = 0;
					for (;;)
					{
						byte[] counterNonce = this._counterNonce;
						int num2 = num;
						byte b = counterNonce[num2] + 1;
						counterNonce[num2] = b;
						if (b != 0)
						{
							break;
						}
						num++;
					}
					this._encryptor.TransformBlock(this._counterNonce, 0, this._blockSize, this._encryptBuffer, 0);
					this._encrPos = 0;
				}
				int num3 = i + outputOffset;
				byte b2 = inputBuffer[i + inputOffset];
				byte[] encryptBuffer = this._encryptBuffer;
				int encrPos = this._encrPos;
				this._encrPos = encrPos + 1;
				outputBuffer[num3] = b2 ^ encryptBuffer[encrPos];
			}
			bool writeMode = this._writeMode;
			if (writeMode)
			{
				this._hmacsha1.TransformBlock(outputBuffer, outputOffset, inputCount, outputBuffer, outputOffset);
			}
			return inputCount;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0002E784 File Offset: 0x0002C984
		public byte[] PwdVerifier
		{
			get
			{
				return this._pwdVerifier;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002E79C File Offset: 0x0002C99C
		public byte[] GetAuthCode()
		{
			bool flag = !this._finalised;
			if (flag)
			{
				byte[] array = new byte[0];
				this._hmacsha1.TransformFinalBlock(array, 0, 0);
				this._finalised = true;
			}
			return this._hmacsha1.Hash;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0002E7E5 File Offset: 0x0002C9E5
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			throw new NotImplementedException("ZipAESTransform.TransformFinalBlock");
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		public int InputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0002E80C File Offset: 0x0002CA0C
		public int OutputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0002E824 File Offset: 0x0002CA24
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0002E838 File Offset: 0x0002CA38
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0002E84B File Offset: 0x0002CA4B
		public void Dispose()
		{
			this._encryptor.Dispose();
		}

		// Token: 0x0400048A RID: 1162
		private const int PWD_VER_LENGTH = 2;

		// Token: 0x0400048B RID: 1163
		private const int KEY_ROUNDS = 1000;

		// Token: 0x0400048C RID: 1164
		private const int ENCRYPT_BLOCK = 16;

		// Token: 0x0400048D RID: 1165
		private int _blockSize;

		// Token: 0x0400048E RID: 1166
		private ICryptoTransform _encryptor;

		// Token: 0x0400048F RID: 1167
		private readonly byte[] _counterNonce;

		// Token: 0x04000490 RID: 1168
		private byte[] _encryptBuffer;

		// Token: 0x04000491 RID: 1169
		private int _encrPos;

		// Token: 0x04000492 RID: 1170
		private byte[] _pwdVerifier;

		// Token: 0x04000493 RID: 1171
		private HMACSHA1 _hmacsha1;

		// Token: 0x04000494 RID: 1172
		private bool _finalised;

		// Token: 0x04000495 RID: 1173
		private bool _writeMode;
	}
}
