using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007D RID: 125
	internal class ZipHelperStream : Stream
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00026206 File Offset: 0x00024406
		public ZipHelperStream(string name)
		{
			this.stream_ = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
			this.isOwner_ = true;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00026225 File Offset: 0x00024425
		public ZipHelperStream(Stream stream)
		{
			this.stream_ = stream;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00026238 File Offset: 0x00024438
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x00026250 File Offset: 0x00024450
		public bool IsStreamOwner
		{
			get
			{
				return this.isOwner_;
			}
			set
			{
				this.isOwner_ = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0002625C File Offset: 0x0002445C
		public override bool CanRead
		{
			get
			{
				return this.stream_.CanRead;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0002627C File Offset: 0x0002447C
		public override bool CanSeek
		{
			get
			{
				return this.stream_.CanSeek;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0002629C File Offset: 0x0002449C
		public override bool CanTimeout
		{
			get
			{
				return this.stream_.CanTimeout;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000262BC File Offset: 0x000244BC
		public override long Length
		{
			get
			{
				return this.stream_.Length;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000262DC File Offset: 0x000244DC
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000262F9 File Offset: 0x000244F9
		public override long Position
		{
			get
			{
				return this.stream_.Position;
			}
			set
			{
				this.stream_.Position = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0002630C File Offset: 0x0002450C
		public override bool CanWrite
		{
			get
			{
				return this.stream_.CanWrite;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00026329 File Offset: 0x00024529
		public override void Flush()
		{
			this.stream_.Flush();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00026338 File Offset: 0x00024538
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream_.Seek(offset, origin);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00026357 File Offset: 0x00024557
		public override void SetLength(long value)
		{
			this.stream_.SetLength(value);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00026368 File Offset: 0x00024568
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream_.Read(buffer, offset, count);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00026388 File Offset: 0x00024588
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream_.Write(buffer, offset, count);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002639C File Offset: 0x0002459C
		public override void Close()
		{
			Stream stream = this.stream_;
			this.stream_ = null;
			bool flag = this.isOwner_ && stream != null;
			if (flag)
			{
				this.isOwner_ = false;
				stream.Close();
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000263DC File Offset: 0x000245DC
		private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
		{
			CompressionMethod compressionMethod = entry.CompressionMethod;
			bool flag = true;
			bool flag2 = false;
			this.WriteLEInt(67324752);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)compressionMethod));
			this.WriteLEInt((int)entry.DosTime);
			bool flag3 = flag;
			if (flag3)
			{
				this.WriteLEInt((int)entry.Crc);
				bool localHeaderRequiresZip = entry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip)
				{
					this.WriteLEInt(-1);
					this.WriteLEInt(-1);
				}
				else
				{
					this.WriteLEInt(entry.IsCrypted ? ((int)entry.CompressedSize + 12) : ((int)entry.CompressedSize));
					this.WriteLEInt((int)entry.Size);
				}
			}
			else
			{
				bool flag4 = patchData != null;
				if (flag4)
				{
					patchData.CrcPatchOffset = this.stream_.Position;
				}
				this.WriteLEInt(0);
				bool flag5 = patchData != null;
				if (flag5)
				{
					patchData.SizePatchOffset = this.stream_.Position;
				}
				bool flag6 = entry.LocalHeaderRequiresZip64 && flag2;
				if (flag6)
				{
					this.WriteLEInt(-1);
					this.WriteLEInt(-1);
				}
				else
				{
					this.WriteLEInt(0);
					this.WriteLEInt(0);
				}
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			bool flag7 = array.Length > 65535;
			if (flag7)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			bool flag8 = entry.LocalHeaderRequiresZip64 && (flag || flag2);
			if (flag8)
			{
				zipExtraData.StartNewEntry();
				bool flag9 = flag;
				if (flag9)
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
				bool flag10 = !zipExtraData.Find(1);
				if (flag10)
				{
					throw new ZipException("Internal error cant find extra data");
				}
				bool flag11 = patchData != null;
				if (flag11)
				{
					patchData.SizePatchOffset = (long)zipExtraData.CurrentReadIndex;
				}
			}
			else
			{
				zipExtraData.Delete(1);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLEShort(array.Length);
			this.WriteLEShort(entryData.Length);
			bool flag12 = array.Length != 0;
			if (flag12)
			{
				this.stream_.Write(array, 0, array.Length);
			}
			bool flag13 = entry.LocalHeaderRequiresZip64 && flag2;
			if (flag13)
			{
				patchData.SizePatchOffset += this.stream_.Position;
			}
			bool flag14 = entryData.Length != 0;
			if (flag14)
			{
				this.stream_.Write(entryData, 0, entryData.Length);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00026684 File Offset: 0x00024884
		public long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long num = endLocation - (long)minimumBlockSize;
			bool flag = num < 0L;
			long num2;
			if (flag)
			{
				num2 = -1L;
			}
			else
			{
				long num3 = Math.Max(num - (long)maximumVariableData, 0L);
				for (;;)
				{
					bool flag2 = num < num3;
					if (flag2)
					{
						break;
					}
					long num4 = num;
					num = num4 - 1L;
					this.Seek(num4, SeekOrigin.Begin);
					if (this.ReadLEInt() == signature)
					{
						goto Block_3;
					}
				}
				return -1L;
				Block_3:
				num2 = this.Position;
			}
			return num2;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000266F0 File Offset: 0x000248F0
		public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
		{
			long position = this.stream_.Position;
			this.WriteLEInt(101075792);
			this.WriteLELong(44L);
			this.WriteLEShort(51);
			this.WriteLEShort(45);
			this.WriteLEInt(0);
			this.WriteLEInt(0);
			this.WriteLELong(noOfEntries);
			this.WriteLELong(noOfEntries);
			this.WriteLELong(sizeEntries);
			this.WriteLELong(centralDirOffset);
			this.WriteLEInt(117853008);
			this.WriteLEInt(0);
			this.WriteLELong(position);
			this.WriteLEInt(1);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00026788 File Offset: 0x00024988
		public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
		{
			bool flag = noOfEntries >= 65535L || startOfCentralDirectory >= (long)((ulong)(-1)) || sizeEntries >= (long)((ulong)(-1));
			if (flag)
			{
				this.WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
			}
			this.WriteLEInt(101010256);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			bool flag2 = noOfEntries >= 65535L;
			if (flag2)
			{
				this.WriteLEUshort(ushort.MaxValue);
				this.WriteLEUshort(ushort.MaxValue);
			}
			else
			{
				this.WriteLEShort((int)((short)noOfEntries));
				this.WriteLEShort((int)((short)noOfEntries));
			}
			bool flag3 = sizeEntries >= (long)((ulong)(-1));
			if (flag3)
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEInt((int)sizeEntries);
			}
			bool flag4 = startOfCentralDirectory >= (long)((ulong)(-1));
			if (flag4)
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEInt((int)startOfCentralDirectory);
			}
			int num = ((comment != null) ? comment.Length : 0);
			bool flag5 = num > 65535;
			if (flag5)
			{
				throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", num));
			}
			this.WriteLEShort(num);
			bool flag6 = num > 0;
			if (flag6)
			{
				this.Write(comment, 0, comment.Length);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000268B8 File Offset: 0x00024AB8
		public int ReadLEShort()
		{
			int num = this.stream_.ReadByte();
			bool flag = num < 0;
			if (flag)
			{
				throw new EndOfStreamException();
			}
			int num2 = this.stream_.ReadByte();
			bool flag2 = num2 < 0;
			if (flag2)
			{
				throw new EndOfStreamException();
			}
			return num | (num2 << 8);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00026908 File Offset: 0x00024B08
		public int ReadLEInt()
		{
			return this.ReadLEShort() | (this.ReadLEShort() << 16);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0002692C File Offset: 0x00024B2C
		public long ReadLELong()
		{
			return (long)((ulong)this.ReadLEInt() | (ulong)((ulong)((long)this.ReadLEInt()) << 32));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00026950 File Offset: 0x00024B50
		public void WriteLEShort(int value)
		{
			this.stream_.WriteByte((byte)(value & 255));
			this.stream_.WriteByte((byte)((value >> 8) & 255));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002697D File Offset: 0x00024B7D
		public void WriteLEUshort(ushort value)
		{
			this.stream_.WriteByte((byte)(value & 255));
			this.stream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000269A4 File Offset: 0x00024BA4
		public void WriteLEInt(int value)
		{
			this.WriteLEShort(value);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000269BA File Offset: 0x00024BBA
		public void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000269D8 File Offset: 0x00024BD8
		public void WriteLELong(long value)
		{
			this.WriteLEInt((int)value);
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000269F0 File Offset: 0x00024BF0
		public void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)(-1)));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00026A0C File Offset: 0x00024C0C
		public int WriteDataDescriptor(ZipEntry entry)
		{
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			int num = 0;
			bool flag2 = (entry.Flags & 8) != 0;
			if (flag2)
			{
				this.WriteLEInt(134695760);
				this.WriteLEInt((int)entry.Crc);
				num += 8;
				bool localHeaderRequiresZip = entry.LocalHeaderRequiresZip64;
				if (localHeaderRequiresZip)
				{
					this.WriteLELong(entry.CompressedSize);
					this.WriteLELong(entry.Size);
					num += 16;
				}
				else
				{
					this.WriteLEInt((int)entry.CompressedSize);
					this.WriteLEInt((int)entry.Size);
					num += 8;
				}
			}
			return num;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00026AB8 File Offset: 0x00024CB8
		public void ReadDataDescriptor(bool zip64, DescriptorData data)
		{
			int num = this.ReadLEInt();
			bool flag = num != 134695760;
			if (flag)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			data.Crc = (long)this.ReadLEInt();
			if (zip64)
			{
				data.CompressedSize = this.ReadLELong();
				data.Size = this.ReadLELong();
			}
			else
			{
				data.CompressedSize = (long)this.ReadLEInt();
				data.Size = (long)this.ReadLEInt();
			}
		}

		// Token: 0x040003A7 RID: 935
		private bool isOwner_;

		// Token: 0x040003A8 RID: 936
		private Stream stream_;
	}
}
