using System;
using System.IO;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x02000056 RID: 86
	internal class ZipContainer
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0001DA87 File Offset: 0x0001BC87
		public ZipContainer(object o)
		{
			this._zf = o as ZipFile;
			this._zos = o as ZipOutputStream;
			this._zis = o as ZipInputStream;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		public ZipFile ZipFile
		{
			get
			{
				return this._zf;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		public ZipOutputStream ZipOutputStream
		{
			get
			{
				return this._zos;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		public string Name
		{
			get
			{
				bool flag = this._zf != null;
				string text;
				if (flag)
				{
					text = this._zf.Name;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						throw new NotSupportedException();
					}
					text = this._zos.Name;
				}
				return text;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001DB34 File Offset: 0x0001BD34
		public string Password
		{
			get
			{
				bool flag = this._zf != null;
				string text;
				if (flag)
				{
					text = this._zf._Password;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						text = this._zis._Password;
					}
					else
					{
						text = this._zos._password;
					}
				}
				return text;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001DB88 File Offset: 0x0001BD88
		public Zip64Option Zip64
		{
			get
			{
				bool flag = this._zf != null;
				Zip64Option zip64Option;
				if (flag)
				{
					zip64Option = this._zf._zip64;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						throw new NotSupportedException();
					}
					zip64Option = this._zos._zip64;
				}
				return zip64Option;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		public int BufferSize
		{
			get
			{
				bool flag = this._zf != null;
				int num;
				if (flag)
				{
					num = this._zf.BufferSize;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						throw new NotSupportedException();
					}
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001DC18 File Offset: 0x0001BE18
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public ParallelDeflateOutputStream ParallelDeflater
		{
			get
			{
				bool flag = this._zf != null;
				ParallelDeflateOutputStream parallelDeflateOutputStream;
				if (flag)
				{
					parallelDeflateOutputStream = this._zf.ParallelDeflater;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						parallelDeflateOutputStream = null;
					}
					else
					{
						parallelDeflateOutputStream = this._zos.ParallelDeflater;
					}
				}
				return parallelDeflateOutputStream;
			}
			set
			{
				bool flag = this._zf != null;
				if (flag)
				{
					this._zf.ParallelDeflater = value;
				}
				else
				{
					bool flag2 = this._zos != null;
					if (flag2)
					{
						this._zos.ParallelDeflater = value;
					}
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
		public long ParallelDeflateThreshold
		{
			get
			{
				bool flag = this._zf != null;
				long num;
				if (flag)
				{
					num = this._zf.ParallelDeflateThreshold;
				}
				else
				{
					num = this._zos.ParallelDeflateThreshold;
				}
				return num;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				bool flag = this._zf != null;
				int num;
				if (flag)
				{
					num = this._zf.ParallelDeflateMaxBufferPairs;
				}
				else
				{
					num = this._zos.ParallelDeflateMaxBufferPairs;
				}
				return num;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001DD18 File Offset: 0x0001BF18
		public int CodecBufferSize
		{
			get
			{
				bool flag = this._zf != null;
				int num;
				if (flag)
				{
					num = this._zf.CodecBufferSize;
				}
				else
				{
					bool flag2 = this._zis != null;
					if (flag2)
					{
						num = this._zis.CodecBufferSize;
					}
					else
					{
						num = this._zos.CodecBufferSize;
					}
				}
				return num;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public CompressionStrategy Strategy
		{
			get
			{
				bool flag = this._zf != null;
				CompressionStrategy compressionStrategy;
				if (flag)
				{
					compressionStrategy = this._zf.Strategy;
				}
				else
				{
					compressionStrategy = this._zos.Strategy;
				}
				return compressionStrategy;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				bool flag = this._zf != null;
				Zip64Option zip64Option;
				if (flag)
				{
					zip64Option = this._zf.UseZip64WhenSaving;
				}
				else
				{
					zip64Option = this._zos.EnableZip64;
				}
				return zip64Option;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		public Encoding AlternateEncoding
		{
			get
			{
				bool flag = this._zf != null;
				Encoding encoding;
				if (flag)
				{
					encoding = this._zf.AlternateEncoding;
				}
				else
				{
					bool flag2 = this._zos != null;
					if (flag2)
					{
						encoding = this._zos.AlternateEncoding;
					}
					else
					{
						encoding = null;
					}
				}
				return encoding;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001DE28 File Offset: 0x0001C028
		public Encoding DefaultEncoding
		{
			get
			{
				bool flag = this._zf != null;
				Encoding encoding;
				if (flag)
				{
					encoding = ZipFile.DefaultEncoding;
				}
				else
				{
					bool flag2 = this._zos != null;
					if (flag2)
					{
						encoding = ZipOutputStream.DefaultEncoding;
					}
					else
					{
						encoding = null;
					}
				}
				return encoding;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001DE68 File Offset: 0x0001C068
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				bool flag = this._zf != null;
				ZipOption zipOption;
				if (flag)
				{
					zipOption = this._zf.AlternateEncodingUsage;
				}
				else
				{
					bool flag2 = this._zos != null;
					if (flag2)
					{
						zipOption = this._zos.AlternateEncodingUsage;
					}
					else
					{
						zipOption = ZipOption.Default;
					}
				}
				return zipOption;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public Stream ReadStream
		{
			get
			{
				bool flag = this._zf != null;
				Stream stream;
				if (flag)
				{
					stream = this._zf.ReadStream;
				}
				else
				{
					stream = this._zis.ReadStream;
				}
				return stream;
			}
		}

		// Token: 0x040002B9 RID: 697
		private ZipFile _zf;

		// Token: 0x040002BA RID: 698
		private ZipOutputStream _zos;

		// Token: 0x040002BB RID: 699
		private ZipInputStream _zis;
	}
}
