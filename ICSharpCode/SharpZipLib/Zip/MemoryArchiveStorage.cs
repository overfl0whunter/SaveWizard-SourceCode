using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007A RID: 122
	public class MemoryArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x00025FE4 File Offset: 0x000241E4
		public MemoryArchiveStorage()
			: base(FileUpdateMode.Direct)
		{
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00025FEF File Offset: 0x000241EF
		public MemoryArchiveStorage(FileUpdateMode updateMode)
			: base(updateMode)
		{
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00025FFC File Offset: 0x000241FC
		public MemoryStream FinalStream
		{
			get
			{
				return this.finalStream_;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00026014 File Offset: 0x00024214
		public override Stream GetTemporaryOutput()
		{
			this.temporaryStream_ = new MemoryStream();
			return this.temporaryStream_;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00026038 File Offset: 0x00024238
		public override Stream ConvertTemporaryToFinal()
		{
			bool flag = this.temporaryStream_ == null;
			if (flag)
			{
				throw new ZipException("No temporary stream has been created");
			}
			this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
			return this.finalStream_;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00026080 File Offset: 0x00024280
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			this.temporaryStream_ = new MemoryStream();
			stream.Position = 0L;
			StreamUtils.Copy(stream, this.temporaryStream_, new byte[4096]);
			return this.temporaryStream_;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000260C4 File Offset: 0x000242C4
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			bool flag = stream == null || !stream.CanWrite;
			Stream stream2;
			if (flag)
			{
				stream2 = new MemoryStream();
				bool flag2 = stream != null;
				if (flag2)
				{
					stream.Position = 0L;
					StreamUtils.Copy(stream, stream2, new byte[4096]);
					stream.Close();
				}
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00026124 File Offset: 0x00024324
		public override void Dispose()
		{
			bool flag = this.temporaryStream_ != null;
			if (flag)
			{
				this.temporaryStream_.Close();
			}
		}

		// Token: 0x040003A0 RID: 928
		private MemoryStream temporaryStream_;

		// Token: 0x040003A1 RID: 929
		private MemoryStream finalStream_;
	}
}
