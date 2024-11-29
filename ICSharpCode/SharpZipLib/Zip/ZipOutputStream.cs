using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000080 RID: 128
	public class ZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00027C44 File Offset: 0x00025E44
		public ZipOutputStream(Stream baseOutputStream)
			: base(baseOutputStream, new Deflater(-1, true))
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00027CA8 File Offset: 0x00025EA8
		public ZipOutputStream(Stream baseOutputStream, int bufferSize)
			: base(baseOutputStream, new Deflater(-1, true), bufferSize)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00027D10 File Offset: 0x00025F10
		public bool IsFinished
		{
			get
			{
				return this.entries == null;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00027D2C File Offset: 0x00025F2C
		public void SetComment(string comment)
		{
			byte[] array = ZipConstants.ConvertToArray(comment);
			bool flag = array.Length > 65535;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("comment");
			}
			this.zipComment = array;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00027D62 File Offset: 0x00025F62
		public void SetLevel(int level)
		{
			this.deflater_.SetLevel(level);
			this.defaultCompressionLevel = level;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00027D7C File Offset: 0x00025F7C
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00027D9C File Offset: 0x00025F9C
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00027DB4 File Offset: 0x00025FB4
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

		// Token: 0x0600060C RID: 1548 RVA: 0x00027DBE File Offset: 0x00025FBE
		private void WriteLeShort(int value)
		{
			this.baseOutputStream_.WriteByte((byte)(value & 255));
			this.baseOutputStream_.WriteByte((byte)((value >> 8) & 255));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00027DED File Offset: 0x00025FED
		private void WriteLeInt(int value)
		{
			this.WriteLeShort(value);
			this.WriteLeShort(value >> 16);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00027E05 File Offset: 0x00026005
		private void WriteLeLong(long value)
		{
			this.WriteLeInt((int)value);
			this.WriteLeInt((int)(value >> 32));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00027E20 File Offset: 0x00026020
		public void PutNextEntry(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			bool flag2 = this.entries == null;
			if (flag2)
			{
				throw new InvalidOperationException("ZipOutputStream was finished");
			}
			bool flag3 = this.curEntry != null;
			if (flag3)
			{
				this.CloseEntry();
			}
			bool flag4 = this.entries.Count == int.MaxValue;
			if (flag4)
			{
				throw new ZipException("Too many entries for Zip file");
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			int num = this.defaultCompressionLevel;
			entry.Flags &= 2048;
			this.patchEntryHeader = false;
			bool flag5 = entry.Size == 0L;
			bool flag6;
			if (flag5)
			{
				entry.CompressedSize = entry.Size;
				entry.Crc = 0L;
				compressionMethod = CompressionMethod.Stored;
				flag6 = true;
			}
			else
			{
				flag6 = entry.Size >= 0L && entry.HasCrc;
				bool flag7 = compressionMethod == CompressionMethod.Stored;
				if (flag7)
				{
					bool flag8 = !flag6;
					if (flag8)
					{
						bool flag9 = !base.CanPatchEntries;
						if (flag9)
						{
							compressionMethod = CompressionMethod.Deflated;
							num = 0;
						}
					}
					else
					{
						entry.CompressedSize = entry.Size;
						flag6 = entry.HasCrc;
					}
				}
			}
			bool flag10 = !flag6;
			if (flag10)
			{
				bool flag11 = !base.CanPatchEntries;
				if (flag11)
				{
					entry.Flags |= 8;
				}
				else
				{
					this.patchEntryHeader = true;
				}
			}
			bool flag12 = base.Password != null;
			if (flag12)
			{
				entry.IsCrypted = true;
				bool flag13 = entry.Crc < 0L;
				if (flag13)
				{
					entry.Flags |= 8;
				}
			}
			entry.Offset = this.offset;
			entry.CompressionMethod = compressionMethod;
			this.curMethod = compressionMethod;
			this.sizePatchPos = -1L;
			bool flag14 = this.useZip64_ == UseZip64.On || (entry.Size < 0L && this.useZip64_ == UseZip64.Dynamic);
			if (flag14)
			{
				entry.ForceZip64();
			}
			this.WriteLeInt(67324752);
			this.WriteLeShort(entry.Version);
			this.WriteLeShort(entry.Flags);
			this.WriteLeShort((int)((byte)entry.CompressionMethodForHeader));
			this.WriteLeInt((int)entry.DosTime);
			bool flag15 = flag6;
			if (flag15)
			{
				this.WriteLeInt((int)entry.Crc);
				bool localHeaderRequiresZip = entry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip)
				{
					this.WriteLeInt(-1);
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt(entry.IsCrypted ? ((int)entry.CompressedSize + 12) : ((int)entry.CompressedSize));
					this.WriteLeInt((int)entry.Size);
				}
			}
			else
			{
				bool flag16 = this.patchEntryHeader;
				if (flag16)
				{
					this.crcPatchPos = this.baseOutputStream_.Position;
				}
				this.WriteLeInt(0);
				bool flag17 = this.patchEntryHeader;
				if (flag17)
				{
					this.sizePatchPos = this.baseOutputStream_.Position;
				}
				bool flag18 = entry.LocalHeaderRequiresZip64 || this.patchEntryHeader;
				if (flag18)
				{
					this.WriteLeInt(-1);
					this.WriteLeInt(-1);
				}
				else
				{
					this.WriteLeInt(0);
					this.WriteLeInt(0);
				}
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			bool flag19 = array.Length > 65535;
			if (flag19)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			bool localHeaderRequiresZip2 = entry.LocalHeaderRequiresZip64;
			if (localHeaderRequiresZip2)
			{
				zipExtraData.StartNewEntry();
				bool flag20 = flag6;
				if (flag20)
				{
					zipExtraData.AddLeLong(entry.Size);
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				else
				{
					zipExtraData.AddLeLong(-1L);
					zipExtraData.AddLeLong(-1L);
				}
				zipExtraData.AddNewEntry(1);
				bool flag21 = !zipExtraData.Find(1);
				if (flag21)
				{
					throw new ZipException("Internal error cant find extra data");
				}
				bool flag22 = this.patchEntryHeader;
				if (flag22)
				{
					this.sizePatchPos = (long)zipExtraData.CurrentReadIndex;
				}
			}
			else
			{
				zipExtraData.Delete(1);
			}
			bool flag23 = entry.AESKeySize > 0;
			if (flag23)
			{
				ZipOutputStream.AddExtraDataAES(entry, zipExtraData);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLeShort(array.Length);
			this.WriteLeShort(entryData.Length);
			bool flag24 = array.Length != 0;
			if (flag24)
			{
				this.baseOutputStream_.Write(array, 0, array.Length);
			}
			bool flag25 = entry.LocalHeaderRequiresZip64 && this.patchEntryHeader;
			if (flag25)
			{
				this.sizePatchPos += this.baseOutputStream_.Position;
			}
			bool flag26 = entryData.Length != 0;
			if (flag26)
			{
				this.baseOutputStream_.Write(entryData, 0, entryData.Length);
			}
			this.offset += (long)(30 + array.Length + entryData.Length);
			bool flag27 = entry.AESKeySize > 0;
			if (flag27)
			{
				this.offset += (long)entry.AESOverheadSize;
			}
			this.curEntry = entry;
			this.crc.Reset();
			bool flag28 = compressionMethod == CompressionMethod.Deflated;
			if (flag28)
			{
				this.deflater_.Reset();
				this.deflater_.SetLevel(num);
			}
			this.size = 0L;
			bool isCrypted = entry.IsCrypted;
			if (isCrypted)
			{
				bool flag29 = entry.AESKeySize > 0;
				if (flag29)
				{
					this.WriteAESHeader(entry);
				}
				else
				{
					bool flag30 = entry.Crc < 0L;
					if (flag30)
					{
						this.WriteEncryptionHeader(entry.DosTime << 16);
					}
					else
					{
						this.WriteEncryptionHeader(entry.Crc);
					}
				}
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000283AC File Offset: 0x000265AC
		public void CloseEntry()
		{
			bool flag = this.curEntry == null;
			if (flag)
			{
				throw new InvalidOperationException("No open entry");
			}
			long totalOut = this.size;
			bool flag2 = this.curMethod == CompressionMethod.Deflated;
			if (flag2)
			{
				bool flag3 = this.size >= 0L;
				if (flag3)
				{
					base.Finish();
					totalOut = this.deflater_.TotalOut;
				}
				else
				{
					this.deflater_.Reset();
				}
			}
			bool flag4 = this.curEntry.AESKeySize > 0;
			if (flag4)
			{
				this.baseOutputStream_.Write(this.AESAuthCode, 0, 10);
			}
			bool flag5 = this.curEntry.Size < 0L;
			if (flag5)
			{
				this.curEntry.Size = this.size;
			}
			else
			{
				bool flag6 = this.curEntry.Size != this.size;
				if (flag6)
				{
					throw new ZipException(string.Concat(new object[]
					{
						"size was ",
						this.size,
						", but I expected ",
						this.curEntry.Size
					}));
				}
			}
			bool flag7 = this.curEntry.CompressedSize < 0L;
			if (flag7)
			{
				this.curEntry.CompressedSize = totalOut;
			}
			else
			{
				bool flag8 = this.curEntry.CompressedSize != totalOut;
				if (flag8)
				{
					throw new ZipException(string.Concat(new object[]
					{
						"compressed size was ",
						totalOut,
						", but I expected ",
						this.curEntry.CompressedSize
					}));
				}
			}
			bool flag9 = this.curEntry.Crc < 0L;
			if (flag9)
			{
				this.curEntry.Crc = this.crc.Value;
			}
			else
			{
				bool flag10 = this.curEntry.Crc != this.crc.Value;
				if (flag10)
				{
					throw new ZipException(string.Concat(new object[]
					{
						"crc was ",
						this.crc.Value,
						", but I expected ",
						this.curEntry.Crc
					}));
				}
			}
			this.offset += totalOut;
			bool isCrypted = this.curEntry.IsCrypted;
			if (isCrypted)
			{
				bool flag11 = this.curEntry.AESKeySize > 0;
				if (flag11)
				{
					this.curEntry.CompressedSize += (long)this.curEntry.AESOverheadSize;
				}
				else
				{
					this.curEntry.CompressedSize += 12L;
				}
			}
			bool flag12 = this.patchEntryHeader;
			if (flag12)
			{
				this.patchEntryHeader = false;
				long position = this.baseOutputStream_.Position;
				this.baseOutputStream_.Seek(this.crcPatchPos, SeekOrigin.Begin);
				this.WriteLeInt((int)this.curEntry.Crc);
				bool localHeaderRequiresZip = this.curEntry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip)
				{
					bool flag13 = this.sizePatchPos == -1L;
					if (flag13)
					{
						throw new ZipException("Entry requires zip64 but this has been turned off");
					}
					this.baseOutputStream_.Seek(this.sizePatchPos, SeekOrigin.Begin);
					this.WriteLeLong(this.curEntry.Size);
					this.WriteLeLong(this.curEntry.CompressedSize);
				}
				else
				{
					this.WriteLeInt((int)this.curEntry.CompressedSize);
					this.WriteLeInt((int)this.curEntry.Size);
				}
				this.baseOutputStream_.Seek(position, SeekOrigin.Begin);
			}
			bool flag14 = (this.curEntry.Flags & 8) != 0;
			if (flag14)
			{
				this.WriteLeInt(134695760);
				this.WriteLeInt((int)this.curEntry.Crc);
				bool localHeaderRequiresZip2 = this.curEntry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip2)
				{
					this.WriteLeLong(this.curEntry.CompressedSize);
					this.WriteLeLong(this.curEntry.Size);
					this.offset += 24L;
				}
				else
				{
					this.WriteLeInt((int)this.curEntry.CompressedSize);
					this.WriteLeInt((int)this.curEntry.Size);
					this.offset += 16L;
				}
			}
			this.entries.Add(this.curEntry);
			this.curEntry = null;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00028818 File Offset: 0x00026A18
		private void WriteEncryptionHeader(long crcValue)
		{
			this.offset += 12L;
			base.InitializePassword(base.Password);
			byte[] array = new byte[12];
			Random random = new Random();
			random.NextBytes(array);
			array[11] = (byte)(crcValue >> 24);
			base.EncryptBlock(array, 0, array.Length);
			this.baseOutputStream_.Write(array, 0, array.Length);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00028880 File Offset: 0x00026A80
		private static void AddExtraDataAES(ZipEntry entry, ZipExtraData extraData)
		{
			extraData.StartNewEntry();
			extraData.AddLeShort(2);
			extraData.AddLeShort(17729);
			extraData.AddData(entry.AESEncryptionStrength);
			extraData.AddLeShort((int)entry.CompressionMethod);
			extraData.AddNewEntry(39169);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000288D0 File Offset: 0x00026AD0
		private void WriteAESHeader(ZipEntry entry)
		{
			byte[] array;
			byte[] array2;
			base.InitializeAESPassword(entry, base.Password, out array, out array2);
			this.baseOutputStream_.Write(array, 0, array.Length);
			this.baseOutputStream_.Write(array2, 0, array2.Length);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00028914 File Offset: 0x00026B14
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this.curEntry == null;
			if (flag)
			{
				throw new InvalidOperationException("No open entry.");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag3 = offset < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			bool flag5 = buffer.Length - offset < count;
			if (flag5)
			{
				throw new ArgumentException("Invalid offset/count combination");
			}
			this.crc.Update(buffer, offset, count);
			this.size += (long)count;
			CompressionMethod compressionMethod = this.curMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod == CompressionMethod.Deflated)
				{
					base.Write(buffer, offset, count);
				}
			}
			else
			{
				bool flag6 = base.Password != null;
				if (flag6)
				{
					this.CopyAndEncrypt(buffer, offset, count);
				}
				else
				{
					this.baseOutputStream_.Write(buffer, offset, count);
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00028A0C File Offset: 0x00026C0C
		private void CopyAndEncrypt(byte[] buffer, int offset, int count)
		{
			byte[] array = new byte[4096];
			while (count > 0)
			{
				int num = ((count < 4096) ? count : 4096);
				Array.Copy(buffer, offset, array, 0, num);
				base.EncryptBlock(array, 0, num);
				this.baseOutputStream_.Write(array, 0, num);
				count -= num;
				offset += num;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00028A70 File Offset: 0x00026C70
		public override void Finish()
		{
			bool flag = this.entries == null;
			if (!flag)
			{
				bool flag2 = this.curEntry != null;
				if (flag2)
				{
					this.CloseEntry();
				}
				long num = (long)this.entries.Count;
				long num2 = 0L;
				foreach (object obj in this.entries)
				{
					ZipEntry zipEntry = (ZipEntry)obj;
					this.WriteLeInt(33639248);
					this.WriteLeShort(51);
					this.WriteLeShort(zipEntry.Version);
					this.WriteLeShort(zipEntry.Flags);
					this.WriteLeShort((int)((short)zipEntry.CompressionMethodForHeader));
					this.WriteLeInt((int)zipEntry.DosTime);
					this.WriteLeInt((int)zipEntry.Crc);
					bool flag3 = zipEntry.IsZip64Forced() || zipEntry.CompressedSize >= (long)((ulong)(-1));
					if (flag3)
					{
						this.WriteLeInt(-1);
					}
					else
					{
						this.WriteLeInt((int)zipEntry.CompressedSize);
					}
					bool flag4 = zipEntry.IsZip64Forced() || zipEntry.Size >= (long)((ulong)(-1));
					if (flag4)
					{
						this.WriteLeInt(-1);
					}
					else
					{
						this.WriteLeInt((int)zipEntry.Size);
					}
					byte[] array = ZipConstants.ConvertToArray(zipEntry.Flags, zipEntry.Name);
					bool flag5 = array.Length > 65535;
					if (flag5)
					{
						throw new ZipException("Name too long.");
					}
					ZipExtraData zipExtraData = new ZipExtraData(zipEntry.ExtraData);
					bool centralHeaderRequiresZip = zipEntry.CentralHeaderRequiresZip64;
					if (centralHeaderRequiresZip)
					{
						zipExtraData.StartNewEntry();
						bool flag6 = zipEntry.IsZip64Forced() || zipEntry.Size >= (long)((ulong)(-1));
						if (flag6)
						{
							zipExtraData.AddLeLong(zipEntry.Size);
						}
						bool flag7 = zipEntry.IsZip64Forced() || zipEntry.CompressedSize >= (long)((ulong)(-1));
						if (flag7)
						{
							zipExtraData.AddLeLong(zipEntry.CompressedSize);
						}
						bool flag8 = zipEntry.Offset >= (long)((ulong)(-1));
						if (flag8)
						{
							zipExtraData.AddLeLong(zipEntry.Offset);
						}
						zipExtraData.AddNewEntry(1);
					}
					else
					{
						zipExtraData.Delete(1);
					}
					bool flag9 = zipEntry.AESKeySize > 0;
					if (flag9)
					{
						ZipOutputStream.AddExtraDataAES(zipEntry, zipExtraData);
					}
					byte[] entryData = zipExtraData.GetEntryData();
					byte[] array2 = ((zipEntry.Comment != null) ? ZipConstants.ConvertToArray(zipEntry.Flags, zipEntry.Comment) : new byte[0]);
					bool flag10 = array2.Length > 65535;
					if (flag10)
					{
						throw new ZipException("Comment too long.");
					}
					this.WriteLeShort(array.Length);
					this.WriteLeShort(entryData.Length);
					this.WriteLeShort(array2.Length);
					this.WriteLeShort(0);
					this.WriteLeShort(0);
					bool flag11 = zipEntry.ExternalFileAttributes != -1;
					if (flag11)
					{
						this.WriteLeInt(zipEntry.ExternalFileAttributes);
					}
					else
					{
						bool isDirectory = zipEntry.IsDirectory;
						if (isDirectory)
						{
							this.WriteLeInt(16);
						}
						else
						{
							this.WriteLeInt(0);
						}
					}
					bool flag12 = zipEntry.Offset >= (long)((ulong)(-1));
					if (flag12)
					{
						this.WriteLeInt(-1);
					}
					else
					{
						this.WriteLeInt((int)zipEntry.Offset);
					}
					bool flag13 = array.Length != 0;
					if (flag13)
					{
						this.baseOutputStream_.Write(array, 0, array.Length);
					}
					bool flag14 = entryData.Length != 0;
					if (flag14)
					{
						this.baseOutputStream_.Write(entryData, 0, entryData.Length);
					}
					bool flag15 = array2.Length != 0;
					if (flag15)
					{
						this.baseOutputStream_.Write(array2, 0, array2.Length);
					}
					num2 += (long)(46 + array.Length + entryData.Length + array2.Length);
				}
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseOutputStream_))
				{
					zipHelperStream.WriteEndOfCentralDirectory(num, num2, this.offset, this.zipComment);
				}
				this.entries = null;
			}
		}

		// Token: 0x040003B3 RID: 947
		private ArrayList entries = new ArrayList();

		// Token: 0x040003B4 RID: 948
		private Crc32 crc = new Crc32();

		// Token: 0x040003B5 RID: 949
		private ZipEntry curEntry;

		// Token: 0x040003B6 RID: 950
		private int defaultCompressionLevel = -1;

		// Token: 0x040003B7 RID: 951
		private CompressionMethod curMethod = CompressionMethod.Deflated;

		// Token: 0x040003B8 RID: 952
		private long size;

		// Token: 0x040003B9 RID: 953
		private long offset;

		// Token: 0x040003BA RID: 954
		private byte[] zipComment = new byte[0];

		// Token: 0x040003BB RID: 955
		private bool patchEntryHeader;

		// Token: 0x040003BC RID: 956
		private long crcPatchPos = -1L;

		// Token: 0x040003BD RID: 957
		private long sizePatchPos = -1L;

		// Token: 0x040003BE RID: 958
		private UseZip64 useZip64_ = UseZip64.Dynamic;
	}
}
