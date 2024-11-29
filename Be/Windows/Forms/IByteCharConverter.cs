using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000D5 RID: 213
	public interface IByteCharConverter
	{
		// Token: 0x060008EA RID: 2282
		char ToChar(byte b);

		// Token: 0x060008EB RID: 2283
		byte ToByte(char c);
	}
}
