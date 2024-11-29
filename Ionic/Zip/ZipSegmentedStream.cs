using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000057 RID: 87
	internal class ZipSegmentedStream : Stream
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001DEEC File Offset: 0x0001C0EC
		private ZipSegmentedStream()
		{
			this._exceptionPending = false;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001DF00 File Offset: 0x0001C100
		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.ReadOnly,
				CurrentSegment = initialDiskNumber,
				_maxDiskNumber = maxDiskNumber,
				_baseName = name
			};
			zipSegmentedStream._SetReadStream();
			return zipSegmentedStream;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001DF40 File Offset: 0x0001C140
		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.Write,
				CurrentSegment = 0U,
				_baseName = name,
				_maxSegmentSize = maxSegmentSize,
				_baseDir = Path.GetDirectoryName(name)
			};
			bool flag = zipSegmentedStream._baseDir == "";
			if (flag)
			{
				zipSegmentedStream._baseDir = ".";
			}
			zipSegmentedStream._SetWriteStream(0U);
			return zipSegmentedStream;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		public static Stream ForUpdate(string name, uint diskNumber)
		{
			bool flag = diskNumber >= 99U;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string text = string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)), diskNumber + 1U);
			return File.Open(text, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001E002 File Offset: 0x0001C202
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001E00A File Offset: 0x0001C20A
		public bool ContiguousWrite { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001E014 File Offset: 0x0001C214
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0001E02C File Offset: 0x0001C22C
		public uint CurrentSegment
		{
			get
			{
				return this._currentDiskNumber;
			}
			private set
			{
				this._currentDiskNumber = value;
				this._currentName = null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001E040 File Offset: 0x0001C240
		public string CurrentName
		{
			get
			{
				bool flag = this._currentName == null;
				if (flag)
				{
					this._currentName = this._NameForSegment(this.CurrentSegment);
				}
				return this._currentName;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001E078 File Offset: 0x0001C278
		public string CurrentTempName
		{
			get
			{
				return this._currentTempName;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001E090 File Offset: 0x0001C290
		private string _NameForSegment(uint diskNumber)
		{
			bool flag = diskNumber >= 99U;
			if (flag)
			{
				this._exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(this._baseName), Path.GetFileNameWithoutExtension(this._baseName)), diskNumber + 1U);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		public uint ComputeSegment(int length)
		{
			bool flag = this._innerStream.Position + (long)length > (long)this._maxSegmentSize;
			uint num;
			if (flag)
			{
				num = this.CurrentSegment + 1U;
			}
			else
			{
				num = this.CurrentSegment;
			}
			return num;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001E130 File Offset: 0x0001C330
		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", new object[]
			{
				"ZipSegmentedStream",
				this.CurrentName,
				this.rwMode.ToString(),
				this.Position
			});
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001E188 File Offset: 0x0001C388
		private void _SetReadStream()
		{
			bool flag = this._innerStream != null;
			if (flag)
			{
				this._innerStream.Dispose();
			}
			bool flag2 = this.CurrentSegment + 1U == this._maxDiskNumber;
			if (flag2)
			{
				this._currentName = this._baseName;
			}
			this._innerStream = File.OpenRead(this.CurrentName);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = this.rwMode != ZipSegmentedStream.RwMode.ReadOnly;
			if (flag)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = this._innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				bool flag2 = this._innerStream.Position != this._innerStream.Length;
				if (flag2)
				{
					this._exceptionPending = true;
					throw new ZipException(string.Format("Read error in file {0}", this.CurrentName));
				}
				bool flag3 = this.CurrentSegment + 1U == this._maxDiskNumber;
				if (flag3)
				{
					return num;
				}
				uint currentSegment = this.CurrentSegment;
				this.CurrentSegment = currentSegment + 1U;
				this._SetReadStream();
				offset += num2;
				count -= num2;
				num2 = this._innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		private void _SetWriteStream(uint increment)
		{
			bool flag = this._innerStream != null;
			if (flag)
			{
				this._innerStream.Dispose();
				bool flag2 = File.Exists(this.CurrentName);
				if (flag2)
				{
					File.Delete(this.CurrentName);
				}
				File.Move(this._currentTempName, this.CurrentName);
			}
			bool flag3 = increment > 0U;
			if (flag3)
			{
				this.CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(this._baseDir, out this._innerStream, out this._currentTempName);
			bool flag4 = this.CurrentSegment == 0U;
			if (flag4)
			{
				this._innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001E380 File Offset: 0x0001C580
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this.rwMode != ZipSegmentedStream.RwMode.Write;
			if (flag)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			bool contiguousWrite = this.ContiguousWrite;
			if (contiguousWrite)
			{
				bool flag2 = this._innerStream.Position + (long)count > (long)this._maxSegmentSize;
				if (flag2)
				{
					this._SetWriteStream(1U);
				}
			}
			else
			{
				while (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					int num = this._maxSegmentSize - (int)this._innerStream.Position;
					this._innerStream.Write(buffer, offset, num);
					this._SetWriteStream(1U);
					count -= num;
					offset += num;
				}
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001E44C File Offset: 0x0001C64C
		public long TruncateBackward(uint diskNumber, long offset)
		{
			bool flag = diskNumber >= 99U;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			bool flag2 = this.rwMode != ZipSegmentedStream.RwMode.Write;
			if (flag2)
			{
				this._exceptionPending = true;
				throw new ZipException("bad state.");
			}
			bool flag3 = diskNumber == this.CurrentSegment;
			long num2;
			if (flag3)
			{
				long num = this._innerStream.Seek(offset, SeekOrigin.Begin);
				num2 = num;
			}
			else
			{
				bool flag4 = this._innerStream != null;
				if (flag4)
				{
					this._innerStream.Dispose();
					bool flag5 = File.Exists(this._currentTempName);
					if (flag5)
					{
						File.Delete(this._currentTempName);
					}
				}
				for (uint num3 = this.CurrentSegment - 1U; num3 > diskNumber; num3 -= 1U)
				{
					string text = this._NameForSegment(num3);
					bool flag6 = File.Exists(text);
					if (flag6)
					{
						File.Delete(text);
					}
				}
				this.CurrentSegment = diskNumber;
				for (int i = 0; i < 3; i++)
				{
					try
					{
						this._currentTempName = SharedUtilities.InternalGetTempFileName();
						File.Move(this.CurrentName, this._currentTempName);
						break;
					}
					catch (IOException)
					{
						bool flag7 = i == 2;
						if (flag7)
						{
							throw;
						}
					}
				}
				this._innerStream = new FileStream(this._currentTempName, FileMode.Open);
				long num4 = this._innerStream.Seek(offset, SeekOrigin.Begin);
				num2 = num4;
			}
			return num2;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001E5BC File Offset: 0x0001C7BC
		public override bool CanRead
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.ReadOnly && this._innerStream != null && this._innerStream.CanRead;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		public override bool CanSeek
		{
			get
			{
				return this._innerStream != null && this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001E618 File Offset: 0x0001C818
		public override bool CanWrite
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.Write && this._innerStream != null && this._innerStream.CanWrite;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001E649 File Offset: 0x0001C849
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001E658 File Offset: 0x0001C858
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001E678 File Offset: 0x0001C878
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001E695 File Offset: 0x0001C895
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		public override void SetLength(long value)
		{
			bool flag = this.rwMode != ZipSegmentedStream.RwMode.Write;
			if (flag)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException();
			}
			this._innerStream.SetLength(value);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001E708 File Offset: 0x0001C908
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = this._innerStream != null;
				if (flag)
				{
					this._innerStream.Dispose();
					bool flag2 = this.rwMode == ZipSegmentedStream.RwMode.Write;
					if (flag2)
					{
						bool exceptionPending = this._exceptionPending;
						if (exceptionPending)
						{
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040002BC RID: 700
		private ZipSegmentedStream.RwMode rwMode;

		// Token: 0x040002BD RID: 701
		private bool _exceptionPending;

		// Token: 0x040002BE RID: 702
		private string _baseName;

		// Token: 0x040002BF RID: 703
		private string _baseDir;

		// Token: 0x040002C0 RID: 704
		private string _currentName;

		// Token: 0x040002C1 RID: 705
		private string _currentTempName;

		// Token: 0x040002C2 RID: 706
		private uint _currentDiskNumber;

		// Token: 0x040002C3 RID: 707
		private uint _maxDiskNumber;

		// Token: 0x040002C4 RID: 708
		private int _maxSegmentSize;

		// Token: 0x040002C5 RID: 709
		private Stream _innerStream;

		// Token: 0x02000207 RID: 519
		private enum RwMode
		{
			// Token: 0x04000DA4 RID: 3492
			None,
			// Token: 0x04000DA5 RID: 3493
			ReadOnly,
			// Token: 0x04000DA6 RID: 3494
			Write
		}
	}
}
