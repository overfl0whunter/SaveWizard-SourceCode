using System;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000050 RID: 80
	public class ReadOptions
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001C2A9 File Offset: 0x0001A4A9
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001C2B1 File Offset: 0x0001A4B1
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001C2BA File Offset: 0x0001A4BA
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		public TextWriter StatusMessageWriter { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001C2CB File Offset: 0x0001A4CB
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001C2D3 File Offset: 0x0001A4D3
		public Encoding Encoding { get; set; }
	}
}
