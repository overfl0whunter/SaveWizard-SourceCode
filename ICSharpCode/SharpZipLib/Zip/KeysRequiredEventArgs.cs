using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200006C RID: 108
	public class KeysRequiredEventArgs : EventArgs
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x00021F3D File Offset: 0x0002013D
		public KeysRequiredEventArgs(string name)
		{
			this.fileName = name;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00021F4E File Offset: 0x0002014E
		public KeysRequiredEventArgs(string name, byte[] keyValue)
		{
			this.fileName = name;
			this.key = keyValue;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00021F68 File Offset: 0x00020168
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00021F80 File Offset: 0x00020180
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00021F98 File Offset: 0x00020198
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x0400036E RID: 878
		private string fileName;

		// Token: 0x0400036F RID: 879
		private byte[] key;
	}
}
