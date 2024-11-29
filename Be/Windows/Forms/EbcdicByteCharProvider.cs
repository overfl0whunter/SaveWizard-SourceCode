using System;
using System.Text;

namespace Be.Windows.Forms
{
	// Token: 0x020000D7 RID: 215
	public class EbcdicByteCharProvider : IByteCharConverter
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x00035D84 File Offset: 0x00033F84
		public char ToChar(byte b)
		{
			string @string = this._ebcdicEncoding.GetString(new byte[] { b });
			return (@string.Length > 0) ? @string[0] : '.';
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00035DC0 File Offset: 0x00033FC0
		public byte ToByte(char c)
		{
			byte[] bytes = this._ebcdicEncoding.GetBytes(new char[] { c });
			return (bytes.Length != 0) ? bytes[0] : 0;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00035DF4 File Offset: 0x00033FF4
		public override string ToString()
		{
			return "EBCDIC (Code Page 500)";
		}

		// Token: 0x04000544 RID: 1348
		private Encoding _ebcdicEncoding = Encoding.GetEncoding(500);
	}
}
