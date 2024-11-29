using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200003A RID: 58
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	[Serializable]
	public class BadReadException : ZipException
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000F615 File Offset: 0x0000D815
		public BadReadException()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000F61F File Offset: 0x0000D81F
		public BadReadException(string message)
			: base(message)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000F62A File Offset: 0x0000D82A
		public BadReadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000F636 File Offset: 0x0000D836
		protected BadReadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
