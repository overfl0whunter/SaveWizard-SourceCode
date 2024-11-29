using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000D6 RID: 214
	public class DefaultByteCharConverter : IByteCharConverter
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00035D2C File Offset: 0x00033F2C
		public char ToChar(byte b)
		{
			return (char)((b > 31 && (b <= 126 || b >= 160)) ? b : 46);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00035D58 File Offset: 0x00033F58
		public byte ToByte(char c)
		{
			return (byte)c;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00035D6C File Offset: 0x00033F6C
		public override string ToString()
		{
			return "Default";
		}
	}
}
