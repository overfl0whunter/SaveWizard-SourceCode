using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007B RID: 123
	public class DescriptorData
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00026150 File Offset: 0x00024350
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00026168 File Offset: 0x00024368
		public long CompressedSize
		{
			get
			{
				return this.compressedSize;
			}
			set
			{
				this.compressedSize = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00026174 File Offset: 0x00024374
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0002618C File Offset: 0x0002438C
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00026198 File Offset: 0x00024398
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000261B0 File Offset: 0x000243B0
		public long Crc
		{
			get
			{
				return this.crc;
			}
			set
			{
				this.crc = value & (long)((ulong)(-1));
			}
		}

		// Token: 0x040003A2 RID: 930
		private long size;

		// Token: 0x040003A3 RID: 931
		private long compressedSize;

		// Token: 0x040003A4 RID: 932
		private long crc;
	}
}
