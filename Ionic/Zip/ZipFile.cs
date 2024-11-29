using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zlib;
using Microsoft.CSharp;

namespace Ionic.Zip
{
	// Token: 0x0200004C RID: 76
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ZipFile : IEnumerable, IEnumerable<ZipEntry>, IDisposable
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00016E08 File Offset: 0x00015008
		public ZipEntry AddItem(string fileOrDirectoryName)
		{
			return this.AddItem(fileOrDirectoryName, null);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00016E24 File Offset: 0x00015024
		public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
		{
			bool flag = File.Exists(fileOrDirectoryName);
			ZipEntry zipEntry;
			if (flag)
			{
				zipEntry = this.AddFile(fileOrDirectoryName, directoryPathInArchive);
			}
			else
			{
				bool flag2 = Directory.Exists(fileOrDirectoryName);
				if (!flag2)
				{
					throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", fileOrDirectoryName));
				}
				zipEntry = this.AddDirectory(fileOrDirectoryName, directoryPathInArchive);
			}
			return zipEntry;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00016E70 File Offset: 0x00015070
		public ZipEntry AddFile(string fileName)
		{
			return this.AddFile(fileName, null);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00016E8C File Offset: 0x0001508C
		public ZipEntry AddFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			ZipEntry zipEntry = ZipEntry.CreateFromFile(fileName, text);
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00016ED4 File Offset: 0x000150D4
		public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
		{
			bool flag = entriesToRemove == null;
			if (flag)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (ZipEntry zipEntry in entriesToRemove)
			{
				this.RemoveEntry(zipEntry);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00016F38 File Offset: 0x00015138
		public void RemoveEntries(ICollection<string> entriesToRemove)
		{
			bool flag = entriesToRemove == null;
			if (flag)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (string text in entriesToRemove)
			{
				this.RemoveEntry(text);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00016F9C File Offset: 0x0001519C
		public void AddFiles(IEnumerable<string> fileNames)
		{
			this.AddFiles(fileNames, null);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00016FA8 File Offset: 0x000151A8
		public void UpdateFiles(IEnumerable<string> fileNames)
		{
			this.UpdateFiles(fileNames, null);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00016FB4 File Offset: 0x000151B4
		public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			this.AddFiles(fileNames, false, directoryPathInArchive);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00016FC4 File Offset: 0x000151C4
		public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
		{
			bool flag = fileNames == null;
			if (flag)
			{
				throw new ArgumentNullException("fileNames");
			}
			this._addOperationCanceled = false;
			this.OnAddStarted();
			if (preserveDirHierarchy)
			{
				foreach (string text in fileNames)
				{
					bool addOperationCanceled = this._addOperationCanceled;
					if (addOperationCanceled)
					{
						break;
					}
					bool flag2 = directoryPathInArchive != null;
					if (flag2)
					{
						string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(text)));
						this.AddFile(text, fullPath);
					}
					else
					{
						this.AddFile(text, null);
					}
				}
			}
			else
			{
				foreach (string text2 in fileNames)
				{
					bool addOperationCanceled2 = this._addOperationCanceled;
					if (addOperationCanceled2)
					{
						break;
					}
					this.AddFile(text2, directoryPathInArchive);
				}
			}
			bool flag3 = !this._addOperationCanceled;
			if (flag3)
			{
				this.OnAddCompleted();
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000170E0 File Offset: 0x000152E0
		public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			bool flag = fileNames == null;
			if (flag)
			{
				throw new ArgumentNullException("fileNames");
			}
			this.OnAddStarted();
			foreach (string text in fileNames)
			{
				this.UpdateFile(text, directoryPathInArchive);
			}
			this.OnAddCompleted();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00017150 File Offset: 0x00015350
		public ZipEntry UpdateFile(string fileName)
		{
			return this.UpdateFile(fileName, null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001716C File Offset: 0x0001536C
		public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			bool flag = this[text] != null;
			if (flag)
			{
				this.RemoveEntry(text);
			}
			return this.AddFile(fileName, directoryPathInArchive);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000171A4 File Offset: 0x000153A4
		public ZipEntry UpdateDirectory(string directoryName)
		{
			return this.UpdateDirectory(directoryName, null);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000171C0 File Offset: 0x000153C0
		public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOrUpdate);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000171DB File Offset: 0x000153DB
		public void UpdateItem(string itemName)
		{
			this.UpdateItem(itemName, null);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000171E8 File Offset: 0x000153E8
		public void UpdateItem(string itemName, string directoryPathInArchive)
		{
			bool flag = File.Exists(itemName);
			if (flag)
			{
				this.UpdateFile(itemName, directoryPathInArchive);
			}
			else
			{
				bool flag2 = Directory.Exists(itemName);
				if (!flag2)
				{
					throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", itemName));
				}
				this.UpdateDirectory(itemName, directoryPathInArchive);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00017234 File Offset: 0x00015434
		public ZipEntry AddEntry(string entryName, string content)
		{
			return this.AddEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00017254 File Offset: 0x00015454
		public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00017298 File Offset: 0x00015498
		public ZipEntry AddEntry(string entryName, Stream stream)
		{
			ZipEntry zipEntry = ZipEntry.CreateForStream(entryName, stream);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000172EC File Offset: 0x000154EC
		public ZipEntry AddEntry(string entryName, WriteDelegate writer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForWriter(entryName, writer);
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001732C File Offset: 0x0001552C
		public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForJitStreamProvider(entryName, opener, closer);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00017380 File Offset: 0x00015580
		private ZipEntry _InternalAddEntry(ZipEntry ze)
		{
			ze._container = new ZipContainer(this);
			ze.CompressionMethod = this.CompressionMethod;
			ze.CompressionLevel = this.CompressionLevel;
			ze.ExtractExistingFile = this.ExtractExistingFile;
			ze.ZipErrorAction = this.ZipErrorAction;
			ze.SetCompression = this.SetCompression;
			ze.AlternateEncoding = this.AlternateEncoding;
			ze.AlternateEncodingUsage = this.AlternateEncodingUsage;
			ze.Password = this._Password;
			ze.Encryption = this.Encryption;
			ze.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			ze.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			this.InternalAddEntry(ze.FileName, ze);
			this.AfterAddEntry(ze);
			return ze;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00017444 File Offset: 0x00015644
		public ZipEntry UpdateEntry(string entryName, string content)
		{
			return this.UpdateEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00017464 File Offset: 0x00015664
		public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, content, encoding);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00017488 File Offset: 0x00015688
		public ZipEntry UpdateEntry(string entryName, WriteDelegate writer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, writer);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000174AC File Offset: 0x000156AC
		public ZipEntry UpdateEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, opener, closer);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000174D0 File Offset: 0x000156D0
		public ZipEntry UpdateEntry(string entryName, Stream stream)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, stream);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000174F4 File Offset: 0x000156F4
		private void RemoveEntryForUpdate(string entryName)
		{
			bool flag = string.IsNullOrEmpty(entryName);
			if (flag)
			{
				throw new ArgumentNullException("entryName");
			}
			string text = null;
			bool flag2 = entryName.IndexOf('\\') != -1;
			if (flag2)
			{
				text = Path.GetDirectoryName(entryName);
				entryName = Path.GetFileName(entryName);
			}
			string text2 = ZipEntry.NameInArchive(entryName, text);
			bool flag3 = this[text2] != null;
			if (flag3)
			{
				this.RemoveEntry(text2);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001755C File Offset: 0x0001575C
		public ZipEntry AddEntry(string entryName, byte[] byteContent)
		{
			bool flag = byteContent == null;
			if (flag)
			{
				throw new ArgumentException("bad argument", "byteContent");
			}
			MemoryStream memoryStream = new MemoryStream(byteContent);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00017598 File Offset: 0x00015798
		public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, byteContent);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000175BC File Offset: 0x000157BC
		public ZipEntry AddDirectory(string directoryName)
		{
			return this.AddDirectory(directoryName, null);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000175D8 File Offset: 0x000157D8
		public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOnly);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000175F4 File Offset: 0x000157F4
		public ZipEntry AddDirectoryByName(string directoryNameInArchive)
		{
			ZipEntry zipEntry = ZipEntry.CreateFromNothing(directoryNameInArchive);
			zipEntry._container = new ZipContainer(this);
			zipEntry.MarkAsDirectory();
			zipEntry.AlternateEncoding = this.AlternateEncoding;
			zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			zipEntry._Source = ZipEntrySource.Stream;
			this.InternalAddEntry(zipEntry.FileName, zipEntry);
			this.AfterAddEntry(zipEntry);
			return zipEntry;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00017688 File Offset: 0x00015888
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action)
		{
			bool flag = rootDirectoryPathInArchive == null;
			if (flag)
			{
				rootDirectoryPathInArchive = "";
			}
			return this.AddOrUpdateDirectoryImpl(directoryName, rootDirectoryPathInArchive, action, true, 0);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000176B6 File Offset: 0x000158B6
		internal void InternalAddEntry(string name, ZipEntry entry)
		{
			this._entries.Add(name, entry);
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000176D8 File Offset: 0x000158D8
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action, bool recurse, int level)
		{
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("{0} {1}...", (action == AddOrUpdateAction.AddOnly) ? "adding" : "Adding or updating", directoryName);
			}
			bool flag = level == 0;
			if (flag)
			{
				this._addOperationCanceled = false;
				this.OnAddStarted();
			}
			bool addOperationCanceled = this._addOperationCanceled;
			ZipEntry zipEntry;
			if (addOperationCanceled)
			{
				zipEntry = null;
			}
			else
			{
				string text = rootDirectoryPathInArchive;
				ZipEntry zipEntry2 = null;
				bool flag2 = level > 0;
				if (flag2)
				{
					int num = directoryName.Length;
					for (int i = level; i > 0; i--)
					{
						num = directoryName.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
					}
					text = directoryName.Substring(num + 1);
					text = Path.Combine(rootDirectoryPathInArchive, text);
				}
				bool flag3 = level > 0 || rootDirectoryPathInArchive != "";
				if (flag3)
				{
					zipEntry2 = ZipEntry.CreateFromFile(directoryName, text);
					zipEntry2._container = new ZipContainer(this);
					zipEntry2.AlternateEncoding = this.AlternateEncoding;
					zipEntry2.AlternateEncodingUsage = this.AlternateEncodingUsage;
					zipEntry2.MarkAsDirectory();
					zipEntry2.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
					zipEntry2.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
					bool flag4 = !this._entries.ContainsKey(zipEntry2.FileName);
					if (flag4)
					{
						this.InternalAddEntry(zipEntry2.FileName, zipEntry2);
						this.AfterAddEntry(zipEntry2);
					}
					text = zipEntry2.FileName;
				}
				bool flag5 = !this._addOperationCanceled;
				if (flag5)
				{
					string[] files = Directory.GetFiles(directoryName);
					if (recurse)
					{
						foreach (string text2 in files)
						{
							bool addOperationCanceled2 = this._addOperationCanceled;
							if (addOperationCanceled2)
							{
								break;
							}
							bool flag6 = action == AddOrUpdateAction.AddOnly;
							if (flag6)
							{
								this.AddFile(text2, text);
							}
							else
							{
								this.UpdateFile(text2, text);
							}
						}
						bool flag7 = !this._addOperationCanceled;
						if (flag7)
						{
							string[] directories = Directory.GetDirectories(directoryName);
							foreach (string text3 in directories)
							{
								FileAttributes attributes = File.GetAttributes(text3);
								bool flag8 = this.AddDirectoryWillTraverseReparsePoints || (attributes & FileAttributes.ReparsePoint) == (FileAttributes)0;
								if (flag8)
								{
									this.AddOrUpdateDirectoryImpl(text3, rootDirectoryPathInArchive, action, recurse, level + 1);
								}
							}
						}
					}
				}
				bool flag9 = level == 0;
				if (flag9)
				{
					this.OnAddCompleted();
				}
				zipEntry = zipEntry2;
			}
			return zipEntry;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017944 File Offset: 0x00015B44
		public static bool CheckZip(string zipFileName)
		{
			return ZipFile.CheckZip(zipFileName, false, null);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00017960 File Offset: 0x00015B60
		public static bool CheckZip(string zipFileName, bool fixIfNecessary, TextWriter writer)
		{
			ZipFile zipFile = null;
			ZipFile zipFile2 = null;
			bool flag = true;
			try
			{
				zipFile = new ZipFile();
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile2 = ZipFile.Read(zipFileName);
				foreach (ZipEntry zipEntry in zipFile)
				{
					foreach (ZipEntry zipEntry2 in zipFile2)
					{
						bool flag2 = zipEntry.FileName == zipEntry2.FileName;
						if (flag2)
						{
							bool flag3 = zipEntry._RelativeOffsetOfLocalHeader != zipEntry2._RelativeOffsetOfLocalHeader;
							if (flag3)
							{
								flag = false;
								bool flag4 = writer != null;
								if (flag4)
								{
									writer.WriteLine("{0}: mismatch in RelativeOffsetOfLocalHeader  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._RelativeOffsetOfLocalHeader, zipEntry2._RelativeOffsetOfLocalHeader);
								}
							}
							bool flag5 = zipEntry._CompressedSize != zipEntry2._CompressedSize;
							if (flag5)
							{
								flag = false;
								bool flag6 = writer != null;
								if (flag6)
								{
									writer.WriteLine("{0}: mismatch in CompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._CompressedSize, zipEntry2._CompressedSize);
								}
							}
							bool flag7 = zipEntry._UncompressedSize != zipEntry2._UncompressedSize;
							if (flag7)
							{
								flag = false;
								bool flag8 = writer != null;
								if (flag8)
								{
									writer.WriteLine("{0}: mismatch in UncompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._UncompressedSize, zipEntry2._UncompressedSize);
								}
							}
							bool flag9 = zipEntry.CompressionMethod != zipEntry2.CompressionMethod;
							if (flag9)
							{
								flag = false;
								bool flag10 = writer != null;
								if (flag10)
								{
									writer.WriteLine("{0}: mismatch in CompressionMethod  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.CompressionMethod, zipEntry2.CompressionMethod);
								}
							}
							bool flag11 = zipEntry.Crc != zipEntry2.Crc;
							if (flag11)
							{
								flag = false;
								bool flag12 = writer != null;
								if (flag12)
								{
									writer.WriteLine("{0}: mismatch in Crc32  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.Crc, zipEntry2.Crc);
								}
							}
							break;
						}
					}
				}
				zipFile2.Dispose();
				zipFile2 = null;
				bool flag13 = !flag && fixIfNecessary;
				if (flag13)
				{
					string text = Path.GetFileNameWithoutExtension(zipFileName);
					text = string.Format("{0}_fixed.zip", text);
					zipFile.Save(text);
				}
			}
			finally
			{
				bool flag14 = zipFile != null;
				if (flag14)
				{
					zipFile.Dispose();
				}
				bool flag15 = zipFile2 != null;
				if (flag15)
				{
					zipFile2.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00017C60 File Offset: 0x00015E60
		public static void FixZipDirectory(string zipFileName)
		{
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile.Save(zipFileName);
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00017CAC File Offset: 0x00015EAC
		public static bool CheckZipPassword(string zipFileName, string password)
		{
			bool flag = false;
			try
			{
				using (ZipFile zipFile = ZipFile.Read(zipFileName))
				{
					foreach (ZipEntry zipEntry in zipFile)
					{
						bool flag2 = !zipEntry.IsDirectory && zipEntry.UsesEncryption;
						if (flag2)
						{
							zipEntry.ExtractWithPassword(Stream.Null, password);
						}
					}
				}
				flag = true;
			}
			catch (BadPasswordException)
			{
			}
			return flag;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00017D5C File Offset: 0x00015F5C
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipFile: {0}\n", this.Name));
				bool flag = !string.IsNullOrEmpty(this._Comment);
				if (flag)
				{
					stringBuilder.Append(string.Format("          Comment: {0}\n", this._Comment));
				}
				bool flag2 = this._versionMadeBy > 0;
				if (flag2)
				{
					stringBuilder.Append(string.Format("  version made by: 0x{0:X4}\n", this._versionMadeBy));
				}
				bool flag3 = this._versionNeededToExtract > 0;
				if (flag3)
				{
					stringBuilder.Append(string.Format("needed to extract: 0x{0:X4}\n", this._versionNeededToExtract));
				}
				stringBuilder.Append(string.Format("       uses ZIP64: {0}\n", this.InputUsesZip64));
				stringBuilder.Append(string.Format("     disk with CD: {0}\n", this._diskNumberWithCd));
				bool flag4 = this._OffsetOfCentralDirectory == uint.MaxValue;
				if (flag4)
				{
					stringBuilder.Append(string.Format("      CD64 offset: 0x{0:X16}\n", this._OffsetOfCentralDirectory64));
				}
				else
				{
					stringBuilder.Append(string.Format("        CD offset: 0x{0:X8}\n", this._OffsetOfCentralDirectory));
				}
				stringBuilder.Append("\n");
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					stringBuilder.Append(zipEntry.Info);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00017EFC File Offset: 0x000160FC
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00017F04 File Offset: 0x00016104
		public bool FullScan { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00017F0D File Offset: 0x0001610D
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00017F15 File Offset: 0x00016115
		public bool SortEntriesBeforeSaving { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00017F1E File Offset: 0x0001611E
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00017F26 File Offset: 0x00016126
		public bool AddDirectoryWillTraverseReparsePoints { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00017F30 File Offset: 0x00016130
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00017F48 File Offset: 0x00016148
		public int BufferSize
		{
			get
			{
				return this._BufferSize;
			}
			set
			{
				this._BufferSize = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00017F52 File Offset: 0x00016152
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00017F5A File Offset: 0x0001615A
		public int CodecBufferSize { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00017F63 File Offset: 0x00016163
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00017F6B File Offset: 0x0001616B
		public bool FlattenFoldersOnExtract { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00017F74 File Offset: 0x00016174
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00017F8C File Offset: 0x0001618C
		public CompressionStrategy Strategy
		{
			get
			{
				return this._Strategy;
			}
			set
			{
				this._Strategy = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00017F98 File Offset: 0x00016198
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00017FB0 File Offset: 0x000161B0
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00017FBA File Offset: 0x000161BA
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00017FC2 File Offset: 0x000161C2
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00017FCC File Offset: 0x000161CC
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00017FE4 File Offset: 0x000161E4
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this._compressionMethod;
			}
			set
			{
				this._compressionMethod = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00017FF0 File Offset: 0x000161F0
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00018008 File Offset: 0x00016208
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._contentsChanged = true;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0001801C File Offset: 0x0001621C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00018034 File Offset: 0x00016234
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00018040 File Offset: 0x00016240
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00018058 File Offset: 0x00016258
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00018064 File Offset: 0x00016264
		internal bool Verbose
		{
			get
			{
				return this._StatusMessageTextWriter != null;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00018080 File Offset: 0x00016280
		public bool ContainsEntry(string name)
		{
			return this._entries.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000180A4 File Offset: 0x000162A4
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000180BC File Offset: 0x000162BC
		public bool CaseSensitiveRetrieval
		{
			get
			{
				return this._CaseSensitiveRetrieval;
			}
			set
			{
				bool flag = value != this._CaseSensitiveRetrieval;
				if (flag)
				{
					this._CaseSensitiveRetrieval = value;
					this._initEntriesDictionary();
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000180EC File Offset: 0x000162EC
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0001811C File Offset: 0x0001631C
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.GetEncoding("UTF-8") && this._alternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.GetEncoding("UTF-8");
					this._alternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					this._alternateEncoding = ZipFile.DefaultEncoding;
					this._alternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00018160 File Offset: 0x00016360
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00018178 File Offset: 0x00016378
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				return this._zip64;
			}
			set
			{
				this._zip64 = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00018184 File Offset: 0x00016384
		public bool? RequiresZip64
		{
			get
			{
				bool flag = this._entries.Count > 65534;
				bool? flag2;
				if (flag)
				{
					flag2 = new bool?(true);
				}
				else
				{
					bool flag3 = !this._hasBeenSaved || this._contentsChanged;
					if (flag3)
					{
						flag2 = null;
					}
					else
					{
						foreach (ZipEntry zipEntry in this._entries.Values)
						{
							bool value = zipEntry.RequiresZip64.Value;
							if (value)
							{
								return new bool?(true);
							}
						}
						flag2 = new bool?(false);
					}
				}
				return flag2;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00018248 File Offset: 0x00016448
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00018260 File Offset: 0x00016460
		public bool? InputUsesZip64
		{
			get
			{
				bool flag = this._entries.Count > 65534;
				bool? flag2;
				if (flag)
				{
					flag2 = new bool?(true);
				}
				else
				{
					foreach (ZipEntry zipEntry in this)
					{
						bool flag3 = zipEntry.Source != ZipEntrySource.ZipFile;
						if (flag3)
						{
							return null;
						}
						bool inputUsesZip = zipEntry._InputUsesZip64;
						if (inputUsesZip)
						{
							return new bool?(true);
						}
					}
					flag2 = new bool?(false);
				}
				return flag2;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00018304 File Offset: 0x00016504
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0001832D File Offset: 0x0001652D
		[Obsolete("use AlternateEncoding instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				bool flag = this._alternateEncodingUsage == ZipOption.AsNecessary;
				Encoding encoding;
				if (flag)
				{
					encoding = this._alternateEncoding;
				}
				else
				{
					encoding = null;
				}
				return encoding;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00018340 File Offset: 0x00016540
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00018358 File Offset: 0x00016558
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00018364 File Offset: 0x00016564
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0001837C File Offset: 0x0001657C
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00018388 File Offset: 0x00016588
		public static Encoding DefaultEncoding
		{
			get
			{
				return ZipFile._defaultEncoding;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000183A0 File Offset: 0x000165A0
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000183B8 File Offset: 0x000165B8
		public TextWriter StatusMessageTextWriter
		{
			get
			{
				return this._StatusMessageTextWriter;
			}
			set
			{
				this._StatusMessageTextWriter = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000183C4 File Offset: 0x000165C4
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000183DC File Offset: 0x000165DC
		public string TempFileFolder
		{
			get
			{
				return this._TempFileFolder;
			}
			set
			{
				this._TempFileFolder = value;
				bool flag = value == null;
				if (!flag)
				{
					bool flag2 = !Directory.Exists(value);
					if (flag2)
					{
						throw new FileNotFoundException(string.Format("That directory ({0}) does not exist.", value));
					}
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00018464 File Offset: 0x00016664
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0001841C File Offset: 0x0001661C
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				bool flag = this._Password == null;
				if (flag)
				{
					this.Encryption = EncryptionAlgorithm.None;
				}
				else
				{
					bool flag2 = this.Encryption == EncryptionAlgorithm.None;
					if (flag2)
					{
						this.Encryption = EncryptionAlgorithm.PkzipWeak;
					}
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001847C File Offset: 0x0001667C
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00018484 File Offset: 0x00016684
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00018490 File Offset: 0x00016690
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000184BC File Offset: 0x000166BC
		public ZipErrorAction ZipErrorAction
		{
			get
			{
				bool flag = this.ZipError != null;
				if (flag)
				{
					this._zipErrorAction = ZipErrorAction.InvokeErrorEvent;
				}
				return this._zipErrorAction;
			}
			set
			{
				this._zipErrorAction = value;
				bool flag = this._zipErrorAction != ZipErrorAction.InvokeErrorEvent && this.ZipError != null;
				if (flag)
				{
					this.ZipError = null;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000184F4 File Offset: 0x000166F4
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0001850C File Offset: 0x0001670C
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				bool flag = value == EncryptionAlgorithm.Unsupported;
				if (flag)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00018534 File Offset: 0x00016734
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0001853C File Offset: 0x0001673C
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00018548 File Offset: 0x00016748
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00018560 File Offset: 0x00016760
		public int MaxOutputSegmentSize
		{
			get
			{
				return this._maxOutputSegmentSize;
			}
			set
			{
				bool flag = value < 65536 && value != 0;
				if (flag)
				{
					throw new ZipException("The minimum acceptable segment size is 65536.");
				}
				this._maxOutputSegmentSize = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00018594 File Offset: 0x00016794
		public int NumberOfSegmentsForMostRecentSave
		{
			get
			{
				return (int)(this._numberOfSegmentsForMostRecentSave + 1U);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000185E8 File Offset: 0x000167E8
		// (set) Token: 0x06000327 RID: 807 RVA: 0x000185B0 File Offset: 0x000167B0
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				bool flag = value != 0L && value != -1L && value < 65536L;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateThreshold should be -1, 0, or > 65536");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00018600 File Offset: 0x00016800
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00018618 File Offset: 0x00016818
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				bool flag = value < 4;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018648 File Offset: 0x00016848
		public override string ToString()
		{
			return string.Format("ZipFile::{0}", this.Name);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0001866C File Offset: 0x0001686C
		public static Version LibraryVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001868D File Offset: 0x0001688D
		internal void NotifyEntryChanged()
		{
			this._contentsChanged = true;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00018698 File Offset: 0x00016898
		internal Stream StreamForDiskNumber(uint diskNumber)
		{
			bool flag = diskNumber + 1U == this._diskNumberWithCd || (diskNumber == 0U && this._diskNumberWithCd == 0U);
			Stream stream;
			if (flag)
			{
				stream = this.ReadStream;
			}
			else
			{
				stream = ZipSegmentedStream.ForReading(this._readName ?? this._name, diskNumber, this._diskNumberWithCd);
			}
			return stream;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000186F4 File Offset: 0x000168F4
		internal void Reset(bool whileSaving)
		{
			bool justSaved = this._JustSaved;
			if (justSaved)
			{
				using (ZipFile zipFile = new ZipFile())
				{
					zipFile._readName = (zipFile._name = (whileSaving ? (this._readName ?? this._name) : this._name));
					zipFile.AlternateEncoding = this.AlternateEncoding;
					zipFile.AlternateEncodingUsage = this.AlternateEncodingUsage;
					ZipFile.ReadIntoInstance(zipFile);
					foreach (ZipEntry zipEntry in zipFile)
					{
						foreach (ZipEntry zipEntry2 in this)
						{
							bool flag = zipEntry.FileName == zipEntry2.FileName;
							if (flag)
							{
								zipEntry2.CopyMetaData(zipEntry);
								break;
							}
						}
					}
				}
				this._JustSaved = false;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00018820 File Offset: 0x00016A20
		public ZipFile(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("Could not read {0} as a zip file", fileName), ex);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000188D0 File Offset: 0x00016AD0
		public ZipFile(string fileName, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00018990 File Offset: 0x00016B90
		public ZipFile()
		{
			this._InitInstance(null, null);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00018A18 File Offset: 0x00016C18
		public ZipFile(Encoding encoding)
		{
			this.AlternateEncoding = encoding;
			this.AlternateEncodingUsage = ZipOption.Always;
			this._InitInstance(null, null);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public ZipFile(string fileName, TextWriter statusMessageWriter)
		{
			try
			{
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00018B60 File Offset: 0x00016D60
		public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00018C20 File Offset: 0x00016E20
		public void Initialize(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00018C60 File Offset: 0x00016E60
		private void _initEntriesDictionary()
		{
			StringComparer stringComparer = (this.CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			this._entries = ((this._entries == null) ? new Dictionary<string, ZipEntry>(stringComparer) : new Dictionary<string, ZipEntry>(this._entries, stringComparer));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00018CA8 File Offset: 0x00016EA8
		private void _InitInstance(string zipFileName, TextWriter statusMessageWriter)
		{
			this._name = zipFileName;
			this._StatusMessageTextWriter = statusMessageWriter;
			this._contentsChanged = true;
			this.AddDirectoryWillTraverseReparsePoints = true;
			this.CompressionLevel = CompressionLevel.Default;
			this.ParallelDeflateThreshold = 524288L;
			this._initEntriesDictionary();
			bool flag = File.Exists(this._name);
			if (flag)
			{
				bool fullScan = this.FullScan;
				if (fullScan)
				{
					ZipFile.ReadIntoInstance_Orig(this);
				}
				else
				{
					ZipFile.ReadIntoInstance(this);
				}
				this._fileAlreadyExists = true;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00018D24 File Offset: 0x00016F24
		private List<ZipEntry> ZipEntriesAsList
		{
			get
			{
				bool flag = this._zipEntriesAsList == null;
				if (flag)
				{
					this._zipEntriesAsList = new List<ZipEntry>(this._entries.Values);
				}
				return this._zipEntriesAsList;
			}
		}

		// Token: 0x170000BD RID: 189
		public ZipEntry this[int ix]
		{
			get
			{
				return this.ZipEntriesAsList[ix];
			}
		}

		// Token: 0x170000BE RID: 190
		public ZipEntry this[string fileName]
		{
			get
			{
				string text = SharedUtilities.NormalizePathForUseInZipFile(fileName);
				bool flag = this._entries.ContainsKey(text);
				ZipEntry zipEntry;
				if (flag)
				{
					zipEntry = this._entries[text];
				}
				else
				{
					text = text.Replace("/", "\\");
					bool flag2 = this._entries.ContainsKey(text);
					if (flag2)
					{
						zipEntry = this._entries[text];
					}
					else
					{
						zipEntry = null;
					}
				}
				return zipEntry;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00018DEC File Offset: 0x00016FEC
		public ICollection<string> EntryFileNames
		{
			get
			{
				return this._entries.Keys;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00018E0C File Offset: 0x0001700C
		public ICollection<ZipEntry> Entries
		{
			get
			{
				return this._entries.Values;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00018E2C File Offset: 0x0001702C
		public ICollection<ZipEntry> EntriesSorted
		{
			get
			{
				List<ZipEntry> list = new List<ZipEntry>();
				foreach (ZipEntry zipEntry in this.Entries)
				{
					list.Add(zipEntry);
				}
				StringComparison sc = (this.CaseSensitiveRetrieval ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
				list.Sort((ZipEntry x, ZipEntry y) => string.Compare(x.FileName, y.FileName, sc));
				return list.AsReadOnly();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00018EBC File Offset: 0x000170BC
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00018EDC File Offset: 0x000170DC
		public void RemoveEntry(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			this._entries.Remove(SharedUtilities.NormalizePathForUseInZipFile(entry.FileName));
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00018F24 File Offset: 0x00017124
		public void RemoveEntry(string fileName)
		{
			string text = ZipEntry.NameInArchive(fileName, null);
			ZipEntry zipEntry = this[text];
			bool flag = zipEntry == null;
			if (flag)
			{
				throw new ArgumentException("The entry you specified was not found in the zip archive.");
			}
			this.RemoveEntry(zipEntry);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00018F5D File Offset: 0x0001715D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00018F70 File Offset: 0x00017170
		protected virtual void Dispose(bool disposeManagedResources)
		{
			bool flag = !this._disposed;
			if (flag)
			{
				if (disposeManagedResources)
				{
					bool readStreamIsOurs = this._ReadStreamIsOurs;
					if (readStreamIsOurs)
					{
						bool flag2 = this._readstream != null;
						if (flag2)
						{
							this._readstream.Dispose();
							this._readstream = null;
						}
					}
					bool flag3 = this._temporaryFileName != null && this._name != null;
					if (flag3)
					{
						bool flag4 = this._writestream != null;
						if (flag4)
						{
							this._writestream.Dispose();
							this._writestream = null;
						}
					}
					bool flag5 = this.ParallelDeflater != null;
					if (flag5)
					{
						this.ParallelDeflater.Dispose();
						this.ParallelDeflater = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00019034 File Offset: 0x00017234
		internal Stream ReadStream
		{
			get
			{
				bool flag = this._readstream == null;
				if (flag)
				{
					bool flag2 = this._readName != null || this._name != null;
					if (flag2)
					{
						this._readstream = File.Open(this._readName ?? this._name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						this._ReadStreamIsOurs = true;
					}
				}
				return this._readstream;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001909C File Offset: 0x0001729C
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00019138 File Offset: 0x00017338
		private Stream WriteStream
		{
			get
			{
				bool flag = this._writestream != null;
				Stream stream;
				if (flag)
				{
					stream = this._writestream;
				}
				else
				{
					bool flag2 = this._name == null;
					if (flag2)
					{
						stream = this._writestream;
					}
					else
					{
						bool flag3 = this._maxOutputSegmentSize != 0;
						if (flag3)
						{
							this._writestream = ZipSegmentedStream.ForWriting(this._name, this._maxOutputSegmentSize);
							stream = this._writestream;
						}
						else
						{
							SharedUtilities.CreateAndOpenUniqueTempFile(this.TempFileFolder ?? Path.GetDirectoryName(this._name), out this._writestream, out this._temporaryFileName);
							stream = this._writestream;
						}
					}
				}
				return stream;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					throw new ZipException("Cannot set the stream to a non-null value.");
				}
				this._writestream = null;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00019160 File Offset: 0x00017360
		private string ArchiveNameForEvent
		{
			get
			{
				return (this._name != null) ? this._name : "(stream)";
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000348 RID: 840 RVA: 0x00019188 File Offset: 0x00017388
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x000191C0 File Offset: 0x000173C0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SaveProgressEventArgs> SaveProgress;

		// Token: 0x0600034A RID: 842 RVA: 0x000191F8 File Offset: 0x000173F8
		internal bool OnSaveBlock(ZipEntry entry, long bytesXferred, long totalBytesToXfer)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			bool flag = saveProgress != null;
			if (flag)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesXferred, totalBytesToXfer);
				saveProgress(this, saveProgressEventArgs);
				bool cancel = saveProgressEventArgs.Cancel;
				if (cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001924C File Offset: 0x0001744C
		private void OnSaveEntry(int current, ZipEntry entry, bool before)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			bool flag = saveProgress != null;
			if (flag)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, entry);
				saveProgress(this, saveProgressEventArgs);
				bool cancel = saveProgressEventArgs.Cancel;
				if (cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000192A0 File Offset: 0x000174A0
		private void OnSaveEvent(ZipProgressEventType eventFlavor)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			bool flag = saveProgress != null;
			if (flag)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, eventFlavor);
				saveProgress(this, saveProgressEventArgs);
				bool cancel = saveProgressEventArgs.Cancel;
				if (cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000192E8 File Offset: 0x000174E8
		private void OnSaveStarted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			bool flag = saveProgress != null;
			if (flag)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.Started(this.ArchiveNameForEvent);
				saveProgress(this, saveProgressEventArgs);
				bool cancel = saveProgressEventArgs.Cancel;
				if (cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00019330 File Offset: 0x00017530
		private void OnSaveCompleted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			bool flag = saveProgress != null;
			if (flag)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.Completed(this.ArchiveNameForEvent);
				saveProgress(this, saveProgressEventArgs);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600034F RID: 847 RVA: 0x00019364 File Offset: 0x00017564
		// (remove) Token: 0x06000350 RID: 848 RVA: 0x0001939C File Offset: 0x0001759C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ReadProgressEventArgs> ReadProgress;

		// Token: 0x06000351 RID: 849 RVA: 0x000193D4 File Offset: 0x000175D4
		private void OnReadStarted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			bool flag = readProgress != null;
			if (flag)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.Started(this.ArchiveNameForEvent);
				readProgress(this, readProgressEventArgs);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00019408 File Offset: 0x00017608
		private void OnReadCompleted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			bool flag = readProgress != null;
			if (flag)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.Completed(this.ArchiveNameForEvent);
				readProgress(this, readProgressEventArgs);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001943C File Offset: 0x0001763C
		internal void OnReadBytes(ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			bool flag = readProgress != null;
			if (flag)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, this.ReadStream.Position, this.LengthOfReadStream);
				readProgress(this, readProgressEventArgs);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00019484 File Offset: 0x00017684
		internal void OnReadEntry(bool before, ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			bool flag = readProgress != null;
			if (flag)
			{
				ReadProgressEventArgs readProgressEventArgs = (before ? ReadProgressEventArgs.Before(this.ArchiveNameForEvent, this._entries.Count) : ReadProgressEventArgs.After(this.ArchiveNameForEvent, entry, this._entries.Count));
				readProgress(this, readProgressEventArgs);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000194E0 File Offset: 0x000176E0
		private long LengthOfReadStream
		{
			get
			{
				bool flag = this._lengthOfReadStream == -99L;
				if (flag)
				{
					this._lengthOfReadStream = (this._ReadStreamIsOurs ? SharedUtilities.GetFileLength(this._name) : (-1L));
				}
				return this._lengthOfReadStream;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000356 RID: 854 RVA: 0x00019528 File Offset: 0x00017728
		// (remove) Token: 0x06000357 RID: 855 RVA: 0x00019560 File Offset: 0x00017760
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ExtractProgressEventArgs> ExtractProgress;

		// Token: 0x06000358 RID: 856 RVA: 0x00019598 File Offset: 0x00017798
		private void OnExtractEntry(int current, bool before, ZipEntry currentEntry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, currentEntry, path);
				extractProgress(this, extractProgressEventArgs);
				bool cancel = extractProgressEventArgs.Cancel;
				if (cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000195F0 File Offset: 0x000177F0
		internal bool OnExtractBlock(ZipEntry entry, long bytesWritten, long totalBytesToWrite)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesWritten, totalBytesToWrite);
				extractProgress(this, extractProgressEventArgs);
				bool cancel = extractProgressEventArgs.Cancel;
				if (cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00019644 File Offset: 0x00017844
		internal bool OnSingleEntryExtract(ZipEntry entry, string path, bool before)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = (before ? ExtractProgressEventArgs.BeforeExtractEntry(this.ArchiveNameForEvent, entry, path) : ExtractProgressEventArgs.AfterExtractEntry(this.ArchiveNameForEvent, entry, path));
				extractProgress(this, extractProgressEventArgs);
				bool cancel = extractProgressEventArgs.Cancel;
				if (cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000196AC File Offset: 0x000178AC
		internal bool OnExtractExisting(ZipEntry entry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractExisting(this.ArchiveNameForEvent, entry, path);
				extractProgress(this, extractProgressEventArgs);
				bool cancel = extractProgressEventArgs.Cancel;
				if (cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00019700 File Offset: 0x00017900
		private void OnExtractAllCompleted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractAllCompleted(this.ArchiveNameForEvent, path);
				extractProgress(this, extractProgressEventArgs);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00019738 File Offset: 0x00017938
		private void OnExtractAllStarted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			bool flag = extractProgress != null;
			if (flag)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractAllStarted(this.ArchiveNameForEvent, path);
				extractProgress(this, extractProgressEventArgs);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600035E RID: 862 RVA: 0x00019770 File Offset: 0x00017970
		// (remove) Token: 0x0600035F RID: 863 RVA: 0x000197A8 File Offset: 0x000179A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AddProgressEventArgs> AddProgress;

		// Token: 0x06000360 RID: 864 RVA: 0x000197E0 File Offset: 0x000179E0
		private void OnAddStarted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			bool flag = addProgress != null;
			if (flag)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.Started(this.ArchiveNameForEvent);
				addProgress(this, addProgressEventArgs);
				bool cancel = addProgressEventArgs.Cancel;
				if (cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00019828 File Offset: 0x00017A28
		private void OnAddCompleted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			bool flag = addProgress != null;
			if (flag)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.Completed(this.ArchiveNameForEvent);
				addProgress(this, addProgressEventArgs);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001985C File Offset: 0x00017A5C
		internal void AfterAddEntry(ZipEntry entry)
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			bool flag = addProgress != null;
			if (flag)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.AfterEntry(this.ArchiveNameForEvent, entry, this._entries.Count);
				addProgress(this, addProgressEventArgs);
				bool cancel = addProgressEventArgs.Cancel;
				if (cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000363 RID: 867 RVA: 0x000198B0 File Offset: 0x00017AB0
		// (remove) Token: 0x06000364 RID: 868 RVA: 0x000198E8 File Offset: 0x00017AE8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ZipErrorEventArgs> ZipError;

		// Token: 0x06000365 RID: 869 RVA: 0x00019920 File Offset: 0x00017B20
		internal bool OnZipErrorSaving(ZipEntry entry, Exception exc)
		{
			bool flag = this.ZipError != null;
			if (flag)
			{
				object @lock = this.LOCK;
				lock (@lock)
				{
					ZipErrorEventArgs zipErrorEventArgs = ZipErrorEventArgs.Saving(this.Name, entry, exc);
					this.ZipError(this, zipErrorEventArgs);
					bool cancel = zipErrorEventArgs.Cancel;
					if (cancel)
					{
						this._saveOperationCanceled = true;
					}
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000199AC File Offset: 0x00017BAC
		public void ExtractAll(string path)
		{
			this._InternalExtractAll(path, true);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000199B8 File Offset: 0x00017BB8
		public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this._InternalExtractAll(path, true);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000199CC File Offset: 0x00017BCC
		private void _InternalExtractAll(string path, bool overrideExtractExistingProperty)
		{
			bool flag = this.Verbose;
			this._inExtractAll = true;
			try
			{
				this.OnExtractAllStarted(path);
				int num = 0;
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					bool flag2 = flag;
					if (flag2)
					{
						this.StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", new object[] { "Name", "Modified", "Size", "Ratio", "Packed" });
						this.StatusMessageTextWriter.WriteLine(new string('-', 72));
						flag = false;
					}
					bool verbose = this.Verbose;
					if (verbose)
					{
						this.StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", new object[]
						{
							zipEntry.FileName,
							zipEntry.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
							zipEntry.UncompressedSize,
							zipEntry.CompressionRatio,
							zipEntry.CompressedSize
						});
						bool flag3 = !string.IsNullOrEmpty(zipEntry.Comment);
						if (flag3)
						{
							this.StatusMessageTextWriter.WriteLine("  Comment: {0}", zipEntry.Comment);
						}
					}
					zipEntry.Password = this._Password;
					this.OnExtractEntry(num, true, zipEntry, path);
					if (overrideExtractExistingProperty)
					{
						zipEntry.ExtractExistingFile = this.ExtractExistingFile;
					}
					zipEntry.Extract(path);
					num++;
					this.OnExtractEntry(num, false, zipEntry, path);
					bool extractOperationCanceled = this._extractOperationCanceled;
					if (extractOperationCanceled)
					{
						break;
					}
				}
				bool flag4 = !this._extractOperationCanceled;
				if (flag4)
				{
					foreach (ZipEntry zipEntry2 in this._entries.Values)
					{
						bool flag5 = zipEntry2.IsDirectory || zipEntry2.FileName.EndsWith("/");
						if (flag5)
						{
							string text = (zipEntry2.FileName.StartsWith("/") ? Path.Combine(path, zipEntry2.FileName.Substring(1)) : Path.Combine(path, zipEntry2.FileName));
							zipEntry2._SetTimes(text, false);
						}
					}
					this.OnExtractAllCompleted(path);
				}
			}
			finally
			{
				this._inExtractAll = false;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00019C94 File Offset: 0x00017E94
		public static ZipFile Read(string fileName)
		{
			return ZipFile.Read(fileName, null, null, null);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00019CB0 File Offset: 0x00017EB0
		public static ZipFile Read(string fileName, ReadOptions options)
		{
			bool flag = options == null;
			if (flag)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(fileName, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00019CF0 File Offset: 0x00017EF0
		private static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			ZipFile zipFile = new ZipFile();
			zipFile.AlternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile.AlternateEncodingUsage = ZipOption.Always;
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._name = fileName;
			bool flag = readProgress != null;
			if (flag)
			{
				zipFile.ReadProgress = readProgress;
			}
			bool verbose = zipFile.Verbose;
			if (verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from {0}...", fileName);
			}
			ZipFile.ReadIntoInstance(zipFile);
			zipFile._fileAlreadyExists = true;
			return zipFile;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00019D6C File Offset: 0x00017F6C
		public static ZipFile Read(Stream zipStream)
		{
			return ZipFile.Read(zipStream, null, null, null);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00019D88 File Offset: 0x00017F88
		public static ZipFile Read(Stream zipStream, ReadOptions options)
		{
			bool flag = options == null;
			if (flag)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(zipStream, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00019DC8 File Offset: 0x00017FC8
		private static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			bool flag = zipStream == null;
			if (flag)
			{
				throw new ArgumentNullException("zipStream");
			}
			ZipFile zipFile = new ZipFile();
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._alternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile._alternateEncodingUsage = ZipOption.Always;
			bool flag2 = readProgress != null;
			if (flag2)
			{
				zipFile.ReadProgress += readProgress;
			}
			zipFile._readstream = ((zipStream.Position == 0L) ? zipStream : new OffsetStream(zipStream));
			zipFile._ReadStreamIsOurs = false;
			bool verbose = zipFile.Verbose;
			if (verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from stream...");
			}
			ZipFile.ReadIntoInstance(zipFile);
			return zipFile;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00019E64 File Offset: 0x00018064
		private static void ReadIntoInstance(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			try
			{
				zf._readName = zf._name;
				bool flag = !readStream.CanSeek;
				if (flag)
				{
					ZipFile.ReadIntoInstance_Orig(zf);
					return;
				}
				zf.OnReadStarted();
				uint num = ZipFile.ReadFirstFourBytes(readStream);
				bool flag2 = num == 101010256U;
				if (flag2)
				{
					return;
				}
				int num2 = 0;
				bool flag3 = false;
				long num3 = readStream.Length - 64L;
				long num4 = Math.Max(readStream.Length - 16384L, 10L);
				do
				{
					bool flag4 = num3 < 0L;
					if (flag4)
					{
						num3 = 0L;
					}
					readStream.Seek(num3, SeekOrigin.Begin);
					long num5 = SharedUtilities.FindSignature(readStream, 101010256);
					bool flag5 = num5 != -1L;
					if (flag5)
					{
						flag3 = true;
					}
					else
					{
						bool flag6 = num3 == 0L;
						if (flag6)
						{
							break;
						}
						num2++;
						num3 -= (long)(32 * (num2 + 1) * num2);
					}
				}
				while (!flag3 && num3 > num4);
				bool flag7 = flag3;
				if (flag7)
				{
					zf._locEndOfCDS = readStream.Position - 4L;
					byte[] array = new byte[16];
					readStream.Read(array, 0, array.Length);
					zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
					bool flag8 = zf._diskNumberWithCd == 65535U;
					if (flag8)
					{
						throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
					}
					zf._diskNumberWithCd += 1U;
					int num6 = 12;
					uint num7 = BitConverter.ToUInt32(array, num6);
					bool flag9 = num7 == uint.MaxValue;
					if (flag9)
					{
						ZipFile.Zip64SeekToCentralDirectory(zf);
					}
					else
					{
						zf._OffsetOfCentralDirectory = num7;
						readStream.Seek((long)((ulong)num7), SeekOrigin.Begin);
					}
					ZipFile.ReadCentralDirectory(zf);
				}
				else
				{
					readStream.Seek(0L, SeekOrigin.Begin);
					ZipFile.ReadIntoInstance_Orig(zf);
				}
			}
			catch (Exception ex)
			{
				bool flag10 = zf._ReadStreamIsOurs && zf._readstream != null;
				if (flag10)
				{
					try
					{
						zf._readstream.Dispose();
						zf._readstream = null;
					}
					finally
					{
					}
				}
				throw new ZipException("Cannot read that as a ZipFile", ex);
			}
			zf._contentsChanged = false;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001A0A0 File Offset: 0x000182A0
		private static void Zip64SeekToCentralDirectory(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			byte[] array = new byte[16];
			readStream.Seek(-40L, SeekOrigin.Current);
			readStream.Read(array, 0, 16);
			long num = BitConverter.ToInt64(array, 8);
			zf._OffsetOfCentralDirectory = uint.MaxValue;
			zf._OffsetOfCentralDirectory64 = num;
			readStream.Seek(num, SeekOrigin.Begin);
			uint num2 = (uint)SharedUtilities.ReadInt(readStream);
			bool flag = num2 != 101075792U;
			if (flag)
			{
				throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) looking for ZIP64 EoCD Record at position 0x{1:X8}", num2, readStream.Position));
			}
			readStream.Read(array, 0, 8);
			long num3 = BitConverter.ToInt64(array, 0);
			array = new byte[num3];
			readStream.Read(array, 0, array.Length);
			num = BitConverter.ToInt64(array, 36);
			readStream.Seek(num, SeekOrigin.Begin);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001A168 File Offset: 0x00018368
		private static uint ReadFirstFourBytes(Stream s)
		{
			return (uint)SharedUtilities.ReadInt(s);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001A184 File Offset: 0x00018384
		private static void ReadCentralDirectory(ZipFile zf)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
			{
				zipEntry.ResetDirEntry();
				zf.OnReadEntry(true, null);
				bool verbose = zf.Verbose;
				if (verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				bool inputUsesZip = zipEntry._InputUsesZip64;
				if (inputUsesZip)
				{
					flag = true;
				}
				dictionary.Add(zipEntry.FileName, null);
			}
			bool flag2 = flag;
			if (flag2)
			{
				zf.UseZip64WhenSaving = Zip64Option.Always;
			}
			bool flag3 = zf._locEndOfCDS > 0L;
			if (flag3)
			{
				zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
			}
			ZipFile.ReadCentralDirectoryFooter(zf);
			bool flag4 = zf.Verbose && !string.IsNullOrEmpty(zf.Comment);
			if (flag4)
			{
				zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
			}
			bool verbose2 = zf.Verbose;
			if (verbose2)
			{
				zf.StatusMessageTextWriter.WriteLine("read in {0} entries.", zf._entries.Count);
			}
			zf.OnReadCompleted();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001A2B4 File Offset: 0x000184B4
		private static void ReadIntoInstance_Orig(ZipFile zf)
		{
			zf.OnReadStarted();
			zf._entries = new Dictionary<string, ZipEntry>();
			bool verbose = zf.Verbose;
			if (verbose)
			{
				bool flag = zf.Name == null;
				if (flag)
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
				}
				else
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zf.Name);
				}
			}
			bool flag2 = true;
			ZipContainer zipContainer = new ZipContainer(zf);
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadEntry(zipContainer, flag2)) != null)
			{
				bool verbose2 = zf.Verbose;
				if (verbose2)
				{
					zf.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				flag2 = false;
			}
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ZipEntry zipEntry2;
				while ((zipEntry2 = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
				{
					ZipEntry zipEntry3 = zf._entries[zipEntry2.FileName];
					bool flag3 = zipEntry3 != null;
					if (flag3)
					{
						zipEntry3._Comment = zipEntry2.Comment;
						bool isDirectory = zipEntry2.IsDirectory;
						if (isDirectory)
						{
							zipEntry3.MarkAsDirectory();
						}
					}
					dictionary.Add(zipEntry2.FileName, null);
				}
				bool flag4 = zf._locEndOfCDS > 0L;
				if (flag4)
				{
					zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
				}
				ZipFile.ReadCentralDirectoryFooter(zf);
				bool flag5 = zf.Verbose && !string.IsNullOrEmpty(zf.Comment);
				if (flag5)
				{
					zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
				}
			}
			catch (ZipException)
			{
			}
			catch (IOException)
			{
			}
			zf.OnReadCompleted();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001A474 File Offset: 0x00018674
		private static void ReadCentralDirectoryFooter(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			int num = SharedUtilities.ReadSignature(readStream);
			int num2 = 0;
			bool flag = (long)num == 101075792L;
			byte[] array;
			if (flag)
			{
				array = new byte[52];
				readStream.Read(array, 0, array.Length);
				long num3 = BitConverter.ToInt64(array, 0);
				bool flag2 = num3 < 44L;
				if (flag2)
				{
					throw new ZipException("Bad size in the ZIP64 Central Directory.");
				}
				zf._versionMadeBy = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._versionNeededToExtract = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._diskNumberWithCd = BitConverter.ToUInt32(array, num2);
				num2 += 2;
				array = new byte[num3 - 44L];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
				bool flag3 = (long)num != 117853008L;
				if (flag3)
				{
					throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
				}
				array = new byte[16];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
			}
			bool flag4 = (long)num != 101010256L;
			if (flag4)
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				throw new BadReadException(string.Format("Bad signature ({0:X8}) at position 0x{1:X8}", num, readStream.Position));
			}
			array = new byte[16];
			zf.ReadStream.Read(array, 0, array.Length);
			bool flag5 = zf._diskNumberWithCd == 0U;
			if (flag5)
			{
				zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
			}
			ZipFile.ReadZipFileComment(zf);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001A5E8 File Offset: 0x000187E8
		private static void ReadZipFileComment(ZipFile zf)
		{
			byte[] array = new byte[2];
			zf.ReadStream.Read(array, 0, array.Length);
			short num = (short)((int)array[0] + (int)array[1] * 256);
			bool flag = num > 0;
			if (flag)
			{
				array = new byte[(int)num];
				zf.ReadStream.Read(array, 0, array.Length);
				string @string = zf.AlternateEncoding.GetString(array, 0, array.Length);
				zf.Comment = @string;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001A658 File Offset: 0x00018858
		public static bool IsZipFile(string fileName)
		{
			return ZipFile.IsZipFile(fileName, false);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001A674 File Offset: 0x00018874
		public static bool IsZipFile(string fileName, bool testExtract)
		{
			bool flag = false;
			try
			{
				bool flag2 = !File.Exists(fileName);
				if (flag2)
				{
					return false;
				}
				using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					flag = ZipFile.IsZipFile(fileStream, testExtract);
				}
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001A6F4 File Offset: 0x000188F4
		public static bool IsZipFile(Stream stream, bool testExtract)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag2 = false;
			try
			{
				bool flag3 = !stream.CanRead;
				if (flag3)
				{
					return false;
				}
				Stream @null = Stream.Null;
				using (ZipFile zipFile = ZipFile.Read(stream, null, null, null))
				{
					if (testExtract)
					{
						foreach (ZipEntry zipEntry in zipFile)
						{
							bool flag4 = !zipEntry.IsDirectory;
							if (flag4)
							{
								zipEntry.Extract(@null);
							}
						}
					}
				}
				flag2 = true;
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag2;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001A7EC File Offset: 0x000189EC
		private void DeleteFileWithRetry(string filename)
		{
			bool flag = false;
			int num = 3;
			int num2 = 0;
			while (num2 < num && !flag)
			{
				try
				{
					File.Delete(filename);
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("************************************************** Retry delete.");
					Thread.Sleep(200 + num2 * 200);
				}
				num2++;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001A858 File Offset: 0x00018A58
		public void Save()
		{
			try
			{
				bool flag = false;
				this._saveOperationCanceled = false;
				this._numberOfSegmentsForMostRecentSave = 0U;
				this.OnSaveStarted();
				bool flag2 = this.WriteStream == null;
				if (flag2)
				{
					throw new BadStateException("You haven't specified where to save the zip.");
				}
				bool flag3 = this._name != null && this._name.EndsWith(".exe") && !this._SavingSfx;
				if (flag3)
				{
					throw new BadStateException("You specified an EXE for a plain zip file.");
				}
				bool flag4 = !this._contentsChanged;
				if (flag4)
				{
					this.OnSaveCompleted();
					bool verbose = this.Verbose;
					if (verbose)
					{
						this.StatusMessageTextWriter.WriteLine("No save is necessary....");
					}
				}
				else
				{
					this.Reset(true);
					bool verbose2 = this.Verbose;
					if (verbose2)
					{
						this.StatusMessageTextWriter.WriteLine("saving....");
					}
					bool flag5 = this._entries.Count >= 65535 && this._zip64 == Zip64Option.Default;
					if (flag5)
					{
						throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
					}
					int num = 0;
					ICollection<ZipEntry> collection = (this.SortEntriesBeforeSaving ? this.EntriesSorted : this.Entries);
					foreach (ZipEntry zipEntry in collection)
					{
						this.OnSaveEntry(num, zipEntry, true);
						zipEntry.Write(this.WriteStream);
						bool saveOperationCanceled = this._saveOperationCanceled;
						if (saveOperationCanceled)
						{
							break;
						}
						num++;
						this.OnSaveEntry(num, zipEntry, false);
						bool saveOperationCanceled2 = this._saveOperationCanceled;
						if (saveOperationCanceled2)
						{
							break;
						}
						bool includedInMostRecentSave = zipEntry.IncludedInMostRecentSave;
						if (includedInMostRecentSave)
						{
							flag |= zipEntry.OutputUsedZip64.Value;
						}
					}
					bool saveOperationCanceled3 = this._saveOperationCanceled;
					if (!saveOperationCanceled3)
					{
						ZipSegmentedStream zipSegmentedStream = this.WriteStream as ZipSegmentedStream;
						this._numberOfSegmentsForMostRecentSave = ((zipSegmentedStream != null) ? zipSegmentedStream.CurrentSegment : 1U);
						bool flag6 = ZipOutput.WriteCentralDirectoryStructure(this.WriteStream, collection, this._numberOfSegmentsForMostRecentSave, this._zip64, this.Comment, new ZipContainer(this));
						this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
						this._hasBeenSaved = true;
						this._contentsChanged = false;
						flag = flag || flag6;
						this._OutputUsesZip64 = new bool?(flag);
						bool flag7 = this._name != null && (this._temporaryFileName != null || zipSegmentedStream != null);
						if (flag7)
						{
							this.WriteStream.Dispose();
							bool saveOperationCanceled4 = this._saveOperationCanceled;
							if (saveOperationCanceled4)
							{
								return;
							}
							bool flag8 = this._fileAlreadyExists && this._readstream != null;
							if (flag8)
							{
								this._readstream.Close();
								this._readstream = null;
								foreach (ZipEntry zipEntry2 in collection)
								{
									ZipSegmentedStream zipSegmentedStream2 = zipEntry2._archiveStream as ZipSegmentedStream;
									bool flag9 = zipSegmentedStream2 != null;
									if (flag9)
									{
										zipSegmentedStream2.Dispose();
									}
									zipEntry2._archiveStream = null;
								}
							}
							string text = null;
							bool flag10 = File.Exists(this._name);
							if (flag10)
							{
								text = this._name + "." + Path.GetRandomFileName();
								bool flag11 = File.Exists(text);
								if (flag11)
								{
									this.DeleteFileWithRetry(text);
								}
								File.Move(this._name, text);
							}
							this.OnSaveEvent(ZipProgressEventType.Saving_BeforeRenameTempArchive);
							File.Move((zipSegmentedStream != null) ? zipSegmentedStream.CurrentTempName : this._temporaryFileName, this._name);
							this.OnSaveEvent(ZipProgressEventType.Saving_AfterRenameTempArchive);
							bool flag12 = text != null;
							if (flag12)
							{
								try
								{
									bool flag13 = File.Exists(text);
									if (flag13)
									{
										File.Delete(text);
									}
								}
								catch
								{
								}
							}
							this._fileAlreadyExists = true;
						}
						ZipFile.NotifyEntriesSaveComplete(collection);
						this.OnSaveCompleted();
						this._JustSaved = true;
					}
				}
			}
			finally
			{
				this.CleanupAfterSaveOperation();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001AC88 File Offset: 0x00018E88
		private static void NotifyEntriesSaveComplete(ICollection<ZipEntry> c)
		{
			foreach (ZipEntry zipEntry in c)
			{
				zipEntry.NotifySaveComplete();
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		private void RemoveTempFile()
		{
			try
			{
				bool flag = File.Exists(this._temporaryFileName);
				if (flag)
				{
					File.Delete(this._temporaryFileName);
				}
			}
			catch (IOException ex)
			{
				bool verbose = this.Verbose;
				if (verbose)
				{
					this.StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
				}
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001AD40 File Offset: 0x00018F40
		private void CleanupAfterSaveOperation()
		{
			bool flag = this._name != null;
			if (flag)
			{
				bool flag2 = this._writestream != null;
				if (flag2)
				{
					try
					{
						this._writestream.Dispose();
					}
					catch (IOException)
					{
					}
				}
				this._writestream = null;
				bool flag3 = this._temporaryFileName != null;
				if (flag3)
				{
					this.RemoveTempFile();
					this._temporaryFileName = null;
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		public void Save(string fileName)
		{
			bool flag = this._name == null;
			if (flag)
			{
				this._writestream = null;
			}
			else
			{
				this._readName = this._name;
			}
			this._name = fileName;
			bool flag2 = Directory.Exists(this._name);
			if (flag2)
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this.Save();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001AE38 File Offset: 0x00019038
		public void Save(Stream outputStream)
		{
			bool flag = outputStream == null;
			if (flag)
			{
				throw new ArgumentNullException("outputStream");
			}
			bool flag2 = !outputStream.CanWrite;
			if (flag2)
			{
				throw new ArgumentException("Must be a writable stream.", "outputStream");
			}
			this._name = null;
			this._writestream = new CountingStream(outputStream);
			this._contentsChanged = true;
			this._fileAlreadyExists = false;
			this.Save();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001AEA0 File Offset: 0x000190A0
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
		{
			this.SaveSelfExtractor(exeToGenerate, new SelfExtractorSaveOptions
			{
				Flavor = flavor
			});
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001AEC8 File Offset: 0x000190C8
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			bool flag = this._name == null;
			if (flag)
			{
				this._writestream = null;
			}
			this._SavingSfx = true;
			this._name = exeToGenerate;
			bool flag2 = Directory.Exists(this._name);
			if (flag2)
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this._SaveSfxStub(exeToGenerate, options);
			this.Save();
			this._SavingSfx = false;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001AF50 File Offset: 0x00019150
		private static void ExtractResourceToFile(Assembly a, string resourceName, string filename)
		{
			byte[] array = new byte[1024];
			using (Stream manifestResourceStream = a.GetManifestResourceStream(resourceName))
			{
				bool flag = manifestResourceStream == null;
				if (flag)
				{
					throw new ZipException(string.Format("missing resource '{0}'", resourceName));
				}
				using (FileStream fileStream = File.OpenWrite(filename))
				{
					int num;
					do
					{
						num = manifestResourceStream.Read(array, 0, array.Length);
						fileStream.Write(array, 0, num);
					}
					while (num > 0);
				}
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001AFF4 File Offset: 0x000191F4
		private void _SaveSfxStub(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				bool flag = File.Exists(exeToGenerate);
				if (flag)
				{
					bool verbose = this.Verbose;
					if (verbose)
					{
						this.StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", exeToGenerate);
					}
				}
				bool flag2 = !exeToGenerate.EndsWith(".exe");
				if (flag2)
				{
					bool verbose2 = this.Verbose;
					if (verbose2)
					{
						this.StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
					}
				}
				text3 = this.TempFileFolder ?? Path.GetDirectoryName(exeToGenerate);
				text = ZipFile.GenerateTempPathname(text3, "exe");
				Assembly assembly = typeof(ZipFile).Assembly;
				using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider())
				{
					ZipFile.ExtractorSettings extractorSettings = null;
					foreach (ZipFile.ExtractorSettings extractorSettings2 in ZipFile.SettingsList)
					{
						bool flag3 = extractorSettings2.Flavor == options.Flavor;
						if (flag3)
						{
							extractorSettings = extractorSettings2;
							break;
						}
					}
					bool flag4 = extractorSettings == null;
					if (flag4)
					{
						throw new BadStateException(string.Format("While saving a Self-Extracting Zip, Cannot find that flavor ({0})?", options.Flavor));
					}
					CompilerParameters compilerParameters = new CompilerParameters();
					compilerParameters.ReferencedAssemblies.Add(assembly.Location);
					bool flag5 = extractorSettings.ReferencedAssemblies != null;
					if (flag5)
					{
						foreach (string text4 in extractorSettings.ReferencedAssemblies)
						{
							compilerParameters.ReferencedAssemblies.Add(text4);
						}
					}
					compilerParameters.GenerateInMemory = false;
					compilerParameters.GenerateExecutable = true;
					compilerParameters.IncludeDebugInformation = false;
					compilerParameters.CompilerOptions = "";
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					StringBuilder stringBuilder = new StringBuilder();
					string text5 = ZipFile.GenerateTempPathname(text3, "cs");
					using (ZipFile zipFile = ZipFile.Read(executingAssembly.GetManifestResourceStream("Ionic.Zip.Resources.ZippedResources.zip")))
					{
						text2 = ZipFile.GenerateTempPathname(text3, "tmp");
						bool flag6 = string.IsNullOrEmpty(options.IconFile);
						if (flag6)
						{
							Directory.CreateDirectory(text2);
							ZipEntry zipEntry = zipFile["zippedFile.ico"];
							bool flag7 = (zipEntry.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
							if (flag7)
							{
								zipEntry.Attributes ^= FileAttributes.ReadOnly;
							}
							zipEntry.Extract(text2);
							string text6 = Path.Combine(text2, "zippedFile.ico");
							CompilerParameters compilerParameters2 = compilerParameters;
							compilerParameters2.CompilerOptions += string.Format("/win32icon:\"{0}\"", text6);
						}
						else
						{
							CompilerParameters compilerParameters3 = compilerParameters;
							compilerParameters3.CompilerOptions += string.Format("/win32icon:\"{0}\"", options.IconFile);
						}
						compilerParameters.OutputAssembly = text;
						bool flag8 = options.Flavor == SelfExtractorFlavor.WinFormsApplication;
						if (flag8)
						{
							CompilerParameters compilerParameters4 = compilerParameters;
							compilerParameters4.CompilerOptions += " /target:winexe";
						}
						bool flag9 = !string.IsNullOrEmpty(options.AdditionalCompilerSwitches);
						if (flag9)
						{
							CompilerParameters compilerParameters5 = compilerParameters;
							compilerParameters5.CompilerOptions = compilerParameters5.CompilerOptions + " " + options.AdditionalCompilerSwitches;
						}
						bool flag10 = string.IsNullOrEmpty(compilerParameters.CompilerOptions);
						if (flag10)
						{
							compilerParameters.CompilerOptions = null;
						}
						bool flag11 = extractorSettings.CopyThroughResources != null && extractorSettings.CopyThroughResources.Count != 0;
						if (flag11)
						{
							bool flag12 = !Directory.Exists(text2);
							if (flag12)
							{
								Directory.CreateDirectory(text2);
							}
							foreach (string text7 in extractorSettings.CopyThroughResources)
							{
								string text8 = Path.Combine(text2, text7);
								ZipFile.ExtractResourceToFile(executingAssembly, text7, text8);
								compilerParameters.EmbeddedResources.Add(text8);
							}
						}
						compilerParameters.EmbeddedResources.Add(assembly.Location);
						stringBuilder.Append("// " + Path.GetFileName(text5) + "\n").Append("// --------------------------------------------\n//\n").Append("// This SFX source file was generated by DotNetZip ")
							.Append(ZipFile.LibraryVersion.ToString())
							.Append("\n//         at ")
							.Append(DateTime.Now.ToString("yyyy MMMM dd  HH:mm:ss"))
							.Append("\n//\n// --------------------------------------------\n\n\n");
						bool flag13 = !string.IsNullOrEmpty(options.Description);
						if (flag13)
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"" + options.Description.Replace("\"", "") + "\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
						}
						bool flag14 = !string.IsNullOrEmpty(options.ProductVersion);
						if (flag14)
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyInformationalVersion(\"" + options.ProductVersion.Replace("\"", "") + "\")]\n");
						}
						string text9 = (string.IsNullOrEmpty(options.Copyright) ? "Extractor: Copyright © Dino Chiesa 2008-2011" : options.Copyright.Replace("\"", ""));
						bool flag15 = !string.IsNullOrEmpty(options.ProductName);
						if (flag15)
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"").Append(options.ProductName.Replace("\"", "")).Append("\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"DotNetZip\")]\n");
						}
						stringBuilder.Append("[assembly: System.Reflection.AssemblyCopyright(\"" + text9 + "\")]\n").Append(string.Format("[assembly: System.Reflection.AssemblyVersion(\"{0}\")]\n", ZipFile.LibraryVersion.ToString()));
						bool flag16 = options.FileVersion != null;
						if (flag16)
						{
							stringBuilder.Append(string.Format("[assembly: System.Reflection.AssemblyFileVersion(\"{0}\")]\n", options.FileVersion.ToString()));
						}
						stringBuilder.Append("\n\n\n");
						string text10 = options.DefaultExtractDirectory;
						bool flag17 = text10 != null;
						if (flag17)
						{
							text10 = text10.Replace("\"", "").Replace("\\", "\\\\");
						}
						string text11 = options.PostExtractCommandLine;
						bool flag18 = text11 != null;
						if (flag18)
						{
							text11 = text11.Replace("\\", "\\\\");
							text11 = text11.Replace("\"", "\\\"");
						}
						foreach (string text12 in extractorSettings.ResourcesToCompile)
						{
							using (Stream stream = zipFile[text12].OpenReader())
							{
								bool flag19 = stream == null;
								if (flag19)
								{
									throw new ZipException(string.Format("missing resource '{0}'", text12));
								}
								using (StreamReader streamReader = new StreamReader(stream))
								{
									while (streamReader.Peek() >= 0)
									{
										string text13 = streamReader.ReadLine();
										bool flag20 = text10 != null;
										if (flag20)
										{
											text13 = text13.Replace("@@EXTRACTLOCATION", text10);
										}
										text13 = text13.Replace("@@REMOVE_AFTER_EXECUTE", options.RemoveUnpackedFilesAfterExecute.ToString());
										text13 = text13.Replace("@@QUIET", options.Quiet.ToString());
										bool flag21 = !string.IsNullOrEmpty(options.SfxExeWindowTitle);
										if (flag21)
										{
											text13 = text13.Replace("@@SFX_EXE_WINDOW_TITLE", options.SfxExeWindowTitle);
										}
										text13 = text13.Replace("@@EXTRACT_EXISTING_FILE", ((int)options.ExtractExistingFile).ToString());
										bool flag22 = text11 != null;
										if (flag22)
										{
											text13 = text13.Replace("@@POST_UNPACK_CMD_LINE", text11);
										}
										stringBuilder.Append(text13).Append("\n");
									}
								}
								stringBuilder.Append("\n\n");
							}
						}
					}
					string text14 = stringBuilder.ToString();
					CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[] { text14 });
					bool flag23 = compilerResults == null;
					if (flag23)
					{
						throw new SfxGenerationException("Cannot compile the extraction logic!");
					}
					bool verbose3 = this.Verbose;
					if (verbose3)
					{
						foreach (string text15 in compilerResults.Output)
						{
							this.StatusMessageTextWriter.WriteLine(text15);
						}
					}
					bool flag24 = compilerResults.Errors.Count != 0;
					if (flag24)
					{
						using (TextWriter textWriter = new StreamWriter(text5))
						{
							textWriter.Write(text14);
							textWriter.Write("\n\n\n// ------------------------------------------------------------------\n");
							textWriter.Write("// Errors during compilation: \n//\n");
							string fileName = Path.GetFileName(text5);
							foreach (object obj in compilerResults.Errors)
							{
								CompilerError compilerError = (CompilerError)obj;
								textWriter.Write(string.Format("//   {0}({1},{2}): {3} {4}: {5}\n//\n", new object[]
								{
									fileName,
									compilerError.Line,
									compilerError.Column,
									compilerError.IsWarning ? "Warning" : "error",
									compilerError.ErrorNumber,
									compilerError.ErrorText
								}));
							}
						}
						throw new SfxGenerationException(string.Format("Errors compiling the extraction logic!  {0}", text5));
					}
					this.OnSaveEvent(ZipProgressEventType.Saving_AfterCompileSelfExtractor);
					using (Stream stream2 = File.OpenRead(text))
					{
						byte[] array = new byte[4000];
						int j = 1;
						while (j != 0)
						{
							j = stream2.Read(array, 0, array.Length);
							bool flag25 = j != 0;
							if (flag25)
							{
								this.WriteStream.Write(array, 0, j);
							}
						}
					}
				}
				this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
			}
			finally
			{
				try
				{
					bool flag26 = Directory.Exists(text2);
					if (flag26)
					{
						try
						{
							Directory.Delete(text2, true);
						}
						catch (IOException ex)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", ex);
						}
					}
					bool flag27 = File.Exists(text);
					if (flag27)
					{
						try
						{
							File.Delete(text);
						}
						catch (IOException ex2)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", ex2);
						}
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		internal static string GenerateTempPathname(string dir, string extension)
		{
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			string text3;
			do
			{
				string text = Guid.NewGuid().ToString();
				string text2 = string.Format("{0}-{1}-{2}.{3}", new object[]
				{
					name,
					DateTime.Now.ToString("yyyyMMMdd-HHmmss"),
					text,
					extension
				});
				text3 = Path.Combine(dir, text2);
			}
			while (File.Exists(text3) || Directory.Exists(text3));
			return text3;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001BC38 File Offset: 0x00019E38
		public void AddSelectedFiles(string selectionCriteria)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, false);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001BC4A File Offset: 0x00019E4A
		public void AddSelectedFiles(string selectionCriteria, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001BC5C File Offset: 0x00019E5C
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, false);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001BC6A File Offset: 0x00019E6A
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001BC78 File Offset: 0x00019E78
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, false);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001BC86 File Offset: 0x00019E86
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, false);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001BC96 File Offset: 0x00019E96
		public void UpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, true);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		private string EnsureendInSlash(string s)
		{
			bool flag = s.EndsWith("\\");
			string text;
			if (flag)
			{
				text = s;
			}
			else
			{
				text = s + "\\";
			}
			return text;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		private void _AddOrUpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories, bool wantUpdate)
		{
			bool flag = directoryOnDisk == null && Directory.Exists(selectionCriteria);
			if (flag)
			{
				directoryOnDisk = selectionCriteria;
				selectionCriteria = "*.*";
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(directoryOnDisk);
				if (flag2)
				{
					directoryOnDisk = ".";
				}
			}
			while (directoryOnDisk.EndsWith("\\"))
			{
				directoryOnDisk = directoryOnDisk.Substring(0, directoryOnDisk.Length - 1);
			}
			bool verbose = this.Verbose;
			if (verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding selection '{0}' from dir '{1}'...", selectionCriteria, directoryOnDisk);
			}
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			ReadOnlyCollection<string> readOnlyCollection = fileSelector.SelectFiles(directoryOnDisk, recurseDirectories);
			bool verbose2 = this.Verbose;
			if (verbose2)
			{
				this.StatusMessageTextWriter.WriteLine("found {0} files...", readOnlyCollection.Count);
			}
			this.OnAddStarted();
			AddOrUpdateAction addOrUpdateAction = (wantUpdate ? AddOrUpdateAction.AddOrUpdate : AddOrUpdateAction.AddOnly);
			foreach (string text in readOnlyCollection)
			{
				string text2 = ((directoryPathInArchive == null) ? null : ZipFile.ReplaceLeadingDirectory(Path.GetDirectoryName(text), directoryOnDisk, directoryPathInArchive));
				bool flag3 = File.Exists(text);
				if (flag3)
				{
					if (wantUpdate)
					{
						this.UpdateFile(text, text2);
					}
					else
					{
						this.AddFile(text, text2);
					}
				}
				else
				{
					this.AddOrUpdateDirectoryImpl(text, text2, addOrUpdateAction, false, 0);
				}
			}
			this.OnAddCompleted();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001BE48 File Offset: 0x0001A048
		private static string ReplaceLeadingDirectory(string original, string pattern, string replacement)
		{
			string text = original.ToUpper();
			string text2 = pattern.ToUpper();
			int num = text.IndexOf(text2);
			bool flag = num != 0;
			string text3;
			if (flag)
			{
				text3 = original;
			}
			else
			{
				text3 = replacement + original.Substring(text2.Length);
			}
			return text3;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001BE94 File Offset: 0x0001A094
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria, string directoryPathInArchive)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this, directoryPathInArchive);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		public int RemoveSelectedEntries(string selectionCriteria)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001BF0C File Offset: 0x0001A10C
		public int RemoveSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria, directoryPathInArchive);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001BF38 File Offset: 0x0001A138
		public void ExtractSelectedEntries(string selectionCriteria)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001BF98 File Offset: 0x0001A198
		public void ExtractSelectedEntries(string selectionCriteria, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractExistingFile);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001C060 File Offset: 0x0001A260
		public void ExtractSelectedEntries(string selectionCriteria, string directoryInArchive, string extractDirectory)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive, string extractDirectory, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory, extractExistingFile);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001C128 File Offset: 0x0001A328
		public IEnumerator<ZipEntry> GetEnumerator()
		{
			foreach (ZipEntry e in this._entries.Values)
			{
				yield return e;
				e = null;
			}
			Dictionary<string, ZipEntry>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ZipEntry>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001C138 File Offset: 0x0001A338
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001C150 File Offset: 0x0001A350
		[DispId(-4)]
		public IEnumerator GetNewEnum()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000233 RID: 563
		private TextWriter _StatusMessageTextWriter;

		// Token: 0x04000234 RID: 564
		private bool _CaseSensitiveRetrieval;

		// Token: 0x04000235 RID: 565
		private Stream _readstream;

		// Token: 0x04000236 RID: 566
		private Stream _writestream;

		// Token: 0x04000237 RID: 567
		private ushort _versionMadeBy;

		// Token: 0x04000238 RID: 568
		private ushort _versionNeededToExtract;

		// Token: 0x04000239 RID: 569
		private uint _diskNumberWithCd;

		// Token: 0x0400023A RID: 570
		private int _maxOutputSegmentSize;

		// Token: 0x0400023B RID: 571
		private uint _numberOfSegmentsForMostRecentSave;

		// Token: 0x0400023C RID: 572
		private ZipErrorAction _zipErrorAction;

		// Token: 0x0400023D RID: 573
		private bool _disposed;

		// Token: 0x0400023E RID: 574
		private Dictionary<string, ZipEntry> _entries;

		// Token: 0x0400023F RID: 575
		private List<ZipEntry> _zipEntriesAsList;

		// Token: 0x04000240 RID: 576
		private string _name;

		// Token: 0x04000241 RID: 577
		private string _readName;

		// Token: 0x04000242 RID: 578
		private string _Comment;

		// Token: 0x04000243 RID: 579
		internal string _Password;

		// Token: 0x04000244 RID: 580
		private bool _emitNtfsTimes = true;

		// Token: 0x04000245 RID: 581
		private bool _emitUnixTimes;

		// Token: 0x04000246 RID: 582
		private CompressionStrategy _Strategy = CompressionStrategy.Default;

		// Token: 0x04000247 RID: 583
		private CompressionMethod _compressionMethod = CompressionMethod.Deflate;

		// Token: 0x04000248 RID: 584
		private bool _fileAlreadyExists;

		// Token: 0x04000249 RID: 585
		private string _temporaryFileName;

		// Token: 0x0400024A RID: 586
		private bool _contentsChanged;

		// Token: 0x0400024B RID: 587
		private bool _hasBeenSaved;

		// Token: 0x0400024C RID: 588
		private string _TempFileFolder;

		// Token: 0x0400024D RID: 589
		private bool _ReadStreamIsOurs = true;

		// Token: 0x0400024E RID: 590
		private object LOCK = new object();

		// Token: 0x0400024F RID: 591
		private bool _saveOperationCanceled;

		// Token: 0x04000250 RID: 592
		private bool _extractOperationCanceled;

		// Token: 0x04000251 RID: 593
		private bool _addOperationCanceled;

		// Token: 0x04000252 RID: 594
		private EncryptionAlgorithm _Encryption;

		// Token: 0x04000253 RID: 595
		private bool _JustSaved;

		// Token: 0x04000254 RID: 596
		private long _locEndOfCDS = -1L;

		// Token: 0x04000255 RID: 597
		private uint _OffsetOfCentralDirectory;

		// Token: 0x04000256 RID: 598
		private long _OffsetOfCentralDirectory64;

		// Token: 0x04000257 RID: 599
		private bool? _OutputUsesZip64;

		// Token: 0x04000258 RID: 600
		internal bool _inExtractAll;

		// Token: 0x04000259 RID: 601
		private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x0400025A RID: 602
		private ZipOption _alternateEncodingUsage = ZipOption.Default;

		// Token: 0x0400025B RID: 603
		private static Encoding _defaultEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x0400025C RID: 604
		private int _BufferSize = ZipFile.BufferSizeDefault;

		// Token: 0x0400025D RID: 605
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x0400025E RID: 606
		private long _ParallelDeflateThreshold;

		// Token: 0x0400025F RID: 607
		private int _maxBufferPairs = 16;

		// Token: 0x04000260 RID: 608
		internal Zip64Option _zip64 = Zip64Option.Default;

		// Token: 0x04000261 RID: 609
		private bool _SavingSfx;

		// Token: 0x04000262 RID: 610
		public static readonly int BufferSizeDefault = 32768;

		// Token: 0x04000265 RID: 613
		private long _lengthOfReadStream = -99L;

		// Token: 0x04000269 RID: 617
		private static ZipFile.ExtractorSettings[] SettingsList = new ZipFile.ExtractorSettings[]
		{
			new ZipFile.ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.WinFormsApplication,
				ReferencedAssemblies = new List<string> { "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" },
				CopyThroughResources = new List<string> { "Ionic.Zip.WinFormsSelfExtractorStub.resources", "Ionic.Zip.Forms.PasswordDialog.resources", "Ionic.Zip.Forms.ZipContentsDialog.resources" },
				ResourcesToCompile = new List<string> { "WinFormsSelfExtractorStub.cs", "WinFormsSelfExtractorStub.Designer.cs", "PasswordDialog.cs", "PasswordDialog.Designer.cs", "ZipContentsDialog.cs", "ZipContentsDialog.Designer.cs", "FolderBrowserDialogEx.cs" }
			},
			new ZipFile.ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.ConsoleApplication,
				ReferencedAssemblies = new List<string> { "System.dll" },
				CopyThroughResources = null,
				ResourcesToCompile = new List<string> { "CommandLineSelfExtractorStub.cs" }
			}
		};

		// Token: 0x02000204 RID: 516
		private class ExtractorSettings
		{
			// Token: 0x04000D99 RID: 3481
			public SelfExtractorFlavor Flavor;

			// Token: 0x04000D9A RID: 3482
			public List<string> ReferencedAssemblies;

			// Token: 0x04000D9B RID: 3483
			public List<string> CopyThroughResources;

			// Token: 0x04000D9C RID: 3484
			public List<string> ResourcesToCompile;
		}
	}
}
