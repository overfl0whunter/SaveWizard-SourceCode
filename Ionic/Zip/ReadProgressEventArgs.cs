using System;

namespace Ionic.Zip
{
	// Token: 0x02000034 RID: 52
	public class ReadProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000F216 File Offset: 0x0000D416
		internal ReadProgressEventArgs()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F220 File Offset: 0x0000D420
		private ReadProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F22C File Offset: 0x0000D42C
		internal static ReadProgressEventArgs Before(string archiveName, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_BeforeReadEntry)
			{
				EntriesTotal = entriesTotal
			};
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F250 File Offset: 0x0000D450
		internal static ReadProgressEventArgs After(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_AfterReadEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F27C File Offset: 0x0000D47C
		internal static ReadProgressEventArgs Started(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Started);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F298 File Offset: 0x0000D498
		internal static ReadProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_ArchiveBytesRead)
			{
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		internal static ReadProgressEventArgs Completed(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Completed);
		}
	}
}
