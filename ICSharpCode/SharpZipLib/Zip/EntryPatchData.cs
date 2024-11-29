using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007C RID: 124
	internal class EntryPatchData
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000261C0 File Offset: 0x000243C0
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x000261D8 File Offset: 0x000243D8
		public long SizePatchOffset
		{
			get
			{
				return this.sizePatchOffset_;
			}
			set
			{
				this.sizePatchOffset_ = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000261E4 File Offset: 0x000243E4
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000261FC File Offset: 0x000243FC
		public long CrcPatchOffset
		{
			get
			{
				return this.crcPatchOffset_;
			}
			set
			{
				this.crcPatchOffset_ = value;
			}
		}

		// Token: 0x040003A5 RID: 933
		private long sizePatchOffset_;

		// Token: 0x040003A6 RID: 934
		private long crcPatchOffset_;
	}
}
