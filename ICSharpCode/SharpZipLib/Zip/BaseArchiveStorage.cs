using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000078 RID: 120
	public abstract class BaseArchiveStorage : IArchiveStorage
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00025C89 File Offset: 0x00023E89
		protected BaseArchiveStorage(FileUpdateMode updateMode)
		{
			this.updateMode_ = updateMode;
		}

		// Token: 0x060005A6 RID: 1446
		public abstract Stream GetTemporaryOutput();

		// Token: 0x060005A7 RID: 1447
		public abstract Stream ConvertTemporaryToFinal();

		// Token: 0x060005A8 RID: 1448
		public abstract Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060005A9 RID: 1449
		public abstract Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060005AA RID: 1450
		public abstract void Dispose();

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00025C9C File Offset: 0x00023E9C
		public FileUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode_;
			}
		}

		// Token: 0x0400039C RID: 924
		private FileUpdateMode updateMode_;
	}
}
