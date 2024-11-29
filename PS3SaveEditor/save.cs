using System;

namespace PS3SaveEditor
{
	// Token: 0x020001BB RID: 443
	public class save
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0006F82D File Offset: 0x0006DA2D
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x0006F835 File Offset: 0x0006DA35
		public string id { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0006F83E File Offset: 0x0006DA3E
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x0006F846 File Offset: 0x0006DA46
		public string gamecode { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0006F84F File Offset: 0x0006DA4F
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x0006F857 File Offset: 0x0006DA57
		public string title { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0006F860 File Offset: 0x0006DA60
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x0006F868 File Offset: 0x0006DA68
		public string description { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0006F871 File Offset: 0x0006DA71
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0006F879 File Offset: 0x0006DA79
		public string note { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0006F882 File Offset: 0x0006DA82
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0006F88A File Offset: 0x0006DA8A
		public string folder { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0006F893 File Offset: 0x0006DA93
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x0006F89B File Offset: 0x0006DA9B
		public string region { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0006F8A4 File Offset: 0x0006DAA4
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0006F8AC File Offset: 0x0006DAAC
		public long updated { get; set; }

		// Token: 0x0600168A RID: 5770 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
		internal static save Copy(save save)
		{
			return new save
			{
				folder = save.folder,
				region = save.region,
				updated = save.updated,
				description = save.description,
				gamecode = save.gamecode,
				note = save.note,
				title = save.title,
				id = save.id
			};
		}
	}
}
