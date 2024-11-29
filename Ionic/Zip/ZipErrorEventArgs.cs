using System;

namespace Ionic.Zip
{
	// Token: 0x02000038 RID: 56
	public class ZipErrorEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000F216 File Offset: 0x0000D416
		private ZipErrorEventArgs()
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		internal static ZipErrorEventArgs Saving(string archiveName, ZipEntry entry, Exception exception)
		{
			return new ZipErrorEventArgs
			{
				EventType = ZipProgressEventType.Error_Saving,
				ArchiveName = archiveName,
				CurrentEntry = entry,
				_exc = exception
			};
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public Exception Exception
		{
			get
			{
				return this._exc;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public string FileName
		{
			get
			{
				return base.CurrentEntry.LocalFileName;
			}
		}

		// Token: 0x040001A1 RID: 417
		private Exception _exc;
	}
}
