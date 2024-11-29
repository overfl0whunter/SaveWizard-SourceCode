using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Be.Windows.Forms
{
	// Token: 0x020000DE RID: 222
	public class FileByteProvider : IByteProvider, IDisposable
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000951 RID: 2385 RVA: 0x00037500 File Offset: 0x00035700
		// (remove) Token: 0x06000952 RID: 2386 RVA: 0x00037538 File Offset: 0x00035738
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ByteProviderChanged> Changed;

		// Token: 0x06000953 RID: 2387 RVA: 0x00037570 File Offset: 0x00035770
		public FileByteProvider(string fileName)
		{
			this._fileName = fileName;
			try
			{
				this._fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
			}
			catch
			{
				try
				{
					this._fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					this._readOnly = true;
				}
				catch
				{
					throw;
				}
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000375EC File Offset: 0x000357EC
		~FileByteProvider()
		{
			this.Dispose();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0003761C File Offset: 0x0003581C
		private void OnChanged(ByteProviderChanged e)
		{
			bool flag = this.Changed != null;
			if (flag)
			{
				this.Changed(this, e);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00037648 File Offset: 0x00035848
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00037660 File Offset: 0x00035860
		public bool HasChanges()
		{
			return this._writes.Count > 0;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00037680 File Offset: 0x00035880
		public void ApplyChanges()
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				throw new Exception("File is in read-only mode.");
			}
			bool flag = !this.HasChanges();
			if (!flag)
			{
				IDictionaryEnumerator enumerator = this._writes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					long num = (long)enumerator.Key;
					byte b = (byte)enumerator.Value;
					bool flag2 = this._fileStream.Position != num;
					if (flag2)
					{
						this._fileStream.Position = num;
					}
					this._fileStream.Write(new byte[] { b }, 0, 1);
				}
				this._writes.Clear();
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00037731 File Offset: 0x00035931
		public void RejectChanges()
		{
			this._writes.Clear();
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600095A RID: 2394 RVA: 0x00037740 File Offset: 0x00035940
		// (remove) Token: 0x0600095B RID: 2395 RVA: 0x00037778 File Offset: 0x00035978
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LengthChanged;

		// Token: 0x0600095C RID: 2396 RVA: 0x000377B0 File Offset: 0x000359B0
		public byte ReadByte(long index)
		{
			bool flag = this._writes.Contains(index);
			byte b;
			if (flag)
			{
				b = this._writes[index];
			}
			else
			{
				bool flag2 = this._fileStream.Position != index;
				if (flag2)
				{
					this._fileStream.Position = index;
				}
				byte b2 = (byte)this._fileStream.ReadByte();
				b = b2;
			}
			return b;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00037814 File Offset: 0x00035A14
		public long Length
		{
			get
			{
				return this._fileStream.Length;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00037834 File Offset: 0x00035A34
		public void WriteByte(long index, byte value, bool noEvt = false)
		{
			bool flag = this._writes.Contains(index);
			if (flag)
			{
				this._writes[index] = value;
			}
			else
			{
				this._writes.Add(index, value);
			}
			if (noEvt)
			{
				this.OnChanged(new ByteProviderChanged());
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00037881 File Offset: 0x00035A81
		public void DeleteBytes(long index, long length)
		{
			throw new NotSupportedException("FileByteProvider.DeleteBytes");
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003788E File Offset: 0x00035A8E
		public void InsertBytes(long index, byte[] bs)
		{
			throw new NotSupportedException("FileByteProvider.InsertBytes");
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0003789C File Offset: 0x00035A9C
		public bool SupportsWriteByte()
		{
			return !this._readOnly;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000378B8 File Offset: 0x00035AB8
		public bool SupportsInsertBytes()
		{
			return false;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000378CC File Offset: 0x00035ACC
		public bool SupportsDeleteBytes()
		{
			return false;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000378E0 File Offset: 0x00035AE0
		public void Dispose()
		{
			bool flag = this._fileStream != null;
			if (flag)
			{
				this._fileName = null;
				this._fileStream.Close();
				this._fileStream = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400055B RID: 1371
		private FileByteProvider.WriteCollection _writes = new FileByteProvider.WriteCollection();

		// Token: 0x0400055C RID: 1372
		private string _fileName;

		// Token: 0x0400055D RID: 1373
		private FileStream _fileStream;

		// Token: 0x0400055E RID: 1374
		private bool _readOnly;

		// Token: 0x02000219 RID: 537
		private class WriteCollection : DictionaryBase
		{
			// Token: 0x170007B2 RID: 1970
			public byte this[long index]
			{
				get
				{
					return (byte)base.Dictionary[index];
				}
				set
				{
					base.Dictionary[index] = value;
				}
			}

			// Token: 0x06001C63 RID: 7267 RVA: 0x000B2F93 File Offset: 0x000B1193
			public void Add(long index, byte value)
			{
				base.Dictionary.Add(index, value);
			}

			// Token: 0x06001C64 RID: 7268 RVA: 0x000B2FB0 File Offset: 0x000B11B0
			public bool Contains(long index)
			{
				return base.Dictionary.Contains(index);
			}
		}
	}
}
