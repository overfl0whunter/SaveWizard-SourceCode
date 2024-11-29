using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200002C RID: 44
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000F")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ComHelper
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000F064 File Offset: 0x0000D264
		public bool IsZipFile(string filename)
		{
			return ZipFile.IsZipFile(filename);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000F07C File Offset: 0x0000D27C
		public bool IsZipFileWithExtract(string filename)
		{
			return ZipFile.IsZipFile(filename, true);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000F098 File Offset: 0x0000D298
		public bool CheckZip(string filename)
		{
			return ZipFile.CheckZip(filename);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public bool CheckZipPassword(string filename, string password)
		{
			return ZipFile.CheckZipPassword(filename, password);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000F0C9 File Offset: 0x0000D2C9
		public void FixZipDirectory(string filename)
		{
			ZipFile.FixZipDirectory(filename);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public string GetZipLibraryVersion()
		{
			return ZipFile.LibraryVersion.ToString();
		}
	}
}
