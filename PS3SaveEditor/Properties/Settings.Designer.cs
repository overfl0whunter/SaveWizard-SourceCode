using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace PS3SaveEditor.Properties
{
	// Token: 0x020001F3 RID: 499
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000ACF48 File Offset: 0x000AB148
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000D28 RID: 3368
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
