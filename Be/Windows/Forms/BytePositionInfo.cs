using System;

namespace Be.Windows.Forms
{
	// Token: 0x020000D9 RID: 217
	internal struct BytePositionInfo
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x00035FA0 File Offset: 0x000341A0
		public BytePositionInfo(long index, int characterPosition)
		{
			this._index = index;
			this._characterPosition = characterPosition;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00035FB4 File Offset: 0x000341B4
		public int CharacterPosition
		{
			get
			{
				return this._characterPosition;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00035FCC File Offset: 0x000341CC
		public long Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04000545 RID: 1349
		private int _characterPosition;

		// Token: 0x04000546 RID: 1350
		private long _index;
	}
}
