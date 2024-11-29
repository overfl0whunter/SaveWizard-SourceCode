using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace PS3SaveEditor
{
	// Token: 0x020001EB RID: 491
	internal class ZipUtil
	{
		// Token: 0x060019F4 RID: 6644 RVA: 0x000A48F4 File Offset: 0x000A2AF4
		public static string GetAsZipFile(string[] filePaths, ZipUtil.OnZipProgress onProgress)
		{
			string tempFileName = Path.GetTempFileName();
			File.Delete(tempFileName);
			ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(tempFileName));
			zipOutputStream.UseZip64 = UseZip64.Off;
			byte[] array = new byte[4096];
			ProgressHandler <>9__0;
			foreach (string text in filePaths)
			{
				FileStream fileStream = File.OpenRead(text);
				try
				{
					ZipEntry zipEntry = new ZipEntry(Path.GetFileName(text));
					zipEntry.DosTime = 0L;
					zipEntry.DateTime = DateTime.MinValue;
					zipOutputStream.PutNextEntry(zipEntry);
					bool flag = onProgress != null;
					if (flag)
					{
						Stream stream = fileStream;
						Stream stream2 = zipOutputStream;
						byte[] array2 = array;
						ProgressHandler progressHandler;
						if ((progressHandler = <>9__0) == null)
						{
							progressHandler = (<>9__0 = delegate(object snder, ProgressEventArgs e)
							{
								e.ContinueRunning = onProgress((int)e.PercentComplete);
							});
						}
						StreamUtils.Copy(stream, stream2, array2, progressHandler, TimeSpan.FromSeconds(1.0), null, "");
					}
					else
					{
						StreamUtils.Copy(fileStream, zipOutputStream, array);
					}
					bool flag2 = zipEntry.CompressedSize == 0L;
					if (flag2)
					{
						break;
					}
				}
				finally
				{
					fileStream.Close();
				}
			}
			zipOutputStream.Finish();
			zipOutputStream.Close();
			return tempFileName;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000A4A40 File Offset: 0x000A2C40
		public static string GetAsZipFile(string[] filePaths, string profile, ZipUtil.OnZipProgress onProgress)
		{
			string tempFileName = Path.GetTempFileName();
			File.Delete(tempFileName);
			ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(tempFileName));
			zipOutputStream.UseZip64 = UseZip64.Off;
			byte[] array = new byte[4096];
			int num = 0;
			ProgressHandler <>9__0;
			foreach (string text in filePaths)
			{
				bool flag = !File.Exists(text);
				if (!flag)
				{
					FileStream fileStream = File.OpenRead(text);
					string fileName = Path.GetFileName(text);
					try
					{
						bool flag2 = fileName.ToUpper() == "PARAM.SFO" && profile != "None";
						if (flag2)
						{
							string tempFileName2 = Path.GetTempFileName();
							File.Delete(tempFileName2);
							File.Copy(text, tempFileName2);
							fileStream.Close();
							fileStream = File.OpenRead(tempFileName2);
						}
						ZipEntry zipEntry = new ZipEntry(fileName);
						zipOutputStream.PutNextEntry(zipEntry);
						bool flag3 = fileStream.Length > 1000000L;
						if (flag3)
						{
							Stream stream = fileStream;
							Stream stream2 = zipOutputStream;
							byte[] array2 = array;
							ProgressHandler progressHandler;
							if ((progressHandler = <>9__0) == null)
							{
								progressHandler = (<>9__0 = delegate(object snder, ProgressEventArgs e)
								{
									e.ContinueRunning = onProgress((int)e.PercentComplete);
								});
							}
							StreamUtils.Copy(stream, stream2, array2, progressHandler, TimeSpan.FromSeconds(1.0), null, "");
						}
						else
						{
							StreamUtils.Copy(fileStream, zipOutputStream, array);
						}
						onProgress(num * 100 / filePaths.Length);
					}
					finally
					{
						fileStream.Close();
						bool flag4 = fileName.ToUpper() == "PARAM.SFO" && profile != "None";
						if (flag4)
						{
							File.SetAttributes(fileStream.Name, FileAttributes.Normal);
							File.Delete(fileStream.Name);
						}
					}
					num++;
				}
			}
			zipOutputStream.Finish();
			zipOutputStream.Close();
			return tempFileName;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000A4C34 File Offset: 0x000A2E34
		public static string GetPs3SeTempFolder()
		{
			string tempFolder = Util.GetTempFolder();
			bool flag = !Directory.Exists(tempFolder);
			if (flag)
			{
				Directory.CreateDirectory(tempFolder);
			}
			return tempFolder;
		}

		// Token: 0x020002CD RID: 717
		// (Invoke) Token: 0x06001EC4 RID: 7876
		public delegate bool OnZipProgress(int progress);
	}
}
