using System;

namespace PS3SaveEditor
{
	// Token: 0x020001AE RID: 430
	internal class ActionItem
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0006E566 File Offset: 0x0006C766
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0006E56E File Offset: 0x0006C76E
		public long Location { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0006E577 File Offset: 0x0006C777
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0006E57F File Offset: 0x0006C77F
		public byte Value { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0006E588 File Offset: 0x0006C788
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0006E590 File Offset: 0x0006C790
		public byte NewValue { get; set; }
	}
}
