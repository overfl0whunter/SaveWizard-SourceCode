using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000046 RID: 70
	internal class ZipCipherStream : Stream
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00010757 File Offset: 0x0000E957
		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			this._cipher = cipher;
			this._s = s;
			this._mode = mode;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00010778 File Offset: 0x0000E978
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = this._mode == CryptoMode.Encrypt;
			if (flag)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = this._s.Read(array, 0, count);
			byte[] array2 = this._cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00010800 File Offset: 0x0000EA00
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this._mode == CryptoMode.Decrypt;
			if (flag)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag3 = count == 0;
			if (!flag3)
			{
				bool flag4 = offset != 0;
				byte[] array;
				if (flag4)
				{
					array = new byte[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = buffer[offset + i];
					}
				}
				else
				{
					array = buffer;
				}
				byte[] array2 = this._cipher.EncryptMessage(array, count);
				this._s.Write(array2, 0, array2.Length);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000108A0 File Offset: 0x0000EAA0
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000108BC File Offset: 0x0000EABC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000021C5 File Offset: 0x000003C5
		public override void Flush()
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040001C2 RID: 450
		private ZipCrypto _cipher;

		// Token: 0x040001C3 RID: 451
		private Stream _s;

		// Token: 0x040001C4 RID: 452
		private CryptoMode _mode;
	}
}
