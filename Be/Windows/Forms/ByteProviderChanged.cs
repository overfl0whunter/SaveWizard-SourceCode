using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000E4 RID: 228
	public class ByteProviderChanged : EventArgs
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0003C76D File Offset: 0x0003A96D
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0003C775 File Offset: 0x0003A975
		public long Index { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0003C77E File Offset: 0x0003A97E
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0003C786 File Offset: 0x0003A986
		public byte OldValue { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0003C78F File Offset: 0x0003A98F
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0003C797 File Offset: 0x0003A997
		public byte NewValue { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
		public ChangeType ChangeType { get; set; }
	}
}
