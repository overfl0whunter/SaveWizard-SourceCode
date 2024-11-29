using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000098 RID: 152
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0002E8AE File Offset: 0x0002CAAE
		public ProgressEventArgs(string name, long processed, long target)
		{
			this.name_ = name;
			this.processed_ = processed;
			this.target_ = target;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0002E904 File Offset: 0x0002CB04
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0002E910 File Offset: 0x0002CB10
		public float PercentComplete
		{
			get
			{
				bool flag = this.target_ <= 0L;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = (float)this.processed_ / (float)this.target_ * 100f;
				}
				return num;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0002E958 File Offset: 0x0002CB58
		public long Processed
		{
			get
			{
				return this.processed_;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0002E970 File Offset: 0x0002CB70
		public long Target
		{
			get
			{
				return this.target_;
			}
		}

		// Token: 0x04000498 RID: 1176
		private string name_;

		// Token: 0x04000499 RID: 1177
		private long processed_;

		// Token: 0x0400049A RID: 1178
		private long target_;

		// Token: 0x0400049B RID: 1179
		private bool continueRunning_ = true;
	}
}
