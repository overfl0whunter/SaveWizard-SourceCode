using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000039 RID: 57
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	[Serializable]
	public class BadPasswordException : ZipException
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000F615 File Offset: 0x0000D815
		public BadPasswordException()
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000F61F File Offset: 0x0000D81F
		public BadPasswordException(string message)
			: base(message)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000F62A File Offset: 0x0000D82A
		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000F636 File Offset: 0x0000D836
		protected BadPasswordException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
