using System;

namespace Rss
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class RssCloud : RssElement
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00030C08 File Offset: 0x0002EE08
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00030C20 File Offset: 0x0002EE20
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = RssDefault.Check(value);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00030C30 File Offset: 0x0002EE30
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00030C48 File Offset: 0x0002EE48
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = RssDefault.Check(value);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00030C58 File Offset: 0x0002EE58
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00030C70 File Offset: 0x0002EE70
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = RssDefault.Check(value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00030C80 File Offset: 0x0002EE80
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00030C98 File Offset: 0x0002EE98
		public string RegisterProcedure
		{
			get
			{
				return this.registerProcedure;
			}
			set
			{
				this.registerProcedure = RssDefault.Check(value);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00030CA8 File Offset: 0x0002EEA8
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00030CC0 File Offset: 0x0002EEC0
		public RssCloudProtocol Protocol
		{
			get
			{
				return this.protocol;
			}
			set
			{
				this.protocol = value;
			}
		}

		// Token: 0x040004D2 RID: 1234
		private RssCloudProtocol protocol = RssCloudProtocol.Empty;

		// Token: 0x040004D3 RID: 1235
		private string domain = "";

		// Token: 0x040004D4 RID: 1236
		private string path = "";

		// Token: 0x040004D5 RID: 1237
		private string registerProcedure = "";

		// Token: 0x040004D6 RID: 1238
		private int port = -1;
	}
}
