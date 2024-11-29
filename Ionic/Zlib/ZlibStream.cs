using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000029 RID: 41
	public class ZlibStream : Stream
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000E2E9 File Offset: 0x0000C4E9
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000E2F7 File Offset: 0x0000C4F7
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000E305 File Offset: 0x0000C505
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000E313 File Offset: 0x0000C513
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000E334 File Offset: 0x0000C534
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000E354 File Offset: 0x0000C554
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000E384 File Offset: 0x0000C584
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				bool flag = this._baseStream._workingBuffer != null;
				if (flag)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				bool flag2 = value < 1024;
				if (flag2)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000E41C File Offset: 0x0000C61C
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000E440 File Offset: 0x0000C640
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000E464 File Offset: 0x0000C664
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = !this._disposed;
				if (flag)
				{
					bool flag2 = disposing && this._baseStream != null;
					if (flag2)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public override bool CanRead
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000E500 File Offset: 0x0000C700
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000E514 File Offset: 0x0000C714
		public override bool CanWrite
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000E54C File Offset: 0x0000C74C
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000E57C File Offset: 0x0000C77C
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Position
		{
			get
			{
				bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer;
				long num;
				if (flag)
				{
					num = this._baseStream._z.TotalBytesOut;
				}
				else
				{
					bool flag2 = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag2)
					{
						num = this._baseStream._z.TotalBytesIn;
					}
					else
					{
						num = 0L;
					}
				}
				return num;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000E614 File Offset: 0x0000C814
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000E648 File Offset: 0x0000C848
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000E694 File Offset: 0x0000C894
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000E724 File Offset: 0x0000C924
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x0400016D RID: 365
		internal ZlibBaseStream _baseStream;

		// Token: 0x0400016E RID: 366
		private bool _disposed;
	}
}
