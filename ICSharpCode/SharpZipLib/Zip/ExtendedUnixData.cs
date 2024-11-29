using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000068 RID: 104
	public class ExtendedUnixData : ITaggedData
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00020F9C File Offset: 0x0001F19C
		public short TagID
		{
			get
			{
				return 21589;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					this._flags = (ExtendedUnixData.Flags)zipHelperStream.ReadByte();
					bool flag = (this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0 && count >= 5;
					if (flag)
					{
						int num = zipHelperStream.ReadLEInt();
						this._modificationTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, num, 0)).ToLocalTime();
					}
					bool flag2 = (this._flags & ExtendedUnixData.Flags.AccessTime) > (ExtendedUnixData.Flags)0;
					if (flag2)
					{
						int num2 = zipHelperStream.ReadLEInt();
						this._lastAccessTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, num2, 0)).ToLocalTime();
					}
					bool flag3 = (this._flags & ExtendedUnixData.Flags.CreateTime) > (ExtendedUnixData.Flags)0;
					if (flag3)
					{
						int num3 = zipHelperStream.ReadLEInt();
						this._createTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, num3, 0)).ToLocalTime();
					}
				}
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0002112C File Offset: 0x0001F32C
		public byte[] GetData()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteByte((byte)this._flags);
					bool flag = (this._flags & ExtendedUnixData.Flags.ModificationTime) > (ExtendedUnixData.Flags)0;
					if (flag)
					{
						int num = (int)(this._modificationTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
						zipHelperStream.WriteLEInt(num);
					}
					bool flag2 = (this._flags & ExtendedUnixData.Flags.AccessTime) > (ExtendedUnixData.Flags)0;
					if (flag2)
					{
						int num2 = (int)(this._lastAccessTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
						zipHelperStream.WriteLEInt(num2);
					}
					bool flag3 = (this._flags & ExtendedUnixData.Flags.CreateTime) > (ExtendedUnixData.Flags)0;
					if (flag3)
					{
						int num3 = (int)(this._createTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
						zipHelperStream.WriteLEInt(num3);
					}
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000212A0 File Offset: 0x0001F4A0
		public static bool IsValidValue(DateTime value)
		{
			return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000212E8 File Offset: 0x0001F4E8
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00021300 File Offset: 0x0001F500
		public DateTime ModificationTime
		{
			get
			{
				return this._modificationTime;
			}
			set
			{
				bool flag = !ExtendedUnixData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.ModificationTime;
				this._modificationTime = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0002133C File Offset: 0x0001F53C
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00021354 File Offset: 0x0001F554
		public DateTime AccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				bool flag = !ExtendedUnixData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.AccessTime;
				this._lastAccessTime = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00021390 File Offset: 0x0001F590
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x000213A8 File Offset: 0x0001F5A8
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				bool flag = !ExtendedUnixData.IsValidValue(value);
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.CreateTime;
				this._createTime = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x000213E4 File Offset: 0x0001F5E4
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x000213FC File Offset: 0x0001F5FC
		private ExtendedUnixData.Flags Include
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x04000362 RID: 866
		private ExtendedUnixData.Flags _flags;

		// Token: 0x04000363 RID: 867
		private DateTime _modificationTime = new DateTime(1970, 1, 1);

		// Token: 0x04000364 RID: 868
		private DateTime _lastAccessTime = new DateTime(1970, 1, 1);

		// Token: 0x04000365 RID: 869
		private DateTime _createTime = new DateTime(1970, 1, 1);

		// Token: 0x0200020C RID: 524
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04000DBB RID: 3515
			ModificationTime = 1,
			// Token: 0x04000DBC RID: 3516
			AccessTime = 2,
			// Token: 0x04000DBD RID: 3517
			CreateTime = 4
		}
	}
}
