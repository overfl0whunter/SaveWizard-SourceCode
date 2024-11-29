using System;
using System.ComponentModel;

namespace Ionic
{
	// Token: 0x02000005 RID: 5
	internal enum ComparisonOperator
	{
		// Token: 0x0400000E RID: 14
		[Description(">")]
		GreaterThan,
		// Token: 0x0400000F RID: 15
		[Description(">=")]
		GreaterThanOrEqualTo,
		// Token: 0x04000010 RID: 16
		[Description("<")]
		LesserThan,
		// Token: 0x04000011 RID: 17
		[Description("<=")]
		LesserThanOrEqualTo,
		// Token: 0x04000012 RID: 18
		[Description("=")]
		EqualTo,
		// Token: 0x04000013 RID: 19
		[Description("!=")]
		NotEqualTo
	}
}
