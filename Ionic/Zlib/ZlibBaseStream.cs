using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x02000026 RID: 38
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		internal int Crc32
		{
			get
			{
				bool flag = this.crc == null;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					num = this.crc.Crc32Result;
				}
				return num;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			bool flag = flavor == ZlibStreamFlavor.GZIP;
			if (flag)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000CD88 File Offset: 0x0000AF88
		private ZlibCodec z
		{
			get
			{
				bool flag = this._z == null;
				if (flag)
				{
					bool flag2 = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					bool flag3 = this._compressionMode == CompressionMode.Decompress;
					if (flag3)
					{
						this._z.InitializeInflate(flag2);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, flag2);
					}
				}
				return this._z;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000CE0C File Offset: 0x0000B00C
		private byte[] workingBuffer
		{
			get
			{
				bool flag = this._workingBuffer == null;
				if (flag)
				{
					this._workingBuffer = new byte[this._bufferSize];
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000CE44 File Offset: 0x0000B044
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this.crc != null;
			if (flag)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			bool flag2 = this._streamMode == ZlibBaseStream.StreamMode.Undefined;
			if (flag2)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else
			{
				bool flag3 = this._streamMode > ZlibBaseStream.StreamMode.Writer;
				if (flag3)
				{
					throw new ZlibException("Cannot Write after Reading.");
				}
			}
			bool flag4 = count == 0;
			if (!flag4)
			{
				this.z.InputBuffer = buffer;
				this._z.NextIn = offset;
				this._z.AvailableBytesIn = count;
				for (;;)
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					int num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
					bool flag5 = num != 0 && num != 1;
					if (flag5)
					{
						break;
					}
					this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					bool flag6 = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					bool flag7 = this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress;
					if (flag7)
					{
						flag6 = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
					}
					if (flag6)
					{
						return;
					}
				}
				throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000D008 File Offset: 0x0000B208
		private void finish()
		{
			bool flag = this._z == null;
			if (!flag)
			{
				bool flag2 = this._streamMode == ZlibBaseStream.StreamMode.Writer;
				if (flag2)
				{
					int num;
					for (;;)
					{
						this._z.OutputBuffer = this.workingBuffer;
						this._z.NextOut = 0;
						this._z.AvailableBytesOut = this._workingBuffer.Length;
						num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
						bool flag3 = num != 1 && num != 0;
						if (flag3)
						{
							break;
						}
						bool flag4 = this._workingBuffer.Length - this._z.AvailableBytesOut > 0;
						if (flag4)
						{
							this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
						}
						bool flag5 = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
						bool flag6 = this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress;
						if (flag6)
						{
							flag5 = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
						}
						if (flag5)
						{
							goto Block_12;
						}
					}
					string text = (this._wantCompress ? "de" : "in") + "flating";
					bool flag7 = this._z.Message == null;
					if (flag7)
					{
						throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
					}
					throw new ZlibException(text + ": " + this._z.Message);
					Block_12:
					this.Flush();
					bool flag8 = this._flavor == ZlibStreamFlavor.GZIP;
					if (flag8)
					{
						bool wantCompress = this._wantCompress;
						if (!wantCompress)
						{
							throw new ZlibException("Writing with decompression is not supported.");
						}
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int num2 = (int)(this.crc.TotalBytesRead & (long)((ulong)(-1)));
						this._stream.Write(BitConverter.GetBytes(num2), 0, 4);
					}
				}
				else
				{
					bool flag9 = this._streamMode == ZlibBaseStream.StreamMode.Reader;
					if (flag9)
					{
						bool flag10 = this._flavor == ZlibStreamFlavor.GZIP;
						if (flag10)
						{
							bool flag11 = !this._wantCompress;
							if (!flag11)
							{
								throw new ZlibException("Reading with compression is not supported.");
							}
							bool flag12 = this._z.TotalBytesOut == 0L;
							if (!flag12)
							{
								byte[] array = new byte[8];
								bool flag13 = this._z.AvailableBytesIn < 8;
								if (flag13)
								{
									Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, this._z.AvailableBytesIn);
									int num3 = 8 - this._z.AvailableBytesIn;
									int num4 = this._stream.Read(array, this._z.AvailableBytesIn, num3);
									bool flag14 = num3 != num4;
									if (flag14)
									{
										throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", this._z.AvailableBytesIn + num4));
									}
								}
								else
								{
									Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, array.Length);
								}
								int num5 = BitConverter.ToInt32(array, 0);
								int crc32Result2 = this.crc.Crc32Result;
								int num6 = BitConverter.ToInt32(array, 4);
								int num7 = (int)(this._z.TotalBytesOut & (long)((ulong)(-1)));
								bool flag15 = crc32Result2 != num5;
								if (flag15)
								{
									throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", crc32Result2, num5));
								}
								bool flag16 = num7 != num6;
								if (flag16)
								{
									throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num7, num6));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000D408 File Offset: 0x0000B608
		private void end()
		{
			bool flag = this.z == null;
			if (!flag)
			{
				bool wantCompress = this._wantCompress;
				if (wantCompress)
				{
					this._z.EndDeflate();
				}
				else
				{
					this._z.EndInflate();
				}
				this._z = null;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000D454 File Offset: 0x0000B654
		public override void Close()
		{
			bool flag = this._stream == null;
			if (!flag)
			{
				try
				{
					this.finish();
				}
				finally
				{
					this.end();
					bool flag2 = !this._leaveOpen;
					if (flag2)
					{
						this._stream.Close();
					}
					this._stream = null;
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			for (;;)
			{
				int num = this._stream.Read(this._buf1, 0, 1);
				bool flag2 = num != 1;
				if (flag2)
				{
					break;
				}
				bool flag3 = this._buf1[0] == 0;
				if (flag3)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					goto Block_3;
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
			Block_3:
			byte[] array = list.ToArray();
			return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000D568 File Offset: 0x0000B768
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = this._stream.Read(array, 0, array.Length);
			bool flag = num2 == 0;
			int num3;
			if (flag)
			{
				num3 = 0;
			}
			else
			{
				bool flag2 = num2 != 10;
				if (flag2)
				{
					throw new ZlibException("Not a valid GZIP stream.");
				}
				bool flag3 = array[0] != 31 || array[1] != 139 || array[2] != 8;
				if (flag3)
				{
					throw new ZlibException("Bad GZIP header.");
				}
				int num4 = BitConverter.ToInt32(array, 4);
				this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num4);
				num += num2;
				bool flag4 = (array[3] & 4) == 4;
				if (flag4)
				{
					num2 = this._stream.Read(array, 0, 2);
					num += num2;
					short num5 = (short)((int)array[0] + (int)array[1] * 256);
					byte[] array2 = new byte[(int)num5];
					num2 = this._stream.Read(array2, 0, array2.Length);
					bool flag5 = num2 != (int)num5;
					if (flag5)
					{
						throw new ZlibException("Unexpected end-of-file reading GZIP header.");
					}
					num += num2;
				}
				bool flag6 = (array[3] & 8) == 8;
				if (flag6)
				{
					this._GzipFileName = this.ReadZeroTerminatedString();
				}
				bool flag7 = (array[3] & 16) == 16;
				if (flag7)
				{
					this._GzipComment = this.ReadZeroTerminatedString();
				}
				bool flag8 = (array[3] & 2) == 2;
				if (flag8)
				{
					this.Read(this._buf1, 0, 1);
				}
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = this._streamMode == ZlibBaseStream.StreamMode.Undefined;
			if (flag)
			{
				bool flag2 = !this._stream.CanRead;
				if (flag2)
				{
					throw new ZlibException("The stream is not readable.");
				}
				this._streamMode = ZlibBaseStream.StreamMode.Reader;
				this.z.AvailableBytesIn = 0;
				bool flag3 = this._flavor == ZlibStreamFlavor.GZIP;
				if (flag3)
				{
					this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
					bool flag4 = this._gzipHeaderByteCount == 0;
					if (flag4)
					{
						return 0;
					}
				}
			}
			bool flag5 = this._streamMode != ZlibBaseStream.StreamMode.Reader;
			if (flag5)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			bool flag6 = count == 0;
			int num;
			if (flag6)
			{
				num = 0;
			}
			else
			{
				bool flag7 = this.nomoreinput && this._wantCompress;
				if (flag7)
				{
					num = 0;
				}
				else
				{
					bool flag8 = buffer == null;
					if (flag8)
					{
						throw new ArgumentNullException("buffer");
					}
					bool flag9 = count < 0;
					if (flag9)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					bool flag10 = offset < buffer.GetLowerBound(0);
					if (flag10)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					bool flag11 = offset + count > buffer.GetLength(0);
					if (flag11)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					this._z.OutputBuffer = buffer;
					this._z.NextOut = offset;
					this._z.AvailableBytesOut = count;
					this._z.InputBuffer = this.workingBuffer;
					int num2;
					for (;;)
					{
						bool flag12 = this._z.AvailableBytesIn == 0 && !this.nomoreinput;
						if (flag12)
						{
							this._z.NextIn = 0;
							this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
							bool flag13 = this._z.AvailableBytesIn == 0;
							if (flag13)
							{
								this.nomoreinput = true;
							}
						}
						num2 = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
						bool flag14 = this.nomoreinput && num2 == -5;
						if (flag14)
						{
							break;
						}
						bool flag15 = num2 != 0 && num2 != 1;
						if (flag15)
						{
							goto Block_20;
						}
						bool flag16 = (this.nomoreinput || num2 == 1) && this._z.AvailableBytesOut == count;
						if (flag16)
						{
							goto Block_23;
						}
						if (this._z.AvailableBytesOut <= 0 || this.nomoreinput || num2 != 0)
						{
							goto IL_02AA;
						}
					}
					return 0;
					Block_20:
					throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", this._wantCompress ? "de" : "in", num2, this._z.Message));
					Block_23:
					IL_02AA:
					bool flag17 = this._z.AvailableBytesOut > 0;
					if (flag17)
					{
						bool flag18 = num2 == 0 && this._z.AvailableBytesIn == 0;
						if (flag18)
						{
						}
						bool flag19 = this.nomoreinput;
						if (flag19)
						{
							bool wantCompress = this._wantCompress;
							if (wantCompress)
							{
								num2 = this._z.Deflate(FlushType.Finish);
								bool flag20 = num2 != 0 && num2 != 1;
								if (flag20)
								{
									throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num2, this._z.Message));
								}
							}
						}
					}
					num2 = count - this._z.AvailableBytesOut;
					bool flag21 = this.crc != null;
					if (flag21)
					{
						this.crc.SlurpBlock(buffer, offset, num2);
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000DA50 File Offset: 0x0000BC50
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000DA90 File Offset: 0x0000BC90
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006DFB File Offset: 0x00004FFB
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			try
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			try
			{
				compressor.Write(b, 0, b.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			Encoding utf = Encoding.UTF8;
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				StreamReader streamReader = new StreamReader(memoryStream, utf);
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x04000142 RID: 322
		protected internal ZlibCodec _z = null;

		// Token: 0x04000143 RID: 323
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x04000144 RID: 324
		protected internal FlushType _flushMode;

		// Token: 0x04000145 RID: 325
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x04000146 RID: 326
		protected internal CompressionMode _compressionMode;

		// Token: 0x04000147 RID: 327
		protected internal CompressionLevel _level;

		// Token: 0x04000148 RID: 328
		protected internal bool _leaveOpen;

		// Token: 0x04000149 RID: 329
		protected internal byte[] _workingBuffer;

		// Token: 0x0400014A RID: 330
		protected internal int _bufferSize = 16384;

		// Token: 0x0400014B RID: 331
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x0400014C RID: 332
		protected internal Stream _stream;

		// Token: 0x0400014D RID: 333
		protected internal CompressionStrategy Strategy = CompressionStrategy.Default;

		// Token: 0x0400014E RID: 334
		private CRC32 crc;

		// Token: 0x0400014F RID: 335
		protected internal string _GzipFileName;

		// Token: 0x04000150 RID: 336
		protected internal string _GzipComment;

		// Token: 0x04000151 RID: 337
		protected internal DateTime _GzipMtime;

		// Token: 0x04000152 RID: 338
		protected internal int _gzipHeaderByteCount;

		// Token: 0x04000153 RID: 339
		private bool nomoreinput = false;

		// Token: 0x020001FF RID: 511
		internal enum StreamMode
		{
			// Token: 0x04000D8D RID: 3469
			Writer,
			// Token: 0x04000D8E RID: 3470
			Reader,
			// Token: 0x04000D8F RID: 3471
			Undefined
		}
	}
}
