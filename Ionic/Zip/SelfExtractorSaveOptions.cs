using System;

namespace Ionic.Zip
{
	// Token: 0x02000053 RID: 83
	public class SelfExtractorSaveOptions
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001CA98 File Offset: 0x0001AC98
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		public SelfExtractorFlavor Flavor { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001CAA9 File Offset: 0x0001ACA9
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0001CAB1 File Offset: 0x0001ACB1
		public string PostExtractCommandLine { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001CABA File Offset: 0x0001ACBA
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0001CAC2 File Offset: 0x0001ACC2
		public string DefaultExtractDirectory { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0001CACB File Offset: 0x0001ACCB
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		public string IconFile { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001CADC File Offset: 0x0001ACDC
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public bool Quiet { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001CAED File Offset: 0x0001ACED
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0001CAF5 File Offset: 0x0001ACF5
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001CAFE File Offset: 0x0001ACFE
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0001CB06 File Offset: 0x0001AD06
		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001CB0F File Offset: 0x0001AD0F
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0001CB17 File Offset: 0x0001AD17
		public Version FileVersion { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001CB20 File Offset: 0x0001AD20
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0001CB28 File Offset: 0x0001AD28
		public string ProductVersion { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001CB31 File Offset: 0x0001AD31
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0001CB39 File Offset: 0x0001AD39
		public string Copyright { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001CB42 File Offset: 0x0001AD42
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0001CB4A File Offset: 0x0001AD4A
		public string Description { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001CB53 File Offset: 0x0001AD53
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0001CB5B File Offset: 0x0001AD5B
		public string ProductName { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001CB64 File Offset: 0x0001AD64
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public string SfxExeWindowTitle { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001CB75 File Offset: 0x0001AD75
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001CB7D File Offset: 0x0001AD7D
		public string AdditionalCompilerSwitches { get; set; }
	}
}
