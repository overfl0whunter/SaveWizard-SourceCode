using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000079 RID: 121
	public class DiskArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x00025CB4 File Offset: 0x00023EB4
		public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
			: base(updateMode)
		{
			bool flag = file.Name == null;
			if (flag)
			{
				throw new ZipException("Cant handle non file archives");
			}
			this.fileName_ = file.Name;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00025CEF File Offset: 0x00023EEF
		public DiskArchiveStorage(ZipFile file)
			: this(file, FileUpdateMode.Safe)
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00025CFC File Offset: 0x00023EFC
		public override Stream GetTemporaryOutput()
		{
			bool flag = this.temporaryName_ != null;
			if (flag)
			{
				this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.temporaryName_, true);
				this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			}
			else
			{
				this.temporaryName_ = Path.GetTempFileName();
				this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			}
			return this.temporaryStream_;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00025D6C File Offset: 0x00023F6C
		public override Stream ConvertTemporaryToFinal()
		{
			bool flag = this.temporaryStream_ == null;
			if (flag)
			{
				throw new ZipException("No temporary stream has been created");
			}
			Stream stream = null;
			string tempFileName = DiskArchiveStorage.GetTempFileName(this.fileName_, false);
			bool flag2 = false;
			try
			{
				this.temporaryStream_.Close();
				File.Move(this.fileName_, tempFileName);
				File.Move(this.temporaryName_, this.fileName_);
				flag2 = true;
				File.Delete(tempFileName);
				stream = File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception)
			{
				stream = null;
				bool flag3 = !flag2;
				if (flag3)
				{
					File.Move(tempFileName, this.fileName_);
					File.Delete(this.temporaryName_);
				}
				throw;
			}
			return stream;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00025E30 File Offset: 0x00024030
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			stream.Close();
			this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.fileName_, true);
			File.Copy(this.fileName_, this.temporaryName_, true);
			this.temporaryStream_ = new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
			return this.temporaryStream_;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00025E88 File Offset: 0x00024088
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			bool flag = stream == null || !stream.CanWrite;
			Stream stream2;
			if (flag)
			{
				bool flag2 = stream != null;
				if (flag2)
				{
					stream.Close();
				}
				stream2 = new FileStream(this.fileName_, FileMode.Open, FileAccess.ReadWrite);
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00025ED8 File Offset: 0x000240D8
		public override void Dispose()
		{
			bool flag = this.temporaryStream_ != null;
			if (flag)
			{
				this.temporaryStream_.Close();
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00025F04 File Offset: 0x00024104
		private static string GetTempFileName(string original, bool makeTempFile)
		{
			string text = null;
			bool flag = original == null;
			if (flag)
			{
				text = Path.GetTempFileName();
			}
			else
			{
				int num = 0;
				int num2 = DateTime.Now.Second;
				while (text == null)
				{
					num++;
					string text2 = string.Format("{0}.{1}{2}.tmp", original, num2, num);
					bool flag2 = !File.Exists(text2);
					if (flag2)
					{
						if (makeTempFile)
						{
							try
							{
								using (File.Create(text2))
								{
								}
								text = text2;
							}
							catch
							{
								num2 = DateTime.Now.Second;
							}
						}
						else
						{
							text = text2;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0400039D RID: 925
		private Stream temporaryStream_;

		// Token: 0x0400039E RID: 926
		private string fileName_;

		// Token: 0x0400039F RID: 927
		private string temporaryName_;
	}
}
