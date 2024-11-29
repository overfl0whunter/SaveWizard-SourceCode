using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PS3SaveEditor.Properties
{
	// Token: 0x020001F2 RID: 498
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal Resources()
		{
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000ACEE0 File Offset: 0x000AB0E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = object.Equals(null, Resources.resourceMan);
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("PS3SaveEditor.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000ACF28 File Offset: 0x000AB128
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x000ACF3F File Offset: 0x000AB13F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000D26 RID: 3366
		private static ResourceManager resourceMan;

		// Token: 0x04000D27 RID: 3367
		private static CultureInfo resourceCulture;
	}
}
