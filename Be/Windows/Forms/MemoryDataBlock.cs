using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000E5 RID: 229
	internal sealed class MemoryDataBlock : DataBlock
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0003C7BA File Offset: 0x0003A9BA
		public MemoryDataBlock(byte data)
		{
			this._data = new byte[] { data };
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0003C7D4 File Offset: 0x0003A9D4
		public MemoryDataBlock(byte[] data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			this._data = (byte[])data.Clone();
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0003C810 File Offset: 0x0003AA10
		public override long Length
		{
			get
			{
				return (long)this._data.Length;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0003C82C File Offset: 0x0003AA2C
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0003C844 File Offset: 0x0003AA44
		public void AddByteToEnd(byte value)
		{
			byte[] array = new byte[(long)this._data.Length + 1L];
			this._data.CopyTo(array, 0);
			checked
			{
				array[(int)((IntPtr)(unchecked((long)array.Length - 1L)))] = value;
				this._data = array;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0003C884 File Offset: 0x0003AA84
		public void AddByteToStart(byte value)
		{
			byte[] array = new byte[(long)this._data.Length + 1L];
			array[0] = value;
			this._data.CopyTo(array, 1);
			this._data = array;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0003C8C0 File Offset: 0x0003AAC0
		public void InsertBytes(long position, byte[] data)
		{
			byte[] array = new byte[(long)this._data.Length + (long)data.Length];
			bool flag = position > 0L;
			if (flag)
			{
				Array.Copy(this._data, 0L, array, 0L, position);
			}
			Array.Copy(data, 0L, array, position, (long)data.Length);
			bool flag2 = position < (long)this._data.Length;
			if (flag2)
			{
				Array.Copy(this._data, position, array, position + (long)data.Length, (long)this._data.Length - position);
			}
			this._data = array;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003C944 File Offset: 0x0003AB44
		public override void RemoveBytes(long position, long count)
		{
			byte[] array = new byte[(long)this._data.Length - count];
			bool flag = position > 0L;
			if (flag)
			{
				Array.Copy(this._data, 0L, array, 0L, position);
			}
			bool flag2 = position + count < (long)this._data.Length;
			if (flag2)
			{
				Array.Copy(this._data, position + count, array, position, (long)array.Length - position);
			}
			this._data = array;
		}

		// Token: 0x040005C3 RID: 1475
		private byte[] _data;
	}
}
