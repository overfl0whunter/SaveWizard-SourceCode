using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x02000047 RID: 71
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ZipEntry
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000108EC File Offset: 0x0000EAEC
		internal bool AttributesIndicateDirectory
		{
			get
			{
				return this._InternalFileAttrs == 0 && (this._ExternalFileAttrs & 16) == 16;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00010916 File Offset: 0x0000EB16
		internal void ResetDirEntry()
		{
			this.__FileDataPosition = -1L;
			this._LengthOfHeader = 0;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00010928 File Offset: 0x0000EB28
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipEntry: {0}\n", this.FileName)).Append(string.Format("   Version Made By: {0}\n", this._VersionMadeBy)).Append(string.Format(" Needed to extract: {0}\n", this.VersionNeeded));
				bool isDirectory = this._IsDirectory;
				if (isDirectory)
				{
					stringBuilder.Append("        Entry type: directory\n");
				}
				else
				{
					stringBuilder.Append(string.Format("         File type: {0}\n", this._IsText ? "text" : "binary")).Append(string.Format("       Compression: {0}\n", this.CompressionMethod)).Append(string.Format("        Compressed: 0x{0:X}\n", this.CompressedSize))
						.Append(string.Format("      Uncompressed: 0x{0:X}\n", this.UncompressedSize))
						.Append(string.Format("             CRC32: 0x{0:X8}\n", this._Crc32));
				}
				stringBuilder.Append(string.Format("       Disk Number: {0}\n", this._diskNumber));
				bool flag = this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1));
				if (flag)
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X16}\n", this._RelativeOffsetOfLocalHeader));
				}
				else
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X8}\n", this._RelativeOffsetOfLocalHeader));
				}
				stringBuilder.Append(string.Format("         Bit Field: 0x{0:X4}\n", this._BitField)).Append(string.Format("        Encrypted?: {0}\n", this._sourceIsEncrypted)).Append(string.Format("          Timeblob: 0x{0:X8}\n", this._TimeBlob))
					.Append(string.Format("              Time: {0}\n", SharedUtilities.PackedToDateTime(this._TimeBlob)));
				stringBuilder.Append(string.Format("         Is Zip64?: {0}\n", this._InputUsesZip64));
				bool flag2 = !string.IsNullOrEmpty(this._Comment);
				if (flag2)
				{
					stringBuilder.Append(string.Format("           Comment: {0}\n", this._Comment));
				}
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00010B64 File Offset: 0x0000ED64
		internal static ZipEntry ReadDirEntry(ZipFile zf, Dictionary<string, object> previouslySeen)
		{
			Stream readStream = zf.ReadStream;
			Encoding encoding = ((zf.AlternateEncodingUsage == ZipOption.Always) ? zf.AlternateEncoding : ZipFile.DefaultEncoding);
			int num = SharedUtilities.ReadSignature(readStream);
			bool flag = ZipEntry.IsNotValidZipDirEntrySig(num);
			ZipEntry zipEntry;
			if (flag)
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				bool flag2 = (long)num != 101010256L && (long)num != 101075792L && num != 67324752;
				if (flag2)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position 0x{1:X8}", num, readStream.Position));
				}
				zipEntry = null;
			}
			else
			{
				int num2 = 46;
				byte[] array = new byte[42];
				int num3 = readStream.Read(array, 0, array.Length);
				bool flag3 = num3 != array.Length;
				if (flag3)
				{
					zipEntry = null;
				}
				else
				{
					int num4 = 0;
					ZipEntry zipEntry2 = new ZipEntry();
					zipEntry2.AlternateEncoding = encoding;
					zipEntry2._Source = ZipEntrySource.ZipFile;
					zipEntry2._container = new ZipContainer(zf);
					zipEntry2._VersionMadeBy = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
					zipEntry2._LastModified = SharedUtilities.PackedToDateTime(zipEntry2._TimeBlob);
					zipEntry2._timestamp |= ZipEntryTimestamp.DOS;
					zipEntry2._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
					zipEntry2._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
					zipEntry2._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
					zipEntry2._CompressionMethod_FromZipFile = zipEntry2._CompressionMethod;
					zipEntry2._filenameLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._extraFieldLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._commentLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._diskNumber = (uint)array[num4++] + (uint)array[num4++] * 256U;
					zipEntry2._InternalFileAttrs = (short)((int)array[num4++] + (int)array[num4++] * 256);
					zipEntry2._ExternalFileAttrs = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
					zipEntry2._RelativeOffsetOfLocalHeader = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
					zipEntry2.IsText = (zipEntry2._InternalFileAttrs & 1) == 1;
					array = new byte[(int)zipEntry2._filenameLength];
					num3 = readStream.Read(array, 0, array.Length);
					num2 += num3;
					bool flag4 = (zipEntry2._BitField & 2048) == 2048;
					if (flag4)
					{
						zipEntry2._FileNameInArchive = SharedUtilities.Utf8StringFromBuffer(array);
					}
					else
					{
						zipEntry2._FileNameInArchive = SharedUtilities.StringFromBuffer(array, encoding);
					}
					while (previouslySeen.ContainsKey(zipEntry2._FileNameInArchive))
					{
						zipEntry2._FileNameInArchive = ZipEntry.CopyHelper.AppendCopyToFileName(zipEntry2._FileNameInArchive);
						zipEntry2._metadataChanged = true;
					}
					bool attributesIndicateDirectory = zipEntry2.AttributesIndicateDirectory;
					if (attributesIndicateDirectory)
					{
						zipEntry2.MarkAsDirectory();
					}
					else
					{
						bool flag5 = zipEntry2._FileNameInArchive.EndsWith("/");
						if (flag5)
						{
							zipEntry2.MarkAsDirectory();
						}
					}
					zipEntry2._CompressedFileDataSize = zipEntry2._CompressedSize;
					bool flag6 = (zipEntry2._BitField & 1) == 1;
					if (flag6)
					{
						zipEntry2._Encryption_FromZipFile = (zipEntry2._Encryption = EncryptionAlgorithm.PkzipWeak);
						zipEntry2._sourceIsEncrypted = true;
					}
					bool flag7 = zipEntry2._extraFieldLength > 0;
					if (flag7)
					{
						zipEntry2._InputUsesZip64 = zipEntry2._CompressedSize == (long)((ulong)(-1)) || zipEntry2._UncompressedSize == (long)((ulong)(-1)) || zipEntry2._RelativeOffsetOfLocalHeader == (long)((ulong)(-1));
						num2 += zipEntry2.ProcessExtraField(readStream, zipEntry2._extraFieldLength);
						zipEntry2._CompressedFileDataSize = zipEntry2._CompressedSize;
					}
					bool flag8 = zipEntry2._Encryption == EncryptionAlgorithm.PkzipWeak;
					if (flag8)
					{
						zipEntry2._CompressedFileDataSize -= 12L;
					}
					bool flag9 = (zipEntry2._BitField & 8) == 8;
					if (flag9)
					{
						bool inputUsesZip = zipEntry2._InputUsesZip64;
						if (inputUsesZip)
						{
							zipEntry2._LengthOfTrailer += 24;
						}
						else
						{
							zipEntry2._LengthOfTrailer += 16;
						}
					}
					zipEntry2.AlternateEncoding = (((zipEntry2._BitField & 2048) == 2048) ? Encoding.UTF8 : encoding);
					zipEntry2.AlternateEncodingUsage = ZipOption.Always;
					bool flag10 = zipEntry2._commentLength > 0;
					if (flag10)
					{
						array = new byte[(int)zipEntry2._commentLength];
						num3 = readStream.Read(array, 0, array.Length);
						num2 += num3;
						bool flag11 = (zipEntry2._BitField & 2048) == 2048;
						if (flag11)
						{
							zipEntry2._Comment = SharedUtilities.Utf8StringFromBuffer(array);
						}
						else
						{
							zipEntry2._Comment = SharedUtilities.StringFromBuffer(array, encoding);
						}
					}
					zipEntry = zipEntry2;
				}
			}
			return zipEntry;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00011254 File Offset: 0x0000F454
		internal static bool IsNotValidZipDirEntrySig(int signature)
		{
			return signature != 33639248;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00011274 File Offset: 0x0000F474
		public ZipEntry()
		{
			this._CompressionMethod = 8;
			this._CompressionLevel = CompressionLevel.Default;
			this._Encryption = EncryptionAlgorithm.None;
			this._Source = ZipEntrySource.None;
			this.AlternateEncoding = Encoding.GetEncoding("IBM437");
			this.AlternateEncodingUsage = ZipOption.Default;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000112E0 File Offset: 0x0000F4E0
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00011300 File Offset: 0x0000F500
		public DateTime LastModified
		{
			get
			{
				return this._LastModified.ToLocalTime();
			}
			set
			{
				this._LastModified = ((value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(value, DateTimeKind.Local) : value.ToLocalTime());
				this._Mtime = SharedUtilities.AdjustTime_Reverse(this._LastModified).ToUniversalTime();
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00011350 File Offset: 0x0000F550
		private int BufferSize
		{
			get
			{
				return this._container.BufferSize;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00011370 File Offset: 0x0000F570
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00011388 File Offset: 0x0000F588
		public DateTime ModifiedTime
		{
			get
			{
				return this._Mtime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, this._Atime, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000113A0 File Offset: 0x0000F5A0
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public DateTime AccessedTime
		{
			get
			{
				return this._Atime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, value, this._Mtime);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000113D0 File Offset: 0x0000F5D0
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000113E8 File Offset: 0x0000F5E8
		public DateTime CreationTime
		{
			get
			{
				return this._Ctime;
			}
			set
			{
				this.SetEntryTimes(value, this._Atime, this._Mtime);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00011400 File Offset: 0x0000F600
		public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
		{
			this._ntfsTimesAreSet = true;
			bool flag = created == ZipEntry._zeroHour && created.Kind == ZipEntry._zeroHour.Kind;
			if (flag)
			{
				created = ZipEntry._win32Epoch;
			}
			bool flag2 = accessed == ZipEntry._zeroHour && accessed.Kind == ZipEntry._zeroHour.Kind;
			if (flag2)
			{
				accessed = ZipEntry._win32Epoch;
			}
			bool flag3 = modified == ZipEntry._zeroHour && modified.Kind == ZipEntry._zeroHour.Kind;
			if (flag3)
			{
				modified = ZipEntry._win32Epoch;
			}
			this._Ctime = created.ToUniversalTime();
			this._Atime = accessed.ToUniversalTime();
			this._Mtime = modified.ToUniversalTime();
			this._LastModified = this._Mtime;
			bool flag4 = !this._emitUnixTimes && !this._emitNtfsTimes;
			if (flag4)
			{
				this._emitNtfsTimes = true;
			}
			this._metadataChanged = true;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000114F8 File Offset: 0x0000F6F8
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00011510 File Offset: 0x0000F710
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00011524 File Offset: 0x0000F724
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0001153C File Offset: 0x0000F73C
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00011550 File Offset: 0x0000F750
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00011568 File Offset: 0x0000F768
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00011580 File Offset: 0x0000F780
		public FileAttributes Attributes
		{
			get
			{
				return (FileAttributes)this._ExternalFileAttrs;
			}
			set
			{
				this._ExternalFileAttrs = (int)value;
				this._VersionMadeBy = 45;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001159C File Offset: 0x0000F79C
		internal string LocalFileName
		{
			get
			{
				return this._LocalFileName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000115B4 File Offset: 0x0000F7B4
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000115CC File Offset: 0x0000F7CC
		public string FileName
		{
			get
			{
				return this._FileNameInArchive;
			}
			set
			{
				bool flag = this._container.ZipFile == null;
				if (flag)
				{
					throw new ZipException("Cannot rename; this is not supported in ZipOutputStream/ZipInputStream.");
				}
				bool flag2 = string.IsNullOrEmpty(value);
				if (flag2)
				{
					throw new ZipException("The FileName must be non empty and non-null.");
				}
				string text = ZipEntry.NameInArchive(value, null);
				bool flag3 = this._FileNameInArchive == text;
				if (!flag3)
				{
					this._container.ZipFile.RemoveEntry(this);
					this._container.ZipFile.InternalAddEntry(text, this);
					this._FileNameInArchive = text;
					this._container.ZipFile.NotifyEntryChanged();
					this._metadataChanged = true;
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0001166C File Offset: 0x0000F86C
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00011684 File Offset: 0x0000F884
		public Stream InputStream
		{
			get
			{
				return this._sourceStream;
			}
			set
			{
				bool flag = this._Source != ZipEntrySource.Stream;
				if (flag)
				{
					throw new ZipException("You must not set the input stream for this entry.");
				}
				this._sourceWasJitProvided = true;
				this._sourceStream = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000116BC File Offset: 0x0000F8BC
		public bool InputStreamWasJitProvided
		{
			get
			{
				return this._sourceWasJitProvided;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000116D4 File Offset: 0x0000F8D4
		public ZipEntrySource Source
		{
			get
			{
				return this._Source;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000116EC File Offset: 0x0000F8EC
		public short VersionNeeded
		{
			get
			{
				return this._VersionNeeded;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00011704 File Offset: 0x0000F904
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0001171C File Offset: 0x0000F91C
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00011730 File Offset: 0x0000F930
		public bool? RequiresZip64
		{
			get
			{
				return this._entryRequiresZip64;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00011748 File Offset: 0x0000F948
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00011760 File Offset: 0x0000F960
		public short BitField
		{
			get
			{
				return this._BitField;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00011778 File Offset: 0x0000F978
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00011790 File Offset: 0x0000F990
		public CompressionMethod CompressionMethod
		{
			get
			{
				return (CompressionMethod)this._CompressionMethod;
			}
			set
			{
				bool flag = value == (CompressionMethod)this._CompressionMethod;
				if (!flag)
				{
					bool flag2 = value != CompressionMethod.None && value != CompressionMethod.Deflate;
					if (flag2)
					{
						throw new InvalidOperationException("Unsupported compression method.");
					}
					this._CompressionMethod = (short)value;
					bool flag3 = this._CompressionMethod == 0;
					if (flag3)
					{
						this._CompressionLevel = CompressionLevel.None;
					}
					else
					{
						bool flag4 = this.CompressionLevel == CompressionLevel.None;
						if (flag4)
						{
							this._CompressionLevel = CompressionLevel.Default;
						}
					}
					bool flag5 = this._container.ZipFile != null;
					if (flag5)
					{
						this._container.ZipFile.NotifyEntryChanged();
					}
					this._restreamRequiredOnSave = true;
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00011828 File Offset: 0x0000FA28
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00011840 File Offset: 0x0000FA40
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this._CompressionLevel;
			}
			set
			{
				bool flag = this._CompressionMethod != 8 && this._CompressionMethod != 0;
				if (!flag)
				{
					bool flag2 = value == CompressionLevel.Default && this._CompressionMethod == 8;
					if (!flag2)
					{
						this._CompressionLevel = value;
						bool flag3 = value == CompressionLevel.None && this._CompressionMethod == 0;
						if (!flag3)
						{
							bool flag4 = this._CompressionLevel == CompressionLevel.None;
							if (flag4)
							{
								this._CompressionMethod = 0;
							}
							else
							{
								this._CompressionMethod = 8;
							}
							bool flag5 = this._container.ZipFile != null;
							if (flag5)
							{
								this._container.ZipFile.NotifyEntryChanged();
							}
							this._restreamRequiredOnSave = true;
						}
					}
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public long CompressedSize
		{
			get
			{
				return this._CompressedSize;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000118FC File Offset: 0x0000FAFC
		public long UncompressedSize
		{
			get
			{
				return this._UncompressedSize;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00011914 File Offset: 0x0000FB14
		public double CompressionRatio
		{
			get
			{
				bool flag = this.UncompressedSize == 0L;
				double num;
				if (flag)
				{
					num = 0.0;
				}
				else
				{
					num = 100.0 * (1.0 - 1.0 * (double)this.CompressedSize / (1.0 * (double)this.UncompressedSize));
				}
				return num;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00011978 File Offset: 0x0000FB78
		public int Crc
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00011990 File Offset: 0x0000FB90
		public bool IsDirectory
		{
			get
			{
				return this._IsDirectory;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public bool UsesEncryption
		{
			get
			{
				return this._Encryption_FromZipFile > EncryptionAlgorithm.None;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000119C4 File Offset: 0x0000FBC4
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000119DC File Offset: 0x0000FBDC
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				bool flag = value == this._Encryption;
				if (!flag)
				{
					bool flag2 = value == EncryptionAlgorithm.Unsupported;
					if (flag2)
					{
						throw new InvalidOperationException("You may not set Encryption to that value.");
					}
					this._Encryption = value;
					this._restreamRequiredOnSave = true;
					bool flag3 = this._container.ZipFile != null;
					if (flag3)
					{
						this._container.ZipFile.NotifyEntryChanged();
					}
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00011A40 File Offset: 0x0000FC40
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
					this._Encryption = EncryptionAlgorithm.None;
				}
				else
				{
					bool flag2 = this._Source == ZipEntrySource.ZipFile && !this._sourceIsEncrypted;
					if (flag2)
					{
						this._restreamRequiredOnSave = true;
					}
					bool flag3 = this.Encryption == EncryptionAlgorithm.None;
					if (flag3)
					{
						this._Encryption = EncryptionAlgorithm.PkzipWeak;
					}
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00011AC0 File Offset: 0x0000FCC0
		internal bool IsChanged
		{
			get
			{
				return this._restreamRequiredOnSave | this._metadataChanged;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00011ADF File Offset: 0x0000FCDF
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00011AE7 File Offset: 0x0000FCE7
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00011AF0 File Offset: 0x0000FCF0
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public ZipErrorAction ZipErrorAction { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00011B04 File Offset: 0x0000FD04
		public bool IncludedInMostRecentSave
		{
			get
			{
				return !this._skippedDuringSave;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00011B1F File Offset: 0x0000FD1F
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00011B27 File Offset: 0x0000FD27
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00011B30 File Offset: 0x0000FD30
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00011B60 File Offset: 0x0000FD60
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this.AlternateEncoding == Encoding.GetEncoding("UTF-8") && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this.AlternateEncoding = Encoding.GetEncoding("UTF-8");
					this.AlternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					this.AlternateEncoding = ZipFile.DefaultEncoding;
					this.AlternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00011BA6 File Offset: 0x0000FDA6
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00011BAE File Offset: 0x0000FDAE
		[Obsolete("This property is obsolete since v1.9.1.6. Use AlternateEncoding and AlternateEncodingUsage instead.", true)]
		public Encoding ProvisionalAlternateEncoding { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00011BB7 File Offset: 0x0000FDB7
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00011BBF File Offset: 0x0000FDBF
		public Encoding AlternateEncoding { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public ZipOption AlternateEncodingUsage { get; set; }

		// Token: 0x06000263 RID: 611 RVA: 0x00011BDC File Offset: 0x0000FDDC
		internal static string NameInArchive(string filename, string directoryPathInArchive)
		{
			bool flag = directoryPathInArchive == null;
			string text;
			if (flag)
			{
				text = filename;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(directoryPathInArchive);
				if (flag2)
				{
					text = Path.GetFileName(filename);
				}
				else
				{
					text = Path.Combine(directoryPathInArchive, Path.GetFileName(filename));
				}
			}
			return SharedUtilities.NormalizePathForUseInZipFile(text);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00011C2C File Offset: 0x0000FE2C
		internal static ZipEntry CreateFromNothing(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.None, null, null);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00011C48 File Offset: 0x0000FE48
		internal static ZipEntry CreateFromFile(string filename, string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.FileSystem, filename, null);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00011C64 File Offset: 0x0000FE64
		internal static ZipEntry CreateForStream(string entryName, Stream s)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.Stream, s, null);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00011C80 File Offset: 0x0000FE80
		internal static ZipEntry CreateForWriter(string entryName, WriteDelegate d)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.WriteDelegate, d, null);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00011C9C File Offset: 0x0000FE9C
		internal static ZipEntry CreateForJitStreamProvider(string nameInArchive, OpenDelegate opener, CloseDelegate closer)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.JitStream, opener, closer);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		internal static ZipEntry CreateForZipOutputStream(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.ZipOutputStream, null, null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011CD4 File Offset: 0x0000FED4
		private static ZipEntry Create(string nameInArchive, ZipEntrySource source, object arg1, object arg2)
		{
			bool flag = string.IsNullOrEmpty(nameInArchive);
			if (flag)
			{
				throw new ZipException("The entry name must be non-null and non-empty.");
			}
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._VersionMadeBy = 45;
			zipEntry._Source = source;
			zipEntry._Mtime = (zipEntry._Atime = (zipEntry._Ctime = DateTime.UtcNow));
			bool flag2 = source == ZipEntrySource.Stream;
			if (flag2)
			{
				zipEntry._sourceStream = arg1 as Stream;
			}
			else
			{
				bool flag3 = source == ZipEntrySource.WriteDelegate;
				if (flag3)
				{
					zipEntry._WriteDelegate = arg1 as WriteDelegate;
				}
				else
				{
					bool flag4 = source == ZipEntrySource.JitStream;
					if (flag4)
					{
						zipEntry._OpenDelegate = arg1 as OpenDelegate;
						zipEntry._CloseDelegate = arg2 as CloseDelegate;
					}
					else
					{
						bool flag5 = source == ZipEntrySource.ZipOutputStream;
						if (!flag5)
						{
							bool flag6 = source == ZipEntrySource.None;
							if (flag6)
							{
								zipEntry._Source = ZipEntrySource.FileSystem;
							}
							else
							{
								string text = arg1 as string;
								bool flag7 = string.IsNullOrEmpty(text);
								if (flag7)
								{
									throw new ZipException("The filename must be non-null and non-empty.");
								}
								try
								{
									zipEntry._Mtime = File.GetLastWriteTime(text).ToUniversalTime();
									zipEntry._Ctime = File.GetCreationTime(text).ToUniversalTime();
									zipEntry._Atime = File.GetLastAccessTime(text).ToUniversalTime();
									bool flag8 = File.Exists(text) || Directory.Exists(text);
									if (flag8)
									{
										zipEntry._ExternalFileAttrs = (int)File.GetAttributes(text);
									}
									zipEntry._ntfsTimesAreSet = true;
									zipEntry._LocalFileName = Path.GetFullPath(text);
								}
								catch (PathTooLongException ex)
								{
									string text2 = string.Format("The path is too long, filename={0}", text);
									throw new ZipException(text2, ex);
								}
							}
						}
					}
				}
			}
			zipEntry._LastModified = zipEntry._Mtime;
			zipEntry._FileNameInArchive = SharedUtilities.NormalizePathForUseInZipFile(nameInArchive);
			return zipEntry;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00011EA0 File Offset: 0x000100A0
		internal void MarkAsDirectory()
		{
			this._IsDirectory = true;
			bool flag = !this._FileNameInArchive.EndsWith("/");
			if (flag)
			{
				this._FileNameInArchive += "/";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00011EE4 File Offset: 0x000100E4
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00011EFC File Offset: 0x000100FC
		public bool IsText
		{
			get
			{
				return this._IsText;
			}
			set
			{
				this._IsText = value;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011F08 File Offset: 0x00010108
		public override string ToString()
		{
			return string.Format("ZipEntry::{0}", this.FileName);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00011F2C File Offset: 0x0001012C
		internal Stream ArchiveStream
		{
			get
			{
				bool flag = this._archiveStream == null;
				if (flag)
				{
					bool flag2 = this._container.ZipFile != null;
					if (flag2)
					{
						ZipFile zipFile = this._container.ZipFile;
						zipFile.Reset(false);
						this._archiveStream = zipFile.StreamForDiskNumber(this._diskNumber);
					}
					else
					{
						this._archiveStream = this._container.ZipOutputStream.OutputStream;
					}
				}
				return this._archiveStream;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00011FA8 File Offset: 0x000101A8
		private void SetFdpLoh()
		{
			long position = this.ArchiveStream.Position;
			try
			{
				this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			}
			catch (IOException ex)
			{
				string text = string.Format("Exception seeking  entry({0}) offset(0x{1:X8}) len(0x{2:X8})", this.FileName, this._RelativeOffsetOfLocalHeader, this.ArchiveStream.Length);
				throw new BadStateException(text, ex);
			}
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			short num = (short)((int)array[26] + (int)array[27] * 256);
			short num2 = (short)((int)array[28] + (int)array[29] * 256);
			this.ArchiveStream.Seek((long)(num + num2), SeekOrigin.Current);
			this._LengthOfHeader = (int)(30 + num2 + num) + ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
			this.__FileDataPosition = this._RelativeOffsetOfLocalHeader + (long)this._LengthOfHeader;
			this.ArchiveStream.Seek(position, SeekOrigin.Begin);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000120AC File Offset: 0x000102AC
		internal static int GetLengthOfCryptoHeaderBytes(EncryptionAlgorithm a)
		{
			bool flag = a == EncryptionAlgorithm.None;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = a == EncryptionAlgorithm.PkzipWeak;
				if (!flag2)
				{
					throw new ZipException("internal error");
				}
				num = 12;
			}
			return num;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000120E0 File Offset: 0x000102E0
		internal long FileDataPosition
		{
			get
			{
				bool flag = this.__FileDataPosition == -1L;
				if (flag)
				{
					this.SetFdpLoh();
				}
				return this.__FileDataPosition;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00012110 File Offset: 0x00010310
		private int LengthOfHeader
		{
			get
			{
				bool flag = this._LengthOfHeader == 0;
				if (flag)
				{
					this.SetFdpLoh();
				}
				return this._LengthOfHeader;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001213C File Offset: 0x0001033C
		public void Extract()
		{
			this.InternalExtract(".", null, null);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0001214D File Offset: 0x0001034D
		public void Extract(ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00012166 File Offset: 0x00010366
		public void Extract(Stream stream)
		{
			this.InternalExtract(null, stream, null);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00012173 File Offset: 0x00010373
		public void Extract(string baseDirectory)
		{
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00012180 File Offset: 0x00010380
		public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00012195 File Offset: 0x00010395
		public void ExtractWithPassword(string password)
		{
			this.InternalExtract(".", null, password);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000121A6 File Offset: 0x000103A6
		public void ExtractWithPassword(string baseDirectory, string password)
		{
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000121B3 File Offset: 0x000103B3
		public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, password);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000121CC File Offset: 0x000103CC
		public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000121E1 File Offset: 0x000103E1
		public void ExtractWithPassword(Stream stream, string password)
		{
			this.InternalExtract(null, stream, password);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000121F0 File Offset: 0x000103F0
		public CrcCalculatorStream OpenReader()
		{
			bool flag = this._container.ZipFile == null;
			if (flag)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(this._Password ?? this._container.Password);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001223C File Offset: 0x0001043C
		public CrcCalculatorStream OpenReader(string password)
		{
			bool flag = this._container.ZipFile == null;
			if (flag)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(password);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00012274 File Offset: 0x00010474
		internal CrcCalculatorStream InternalOpenReader(string password)
		{
			this.ValidateCompression();
			this.ValidateEncryption();
			this.SetupCryptoForExtract(password);
			bool flag = this._Source != ZipEntrySource.ZipFile;
			if (flag)
			{
				throw new BadStateException("You must call ZipFile.Save before calling OpenReader");
			}
			long num = ((this._CompressionMethod_FromZipFile == 0) ? this._CompressedFileDataSize : this.UncompressedSize);
			Stream archiveStream = this.ArchiveStream;
			this.ArchiveStream.Seek(this.FileDataPosition, SeekOrigin.Begin);
			this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
			Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
			return new CrcCalculatorStream(extractDecompressor, num);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001230C File Offset: 0x0001050C
		private void OnExtractProgress(long bytesWritten, long totalBytesToWrite)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractBlock(this, bytesWritten, totalBytesToWrite);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00012348 File Offset: 0x00010548
		private void OnBeforeExtract(string path)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				bool flag2 = !this._container.ZipFile._inExtractAll;
				if (flag2)
				{
					this._ioOperationCanceled = this._container.ZipFile.OnSingleEntryExtract(this, path, true);
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001239C File Offset: 0x0001059C
		private void OnAfterExtract(string path)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				bool flag2 = !this._container.ZipFile._inExtractAll;
				if (flag2)
				{
					this._container.ZipFile.OnSingleEntryExtract(this, path, false);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000123EC File Offset: 0x000105EC
		private void OnExtractExisting(string path)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractExisting(this, path);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00012424 File Offset: 0x00010624
		private static void ReallyDelete(string fileName)
		{
			bool flag = (File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
			if (flag)
			{
				File.SetAttributes(fileName, FileAttributes.Normal);
			}
			File.Delete(fileName);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00012454 File Offset: 0x00010654
		private void WriteStatus(string format, params object[] args)
		{
			bool flag = this._container.ZipFile != null && this._container.ZipFile.Verbose;
			if (flag)
			{
				this._container.ZipFile.StatusMessageTextWriter.WriteLine(format, args);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000124A0 File Offset: 0x000106A0
		private void InternalExtract(string baseDir, Stream outstream, string password)
		{
			bool flag = this._container == null;
			if (flag)
			{
				throw new BadStateException("This entry is an orphan");
			}
			bool flag2 = this._container.ZipFile == null;
			if (flag2)
			{
				throw new InvalidOperationException("Use Extract() only with ZipFile.");
			}
			this._container.ZipFile.Reset(false);
			bool flag3 = this._Source != ZipEntrySource.ZipFile;
			if (flag3)
			{
				throw new BadStateException("You must call ZipFile.Save before calling any Extract method");
			}
			this.OnBeforeExtract(baseDir);
			this._ioOperationCanceled = false;
			string text = null;
			Stream stream = null;
			bool flag4 = false;
			bool flag5 = false;
			try
			{
				this.ValidateCompression();
				this.ValidateEncryption();
				bool flag6 = this.ValidateOutput(baseDir, outstream, out text);
				if (flag6)
				{
					this.WriteStatus("extract dir {0}...", new object[] { text });
					this.OnAfterExtract(baseDir);
				}
				else
				{
					bool flag7 = text != null;
					if (flag7)
					{
						bool flag8 = File.Exists(text);
						if (flag8)
						{
							flag4 = true;
							int num = this.CheckExtractExistingFile(baseDir, text);
							bool flag9 = num == 2;
							if (flag9)
							{
								goto IL_0340;
							}
							bool flag10 = num == 1;
							if (flag10)
							{
								return;
							}
						}
					}
					string text2 = password ?? this._Password ?? this._container.Password;
					bool flag11 = this._Encryption_FromZipFile > EncryptionAlgorithm.None;
					if (flag11)
					{
						bool flag12 = text2 == null;
						if (flag12)
						{
							throw new BadPasswordException();
						}
						this.SetupCryptoForExtract(text2);
					}
					bool flag13 = text != null;
					if (flag13)
					{
						this.WriteStatus("extract file {0}...", new object[] { text });
						text += ".tmp";
						string directoryName = Path.GetDirectoryName(text);
						bool flag14 = !Directory.Exists(directoryName);
						if (flag14)
						{
							Directory.CreateDirectory(directoryName);
						}
						else
						{
							bool flag15 = this._container.ZipFile != null;
							if (flag15)
							{
								flag5 = this._container.ZipFile._inExtractAll;
							}
						}
						stream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite);
					}
					else
					{
						this.WriteStatus("extract entry {0} to stream...", new object[] { this.FileName });
						stream = outstream;
					}
					bool ioOperationCanceled = this._ioOperationCanceled;
					if (!ioOperationCanceled)
					{
						int num2 = this.ExtractOne(stream);
						bool ioOperationCanceled2 = this._ioOperationCanceled;
						if (!ioOperationCanceled2)
						{
							this.VerifyCrcAfterExtract(num2);
							bool flag16 = text != null;
							if (flag16)
							{
								stream.Close();
								stream = null;
								string text3 = text;
								string text4 = null;
								text = text3.Substring(0, text3.Length - 4);
								bool flag17 = flag4;
								if (flag17)
								{
									text4 = text + ".PendingOverwrite";
									File.Move(text, text4);
								}
								File.Move(text3, text);
								this._SetTimes(text, true);
								bool flag18 = text4 != null && File.Exists(text4);
								if (flag18)
								{
									ZipEntry.ReallyDelete(text4);
								}
								bool flag19 = flag5;
								if (flag19)
								{
									bool flag20 = this.FileName.IndexOf('/') != -1;
									if (flag20)
									{
										string directoryName2 = Path.GetDirectoryName(this.FileName);
										bool flag21 = this._container.ZipFile[directoryName2] == null;
										if (flag21)
										{
											this._SetTimes(Path.GetDirectoryName(text), false);
										}
									}
								}
								bool flag22 = ((int)this._VersionMadeBy & 65280) == 2560 || ((int)this._VersionMadeBy & 65280) == 0;
								if (flag22)
								{
									File.SetAttributes(text, (FileAttributes)this._ExternalFileAttrs);
								}
							}
							this.OnAfterExtract(baseDir);
						}
					}
					IL_0340:;
				}
			}
			catch (Exception)
			{
				this._ioOperationCanceled = true;
				throw;
			}
			finally
			{
				bool ioOperationCanceled3 = this._ioOperationCanceled;
				if (ioOperationCanceled3)
				{
					bool flag23 = text != null;
					if (flag23)
					{
						try
						{
							bool flag24 = stream != null;
							if (flag24)
							{
								stream.Close();
							}
							bool flag25 = File.Exists(text) && !flag4;
							if (flag25)
							{
								File.Delete(text);
							}
						}
						finally
						{
						}
					}
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001289C File Offset: 0x00010A9C
		internal void VerifyCrcAfterExtract(int actualCrc32)
		{
			bool flag = actualCrc32 != this._Crc32;
			if (flag)
			{
				throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + string.Format("Expected 0x{0:X8}, Actual 0x{1:X8}", this._Crc32, actualCrc32));
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000128E8 File Offset: 0x00010AE8
		private int CheckExtractExistingFile(string baseDir, string targetFileName)
		{
			int num = 0;
			for (;;)
			{
				switch (this.ExtractExistingFile)
				{
				case ExtractExistingFileAction.OverwriteSilently:
					goto IL_0023;
				case ExtractExistingFileAction.DoNotOverwrite:
					goto IL_003D;
				case ExtractExistingFileAction.InvokeExtractProgressEvent:
				{
					bool flag = num > 0;
					if (flag)
					{
						goto Block_2;
					}
					this.OnExtractExisting(baseDir);
					bool ioOperationCanceled = this._ioOperationCanceled;
					if (ioOperationCanceled)
					{
						goto Block_3;
					}
					num++;
					continue;
				}
				}
				break;
			}
			goto IL_0097;
			IL_0023:
			this.WriteStatus("the file {0} exists; will overwrite it...", new object[] { targetFileName });
			return 0;
			IL_003D:
			this.WriteStatus("the file {0} exists; not extracting entry...", new object[] { this.FileName });
			this.OnAfterExtract(baseDir);
			return 1;
			Block_2:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
			Block_3:
			return 2;
			IL_0097:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000129AC File Offset: 0x00010BAC
		private void _CheckRead(int nbytes)
		{
			bool flag = nbytes == 0;
			if (flag)
			{
				throw new BadReadException(string.Format("bad read of entry {0} from compressed archive.", this.FileName));
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000129D8 File Offset: 0x00010BD8
		private int ExtractOne(Stream output)
		{
			int num = 0;
			Stream archiveStream = this.ArchiveStream;
			try
			{
				archiveStream.Seek(this.FileDataPosition, SeekOrigin.Begin);
				byte[] array = new byte[this.BufferSize];
				long num2 = ((this._CompressionMethod_FromZipFile != 0) ? this.UncompressedSize : this._CompressedFileDataSize);
				this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
				Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
				long num3 = 0L;
				using (CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(extractDecompressor))
				{
					while (num2 > 0L)
					{
						int num4 = ((num2 > (long)array.Length) ? array.Length : ((int)num2));
						int num5 = crcCalculatorStream.Read(array, 0, num4);
						this._CheckRead(num5);
						output.Write(array, 0, num5);
						num2 -= (long)num5;
						num3 += (long)num5;
						this.OnExtractProgress(num3, this.UncompressedSize);
						bool ioOperationCanceled = this._ioOperationCanceled;
						if (ioOperationCanceled)
						{
							break;
						}
					}
					num = crcCalculatorStream.Crc;
				}
			}
			finally
			{
				ZipSegmentedStream zipSegmentedStream = archiveStream as ZipSegmentedStream;
				bool flag = zipSegmentedStream != null;
				if (flag)
				{
					zipSegmentedStream.Dispose();
					this._archiveStream = null;
				}
			}
			return num;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00012B1C File Offset: 0x00010D1C
		internal Stream GetExtractDecompressor(Stream input2)
		{
			short compressionMethod_FromZipFile = this._CompressionMethod_FromZipFile;
			Stream stream;
			if (compressionMethod_FromZipFile != 0)
			{
				if (compressionMethod_FromZipFile != 8)
				{
					stream = null;
				}
				else
				{
					stream = new DeflateStream(input2, CompressionMode.Decompress, true);
				}
			}
			else
			{
				stream = input2;
			}
			return stream;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00012B50 File Offset: 0x00010D50
		internal Stream GetExtractDecryptor(Stream input)
		{
			bool flag = this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak;
			Stream stream;
			if (flag)
			{
				stream = new ZipCipherStream(input, this._zipCrypto_forExtract, CryptoMode.Decrypt);
			}
			else
			{
				stream = input;
			}
			return stream;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00012B84 File Offset: 0x00010D84
		internal void _SetTimes(string fileOrDirectory, bool isFile)
		{
			try
			{
				bool ntfsTimesAreSet = this._ntfsTimesAreSet;
				if (ntfsTimesAreSet)
				{
					if (isFile)
					{
						bool flag = File.Exists(fileOrDirectory);
						if (flag)
						{
							File.SetCreationTimeUtc(fileOrDirectory, this._Ctime);
							File.SetLastAccessTimeUtc(fileOrDirectory, this._Atime);
							File.SetLastWriteTimeUtc(fileOrDirectory, this._Mtime);
						}
					}
					else
					{
						bool flag2 = Directory.Exists(fileOrDirectory);
						if (flag2)
						{
							Directory.SetCreationTimeUtc(fileOrDirectory, this._Ctime);
							Directory.SetLastAccessTimeUtc(fileOrDirectory, this._Atime);
							Directory.SetLastWriteTimeUtc(fileOrDirectory, this._Mtime);
						}
					}
				}
				else
				{
					DateTime dateTime = SharedUtilities.AdjustTime_Reverse(this.LastModified);
					if (isFile)
					{
						File.SetLastWriteTime(fileOrDirectory, dateTime);
					}
					else
					{
						Directory.SetLastWriteTime(fileOrDirectory, dateTime);
					}
				}
			}
			catch (IOException ex)
			{
				this.WriteStatus("failed to set time on {0}: {1}", new object[] { fileOrDirectory, ex.Message });
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00012C78 File Offset: 0x00010E78
		private string UnsupportedAlgorithm
		{
			get
			{
				string empty = string.Empty;
				uint unsupportedAlgorithmId = this._UnsupportedAlgorithmId;
				if (unsupportedAlgorithmId <= 26128U)
				{
					if (unsupportedAlgorithmId <= 26115U)
					{
						if (unsupportedAlgorithmId == 0U)
						{
							return "--";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26113U:
							return "DES";
						case 26114U:
							return "RC2";
						case 26115U:
							return "3DES-168";
						}
					}
					else
					{
						if (unsupportedAlgorithmId == 26121U)
						{
							return "3DES-112";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26126U:
							return "PKWare AES128";
						case 26127U:
							return "PKWare AES192";
						case 26128U:
							return "PKWare AES256";
						}
					}
				}
				else if (unsupportedAlgorithmId <= 26400U)
				{
					if (unsupportedAlgorithmId == 26370U)
					{
						return "RC2";
					}
					if (unsupportedAlgorithmId == 26400U)
					{
						return "Blowfish";
					}
				}
				else
				{
					if (unsupportedAlgorithmId == 26401U)
					{
						return "Twofish";
					}
					if (unsupportedAlgorithmId == 26625U)
					{
						return "RC4";
					}
					if (unsupportedAlgorithmId != 65535U)
					{
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._UnsupportedAlgorithmId);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00012DA8 File Offset: 0x00010FA8
		private string UnsupportedCompressionMethod
		{
			get
			{
				string empty = string.Empty;
				int compressionMethod = (int)this._CompressionMethod;
				if (compressionMethod <= 1)
				{
					if (compressionMethod == 0)
					{
						return "Store";
					}
					if (compressionMethod == 1)
					{
						return "Shrink";
					}
				}
				else
				{
					switch (compressionMethod)
					{
					case 8:
						return "DEFLATE";
					case 9:
						return "Deflate64";
					case 10:
					case 11:
					case 13:
						break;
					case 12:
						return "BZIP2";
					case 14:
						return "LZMA";
					default:
						if (compressionMethod == 19)
						{
							return "LZ77";
						}
						if (compressionMethod == 98)
						{
							return "PPMd";
						}
						break;
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._CompressionMethod);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00012E64 File Offset: 0x00011064
		internal void ValidateEncryption()
		{
			bool flag = this.Encryption != EncryptionAlgorithm.PkzipWeak && this.Encryption > EncryptionAlgorithm.None;
			if (!flag)
			{
				return;
			}
			bool flag2 = this._UnsupportedAlgorithmId > 0U;
			if (flag2)
			{
				throw new ZipException(string.Format("Cannot extract: Entry {0} is encrypted with an algorithm not supported by DotNetZip: {1}", this.FileName, this.UnsupportedAlgorithm));
			}
			throw new ZipException(string.Format("Cannot extract: Entry {0} uses an unsupported encryption algorithm ({1:X2})", this.FileName, (int)this.Encryption));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00012ED8 File Offset: 0x000110D8
		private void ValidateCompression()
		{
			bool flag = this._CompressionMethod_FromZipFile != 0 && this._CompressionMethod_FromZipFile != 8;
			if (flag)
			{
				throw new ZipException(string.Format("Entry {0} uses an unsupported compression method (0x{1:X2}, {2})", this.FileName, this._CompressionMethod_FromZipFile, this.UnsupportedCompressionMethod));
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00012F28 File Offset: 0x00011128
		private void SetupCryptoForExtract(string password)
		{
			bool flag = this._Encryption_FromZipFile == EncryptionAlgorithm.None;
			if (!flag)
			{
				bool flag2 = this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak;
				if (flag2)
				{
					bool flag3 = password == null;
					if (flag3)
					{
						throw new ZipException("Missing password.");
					}
					this.ArchiveStream.Seek(this.FileDataPosition - 12L, SeekOrigin.Begin);
					this._zipCrypto_forExtract = ZipCrypto.ForRead(password, this);
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00012F8C File Offset: 0x0001118C
		private bool ValidateOutput(string basedir, Stream outstream, out string outFileName)
		{
			bool flag = basedir != null;
			bool flag7;
			if (flag)
			{
				string text = this.FileName.Replace("\\", "/");
				bool flag2 = text.IndexOf(':') == 1;
				if (flag2)
				{
					text = text.Substring(2);
				}
				bool flag3 = text.StartsWith("/");
				if (flag3)
				{
					text = text.Substring(1);
				}
				bool flattenFoldersOnExtract = this._container.ZipFile.FlattenFoldersOnExtract;
				if (flattenFoldersOnExtract)
				{
					outFileName = Path.Combine(basedir, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
				}
				else
				{
					outFileName = Path.Combine(basedir, text);
				}
				bool flag4 = this.IsDirectory || this.FileName.EndsWith("/");
				if (flag4)
				{
					bool flag5 = !Directory.Exists(outFileName);
					if (flag5)
					{
						Directory.CreateDirectory(outFileName);
						this._SetTimes(outFileName, false);
					}
					else
					{
						bool flag6 = this.ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently;
						if (flag6)
						{
							this._SetTimes(outFileName, false);
						}
					}
					flag7 = true;
				}
				else
				{
					flag7 = false;
				}
			}
			else
			{
				bool flag8 = outstream != null;
				if (!flag8)
				{
					throw new ArgumentNullException("outstream");
				}
				outFileName = null;
				bool flag9 = this.IsDirectory || this.FileName.EndsWith("/");
				flag7 = flag9;
			}
			return flag7;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000130DC File Offset: 0x000112DC
		private void ReadExtraField()
		{
			this._readExtraDepth++;
			long position = this.ArchiveStream.Position;
			this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			int num = 26;
			short num2 = (short)((int)array[num++] + (int)array[num++] * 256);
			short num3 = (short)((int)array[num++] + (int)array[num++] * 256);
			this.ArchiveStream.Seek((long)num2, SeekOrigin.Current);
			this.ProcessExtraField(this.ArchiveStream, num3);
			this.ArchiveStream.Seek(position, SeekOrigin.Begin);
			this._readExtraDepth--;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001319C File Offset: 0x0001139C
		private static bool ReadHeader(ZipEntry ze, Encoding defaultEncoding)
		{
			int num = 0;
			ze._RelativeOffsetOfLocalHeader = ze.ArchiveStream.Position;
			int num2 = SharedUtilities.ReadEntrySignature(ze.ArchiveStream);
			num += 4;
			bool flag = ZipEntry.IsNotValidSig(num2);
			bool flag3;
			if (flag)
			{
				ze.ArchiveStream.Seek(-4L, SeekOrigin.Current);
				bool flag2 = ZipEntry.IsNotValidZipDirEntrySig(num2) && (long)num2 != 101010256L;
				if (flag2)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position  0x{1:X8}", num2, ze.ArchiveStream.Position));
				}
				flag3 = false;
			}
			else
			{
				byte[] array = new byte[26];
				int num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				bool flag4 = num3 != array.Length;
				if (flag4)
				{
					flag3 = false;
				}
				else
				{
					num += num3;
					int num4 = 0;
					ze._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
					ze._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
					ze._CompressionMethod_FromZipFile = (ze._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256));
					ze._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
					ze._LastModified = SharedUtilities.PackedToDateTime(ze._TimeBlob);
					ze._timestamp |= ZipEntryTimestamp.DOS;
					bool flag5 = (ze._BitField & 1) == 1;
					if (flag5)
					{
						ze._Encryption_FromZipFile = (ze._Encryption = EncryptionAlgorithm.PkzipWeak);
						ze._sourceIsEncrypted = true;
					}
					ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
					ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
					ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
					bool flag6 = (uint)ze._CompressedSize == uint.MaxValue || (uint)ze._UncompressedSize == uint.MaxValue;
					if (flag6)
					{
						ze._InputUsesZip64 = true;
					}
					short num5 = (short)((int)array[num4++] + (int)array[num4++] * 256);
					short num6 = (short)((int)array[num4++] + (int)array[num4++] * 256);
					array = new byte[(int)num5];
					num3 = ze.ArchiveStream.Read(array, 0, array.Length);
					num += num3;
					bool flag7 = (ze._BitField & 2048) == 2048;
					if (flag7)
					{
						ze.AlternateEncoding = Encoding.UTF8;
						ze.AlternateEncodingUsage = ZipOption.Always;
					}
					ze._FileNameInArchive = ze.AlternateEncoding.GetString(array, 0, array.Length);
					bool flag8 = ze._FileNameInArchive.EndsWith("/");
					if (flag8)
					{
						ze.MarkAsDirectory();
					}
					num += ze.ProcessExtraField(ze.ArchiveStream, num6);
					ze._LengthOfTrailer = 0;
					bool flag9 = !ze._FileNameInArchive.EndsWith("/") && (ze._BitField & 8) == 8;
					if (flag9)
					{
						long position = ze.ArchiveStream.Position;
						bool flag10 = true;
						long num7 = 0L;
						int num8 = 0;
						while (flag10)
						{
							num8++;
							bool flag11 = ze._container.ZipFile != null;
							if (flag11)
							{
								ze._container.ZipFile.OnReadBytes(ze);
							}
							long num9 = SharedUtilities.FindSignature(ze.ArchiveStream, 134695760);
							bool flag12 = num9 == -1L;
							if (flag12)
							{
								return false;
							}
							num7 += num9;
							bool inputUsesZip = ze._InputUsesZip64;
							if (inputUsesZip)
							{
								array = new byte[20];
								num3 = ze.ArchiveStream.Read(array, 0, array.Length);
								bool flag13 = num3 != 20;
								if (flag13)
								{
									return false;
								}
								num4 = 0;
								ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
								ze._CompressedSize = BitConverter.ToInt64(array, num4);
								num4 += 8;
								ze._UncompressedSize = BitConverter.ToInt64(array, num4);
								num4 += 8;
								ze._LengthOfTrailer += 24;
							}
							else
							{
								array = new byte[12];
								num3 = ze.ArchiveStream.Read(array, 0, array.Length);
								bool flag14 = num3 != 12;
								if (flag14)
								{
									return false;
								}
								num4 = 0;
								ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
								ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
								ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
								ze._LengthOfTrailer += 16;
							}
							flag10 = num7 != ze._CompressedSize;
							bool flag15 = flag10;
							if (flag15)
							{
								ze.ArchiveStream.Seek(-12L, SeekOrigin.Current);
								num7 += 4L;
							}
						}
						ze.ArchiveStream.Seek(position, SeekOrigin.Begin);
					}
					ze._CompressedFileDataSize = ze._CompressedSize;
					bool flag16 = (ze._BitField & 1) == 1;
					if (flag16)
					{
						ze._WeakEncryptionHeader = new byte[12];
						num += ZipEntry.ReadWeakEncryptionHeader(ze._archiveStream, ze._WeakEncryptionHeader);
						ze._CompressedFileDataSize -= 12L;
					}
					ze._LengthOfHeader = num;
					ze._TotalEntrySize = (long)ze._LengthOfHeader + ze._CompressedFileDataSize + (long)ze._LengthOfTrailer;
					flag3 = true;
				}
			}
			return flag3;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000138D0 File Offset: 0x00011AD0
		internal static int ReadWeakEncryptionHeader(Stream s, byte[] buffer)
		{
			int num = s.Read(buffer, 0, 12);
			bool flag = num != 12;
			if (flag)
			{
				throw new ZipException(string.Format("Unexpected end of data at position 0x{0:X8}", s.Position));
			}
			return num;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00013918 File Offset: 0x00011B18
		private static bool IsNotValidSig(int signature)
		{
			return signature != 67324752;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00013938 File Offset: 0x00011B38
		internal static ZipEntry ReadEntry(ZipContainer zc, bool first)
		{
			ZipFile zipFile = zc.ZipFile;
			Stream readStream = zc.ReadStream;
			Encoding alternateEncoding = zc.AlternateEncoding;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = zc;
			zipEntry._archiveStream = readStream;
			bool flag = zipFile != null;
			if (flag)
			{
				zipFile.OnReadEntry(true, null);
			}
			if (first)
			{
				ZipEntry.HandlePK00Prefix(readStream);
			}
			bool flag2 = !ZipEntry.ReadHeader(zipEntry, alternateEncoding);
			ZipEntry zipEntry2;
			if (flag2)
			{
				zipEntry2 = null;
			}
			else
			{
				zipEntry.__FileDataPosition = zipEntry.ArchiveStream.Position;
				readStream.Seek(zipEntry._CompressedFileDataSize + (long)zipEntry._LengthOfTrailer, SeekOrigin.Current);
				ZipEntry.HandleUnexpectedDataDescriptor(zipEntry);
				bool flag3 = zipFile != null;
				if (flag3)
				{
					zipFile.OnReadBytes(zipEntry);
					zipFile.OnReadEntry(false, zipEntry);
				}
				zipEntry2 = zipEntry;
			}
			return zipEntry2;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00013A00 File Offset: 0x00011C00
		internal static void HandlePK00Prefix(Stream s)
		{
			uint num = (uint)SharedUtilities.ReadInt(s);
			bool flag = num != 808471376U;
			if (flag)
			{
				s.Seek(-4L, SeekOrigin.Current);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00013A34 File Offset: 0x00011C34
		private static void HandleUnexpectedDataDescriptor(ZipEntry entry)
		{
			Stream archiveStream = entry.ArchiveStream;
			uint num = (uint)SharedUtilities.ReadInt(archiveStream);
			bool flag = (ulong)num == (ulong)((long)entry._Crc32);
			if (flag)
			{
				int num2 = SharedUtilities.ReadInt(archiveStream);
				bool flag2 = (long)num2 == entry._CompressedSize;
				if (flag2)
				{
					num2 = SharedUtilities.ReadInt(archiveStream);
					bool flag3 = (long)num2 == entry._UncompressedSize;
					if (!flag3)
					{
						archiveStream.Seek(-12L, SeekOrigin.Current);
					}
				}
				else
				{
					archiveStream.Seek(-8L, SeekOrigin.Current);
				}
			}
			else
			{
				archiveStream.Seek(-4L, SeekOrigin.Current);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00013AC0 File Offset: 0x00011CC0
		internal static int FindExtraFieldSegment(byte[] extra, int offx, ushort targetHeaderId)
		{
			int num = offx;
			while (num + 3 < extra.Length)
			{
				ushort num2 = (ushort)((int)extra[num++] + (int)extra[num++] * 256);
				bool flag = num2 == targetHeaderId;
				if (flag)
				{
					return num - 2;
				}
				short num3 = (short)((int)extra[num++] + (int)extra[num++] * 256);
				num += (int)num3;
			}
			return -1;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00013B2C File Offset: 0x00011D2C
		internal int ProcessExtraField(Stream s, short extraFieldLength)
		{
			int num = 0;
			bool flag = extraFieldLength > 0;
			if (flag)
			{
				byte[] array = (this._Extra = new byte[(int)extraFieldLength]);
				num = s.Read(array, 0, array.Length);
				long num2 = s.Position - (long)num;
				int num3 = 0;
				while (num3 + 3 < array.Length)
				{
					int num4 = num3;
					ushort num5 = (ushort)((int)array[num3++] + (int)array[num3++] * 256);
					short num6 = (short)((int)array[num3++] + (int)array[num3++] * 256);
					ushort num7 = num5;
					if (num7 <= 23)
					{
						if (num7 != 1)
						{
							if (num7 != 10)
							{
								if (num7 == 23)
								{
									num3 = this.ProcessExtraFieldPkwareStrongEncryption(array, num3);
								}
							}
							else
							{
								num3 = this.ProcessExtraFieldWindowsTimes(array, num3, num6, num2);
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldZip64(array, num3, num6, num2);
						}
					}
					else if (num7 <= 22613)
					{
						if (num7 != 21589)
						{
							if (num7 == 22613)
							{
								num3 = this.ProcessExtraFieldInfoZipTimes(array, num3, num6, num2);
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldUnixTimes(array, num3, num6, num2);
						}
					}
					else if (num7 != 30805)
					{
						if (num7 != 30837)
						{
						}
					}
					num3 = num4 + (int)num6 + 4;
				}
			}
			return num;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00013C84 File Offset: 0x00011E84
		private int ProcessExtraFieldPkwareStrongEncryption(byte[] Buffer, int j)
		{
			j += 2;
			this._UnsupportedAlgorithmId = (uint)((ushort)((int)Buffer[j++] + (int)Buffer[j++] * 256));
			this._Encryption_FromZipFile = (this._Encryption = EncryptionAlgorithm.Unsupported);
			return j;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00013CCC File Offset: 0x00011ECC
		private int ProcessExtraFieldZip64(byte[] buffer, int j, short dataSize, long posn)
		{
			this._InputUsesZip64 = true;
			bool flag = dataSize > 28;
			if (flag)
			{
				throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) for ZIP64 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int remainingData = (int)dataSize;
			ZipEntry.Func<long> func = delegate
			{
				bool flag5 = remainingData < 8;
				if (flag5)
				{
					throw new BadReadException(string.Format("  Missing data for ZIP64 extra field, position 0x{0:X16}", posn));
				}
				long num = BitConverter.ToInt64(buffer, j);
				j += 8;
				remainingData -= 8;
				return num;
			};
			bool flag2 = this._UncompressedSize == (long)((ulong)(-1));
			if (flag2)
			{
				this._UncompressedSize = func();
			}
			bool flag3 = this._CompressedSize == (long)((ulong)(-1));
			if (flag3)
			{
				this._CompressedSize = func();
			}
			bool flag4 = this._RelativeOffsetOfLocalHeader == (long)((ulong)(-1));
			if (flag4)
			{
				this._RelativeOffsetOfLocalHeader = func();
			}
			return j;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00013D9C File Offset: 0x00011F9C
		private int ProcessExtraFieldInfoZipTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			bool flag = dataSize != 12 && dataSize != 8;
			if (flag)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for InfoZip v1 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int num = BitConverter.ToInt32(buffer, j);
			this._Mtime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			num = BitConverter.ToInt32(buffer, j);
			this._Atime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			this._Ctime = DateTime.UtcNow;
			this._ntfsTimesAreSet = true;
			this._timestamp |= ZipEntryTimestamp.InfoZip1;
			return j;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00013E40 File Offset: 0x00012040
		private int ProcessExtraFieldUnixTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			bool flag = dataSize != 13 && dataSize != 9 && dataSize != 5;
			if (flag)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for Extended Timestamp extra field at position 0x{1:X16}", dataSize, posn));
			}
			int remainingData = (int)dataSize;
			ZipEntry.Func<DateTime> func = delegate
			{
				int num2 = BitConverter.ToInt32(buffer, j);
				j += 4;
				remainingData -= 4;
				return ZipEntry._unixEpoch.AddSeconds((double)num2);
			};
			bool flag2 = dataSize == 13 || this._readExtraDepth > 0;
			if (flag2)
			{
				byte[] buffer2 = buffer;
				int num = j;
				j = num + 1;
				byte b = buffer2[num];
				num = remainingData;
				remainingData = num - 1;
				bool flag3 = (b & 1) != 0 && remainingData >= 4;
				if (flag3)
				{
					this._Mtime = func();
				}
				this._Atime = (((b & 2) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				this._Ctime = (((b & 4) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				this._timestamp |= ZipEntryTimestamp.Unix;
				this._ntfsTimesAreSet = true;
				this._emitUnixTimes = true;
			}
			else
			{
				this.ReadExtraField();
			}
			return j;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00013F90 File Offset: 0x00012190
		private int ProcessExtraFieldWindowsTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			bool flag = dataSize != 32;
			if (flag)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for NTFS times extra field at position 0x{1:X16}", dataSize, posn));
			}
			j += 4;
			short num = (short)((int)buffer[j] + (int)buffer[j + 1] * 256);
			short num2 = (short)((int)buffer[j + 2] + (int)buffer[j + 3] * 256);
			j += 4;
			bool flag2 = num == 1 && num2 == 24;
			if (flag2)
			{
				long num3 = BitConverter.ToInt64(buffer, j);
				this._Mtime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Atime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Ctime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				this._ntfsTimesAreSet = true;
				this._timestamp |= ZipEntryTimestamp.Windows;
				this._emitNtfsTimes = true;
			}
			return j;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001407C File Offset: 0x0001227C
		internal void WriteCentralDirectoryEntry(Stream s)
		{
			byte[] array = new byte[4096];
			int num = 0;
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 1;
			array[num++] = 2;
			array[num++] = (byte)(this._VersionMadeBy & 255);
			array[num++] = (byte)(((int)this._VersionMadeBy & 65280) >> 8);
			short num2 = ((this.VersionNeeded != 0) ? this.VersionNeeded : 20);
			bool flag = this._OutputUsesZip64 == null;
			if (flag)
			{
				this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always);
			}
			short num3 = (this._OutputUsesZip64.Value ? 45 : num2);
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			bool value = this._OutputUsesZip64.Value;
			if (value)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num4 = (short)encodedFileNameBytes.Length;
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			this._presumeZip64 = this._OutputUsesZip64.Value;
			this._Extra = this.ConstructExtraField(true);
			short num5 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num5 & 255);
			array[num++] = (byte)(((int)num5 & 65280) >> 8);
			int num6 = ((this._CommentBytes == null) ? 0 : this._CommentBytes.Length);
			bool flag2 = num6 + num > array.Length;
			if (flag2)
			{
				num6 = array.Length - num;
			}
			array[num++] = (byte)(num6 & 255);
			array[num++] = (byte)((num6 & 65280) >> 8);
			bool flag3 = this._container.ZipFile != null && this._container.ZipFile.MaxOutputSegmentSize != 0;
			bool flag4 = flag3;
			if (flag4)
			{
				array[num++] = (byte)(this._diskNumber & 255U);
				array[num++] = (byte)((this._diskNumber & 65280U) >> 8);
			}
			else
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			array[num++] = (this._IsText ? 1 : 0);
			array[num++] = 0;
			array[num++] = (byte)(this._ExternalFileAttrs & 255);
			array[num++] = (byte)((this._ExternalFileAttrs & 65280) >> 8);
			array[num++] = (byte)((this._ExternalFileAttrs & 16711680) >> 16);
			array[num++] = (byte)(((long)this._ExternalFileAttrs & (long)((ulong)(-16777216))) >> 24);
			bool flag5 = this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1));
			if (flag5)
			{
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
			}
			else
			{
				array[num++] = (byte)(this._RelativeOffsetOfLocalHeader & 255L);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 65280L) >> 8);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 16711680L) >> 16);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & (long)((ulong)(-16777216))) >> 24);
			}
			Buffer.BlockCopy(encodedFileNameBytes, 0, array, num, (int)num4);
			num += (int)num4;
			bool flag6 = this._Extra != null;
			if (flag6)
			{
				byte[] extra = this._Extra;
				int num7 = 0;
				Buffer.BlockCopy(extra, num7, array, num, (int)num5);
				num += (int)num5;
			}
			bool flag7 = num6 != 0;
			if (flag7)
			{
				Buffer.BlockCopy(this._CommentBytes, 0, array, num, num6);
				num += num6;
			}
			s.Write(array, 0, num);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014620 File Offset: 0x00012820
		private byte[] ConstructExtraField(bool forCentralDirectory)
		{
			List<byte[]> list = new List<byte[]>();
			bool flag = this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && (!forCentralDirectory || this._entryRequiresZip64.Value));
			if (flag)
			{
				int num = 4 + (forCentralDirectory ? 28 : 16);
				byte[] array = new byte[num];
				int num2 = 0;
				bool flag2 = this._presumeZip64 || forCentralDirectory;
				if (flag2)
				{
					array[num2++] = 1;
					array[num2++] = 0;
				}
				else
				{
					array[num2++] = 153;
					array[num2++] = 153;
				}
				array[num2++] = (byte)(num - 4);
				array[num2++] = 0;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num2, 8);
				num2 += 8;
				if (forCentralDirectory)
				{
					Array.Copy(BitConverter.GetBytes(this._RelativeOffsetOfLocalHeader), 0, array, num2, 8);
					num2 += 8;
					Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
				}
				list.Add(array);
			}
			bool flag3 = this._ntfsTimesAreSet && this._emitNtfsTimes;
			if (flag3)
			{
				byte[] array = new byte[36];
				int num3 = 0;
				array[num3++] = 10;
				array[num3++] = 0;
				array[num3++] = 32;
				array[num3++] = 0;
				num3 += 4;
				array[num3++] = 1;
				array[num3++] = 0;
				array[num3++] = 24;
				array[num3++] = 0;
				long num4 = this._Mtime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				num4 = this._Atime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				num4 = this._Ctime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				list.Add(array);
			}
			bool flag4 = this._ntfsTimesAreSet && this._emitUnixTimes;
			if (flag4)
			{
				int num5 = 9;
				bool flag5 = !forCentralDirectory;
				if (flag5)
				{
					num5 += 8;
				}
				byte[] array = new byte[num5];
				int num6 = 0;
				array[num6++] = 85;
				array[num6++] = 84;
				array[num6++] = (byte)(num5 - 4);
				array[num6++] = 0;
				array[num6++] = 7;
				int num7 = (int)(this._Mtime - ZipEntry._unixEpoch).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
				num6 += 4;
				bool flag6 = !forCentralDirectory;
				if (flag6)
				{
					num7 = (int)(this._Atime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
					num6 += 4;
					num7 = (int)(this._Ctime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
					num6 += 4;
				}
				list.Add(array);
			}
			byte[] array2 = null;
			bool flag7 = list.Count > 0;
			if (flag7)
			{
				int num8 = 0;
				int num9 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					num8 += list[i].Length;
				}
				array2 = new byte[num8];
				for (int i = 0; i < list.Count; i++)
				{
					Array.Copy(list[i], 0, array2, num9, list[i].Length);
					num9 += list[i].Length;
				}
			}
			return array2;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014A14 File Offset: 0x00012C14
		private string NormalizeFileName()
		{
			string text = this.FileName.Replace("\\", "/");
			bool flag = this._TrimVolumeFromFullyQualifiedPaths && this.FileName.Length >= 3 && this.FileName[1] == ':' && text[2] == '/';
			string text2;
			if (flag)
			{
				text2 = text.Substring(3);
			}
			else
			{
				bool flag2 = this.FileName.Length >= 4 && text[0] == '/' && text[1] == '/';
				if (flag2)
				{
					int num = text.IndexOf('/', 2);
					bool flag3 = num == -1;
					if (flag3)
					{
						throw new ArgumentException("The path for that entry appears to be badly formatted");
					}
					text2 = text.Substring(num + 1);
				}
				else
				{
					bool flag4 = this.FileName.Length >= 3 && text[0] == '.' && text[1] == '/';
					if (flag4)
					{
						text2 = text.Substring(2);
					}
					else
					{
						text2 = text;
					}
				}
			}
			return text2;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014B28 File Offset: 0x00012D28
		private byte[] GetEncodedFileNameBytes()
		{
			string text = this.NormalizeFileName();
			ZipOption alternateEncodingUsage = this.AlternateEncodingUsage;
			byte[] array2;
			if (alternateEncodingUsage != ZipOption.Default)
			{
				if (alternateEncodingUsage != ZipOption.Always)
				{
					byte[] array = ZipEntry.ibm437.GetBytes(text);
					string @string = ZipEntry.ibm437.GetString(array, 0, array.Length);
					this._CommentBytes = null;
					bool flag = @string != text;
					if (flag)
					{
						array = this.AlternateEncoding.GetBytes(text);
						bool flag2 = this._Comment != null && this._Comment.Length != 0;
						if (flag2)
						{
							this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
						}
						this._actualEncoding = this.AlternateEncoding;
						array2 = array;
					}
					else
					{
						this._actualEncoding = ZipEntry.ibm437;
						bool flag3 = this._Comment == null || this._Comment.Length == 0;
						if (flag3)
						{
							array2 = array;
						}
						else
						{
							byte[] bytes = ZipEntry.ibm437.GetBytes(this._Comment);
							string string2 = ZipEntry.ibm437.GetString(bytes, 0, bytes.Length);
							bool flag4 = string2 != this.Comment;
							if (flag4)
							{
								array = this.AlternateEncoding.GetBytes(text);
								this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
								this._actualEncoding = this.AlternateEncoding;
								array2 = array;
							}
							else
							{
								this._CommentBytes = bytes;
								array2 = array;
							}
						}
					}
				}
				else
				{
					bool flag5 = this._Comment != null && this._Comment.Length != 0;
					if (flag5)
					{
						this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
					}
					this._actualEncoding = this.AlternateEncoding;
					array2 = this.AlternateEncoding.GetBytes(text);
				}
			}
			else
			{
				bool flag6 = this._Comment != null && this._Comment.Length != 0;
				if (flag6)
				{
					this._CommentBytes = ZipEntry.ibm437.GetBytes(this._Comment);
				}
				this._actualEncoding = ZipEntry.ibm437;
				array2 = ZipEntry.ibm437.GetBytes(text);
			}
			return array2;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00014D2C File Offset: 0x00012F2C
		private bool WantReadAgain()
		{
			bool flag = this._UncompressedSize < 16L;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this._CompressionMethod == 0;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = this.CompressionLevel == CompressionLevel.None;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = this._CompressedSize < this._UncompressedSize;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool flag6 = this._Source == ZipEntrySource.Stream && !this._sourceStream.CanSeek;
							if (flag6)
							{
								flag2 = false;
							}
							else
							{
								bool flag7 = this._zipCrypto_forWrite != null && this.CompressedSize - 12L <= this.UncompressedSize;
								flag2 = !flag7;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014DE0 File Offset: 0x00012FE0
		private void MaybeUnsetCompressionMethodForWriting(int cycle)
		{
			bool flag = cycle > 1;
			if (flag)
			{
				this._CompressionMethod = 0;
			}
			else
			{
				bool isDirectory = this.IsDirectory;
				if (isDirectory)
				{
					this._CompressionMethod = 0;
				}
				else
				{
					bool flag2 = this._Source == ZipEntrySource.ZipFile;
					if (!flag2)
					{
						bool flag3 = this._Source == ZipEntrySource.Stream;
						if (flag3)
						{
							bool flag4 = this._sourceStream != null && this._sourceStream.CanSeek;
							if (flag4)
							{
								long length = this._sourceStream.Length;
								bool flag5 = length == 0L;
								if (flag5)
								{
									this._CompressionMethod = 0;
									return;
								}
							}
						}
						else
						{
							bool flag6 = this._Source == ZipEntrySource.FileSystem && SharedUtilities.GetFileLength(this.LocalFileName) == 0L;
							if (flag6)
							{
								this._CompressionMethod = 0;
								return;
							}
						}
						bool flag7 = this.SetCompression != null;
						if (flag7)
						{
							this.CompressionLevel = this.SetCompression(this.LocalFileName, this._FileNameInArchive);
						}
						bool flag8 = this.CompressionLevel == CompressionLevel.None && this.CompressionMethod == CompressionMethod.Deflate;
						if (flag8)
						{
							this._CompressionMethod = 0;
						}
					}
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014EFC File Offset: 0x000130FC
		internal void WriteHeader(Stream s, int cycle)
		{
			CountingStream countingStream = s as CountingStream;
			this._future_ROLH = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
			int num = 0;
			byte[] array = new byte[30];
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 3;
			array[num++] = 4;
			this._presumeZip64 = this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && !s.CanSeek);
			short num2 = (this._presumeZip64 ? 45 : 20);
			array[num++] = (byte)(num2 & 255);
			array[num++] = (byte)(((int)num2 & 65280) >> 8);
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num3 = (short)encodedFileNameBytes.Length;
			bool flag = this._Encryption == EncryptionAlgorithm.None;
			if (flag)
			{
				this._BitField &= -2;
			}
			else
			{
				this._BitField |= 1;
			}
			bool flag2 = this._actualEncoding.CodePage == Encoding.UTF8.CodePage;
			if (flag2)
			{
				this._BitField |= 2048;
			}
			bool flag3 = this.IsDirectory || cycle == 99;
			if (flag3)
			{
				this._BitField &= -9;
				this._BitField &= -2;
				this.Encryption = EncryptionAlgorithm.None;
				this.Password = null;
			}
			else
			{
				bool flag4 = !s.CanSeek;
				if (flag4)
				{
					this._BitField |= 8;
				}
			}
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			bool flag5 = this.__FileDataPosition == -1L;
			if (flag5)
			{
				this._CompressedSize = 0L;
				this._crcCalculated = false;
			}
			this.MaybeUnsetCompressionMethodForWriting(cycle);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			bool flag6 = cycle == 99;
			if (flag6)
			{
				this.SetZip64Flags();
			}
			this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			bool presumeZip = this._presumeZip64;
			if (presumeZip)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			this._Extra = this.ConstructExtraField(false);
			short num4 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			byte[] array2 = new byte[num + (int)num3 + (int)num4];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			Buffer.BlockCopy(encodedFileNameBytes, 0, array2, num, encodedFileNameBytes.Length);
			num += encodedFileNameBytes.Length;
			bool flag7 = this._Extra != null;
			if (flag7)
			{
				Buffer.BlockCopy(this._Extra, 0, array2, num, this._Extra.Length);
				num += this._Extra.Length;
			}
			this._LengthOfHeader = num;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag8 = zipSegmentedStream != null;
			if (flag8)
			{
				zipSegmentedStream.ContiguousWrite = true;
				uint num5 = zipSegmentedStream.ComputeSegment(num);
				bool flag9 = num5 != zipSegmentedStream.CurrentSegment;
				if (flag9)
				{
					this._future_ROLH = 0L;
				}
				else
				{
					this._future_ROLH = zipSegmentedStream.Position;
				}
				this._diskNumber = num5;
			}
			bool flag10 = this._container.Zip64 == Zip64Option.Default && (uint)this._RelativeOffsetOfLocalHeader >= uint.MaxValue;
			if (flag10)
			{
				throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			s.Write(array2, 0, num);
			bool flag11 = zipSegmentedStream != null;
			if (flag11)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			this._EntryHeader = array2;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00015458 File Offset: 0x00013658
		private int FigureCrc32()
		{
			bool flag = !this._crcCalculated;
			if (flag)
			{
				Stream stream = null;
				bool flag2 = this._Source == ZipEntrySource.WriteDelegate;
				if (flag2)
				{
					CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
					this._WriteDelegate(this.FileName, crcCalculatorStream);
					this._Crc32 = crcCalculatorStream.Crc;
				}
				else
				{
					bool flag3 = this._Source == ZipEntrySource.ZipFile;
					if (!flag3)
					{
						bool flag4 = this._Source == ZipEntrySource.Stream;
						if (flag4)
						{
							this.PrepSourceStream();
							stream = this._sourceStream;
						}
						else
						{
							bool flag5 = this._Source == ZipEntrySource.JitStream;
							if (flag5)
							{
								bool flag6 = this._sourceStream == null;
								if (flag6)
								{
									this._sourceStream = this._OpenDelegate(this.FileName);
								}
								this.PrepSourceStream();
								stream = this._sourceStream;
							}
							else
							{
								bool flag7 = this._Source == ZipEntrySource.ZipOutputStream;
								if (!flag7)
								{
									stream = File.Open(this.LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
								}
							}
						}
						CRC32 crc = new CRC32();
						this._Crc32 = crc.GetCrc32(stream);
						bool flag8 = this._sourceStream == null;
						if (flag8)
						{
							stream.Dispose();
						}
					}
				}
				this._crcCalculated = true;
			}
			return this._Crc32;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001559C File Offset: 0x0001379C
		private void PrepSourceStream()
		{
			bool flag = this._sourceStream == null;
			if (flag)
			{
				throw new ZipException(string.Format("The input stream is null for entry '{0}'.", this.FileName));
			}
			bool flag2 = this._sourceStreamOriginalPosition != null;
			if (flag2)
			{
				this._sourceStream.Position = this._sourceStreamOriginalPosition.Value;
			}
			else
			{
				bool canSeek = this._sourceStream.CanSeek;
				if (canSeek)
				{
					this._sourceStreamOriginalPosition = new long?(this._sourceStream.Position);
				}
				else
				{
					bool flag3 = this.Encryption == EncryptionAlgorithm.PkzipWeak;
					if (flag3)
					{
						bool flag4 = this._Source != ZipEntrySource.ZipFile && (this._BitField & 8) != 8;
						if (flag4)
						{
							throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
						}
					}
				}
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001565C File Offset: 0x0001385C
		internal void CopyMetaData(ZipEntry source)
		{
			this.__FileDataPosition = source.__FileDataPosition;
			this.CompressionMethod = source.CompressionMethod;
			this._CompressionMethod_FromZipFile = source._CompressionMethod_FromZipFile;
			this._CompressedFileDataSize = source._CompressedFileDataSize;
			this._UncompressedSize = source._UncompressedSize;
			this._BitField = source._BitField;
			this._Source = source._Source;
			this._LastModified = source._LastModified;
			this._Mtime = source._Mtime;
			this._Atime = source._Atime;
			this._Ctime = source._Ctime;
			this._ntfsTimesAreSet = source._ntfsTimesAreSet;
			this._emitUnixTimes = source._emitUnixTimes;
			this._emitNtfsTimes = source._emitNtfsTimes;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00015714 File Offset: 0x00013914
		private void OnWriteBlock(long bytesXferred, long totalBytesToXfer)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSaveBlock(this, bytesXferred, totalBytesToXfer);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00015750 File Offset: 0x00013950
		private void _WriteEntryData(Stream s)
		{
			Stream stream = null;
			long num = -1L;
			try
			{
				num = s.Position;
			}
			catch (Exception)
			{
			}
			try
			{
				long num2 = this.SetInputAndFigureFileLength(ref stream);
				CountingStream countingStream = new CountingStream(s);
				bool flag = num2 != 0L;
				Stream stream2;
				Stream stream3;
				if (flag)
				{
					stream2 = this.MaybeApplyEncryption(countingStream);
					stream3 = this.MaybeApplyCompression(stream2, num2);
				}
				else
				{
					stream3 = (stream2 = countingStream);
				}
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream3, true);
				bool flag2 = this._Source == ZipEntrySource.WriteDelegate;
				if (flag2)
				{
					this._WriteDelegate(this.FileName, crcCalculatorStream);
				}
				else
				{
					byte[] array = new byte[this.BufferSize];
					int num3;
					while ((num3 = SharedUtilities.ReadWithRetry(stream, array, 0, array.Length, this.FileName)) != 0)
					{
						crcCalculatorStream.Write(array, 0, num3);
						this.OnWriteBlock(crcCalculatorStream.TotalBytesSlurped, num2);
						bool ioOperationCanceled = this._ioOperationCanceled;
						if (ioOperationCanceled)
						{
							break;
						}
					}
				}
				this.FinishOutputStream(s, countingStream, stream2, stream3, crcCalculatorStream);
			}
			finally
			{
				bool flag3 = this._Source == ZipEntrySource.JitStream;
				if (flag3)
				{
					bool flag4 = this._CloseDelegate != null;
					if (flag4)
					{
						this._CloseDelegate(this.FileName, stream);
					}
				}
				else
				{
					bool flag5 = stream is FileStream;
					if (flag5)
					{
						stream.Dispose();
					}
				}
			}
			bool ioOperationCanceled2 = this._ioOperationCanceled;
			if (!ioOperationCanceled2)
			{
				this.__FileDataPosition = num;
				this.PostProcessOutput(s);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000158D8 File Offset: 0x00013AD8
		private long SetInputAndFigureFileLength(ref Stream input)
		{
			long num = -1L;
			bool flag = this._Source == ZipEntrySource.Stream;
			if (flag)
			{
				this.PrepSourceStream();
				input = this._sourceStream;
				try
				{
					num = this._sourceStream.Length;
				}
				catch (NotSupportedException)
				{
				}
			}
			else
			{
				bool flag2 = this._Source == ZipEntrySource.ZipFile;
				if (flag2)
				{
					string text = ((this._Encryption_FromZipFile == EncryptionAlgorithm.None) ? null : (this._Password ?? this._container.Password));
					this._sourceStream = this.InternalOpenReader(text);
					this.PrepSourceStream();
					input = this._sourceStream;
					num = this._sourceStream.Length;
				}
				else
				{
					bool flag3 = this._Source == ZipEntrySource.JitStream;
					if (flag3)
					{
						bool flag4 = this._sourceStream == null;
						if (flag4)
						{
							this._sourceStream = this._OpenDelegate(this.FileName);
						}
						this.PrepSourceStream();
						input = this._sourceStream;
						try
						{
							num = this._sourceStream.Length;
						}
						catch (NotSupportedException)
						{
						}
					}
					else
					{
						bool flag5 = this._Source == ZipEntrySource.FileSystem;
						if (flag5)
						{
							FileShare fileShare = FileShare.ReadWrite;
							fileShare |= FileShare.Delete;
							input = File.Open(this.LocalFileName, FileMode.Open, FileAccess.Read, fileShare);
							num = input.Length;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00015A34 File Offset: 0x00013C34
		internal void FinishOutputStream(Stream s, CountingStream entryCounter, Stream encryptor, Stream compressor, CrcCalculatorStream output)
		{
			bool flag = output == null;
			if (!flag)
			{
				output.Close();
				bool flag2 = compressor is DeflateStream;
				if (flag2)
				{
					compressor.Close();
				}
				else
				{
					bool flag3 = compressor is ParallelDeflateOutputStream;
					if (flag3)
					{
						compressor.Close();
					}
				}
				encryptor.Flush();
				encryptor.Close();
				this._LengthOfTrailer = 0;
				this._UncompressedSize = output.TotalBytesSlurped;
				this._CompressedFileDataSize = entryCounter.BytesWritten;
				this._CompressedSize = this._CompressedFileDataSize;
				this._Crc32 = output.Crc;
				this.StoreRelativeOffset();
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015AD4 File Offset: 0x00013CD4
		internal void PostProcessOutput(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			bool flag = this._UncompressedSize == 0L && this._CompressedSize == 0L;
			if (flag)
			{
				bool flag2 = this._Source == ZipEntrySource.ZipOutputStream;
				if (flag2)
				{
					return;
				}
				bool flag3 = this._Password != null;
				if (flag3)
				{
					int num = 0;
					bool flag4 = this.Encryption == EncryptionAlgorithm.PkzipWeak;
					if (flag4)
					{
						num = 12;
					}
					bool flag5 = this._Source == ZipEntrySource.ZipOutputStream && !s.CanSeek;
					if (flag5)
					{
						throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
					}
					bool flag6 = this.Encryption > EncryptionAlgorithm.None;
					if (flag6)
					{
						s.Seek((long)(-1 * num), SeekOrigin.Current);
						s.SetLength(s.Position);
						bool flag7 = countingStream != null;
						if (flag7)
						{
							countingStream.Adjust((long)num);
						}
						this._LengthOfHeader -= num;
						this.__FileDataPosition -= (long)num;
					}
					this._Password = null;
					this._BitField &= -2;
					int num2 = 6;
					this._EntryHeader[num2++] = (byte)(this._BitField & 255);
					this._EntryHeader[num2++] = (byte)(((int)this._BitField & 65280) >> 8);
				}
				this.CompressionMethod = CompressionMethod.None;
				this.Encryption = EncryptionAlgorithm.None;
			}
			else
			{
				bool flag8 = this._zipCrypto_forWrite != null;
				if (flag8)
				{
					bool flag9 = this.Encryption == EncryptionAlgorithm.PkzipWeak;
					if (flag9)
					{
						this._CompressedSize += 12L;
					}
				}
			}
			int num3 = 8;
			this._EntryHeader[num3++] = (byte)(this._CompressionMethod & 255);
			this._EntryHeader[num3++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			num3 = 14;
			this._EntryHeader[num3++] = (byte)(this._Crc32 & 255);
			this._EntryHeader[num3++] = (byte)((this._Crc32 & 65280) >> 8);
			this._EntryHeader[num3++] = (byte)((this._Crc32 & 16711680) >> 16);
			this._EntryHeader[num3++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			this.SetZip64Flags();
			short num4 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
			short num5 = (short)((int)this._EntryHeader[28] + (int)this._EntryHeader[29] * 256);
			bool value = this._OutputUsesZip64.Value;
			if (value)
			{
				this._EntryHeader[4] = 45;
				this._EntryHeader[5] = 0;
				for (int i = 0; i < 8; i++)
				{
					this._EntryHeader[num3++] = byte.MaxValue;
				}
				num3 = (int)(30 + num4);
				this._EntryHeader[num3++] = 1;
				this._EntryHeader[num3++] = 0;
				num3 += 2;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, this._EntryHeader, num3, 8);
				num3 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, this._EntryHeader, num3, 8);
			}
			else
			{
				this._EntryHeader[4] = 20;
				this._EntryHeader[5] = 0;
				num3 = 18;
				this._EntryHeader[num3++] = (byte)(this._CompressedSize & 255L);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & 65280L) >> 8);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				this._EntryHeader[num3++] = (byte)(this._UncompressedSize & 255L);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				bool flag10 = num5 != 0;
				if (flag10)
				{
					num3 = (int)(30 + num4);
					short num6 = (short)((int)this._EntryHeader[num3 + 2] + (int)this._EntryHeader[num3 + 3] * 256);
					bool flag11 = num6 == 16;
					if (flag11)
					{
						this._EntryHeader[num3++] = 153;
						this._EntryHeader[num3++] = 153;
					}
				}
			}
			bool flag12 = (this._BitField & 8) != 8 || (this._Source == ZipEntrySource.ZipOutputStream && s.CanSeek);
			if (flag12)
			{
				ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
				bool flag13 = zipSegmentedStream != null && this._diskNumber != zipSegmentedStream.CurrentSegment;
				if (flag13)
				{
					using (Stream stream = ZipSegmentedStream.ForUpdate(this._container.ZipFile.Name, this._diskNumber))
					{
						stream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
						stream.Write(this._EntryHeader, 0, this._EntryHeader.Length);
					}
				}
				else
				{
					s.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
					s.Write(this._EntryHeader, 0, this._EntryHeader.Length);
					bool flag14 = countingStream != null;
					if (flag14)
					{
						countingStream.Adjust((long)this._EntryHeader.Length);
					}
					s.Seek(this._CompressedSize, SeekOrigin.Current);
				}
			}
			bool flag15 = (this._BitField & 8) == 8 && !this.IsDirectory;
			if (flag15)
			{
				byte[] array = new byte[16 + (this._OutputUsesZip64.Value ? 8 : 0)];
				num3 = 0;
				Array.Copy(BitConverter.GetBytes(134695760), 0, array, num3, 4);
				num3 += 4;
				Array.Copy(BitConverter.GetBytes(this._Crc32), 0, array, num3, 4);
				num3 += 4;
				bool value2 = this._OutputUsesZip64.Value;
				if (value2)
				{
					Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num3, 8);
					num3 += 8;
					Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num3, 8);
					num3 += 8;
				}
				else
				{
					array[num3++] = (byte)(this._CompressedSize & 255L);
					array[num3++] = (byte)((this._CompressedSize & 65280L) >> 8);
					array[num3++] = (byte)((this._CompressedSize & 16711680L) >> 16);
					array[num3++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
					array[num3++] = (byte)(this._UncompressedSize & 255L);
					array[num3++] = (byte)((this._UncompressedSize & 65280L) >> 8);
					array[num3++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
					array[num3++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				}
				s.Write(array, 0, array.Length);
				this._LengthOfTrailer += array.Length;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000161F4 File Offset: 0x000143F4
		private void SetZip64Flags()
		{
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			bool flag = this._container.Zip64 == Zip64Option.Default && this._entryRequiresZip64.Value;
			if (flag)
			{
				throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00016284 File Offset: 0x00014484
		internal void PrepOutputStream(Stream s, long streamLength, out CountingStream outputCounter, out Stream encryptor, out Stream compressor, out CrcCalculatorStream output)
		{
			outputCounter = new CountingStream(s);
			bool flag = streamLength != 0L;
			if (flag)
			{
				encryptor = this.MaybeApplyEncryption(outputCounter);
				compressor = this.MaybeApplyCompression(encryptor, streamLength);
			}
			else
			{
				Stream stream;
				compressor = (stream = outputCounter);
				encryptor = stream;
			}
			output = new CrcCalculatorStream(compressor, true);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000162D8 File Offset: 0x000144D8
		private Stream MaybeApplyCompression(Stream s, long streamLength)
		{
			bool flag = this._CompressionMethod == 8 && this.CompressionLevel > CompressionLevel.None;
			Stream stream;
			if (flag)
			{
				bool flag2 = this._container.ParallelDeflateThreshold == 0L || (streamLength > this._container.ParallelDeflateThreshold && this._container.ParallelDeflateThreshold > 0L);
				if (flag2)
				{
					bool flag3 = this._container.ParallelDeflater == null;
					if (flag3)
					{
						this._container.ParallelDeflater = new ParallelDeflateOutputStream(s, this.CompressionLevel, this._container.Strategy, true);
						bool flag4 = this._container.CodecBufferSize > 0;
						if (flag4)
						{
							this._container.ParallelDeflater.BufferSize = this._container.CodecBufferSize;
						}
						bool flag5 = this._container.ParallelDeflateMaxBufferPairs > 0;
						if (flag5)
						{
							this._container.ParallelDeflater.MaxBufferPairs = this._container.ParallelDeflateMaxBufferPairs;
						}
					}
					ParallelDeflateOutputStream parallelDeflater = this._container.ParallelDeflater;
					parallelDeflater.Reset(s);
					stream = parallelDeflater;
				}
				else
				{
					DeflateStream deflateStream = new DeflateStream(s, CompressionMode.Compress, this.CompressionLevel, true);
					bool flag6 = this._container.CodecBufferSize > 0;
					if (flag6)
					{
						deflateStream.BufferSize = this._container.CodecBufferSize;
					}
					deflateStream.Strategy = this._container.Strategy;
					stream = deflateStream;
				}
			}
			else
			{
				stream = s;
			}
			return stream;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00016444 File Offset: 0x00014644
		private Stream MaybeApplyEncryption(Stream s)
		{
			bool flag = this.Encryption == EncryptionAlgorithm.PkzipWeak;
			Stream stream;
			if (flag)
			{
				stream = new ZipCipherStream(s, this._zipCrypto_forWrite, CryptoMode.Encrypt);
			}
			else
			{
				stream = s;
			}
			return stream;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00016478 File Offset: 0x00014678
		private void OnZipErrorWhileSaving(Exception e)
		{
			bool flag = this._container.ZipFile != null;
			if (flag)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnZipErrorSaving(this, e);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000164B0 File Offset: 0x000146B0
		internal void Write(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = false;
			do
			{
				try
				{
					bool flag2 = this._Source == ZipEntrySource.ZipFile && !this._restreamRequiredOnSave;
					if (flag2)
					{
						this.CopyThroughOneEntry(s);
						break;
					}
					bool isDirectory = this.IsDirectory;
					if (isDirectory)
					{
						this.WriteHeader(s, 1);
						this.StoreRelativeOffset();
						this._entryRequiresZip64 = new bool?(this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
						this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
						bool flag3 = zipSegmentedStream != null;
						if (flag3)
						{
							this._diskNumber = zipSegmentedStream.CurrentSegment;
						}
						break;
					}
					int num = 0;
					bool flag5;
					do
					{
						num++;
						this.WriteHeader(s, num);
						this.WriteSecurityMetadata(s);
						this._WriteEntryData(s);
						this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
						bool flag4 = num > 1;
						if (flag4)
						{
							flag5 = false;
						}
						else
						{
							bool flag6 = !s.CanSeek;
							flag5 = !flag6 && this.WantReadAgain();
						}
						bool flag7 = flag5;
						if (flag7)
						{
							bool flag8 = zipSegmentedStream != null;
							if (flag8)
							{
								zipSegmentedStream.TruncateBackward(this._diskNumber, this._RelativeOffsetOfLocalHeader);
							}
							else
							{
								s.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
							}
							s.SetLength(s.Position);
							bool flag9 = countingStream != null;
							if (flag9)
							{
								countingStream.Adjust(this._TotalEntrySize);
							}
						}
					}
					while (flag5);
					this._skippedDuringSave = false;
					flag = true;
				}
				catch (Exception ex)
				{
					ZipErrorAction zipErrorAction = this.ZipErrorAction;
					int num2 = 0;
					for (;;)
					{
						bool flag10 = this.ZipErrorAction == ZipErrorAction.Throw;
						if (flag10)
						{
							break;
						}
						bool flag11 = this.ZipErrorAction == ZipErrorAction.Skip || this.ZipErrorAction == ZipErrorAction.Retry;
						if (flag11)
						{
							goto Block_17;
						}
						bool flag12 = num2 > 0;
						if (flag12)
						{
							goto Block_22;
						}
						bool flag13 = this.ZipErrorAction == ZipErrorAction.InvokeErrorEvent;
						if (flag13)
						{
							this.OnZipErrorWhileSaving(ex);
							bool ioOperationCanceled = this._ioOperationCanceled;
							if (ioOperationCanceled)
							{
								goto Block_24;
							}
						}
						num2++;
					}
					throw;
					Block_17:
					long num3 = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
					long num4 = num3 - this._future_ROLH;
					bool flag14 = num4 > 0L;
					if (flag14)
					{
						s.Seek(num4, SeekOrigin.Current);
						long position = s.Position;
						s.SetLength(s.Position);
						bool flag15 = countingStream != null;
						if (flag15)
						{
							countingStream.Adjust(num3 - position);
						}
					}
					bool flag16 = this.ZipErrorAction == ZipErrorAction.Skip;
					if (flag16)
					{
						this.WriteStatus("Skipping file {0} (exception: {1})", new object[]
						{
							this.LocalFileName,
							ex.ToString()
						});
						this._skippedDuringSave = true;
						flag = true;
					}
					else
					{
						this.ZipErrorAction = zipErrorAction;
					}
					goto IL_02C7;
					Block_22:
					throw;
					Block_24:
					flag = true;
					IL_02C7:;
				}
			}
			while (!flag);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000167B4 File Offset: 0x000149B4
		internal void StoreRelativeOffset()
		{
			this._RelativeOffsetOfLocalHeader = this._future_ROLH;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000167C3 File Offset: 0x000149C3
		internal void NotifySaveComplete()
		{
			this._Encryption_FromZipFile = this._Encryption;
			this._CompressionMethod_FromZipFile = this._CompressionMethod;
			this._restreamRequiredOnSave = false;
			this._metadataChanged = false;
			this._Source = ZipEntrySource.ZipFile;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000167F4 File Offset: 0x000149F4
		internal void WriteSecurityMetadata(Stream outstream)
		{
			bool flag = this.Encryption == EncryptionAlgorithm.None;
			if (!flag)
			{
				string text = this._Password;
				bool flag2 = this._Source == ZipEntrySource.ZipFile && text == null;
				if (flag2)
				{
					text = this._container.Password;
				}
				bool flag3 = text == null;
				if (flag3)
				{
					this._zipCrypto_forWrite = null;
				}
				else
				{
					bool flag4 = this.Encryption == EncryptionAlgorithm.PkzipWeak;
					if (flag4)
					{
						this._zipCrypto_forWrite = ZipCrypto.ForWrite(text);
						Random random = new Random();
						byte[] array = new byte[12];
						random.NextBytes(array);
						bool flag5 = (this._BitField & 8) == 8;
						if (flag5)
						{
							this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
							array[11] = (byte)((this._TimeBlob >> 8) & 255);
						}
						else
						{
							this.FigureCrc32();
							array[11] = (byte)((this._Crc32 >> 24) & 255);
						}
						byte[] array2 = this._zipCrypto_forWrite.EncryptMessage(array, array.Length);
						outstream.Write(array2, 0, array2.Length);
						this._LengthOfHeader += array2.Length;
					}
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00016914 File Offset: 0x00014B14
		private void CopyThroughOneEntry(Stream outStream)
		{
			bool flag = this.LengthOfHeader == 0;
			if (flag)
			{
				throw new BadStateException("Bad header length.");
			}
			bool flag2 = this._metadataChanged || this.ArchiveStream is ZipSegmentedStream || outStream is ZipSegmentedStream || (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default) || (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always);
			bool flag3 = flag2;
			if (flag3)
			{
				this.CopyThroughWithRecompute(outStream);
			}
			else
			{
				this.CopyThroughWithNoChange(outStream);
			}
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000169F8 File Offset: 0x00014BF8
		private void CopyThroughWithRecompute(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			long relativeOffsetOfLocalHeader = this._RelativeOffsetOfLocalHeader;
			int lengthOfHeader = this.LengthOfHeader;
			this.WriteHeader(outstream, 0);
			this.StoreRelativeOffset();
			bool flag = !this.FileName.EndsWith("/");
			if (flag)
			{
				long num = relativeOffsetOfLocalHeader + (long)lengthOfHeader;
				int num2 = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
				num -= (long)num2;
				this._LengthOfHeader += num2;
				countingStream.Seek(num, SeekOrigin.Begin);
				long num3 = this._CompressedSize;
				while (num3 > 0L)
				{
					num2 = ((num3 > (long)array.Length) ? array.Length : ((int)num3));
					int num4 = countingStream.Read(array, 0, num2);
					outstream.Write(array, 0, num4);
					num3 -= (long)num4;
					this.OnWriteBlock(countingStream.BytesRead, this._CompressedSize);
					bool ioOperationCanceled = this._ioOperationCanceled;
					if (ioOperationCanceled)
					{
						break;
					}
				}
				bool flag2 = (this._BitField & 8) == 8;
				if (flag2)
				{
					int num5 = 16;
					bool inputUsesZip = this._InputUsesZip64;
					if (inputUsesZip)
					{
						num5 += 8;
					}
					byte[] array2 = new byte[num5];
					countingStream.Read(array2, 0, num5);
					bool flag3 = this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default;
					if (flag3)
					{
						outstream.Write(array2, 0, 8);
						bool flag4 = this._CompressedSize > (long)((ulong)(-1));
						if (flag4)
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 8, 4);
						bool flag5 = this._UncompressedSize > (long)((ulong)(-1));
						if (flag5)
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 16, 4);
						this._LengthOfTrailer -= 8;
					}
					else
					{
						bool flag6 = !this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always;
						if (flag6)
						{
							byte[] array3 = new byte[4];
							outstream.Write(array2, 0, 8);
							outstream.Write(array2, 8, 4);
							outstream.Write(array3, 0, 4);
							outstream.Write(array2, 12, 4);
							outstream.Write(array3, 0, 4);
							this._LengthOfTrailer += 8;
						}
						else
						{
							outstream.Write(array2, 0, num5);
						}
					}
				}
			}
			this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00016C5C File Offset: 0x00014E5C
		private void CopyThroughWithNoChange(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			countingStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			bool flag = this._TotalEntrySize == 0L;
			if (flag)
			{
				this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
			}
			CountingStream countingStream2 = outstream as CountingStream;
			this._RelativeOffsetOfLocalHeader = ((countingStream2 != null) ? countingStream2.ComputedPosition : outstream.Position);
			long num = this._TotalEntrySize;
			while (num > 0L)
			{
				int num2 = ((num > (long)array.Length) ? array.Length : ((int)num));
				int num3 = countingStream.Read(array, 0, num2);
				outstream.Write(array, 0, num3);
				num -= (long)num3;
				this.OnWriteBlock(countingStream.BytesRead, this._TotalEntrySize);
				bool ioOperationCanceled = this._ioOperationCanceled;
				if (ioOperationCanceled)
				{
					break;
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00016D44 File Offset: 0x00014F44
		[Conditional("Trace")]
		private void TraceWriteLine(string format, params object[] varParams)
		{
			object outputLock = this._outputLock;
			lock (outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
				Console.Write("{0:000} ZipEntry.Write ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}

		// Token: 0x040001C5 RID: 453
		private short _VersionMadeBy;

		// Token: 0x040001C6 RID: 454
		private short _InternalFileAttrs;

		// Token: 0x040001C7 RID: 455
		private int _ExternalFileAttrs;

		// Token: 0x040001C8 RID: 456
		private short _filenameLength;

		// Token: 0x040001C9 RID: 457
		private short _extraFieldLength;

		// Token: 0x040001CA RID: 458
		private short _commentLength;

		// Token: 0x040001D1 RID: 465
		private ZipCrypto _zipCrypto_forExtract;

		// Token: 0x040001D2 RID: 466
		private ZipCrypto _zipCrypto_forWrite;

		// Token: 0x040001D3 RID: 467
		internal DateTime _LastModified;

		// Token: 0x040001D4 RID: 468
		private DateTime _Mtime;

		// Token: 0x040001D5 RID: 469
		private DateTime _Atime;

		// Token: 0x040001D6 RID: 470
		private DateTime _Ctime;

		// Token: 0x040001D7 RID: 471
		private bool _ntfsTimesAreSet;

		// Token: 0x040001D8 RID: 472
		private bool _emitNtfsTimes = true;

		// Token: 0x040001D9 RID: 473
		private bool _emitUnixTimes;

		// Token: 0x040001DA RID: 474
		private bool _TrimVolumeFromFullyQualifiedPaths = true;

		// Token: 0x040001DB RID: 475
		internal string _LocalFileName;

		// Token: 0x040001DC RID: 476
		private string _FileNameInArchive;

		// Token: 0x040001DD RID: 477
		internal short _VersionNeeded;

		// Token: 0x040001DE RID: 478
		internal short _BitField;

		// Token: 0x040001DF RID: 479
		internal short _CompressionMethod;

		// Token: 0x040001E0 RID: 480
		private short _CompressionMethod_FromZipFile;

		// Token: 0x040001E1 RID: 481
		private CompressionLevel _CompressionLevel;

		// Token: 0x040001E2 RID: 482
		internal string _Comment;

		// Token: 0x040001E3 RID: 483
		private bool _IsDirectory;

		// Token: 0x040001E4 RID: 484
		private byte[] _CommentBytes;

		// Token: 0x040001E5 RID: 485
		internal long _CompressedSize;

		// Token: 0x040001E6 RID: 486
		internal long _CompressedFileDataSize;

		// Token: 0x040001E7 RID: 487
		internal long _UncompressedSize;

		// Token: 0x040001E8 RID: 488
		internal int _TimeBlob;

		// Token: 0x040001E9 RID: 489
		private bool _crcCalculated;

		// Token: 0x040001EA RID: 490
		internal int _Crc32;

		// Token: 0x040001EB RID: 491
		internal byte[] _Extra;

		// Token: 0x040001EC RID: 492
		private bool _metadataChanged;

		// Token: 0x040001ED RID: 493
		private bool _restreamRequiredOnSave;

		// Token: 0x040001EE RID: 494
		private bool _sourceIsEncrypted;

		// Token: 0x040001EF RID: 495
		private bool _skippedDuringSave;

		// Token: 0x040001F0 RID: 496
		private uint _diskNumber;

		// Token: 0x040001F1 RID: 497
		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		// Token: 0x040001F2 RID: 498
		private Encoding _actualEncoding;

		// Token: 0x040001F3 RID: 499
		internal ZipContainer _container;

		// Token: 0x040001F4 RID: 500
		private long __FileDataPosition = -1L;

		// Token: 0x040001F5 RID: 501
		private byte[] _EntryHeader;

		// Token: 0x040001F6 RID: 502
		internal long _RelativeOffsetOfLocalHeader;

		// Token: 0x040001F7 RID: 503
		private long _future_ROLH;

		// Token: 0x040001F8 RID: 504
		private long _TotalEntrySize;

		// Token: 0x040001F9 RID: 505
		private int _LengthOfHeader;

		// Token: 0x040001FA RID: 506
		private int _LengthOfTrailer;

		// Token: 0x040001FB RID: 507
		internal bool _InputUsesZip64;

		// Token: 0x040001FC RID: 508
		private uint _UnsupportedAlgorithmId;

		// Token: 0x040001FD RID: 509
		internal string _Password;

		// Token: 0x040001FE RID: 510
		internal ZipEntrySource _Source;

		// Token: 0x040001FF RID: 511
		internal EncryptionAlgorithm _Encryption;

		// Token: 0x04000200 RID: 512
		internal EncryptionAlgorithm _Encryption_FromZipFile;

		// Token: 0x04000201 RID: 513
		internal byte[] _WeakEncryptionHeader;

		// Token: 0x04000202 RID: 514
		internal Stream _archiveStream;

		// Token: 0x04000203 RID: 515
		private Stream _sourceStream;

		// Token: 0x04000204 RID: 516
		private long? _sourceStreamOriginalPosition;

		// Token: 0x04000205 RID: 517
		private bool _sourceWasJitProvided;

		// Token: 0x04000206 RID: 518
		private bool _ioOperationCanceled;

		// Token: 0x04000207 RID: 519
		private bool _presumeZip64;

		// Token: 0x04000208 RID: 520
		private bool? _entryRequiresZip64;

		// Token: 0x04000209 RID: 521
		private bool? _OutputUsesZip64;

		// Token: 0x0400020A RID: 522
		private bool _IsText;

		// Token: 0x0400020B RID: 523
		private ZipEntryTimestamp _timestamp;

		// Token: 0x0400020C RID: 524
		private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400020D RID: 525
		private static DateTime _win32Epoch = DateTime.FromFileTimeUtc(0L);

		// Token: 0x0400020E RID: 526
		private static DateTime _zeroHour = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400020F RID: 527
		private WriteDelegate _WriteDelegate;

		// Token: 0x04000210 RID: 528
		private OpenDelegate _OpenDelegate;

		// Token: 0x04000211 RID: 529
		private CloseDelegate _CloseDelegate;

		// Token: 0x04000212 RID: 530
		private Stream _inputDecryptorStream;

		// Token: 0x04000213 RID: 531
		private int _readExtraDepth;

		// Token: 0x04000214 RID: 532
		private object _outputLock = new object();

		// Token: 0x02000200 RID: 512
		private class CopyHelper
		{
			// Token: 0x06001BF2 RID: 7154 RVA: 0x000B1728 File Offset: 0x000AF928
			internal static string AppendCopyToFileName(string f)
			{
				ZipEntry.CopyHelper.callCount++;
				bool flag = ZipEntry.CopyHelper.callCount > 25;
				if (flag)
				{
					throw new OverflowException("overflow while creating filename");
				}
				int num = 1;
				int num2 = f.LastIndexOf(".");
				bool flag2 = num2 == -1;
				if (flag2)
				{
					Match match = ZipEntry.CopyHelper.re.Match(f);
					bool success = match.Success;
					if (success)
					{
						num = int.Parse(match.Groups[1].Value) + 1;
						string text = string.Format(" (copy {0})", num);
						f = f.Substring(0, match.Index) + text;
					}
					else
					{
						string text2 = string.Format(" (copy {0})", num);
						f += text2;
					}
				}
				else
				{
					Match match2 = ZipEntry.CopyHelper.re.Match(f.Substring(0, num2));
					bool success2 = match2.Success;
					if (success2)
					{
						num = int.Parse(match2.Groups[1].Value) + 1;
						string text3 = string.Format(" (copy {0})", num);
						f = f.Substring(0, match2.Index) + text3 + f.Substring(num2);
					}
					else
					{
						string text4 = string.Format(" (copy {0})", num);
						f = f.Substring(0, num2) + text4 + f.Substring(num2);
					}
				}
				return f;
			}

			// Token: 0x04000D90 RID: 3472
			private static Regex re = new Regex(" \\(copy (\\d+)\\)$");

			// Token: 0x04000D91 RID: 3473
			private static int callCount = 0;
		}

		// Token: 0x02000201 RID: 513
		// (Invoke) Token: 0x06001BF6 RID: 7158
		private delegate T Func<T>();
	}
}
