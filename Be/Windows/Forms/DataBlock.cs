using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000DA RID: 218
	internal abstract class DataBlock
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000906 RID: 2310
		public abstract long Length { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00035FE4 File Offset: 0x000341E4
		public DataMap Map
		{
			get
			{
				return this._map;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00035FFC File Offset: 0x000341FC
		public DataBlock NextBlock
		{
			get
			{
				return this._nextBlock;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00036014 File Offset: 0x00034214
		public DataBlock PreviousBlock
		{
			get
			{
				return this._previousBlock;
			}
		}

		// Token: 0x0600090A RID: 2314
		public abstract void RemoveBytes(long position, long count);

		// Token: 0x04000547 RID: 1351
		internal DataMap _map;

		// Token: 0x04000548 RID: 1352
		internal DataBlock _nextBlock;

		// Token: 0x04000549 RID: 1353
		internal DataBlock _previousBlock;
	}
}
