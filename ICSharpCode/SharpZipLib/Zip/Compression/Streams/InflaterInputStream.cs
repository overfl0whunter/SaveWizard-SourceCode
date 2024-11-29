using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200008D RID: 141
	public class InflaterInputStream : Stream
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0002D15C File Offset: 0x0002B35C
		public InflaterInputStream(Stream baseInputStream)
			: this(baseInputStream, new Inflater(), 4096)
		{
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0002D171 File Offset: 0x0002B371
		public InflaterInputStream(Stream baseInputStream, Inflater inf)
			: this(baseInputStream, inf, 4096)
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0002D184 File Offset: 0x0002B384
		public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
		{
			bool flag = baseInputStream == null;
			if (flag)
			{
				throw new ArgumentNullException("baseInputStream");
			}
			bool flag2 = inflater == null;
			if (flag2)
			{
				throw new ArgumentNullException("inflater");
			}
			bool flag3 = bufferSize <= 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseInputStream = baseInputStream;
			this.inf = inflater;
			this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0002D1FC File Offset: 0x0002B3FC
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0002D214 File Offset: 0x0002B414
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0002D220 File Offset: 0x0002B420
		public long Skip(long count)
		{
			bool flag = count <= 0L;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool canSeek = this.baseInputStream.CanSeek;
			long num;
			if (canSeek)
			{
				this.baseInputStream.Seek(count, SeekOrigin.Current);
				num = count;
			}
			else
			{
				int num2 = 2048;
				bool flag2 = count < (long)num2;
				if (flag2)
				{
					num2 = (int)count;
				}
				byte[] array = new byte[num2];
				int num3 = 1;
				long num4 = count;
				while (num4 > 0L && num3 > 0)
				{
					bool flag3 = num4 < (long)num2;
					if (flag3)
					{
						num2 = (int)num4;
					}
					num3 = this.baseInputStream.Read(array, 0, num2);
					num4 -= (long)num3;
				}
				num = count - num4;
			}
			return num;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
		protected void StopDecrypting()
		{
			this.inputBuffer.CryptoTransform = null;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public virtual int Available
		{
			get
			{
				return this.inf.IsFinished ? 0 : 1;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0002D30C File Offset: 0x0002B50C
		protected void Fill()
		{
			bool flag = this.inputBuffer.Available <= 0;
			if (flag)
			{
				this.inputBuffer.Fill();
				bool flag2 = this.inputBuffer.Available <= 0;
				if (flag2)
				{
					throw new SharpZipBaseException("Unexpected EOF");
				}
			}
			this.inputBuffer.SetInflaterInput(this.inf);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0002D370 File Offset: 0x0002B570
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0002D390 File Offset: 0x0002B590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0002D3B8 File Offset: 0x0002B5B8
		public override long Length
		{
			get
			{
				return (long)this.inputBuffer.RawLength;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0002D3D8 File Offset: 0x0002B5D8
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0002D3F5 File Offset: 0x0002B5F5
		public override long Position
		{
			get
			{
				return this.baseInputStream.Position;
			}
			set
			{
				throw new NotSupportedException("InflaterInputStream Position not supported");
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0002D402 File Offset: 0x0002B602
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002D411 File Offset: 0x0002B611
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0002D41E File Offset: 0x0002B61E
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0002D42B File Offset: 0x0002B62B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002D438 File Offset: 0x0002B638
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0002D445 File Offset: 0x0002B645
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0002D454 File Offset: 0x0002B654
		public override void Close()
		{
			bool flag = !this.isClosed;
			if (flag)
			{
				this.isClosed = true;
				bool flag2 = this.isStreamOwner;
				if (flag2)
				{
					this.baseInputStream.Close();
				}
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0002D490 File Offset: 0x0002B690
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool isNeedingDictionary = this.inf.IsNeedingDictionary;
			if (isNeedingDictionary)
			{
				throw new SharpZipBaseException("Need a dictionary");
			}
			int num = count;
			for (;;)
			{
				int num2 = this.inf.Inflate(buffer, offset, num);
				offset += num2;
				num -= num2;
				bool flag = num == 0 || this.inf.IsFinished;
				if (flag)
				{
					break;
				}
				bool isNeedingInput = this.inf.IsNeedingInput;
				if (isNeedingInput)
				{
					this.Fill();
				}
				else
				{
					bool flag2 = num2 == 0;
					if (flag2)
					{
						goto Block_5;
					}
				}
			}
			return count - num;
			Block_5:
			throw new ZipException("Dont know what to do");
		}

		// Token: 0x04000470 RID: 1136
		protected Inflater inf;

		// Token: 0x04000471 RID: 1137
		protected InflaterInputBuffer inputBuffer;

		// Token: 0x04000472 RID: 1138
		private Stream baseInputStream;

		// Token: 0x04000473 RID: 1139
		protected long csize;

		// Token: 0x04000474 RID: 1140
		private bool isClosed;

		// Token: 0x04000475 RID: 1141
		private bool isStreamOwner = true;
	}
}
