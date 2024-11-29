using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A7 RID: 167
	[Obsolete("Use ExtendedPathFilter instead")]
	public class NameAndSizeFilter : PathFilter
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x0002F58A File Offset: 0x0002D78A
		public NameAndSizeFilter(string filter, long minSize, long maxSize)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			bool flag2 = flag;
			if (flag2)
			{
				FileInfo fileInfo = new FileInfo(name);
				long length = fileInfo.Length;
				flag = this.MinSize <= length && this.MaxSize >= length;
			}
			return flag;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0002F600 File Offset: 0x0002D800
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0002F618 File Offset: 0x0002D818
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0002F650 File Offset: 0x0002D850
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0002F668 File Offset: 0x0002D868
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

		// Token: 0x040004B0 RID: 1200
		private long minSize_;

		// Token: 0x040004B1 RID: 1201
		private long maxSize_ = long.MaxValue;
	}
}
