using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PS3SaveEditor.CustomScrollbar
{
	// Token: 0x020001F4 RID: 500
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resource
	{
		// Token: 0x06001A78 RID: 6776 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal Resource()
		{
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x000ACF80 File Offset: 0x000AB180
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resource.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("PS3SaveEditor.CustomScrollbar.Resource", typeof(Resource).Assembly);
					Resource.resourceMan = resourceManager;
				}
				return Resource.resourceMan;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x000ACFC8 File Offset: 0x000AB1C8
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x000ACFDF File Offset: 0x000AB1DF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resource.resourceCulture;
			}
			set
			{
				Resource.resourceCulture = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x000ACFE8 File Offset: 0x000AB1E8
		internal static Bitmap downarrow
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("downarrow", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000AD018 File Offset: 0x000AB218
		internal static Bitmap leftarrow
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("leftarrow", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000AD048 File Offset: 0x000AB248
		internal static Bitmap rightarrow
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("rightarrow", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000AD078 File Offset: 0x000AB278
		internal static Bitmap ThumbBottom
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbBottom", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x000AD0A8 File Offset: 0x000AB2A8
		internal static Bitmap ThumbLeft
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbLeft", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000AD0D8 File Offset: 0x000AB2D8
		internal static Bitmap ThumbMiddle
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbMiddle", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x000AD108 File Offset: 0x000AB308
		internal static Bitmap ThumbMiddleH
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbMiddleH", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000AD138 File Offset: 0x000AB338
		internal static Bitmap ThumbRight
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbRight", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x000AD168 File Offset: 0x000AB368
		internal static Bitmap ThumbSpanBottom
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbSpanBottom", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000AD198 File Offset: 0x000AB398
		internal static Bitmap ThumbSpanBottom1
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbSpanBottom1", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x000AD1C8 File Offset: 0x000AB3C8
		internal static Bitmap ThumbSpanLeft
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbSpanLeft", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000AD1F8 File Offset: 0x000AB3F8
		internal static Bitmap ThumbSpanRight
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbSpanRight", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000AD228 File Offset: 0x000AB428
		internal static Bitmap ThumbSpanTop
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbSpanTop", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x000AD258 File Offset: 0x000AB458
		internal static Bitmap ThumbTop
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("ThumbTop", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000AD288 File Offset: 0x000AB488
		internal static Bitmap uparrow
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("uparrow", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000D29 RID: 3369
		private static ResourceManager resourceMan;

		// Token: 0x04000D2A RID: 3370
		private static CultureInfo resourceCulture;
	}
}
