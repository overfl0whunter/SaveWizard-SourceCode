using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000DF RID: 223
	internal sealed class FileDataBlock : DataBlock
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0003791E File Offset: 0x00035B1E
		public FileDataBlock(long fileOffset, long length)
		{
			this._fileOffset = fileOffset;
			this._length = length;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00037938 File Offset: 0x00035B38
		public long FileOffset
		{
			get
			{
				return this._fileOffset;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00037950 File Offset: 0x00035B50
		public override long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00037968 File Offset: 0x00035B68
		public void SetFileOffset(long value)
		{
			this._fileOffset = value;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00037974 File Offset: 0x00035B74
		public void RemoveBytesFromEnd(long count)
		{
			bool flag = count > this._length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._length -= count;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000379AC File Offset: 0x00035BAC
		public void RemoveBytesFromStart(long count)
		{
			bool flag = count > this._length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._fileOffset += count;
			this._length -= count;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000379F0 File Offset: 0x00035BF0
		public override void RemoveBytes(long position, long count)
		{
			bool flag = position > this._length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			bool flag2 = position + count > this._length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			long fileOffset = this._fileOffset;
			long num = this._length - count - position;
			long num2 = this._fileOffset + position + count;
			bool flag3 = position > 0L && num > 0L;
			if (flag3)
			{
				this._fileOffset = fileOffset;
				this._length = position;
				this._map.AddAfter(this, new FileDataBlock(num2, num));
			}
			else
			{
				bool flag4 = position > 0L;
				if (flag4)
				{
					this._fileOffset = fileOffset;
					this._length = position;
				}
				else
				{
					this._fileOffset = num2;
					this._length = num;
				}
			}
		}

		// Token: 0x04000560 RID: 1376
		private long _length;

		// Token: 0x04000561 RID: 1377
		private long _fileOffset;
	}
}
