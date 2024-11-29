using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000042 RID: 66
	public class CountingStream : Stream
	{
		// Token: 0x060001FC RID: 508 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public CountingStream(Stream stream)
		{
			this._s = stream;
			try
			{
				this._initialOffset = this._s.Position;
			}
			catch
			{
				this._initialOffset = 0L;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00010230 File Offset: 0x0000E430
		public Stream WrappedStream
		{
			get
			{
				return this._s;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00010248 File Offset: 0x0000E448
		public long BytesWritten
		{
			get
			{
				return this._bytesWritten;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00010260 File Offset: 0x0000E460
		public long BytesRead
		{
			get
			{
				return this._bytesRead;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00010278 File Offset: 0x0000E478
		public void Adjust(long delta)
		{
			this._bytesWritten -= delta;
			bool flag = this._bytesWritten < 0L;
			if (flag)
			{
				throw new InvalidOperationException();
			}
			bool flag2 = this._s is CountingStream;
			if (flag2)
			{
				((CountingStream)this._s).Adjust(delta);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000102CC File Offset: 0x0000E4CC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._s.Read(buffer, offset, count);
			this._bytesRead += (long)num;
			return num;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010300 File Offset: 0x0000E500
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = count == 0;
			if (!flag)
			{
				this._s.Write(buffer, offset, count);
				this._bytesWritten += (long)count;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00010338 File Offset: 0x0000E538
		public override bool CanRead
		{
			get
			{
				return this._s.CanRead;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00010358 File Offset: 0x0000E558
		public override bool CanSeek
		{
			get
			{
				return this._s.CanSeek;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00010378 File Offset: 0x0000E578
		public override bool CanWrite
		{
			get
			{
				return this._s.CanWrite;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010395 File Offset: 0x0000E595
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000103A4 File Offset: 0x0000E5A4
		public override long Length
		{
			get
			{
				return this._s.Length;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000103C4 File Offset: 0x0000E5C4
		public long ComputedPosition
		{
			get
			{
				return this._initialOffset + this._bytesWritten;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000103E4 File Offset: 0x0000E5E4
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00010401 File Offset: 0x0000E601
		public override long Position
		{
			get
			{
				return this._s.Position;
			}
			set
			{
				this._s.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00010414 File Offset: 0x0000E614
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._s.Seek(offset, origin);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010433 File Offset: 0x0000E633
		public override void SetLength(long value)
		{
			this._s.SetLength(value);
		}

		// Token: 0x040001AC RID: 428
		private Stream _s;

		// Token: 0x040001AD RID: 429
		private long _bytesWritten;

		// Token: 0x040001AE RID: 430
		private long _bytesRead;

		// Token: 0x040001AF RID: 431
		private long _initialOffset;
	}
}
