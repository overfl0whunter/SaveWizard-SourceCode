using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000097 RID: 151
	public class ScanEventArgs : EventArgs
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0002E85A File Offset: 0x0002CA5A
		public ScanEventArgs(string name)
		{
			this.name_ = name;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0002E874 File Offset: 0x0002CA74
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0002E88C File Offset: 0x0002CA8C
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
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

		// Token: 0x04000496 RID: 1174
		private string name_;

		// Token: 0x04000497 RID: 1175
		private bool continueRunning_ = true;
	}
}
