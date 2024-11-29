using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200003D RID: 61
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	[Serializable]
	public class BadStateException : ZipException
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000F615 File Offset: 0x0000D815
		public BadStateException()
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000F61F File Offset: 0x0000D81F
		public BadStateException(string message)
			: base(message)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F62A File Offset: 0x0000D82A
		public BadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000F636 File Offset: 0x0000D836
		protected BadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
