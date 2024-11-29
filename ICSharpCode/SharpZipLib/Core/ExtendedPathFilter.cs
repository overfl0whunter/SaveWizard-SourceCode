using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A6 RID: 166
	public class ExtendedPathFilter : PathFilter
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0002F2FB File Offset: 0x0002D4FB
		public ExtendedPathFilter(string filter, long minSize, long maxSize)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002F33B File Offset: 0x0002D53B
		public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
			: base(filter)
		{
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002F37C File Offset: 0x0002D57C
		public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			bool flag2 = flag;
			if (flag2)
			{
				FileInfo fileInfo = new FileInfo(name);
				flag = this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime;
			}
			return flag;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0002F44C File Offset: 0x0002D64C
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0002F464 File Offset: 0x0002D664
		public long MinSize
		{
			get
			{
				return this.minSize_;
			}
			set
			{
				bool flag = value < 0L || this.maxSize_ < value;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.minSize_ = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0002F49C File Offset: 0x0002D69C
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0002F4B4 File Offset: 0x0002D6B4
		public long MaxSize
		{
			get
			{
				return this.maxSize_;
			}
			set
			{
				bool flag = value < 0L || this.minSize_ > value;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxSize_ = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0002F504 File Offset: 0x0002D704
		public DateTime MinDate
		{
			get
			{
				return this.minDate_;
			}
			set
			{
				bool flag = value > this.maxDate_;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
				}
				this.minDate_ = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0002F53C File Offset: 0x0002D73C
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0002F554 File Offset: 0x0002D754
		public DateTime MaxDate
		{
			get
			{
				return this.maxDate_;
			}
			set
			{
				bool flag = this.minDate_ > value;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
				}
				this.maxDate_ = value;
			}
		}

		// Token: 0x040004AC RID: 1196
		private long minSize_;

		// Token: 0x040004AD RID: 1197
		private long maxSize_ = long.MaxValue;

		// Token: 0x040004AE RID: 1198
		private DateTime minDate_ = DateTime.MinValue;

		// Token: 0x040004AF RID: 1199
		private DateTime maxDate_ = DateTime.MaxValue;
	}
}
