using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200005A RID: 90
	public class FastZip
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0001E91F File Offset: 0x0001CB1F
		public FastZip()
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001E93B File Offset: 0x0001CB3B
		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001E960 File Offset: 0x0001CB60
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001E978 File Offset: 0x0001CB78
		public bool CreateEmptyDirectories
		{
			get
			{
				return this.createEmptyDirectories_;
			}
			set
			{
				this.createEmptyDirectories_ = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001E984 File Offset: 0x0001CB84
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001E99C File Offset: 0x0001CB9C
		public string Password
		{
			get
			{
				return this.password_;
			}
			set
			{
				this.password_ = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001E9C5 File Offset: 0x0001CBC5
		public INameTransform NameTransform
		{
			get
			{
				return this.entryFactory_.NameTransform;
			}
			set
			{
				this.entryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.entryFactory_;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.entryFactory_ = new ZipEntryFactory();
				}
				else
				{
					this.entryFactory_ = value;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001EA20 File Offset: 0x0001CC20
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0001EA44 File Offset: 0x0001CC44
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		public bool RestoreDateTimeOnExtract
		{
			get
			{
				return this.restoreDateTimeOnExtract_;
			}
			set
			{
				this.restoreDateTimeOnExtract_ = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001EA68 File Offset: 0x0001CC68
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0001EA80 File Offset: 0x0001CC80
		public bool RestoreAttributesOnExtract
		{
			get
			{
				return this.restoreAttributesOnExtract_;
			}
			set
			{
				this.restoreAttributesOnExtract_ = value;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001EA8A File Offset: 0x0001CC8A
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.NameTransform = new ZipNameTransform(sourceDirectory);
			this.sourceDirectory_ = sourceDirectory;
			using (this.outputStream_ = new ZipOutputStream(outputStream))
			{
				bool flag = this.password_ != null;
				if (flag)
				{
					this.outputStream_.Password = this.password_;
				}
				this.outputStream_.UseZip64 = this.UseZip64;
				FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
				FileSystemScanner fileSystemScanner2 = fileSystemScanner;
				fileSystemScanner2.ProcessFile = (ProcessFileHandler)Delegate.Combine(fileSystemScanner2.ProcessFile, new ProcessFileHandler(this.ProcessFile));
				bool createEmptyDirectories = this.CreateEmptyDirectories;
				if (createEmptyDirectories)
				{
					FileSystemScanner fileSystemScanner3 = fileSystemScanner;
					fileSystemScanner3.ProcessDirectory = (ProcessDirectoryHandler)Delegate.Combine(fileSystemScanner3.ProcessDirectory, new ProcessDirectoryHandler(this.ProcessDirectory));
				}
				bool flag2 = this.events_ != null;
				if (flag2)
				{
					bool flag3 = this.events_.FileFailure != null;
					if (flag3)
					{
						FileSystemScanner fileSystemScanner4 = fileSystemScanner;
						fileSystemScanner4.FileFailure = (FileFailureHandler)Delegate.Combine(fileSystemScanner4.FileFailure, this.events_.FileFailure);
					}
					bool flag4 = this.events_.DirectoryFailure != null;
					if (flag4)
					{
						FileSystemScanner fileSystemScanner5 = fileSystemScanner;
						fileSystemScanner5.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(fileSystemScanner5.DirectoryFailure, this.events_.DirectoryFailure);
					}
				}
				fileSystemScanner.Scan(sourceDirectory, recurse);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001EC44 File Offset: 0x0001CE44
		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
		{
			Stream stream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.ExtractZip(stream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001EC70 File Offset: 0x0001CE70
		public void ExtractZip(Stream inputStream, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner)
		{
			bool flag = overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null;
			if (flag)
			{
				throw new ArgumentNullException("confirmDelegate");
			}
			this.continueRunning_ = true;
			this.overwrite_ = overwrite;
			this.confirmDelegate_ = confirmDelegate;
			this.extractNameTransform_ = new WindowsNameTransform(targetDirectory);
			this.fileFilter_ = new NameFilter(fileFilter);
			this.directoryFilter_ = new NameFilter(directoryFilter);
			this.restoreDateTimeOnExtract_ = restoreDateTime;
			using (this.zipFile_ = new ZipFile(inputStream))
			{
				bool flag2 = this.password_ != null;
				if (flag2)
				{
					this.zipFile_.Password = this.password_;
				}
				this.zipFile_.IsStreamOwner = isStreamOwner;
				IEnumerator enumerator = this.zipFile_.GetEnumerator();
				while (this.continueRunning_ && enumerator.MoveNext())
				{
					ZipEntry zipEntry = (ZipEntry)enumerator.Current;
					bool isFile = zipEntry.IsFile;
					if (isFile)
					{
						bool flag3 = this.directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && this.fileFilter_.IsMatch(zipEntry.Name);
						if (flag3)
						{
							this.ExtractEntry(zipEntry);
						}
					}
					else
					{
						bool isDirectory = zipEntry.IsDirectory;
						if (isDirectory)
						{
							bool flag4 = this.directoryFilter_.IsMatch(zipEntry.Name) && this.CreateEmptyDirectories;
							if (flag4)
							{
								this.ExtractEntry(zipEntry);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001EE14 File Offset: 0x0001D014
		private void ProcessDirectory(object sender, DirectoryEventArgs e)
		{
			bool flag = !e.HasMatchingFiles && this.CreateEmptyDirectories;
			if (flag)
			{
				bool flag2 = this.events_ != null;
				if (flag2)
				{
					this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
				}
				bool continueRunning = e.ContinueRunning;
				if (continueRunning)
				{
					bool flag3 = e.Name != this.sourceDirectory_;
					if (flag3)
					{
						ZipEntry zipEntry = this.entryFactory_.MakeDirectoryEntry(e.Name);
						this.outputStream_.PutNextEntry(zipEntry);
					}
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		private void ProcessFile(object sender, ScanEventArgs e)
		{
			bool flag = this.events_ != null && this.events_.ProcessFile != null;
			if (flag)
			{
				this.events_.ProcessFile(sender, e);
			}
			bool continueRunning = e.ContinueRunning;
			if (continueRunning)
			{
				try
				{
					using (FileStream fileStream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						ZipEntry zipEntry = this.entryFactory_.MakeFileEntry(e.Name);
						this.outputStream_.PutNextEntry(zipEntry);
						this.AddFileContents(e.Name, fileStream);
					}
				}
				catch (Exception ex)
				{
					bool flag2 = this.events_ != null;
					if (!flag2)
					{
						this.continueRunning_ = false;
						throw;
					}
					this.continueRunning_ = this.events_.OnFileFailure(e.Name, ex);
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001EF9C File Offset: 0x0001D19C
		private void AddFileContents(string name, Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag2 = this.buffer_ == null;
			if (flag2)
			{
				this.buffer_ = new byte[4096];
			}
			bool flag3 = this.events_ != null && this.events_.Progress != null;
			if (flag3)
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, name);
			}
			else
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_);
			}
			bool flag4 = this.events_ != null;
			if (flag4)
			{
				this.continueRunning_ = this.events_.OnCompletedFile(name);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001F060 File Offset: 0x0001D260
		private void ExtractFileEntry(ZipEntry entry, string targetName)
		{
			bool flag = true;
			bool flag2 = this.overwrite_ != FastZip.Overwrite.Always;
			if (flag2)
			{
				bool flag3 = File.Exists(targetName);
				if (flag3)
				{
					bool flag4 = this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null;
					flag = flag4 && this.confirmDelegate_(targetName);
				}
			}
			bool flag5 = flag;
			if (flag5)
			{
				bool flag6 = this.events_ != null;
				if (flag6)
				{
					this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
				}
				bool flag7 = this.continueRunning_;
				if (flag7)
				{
					try
					{
						using (FileStream fileStream = File.Create(targetName))
						{
							bool flag8 = this.buffer_ == null;
							if (flag8)
							{
								this.buffer_ = new byte[4096];
							}
							bool flag9 = this.events_ != null && this.events_.Progress != null;
							if (flag9)
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, entry.Name, entry.Size);
							}
							else
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_);
							}
							bool flag10 = this.events_ != null;
							if (flag10)
							{
								this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
							}
						}
						bool flag11 = this.restoreDateTimeOnExtract_;
						if (flag11)
						{
							File.SetLastWriteTime(targetName, entry.DateTime);
						}
						bool flag12 = this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1;
						if (flag12)
						{
							FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
							fileAttributes &= FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal;
							File.SetAttributes(targetName, fileAttributes);
						}
					}
					catch (Exception ex)
					{
						bool flag13 = this.events_ != null;
						if (!flag13)
						{
							this.continueRunning_ = false;
							throw;
						}
						this.continueRunning_ = this.events_.OnFileFailure(targetName, ex);
					}
				}
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		private void ExtractEntry(ZipEntry entry)
		{
			bool flag = entry.IsCompressionMethodSupported();
			string text = entry.Name;
			bool flag2 = flag;
			if (flag2)
			{
				bool isFile = entry.IsFile;
				if (isFile)
				{
					text = this.extractNameTransform_.TransformFile(text);
				}
				else
				{
					bool isDirectory = entry.IsDirectory;
					if (isDirectory)
					{
						text = this.extractNameTransform_.TransformDirectory(text);
					}
				}
				flag = text != null && text.Length != 0;
			}
			string text2 = null;
			bool flag3 = flag;
			if (flag3)
			{
				bool isDirectory2 = entry.IsDirectory;
				if (isDirectory2)
				{
					text2 = text;
				}
				else
				{
					text2 = Path.GetDirectoryName(Path.GetFullPath(text));
				}
			}
			bool flag4 = flag && !Directory.Exists(text2);
			if (flag4)
			{
				bool flag5 = !entry.IsDirectory || this.CreateEmptyDirectories;
				if (flag5)
				{
					try
					{
						Directory.CreateDirectory(text2);
					}
					catch (Exception ex)
					{
						flag = false;
						bool flag6 = this.events_ != null;
						if (!flag6)
						{
							this.continueRunning_ = false;
							throw;
						}
						bool isDirectory3 = entry.IsDirectory;
						if (isDirectory3)
						{
							this.continueRunning_ = this.events_.OnDirectoryFailure(text, ex);
						}
						else
						{
							this.continueRunning_ = this.events_.OnFileFailure(text, ex);
						}
					}
				}
			}
			bool flag7 = flag && entry.IsFile;
			if (flag7)
			{
				this.ExtractFileEntry(entry, text);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001F40C File Offset: 0x0001D60C
		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001F424 File Offset: 0x0001D624
		private static bool NameIsValid(string name)
		{
			return name != null && name.Length > 0 && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x040002CE RID: 718
		private bool continueRunning_;

		// Token: 0x040002CF RID: 719
		private byte[] buffer_;

		// Token: 0x040002D0 RID: 720
		private ZipOutputStream outputStream_;

		// Token: 0x040002D1 RID: 721
		private ZipFile zipFile_;

		// Token: 0x040002D2 RID: 722
		private string sourceDirectory_;

		// Token: 0x040002D3 RID: 723
		private NameFilter fileFilter_;

		// Token: 0x040002D4 RID: 724
		private NameFilter directoryFilter_;

		// Token: 0x040002D5 RID: 725
		private FastZip.Overwrite overwrite_;

		// Token: 0x040002D6 RID: 726
		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		// Token: 0x040002D7 RID: 727
		private bool restoreDateTimeOnExtract_;

		// Token: 0x040002D8 RID: 728
		private bool restoreAttributesOnExtract_;

		// Token: 0x040002D9 RID: 729
		private bool createEmptyDirectories_;

		// Token: 0x040002DA RID: 730
		private FastZipEvents events_;

		// Token: 0x040002DB RID: 731
		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		// Token: 0x040002DC RID: 732
		private INameTransform extractNameTransform_;

		// Token: 0x040002DD RID: 733
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x040002DE RID: 734
		private string password_;

		// Token: 0x02000208 RID: 520
		public enum Overwrite
		{
			// Token: 0x04000DA8 RID: 3496
			Prompt,
			// Token: 0x04000DA9 RID: 3497
			Never,
			// Token: 0x04000DAA RID: 3498
			Always
		}

		// Token: 0x02000209 RID: 521
		// (Invoke) Token: 0x06001C08 RID: 7176
		public delegate bool ConfirmOverwriteDelegate(string fileName);
	}
}
