using System;

namespace Ionic.Zip
{
	// Token: 0x02000035 RID: 53
	public class AddProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000F216 File Offset: 0x0000D416
		internal AddProgressEventArgs()
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F220 File Offset: 0x0000D420
		private AddProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		internal static AddProgressEventArgs AfterEntry(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_AfterAddEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F314 File Offset: 0x0000D514
		internal static AddProgressEventArgs Started(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Started);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F330 File Offset: 0x0000D530
		internal static AddProgressEventArgs Completed(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Completed);
		}
	}
}
