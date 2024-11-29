using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PS3SaveEditor;

namespace Be.Windows.Forms
{
	// Token: 0x020000DD RID: 221
	public sealed class DynamicFileByteProvider : IByteProvider, IDisposable
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x000367D3 File Offset: 0x000349D3
		public DynamicFileByteProvider(string fileName)
			: this(fileName, false)
		{
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000367E0 File Offset: 0x000349E0
		public DynamicFileByteProvider(string fileName, bool readOnly)
		{
			this._fileName = fileName;
			try
			{
				bool flag = !readOnly;
				if (flag)
				{
					this._stream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				}
				else
				{
					this._stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				CustomMsgBox.Show(ex.Message, "UnauthorizedAccess", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
			}
			catch (Exception ex2)
			{
				CustomMsgBox.Show(ex2.Message);
			}
			this._readOnly = readOnly;
			this.ReInitialize();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00036880 File Offset: 0x00034A80
		public DynamicFileByteProvider(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag2 = !stream.CanSeek;
			if (flag2)
			{
				throw new ArgumentException("stream must supported seek operations(CanSeek)");
			}
			this._stream = stream;
			this._readOnly = !stream.CanWrite;
			this.ReInitialize();
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000939 RID: 2361 RVA: 0x000368E0 File Offset: 0x00034AE0
		// (remove) Token: 0x0600093A RID: 2362 RVA: 0x00036918 File Offset: 0x00034B18
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LengthChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600093B RID: 2363 RVA: 0x00036950 File Offset: 0x00034B50
		// (remove) Token: 0x0600093C RID: 2364 RVA: 0x00036988 File Offset: 0x00034B88
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ByteProviderChanged> Changed;

		// Token: 0x0600093D RID: 2365 RVA: 0x000369C0 File Offset: 0x00034BC0
		public byte ReadByte(long index)
		{
			long num;
			DataBlock dataBlock = this.GetDataBlock(index, out num);
			FileDataBlock fileDataBlock = dataBlock as FileDataBlock;
			bool flag = fileDataBlock != null;
			byte b;
			if (flag)
			{
				b = this.ReadByteFromFile(fileDataBlock.FileOffset + index - num);
			}
			else
			{
				MemoryDataBlock memoryDataBlock = (MemoryDataBlock)dataBlock;
				checked
				{
					b = memoryDataBlock.Data[(int)((IntPtr)(unchecked(index - num)))];
				}
			}
			return b;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00036A18 File Offset: 0x00034C18
		public void WriteByte(long index, byte value, bool noEvt = false)
		{
			try
			{
				long num;
				DataBlock dataBlock = this.GetDataBlock(index, out num);
				MemoryDataBlock memoryDataBlock = dataBlock as MemoryDataBlock;
				bool flag = memoryDataBlock != null;
				if (flag)
				{
					checked
					{
						memoryDataBlock.Data[(int)((IntPtr)(unchecked(index - num)))] = value;
					}
				}
				else
				{
					FileDataBlock fileDataBlock = (FileDataBlock)dataBlock;
					bool flag2 = num == index && dataBlock.PreviousBlock != null;
					if (flag2)
					{
						MemoryDataBlock memoryDataBlock2 = dataBlock.PreviousBlock as MemoryDataBlock;
						bool flag3 = memoryDataBlock2 != null;
						if (flag3)
						{
							memoryDataBlock2.AddByteToEnd(value);
							fileDataBlock.RemoveBytesFromStart(1L);
							bool flag4 = fileDataBlock.Length == 0L;
							if (flag4)
							{
								this._dataMap.Remove(fileDataBlock);
							}
							return;
						}
					}
					bool flag5 = num + fileDataBlock.Length - 1L == index && dataBlock.NextBlock != null;
					if (flag5)
					{
						MemoryDataBlock memoryDataBlock3 = dataBlock.NextBlock as MemoryDataBlock;
						bool flag6 = memoryDataBlock3 != null;
						if (flag6)
						{
							memoryDataBlock3.AddByteToStart(value);
							fileDataBlock.RemoveBytesFromEnd(1L);
							bool flag7 = fileDataBlock.Length == 0L;
							if (flag7)
							{
								this._dataMap.Remove(fileDataBlock);
							}
							return;
						}
					}
					FileDataBlock fileDataBlock2 = null;
					bool flag8 = index > num;
					if (flag8)
					{
						fileDataBlock2 = new FileDataBlock(fileDataBlock.FileOffset, index - num);
					}
					FileDataBlock fileDataBlock3 = null;
					bool flag9 = index < num + fileDataBlock.Length - 1L;
					if (flag9)
					{
						fileDataBlock3 = new FileDataBlock(fileDataBlock.FileOffset + index - num + 1L, fileDataBlock.Length - (index - num + 1L));
					}
					dataBlock = this._dataMap.Replace(dataBlock, new MemoryDataBlock(value));
					bool flag10 = fileDataBlock2 != null;
					if (flag10)
					{
						this._dataMap.AddBefore(dataBlock, fileDataBlock2);
					}
					bool flag11 = fileDataBlock3 != null;
					if (flag11)
					{
						this._dataMap.AddAfter(dataBlock, fileDataBlock3);
					}
				}
			}
			finally
			{
				if (noEvt)
				{
					this.OnChanged(new ByteProviderChanged());
				}
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00036C0C File Offset: 0x00034E0C
		public void InsertBytes(long index, byte[] bs)
		{
			try
			{
				long num;
				DataBlock dataBlock = this.GetDataBlock(index, out num);
				MemoryDataBlock memoryDataBlock = dataBlock as MemoryDataBlock;
				bool flag = memoryDataBlock != null;
				if (flag)
				{
					memoryDataBlock.InsertBytes(index - num, bs);
				}
				else
				{
					FileDataBlock fileDataBlock = (FileDataBlock)dataBlock;
					bool flag2 = num == index && dataBlock.PreviousBlock != null;
					if (flag2)
					{
						MemoryDataBlock memoryDataBlock2 = dataBlock.PreviousBlock as MemoryDataBlock;
						bool flag3 = memoryDataBlock2 != null;
						if (flag3)
						{
							memoryDataBlock2.InsertBytes(memoryDataBlock2.Length, bs);
							return;
						}
					}
					FileDataBlock fileDataBlock2 = null;
					bool flag4 = index > num;
					if (flag4)
					{
						fileDataBlock2 = new FileDataBlock(fileDataBlock.FileOffset, index - num);
					}
					FileDataBlock fileDataBlock3 = null;
					bool flag5 = index < num + fileDataBlock.Length;
					if (flag5)
					{
						fileDataBlock3 = new FileDataBlock(fileDataBlock.FileOffset + index - num, fileDataBlock.Length - (index - num));
					}
					dataBlock = this._dataMap.Replace(dataBlock, new MemoryDataBlock(bs));
					bool flag6 = fileDataBlock2 != null;
					if (flag6)
					{
						this._dataMap.AddBefore(dataBlock, fileDataBlock2);
					}
					bool flag7 = fileDataBlock3 != null;
					if (flag7)
					{
						this._dataMap.AddAfter(dataBlock, fileDataBlock3);
					}
				}
			}
			finally
			{
				this._totalLength += (long)bs.Length;
				this.OnLengthChanged(EventArgs.Empty);
				this.OnChanged(new ByteProviderChanged());
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00036D7C File Offset: 0x00034F7C
		public void DeleteBytes(long index, long length)
		{
			try
			{
				long num = length;
				long num2;
				DataBlock dataBlock = this.GetDataBlock(index, out num2);
				while (num > 0L && dataBlock != null)
				{
					long length2 = dataBlock.Length;
					DataBlock nextBlock = dataBlock.NextBlock;
					long num3 = Math.Min(num, length2 - (index - num2));
					dataBlock.RemoveBytes(index - num2, num3);
					bool flag = dataBlock.Length == 0L;
					if (flag)
					{
						this._dataMap.Remove(dataBlock);
						bool flag2 = this._dataMap.FirstBlock == null;
						if (flag2)
						{
							this._dataMap.AddFirst(new MemoryDataBlock(new byte[0]));
						}
					}
					num -= num3;
					num2 += dataBlock.Length;
					dataBlock = ((num > 0L) ? nextBlock : null);
				}
			}
			finally
			{
				this._totalLength -= length;
				this.OnLengthChanged(EventArgs.Empty);
				this.OnChanged(new ByteProviderChanged());
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00036E7C File Offset: 0x0003507C
		public long Length
		{
			get
			{
				return this._totalLength;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00036E94 File Offset: 0x00035094
		public bool HasChanges()
		{
			bool readOnly = this._readOnly;
			bool flag;
			if (readOnly)
			{
				flag = false;
			}
			else
			{
				bool flag2 = this._totalLength != this._stream.Length;
				if (flag2)
				{
					flag = true;
				}
				else
				{
					long num = 0L;
					for (DataBlock dataBlock = this._dataMap.FirstBlock; dataBlock != null; dataBlock = dataBlock.NextBlock)
					{
						FileDataBlock fileDataBlock = dataBlock as FileDataBlock;
						bool flag3 = fileDataBlock == null;
						if (flag3)
						{
							return true;
						}
						bool flag4 = fileDataBlock.FileOffset != num;
						if (flag4)
						{
							return true;
						}
						num += fileDataBlock.Length;
					}
					flag = num != this._stream.Length;
				}
			}
			return flag;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00036F4C File Offset: 0x0003514C
		public void ApplyChanges()
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				throw new OperationCanceledException("File is in read-only mode");
			}
			bool flag = this._totalLength > this._stream.Length;
			if (flag)
			{
				this._stream.SetLength(this._totalLength);
			}
			long num = 0L;
			for (DataBlock dataBlock = this._dataMap.FirstBlock; dataBlock != null; dataBlock = dataBlock.NextBlock)
			{
				FileDataBlock fileDataBlock = dataBlock as FileDataBlock;
				bool flag2 = fileDataBlock != null && fileDataBlock.FileOffset != num;
				if (flag2)
				{
					this.MoveFileBlock(fileDataBlock, num);
				}
				num += dataBlock.Length;
			}
			num = 0L;
			for (DataBlock dataBlock2 = this._dataMap.FirstBlock; dataBlock2 != null; dataBlock2 = dataBlock2.NextBlock)
			{
				MemoryDataBlock memoryDataBlock = dataBlock2 as MemoryDataBlock;
				bool flag3 = memoryDataBlock != null;
				if (flag3)
				{
					this._stream.Position = num;
					int num2 = 0;
					while ((long)num2 < memoryDataBlock.Length)
					{
						this._stream.Write(memoryDataBlock.Data, num2, (int)Math.Min(4096L, memoryDataBlock.Length - (long)num2));
						num2 += 4096;
					}
				}
				num += dataBlock2.Length;
			}
			this._stream.SetLength(this._totalLength);
			this.ReInitialize();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000370B8 File Offset: 0x000352B8
		public bool SupportsWriteByte()
		{
			return !this._readOnly;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000370D4 File Offset: 0x000352D4
		public bool SupportsInsertBytes()
		{
			return !this._readOnly;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000370F0 File Offset: 0x000352F0
		public bool SupportsDeleteBytes()
		{
			return !this._readOnly;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0003710C File Offset: 0x0003530C
		~DynamicFileByteProvider()
		{
			this.Dispose();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0003713C File Offset: 0x0003533C
		public void Dispose()
		{
			bool flag = this._stream != null;
			if (flag)
			{
				this._stream.Close();
				this._stream = null;
			}
			this._fileName = null;
			this._dataMap = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00037184 File Offset: 0x00035384
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003719C File Offset: 0x0003539C
		private void OnLengthChanged(EventArgs e)
		{
			bool flag = this.LengthChanged != null;
			if (flag)
			{
				this.LengthChanged(this, e);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000371C8 File Offset: 0x000353C8
		private void OnChanged(ByteProviderChanged e)
		{
			bool flag = this.Changed != null;
			if (flag)
			{
				this.Changed(this, e);
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000371F4 File Offset: 0x000353F4
		private DataBlock GetDataBlock(long findOffset, out long blockOffset)
		{
			bool flag = findOffset < 0L || findOffset > this._totalLength;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("findOffset");
			}
			blockOffset = 0L;
			for (DataBlock dataBlock = this._dataMap.FirstBlock; dataBlock != null; dataBlock = dataBlock.NextBlock)
			{
				bool flag2 = (blockOffset <= findOffset && blockOffset + dataBlock.Length > findOffset) || dataBlock.NextBlock == null;
				if (flag2)
				{
					return dataBlock;
				}
				blockOffset += dataBlock.Length;
			}
			return null;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00037280 File Offset: 0x00035480
		private FileDataBlock GetNextFileDataBlock(DataBlock block, long dataOffset, out long nextDataOffset)
		{
			nextDataOffset = dataOffset + block.Length;
			for (block = block.NextBlock; block != null; block = block.NextBlock)
			{
				FileDataBlock fileDataBlock = block as FileDataBlock;
				bool flag = fileDataBlock != null;
				if (flag)
				{
					return fileDataBlock;
				}
				nextDataOffset += block.Length;
			}
			return null;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000372D8 File Offset: 0x000354D8
		private byte ReadByteFromFile(long fileOffset)
		{
			bool flag = this._stream.Position != fileOffset;
			if (flag)
			{
				this._stream.Position = fileOffset;
			}
			return (byte)this._stream.ReadByte();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003731C File Offset: 0x0003551C
		private void MoveFileBlock(FileDataBlock fileBlock, long dataOffset)
		{
			long num;
			FileDataBlock nextFileDataBlock = this.GetNextFileDataBlock(fileBlock, dataOffset, out num);
			bool flag = nextFileDataBlock != null && dataOffset + fileBlock.Length > nextFileDataBlock.FileOffset;
			if (flag)
			{
				this.MoveFileBlock(nextFileDataBlock, num);
			}
			bool flag2 = fileBlock.FileOffset > dataOffset;
			if (flag2)
			{
				byte[] array = new byte[4096];
				for (long num2 = 0L; num2 < fileBlock.Length; num2 += (long)array.Length)
				{
					long num3 = fileBlock.FileOffset + num2;
					int num4 = (int)Math.Min((long)array.Length, fileBlock.Length - num2);
					this._stream.Position = num3;
					this._stream.Read(array, 0, num4);
					long num5 = dataOffset + num2;
					this._stream.Position = num5;
					this._stream.Write(array, 0, num4);
				}
			}
			else
			{
				byte[] array2 = new byte[4096];
				for (long num6 = 0L; num6 < fileBlock.Length; num6 += (long)array2.Length)
				{
					int num7 = (int)Math.Min((long)array2.Length, fileBlock.Length - num6);
					long num8 = fileBlock.FileOffset + fileBlock.Length - num6 - (long)num7;
					this._stream.Position = num8;
					this._stream.Read(array2, 0, num7);
					long num9 = dataOffset + fileBlock.Length - num6 - (long)num7;
					this._stream.Position = num9;
					this._stream.Write(array2, 0, num7);
				}
			}
			fileBlock.SetFileOffset(dataOffset);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000374C0 File Offset: 0x000356C0
		private void ReInitialize()
		{
			this._dataMap = new DataMap();
			this._dataMap.AddFirst(new FileDataBlock(0L, this._stream.Length));
			this._totalLength = this._stream.Length;
		}

		// Token: 0x04000552 RID: 1362
		private const int COPY_BLOCK_SIZE = 4096;

		// Token: 0x04000553 RID: 1363
		private string _fileName;

		// Token: 0x04000554 RID: 1364
		private Stream _stream;

		// Token: 0x04000555 RID: 1365
		private DataMap _dataMap;

		// Token: 0x04000556 RID: 1366
		private long _totalLength;

		// Token: 0x04000557 RID: 1367
		private bool _readOnly;
	}
}
