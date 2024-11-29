using System;
using System.Collections;

namespace Rss
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public class ExceptionCollection : CollectionBase
	{
		// Token: 0x170001D2 RID: 466
		public Exception this[int index]
		{
			get
			{
				return (Exception)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003000C File Offset: 0x0002E20C
		public int Add(Exception exception)
		{
			foreach (object obj in base.List)
			{
				Exception ex = (Exception)obj;
				bool flag = ex.Message == exception.Message;
				if (flag)
				{
					return -1;
				}
			}
			this.lastException = exception;
			return base.List.Add(exception);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00030094 File Offset: 0x0002E294
		public bool Contains(Exception exception)
		{
			return base.List.Contains(exception);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000300B2 File Offset: 0x0002E2B2
		public void CopyTo(Exception[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000300C4 File Offset: 0x0002E2C4
		public int IndexOf(Exception exception)
		{
			return base.List.IndexOf(exception);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000300E2 File Offset: 0x0002E2E2
		public void Insert(int index, Exception exception)
		{
			base.List.Insert(index, exception);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public void Remove(Exception exception)
		{
			base.List.Remove(exception);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00030104 File Offset: 0x0002E304
		public Exception LastException
		{
			get
			{
				return this.lastException;
			}
		}

		// Token: 0x040004B9 RID: 1209
		private Exception lastException = null;
	}
}
