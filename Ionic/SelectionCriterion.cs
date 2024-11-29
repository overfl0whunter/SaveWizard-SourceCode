using System;
using System.Diagnostics;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000006 RID: 6
	internal abstract class SelectionCriterion
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		internal virtual bool Verbose { get; set; }

		// Token: 0x0600000A RID: 10
		internal abstract bool Evaluate(string filename);

		// Token: 0x0600000B RID: 11 RVA: 0x000021C5 File Offset: 0x000003C5
		[Conditional("SelectorTrace")]
		protected static void CriterionTrace(string format, params object[] args)
		{
		}

		// Token: 0x0600000C RID: 12
		internal abstract bool Evaluate(ZipEntry entry);
	}
}
