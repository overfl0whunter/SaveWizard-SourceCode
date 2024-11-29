using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000040 RID: 64
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000F65A File Offset: 0x0000D85A
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000F678 File Offset: 0x0000D878
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000F698 File Offset: 0x0000D898
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F6EB File Offset: 0x0000D8EB
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000F71C File Offset: 0x0000D91C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000F740 File Offset: 0x0000D940
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F758 File Offset: 0x0000D958
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006DFB File Offset: 0x00004FFB
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F021 File Offset: 0x0000D221
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F785 File Offset: 0x0000D985
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x040001A7 RID: 423
		private long _originalPosition;

		// Token: 0x040001A8 RID: 424
		private Stream _innerStream;
	}
}
