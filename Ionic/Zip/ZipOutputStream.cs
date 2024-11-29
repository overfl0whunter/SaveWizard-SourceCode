using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x02000055 RID: 85
	public class ZipOutputStream : Stream
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0001CFC8 File Offset: 0x0001B1C8
		public ZipOutputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public ZipOutputStream(string fileName)
		{
			this._alternateEncodingUsage = ZipOption.Default;
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			this._maxBufferPairs = 16;
			base..ctor();
			Stream stream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			this._Init(stream, false, fileName);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001D01C File Offset: 0x0001B21C
		public ZipOutputStream(Stream stream, bool leaveOpen)
		{
			this._alternateEncodingUsage = ZipOption.Default;
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			this._maxBufferPairs = 16;
			base..ctor();
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001D050 File Offset: 0x0001B250
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._outputStream = (stream.CanRead ? stream : new CountingStream(stream));
			this.CompressionLevel = CompressionLevel.Default;
			this.CompressionMethod = CompressionMethod.Deflate;
			this._encryption = EncryptionAlgorithm.None;
			this._entriesWritten = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
			this._zip64 = Zip64Option.Default;
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this.Strategy = CompressionStrategy.Default;
			this._name = name ?? "(stream)";
			this.ParallelDeflateThreshold = -1L;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public override string ToString()
		{
			return string.Format("ZipOutputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000E1 RID: 225
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		public string Password
		{
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._password = value;
				bool flag = this._password == null;
				if (flag)
				{
					this._encryption = EncryptionAlgorithm.None;
				}
				else
				{
					bool flag2 = this._encryption == EncryptionAlgorithm.None;
					if (flag2)
					{
						this._encryption = EncryptionAlgorithm.PkzipWeak;
					}
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001D15C File Offset: 0x0001B35C
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001D174 File Offset: 0x0001B374
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._encryption;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				bool flag = value == EncryptionAlgorithm.Unsupported;
				if (flag)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._encryption = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001D1C1 File Offset: 0x0001B3C1
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001D1C9 File Offset: 0x0001B3C9
		public int CodecBufferSize { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001D1D2 File Offset: 0x0001B3D2
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0001D1DA File Offset: 0x0001B3DA
		public CompressionStrategy Strategy { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._timestamp = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001D22E File Offset: 0x0001B42E
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0001D236 File Offset: 0x0001B436
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001D23F File Offset: 0x0001B43F
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0001D247 File Offset: 0x0001B447
		public CompressionMethod CompressionMethod { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001D250 File Offset: 0x0001B450
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001D268 File Offset: 0x0001B468
		public string Comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._comment = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001D29C File Offset: 0x0001B49C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		public Zip64Option EnableZip64
		{
			get
			{
				return this._zip64;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._zip64 = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public bool OutputUsedZip64
		{
			get
			{
				return this._anyEntriesUsedZip64 || this._directoryNeededZip64;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001D30C File Offset: 0x0001B50C
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001D327 File Offset: 0x0001B527
		public bool IgnoreCase
		{
			get
			{
				return !this._DontIgnoreCase;
			}
			set
			{
				this._DontIgnoreCase = !value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001D334 File Offset: 0x0001B534
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001D360 File Offset: 0x0001B560
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete. It will be removed in a future version of the library. Use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.UTF8 && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.UTF8;
					this._alternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					this._alternateEncoding = ZipOutputStream.DefaultEncoding;
					this._alternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001D3C9 File Offset: 0x0001B5C9
		[Obsolete("use AlternateEncoding and AlternateEncodingUsage instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				bool flag = this._alternateEncodingUsage == ZipOption.AsNecessary;
				Encoding encoding;
				if (flag)
				{
					encoding = this._alternateEncoding;
				}
				else
				{
					encoding = null;
				}
				return encoding;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001D400 File Offset: 0x0001B600
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0001D418 File Offset: 0x0001B618
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001D424 File Offset: 0x0001B624
		public static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.GetEncoding("IBM437");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001D478 File Offset: 0x0001B678
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0001D440 File Offset: 0x0001B640
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				bool flag = value != 0L && value != -1L && value < 65536L;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value must be greater than 64k, or 0, or -1");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001D490 File Offset: 0x0001B690
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				bool flag = value < 4;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		private void InsureUniqueEntry(ZipEntry ze1)
		{
			bool flag = this._entriesWritten.ContainsKey(ze1.FileName);
			if (flag)
			{
				this._exceptionPending = true;
				throw new ArgumentException(string.Format("The entry '{0}' already exists in the zip archive.", ze1.FileName));
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001D51C File Offset: 0x0001B71C
		internal Stream OutputStream
		{
			get
			{
				return this._outputStream;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001D534 File Offset: 0x0001B734
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001D54C File Offset: 0x0001B74C
		public bool ContainsEntry(string name)
		{
			return this._entriesWritten.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001D570 File Offset: 0x0001B770
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			bool flag = buffer == null;
			if (flag)
			{
				this._exceptionPending = true;
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = this._currentEntry == null;
			if (flag2)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
			}
			bool isDirectory = this._currentEntry.IsDirectory;
			if (isDirectory)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
			}
			bool needToWriteEntryHeader = this._needToWriteEntryHeader;
			if (needToWriteEntryHeader)
			{
				this._InitiateCurrentEntry(false);
			}
			bool flag3 = count != 0;
			if (flag3)
			{
				this._entryOutputStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001D628 File Offset: 0x0001B828
		public ZipEntry PutNextEntry(string entryName)
		{
			bool flag = string.IsNullOrEmpty(entryName);
			if (flag)
			{
				throw new ArgumentNullException("entryName");
			}
			bool disposed = this._disposed;
			if (disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			this._FinishCurrentEntry();
			this._currentEntry = ZipEntry.CreateForZipOutputStream(entryName);
			this._currentEntry._container = new ZipContainer(this);
			ZipEntry currentEntry = this._currentEntry;
			currentEntry._BitField |= 8;
			this._currentEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			this._currentEntry.CompressionLevel = this.CompressionLevel;
			this._currentEntry.CompressionMethod = this.CompressionMethod;
			this._currentEntry.Password = this._password;
			this._currentEntry.Encryption = this.Encryption;
			this._currentEntry.AlternateEncoding = this.AlternateEncoding;
			this._currentEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			bool flag2 = entryName.EndsWith("/");
			if (flag2)
			{
				this._currentEntry.MarkAsDirectory();
			}
			this._currentEntry.EmitTimesInWindowsFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Windows) > ZipEntryTimestamp.None;
			this._currentEntry.EmitTimesInUnixFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Unix) > ZipEntryTimestamp.None;
			this.InsureUniqueEntry(this._currentEntry);
			this._needToWriteEntryHeader = true;
			return this._currentEntry;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001D790 File Offset: 0x0001B990
		private void _InitiateCurrentEntry(bool finishing)
		{
			this._entriesWritten.Add(this._currentEntry.FileName, this._currentEntry);
			this._entryCount++;
			bool flag = this._entryCount > 65534 && this._zip64 == Zip64Option.Default;
			if (flag)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
			}
			this._currentEntry.WriteHeader(this._outputStream, finishing ? 99 : 0);
			this._currentEntry.StoreRelativeOffset();
			bool flag2 = !this._currentEntry.IsDirectory;
			if (flag2)
			{
				this._currentEntry.WriteSecurityMetadata(this._outputStream);
				this._currentEntry.PrepOutputStream(this._outputStream, finishing ? 0L : (-1L), out this._outputCounter, out this._encryptor, out this._deflater, out this._entryOutputStream);
			}
			this._needToWriteEntryHeader = false;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001D880 File Offset: 0x0001BA80
		private void _FinishCurrentEntry()
		{
			bool flag = this._currentEntry != null;
			if (flag)
			{
				bool needToWriteEntryHeader = this._needToWriteEntryHeader;
				if (needToWriteEntryHeader)
				{
					this._InitiateCurrentEntry(true);
				}
				this._currentEntry.FinishOutputStream(this._outputStream, this._outputCounter, this._encryptor, this._deflater, this._entryOutputStream);
				this._currentEntry.PostProcessOutput(this._outputStream);
				bool flag2 = this._currentEntry.OutputUsedZip64 != null;
				if (flag2)
				{
					this._anyEntriesUsedZip64 |= this._currentEntry.OutputUsedZip64.Value;
				}
				this._outputCounter = null;
				this._encryptor = (this._deflater = null);
				this._entryOutputStream = null;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001D948 File Offset: 0x0001BB48
		protected override void Dispose(bool disposing)
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = !this._exceptionPending;
					if (flag)
					{
						this._FinishCurrentEntry();
						this._directoryNeededZip64 = ZipOutput.WriteCentralDirectoryStructure(this._outputStream, this._entriesWritten.Values, 1U, this._zip64, this.Comment, new ZipContainer(this));
						CountingStream countingStream = this._outputStream as CountingStream;
						bool flag2 = countingStream != null;
						Stream stream;
						if (flag2)
						{
							stream = countingStream.WrappedStream;
							countingStream.Dispose();
						}
						else
						{
							stream = this._outputStream;
						}
						bool flag3 = !this._leaveUnderlyingStreamOpen;
						if (flag3)
						{
							stream.Dispose();
						}
						this._outputStream = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001DA28 File Offset: 0x0001BC28
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001DA50 File Offset: 0x0001BC50
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Position
		{
			get
			{
				return this._outputStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000021C5 File Offset: 0x000003C5
		public override void Flush()
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001DA6D File Offset: 0x0001BC6D
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Read");
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001DA7A File Offset: 0x0001BC7A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek");
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400029F RID: 671
		private EncryptionAlgorithm _encryption;

		// Token: 0x040002A0 RID: 672
		private ZipEntryTimestamp _timestamp;

		// Token: 0x040002A1 RID: 673
		internal string _password;

		// Token: 0x040002A2 RID: 674
		private string _comment;

		// Token: 0x040002A3 RID: 675
		private Stream _outputStream;

		// Token: 0x040002A4 RID: 676
		private ZipEntry _currentEntry;

		// Token: 0x040002A5 RID: 677
		internal Zip64Option _zip64;

		// Token: 0x040002A6 RID: 678
		private Dictionary<string, ZipEntry> _entriesWritten;

		// Token: 0x040002A7 RID: 679
		private int _entryCount;

		// Token: 0x040002A8 RID: 680
		private ZipOption _alternateEncodingUsage;

		// Token: 0x040002A9 RID: 681
		private Encoding _alternateEncoding;

		// Token: 0x040002AA RID: 682
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x040002AB RID: 683
		private bool _disposed;

		// Token: 0x040002AC RID: 684
		private bool _exceptionPending;

		// Token: 0x040002AD RID: 685
		private bool _anyEntriesUsedZip64;

		// Token: 0x040002AE RID: 686
		private bool _directoryNeededZip64;

		// Token: 0x040002AF RID: 687
		private CountingStream _outputCounter;

		// Token: 0x040002B0 RID: 688
		private Stream _encryptor;

		// Token: 0x040002B1 RID: 689
		private Stream _deflater;

		// Token: 0x040002B2 RID: 690
		private CrcCalculatorStream _entryOutputStream;

		// Token: 0x040002B3 RID: 691
		private bool _needToWriteEntryHeader;

		// Token: 0x040002B4 RID: 692
		private string _name;

		// Token: 0x040002B5 RID: 693
		private bool _DontIgnoreCase;

		// Token: 0x040002B6 RID: 694
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x040002B7 RID: 695
		private long _ParallelDeflateThreshold;

		// Token: 0x040002B8 RID: 696
		private int _maxBufferPairs;
	}
}
