using System;

namespace Ionic.Zip
{
	// Token: 0x02000033 RID: 51
	public class ZipProgressEventArgs : EventArgs
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		internal ZipProgressEventArgs()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000F0FA File Offset: 0x0000D2FA
		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			this._archiveName = archiveName;
			this._flavor = flavor;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000F114 File Offset: 0x0000D314
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000F12C File Offset: 0x0000D32C
		public int EntriesTotal
		{
			get
			{
				return this._entriesTotal;
			}
			set
			{
				this._entriesTotal = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000F138 File Offset: 0x0000D338
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000F150 File Offset: 0x0000D350
		public ZipEntry CurrentEntry
		{
			get
			{
				return this._latestEntry;
			}
			set
			{
				this._latestEntry = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000F15C File Offset: 0x0000D35C
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000F174 File Offset: 0x0000D374
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = this._cancel || value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000F188 File Offset: 0x0000D388
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public ZipProgressEventType EventType
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		public string ArchiveName
		{
			get
			{
				return this._archiveName;
			}
			set
			{
				this._archiveName = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public long BytesTransferred
		{
			get
			{
				return this._bytesTransferred;
			}
			set
			{
				this._bytesTransferred = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000F20C File Offset: 0x0000D40C
		public long TotalBytesToTransfer
		{
			get
			{
				return this._totalBytesToTransfer;
			}
			set
			{
				this._totalBytesToTransfer = value;
			}
		}

		// Token: 0x04000197 RID: 407
		private int _entriesTotal;

		// Token: 0x04000198 RID: 408
		private bool _cancel;

		// Token: 0x04000199 RID: 409
		private ZipEntry _latestEntry;

		// Token: 0x0400019A RID: 410
		private ZipProgressEventType _flavor;

		// Token: 0x0400019B RID: 411
		private string _archiveName;

		// Token: 0x0400019C RID: 412
		private long _bytesTransferred;

		// Token: 0x0400019D RID: 413
		private long _totalBytesToTransfer;
	}
}
