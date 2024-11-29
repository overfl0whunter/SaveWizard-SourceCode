using System;

namespace PS3SaveEditor
{
	// Token: 0x020001D7 RID: 471
	public class DownloadFinishEventArgs : EventArgs
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000901A4 File Offset: 0x0008E3A4
		public bool Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000901BC File Offset: 0x0008E3BC
		public string Error
		{
			get
			{
				return this.m_error;
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000901D4 File Offset: 0x0008E3D4
		public DownloadFinishEventArgs(bool status, string error)
		{
			this.m_status = status;
			this.m_error = error;
		}

		// Token: 0x04000BFA RID: 3066
		private bool m_status;

		// Token: 0x04000BFB RID: 3067
		private string m_error;
	}
}
