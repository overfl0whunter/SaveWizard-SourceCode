using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000072 RID: 114
	public class ZipFile : IEnumerable, IDisposable
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0002208C File Offset: 0x0002028C
		private void OnKeysRequired(string fileName)
		{
			bool flag = this.KeysRequired != null;
			if (flag)
			{
				KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, this.key);
				this.KeysRequired(this, keysRequiredEventArgs);
				this.key = keysRequiredEventArgs.Key;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000220D0 File Offset: 0x000202D0
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000220E8 File Offset: 0x000202E8
		private byte[] Key
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

		// Token: 0x1700015E RID: 350
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x000220F4 File Offset: 0x000202F4
		public string Password
		{
			set
			{
				bool flag = value == null || value.Length == 0;
				if (flag)
				{
					this.key = null;
				}
				else
				{
					this.rawPassword_ = value;
					this.key = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(value));
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002213C File Offset: 0x0002033C
		private bool HaveKeys
		{
			get
			{
				return this.key != null;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00022158 File Offset: 0x00020358
		public ZipFile(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			this.name_ = name;
			this.baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.isStreamOwner = true;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000221E4 File Offset: 0x000203E4
		public ZipFile(FileStream file)
		{
			bool flag = file == null;
			if (flag)
			{
				throw new ArgumentNullException("file");
			}
			bool flag2 = !file.CanSeek;
			if (flag2)
			{
				throw new ArgumentException("Stream is not seekable", "file");
			}
			this.baseStream_ = file;
			this.name_ = file.Name;
			this.isStreamOwner = true;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002228C File Offset: 0x0002048C
		public ZipFile(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag2 = !stream.CanSeek;
			if (flag2)
			{
				throw new ArgumentException("Stream is not seekable", "stream");
			}
			this.baseStream_ = stream;
			this.isStreamOwner = true;
			bool flag3 = this.baseStream_.Length > 0L;
			if (flag3)
			{
				try
				{
					this.ReadEntries();
				}
				catch
				{
					this.DisposeInternal(true);
					throw;
				}
			}
			else
			{
				this.entries_ = new ZipEntry[0];
				this.isNewArchive_ = true;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00022354 File Offset: 0x00020554
		internal ZipFile()
		{
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00022390 File Offset: 0x00020590
		~ZipFile()
		{
			this.Dispose(false);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000223C4 File Offset: 0x000205C4
		public void Close()
		{
			this.DisposeInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000223D8 File Offset: 0x000205D8
		public static ZipFile Create(string fileName)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = File.Create(fileName);
			return new ZipFile
			{
				name_ = fileName,
				baseStream_ = fileStream,
				isStreamOwner = true
			};
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00022424 File Offset: 0x00020624
		public static ZipFile Create(Stream outStream)
		{
			bool flag = outStream == null;
			if (flag)
			{
				throw new ArgumentNullException("outStream");
			}
			bool flag2 = !outStream.CanWrite;
			if (flag2)
			{
				throw new ArgumentException("Stream is not writeable", "outStream");
			}
			bool flag3 = !outStream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("Stream is not seekable", "outStream");
			}
			return new ZipFile
			{
				baseStream_ = outStream
			};
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00022498 File Offset: 0x00020698
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x000224B0 File Offset: 0x000206B0
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000224BC File Offset: 0x000206BC
		public bool IsEmbeddedArchive
		{
			get
			{
				return this.offsetOfFirstEntry > 0L;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000224D8 File Offset: 0x000206D8
		public bool IsNewArchive
		{
			get
			{
				return this.isNewArchive_;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000224F0 File Offset: 0x000206F0
		public string ZipFileComment
		{
			get
			{
				return this.comment_;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00022508 File Offset: 0x00020708
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00022520 File Offset: 0x00020720
		[Obsolete("Use the Count property instead")]
		public int Size
		{
			get
			{
				return this.entries_.Length;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002253C File Offset: 0x0002073C
		public long Count
		{
			get
			{
				return (long)this.entries_.Length;
			}
		}

		// Token: 0x17000167 RID: 359
		[IndexerName("EntryByIndex")]
		public ZipEntry this[int index]
		{
			get
			{
				return (ZipEntry)this.entries_[index].Clone();
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0002257C File Offset: 0x0002077C
		public IEnumerator GetEnumerator()
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			return new ZipFile.ZipEntryEnumerator(this.entries_);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000225B0 File Offset: 0x000207B0
		public int FindEntry(string name, bool ignoreCase)
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			for (int i = 0; i < this.entries_.Length; i++)
			{
				bool flag2 = string.Compare(name, this.entries_[i].Name, ignoreCase, CultureInfo.InvariantCulture) == 0;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002261C File Offset: 0x0002081C
		public ZipEntry GetEntry(string name)
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			int num = this.FindEntry(name, true);
			return (num >= 0) ? ((ZipEntry)this.entries_[num].Clone()) : null;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00022668 File Offset: 0x00020868
		public Stream GetInputStream(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			bool flag2 = this.isDisposed_;
			if (flag2)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			long num = entry.ZipFileIndex;
			bool flag3 = num < 0L || num >= (long)this.entries_.Length || this.entries_[(int)(checked((IntPtr)num))].Name != entry.Name;
			if (flag3)
			{
				num = (long)this.FindEntry(entry.Name, true);
				bool flag4 = num < 0L;
				if (flag4)
				{
					throw new ZipException("Entry cannot be found");
				}
			}
			return this.GetInputStream(num);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00022710 File Offset: 0x00020910
		public Stream GetInputStream(long entryIndex)
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			checked
			{
				long num = this.LocateEntry(this.entries_[(int)((IntPtr)entryIndex)]);
				CompressionMethod compressionMethod = this.entries_[(int)((IntPtr)entryIndex)].CompressionMethod;
				Stream stream = new ZipFile.PartialInputStream(this, num, this.entries_[(int)((IntPtr)entryIndex)].CompressedSize);
				bool isCrypted = this.entries_[(int)((IntPtr)entryIndex)].IsCrypted;
				if (isCrypted)
				{
					stream = this.CreateAndInitDecryptionStream(stream, this.entries_[(int)((IntPtr)entryIndex)]);
					bool flag2 = stream == null;
					if (flag2)
					{
						throw new ZipException("Unable to decrypt this entry");
					}
				}
				CompressionMethod compressionMethod2 = compressionMethod;
				if (compressionMethod2 != CompressionMethod.Stored)
				{
					if (compressionMethod2 != CompressionMethod.Deflated)
					{
						throw new ZipException("Unsupported compression method " + compressionMethod);
					}
					stream = new InflaterInputStream(stream, new Inflater(true));
				}
				return stream;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000227E4 File Offset: 0x000209E4
		public bool TestArchive(bool testData)
		{
			return this.TestArchive(testData, TestStrategy.FindFirstError, null);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00022800 File Offset: 0x00020A00
		public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			TestStatus testStatus = new TestStatus(this);
			bool flag2 = resultHandler != null;
			if (flag2)
			{
				resultHandler(testStatus, null);
			}
			ZipFile.HeaderTest headerTest = (testData ? (ZipFile.HeaderTest.Extract | ZipFile.HeaderTest.Header) : ZipFile.HeaderTest.Header);
			bool flag3 = true;
			try
			{
				int num = 0;
				while (flag3 && (long)num < this.Count)
				{
					bool flag4 = resultHandler != null;
					if (flag4)
					{
						testStatus.SetEntry(this[num]);
						testStatus.SetOperation(TestOperation.EntryHeader);
						resultHandler(testStatus, null);
					}
					try
					{
						this.TestLocalHeader(this[num], headerTest);
					}
					catch (ZipException ex)
					{
						testStatus.AddError();
						bool flag5 = resultHandler != null;
						if (flag5)
						{
							resultHandler(testStatus, string.Format("Exception during test - '{0}'", ex.Message));
						}
						bool flag6 = strategy == TestStrategy.FindFirstError;
						if (flag6)
						{
							flag3 = false;
						}
					}
					bool flag7 = flag3 && testData && this[num].IsFile;
					if (flag7)
					{
						bool flag8 = resultHandler != null;
						if (flag8)
						{
							testStatus.SetOperation(TestOperation.EntryData);
							resultHandler(testStatus, null);
						}
						Crc32 crc = new Crc32();
						using (Stream inputStream = this.GetInputStream(this[num]))
						{
							byte[] array = new byte[4096];
							long num2 = 0L;
							int num3;
							while ((num3 = inputStream.Read(array, 0, array.Length)) > 0)
							{
								crc.Update(array, 0, num3);
								bool flag9 = resultHandler != null;
								if (flag9)
								{
									num2 += (long)num3;
									testStatus.SetBytesTested(num2);
									resultHandler(testStatus, null);
								}
							}
						}
						bool flag10 = this[num].Crc != crc.Value;
						if (flag10)
						{
							testStatus.AddError();
							bool flag11 = resultHandler != null;
							if (flag11)
							{
								resultHandler(testStatus, "CRC mismatch");
							}
							bool flag12 = strategy == TestStrategy.FindFirstError;
							if (flag12)
							{
								flag3 = false;
							}
						}
						bool flag13 = (this[num].Flags & 8) != 0;
						if (flag13)
						{
							ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_);
							DescriptorData descriptorData = new DescriptorData();
							zipHelperStream.ReadDataDescriptor(this[num].LocalHeaderRequiresZip64, descriptorData);
							bool flag14 = this[num].Crc != descriptorData.Crc;
							if (flag14)
							{
								testStatus.AddError();
							}
							bool flag15 = this[num].CompressedSize != descriptorData.CompressedSize;
							if (flag15)
							{
								testStatus.AddError();
							}
							bool flag16 = this[num].Size != descriptorData.Size;
							if (flag16)
							{
								testStatus.AddError();
							}
						}
					}
					bool flag17 = resultHandler != null;
					if (flag17)
					{
						testStatus.SetOperation(TestOperation.EntryComplete);
						resultHandler(testStatus, null);
					}
					num++;
				}
				bool flag18 = resultHandler != null;
				if (flag18)
				{
					testStatus.SetOperation(TestOperation.MiscellaneousTests);
					resultHandler(testStatus, null);
				}
			}
			catch (Exception ex2)
			{
				testStatus.AddError();
				bool flag19 = resultHandler != null;
				if (flag19)
				{
					resultHandler(testStatus, string.Format("Exception during test - '{0}'", ex2.Message));
				}
			}
			bool flag20 = resultHandler != null;
			if (flag20)
			{
				testStatus.SetOperation(TestOperation.Complete);
				testStatus.SetEntry(null);
				resultHandler(testStatus, null);
			}
			return testStatus.ErrorCount == 0;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00022BBC File Offset: 0x00020DBC
		private long TestLocalHeader(ZipEntry entry, ZipFile.HeaderTest tests)
		{
			Stream stream = this.baseStream_;
			long num12;
			lock (stream)
			{
				bool flag2 = (tests & ZipFile.HeaderTest.Header) > (ZipFile.HeaderTest)0;
				bool flag3 = (tests & ZipFile.HeaderTest.Extract) > (ZipFile.HeaderTest)0;
				this.baseStream_.Seek(this.offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
				bool flag4 = this.ReadLEUint() != 67324752U;
				if (flag4)
				{
					throw new ZipException(string.Format("Wrong local header signature @{0:X}", this.offsetOfFirstEntry + entry.Offset));
				}
				short num = (short)this.ReadLEUshort();
				short num2 = (short)this.ReadLEUshort();
				short num3 = (short)this.ReadLEUshort();
				short num4 = (short)this.ReadLEUshort();
				short num5 = (short)this.ReadLEUshort();
				uint num6 = this.ReadLEUint();
				long num7 = (long)((ulong)this.ReadLEUint());
				long num8 = (long)((ulong)this.ReadLEUint());
				int num9 = (int)this.ReadLEUshort();
				int num10 = (int)this.ReadLEUshort();
				byte[] array = new byte[num9];
				StreamUtils.ReadFully(this.baseStream_, array);
				byte[] array2 = new byte[num10];
				StreamUtils.ReadFully(this.baseStream_, array2);
				ZipExtraData zipExtraData = new ZipExtraData(array2);
				bool flag5 = zipExtraData.Find(1);
				if (flag5)
				{
					num8 = zipExtraData.ReadLong();
					num7 = zipExtraData.ReadLong();
					bool flag6 = (num2 & 8) != 0;
					if (flag6)
					{
						bool flag7 = num8 != -1L && num8 != entry.Size;
						if (flag7)
						{
							throw new ZipException("Size invalid for descriptor");
						}
						bool flag8 = num7 != -1L && num7 != entry.CompressedSize;
						if (flag8)
						{
							throw new ZipException("Compressed size invalid for descriptor");
						}
					}
				}
				else
				{
					bool flag9 = num >= 45 && ((uint)num8 == uint.MaxValue || (uint)num7 == uint.MaxValue);
					if (flag9)
					{
						throw new ZipException("Required Zip64 extended information missing");
					}
				}
				bool flag10 = flag3;
				if (flag10)
				{
					bool isFile = entry.IsFile;
					if (isFile)
					{
						bool flag11 = !entry.IsCompressionMethodSupported();
						if (flag11)
						{
							throw new ZipException("Compression method not supported");
						}
						bool flag12 = num > 51 || (num > 20 && num < 45);
						if (flag12)
						{
							throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", num));
						}
						bool flag13 = (num2 & 12384) != 0;
						if (flag13)
						{
							throw new ZipException("The library does not support the zip version required to extract this entry");
						}
					}
				}
				bool flag14 = flag2;
				if (flag14)
				{
					bool flag15 = num <= 63 && num != 10 && num != 11 && num != 20 && num != 21 && num != 25 && num != 27 && num != 45 && num != 46 && num != 50 && num != 51 && num != 52 && num != 61 && num != 62 && num != 63;
					if (flag15)
					{
						throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", num));
					}
					bool flag16 = ((int)num2 & 49168) != 0;
					if (flag16)
					{
						throw new ZipException("Reserved bit flags cannot be set.");
					}
					bool flag17 = (num2 & 1) != 0 && num < 20;
					if (flag17)
					{
						throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
					}
					bool flag18 = (num2 & 64) != 0;
					if (flag18)
					{
						bool flag19 = (num2 & 1) == 0;
						if (flag19)
						{
							throw new ZipException("Strong encryption flag set but encryption flag is not set");
						}
						bool flag20 = num < 50;
						if (flag20)
						{
							throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
						}
					}
					bool flag21 = (num2 & 32) != 0 && num < 27;
					if (flag21)
					{
						throw new ZipException(string.Format("Patched data requires higher version than ({0})", num));
					}
					bool flag22 = (int)num2 != entry.Flags;
					if (flag22)
					{
						throw new ZipException("Central header/local header flags mismatch");
					}
					bool flag23 = entry.CompressionMethod != (CompressionMethod)num3;
					if (flag23)
					{
						throw new ZipException("Central header/local header compression method mismatch");
					}
					bool flag24 = entry.Version != (int)num;
					if (flag24)
					{
						throw new ZipException("Extract version mismatch");
					}
					bool flag25 = (num2 & 64) != 0;
					if (flag25)
					{
						bool flag26 = num < 62;
						if (flag26)
						{
							throw new ZipException("Strong encryption flag set but version not high enough");
						}
					}
					bool flag27 = (num2 & 8192) != 0;
					if (flag27)
					{
						bool flag28 = num4 != 0 || num5 != 0;
						if (flag28)
						{
							throw new ZipException("Header masked set but date/time values non-zero");
						}
					}
					bool flag29 = (num2 & 8) == 0;
					if (flag29)
					{
						bool flag30 = num6 != (uint)entry.Crc;
						if (flag30)
						{
							throw new ZipException("Central header/local header crc mismatch");
						}
					}
					bool flag31 = num8 == 0L && num7 == 0L;
					if (flag31)
					{
						bool flag32 = num6 > 0U;
						if (flag32)
						{
							throw new ZipException("Invalid CRC for empty entry");
						}
					}
					bool flag33 = entry.Name.Length > num9;
					if (flag33)
					{
						throw new ZipException("File name length mismatch");
					}
					string text = ZipConstants.ConvertToStringExt((int)num2, array);
					bool flag34 = text != entry.Name;
					if (flag34)
					{
						throw new ZipException("Central header and local header file name mismatch");
					}
					bool isDirectory = entry.IsDirectory;
					if (isDirectory)
					{
						bool flag35 = num8 > 0L;
						if (flag35)
						{
							throw new ZipException("Directory cannot have size");
						}
						bool isCrypted = entry.IsCrypted;
						if (isCrypted)
						{
							bool flag36 = num7 > 14L;
							if (flag36)
							{
								throw new ZipException("Directory compressed size invalid");
							}
						}
						else
						{
							bool flag37 = num7 > 2L;
							if (flag37)
							{
								throw new ZipException("Directory compressed size invalid");
							}
						}
					}
					bool flag38 = !ZipNameTransform.IsValidName(text, true);
					if (flag38)
					{
						throw new ZipException("Name is invalid");
					}
				}
				bool flag39 = (num2 & 8) == 0 || num8 > 0L || num7 > 0L;
				if (flag39)
				{
					bool flag40 = num8 != entry.Size;
					if (flag40)
					{
						throw new ZipException(string.Format("Size mismatch between central header({0}) and local header({1})", entry.Size, num8));
					}
					bool flag41 = num7 != entry.CompressedSize && num7 != (long)((ulong)(-1)) && num7 != -1L;
					if (flag41)
					{
						throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", entry.CompressedSize, num7));
					}
				}
				int num11 = num9 + num10;
				num12 = this.offsetOfFirstEntry + entry.Offset + 30L + (long)num11;
			}
			return num12;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00023214 File Offset: 0x00021414
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00023231 File Offset: 0x00021431
		public INameTransform NameTransform
		{
			get
			{
				return this.updateEntryFactory_.NameTransform;
			}
			set
			{
				this.updateEntryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00023244 File Offset: 0x00021444
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0002325C File Offset: 0x0002145C
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.updateEntryFactory_;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.updateEntryFactory_ = new ZipEntryFactory();
				}
				else
				{
					this.updateEntryFactory_ = value;
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0002328C File Offset: 0x0002148C
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000232A4 File Offset: 0x000214A4
		public int BufferSize
		{
			get
			{
				return this.bufferSize_;
			}
			set
			{
				bool flag = value < 1024;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
				}
				bool flag2 = this.bufferSize_ != value;
				if (flag2)
				{
					this.bufferSize_ = value;
					this.copyBuffer_ = null;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000232F0 File Offset: 0x000214F0
		public bool IsUpdating
		{
			get
			{
				return this.updates_ != null;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002330C File Offset: 0x0002150C
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00023324 File Offset: 0x00021524
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

		// Token: 0x0600055D RID: 1373 RVA: 0x00023330 File Offset: 0x00021530
		public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
		{
			bool flag = archiveStorage == null;
			if (flag)
			{
				throw new ArgumentNullException("archiveStorage");
			}
			bool flag2 = dataSource == null;
			if (flag2)
			{
				throw new ArgumentNullException("dataSource");
			}
			bool flag3 = this.isDisposed_;
			if (flag3)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			bool isEmbeddedArchive = this.IsEmbeddedArchive;
			if (isEmbeddedArchive)
			{
				throw new ZipException("Cannot update embedded/SFX archives");
			}
			this.archiveStorage_ = archiveStorage;
			this.updateDataSource_ = dataSource;
			this.updateIndex_ = new Hashtable();
			this.updates_ = new ArrayList(this.entries_.Length);
			foreach (ZipEntry zipEntry in this.entries_)
			{
				int num = this.updates_.Add(new ZipFile.ZipUpdate(zipEntry));
				this.updateIndex_.Add(zipEntry.Name, num);
			}
			this.updates_.Sort(new ZipFile.UpdateComparer());
			int num2 = 0;
			foreach (object obj in this.updates_)
			{
				ZipFile.ZipUpdate zipUpdate = (ZipFile.ZipUpdate)obj;
				bool flag4 = num2 == this.updates_.Count - 1;
				if (flag4)
				{
					break;
				}
				zipUpdate.OffsetBasedSize = ((ZipFile.ZipUpdate)this.updates_[num2 + 1]).Entry.Offset - zipUpdate.Entry.Offset;
				num2++;
			}
			this.updateCount_ = (long)this.updates_.Count;
			this.contentsEdited_ = false;
			this.commentEdited_ = false;
			this.newComment_ = null;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000234EC File Offset: 0x000216EC
		public void BeginUpdate(IArchiveStorage archiveStorage)
		{
			this.BeginUpdate(archiveStorage, new DynamicDiskDataSource());
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000234FC File Offset: 0x000216FC
		public void BeginUpdate()
		{
			bool flag = this.Name == null;
			if (flag)
			{
				this.BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
			}
			else
			{
				this.BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00023540 File Offset: 0x00021740
		public void CommitUpdate()
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			try
			{
				this.updateIndex_.Clear();
				this.updateIndex_ = null;
				bool flag2 = this.contentsEdited_;
				if (flag2)
				{
					this.RunUpdates();
				}
				else
				{
					bool flag3 = this.commentEdited_;
					if (flag3)
					{
						this.UpdateCommentOnly();
					}
					else
					{
						bool flag4 = this.entries_.Length == 0;
						if (flag4)
						{
							byte[] array = ((this.newComment_ != null) ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_));
							using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
							{
								zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, array);
							}
						}
					}
				}
			}
			finally
			{
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00023634 File Offset: 0x00021834
		public void AbortUpdate()
		{
			this.PostUpdateCleanup();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00023640 File Offset: 0x00021840
		public void SetComment(string comment)
		{
			bool flag = this.isDisposed_;
			if (flag)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			this.newComment_ = new ZipFile.ZipString(comment);
			bool flag2 = this.newComment_.RawLength > 65535;
			if (flag2)
			{
				this.newComment_ = null;
				throw new ZipException("Comment length exceeds maximum - 65535");
			}
			this.commentEdited_ = true;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000236A8 File Offset: 0x000218A8
		private void AddUpdate(ZipFile.ZipUpdate update)
		{
			this.contentsEdited_ = true;
			int num = this.FindExistingUpdate(update.Entry.Name);
			bool flag = num >= 0;
			if (flag)
			{
				bool flag2 = this.updates_[num] == null;
				if (flag2)
				{
					this.updateCount_ += 1L;
				}
				this.updates_[num] = update;
			}
			else
			{
				num = this.updates_.Add(update);
				this.updateCount_ += 1L;
				this.updateIndex_.Add(update.Entry.Name, num);
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002374C File Offset: 0x0002194C
		public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			bool flag2 = this.isDisposed_;
			if (flag2)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			bool flag3 = !ZipEntry.IsCompressionMethodSupported(compressionMethod);
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("compressionMethod");
			}
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000237D8 File Offset: 0x000219D8
		public void Add(string fileName, CompressionMethod compressionMethod)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			bool flag2 = !ZipEntry.IsCompressionMethodSupported(compressionMethod);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("compressionMethod");
			}
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00023844 File Offset: 0x00021A44
		public void Add(string fileName)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00023888 File Offset: 0x00021A88
		public void Add(string fileName, string entryName)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			bool flag2 = entryName == null;
			if (flag2)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(entryName)));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000238E0 File Offset: 0x00021AE0
		public void Add(IStaticDataSource dataSource, string entryName)
		{
			bool flag = dataSource == null;
			if (flag)
			{
				throw new ArgumentNullException("dataSource");
			}
			bool flag2 = entryName == null;
			if (flag2)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, this.EntryFactory.MakeFileEntry(entryName, false)));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00023938 File Offset: 0x00021B38
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
		{
			bool flag = dataSource == null;
			if (flag)
			{
				throw new ArgumentNullException("dataSource");
			}
			bool flag2 = entryName == null;
			if (flag2)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002399C File Offset: 0x00021B9C
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			bool flag = dataSource == null;
			if (flag)
			{
				throw new ArgumentNullException("dataSource");
			}
			bool flag2 = entryName == null;
			if (flag2)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00023A08 File Offset: 0x00021C08
		public void Add(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			bool flag2 = entry.Size != 0L || entry.CompressedSize != 0L;
			if (flag2)
			{
				throw new ZipException("Entry cannot have any data");
			}
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00023A64 File Offset: 0x00021C64
		public void AddDirectory(string directoryName)
		{
			bool flag = directoryName == null;
			if (flag)
			{
				throw new ArgumentNullException("directoryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeDirectoryEntry(directoryName);
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, zipEntry));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public bool Delete(string fileName)
		{
			bool flag = fileName == null;
			if (flag)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(fileName);
			bool flag2 = num >= 0 && this.updates_[num] != null;
			if (flag2)
			{
				bool flag3 = true;
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return flag3;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00023B34 File Offset: 0x00021D34
		public void Delete(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(entry);
			bool flag2 = num >= 0;
			if (flag2)
			{
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00023BA4 File Offset: 0x00021DA4
		private void WriteLEShort(int value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)((value >> 8) & 255));
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00023BD1 File Offset: 0x00021DD1
		private void WriteLEUshort(ushort value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00023BF8 File Offset: 0x00021DF8
		private void WriteLEInt(int value)
		{
			this.WriteLEShort(value & 65535);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00023C14 File Offset: 0x00021E14
		private void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00023C32 File Offset: 0x00021E32
		private void WriteLeLong(long value)
		{
			this.WriteLEInt((int)(value & (long)((ulong)(-1))));
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00023C4D File Offset: 0x00021E4D
		private void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)(-1)));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00023C68 File Offset: 0x00021E68
		private void WriteLocalEntryHeader(ZipFile.ZipUpdate update)
		{
			ZipEntry outEntry = update.OutEntry;
			outEntry.Offset = this.baseStream_.Position;
			bool flag = update.Command > ZipFile.UpdateCommand.Copy;
			if (flag)
			{
				bool flag2 = outEntry.CompressionMethod == CompressionMethod.Deflated;
				if (flag2)
				{
					bool flag3 = outEntry.Size == 0L;
					if (flag3)
					{
						outEntry.CompressedSize = outEntry.Size;
						outEntry.Crc = 0L;
						outEntry.CompressionMethod = CompressionMethod.Stored;
					}
				}
				else
				{
					bool flag4 = outEntry.CompressionMethod == CompressionMethod.Stored;
					if (flag4)
					{
						outEntry.Flags &= -9;
					}
				}
				bool haveKeys = this.HaveKeys;
				if (haveKeys)
				{
					outEntry.IsCrypted = true;
					bool flag5 = outEntry.Crc < 0L;
					if (flag5)
					{
						outEntry.Flags |= 8;
					}
				}
				else
				{
					outEntry.IsCrypted = false;
				}
				switch (this.useZip64_)
				{
				case UseZip64.On:
					outEntry.ForceZip64();
					break;
				case UseZip64.Dynamic:
				{
					bool flag6 = outEntry.Size < 0L;
					if (flag6)
					{
						outEntry.ForceZip64();
					}
					break;
				}
				}
			}
			this.WriteLEInt(67324752);
			this.WriteLEShort(outEntry.Version);
			this.WriteLEShort(outEntry.Flags);
			this.WriteLEShort((int)((byte)outEntry.CompressionMethod));
			this.WriteLEInt((int)outEntry.DosTime);
			bool flag7 = !outEntry.HasCrc;
			if (flag7)
			{
				update.CrcPatchOffset = this.baseStream_.Position;
				this.WriteLEInt(0);
			}
			else
			{
				this.WriteLEInt((int)outEntry.Crc);
			}
			bool localHeaderRequiresZip = outEntry.LocalHeaderRequiresZip64;
			if (localHeaderRequiresZip)
			{
				this.WriteLEInt(-1);
				this.WriteLEInt(-1);
			}
			else
			{
				bool flag8 = outEntry.CompressedSize < 0L || outEntry.Size < 0L;
				if (flag8)
				{
					update.SizePatchOffset = this.baseStream_.Position;
				}
				this.WriteLEInt((int)outEntry.CompressedSize);
				this.WriteLEInt((int)outEntry.Size);
			}
			byte[] array = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
			bool flag9 = array.Length > 65535;
			if (flag9)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(outEntry.ExtraData);
			bool localHeaderRequiresZip2 = outEntry.LocalHeaderRequiresZip64;
			if (localHeaderRequiresZip2)
			{
				zipExtraData.StartNewEntry();
				zipExtraData.AddLeLong(outEntry.Size);
				zipExtraData.AddLeLong(outEntry.CompressedSize);
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			outEntry.ExtraData = zipExtraData.GetEntryData();
			this.WriteLEShort(array.Length);
			this.WriteLEShort(outEntry.ExtraData.Length);
			bool flag10 = array.Length != 0;
			if (flag10)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			bool localHeaderRequiresZip3 = outEntry.LocalHeaderRequiresZip64;
			if (localHeaderRequiresZip3)
			{
				bool flag11 = !zipExtraData.Find(1);
				if (flag11)
				{
					throw new ZipException("Internal error cannot find extra data");
				}
				update.SizePatchOffset = this.baseStream_.Position + (long)zipExtraData.CurrentReadIndex;
			}
			bool flag12 = outEntry.ExtraData.Length != 0;
			if (flag12)
			{
				this.baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00023FAC File Offset: 0x000221AC
		private int WriteCentralDirectoryHeader(ZipEntry entry)
		{
			bool flag = entry.CompressedSize < 0L;
			if (flag)
			{
				throw new ZipException("Attempt to write central directory entry with unknown csize");
			}
			bool flag2 = entry.Size < 0L;
			if (flag2)
			{
				throw new ZipException("Attempt to write central directory entry with unknown size");
			}
			bool flag3 = entry.Crc < 0L;
			if (flag3)
			{
				throw new ZipException("Attempt to write central directory entry with unknown crc");
			}
			this.WriteLEInt(33639248);
			this.WriteLEShort(51);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)entry.CompressionMethod));
			this.WriteLEInt((int)entry.DosTime);
			this.WriteLEInt((int)entry.Crc);
			bool flag4 = entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)(-1));
			if (flag4)
			{
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)(entry.CompressedSize & (long)((ulong)(-1))));
			}
			bool flag5 = entry.IsZip64Forced() || entry.Size >= (long)((ulong)(-1));
			if (flag5)
			{
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)entry.Size);
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			bool flag6 = array.Length > 65535;
			if (flag6)
			{
				throw new ZipException("Entry name is too long.");
			}
			this.WriteLEShort(array.Length);
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			bool centralHeaderRequiresZip = entry.CentralHeaderRequiresZip64;
			if (centralHeaderRequiresZip)
			{
				zipExtraData.StartNewEntry();
				bool flag7 = entry.Size >= (long)((ulong)(-1)) || this.useZip64_ == UseZip64.On;
				if (flag7)
				{
					zipExtraData.AddLeLong(entry.Size);
				}
				bool flag8 = entry.CompressedSize >= (long)((ulong)(-1)) || this.useZip64_ == UseZip64.On;
				if (flag8)
				{
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				bool flag9 = entry.Offset >= (long)((ulong)(-1));
				if (flag9)
				{
					zipExtraData.AddLeLong(entry.Offset);
				}
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLEShort(entryData.Length);
			this.WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			bool flag10 = entry.ExternalFileAttributes != -1;
			if (flag10)
			{
				this.WriteLEInt(entry.ExternalFileAttributes);
			}
			else
			{
				bool isDirectory = entry.IsDirectory;
				if (isDirectory)
				{
					this.WriteLEUint(16U);
				}
				else
				{
					this.WriteLEUint(0U);
				}
			}
			bool flag11 = entry.Offset >= (long)((ulong)(-1));
			if (flag11)
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEUint((uint)((int)entry.Offset));
			}
			bool flag12 = array.Length != 0;
			if (flag12)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			bool flag13 = entryData.Length != 0;
			if (flag13)
			{
				this.baseStream_.Write(entryData, 0, entryData.Length);
			}
			byte[] array2 = ((entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0]);
			bool flag14 = array2.Length != 0;
			if (flag14)
			{
				this.baseStream_.Write(array2, 0, array2.Length);
			}
			return 46 + array.Length + entryData.Length + array2.Length;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002430C File Offset: 0x0002250C
		private void PostUpdateCleanup()
		{
			this.updateDataSource_ = null;
			this.updates_ = null;
			this.updateIndex_ = null;
			bool flag = this.archiveStorage_ != null;
			if (flag)
			{
				this.archiveStorage_.Dispose();
				this.archiveStorage_ = null;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00024354 File Offset: 0x00022554
		private string GetTransformedFileName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			return (nameTransform != null) ? nameTransform.TransformFile(name) : name;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002437C File Offset: 0x0002257C
		private string GetTransformedDirectoryName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			return (nameTransform != null) ? nameTransform.TransformDirectory(name) : name;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000243A4 File Offset: 0x000225A4
		private byte[] GetBuffer()
		{
			bool flag = this.copyBuffer_ == null;
			if (flag)
			{
				this.copyBuffer_ = new byte[this.bufferSize_];
			}
			return this.copyBuffer_;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000243DC File Offset: 0x000225DC
		private void CopyDescriptorBytes(ZipFile.ZipUpdate update, Stream dest, Stream source)
		{
			int i = this.GetDescriptorSize(update);
			bool flag = i > 0;
			if (flag)
			{
				byte[] buffer = this.GetBuffer();
				while (i > 0)
				{
					int num = Math.Min(buffer.Length, i);
					int num2 = source.Read(buffer, 0, num);
					bool flag2 = num2 > 0;
					if (!flag2)
					{
						throw new ZipException("Unxpected end of stream");
					}
					dest.Write(buffer, 0, num2);
					i -= num2;
				}
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00024454 File Offset: 0x00022654
		private void CopyBytes(ZipFile.ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
		{
			bool flag = destination == source;
			if (flag)
			{
				throw new InvalidOperationException("Destination and source are the same");
			}
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num = bytesToCopy;
			long num2 = 0L;
			int num4;
			do
			{
				int num3 = buffer.Length;
				bool flag2 = bytesToCopy < (long)num3;
				if (flag2)
				{
					num3 = (int)bytesToCopy;
				}
				num4 = source.Read(buffer, 0, num3);
				bool flag3 = num4 > 0;
				if (flag3)
				{
					if (updateCrc)
					{
						crc.Update(buffer, 0, num4);
					}
					destination.Write(buffer, 0, num4);
					bytesToCopy -= (long)num4;
					num2 += (long)num4;
				}
			}
			while (num4 > 0 && bytesToCopy > 0L);
			bool flag4 = num2 != num;
			if (flag4)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num, num2));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00024548 File Offset: 0x00022748
		private int GetDescriptorSize(ZipFile.ZipUpdate update)
		{
			int num = 0;
			bool flag = (update.Entry.Flags & 8) != 0;
			if (flag)
			{
				num = 12;
				bool localHeaderRequiresZip = update.Entry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip)
				{
					num = 20;
				}
			}
			return num;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002458C File Offset: 0x0002278C
		private void CopyDescriptorBytesDirect(ZipFile.ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
		{
			int i = this.GetDescriptorSize(update);
			while (i > 0)
			{
				int num = i;
				byte[] buffer = this.GetBuffer();
				stream.Position = sourcePosition;
				int num2 = stream.Read(buffer, 0, num);
				bool flag = num2 > 0;
				if (!flag)
				{
					throw new ZipException("Unxpected end of stream");
				}
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num2);
				i -= num2;
				destinationPosition += (long)num2;
				sourcePosition += (long)num2;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002460C File Offset: 0x0002280C
		private void CopyEntryDataDirect(ZipFile.ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
		{
			long num = update.Entry.CompressedSize;
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num2 = num;
			long num3 = 0L;
			int num5;
			do
			{
				int num4 = buffer.Length;
				bool flag = num < (long)num4;
				if (flag)
				{
					num4 = (int)num;
				}
				stream.Position = sourcePosition;
				num5 = stream.Read(buffer, 0, num4);
				bool flag2 = num5 > 0;
				if (flag2)
				{
					if (updateCrc)
					{
						crc.Update(buffer, 0, num5);
					}
					stream.Position = destinationPosition;
					stream.Write(buffer, 0, num5);
					destinationPosition += (long)num5;
					sourcePosition += (long)num5;
					num -= (long)num5;
					num3 += (long)num5;
				}
			}
			while (num5 > 0 && num > 0L);
			bool flag3 = num3 != num2;
			if (flag3)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num2, num3));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00024720 File Offset: 0x00022920
		private int FindExistingUpdate(ZipEntry entry)
		{
			int num = -1;
			string transformedFileName = this.GetTransformedFileName(entry.Name);
			bool flag = this.updateIndex_.ContainsKey(transformedFileName);
			if (flag)
			{
				num = (int)this.updateIndex_[transformedFileName];
			}
			return num;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00024768 File Offset: 0x00022968
		private int FindExistingUpdate(string fileName)
		{
			int num = -1;
			string transformedFileName = this.GetTransformedFileName(fileName);
			bool flag = this.updateIndex_.ContainsKey(transformedFileName);
			if (flag)
			{
				num = (int)this.updateIndex_[transformedFileName];
			}
			return num;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000247AC File Offset: 0x000229AC
		private Stream GetOutputStream(ZipEntry entry)
		{
			Stream stream = this.baseStream_;
			bool isCrypted = entry.IsCrypted;
			if (isCrypted)
			{
				stream = this.CreateAndInitEncryptionStream(stream, entry);
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod != CompressionMethod.Deflated)
				{
					throw new ZipException("Unknown compression method " + entry.CompressionMethod);
				}
				stream = new DeflaterOutputStream(stream, new Deflater(9, true))
				{
					IsStreamOwner = false
				};
			}
			else
			{
				stream = new ZipFile.UncompressedStream(stream);
			}
			return stream;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00024830 File Offset: 0x00022A30
		private void AddEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			Stream stream = null;
			bool isFile = update.Entry.IsFile;
			if (isFile)
			{
				stream = update.GetSource();
				bool flag = stream == null;
				if (flag)
				{
					stream = this.updateDataSource_.GetSource(update.Entry, update.Filename);
				}
			}
			bool flag2 = stream != null;
			if (flag2)
			{
				using (stream)
				{
					long length = stream.Length;
					bool flag3 = update.OutEntry.Size < 0L;
					if (flag3)
					{
						update.OutEntry.Size = length;
					}
					else
					{
						bool flag4 = update.OutEntry.Size != length;
						if (flag4)
						{
							throw new ZipException("Entry size/stream size mismatch");
						}
					}
					workFile.WriteLocalEntryHeader(update);
					long position = workFile.baseStream_.Position;
					using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
					{
						this.CopyBytes(update, outputStream, stream, length, true);
					}
					long position2 = workFile.baseStream_.Position;
					update.OutEntry.CompressedSize = position2 - position;
					bool flag5 = (update.OutEntry.Flags & 8) == 8;
					if (flag5)
					{
						ZipHelperStream zipHelperStream = new ZipHelperStream(workFile.baseStream_);
						zipHelperStream.WriteDataDescriptor(update.OutEntry);
					}
				}
			}
			else
			{
				workFile.WriteLocalEntryHeader(update);
				update.OutEntry.CompressedSize = 0L;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000249BC File Offset: 0x00022BBC
		private void ModifyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			long position = workFile.baseStream_.Position;
			bool flag = update.Entry.IsFile && update.Filename != null;
			if (flag)
			{
				using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
				{
					using (Stream inputStream = this.GetInputStream(update.Entry))
					{
						this.CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
					}
				}
			}
			long position2 = workFile.baseStream_.Position;
			update.Entry.CompressedSize = position2 - position;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00024A84 File Offset: 0x00022C84
		private void CopyEntryDirect(ZipFile workFile, ZipFile.ZipUpdate update, ref long destinationPosition)
		{
			bool flag = false;
			bool flag2 = update.Entry.Offset == destinationPosition;
			if (flag2)
			{
				flag = true;
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.baseStream_.Position = destinationPosition;
				workFile.WriteLocalEntryHeader(update);
				destinationPosition = this.baseStream_.Position;
			}
			long num = 0L;
			long num2 = update.Entry.Offset + 26L;
			this.baseStream_.Seek(num2, SeekOrigin.Begin);
			uint num3 = (uint)this.ReadLEUshort();
			uint num4 = (uint)this.ReadLEUshort();
			num = this.baseStream_.Position + (long)((ulong)num3) + (long)((ulong)num4);
			bool flag4 = flag;
			if (flag4)
			{
				bool flag5 = update.OffsetBasedSize != -1L;
				if (flag5)
				{
					destinationPosition += update.OffsetBasedSize;
				}
				else
				{
					destinationPosition += num - num2 + 26L + update.Entry.CompressedSize + (long)this.GetDescriptorSize(update);
				}
			}
			else
			{
				bool flag6 = update.Entry.CompressedSize > 0L;
				if (flag6)
				{
					this.CopyEntryDataDirect(update, this.baseStream_, false, ref destinationPosition, ref num);
				}
				this.CopyDescriptorBytesDirect(update, this.baseStream_, ref destinationPosition, num);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00024BA8 File Offset: 0x00022DA8
		private void CopyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			bool flag = update.Entry.CompressedSize > 0L;
			if (flag)
			{
				long num = update.Entry.Offset + 26L;
				this.baseStream_.Seek(num, SeekOrigin.Begin);
				uint num2 = (uint)this.ReadLEUshort();
				uint num3 = (uint)this.ReadLEUshort();
				this.baseStream_.Seek((long)((ulong)(num2 + num3)), SeekOrigin.Current);
				this.CopyBytes(update, workFile.baseStream_, this.baseStream_, update.Entry.CompressedSize, false);
			}
			this.CopyDescriptorBytes(update, workFile.baseStream_, this.baseStream_);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00024C44 File Offset: 0x00022E44
		private void Reopen(Stream source)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ZipException("Failed to reopen archive - no source");
			}
			this.isNewArchive_ = false;
			this.baseStream_ = source;
			this.ReadEntries();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00024C7C File Offset: 0x00022E7C
		private void Reopen()
		{
			bool flag = this.Name == null;
			if (flag)
			{
				throw new InvalidOperationException("Name is not known cannot Reopen");
			}
			this.Reopen(File.Open(this.Name, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00024CB8 File Offset: 0x00022EB8
		private void UpdateCommentOnly()
		{
			long length = this.baseStream_.Length;
			bool flag = this.archiveStorage_.UpdateMode == FileUpdateMode.Safe;
			ZipHelperStream zipHelperStream;
			if (flag)
			{
				Stream stream = this.archiveStorage_.MakeTemporaryCopy(this.baseStream_);
				zipHelperStream = new ZipHelperStream(stream);
				zipHelperStream.IsStreamOwner = true;
				this.baseStream_.Close();
				this.baseStream_ = null;
			}
			else
			{
				bool flag2 = this.archiveStorage_.UpdateMode == FileUpdateMode.Direct;
				if (flag2)
				{
					this.baseStream_ = this.archiveStorage_.OpenForDirectUpdate(this.baseStream_);
					zipHelperStream = new ZipHelperStream(this.baseStream_);
				}
				else
				{
					this.baseStream_.Close();
					this.baseStream_ = null;
					zipHelperStream = new ZipHelperStream(this.Name);
				}
			}
			using (zipHelperStream)
			{
				long num = zipHelperStream.LocateBlockWithSignature(101010256, length, 22, 65535);
				bool flag3 = num < 0L;
				if (flag3)
				{
					throw new ZipException("Cannot find central directory");
				}
				zipHelperStream.Position += 16L;
				byte[] rawComment = this.newComment_.RawComment;
				zipHelperStream.WriteLEShort(rawComment.Length);
				zipHelperStream.Write(rawComment, 0, rawComment.Length);
				zipHelperStream.SetLength(zipHelperStream.Position);
			}
			bool flag4 = this.archiveStorage_.UpdateMode == FileUpdateMode.Safe;
			if (flag4)
			{
				this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
			}
			else
			{
				this.ReadEntries();
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00024E44 File Offset: 0x00023044
		private void RunUpdates()
		{
			long num = 0L;
			long num2 = 0L;
			bool flag = false;
			long num3 = 0L;
			bool isNewArchive = this.IsNewArchive;
			ZipFile zipFile;
			if (isNewArchive)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
			}
			else
			{
				bool flag2 = this.archiveStorage_.UpdateMode == FileUpdateMode.Direct;
				if (flag2)
				{
					zipFile = this;
					zipFile.baseStream_.Position = 0L;
					flag = true;
					this.updates_.Sort(new ZipFile.UpdateComparer());
				}
				else
				{
					zipFile = ZipFile.Create(this.archiveStorage_.GetTemporaryOutput());
					zipFile.UseZip64 = this.UseZip64;
					bool flag3 = this.key != null;
					if (flag3)
					{
						zipFile.key = (byte[])this.key.Clone();
					}
				}
			}
			try
			{
				foreach (object obj in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate = (ZipFile.ZipUpdate)obj;
					bool flag4 = zipUpdate != null;
					if (flag4)
					{
						switch (zipUpdate.Command)
						{
						case ZipFile.UpdateCommand.Copy:
						{
							bool flag5 = flag;
							if (flag5)
							{
								this.CopyEntryDirect(zipFile, zipUpdate, ref num3);
							}
							else
							{
								this.CopyEntry(zipFile, zipUpdate);
							}
							break;
						}
						case ZipFile.UpdateCommand.Modify:
							this.ModifyEntry(zipFile, zipUpdate);
							break;
						case ZipFile.UpdateCommand.Add:
						{
							bool flag6 = !this.IsNewArchive && flag;
							if (flag6)
							{
								zipFile.baseStream_.Position = num3;
							}
							this.AddEntry(zipFile, zipUpdate);
							bool flag7 = flag;
							if (flag7)
							{
								num3 = zipFile.baseStream_.Position;
							}
							break;
						}
						}
					}
				}
				bool flag8 = !this.IsNewArchive && flag;
				if (flag8)
				{
					zipFile.baseStream_.Position = num3;
				}
				long position = zipFile.baseStream_.Position;
				foreach (object obj2 in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate2 = (ZipFile.ZipUpdate)obj2;
					bool flag9 = zipUpdate2 != null;
					if (flag9)
					{
						num += (long)zipFile.WriteCentralDirectoryHeader(zipUpdate2.OutEntry);
					}
				}
				byte[] array = ((this.newComment_ != null) ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_));
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(zipFile.baseStream_))
				{
					zipHelperStream.WriteEndOfCentralDirectory(this.updateCount_, num, position, array);
				}
				num2 = zipFile.baseStream_.Position;
				foreach (object obj3 in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate3 = (ZipFile.ZipUpdate)obj3;
					bool flag10 = zipUpdate3 != null;
					if (flag10)
					{
						bool flag11 = zipUpdate3.CrcPatchOffset > 0L && zipUpdate3.OutEntry.CompressedSize > 0L;
						if (flag11)
						{
							zipFile.baseStream_.Position = zipUpdate3.CrcPatchOffset;
							zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Crc);
						}
						bool flag12 = zipUpdate3.SizePatchOffset > 0L;
						if (flag12)
						{
							zipFile.baseStream_.Position = zipUpdate3.SizePatchOffset;
							bool localHeaderRequiresZip = zipUpdate3.OutEntry.LocalHeaderRequiresZip64;
							if (localHeaderRequiresZip)
							{
								zipFile.WriteLeLong(zipUpdate3.OutEntry.Size);
								zipFile.WriteLeLong(zipUpdate3.OutEntry.CompressedSize);
							}
							else
							{
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.CompressedSize);
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Size);
							}
						}
					}
				}
			}
			catch
			{
				zipFile.Close();
				bool flag13 = !flag && zipFile.Name != null;
				if (flag13)
				{
					File.Delete(zipFile.Name);
				}
				throw;
			}
			bool flag14 = flag;
			if (flag14)
			{
				zipFile.baseStream_.SetLength(num2);
				zipFile.baseStream_.Flush();
				this.isNewArchive_ = false;
				this.ReadEntries();
			}
			else
			{
				this.baseStream_.Close();
				this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00025328 File Offset: 0x00023528
		private void CheckUpdating()
		{
			bool flag = this.updates_ == null;
			if (flag)
			{
				throw new InvalidOperationException("BeginUpdate has not been called");
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002534F File Offset: 0x0002354F
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002535C File Offset: 0x0002355C
		private void DisposeInternal(bool disposing)
		{
			bool flag = !this.isDisposed_;
			if (flag)
			{
				this.isDisposed_ = true;
				this.entries_ = new ZipEntry[0];
				bool flag2 = this.IsStreamOwner && this.baseStream_ != null;
				if (flag2)
				{
					Stream stream = this.baseStream_;
					lock (stream)
					{
						this.baseStream_.Close();
					}
				}
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000253EC File Offset: 0x000235EC
		protected virtual void Dispose(bool disposing)
		{
			this.DisposeInternal(disposing);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000253F8 File Offset: 0x000235F8
		private ushort ReadLEUshort()
		{
			int num = this.baseStream_.ReadByte();
			bool flag = num < 0;
			if (flag)
			{
				throw new EndOfStreamException("End of stream");
			}
			int num2 = this.baseStream_.ReadByte();
			bool flag2 = num2 < 0;
			if (flag2)
			{
				throw new EndOfStreamException("End of stream");
			}
			return (ushort)num | (ushort)(num2 << 8);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00025454 File Offset: 0x00023654
		private uint ReadLEUint()
		{
			return (uint)((int)this.ReadLEUshort() | ((int)this.ReadLEUshort() << 16));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00025478 File Offset: 0x00023678
		private ulong ReadLEUlong()
		{
			return (ulong)this.ReadLEUint() | ((ulong)this.ReadLEUint() << 32);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002549C File Offset: 0x0002369C
		private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long num;
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
			{
				num = zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
			}
			return num;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000254E4 File Offset: 0x000236E4
		private void ReadEntries()
		{
			bool flag = !this.baseStream_.CanSeek;
			if (flag)
			{
				throw new ZipException("ZipFile stream must be seekable");
			}
			long num = this.LocateBlockWithSignature(101010256, this.baseStream_.Length, 22, 65535);
			bool flag2 = num < 0L;
			if (flag2)
			{
				throw new ZipException("Cannot find central directory");
			}
			ushort num2 = this.ReadLEUshort();
			ushort num3 = this.ReadLEUshort();
			ulong num4 = (ulong)this.ReadLEUshort();
			ulong num5 = (ulong)this.ReadLEUshort();
			ulong num6 = (ulong)this.ReadLEUint();
			long num7 = (long)((ulong)this.ReadLEUint());
			uint num8 = (uint)this.ReadLEUshort();
			bool flag3 = num8 > 0U;
			if (flag3)
			{
				byte[] array = new byte[num8];
				StreamUtils.ReadFully(this.baseStream_, array);
				this.comment_ = ZipConstants.ConvertToString(array);
			}
			else
			{
				this.comment_ = string.Empty;
			}
			bool flag4 = false;
			bool flag5 = num2 == ushort.MaxValue || num3 == ushort.MaxValue || num4 == 65535UL || num5 == 65535UL || num6 == (ulong)(-1) || num7 == (long)((ulong)(-1));
			if (flag5)
			{
				flag4 = true;
				long num9 = this.LocateBlockWithSignature(117853008, num, 0, 4096);
				bool flag6 = num9 < 0L;
				if (flag6)
				{
					throw new ZipException("Cannot find Zip64 locator");
				}
				this.ReadLEUint();
				ulong num10 = this.ReadLEUlong();
				uint num11 = this.ReadLEUint();
				this.baseStream_.Position = (long)num10;
				long num12 = (long)((ulong)this.ReadLEUint());
				bool flag7 = num12 != 101075792L;
				if (flag7)
				{
					throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num10));
				}
				ulong num13 = this.ReadLEUlong();
				int num14 = (int)this.ReadLEUshort();
				int num15 = (int)this.ReadLEUshort();
				uint num16 = this.ReadLEUint();
				uint num17 = this.ReadLEUint();
				num4 = this.ReadLEUlong();
				num5 = this.ReadLEUlong();
				num6 = this.ReadLEUlong();
				num7 = (long)this.ReadLEUlong();
			}
			this.entries_ = new ZipEntry[num4];
			bool flag8 = !flag4 && num7 < num - (long)(4UL + num6);
			if (flag8)
			{
				this.offsetOfFirstEntry = num - (long)(4UL + num6 + (ulong)num7);
				bool flag9 = this.offsetOfFirstEntry <= 0L;
				if (flag9)
				{
					throw new ZipException("Invalid embedded zip archive");
				}
			}
			this.baseStream_.Seek(this.offsetOfFirstEntry + num7, SeekOrigin.Begin);
			for (ulong num18 = 0UL; num18 < num4; num18 += 1UL)
			{
				bool flag10 = this.ReadLEUint() != 33639248U;
				if (flag10)
				{
					throw new ZipException("Wrong Central Directory signature");
				}
				int num19 = (int)this.ReadLEUshort();
				int num20 = (int)this.ReadLEUshort();
				int num21 = (int)this.ReadLEUshort();
				int num22 = (int)this.ReadLEUshort();
				uint num23 = this.ReadLEUint();
				uint num24 = this.ReadLEUint();
				long num25 = (long)((ulong)this.ReadLEUint());
				long num26 = (long)((ulong)this.ReadLEUint());
				int num27 = (int)this.ReadLEUshort();
				int num28 = (int)this.ReadLEUshort();
				int num29 = (int)this.ReadLEUshort();
				int num30 = (int)this.ReadLEUshort();
				int num31 = (int)this.ReadLEUshort();
				uint num32 = this.ReadLEUint();
				long num33 = (long)((ulong)this.ReadLEUint());
				byte[] array2 = new byte[Math.Max(num27, num29)];
				StreamUtils.ReadFully(this.baseStream_, array2, 0, num27);
				string text = ZipConstants.ConvertToStringExt(num21, array2, num27);
				ZipEntry zipEntry = new ZipEntry(text, num20, num19, (CompressionMethod)num22);
				zipEntry.Crc = (long)((ulong)num24 & (ulong)(-1));
				zipEntry.Size = num26 & (long)((ulong)(-1));
				zipEntry.CompressedSize = num25 & (long)((ulong)(-1));
				zipEntry.Flags = num21;
				zipEntry.DosTime = (long)((ulong)num23);
				zipEntry.ZipFileIndex = (long)num18;
				zipEntry.Offset = num33;
				zipEntry.ExternalFileAttributes = (int)num32;
				bool flag11 = (num21 & 8) == 0;
				if (flag11)
				{
					zipEntry.CryptoCheckValue = (byte)(num24 >> 24);
				}
				else
				{
					zipEntry.CryptoCheckValue = (byte)((num23 >> 8) & 255U);
				}
				bool flag12 = num28 > 0;
				if (flag12)
				{
					byte[] array3 = new byte[num28];
					StreamUtils.ReadFully(this.baseStream_, array3);
					zipEntry.ExtraData = array3;
				}
				zipEntry.ProcessExtraData(false);
				bool flag13 = num29 > 0;
				if (flag13)
				{
					StreamUtils.ReadFully(this.baseStream_, array2, 0, num29);
					zipEntry.Comment = ZipConstants.ConvertToStringExt(num21, array2, num29);
				}
				this.entries_[(int)(checked((IntPtr)num18))] = zipEntry;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00025938 File Offset: 0x00023B38
		private long LocateEntry(ZipEntry entry)
		{
			return this.TestLocalHeader(entry, ZipFile.HeaderTest.Extract);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00025954 File Offset: 0x00023B54
		private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
		{
			bool flag = entry.Version < 50 || (entry.Flags & 64) == 0;
			CryptoStream cryptoStream;
			if (flag)
			{
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				bool flag2 = !this.HaveKeys;
				if (flag2)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(baseStream, pkzipClassicManaged.CreateDecryptor(this.key, null), CryptoStreamMode.Read);
				ZipFile.CheckClassicPassword(cryptoStream, entry);
			}
			else
			{
				bool flag3 = entry.Version == 51;
				if (!flag3)
				{
					throw new ZipException("Decryption method not supported");
				}
				this.OnKeysRequired(entry.Name);
				bool flag4 = !this.HaveKeys;
				if (flag4)
				{
					throw new ZipException("No password available for AES encrypted stream");
				}
				int aessaltLen = entry.AESSaltLen;
				byte[] array = new byte[aessaltLen];
				int num = baseStream.Read(array, 0, aessaltLen);
				bool flag5 = num != aessaltLen;
				if (flag5)
				{
					throw new ZipException(string.Concat(new object[] { "AES Salt expected ", aessaltLen, " got ", num }));
				}
				byte[] array2 = new byte[2];
				baseStream.Read(array2, 0, 2);
				int num2 = entry.AESKeySize / 8;
				ZipAESTransform zipAESTransform = new ZipAESTransform(this.rawPassword_, array, num2, false);
				byte[] pwdVerifier = zipAESTransform.PwdVerifier;
				bool flag6 = pwdVerifier[0] != array2[0] || pwdVerifier[1] != array2[1];
				if (flag6)
				{
					throw new Exception("Invalid password for AES");
				}
				cryptoStream = new ZipAESStream(baseStream, zipAESTransform, CryptoStreamMode.Read);
			}
			return cryptoStream;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00025AF4 File Offset: 0x00023CF4
		private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream = null;
			bool flag = entry.Version < 50 || (entry.Flags & 64) == 0;
			if (flag)
			{
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				bool flag2 = !this.HaveKeys;
				if (flag2)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(new ZipFile.UncompressedStream(baseStream), pkzipClassicManaged.CreateEncryptor(this.key, null), CryptoStreamMode.Write);
				bool flag3 = entry.Crc < 0L || (entry.Flags & 8) != 0;
				if (flag3)
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
				}
				else
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.Crc);
				}
			}
			return cryptoStream;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00025BB4 File Offset: 0x00023DB4
		private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
		{
			byte[] array = new byte[12];
			StreamUtils.ReadFully(classicCryptoStream, array);
			bool flag = array[11] != entry.CryptoCheckValue;
			if (flag)
			{
				throw new ZipException("Invalid password");
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00025BF4 File Offset: 0x00023DF4
		private static void WriteEncryptionHeader(Stream stream, long crcValue)
		{
			byte[] array = new byte[12];
			Random random = new Random();
			random.NextBytes(array);
			array[11] = (byte)(crcValue >> 24);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x04000383 RID: 899
		public ZipFile.KeysRequiredEventHandler KeysRequired;

		// Token: 0x04000384 RID: 900
		private const int DefaultBufferSize = 4096;

		// Token: 0x04000385 RID: 901
		private bool isDisposed_;

		// Token: 0x04000386 RID: 902
		private string name_;

		// Token: 0x04000387 RID: 903
		private string comment_;

		// Token: 0x04000388 RID: 904
		private string rawPassword_;

		// Token: 0x04000389 RID: 905
		private Stream baseStream_;

		// Token: 0x0400038A RID: 906
		private bool isStreamOwner;

		// Token: 0x0400038B RID: 907
		private long offsetOfFirstEntry;

		// Token: 0x0400038C RID: 908
		private ZipEntry[] entries_;

		// Token: 0x0400038D RID: 909
		private byte[] key;

		// Token: 0x0400038E RID: 910
		private bool isNewArchive_;

		// Token: 0x0400038F RID: 911
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x04000390 RID: 912
		private ArrayList updates_;

		// Token: 0x04000391 RID: 913
		private long updateCount_;

		// Token: 0x04000392 RID: 914
		private Hashtable updateIndex_;

		// Token: 0x04000393 RID: 915
		private IArchiveStorage archiveStorage_;

		// Token: 0x04000394 RID: 916
		private IDynamicDataSource updateDataSource_;

		// Token: 0x04000395 RID: 917
		private bool contentsEdited_;

		// Token: 0x04000396 RID: 918
		private int bufferSize_ = 4096;

		// Token: 0x04000397 RID: 919
		private byte[] copyBuffer_;

		// Token: 0x04000398 RID: 920
		private ZipFile.ZipString newComment_;

		// Token: 0x04000399 RID: 921
		private bool commentEdited_;

		// Token: 0x0400039A RID: 922
		private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

		// Token: 0x0200020D RID: 525
		// (Invoke) Token: 0x06001C0C RID: 7180
		public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

		// Token: 0x0200020E RID: 526
		[Flags]
		private enum HeaderTest
		{
			// Token: 0x04000DBF RID: 3519
			Extract = 1,
			// Token: 0x04000DC0 RID: 3520
			Header = 2
		}

		// Token: 0x0200020F RID: 527
		private enum UpdateCommand
		{
			// Token: 0x04000DC2 RID: 3522
			Copy,
			// Token: 0x04000DC3 RID: 3523
			Modify,
			// Token: 0x04000DC4 RID: 3524
			Add
		}

		// Token: 0x02000210 RID: 528
		private class UpdateComparer : IComparer
		{
			// Token: 0x06001C0F RID: 7183 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
			public int Compare(object x, object y)
			{
				ZipFile.ZipUpdate zipUpdate = x as ZipFile.ZipUpdate;
				ZipFile.ZipUpdate zipUpdate2 = y as ZipFile.ZipUpdate;
				bool flag = zipUpdate == null;
				int num;
				if (flag)
				{
					bool flag2 = zipUpdate2 == null;
					if (flag2)
					{
						num = 0;
					}
					else
					{
						num = -1;
					}
				}
				else
				{
					bool flag3 = zipUpdate2 == null;
					if (flag3)
					{
						num = 1;
					}
					else
					{
						int num2 = ((zipUpdate.Command == ZipFile.UpdateCommand.Copy || zipUpdate.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1);
						int num3 = ((zipUpdate2.Command == ZipFile.UpdateCommand.Copy || zipUpdate2.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1);
						num = num2 - num3;
						bool flag4 = num == 0;
						if (flag4)
						{
							long num4 = zipUpdate.Entry.Offset - zipUpdate2.Entry.Offset;
							bool flag5 = num4 < 0L;
							if (flag5)
							{
								num = -1;
							}
							else
							{
								bool flag6 = num4 == 0L;
								if (flag6)
								{
									num = 0;
								}
								else
								{
									num = 1;
								}
							}
						}
					}
				}
				return num;
			}
		}

		// Token: 0x02000211 RID: 529
		private class ZipUpdate
		{
			// Token: 0x06001C11 RID: 7185 RVA: 0x000B1BBC File Offset: 0x000AFDBC
			public ZipUpdate(string fileName, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.filename_ = fileName;
			}

			// Token: 0x06001C12 RID: 7186 RVA: 0x000B1BF4 File Offset: 0x000AFDF4
			[Obsolete]
			public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName);
				this.entry_.CompressionMethod = compressionMethod;
				this.filename_ = fileName;
			}

			// Token: 0x06001C13 RID: 7187 RVA: 0x000B1C48 File Offset: 0x000AFE48
			[Obsolete]
			public ZipUpdate(string fileName, string entryName)
				: this(fileName, entryName, CompressionMethod.Deflated)
			{
			}

			// Token: 0x06001C14 RID: 7188 RVA: 0x000B1C58 File Offset: 0x000AFE58
			[Obsolete]
			public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName);
				this.entry_.CompressionMethod = compressionMethod;
				this.dataSource_ = dataSource;
			}

			// Token: 0x06001C15 RID: 7189 RVA: 0x000B1CAC File Offset: 0x000AFEAC
			public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.dataSource_ = dataSource;
			}

			// Token: 0x06001C16 RID: 7190 RVA: 0x000B1CE3 File Offset: 0x000AFEE3
			public ZipUpdate(ZipEntry original, ZipEntry updated)
			{
				throw new ZipException("Modify not currently supported");
			}

			// Token: 0x06001C17 RID: 7191 RVA: 0x000B1D0F File Offset: 0x000AFF0F
			public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
			{
				this.command_ = command;
				this.entry_ = (ZipEntry)entry.Clone();
			}

			// Token: 0x06001C18 RID: 7192 RVA: 0x000B1D49 File Offset: 0x000AFF49
			public ZipUpdate(ZipEntry entry)
				: this(ZipFile.UpdateCommand.Copy, entry)
			{
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x06001C19 RID: 7193 RVA: 0x000B1D58 File Offset: 0x000AFF58
			public ZipEntry Entry
			{
				get
				{
					return this.entry_;
				}
			}

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000B1D70 File Offset: 0x000AFF70
			public ZipEntry OutEntry
			{
				get
				{
					bool flag = this.outEntry_ == null;
					if (flag)
					{
						this.outEntry_ = (ZipEntry)this.entry_.Clone();
					}
					return this.outEntry_;
				}
			}

			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000B1DB0 File Offset: 0x000AFFB0
			public ZipFile.UpdateCommand Command
			{
				get
				{
					return this.command_;
				}
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000B1DC8 File Offset: 0x000AFFC8
			public string Filename
			{
				get
				{
					return this.filename_;
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000B1DE0 File Offset: 0x000AFFE0
			// (set) Token: 0x06001C1E RID: 7198 RVA: 0x000B1DF8 File Offset: 0x000AFFF8
			public long SizePatchOffset
			{
				get
				{
					return this.sizePatchOffset_;
				}
				set
				{
					this.sizePatchOffset_ = value;
				}
			}

			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000B1E04 File Offset: 0x000B0004
			// (set) Token: 0x06001C20 RID: 7200 RVA: 0x000B1E1C File Offset: 0x000B001C
			public long CrcPatchOffset
			{
				get
				{
					return this.crcPatchOffset_;
				}
				set
				{
					this.crcPatchOffset_ = value;
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000B1E28 File Offset: 0x000B0028
			// (set) Token: 0x06001C22 RID: 7202 RVA: 0x000B1E40 File Offset: 0x000B0040
			public long OffsetBasedSize
			{
				get
				{
					return this._offsetBasedSize;
				}
				set
				{
					this._offsetBasedSize = value;
				}
			}

			// Token: 0x06001C23 RID: 7203 RVA: 0x000B1E4C File Offset: 0x000B004C
			public Stream GetSource()
			{
				Stream stream = null;
				bool flag = this.dataSource_ != null;
				if (flag)
				{
					stream = this.dataSource_.GetSource();
				}
				return stream;
			}

			// Token: 0x04000DC5 RID: 3525
			private ZipEntry entry_;

			// Token: 0x04000DC6 RID: 3526
			private ZipEntry outEntry_;

			// Token: 0x04000DC7 RID: 3527
			private ZipFile.UpdateCommand command_;

			// Token: 0x04000DC8 RID: 3528
			private IStaticDataSource dataSource_;

			// Token: 0x04000DC9 RID: 3529
			private string filename_;

			// Token: 0x04000DCA RID: 3530
			private long sizePatchOffset_ = -1L;

			// Token: 0x04000DCB RID: 3531
			private long crcPatchOffset_ = -1L;

			// Token: 0x04000DCC RID: 3532
			private long _offsetBasedSize = -1L;
		}

		// Token: 0x02000212 RID: 530
		private class ZipString
		{
			// Token: 0x06001C24 RID: 7204 RVA: 0x000B1E7C File Offset: 0x000B007C
			public ZipString(string comment)
			{
				this.comment_ = comment;
				this.isSourceString_ = true;
			}

			// Token: 0x06001C25 RID: 7205 RVA: 0x000B1E94 File Offset: 0x000B0094
			public ZipString(byte[] rawString)
			{
				this.rawComment_ = rawString;
			}

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000B1EA8 File Offset: 0x000B00A8
			public bool IsSourceString
			{
				get
				{
					return this.isSourceString_;
				}
			}

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000B1EC0 File Offset: 0x000B00C0
			public int RawLength
			{
				get
				{
					this.MakeBytesAvailable();
					return this.rawComment_.Length;
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000B1EE4 File Offset: 0x000B00E4
			public byte[] RawComment
			{
				get
				{
					this.MakeBytesAvailable();
					return (byte[])this.rawComment_.Clone();
				}
			}

			// Token: 0x06001C29 RID: 7209 RVA: 0x000B1F10 File Offset: 0x000B0110
			public void Reset()
			{
				bool flag = this.isSourceString_;
				if (flag)
				{
					this.rawComment_ = null;
				}
				else
				{
					this.comment_ = null;
				}
			}

			// Token: 0x06001C2A RID: 7210 RVA: 0x000B1F3C File Offset: 0x000B013C
			private void MakeTextAvailable()
			{
				bool flag = this.comment_ == null;
				if (flag)
				{
					this.comment_ = ZipConstants.ConvertToString(this.rawComment_);
				}
			}

			// Token: 0x06001C2B RID: 7211 RVA: 0x000B1F6C File Offset: 0x000B016C
			private void MakeBytesAvailable()
			{
				bool flag = this.rawComment_ == null;
				if (flag)
				{
					this.rawComment_ = ZipConstants.ConvertToArray(this.comment_);
				}
			}

			// Token: 0x06001C2C RID: 7212 RVA: 0x000B1F9C File Offset: 0x000B019C
			public static implicit operator string(ZipFile.ZipString zipString)
			{
				zipString.MakeTextAvailable();
				return zipString.comment_;
			}

			// Token: 0x04000DCD RID: 3533
			private string comment_;

			// Token: 0x04000DCE RID: 3534
			private byte[] rawComment_;

			// Token: 0x04000DCF RID: 3535
			private bool isSourceString_;
		}

		// Token: 0x02000213 RID: 531
		private class ZipEntryEnumerator : IEnumerator
		{
			// Token: 0x06001C2D RID: 7213 RVA: 0x000B1FBB File Offset: 0x000B01BB
			public ZipEntryEnumerator(ZipEntry[] entries)
			{
				this.array = entries;
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x06001C2E RID: 7214 RVA: 0x000B1FD4 File Offset: 0x000B01D4
			public object Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x06001C2F RID: 7215 RVA: 0x000B1FF3 File Offset: 0x000B01F3
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x06001C30 RID: 7216 RVA: 0x000B2000 File Offset: 0x000B0200
			public bool MoveNext()
			{
				int num = this.index + 1;
				this.index = num;
				return num < this.array.Length;
			}

			// Token: 0x04000DD0 RID: 3536
			private ZipEntry[] array;

			// Token: 0x04000DD1 RID: 3537
			private int index = -1;
		}

		// Token: 0x02000214 RID: 532
		private class UncompressedStream : Stream
		{
			// Token: 0x06001C31 RID: 7217 RVA: 0x000B202D File Offset: 0x000B022D
			public UncompressedStream(Stream baseStream)
			{
				this.baseStream_ = baseStream;
			}

			// Token: 0x06001C32 RID: 7218 RVA: 0x000021C5 File Offset: 0x000003C5
			public override void Close()
			{
			}

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x06001C33 RID: 7219 RVA: 0x000B2040 File Offset: 0x000B0240
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x000B2053 File Offset: 0x000B0253
			public override void Flush()
			{
				this.baseStream_.Flush();
			}

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x06001C35 RID: 7221 RVA: 0x000B2064 File Offset: 0x000B0264
			public override bool CanWrite
			{
				get
				{
					return this.baseStream_.CanWrite;
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000B2084 File Offset: 0x000B0284
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170007A9 RID: 1961
			// (get) Token: 0x06001C37 RID: 7223 RVA: 0x000B2098 File Offset: 0x000B0298
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170007AA RID: 1962
			// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000B20AC File Offset: 0x000B02AC
			// (set) Token: 0x06001C39 RID: 7225 RVA: 0x000021C5 File Offset: 0x000003C5
			public override long Position
			{
				get
				{
					return this.baseStream_.Position;
				}
				set
				{
				}
			}

			// Token: 0x06001C3A RID: 7226 RVA: 0x000B20CC File Offset: 0x000B02CC
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06001C3B RID: 7227 RVA: 0x000B20E0 File Offset: 0x000B02E0
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06001C3C RID: 7228 RVA: 0x000021C5 File Offset: 0x000003C5
			public override void SetLength(long value)
			{
			}

			// Token: 0x06001C3D RID: 7229 RVA: 0x000B20F4 File Offset: 0x000B02F4
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.baseStream_.Write(buffer, offset, count);
			}

			// Token: 0x04000DD2 RID: 3538
			private Stream baseStream_;
		}

		// Token: 0x02000215 RID: 533
		private class PartialInputStream : Stream
		{
			// Token: 0x06001C3E RID: 7230 RVA: 0x000B2106 File Offset: 0x000B0306
			public PartialInputStream(ZipFile zipFile, long start, long length)
			{
				this.start_ = start;
				this.length_ = length;
				this.zipFile_ = zipFile;
				this.baseStream_ = this.zipFile_.baseStream_;
				this.readPos_ = start;
				this.end_ = start + length;
			}

			// Token: 0x06001C3F RID: 7231 RVA: 0x000B2148 File Offset: 0x000B0348
			public override int ReadByte()
			{
				bool flag = this.readPos_ >= this.end_;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					Stream stream = this.baseStream_;
					lock (stream)
					{
						Stream stream2 = this.baseStream_;
						long num2 = this.readPos_;
						this.readPos_ = num2 + 1L;
						stream2.Seek(num2, SeekOrigin.Begin);
						num = this.baseStream_.ReadByte();
					}
				}
				return num;
			}

			// Token: 0x06001C40 RID: 7232 RVA: 0x000021C5 File Offset: 0x000003C5
			public override void Close()
			{
			}

			// Token: 0x06001C41 RID: 7233 RVA: 0x000B21D0 File Offset: 0x000B03D0
			public override int Read(byte[] buffer, int offset, int count)
			{
				Stream stream = this.baseStream_;
				int num2;
				lock (stream)
				{
					bool flag2 = (long)count > this.end_ - this.readPos_;
					if (flag2)
					{
						count = (int)(this.end_ - this.readPos_);
						bool flag3 = count == 0;
						if (flag3)
						{
							return 0;
						}
					}
					this.baseStream_.Seek(this.readPos_, SeekOrigin.Begin);
					int num = this.baseStream_.Read(buffer, offset, count);
					bool flag4 = num > 0;
					if (flag4)
					{
						this.readPos_ += (long)num;
					}
					num2 = num;
				}
				return num2;
			}

			// Token: 0x06001C42 RID: 7234 RVA: 0x0000C1AD File Offset: 0x0000A3AD
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001C43 RID: 7235 RVA: 0x0000C1AD File Offset: 0x0000A3AD
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001C44 RID: 7236 RVA: 0x000B2288 File Offset: 0x000B0488
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = this.readPos_;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = this.start_ + offset;
					break;
				case SeekOrigin.Current:
					num = this.readPos_ + offset;
					break;
				case SeekOrigin.End:
					num = this.end_ + offset;
					break;
				}
				bool flag = num < this.start_;
				if (flag)
				{
					throw new ArgumentException("Negative position is invalid");
				}
				bool flag2 = num >= this.end_;
				if (flag2)
				{
					throw new IOException("Cannot seek past end");
				}
				this.readPos_ = num;
				return this.readPos_;
			}

			// Token: 0x06001C45 RID: 7237 RVA: 0x000021C5 File Offset: 0x000003C5
			public override void Flush()
			{
			}

			// Token: 0x170007AB RID: 1963
			// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000B231C File Offset: 0x000B051C
			// (set) Token: 0x06001C47 RID: 7239 RVA: 0x000B233C File Offset: 0x000B053C
			public override long Position
			{
				get
				{
					return this.readPos_ - this.start_;
				}
				set
				{
					long num = this.start_ + value;
					bool flag = num < this.start_;
					if (flag)
					{
						throw new ArgumentException("Negative position is invalid");
					}
					bool flag2 = num >= this.end_;
					if (flag2)
					{
						throw new InvalidOperationException("Cannot seek past end");
					}
					this.readPos_ = num;
				}
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000B2390 File Offset: 0x000B0590
			public override long Length
			{
				get
				{
					return this.length_;
				}
			}

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06001C49 RID: 7241 RVA: 0x000B23A8 File Offset: 0x000B05A8
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x06001C4A RID: 7242 RVA: 0x000B23BC File Offset: 0x000B05BC
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x06001C4B RID: 7243 RVA: 0x000B23D0 File Offset: 0x000B05D0
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x06001C4C RID: 7244 RVA: 0x000B23E4 File Offset: 0x000B05E4
			public override bool CanTimeout
			{
				get
				{
					return this.baseStream_.CanTimeout;
				}
			}

			// Token: 0x04000DD3 RID: 3539
			private ZipFile zipFile_;

			// Token: 0x04000DD4 RID: 3540
			private Stream baseStream_;

			// Token: 0x04000DD5 RID: 3541
			private long start_;

			// Token: 0x04000DD6 RID: 3542
			private long length_;

			// Token: 0x04000DD7 RID: 3543
			private long readPos_;

			// Token: 0x04000DD8 RID: 3544
			private long end_;
		}
	}
}
