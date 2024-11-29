using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000069 RID: 105
	public class NTTaggedData : ITaggedData
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00021448 File Offset: 0x0001F648
		public short TagID
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002145C File Offset: 0x0001F65C
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.ReadLEInt();
					while (zipHelperStream.Position < zipHelperStream.Length)
					{
						int num = zipHelperStream.ReadLEShort();
						int num2 = zipHelperStream.ReadLEShort();
						bool flag = num == 1;
						if (flag)
						{
							bool flag2 = num2 >= 24;
							if (flag2)
							{
								long num3 = zipHelperStream.ReadLELong();
								this._lastModificationTime = DateTime.FromFileTime(num3);
								long num4 = zipHelperStream.ReadLELong();
								this._lastAccessTime = DateTime.FromFileTime(num4);
								long num5 = zipHelperStream.ReadLELong();
								this._createTime = DateTime.FromFileTime(num5);
							}
							break;
						}
						zipHelperStream.Seek((long)num2, SeekOrigin.Current);
					}
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002154C File Offset: 0x0001F74C
		public byte[] GetData()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteLEInt(0);
					zipHelperStream.WriteLEShort(1);
					zipHelperStream.WriteLEShort(24);
					zipHelperStream.WriteLELong(this._lastModificationTime.ToFileTime());
					zipHelperStream.WriteLELong(this._lastAccessTime.ToFileTime());
					zipHelperStream.WriteLELong(this._createTime.ToFileTime());
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000215FC File Offset: 0x0001F7FC
		public static bool IsValidValue(DateTime value)
		{
			bool flag = true;
			try
			{
				value.ToFileTimeUtc();
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00021634 File Offset: 0x0001F834
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0002164C File Offset: 0x0001F84C
		public DateTime LastModificationTime
		{
			get
			{
				return this._lastModificationTime;
			}
			set
			{
				bool flag = !NTTaggedData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastModificationTime = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0002167C File Offset: 0x0001F87C
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00021694 File Offset: 0x0001F894
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				bool flag = !NTTaggedData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._createTime = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000216C4 File Offset: 0x0001F8C4
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000216DC File Offset: 0x0001F8DC
		public DateTime LastAccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				bool flag = !NTTaggedData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastAccessTime = value;
			}
		}

		// Token: 0x04000366 RID: 870
		private DateTime _lastAccessTime = DateTime.FromFileTime(0L);

		// Token: 0x04000367 RID: 871
		private DateTime _lastModificationTime = DateTime.FromFileTime(0L);

		// Token: 0x04000368 RID: 872
		private DateTime _createTime = DateTime.FromFileTime(0L);
	}
}
