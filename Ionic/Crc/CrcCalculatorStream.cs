using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x0200002B RID: 43
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000ED4E File Offset: 0x0000CF4E
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			bool flag = length < 0L;
			if (flag)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000EE02 File Offset: 0x0000D002
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000EE3C File Offset: 0x0000D03C
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000EE7C File Offset: 0x0000D07C
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000EE94 File Offset: 0x0000D094
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			bool flag = this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit;
			if (flag)
			{
				bool flag2 = this._Crc32.TotalBytesRead >= this._lengthLimit;
				if (flag2)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				bool flag3 = num2 < (long)count;
				if (flag3)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			bool flag4 = num3 > 0;
			if (flag4)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000EF38 File Offset: 0x0000D138
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = count > 0;
			if (flag)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000EF6C File Offset: 0x0000D16C
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public override long Length
		{
			get
			{
				bool flag = this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit;
				long num;
				if (flag)
				{
					num = this._innerStream.Length;
				}
				else
				{
					num = this._lengthLimit;
				}
				return num;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000F004 File Offset: 0x0000D204
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000F021 File Offset: 0x0000D221
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000F02C File Offset: 0x0000D22C
		public override void Close()
		{
			base.Close();
			bool flag = !this._leaveOpen;
			if (flag)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x04000175 RID: 373
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x04000176 RID: 374
		internal Stream _innerStream;

		// Token: 0x04000177 RID: 375
		private CRC32 _Crc32;

		// Token: 0x04000178 RID: 376
		private long _lengthLimit = -99L;

		// Token: 0x04000179 RID: 377
		private bool _leaveOpen;
	}
}
