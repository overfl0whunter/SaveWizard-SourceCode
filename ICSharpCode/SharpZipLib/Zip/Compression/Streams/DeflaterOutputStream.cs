using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Encryption;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200008B RID: 139
	public class DeflaterOutputStream : Stream
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0002C6AB File Offset: 0x0002A8AB
		public DeflaterOutputStream(Stream baseOutputStream)
			: this(baseOutputStream, new Deflater(), 512)
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002C6C0 File Offset: 0x0002A8C0
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater)
			: this(baseOutputStream, deflater, 512)
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002C6D4 File Offset: 0x0002A8D4
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
		{
			bool flag = baseOutputStream == null;
			if (flag)
			{
				throw new ArgumentNullException("baseOutputStream");
			}
			bool flag2 = !baseOutputStream.CanWrite;
			if (flag2)
			{
				throw new ArgumentException("Must support writing", "baseOutputStream");
			}
			bool flag3 = deflater == null;
			if (flag3)
			{
				throw new ArgumentNullException("deflater");
			}
			bool flag4 = bufferSize < 512;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseOutputStream_ = baseOutputStream;
			this.buffer_ = new byte[bufferSize];
			this.deflater_ = deflater;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002C768 File Offset: 0x0002A968
		public virtual void Finish()
		{
			this.deflater_.Finish();
			while (!this.deflater_.IsFinished)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				bool flag = num <= 0;
				if (flag)
				{
					break;
				}
				bool flag2 = this.cryptoTransform_ != null;
				if (flag2)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			bool flag3 = !this.deflater_.IsFinished;
			if (flag3)
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			this.baseOutputStream_.Flush();
			bool flag4 = this.cryptoTransform_ != null;
			if (flag4)
			{
				bool flag5 = this.cryptoTransform_ is ZipAESTransform;
				if (flag5)
				{
					this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
				}
				this.cryptoTransform_.Dispose();
				this.cryptoTransform_ = null;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0002C870 File Offset: 0x0002AA70
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0002C888 File Offset: 0x0002AA88
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner_;
			}
			set
			{
				this.isStreamOwner_ = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0002C894 File Offset: 0x0002AA94
		public bool CanPatchEntries
		{
			get
			{
				return this.baseOutputStream_.CanSeek;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0002C8CC File Offset: 0x0002AACC
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				bool flag = value != null && value.Length == 0;
				if (flag)
				{
					this.password = null;
				}
				else
				{
					this.password = value;
				}
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0002C901 File Offset: 0x0002AB01
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002C918 File Offset: 0x0002AB18
		protected void InitializePassword(string password)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			byte[] array = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(password));
			this.cryptoTransform_ = pkzipClassicManaged.CreateEncryptor(array, null);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0002C948 File Offset: 0x0002AB48
		protected void InitializeAESPassword(ZipEntry entry, string rawPassword, out byte[] salt, out byte[] pwdVerifier)
		{
			salt = new byte[entry.AESSaltLen];
			bool flag = DeflaterOutputStream._aesRnd == null;
			if (flag)
			{
				DeflaterOutputStream._aesRnd = new RNGCryptoServiceProvider();
			}
			DeflaterOutputStream._aesRnd.GetBytes(salt);
			int num = entry.AESKeySize / 8;
			this.cryptoTransform_ = new ZipAESTransform(rawPassword, salt, num, true);
			pwdVerifier = ((ZipAESTransform)this.cryptoTransform_).PwdVerifier;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		protected void Deflate()
		{
			while (!this.deflater_.IsNeedingInput)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				bool flag = num <= 0;
				if (flag)
				{
					break;
				}
				bool flag2 = this.cryptoTransform_ != null;
				if (flag2)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			bool flag3 = !this.deflater_.IsNeedingInput;
			if (flag3)
			{
				throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0002CA54 File Offset: 0x0002AC54
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0002CA68 File Offset: 0x0002AC68
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0002CA7C File Offset: 0x0002AC7C
		public override bool CanWrite
		{
			get
			{
				return this.baseOutputStream_.CanWrite;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		public override long Length
		{
			get
			{
				return this.baseOutputStream_.Length;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0002CABC File Offset: 0x0002ACBC
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0002CAD9 File Offset: 0x0002ACD9
		public override long Position
		{
			get
			{
				return this.baseOutputStream_.Position;
			}
			set
			{
				throw new NotSupportedException("Position property not supported");
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0002CAE6 File Offset: 0x0002ACE6
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0002CAF3 File Offset: 0x0002ACF3
		public override void SetLength(long value)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002CB00 File Offset: 0x0002AD00
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002CB0D File Offset: 0x0002AD0D
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002CB1A File Offset: 0x0002AD1A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002CB27 File Offset: 0x0002AD27
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("BeginWrite is not supported");
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002CB34 File Offset: 0x0002AD34
		public override void Flush()
		{
			this.deflater_.Flush();
			this.Deflate();
			this.baseOutputStream_.Flush();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002CB58 File Offset: 0x0002AD58
		public override void Close()
		{
			bool flag = !this.isClosed_;
			if (flag)
			{
				this.isClosed_ = true;
				try
				{
					this.Finish();
					bool flag2 = this.cryptoTransform_ != null;
					if (flag2)
					{
						this.GetAuthCodeIfAES();
						this.cryptoTransform_.Dispose();
						this.cryptoTransform_ = null;
					}
				}
				finally
				{
					bool flag3 = this.isStreamOwner_;
					if (flag3)
					{
						this.baseOutputStream_.Close();
					}
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002CBDC File Offset: 0x0002ADDC
		private void GetAuthCodeIfAES()
		{
			bool flag = this.cryptoTransform_ is ZipAESTransform;
			if (flag)
			{
				this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0002CC14 File Offset: 0x0002AE14
		public override void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002CC37 File Offset: 0x0002AE37
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.deflater_.SetInput(buffer, offset, count);
			this.Deflate();
		}

		// Token: 0x0400045F RID: 1119
		private string password;

		// Token: 0x04000460 RID: 1120
		private ICryptoTransform cryptoTransform_;

		// Token: 0x04000461 RID: 1121
		protected byte[] AESAuthCode;

		// Token: 0x04000462 RID: 1122
		private byte[] buffer_;

		// Token: 0x04000463 RID: 1123
		protected Deflater deflater_;

		// Token: 0x04000464 RID: 1124
		protected Stream baseOutputStream_;

		// Token: 0x04000465 RID: 1125
		private bool isClosed_;

		// Token: 0x04000466 RID: 1126
		private bool isStreamOwner_ = true;

		// Token: 0x04000467 RID: 1127
		private static RNGCryptoServiceProvider _aesRnd;
	}
}
