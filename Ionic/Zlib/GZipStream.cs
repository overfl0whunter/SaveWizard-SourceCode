using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000013 RID: 19
	public class GZipStream : Stream
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00006FF0 File Offset: 0x000051F0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00007008 File Offset: 0x00005208
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00007034 File Offset: 0x00005234
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000704C File Offset: 0x0000524C
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				bool flag = this._FileName == null;
				if (!flag)
				{
					bool flag2 = this._FileName.IndexOf("/") != -1;
					if (flag2)
					{
						this._FileName = this._FileName.Replace("/", "\\");
					}
					bool flag3 = this._FileName.EndsWith("\\");
					if (flag3)
					{
						throw new Exception("Illegal filename");
					}
					bool flag4 = this._FileName.IndexOf("\\") != -1;
					if (flag4)
					{
						this._FileName = Path.GetFileName(this._FileName);
					}
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00007110 File Offset: 0x00005310
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007128 File Offset: 0x00005328
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007136 File Offset: 0x00005336
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007144 File Offset: 0x00005344
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007152 File Offset: 0x00005352
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00007174 File Offset: 0x00005374
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00007194 File Offset: 0x00005394
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000071C4 File Offset: 0x000053C4
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000071E4 File Offset: 0x000053E4
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000725C File Offset: 0x0000545C
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00007280 File Offset: 0x00005480
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000072A4 File Offset: 0x000054A4
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
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000731C File Offset: 0x0000551C
		public override bool CanRead
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00007354 File Offset: 0x00005554
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00007368 File Offset: 0x00005568
		public override bool CanWrite
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000073A0 File Offset: 0x000055A0
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000073D0 File Offset: 0x000055D0
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override long Position
		{
			get
			{
				bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer;
				long num;
				if (flag)
				{
					num = this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				else
				{
					bool flag2 = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag2)
					{
						num = this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007444 File Offset: 0x00005644
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int num = this._baseStream.Read(buffer, offset, count);
			bool flag = !this._firstReadDone;
			if (flag)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return num;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000074B8 File Offset: 0x000056B8
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			bool flag = this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined;
			if (flag)
			{
				bool wantCompress = this._baseStream._wantCompress;
				if (!wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007524 File Offset: 0x00005724
		private int EmitHeader()
		{
			byte[] array = ((this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment));
			byte[] array2 = ((this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName));
			int num = ((this.Comment == null) ? 0 : (array.Length + 1));
			int num2 = ((this.FileName == null) ? 0 : (array2.Length + 1));
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			bool flag = this.Comment != null;
			if (flag)
			{
				b ^= 16;
			}
			bool flag2 = this.FileName != null;
			if (flag2)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			bool flag3 = this.LastModified == null;
			if (flag3)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int num5 = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(num5), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			bool flag4 = num2 != 0;
			if (flag4)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			bool flag5 = num != 0;
			if (flag5)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000076F8 File Offset: 0x000058F8
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007744 File Offset: 0x00005944
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007790 File Offset: 0x00005990
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000077D4 File Offset: 0x000059D4
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x0400007D RID: 125
		public DateTime? LastModified;

		// Token: 0x0400007E RID: 126
		private int _headerByteCount;

		// Token: 0x0400007F RID: 127
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000080 RID: 128
		private bool _disposed;

		// Token: 0x04000081 RID: 129
		private bool _firstReadDone;

		// Token: 0x04000082 RID: 130
		private string _FileName;

		// Token: 0x04000083 RID: 131
		private string _Comment;

		// Token: 0x04000084 RID: 132
		private int _Crc32;

		// Token: 0x04000085 RID: 133
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000086 RID: 134
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
