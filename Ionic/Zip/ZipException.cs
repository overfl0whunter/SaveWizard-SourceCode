using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200003E RID: 62
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	[Serializable]
	public class ZipException : Exception
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000C8DD File Offset: 0x0000AADD
		public ZipException()
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C8E7 File Offset: 0x0000AAE7
		public ZipException(string message)
			: base(message)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000F642 File Offset: 0x0000D842
		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F64E File Offset: 0x0000D84E
		protected ZipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
