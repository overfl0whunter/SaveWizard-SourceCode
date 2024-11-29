using System;

namespace Ionic.Zlib
{
	// Token: 0x02000019 RID: 25
	internal class WorkItem
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000B550 File Offset: 0x00009750
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x040000DB RID: 219
		public byte[] buffer;

		// Token: 0x040000DC RID: 220
		public byte[] compressed;

		// Token: 0x040000DD RID: 221
		public int crc;

		// Token: 0x040000DE RID: 222
		public int index;

		// Token: 0x040000DF RID: 223
		public int ordinal;

		// Token: 0x040000E0 RID: 224
		public int inputBytesAvailable;

		// Token: 0x040000E1 RID: 225
		public int compressedBytesAvailable;

		// Token: 0x040000E2 RID: 226
		public ZlibCodec compressor;
	}
}
