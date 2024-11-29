using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class ZipException : SharpZipBaseException
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x00020EBB File Offset: 0x0001F0BB
		protected ZipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00020EC7 File Offset: 0x0001F0C7
		public ZipException()
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00020ED1 File Offset: 0x0001F0D1
		public ZipException(string message)
			: base(message)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00020EDC File Offset: 0x0001F0DC
		public ZipException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
