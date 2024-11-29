using System;

namespace PS3SaveEditor
{
	// Token: 0x020001B0 RID: 432
	internal class BackupItem
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0006EE74 File Offset: 0x0006D074
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0006EE7C File Offset: 0x0006D07C
		public string BackupFile { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0006EE85 File Offset: 0x0006D085
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0006EE8D File Offset: 0x0006D08D
		public string Timestamp { get; set; }
	}
}
