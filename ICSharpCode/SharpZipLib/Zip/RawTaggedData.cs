using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000067 RID: 103
	public class RawTaggedData : ITaggedData
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		public RawTaggedData(short tag)
		{
			this._tag = tag;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00020EFC File Offset: 0x0001F0FC
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00020F14 File Offset: 0x0001F114
		public short TagID
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00020F20 File Offset: 0x0001F120
		public void SetData(byte[] data, int offset, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			this._data = new byte[count];
			Array.Copy(data, offset, this._data, 0, count);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00020F60 File Offset: 0x0001F160
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00020F78 File Offset: 0x0001F178
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00020F90 File Offset: 0x0001F190
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x04000360 RID: 864
		private short _tag;

		// Token: 0x04000361 RID: 865
		private byte[] _data;
	}
}
