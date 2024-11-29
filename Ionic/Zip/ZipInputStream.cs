using System;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x02000054 RID: 84
	public class ZipInputStream : Stream
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001CB86 File Offset: 0x0001AD86
		public ZipInputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._Init(stream, false, fileName);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001CBBD File Offset: 0x0001ADBD
		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._inputStream = stream;
			bool flag = !this._inputStream.CanRead;
			if (flag)
			{
				throw new ZipException("The stream must be readable.");
			}
			this._container = new ZipContainer(this);
			this._provisionalAlternateEncoding = Encoding.GetEncoding("IBM437");
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this._findRequired = true;
			this._name = name ?? "(stream)";
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001CC40 File Offset: 0x0001AE40
		public override string ToString()
		{
			return string.Format("ZipInputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001CC70 File Offset: 0x0001AE70
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001CC88 File Offset: 0x0001AE88
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this._provisionalAlternateEncoding;
			}
			set
			{
				this._provisionalAlternateEncoding = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001CC92 File Offset: 0x0001AE92
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0001CC9A File Offset: 0x0001AE9A
		public int CodecBufferSize { get; set; }

		// Token: 0x170000DA RID: 218
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		public string Password
		{
			set
			{
				bool closed = this._closed;
				if (closed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._Password = value;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001CCD6 File Offset: 0x0001AED6
		private void SetupStream()
		{
			this._crcStream = this._currentEntry.InternalOpenReader(this._Password);
			this._LeftToRead = this._crcStream.Length;
			this._needSetup = false;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001CD08 File Offset: 0x0001AF08
		internal Stream ReadStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001CD20 File Offset: 0x0001AF20
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool closed = this._closed;
			if (closed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			bool needSetup = this._needSetup;
			if (needSetup)
			{
				this.SetupStream();
			}
			bool flag = this._LeftToRead == 0L;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int num2 = ((this._LeftToRead > (long)count) ? count : ((int)this._LeftToRead));
				int num3 = this._crcStream.Read(buffer, offset, num2);
				this._LeftToRead -= (long)num3;
				bool flag2 = this._LeftToRead == 0L;
				if (flag2)
				{
					int crc = this._crcStream.Crc;
					this._currentEntry.VerifyCrcAfterExtract(crc);
					this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
				}
				num = num3;
			}
			return num;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public ZipEntry GetNextEntry()
		{
			bool findRequired = this._findRequired;
			if (findRequired)
			{
				long num = SharedUtilities.FindSignature(this._inputStream, 67324752);
				bool flag = num == -1L;
				if (flag)
				{
					return null;
				}
				this._inputStream.Seek(-4L, SeekOrigin.Current);
			}
			else
			{
				bool firstEntry = this._firstEntry;
				if (firstEntry)
				{
					this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
				}
			}
			this._currentEntry = ZipEntry.ReadEntry(this._container, !this._firstEntry);
			this._endOfEntry = this._inputStream.Position;
			this._firstEntry = true;
			this._needSetup = true;
			this._findRequired = false;
			return this._currentEntry;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		protected override void Dispose(bool disposing)
		{
			bool closed = this._closed;
			if (!closed)
			{
				if (disposing)
				{
					bool exceptionPending = this._exceptionPending;
					if (exceptionPending)
					{
						return;
					}
					bool flag = !this._leaveUnderlyingStreamOpen;
					if (flag)
					{
						this._inputStream.Dispose();
					}
				}
				this._closed = true;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001CF08 File Offset: 0x0001B108
		public override bool CanSeek
		{
			get
			{
				return this._inputStream.CanSeek;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001CF28 File Offset: 0x0001B128
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public override long Length
		{
			get
			{
				return this._inputStream.Length;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001CF5C File Offset: 0x0001B15C
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001CF79 File Offset: 0x0001B179
		public override long Position
		{
			get
			{
				return this._inputStream.Position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001CF85 File Offset: 0x0001B185
		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001CF92 File Offset: 0x0001B192
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this._findRequired = true;
			return this._inputStream.Seek(offset, origin);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400028C RID: 652
		private Stream _inputStream;

		// Token: 0x0400028D RID: 653
		private Encoding _provisionalAlternateEncoding;

		// Token: 0x0400028E RID: 654
		private ZipEntry _currentEntry;

		// Token: 0x0400028F RID: 655
		private bool _firstEntry;

		// Token: 0x04000290 RID: 656
		private bool _needSetup;

		// Token: 0x04000291 RID: 657
		private ZipContainer _container;

		// Token: 0x04000292 RID: 658
		private CrcCalculatorStream _crcStream;

		// Token: 0x04000293 RID: 659
		private long _LeftToRead;

		// Token: 0x04000294 RID: 660
		internal string _Password;

		// Token: 0x04000295 RID: 661
		private long _endOfEntry;

		// Token: 0x04000296 RID: 662
		private string _name;

		// Token: 0x04000297 RID: 663
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x04000298 RID: 664
		private bool _closed;

		// Token: 0x04000299 RID: 665
		private bool _findRequired;

		// Token: 0x0400029A RID: 666
		private bool _exceptionPending;
	}
}
