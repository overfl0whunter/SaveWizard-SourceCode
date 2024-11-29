using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000020 RID: 32
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000C8DD File Offset: 0x0000AADD
		public ZlibException()
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000C8E7 File Offset: 0x0000AAE7
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
