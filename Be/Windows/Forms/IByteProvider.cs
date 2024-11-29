using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000E2 RID: 226
	public interface IByteProvider
	{
		// Token: 0x06000A45 RID: 2629
		byte ReadByte(long index);

		// Token: 0x06000A46 RID: 2630
		void WriteByte(long index, byte value, bool noEvt = false);

		// Token: 0x06000A47 RID: 2631
		void InsertBytes(long index, byte[] bs);

		// Token: 0x06000A48 RID: 2632
		void DeleteBytes(long index, long length);

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A49 RID: 2633
		long Length { get; }

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000A4A RID: 2634
		// (remove) Token: 0x06000A4B RID: 2635
		event EventHandler LengthChanged;

		// Token: 0x06000A4C RID: 2636
		bool HasChanges();

		// Token: 0x06000A4D RID: 2637
		void ApplyChanges();

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000A4E RID: 2638
		// (remove) Token: 0x06000A4F RID: 2639
		event EventHandler<ByteProviderChanged> Changed;

		// Token: 0x06000A50 RID: 2640
		bool SupportsWriteByte();

		// Token: 0x06000A51 RID: 2641
		bool SupportsInsertBytes();

		// Token: 0x06000A52 RID: 2642
		bool SupportsDeleteBytes();
	}
}
