using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200003B RID: 59
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	[Serializable]
	public class BadCrcException : ZipException
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000F615 File Offset: 0x0000D815
		public BadCrcException()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000F61F File Offset: 0x0000D81F
		public BadCrcException(string message)
			: base(message)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000F636 File Offset: 0x0000D836
		protected BadCrcException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
