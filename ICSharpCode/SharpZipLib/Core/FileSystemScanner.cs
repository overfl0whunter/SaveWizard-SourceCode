using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A1 RID: 161
	public class FileSystemScanner
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0002EA26 File Offset: 0x0002CC26
		public FileSystemScanner(string filter)
		{
			this.fileFilter_ = new PathFilter(filter);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002EA3C File Offset: 0x0002CC3C
		public FileSystemScanner(string fileFilter, string directoryFilter)
		{
			this.fileFilter_ = new PathFilter(fileFilter);
			this.directoryFilter_ = new PathFilter(directoryFilter);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002EA5E File Offset: 0x0002CC5E
		public FileSystemScanner(IScanFilter fileFilter)
		{
			this.fileFilter_ = fileFilter;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002EA6F File Offset: 0x0002CC6F
		public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.fileFilter_ = fileFilter;
			this.directoryFilter_ = directoryFilter;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002EA88 File Offset: 0x0002CC88
		private bool OnDirectoryFailure(string directory, Exception e)
		{
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			bool flag = directoryFailure != null;
			bool flag2 = flag;
			if (flag2)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		private bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			bool flag2 = flag;
			if (flag2)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				this.FileFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0002EB1C File Offset: 0x0002CD1C
		private void OnProcessFile(string file)
		{
			ProcessFileHandler processFile = this.ProcessFile;
			bool flag = processFile != null;
			if (flag)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0002EB58 File Offset: 0x0002CD58
		private void OnCompleteFile(string file)
		{
			CompletedFileHandler completedFile = this.CompletedFile;
			bool flag = completedFile != null;
			if (flag)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0002EB94 File Offset: 0x0002CD94
		private void OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
			bool flag = processDirectory != null;
			if (flag)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				this.alive_ = directoryEventArgs.ContinueRunning;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
		public void Scan(string directory, bool recurse)
		{
			this.alive_ = true;
			this.ScanDir(directory, recurse);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
		private void ScanDir(string directory, bool recurse)
		{
			try
			{
				string[] files = Directory.GetFiles(directory);
				bool flag = false;
				for (int i = 0; i < files.Length; i++)
				{
					bool flag2 = !this.fileFilter_.IsMatch(files[i]);
					if (flag2)
					{
						files[i] = null;
					}
					else
					{
						flag = true;
					}
				}
				this.OnProcessDirectory(directory, flag);
				bool flag3 = this.alive_ && flag;
				if (flag3)
				{
					foreach (string text in files)
					{
						try
						{
							bool flag4 = text != null;
							if (flag4)
							{
								this.OnProcessFile(text);
								bool flag5 = !this.alive_;
								if (flag5)
								{
									break;
								}
							}
						}
						catch (Exception ex)
						{
							bool flag6 = !this.OnFileFailure(text, ex);
							if (flag6)
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				bool flag7 = !this.OnDirectoryFailure(directory, ex2);
				if (flag7)
				{
					throw;
				}
			}
			bool flag8 = this.alive_ && recurse;
			if (flag8)
			{
				try
				{
					string[] directories = Directory.GetDirectories(directory);
					foreach (string text2 in directories)
					{
						bool flag9 = this.directoryFilter_ == null || this.directoryFilter_.IsMatch(text2);
						if (flag9)
						{
							this.ScanDir(text2, true);
							bool flag10 = !this.alive_;
							if (flag10)
							{
								break;
							}
						}
					}
				}
				catch (Exception ex3)
				{
					bool flag11 = !this.OnDirectoryFailure(directory, ex3);
					if (flag11)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x040004A0 RID: 1184
		public ProcessDirectoryHandler ProcessDirectory;

		// Token: 0x040004A1 RID: 1185
		public ProcessFileHandler ProcessFile;

		// Token: 0x040004A2 RID: 1186
		public CompletedFileHandler CompletedFile;

		// Token: 0x040004A3 RID: 1187
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x040004A4 RID: 1188
		public FileFailureHandler FileFailure;

		// Token: 0x040004A5 RID: 1189
		private IScanFilter fileFilter_;

		// Token: 0x040004A6 RID: 1190
		private IScanFilter directoryFilter_;

		// Token: 0x040004A7 RID: 1191
		private bool alive_;
	}
}
