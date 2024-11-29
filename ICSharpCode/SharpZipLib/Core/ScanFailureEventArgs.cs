using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200009A RID: 154
	public class ScanFailureEventArgs : EventArgs
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
		public ScanFailureEventArgs(string name, Exception e)
		{
			this.name_ = name;
			this.exception_ = e;
			this.continueRunning_ = true;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0002E9EC File Offset: 0x0002CBEC
		public Exception Exception
		{
			get
			{
				return this.exception_;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0002EA04 File Offset: 0x0002CC04
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0002EA1C File Offset: 0x0002CC1C
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

		// Token: 0x0400049D RID: 1181
		private string name_;

		// Token: 0x0400049E RID: 1182
		private Exception exception_;

		// Token: 0x0400049F RID: 1183
		private bool continueRunning_;
	}
}
