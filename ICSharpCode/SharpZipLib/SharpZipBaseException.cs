using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	public class SharpZipBaseException : ApplicationException
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0001E770 File Offset: 0x0001C970
		protected SharpZipBaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001E77C File Offset: 0x0001C97C
		public SharpZipBaseException()
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001E786 File Offset: 0x0001C986
		public SharpZipBaseException(string message)
			: base(message)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001E791 File Offset: 0x0001C991
		public SharpZipBaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
