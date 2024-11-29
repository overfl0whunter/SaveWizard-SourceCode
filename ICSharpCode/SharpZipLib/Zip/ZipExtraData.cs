using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200006B RID: 107
	public sealed class ZipExtraData : IDisposable
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0002173A File Offset: 0x0001F93A
		public ZipExtraData()
		{
			this.Clear();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002174C File Offset: 0x0001F94C
		public ZipExtraData(byte[] data)
		{
			bool flag = data == null;
			if (flag)
			{
				this._data = new byte[0];
			}
			else
			{
				this._data = data;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00021784 File Offset: 0x0001F984
		public byte[] GetEntryData()
		{
			bool flag = this.Length > 65535;
			if (flag)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			return (byte[])this._data.Clone();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000217C4 File Offset: 0x0001F9C4
		public void Clear()
		{
			bool flag = this._data == null || this._data.Length != 0;
			if (flag)
			{
				this._data = new byte[0];
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000217FC File Offset: 0x0001F9FC
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00021818 File Offset: 0x0001FA18
		public Stream GetStreamForTag(int tag)
		{
			Stream stream = null;
			bool flag = this.Find(tag);
			if (flag)
			{
				stream = new MemoryStream(this._data, this._index, this._readValueLength, false);
			}
			return stream;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00021854 File Offset: 0x0001FA54
		private ITaggedData GetData(short tag)
		{
			ITaggedData taggedData = null;
			bool flag = this.Find((int)tag);
			if (flag)
			{
				taggedData = ZipExtraData.Create(tag, this._data, this._readValueStart, this._readValueLength);
			}
			return taggedData;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00021890 File Offset: 0x0001FA90
		private static ITaggedData Create(short tag, byte[] data, int offset, int count)
		{
			ITaggedData taggedData;
			if (tag != 10)
			{
				if (tag != 21589)
				{
					taggedData = new RawTaggedData(tag);
				}
				else
				{
					taggedData = new ExtendedUnixData();
				}
			}
			else
			{
				taggedData = new NTTaggedData();
			}
			taggedData.SetData(data, offset, count);
			return taggedData;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000218DC File Offset: 0x0001FADC
		public int ValueLength
		{
			get
			{
				return this._readValueLength;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000218F4 File Offset: 0x0001FAF4
		public int CurrentReadIndex
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0002190C File Offset: 0x0001FB0C
		public int UnreadCount
		{
			get
			{
				bool flag = this._readValueStart > this._data.Length || this._readValueStart < 4;
				if (flag)
				{
					throw new ZipException("Find must be called before calling a Read method");
				}
				return this._readValueStart + this._readValueLength - this._index;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00021960 File Offset: 0x0001FB60
		public bool Find(int headerID)
		{
			this._readValueStart = this._data.Length;
			this._readValueLength = 0;
			this._index = 0;
			int num = this._readValueStart;
			int num2 = headerID - 1;
			while (num2 != headerID && this._index < this._data.Length - 3)
			{
				num2 = this.ReadShortInternal();
				num = this.ReadShortInternal();
				bool flag = num2 != headerID;
				if (flag)
				{
					this._index += num;
				}
			}
			bool flag2 = num2 == headerID && this._index + num <= this._data.Length;
			bool flag3 = flag2;
			if (flag3)
			{
				this._readValueStart = this._index;
				this._readValueLength = num;
			}
			return flag2;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00021A24 File Offset: 0x0001FC24
		public void AddEntry(ITaggedData taggedData)
		{
			bool flag = taggedData == null;
			if (flag)
			{
				throw new ArgumentNullException("taggedData");
			}
			this.AddEntry((int)taggedData.TagID, taggedData.GetData());
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00021A5C File Offset: 0x0001FC5C
		public void AddEntry(int headerID, byte[] fieldData)
		{
			bool flag = headerID > 65535 || headerID < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("headerID");
			}
			int num = ((fieldData == null) ? 0 : fieldData.Length);
			bool flag2 = num > 65535;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("fieldData", "exceeds maximum length");
			}
			int num2 = this._data.Length + num + 4;
			bool flag3 = this.Find(headerID);
			if (flag3)
			{
				num2 -= this.ValueLength + 4;
			}
			bool flag4 = num2 > 65535;
			if (flag4)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			this.Delete(headerID);
			byte[] array = new byte[num2];
			this._data.CopyTo(array, 0);
			int num3 = this._data.Length;
			this._data = array;
			this.SetShort(ref num3, headerID);
			this.SetShort(ref num3, num);
			bool flag5 = fieldData != null;
			if (flag5)
			{
				fieldData.CopyTo(array, num3);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00021B4B File Offset: 0x0001FD4B
		public void StartNewEntry()
		{
			this._newEntry = new MemoryStream();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00021B5C File Offset: 0x0001FD5C
		public void AddNewEntry(int headerID)
		{
			byte[] array = this._newEntry.ToArray();
			this._newEntry = null;
			this.AddEntry(headerID, array);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00021B86 File Offset: 0x0001FD86
		public void AddData(byte data)
		{
			this._newEntry.WriteByte(data);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00021B98 File Offset: 0x0001FD98
		public void AddData(byte[] data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			this._newEntry.Write(data, 0, data.Length);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00021BCB File Offset: 0x0001FDCB
		public void AddLeShort(int toAdd)
		{
			this._newEntry.WriteByte((byte)toAdd);
			this._newEntry.WriteByte((byte)(toAdd >> 8));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00021BEE File Offset: 0x0001FDEE
		public void AddLeInt(int toAdd)
		{
			this.AddLeShort((int)((short)toAdd));
			this.AddLeShort((int)((short)(toAdd >> 16)));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00021C08 File Offset: 0x0001FE08
		public void AddLeLong(long toAdd)
		{
			this.AddLeInt((int)(toAdd & (long)((ulong)(-1))));
			this.AddLeInt((int)(toAdd >> 32));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00021C28 File Offset: 0x0001FE28
		public bool Delete(int headerID)
		{
			bool flag = false;
			bool flag2 = this.Find(headerID);
			if (flag2)
			{
				flag = true;
				int num = this._readValueStart - 4;
				byte[] array = new byte[this._data.Length - (this.ValueLength + 4)];
				Array.Copy(this._data, 0, array, 0, num);
				int num2 = num + this.ValueLength + 4;
				Array.Copy(this._data, num2, array, num, this._data.Length - num2);
				this._data = array;
			}
			return flag;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public long ReadLong()
		{
			this.ReadCheck(8);
			return ((long)this.ReadInt() & (long)((ulong)(-1))) | ((long)this.ReadInt() << 32);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00021CDC File Offset: 0x0001FEDC
		public int ReadInt()
		{
			this.ReadCheck(4);
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8) + ((int)this._data[this._index + 2] << 16) + ((int)this._data[this._index + 3] << 24);
			this._index += 4;
			return num;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public int ReadShort()
		{
			this.ReadCheck(2);
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return num;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00021D98 File Offset: 0x0001FF98
		public int ReadByte()
		{
			int num = -1;
			bool flag = this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index;
			if (flag)
			{
				num = (int)this._data[this._index];
				this._index++;
			}
			return num;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00021DF7 File Offset: 0x0001FFF7
		public void Skip(int amount)
		{
			this.ReadCheck(amount);
			this._index += amount;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00021E10 File Offset: 0x00020010
		private void ReadCheck(int length)
		{
			bool flag = this._readValueStart > this._data.Length || this._readValueStart < 4;
			if (flag)
			{
				throw new ZipException("Find must be called before calling a Read method");
			}
			bool flag2 = this._index > this._readValueStart + this._readValueLength - length;
			if (flag2)
			{
				throw new ZipException("End of extra data");
			}
			bool flag3 = this._index + length < 4;
			if (flag3)
			{
				throw new ZipException("Cannot read before start of tag");
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00021E8C File Offset: 0x0002008C
		private int ReadShortInternal()
		{
			bool flag = this._index > this._data.Length - 2;
			if (flag)
			{
				throw new ZipException("End of extra data");
			}
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return num;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00021EEF File Offset: 0x000200EF
		private void SetShort(ref int index, int source)
		{
			this._data[index] = (byte)source;
			this._data[index + 1] = (byte)(source >> 8);
			index += 2;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00021F14 File Offset: 0x00020114
		public void Dispose()
		{
			bool flag = this._newEntry != null;
			if (flag)
			{
				this._newEntry.Close();
			}
		}

		// Token: 0x04000369 RID: 873
		private int _index;

		// Token: 0x0400036A RID: 874
		private int _readValueStart;

		// Token: 0x0400036B RID: 875
		private int _readValueLength;

		// Token: 0x0400036C RID: 876
		private MemoryStream _newEntry;

		// Token: 0x0400036D RID: 877
		private byte[] _data;
	}
}
