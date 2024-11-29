using System;

namespace Rss
{
	// Token: 0x020000CE RID: 206
	internal class SecurityPermissionAttribute : Attribute
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00035854 File Offset: 0x00033A54
		public SecurityPermissionAttribute(SecurityAction securityAction)
		{
		}

		// Token: 0x04000521 RID: 1313
		public bool Execution;
	}
}
