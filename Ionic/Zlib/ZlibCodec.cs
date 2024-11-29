using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000027 RID: 39
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public ZlibCodec()
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public ZlibCodec(CompressionMode mode)
		{
			bool flag = mode == CompressionMode.Compress;
			if (flag)
			{
				int num = this.InitializeDeflate();
				bool flag2 = num != 0;
				if (flag2)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				bool flag3 = mode == CompressionMode.Decompress;
				if (!flag3)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				int num2 = this.InitializeInflate();
				bool flag4 = num2 != 0;
				if (flag4)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			bool flag = this.dstate != null;
			if (flag)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public int Inflate(FlushType flush)
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public int EndInflate()
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			int num = this.istate.End();
			this.istate = null;
			return num;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public int SyncInflate()
		{
			bool flag = this.istate == null;
			if (flag)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000DF00 File Offset: 0x0000C100
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000DF28 File Offset: 0x0000C128
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000DF50 File Offset: 0x0000C150
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			bool flag = this.istate != null;
			if (flag)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		public int Deflate(FlushType flush)
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		public int EndDeflate()
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000E01C File Offset: 0x0000C21C
		public void ResetDeflate()
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000E050 File Offset: 0x0000C250
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			bool flag = this.dstate == null;
			if (flag)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000E088 File Offset: 0x0000C288
		public int SetDictionary(byte[] dictionary)
		{
			bool flag = this.istate != null;
			int num;
			if (flag)
			{
				num = this.istate.SetDictionary(dictionary);
			}
			else
			{
				bool flag2 = this.dstate != null;
				if (!flag2)
				{
					throw new ZlibException("No Inflate or Deflate state!");
				}
				num = this.dstate.SetDictionary(dictionary);
			}
			return num;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			bool flag = num > this.AvailableBytesOut;
			if (flag)
			{
				num = this.AvailableBytesOut;
			}
			bool flag2 = num == 0;
			if (!flag2)
			{
				bool flag3 = this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num;
				if (flag3)
				{
					throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
				}
				Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
				this.NextOut += num;
				this.dstate.nextPending += num;
				this.TotalBytesOut += (long)num;
				this.AvailableBytesOut -= num;
				this.dstate.pendingCount -= num;
				bool flag4 = this.dstate.pendingCount == 0;
				if (flag4)
				{
					this.dstate.nextPending = 0;
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000E248 File Offset: 0x0000C448
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			bool flag = num > size;
			if (flag)
			{
				num = size;
			}
			bool flag2 = num == 0;
			int num2;
			if (flag2)
			{
				num2 = 0;
			}
			else
			{
				this.AvailableBytesIn -= num;
				bool wantRfc1950HeaderBytes = this.dstate.WantRfc1950HeaderBytes;
				if (wantRfc1950HeaderBytes)
				{
					this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
				}
				Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
				this.NextIn += num;
				this.TotalBytesIn += (long)num;
				num2 = num;
			}
			return num2;
		}

		// Token: 0x04000154 RID: 340
		public byte[] InputBuffer;

		// Token: 0x04000155 RID: 341
		public int NextIn;

		// Token: 0x04000156 RID: 342
		public int AvailableBytesIn;

		// Token: 0x04000157 RID: 343
		public long TotalBytesIn;

		// Token: 0x04000158 RID: 344
		public byte[] OutputBuffer;

		// Token: 0x04000159 RID: 345
		public int NextOut;

		// Token: 0x0400015A RID: 346
		public int AvailableBytesOut;

		// Token: 0x0400015B RID: 347
		public long TotalBytesOut;

		// Token: 0x0400015C RID: 348
		public string Message;

		// Token: 0x0400015D RID: 349
		internal DeflateManager dstate;

		// Token: 0x0400015E RID: 350
		internal InflateManager istate;

		// Token: 0x0400015F RID: 351
		internal uint _Adler32;

		// Token: 0x04000160 RID: 352
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x04000161 RID: 353
		public int WindowBits = 15;

		// Token: 0x04000162 RID: 354
		public CompressionStrategy Strategy = CompressionStrategy.Default;
	}
}
