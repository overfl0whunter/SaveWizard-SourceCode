using System;

namespace Ionic.Zip
{
	// Token: 0x02000037 RID: 55
	public class ExtractProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000F404 File Offset: 0x0000D604
		internal ExtractProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesExtracted, ZipEntry entry, string extractLocation)
			: base(archiveName, before ? ZipProgressEventType.Extracting_BeforeExtractEntry : ZipProgressEventType.Extracting_AfterExtractEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesExtracted = entriesExtracted;
			this._target = extractLocation;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000F220 File Offset: 0x0000D420
		internal ExtractProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000F216 File Offset: 0x0000D416
		internal ExtractProgressEventArgs()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F43C File Offset: 0x0000D63C
		internal static ExtractProgressEventArgs BeforeExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_BeforeExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000F478 File Offset: 0x0000D678
		internal static ExtractProgressEventArgs ExtractExisting(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		internal static ExtractProgressEventArgs AfterExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_AfterExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		internal static ExtractProgressEventArgs ExtractAllStarted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_BeforeExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000F514 File Offset: 0x0000D714
		internal static ExtractProgressEventArgs ExtractAllCompleted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_AfterExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000F538 File Offset: 0x0000D738
		internal static ExtractProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesWritten, long totalBytes)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_EntryBytesWritten)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesWritten,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000F574 File Offset: 0x0000D774
		public int EntriesExtracted
		{
			get
			{
				return this._entriesExtracted;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000F58C File Offset: 0x0000D78C
		public string ExtractLocation
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x0400019F RID: 415
		private int _entriesExtracted;

		// Token: 0x040001A0 RID: 416
		private string _target;
	}
}
