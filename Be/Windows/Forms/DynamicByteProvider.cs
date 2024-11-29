using System;
using System.Diagnostics;

namespace Be.Windows.Forms
{
	// Token: 0x020000DC RID: 220
	public class DynamicByteProvider : IByteProvider
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x000364C4 File Offset: 0x000346C4
		public DynamicByteProvider(byte[] data)
			: this(new ByteCollection(data))
		{
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000364D4 File Offset: 0x000346D4
		public DynamicByteProvider(ByteCollection bytes)
		{
			this._bytes = bytes;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000364E8 File Offset: 0x000346E8
		private void OnChanged(ByteProviderChanged e)
		{
			this._hasChanges = true;
			bool flag = this.Changed != null;
			if (flag)
			{
				this.Changed(this, e);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00036518 File Offset: 0x00034718
		private void OnLengthChanged(EventArgs e)
		{
			bool flag = this.LengthChanged != null;
			if (flag)
			{
				this.LengthChanged(this, e);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00036544 File Offset: 0x00034744
		public ByteCollection Bytes
		{
			get
			{
				return this._bytes;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003655C File Offset: 0x0003475C
		public bool HasChanges()
		{
			return this._hasChanges;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00036574 File Offset: 0x00034774
		public void ApplyChanges()
		{
			this._hasChanges = false;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600092A RID: 2346 RVA: 0x00036580 File Offset: 0x00034780
		// (remove) Token: 0x0600092B RID: 2347 RVA: 0x000365B8 File Offset: 0x000347B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ByteProviderChanged> Changed;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600092C RID: 2348 RVA: 0x000365F0 File Offset: 0x000347F0
		// (remove) Token: 0x0600092D RID: 2349 RVA: 0x00036628 File Offset: 0x00034828
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LengthChanged;

		// Token: 0x0600092E RID: 2350 RVA: 0x00036660 File Offset: 0x00034860
		public byte ReadByte(long index)
		{
			return this._bytes[(int)index];
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00036680 File Offset: 0x00034880
		public void WriteByte(long index, byte value, bool noEvt = false)
		{
			byte b = this._bytes[(int)index];
			this._bytes[(int)index] = value;
			bool flag = !noEvt;
			if (flag)
			{
				this.OnChanged(new ByteProviderChanged
				{
					Index = index,
					OldValue = b,
					NewValue = value,
					ChangeType = ChangeType.Insert
				});
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000366E0 File Offset: 0x000348E0
		public void DeleteBytes(long index, long length)
		{
			int num = (int)Math.Max(0L, index);
			int num2 = (int)Math.Min((long)((int)this.Length), length);
			this._bytes.RemoveRange(num, num2);
			this.OnLengthChanged(EventArgs.Empty);
			this.OnChanged(new ByteProviderChanged
			{
				Index = index,
				ChangeType = ChangeType.Delete
			});
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003673E File Offset: 0x0003493E
		public void InsertBytes(long index, byte[] bs)
		{
			this._bytes.InsertRange((int)index, bs);
			this.OnLengthChanged(EventArgs.Empty);
			this.OnChanged(new ByteProviderChanged
			{
				Index = index,
				ChangeType = ChangeType.Insert
			});
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00036778 File Offset: 0x00034978
		public long Length
		{
			get
			{
				return (long)this._bytes.Count;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00036798 File Offset: 0x00034998
		public bool SupportsWriteByte()
		{
			return true;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000367AC File Offset: 0x000349AC
		public bool SupportsInsertBytes()
		{
			return false;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000367C0 File Offset: 0x000349C0
		public bool SupportsDeleteBytes()
		{
			return false;
		}

		// Token: 0x0400054E RID: 1358
		private bool _hasChanges;

		// Token: 0x0400054F RID: 1359
		private ByteCollection _bytes;
	}
}
