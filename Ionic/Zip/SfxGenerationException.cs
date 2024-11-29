using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200003C RID: 60
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00008")]
	[Serializable]
	public class SfxGenerationException : ZipException
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000F615 File Offset: 0x0000D815
		public SfxGenerationException()
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000F61F File Offset: 0x0000D81F
		public SfxGenerationException(string message)
			: base(message)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000F636 File Offset: 0x0000D836
		protected SfxGenerationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
