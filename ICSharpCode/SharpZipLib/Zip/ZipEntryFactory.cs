using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000064 RID: 100
	public class ZipEntryFactory : IEntryFactory
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00020A1E File Offset: 0x0001EC1E
		public ZipEntryFactory()
		{
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00020A45 File Offset: 0x0001EC45
		public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
		{
			this.timeSetting_ = timeSetting;
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00020A73 File Offset: 0x0001EC73
		public ZipEntryFactory(DateTime time)
		{
			this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
			this.FixedDateTime = time;
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00020AAC File Offset: 0x0001ECAC
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public INameTransform NameTransform
		{
			get
			{
				return this.nameTransform_;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.nameTransform_ = new ZipNameTransform();
				}
				else
				{
					this.nameTransform_ = value;
				}
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public ZipEntryFactory.TimeSetting Setting
		{
			get
			{
				return this.timeSetting_;
			}
			set
			{
				this.timeSetting_ = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00020B18 File Offset: 0x0001ED18
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00020B30 File Offset: 0x0001ED30
		public DateTime FixedDateTime
		{
			get
			{
				return this.fixedDateTime_;
			}
			set
			{
				bool flag = value.Year < 1970;
				if (flag)
				{
					throw new ArgumentException("Value is too old to be valid", "value");
				}
				this.fixedDateTime_ = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00020B68 File Offset: 0x0001ED68
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00020B80 File Offset: 0x0001ED80
		public int GetAttributes
		{
			get
			{
				return this.getAttributes_;
			}
			set
			{
				this.getAttributes_ = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00020B8C File Offset: 0x0001ED8C
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00020BA4 File Offset: 0x0001EDA4
		public int SetAttributes
		{
			get
			{
				return this.setAttributes_;
			}
			set
			{
				this.setAttributes_ = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		public bool IsUnicodeText
		{
			get
			{
				return this.isUnicodeText_;
			}
			set
			{
				this.isUnicodeText_ = value;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		public ZipEntry MakeFileEntry(string fileName)
		{
			return this.MakeFileEntry(fileName, true);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00020BF0 File Offset: 0x0001EDF0
		public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile(fileName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			int num = 0;
			bool flag = this.setAttributes_ != 0;
			FileInfo fileInfo = null;
			if (useFileSystem)
			{
				fileInfo = new FileInfo(fileName);
			}
			bool flag2 = fileInfo != null && fileInfo.Exists;
			if (flag2)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = fileInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = fileInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = fileInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = fileInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = fileInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = fileInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeFileEntry");
				}
				zipEntry.Size = fileInfo.Length;
				flag = true;
				num = (int)(fileInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else
			{
				bool flag3 = this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed;
				if (flag3)
				{
					zipEntry.DateTime = this.fixedDateTime_;
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				num |= this.setAttributes_;
				zipEntry.ExternalFileAttributes = num;
			}
			return zipEntry;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00020D54 File Offset: 0x0001EF54
		public ZipEntry MakeDirectoryEntry(string directoryName)
		{
			return this.MakeDirectoryEntry(directoryName, true);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00020D70 File Offset: 0x0001EF70
		public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			zipEntry.Size = 0L;
			int num = 0;
			DirectoryInfo directoryInfo = null;
			if (useFileSystem)
			{
				directoryInfo = new DirectoryInfo(directoryName);
			}
			bool flag = directoryInfo != null && directoryInfo.Exists;
			if (flag)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = directoryInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = directoryInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = directoryInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = directoryInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = directoryInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = directoryInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
				}
				num = (int)(directoryInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else
			{
				bool flag2 = this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed;
				if (flag2)
				{
					zipEntry.DateTime = this.fixedDateTime_;
				}
			}
			num |= this.setAttributes_ | 16;
			zipEntry.ExternalFileAttributes = num;
			return zipEntry;
		}

		// Token: 0x0400035A RID: 858
		private INameTransform nameTransform_;

		// Token: 0x0400035B RID: 859
		private DateTime fixedDateTime_ = DateTime.Now;

		// Token: 0x0400035C RID: 860
		private ZipEntryFactory.TimeSetting timeSetting_;

		// Token: 0x0400035D RID: 861
		private bool isUnicodeText_;

		// Token: 0x0400035E RID: 862
		private int getAttributes_ = -1;

		// Token: 0x0400035F RID: 863
		private int setAttributes_;

		// Token: 0x0200020B RID: 523
		public enum TimeSetting
		{
			// Token: 0x04000DB3 RID: 3507
			LastWriteTime,
			// Token: 0x04000DB4 RID: 3508
			LastWriteTimeUtc,
			// Token: 0x04000DB5 RID: 3509
			CreateTime,
			// Token: 0x04000DB6 RID: 3510
			CreateTimeUtc,
			// Token: 0x04000DB7 RID: 3511
			LastAccessTime,
			// Token: 0x04000DB8 RID: 3512
			LastAccessTimeUtc,
			// Token: 0x04000DB9 RID: 3513
			Fixed
		}
	}
}
