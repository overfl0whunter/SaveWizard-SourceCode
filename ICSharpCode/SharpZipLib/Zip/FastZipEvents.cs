using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000059 RID: 89
	public class FastZipEvents
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
		public bool OnDirectoryFailure(string directory, Exception e)
		{
			bool flag = false;
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			bool flag2 = directoryFailure != null;
			if (flag2)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		public bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			bool flag2 = flag;
			if (flag2)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				fileFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001E820 File Offset: 0x0001CA20
		public bool OnProcessFile(string file)
		{
			bool flag = true;
			ProcessFileHandler processFile = this.ProcessFile;
			bool flag2 = processFile != null;
			if (flag2)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				flag = scanEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001E860 File Offset: 0x0001CA60
		public bool OnCompletedFile(string file)
		{
			bool flag = true;
			CompletedFileHandler completedFile = this.CompletedFile;
			bool flag2 = completedFile != null;
			if (flag2)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				flag = scanEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			bool flag = true;
			ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
			bool flag2 = processDirectory != null;
			if (flag2)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				flag = directoryEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		public TimeSpan ProgressInterval
		{
			get
			{
				return this.progressInterval_;
			}
			set
			{
				this.progressInterval_ = value;
			}
		}

		// Token: 0x040002C7 RID: 711
		public ProcessDirectoryHandler ProcessDirectory;

		// Token: 0x040002C8 RID: 712
		public ProcessFileHandler ProcessFile;

		// Token: 0x040002C9 RID: 713
		public ProgressHandler Progress;

		// Token: 0x040002CA RID: 714
		public CompletedFileHandler CompletedFile;

		// Token: 0x040002CB RID: 715
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x040002CC RID: 716
		public FileFailureHandler FileFailure;

		// Token: 0x040002CD RID: 717
		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);
	}
}
