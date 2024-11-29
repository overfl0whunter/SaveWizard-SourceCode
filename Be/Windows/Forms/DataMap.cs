using System;
using System.Collections;

namespace Be.Windows.Forms
{
	// Token: 0x020000DB RID: 219
	internal class DataMap : ICollection, IEnumerable
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0003602C File Offset: 0x0003422C
		public DataMap()
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00036044 File Offset: 0x00034244
		public DataMap(IEnumerable collection)
		{
			bool flag = collection == null;
			if (flag)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (object obj in collection)
			{
				DataBlock dataBlock = (DataBlock)obj;
				this.AddLast(dataBlock);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x000360C4 File Offset: 0x000342C4
		public DataBlock FirstBlock
		{
			get
			{
				return this._firstBlock;
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000360DC File Offset: 0x000342DC
		public void AddAfter(DataBlock block, DataBlock newBlock)
		{
			this.AddAfterInternal(block, newBlock);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000360E8 File Offset: 0x000342E8
		public void AddBefore(DataBlock block, DataBlock newBlock)
		{
			this.AddBeforeInternal(block, newBlock);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000360F4 File Offset: 0x000342F4
		public void AddFirst(DataBlock block)
		{
			bool flag = this._firstBlock == null;
			if (flag)
			{
				this.AddBlockToEmptyMap(block);
			}
			else
			{
				this.AddBeforeInternal(this._firstBlock, block);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0003612C File Offset: 0x0003432C
		public void AddLast(DataBlock block)
		{
			bool flag = this._firstBlock == null;
			if (flag)
			{
				this.AddBlockToEmptyMap(block);
			}
			else
			{
				this.AddAfterInternal(this.GetLastBlock(), block);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00036163 File Offset: 0x00034363
		public void Remove(DataBlock block)
		{
			this.RemoveInternal(block);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00036170 File Offset: 0x00034370
		public void RemoveFirst()
		{
			bool flag = this._firstBlock == null;
			if (flag)
			{
				throw new InvalidOperationException("The collection is empty.");
			}
			this.RemoveInternal(this._firstBlock);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000361A4 File Offset: 0x000343A4
		public void RemoveLast()
		{
			bool flag = this._firstBlock == null;
			if (flag)
			{
				throw new InvalidOperationException("The collection is empty.");
			}
			this.RemoveInternal(this.GetLastBlock());
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000361D8 File Offset: 0x000343D8
		public DataBlock Replace(DataBlock block, DataBlock newBlock)
		{
			this.AddAfterInternal(block, newBlock);
			this.RemoveInternal(block);
			return newBlock;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000361FC File Offset: 0x000343FC
		public void Clear()
		{
			DataBlock nextBlock;
			for (DataBlock dataBlock = this.FirstBlock; dataBlock != null; dataBlock = nextBlock)
			{
				nextBlock = dataBlock.NextBlock;
				this.InvalidateBlock(dataBlock);
			}
			this._firstBlock = null;
			this._count = 0;
			this._version++;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003624C File Offset: 0x0003444C
		private void AddAfterInternal(DataBlock block, DataBlock newBlock)
		{
			newBlock._previousBlock = block;
			newBlock._nextBlock = block._nextBlock;
			newBlock._map = this;
			bool flag = block._nextBlock != null;
			if (flag)
			{
				block._nextBlock._previousBlock = newBlock;
			}
			block._nextBlock = newBlock;
			this._version++;
			this._count++;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000362B4 File Offset: 0x000344B4
		private void AddBeforeInternal(DataBlock block, DataBlock newBlock)
		{
			newBlock._nextBlock = block;
			newBlock._previousBlock = block._previousBlock;
			newBlock._map = this;
			bool flag = block._previousBlock != null;
			if (flag)
			{
				block._previousBlock._nextBlock = newBlock;
			}
			block._previousBlock = newBlock;
			bool flag2 = this._firstBlock == block;
			if (flag2)
			{
				this._firstBlock = newBlock;
			}
			this._version++;
			this._count++;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00036330 File Offset: 0x00034530
		private void RemoveInternal(DataBlock block)
		{
			DataBlock previousBlock = block._previousBlock;
			DataBlock nextBlock = block._nextBlock;
			bool flag = previousBlock != null;
			if (flag)
			{
				previousBlock._nextBlock = nextBlock;
			}
			bool flag2 = nextBlock != null;
			if (flag2)
			{
				nextBlock._previousBlock = previousBlock;
			}
			bool flag3 = this._firstBlock == block;
			if (flag3)
			{
				this._firstBlock = nextBlock;
			}
			this.InvalidateBlock(block);
			this._count--;
			this._version++;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000363AC File Offset: 0x000345AC
		private DataBlock GetLastBlock()
		{
			DataBlock dataBlock = null;
			for (DataBlock dataBlock2 = this.FirstBlock; dataBlock2 != null; dataBlock2 = dataBlock2.NextBlock)
			{
				dataBlock = dataBlock2;
			}
			return dataBlock;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000363DD File Offset: 0x000345DD
		private void InvalidateBlock(DataBlock block)
		{
			block._map = null;
			block._nextBlock = null;
			block._previousBlock = null;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000363F5 File Offset: 0x000345F5
		private void AddBlockToEmptyMap(DataBlock block)
		{
			block._map = this;
			block._nextBlock = null;
			block._previousBlock = null;
			this._firstBlock = block;
			this._version++;
			this._count++;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00036430 File Offset: 0x00034630
		public void CopyTo(Array array, int index)
		{
			DataBlock[] array2 = array as DataBlock[];
			for (DataBlock dataBlock = this.FirstBlock; dataBlock != null; dataBlock = dataBlock.NextBlock)
			{
				array2[index++] = dataBlock;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00036468 File Offset: 0x00034668
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00036480 File Offset: 0x00034680
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00036494 File Offset: 0x00034694
		public object SyncRoot
		{
			get
			{
				return this._syncRoot;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000364AC File Offset: 0x000346AC
		public IEnumerator GetEnumerator()
		{
			return new DataMap.Enumerator(this);
		}

		// Token: 0x0400054A RID: 1354
		private readonly object _syncRoot = new object();

		// Token: 0x0400054B RID: 1355
		internal int _count;

		// Token: 0x0400054C RID: 1356
		internal DataBlock _firstBlock;

		// Token: 0x0400054D RID: 1357
		internal int _version;

		// Token: 0x02000218 RID: 536
		internal class Enumerator : IEnumerator, IDisposable
		{
			// Token: 0x06001C5C RID: 7260 RVA: 0x000B2DE6 File Offset: 0x000B0FE6
			internal Enumerator(DataMap map)
			{
				this._map = map;
				this._version = map._version;
				this._current = null;
				this._index = -1;
			}

			// Token: 0x170007B1 RID: 1969
			// (get) Token: 0x06001C5D RID: 7261 RVA: 0x000B2E14 File Offset: 0x000B1014
			object IEnumerator.Current
			{
				get
				{
					bool flag = this._index < 0 || this._index > this._map.Count;
					if (flag)
					{
						throw new InvalidOperationException("Enumerator is positioned before the first element or after the last element of the collection.");
					}
					return this._current;
				}
			}

			// Token: 0x06001C5E RID: 7262 RVA: 0x000B2E5C File Offset: 0x000B105C
			public bool MoveNext()
			{
				bool flag = this._version != this._map._version;
				if (flag)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				bool flag2 = this._index >= this._map.Count;
				bool flag3;
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					int num = this._index + 1;
					this._index = num;
					bool flag4 = num == 0;
					if (flag4)
					{
						this._current = this._map.FirstBlock;
					}
					else
					{
						this._current = this._current.NextBlock;
					}
					flag3 = this._index < this._map.Count;
				}
				return flag3;
			}

			// Token: 0x06001C5F RID: 7263 RVA: 0x000B2F0C File Offset: 0x000B110C
			void IEnumerator.Reset()
			{
				bool flag = this._version != this._map._version;
				if (flag)
				{
					throw new InvalidOperationException("Collection was modified after the enumerator was instantiated.");
				}
				this._index = -1;
				this._current = null;
			}

			// Token: 0x06001C60 RID: 7264 RVA: 0x000021C5 File Offset: 0x000003C5
			public void Dispose()
			{
			}

			// Token: 0x04000DE1 RID: 3553
			private DataMap _map;

			// Token: 0x04000DE2 RID: 3554
			private DataBlock _current;

			// Token: 0x04000DE3 RID: 3555
			private int _index;

			// Token: 0x04000DE4 RID: 3556
			private int _version;
		}
	}
}
