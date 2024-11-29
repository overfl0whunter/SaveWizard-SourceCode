using System;

namespace Ionic.Zip
{
	// Token: 0x02000036 RID: 54
	public class SaveProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000F34B File Offset: 0x0000D54B
		internal SaveProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesSaved, ZipEntry entry)
			: base(archiveName, before ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesSaved = entriesSaved;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F216 File Offset: 0x0000D416
		internal SaveProgressEventArgs()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F220 File Offset: 0x0000D420
		internal SaveProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F378 File Offset: 0x0000D578
		internal static SaveProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_EntryBytesRead)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		internal static SaveProgressEventArgs Started(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Started);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		internal static SaveProgressEventArgs Completed(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Completed);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public int EntriesSaved
		{
			get
			{
				return this._entriesSaved;
			}
		}

		// Token: 0x0400019E RID: 414
		private int _entriesSaved;
	}
}
