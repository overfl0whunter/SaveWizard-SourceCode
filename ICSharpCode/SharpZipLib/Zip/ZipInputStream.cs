using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007E RID: 126
	public class ZipInputStream : InflaterInputStream
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00026B37 File Offset: 0x00024D37
		public ZipInputStream(Stream baseInputStream)
			: base(baseInputStream, new Inflater(true))
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00026B65 File Offset: 0x00024D65
		public ZipInputStream(Stream baseInputStream, int bufferSize)
			: base(baseInputStream, new Inflater(true), bufferSize)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00026B94 File Offset: 0x00024D94
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00026BAC File Offset: 0x00024DAC
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public bool CanDecompressEntry
		{
			get
			{
				return this.entry != null && this.entry.CanDecompress;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00026BE0 File Offset: 0x00024DE0
		public ZipEntry GetNextEntry()
		{
			bool flag = this.crc == null;
			if (flag)
			{
				throw new InvalidOperationException("Closed.");
			}
			bool flag2 = this.entry != null;
			if (flag2)
			{
				this.CloseEntry();
			}
			int num = this.inputBuffer.ReadLeInt();
			bool flag3 = num == 33639248 || num == 101010256 || num == 84233040 || num == 117853008 || num == 101075792;
			ZipEntry zipEntry;
			if (flag3)
			{
				this.Close();
				zipEntry = null;
			}
			else
			{
				bool flag4 = num == 808471376 || num == 134695760;
				if (flag4)
				{
					num = this.inputBuffer.ReadLeInt();
				}
				bool flag5 = num != 67324752;
				if (flag5)
				{
					throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num));
				}
				short num2 = (short)this.inputBuffer.ReadLeShort();
				this.flags = this.inputBuffer.ReadLeShort();
				this.method = this.inputBuffer.ReadLeShort();
				uint num3 = (uint)this.inputBuffer.ReadLeInt();
				int num4 = this.inputBuffer.ReadLeInt();
				this.csize = (long)this.inputBuffer.ReadLeInt();
				this.size = (long)this.inputBuffer.ReadLeInt();
				int num5 = this.inputBuffer.ReadLeShort();
				int num6 = this.inputBuffer.ReadLeShort();
				bool flag6 = (this.flags & 1) == 1;
				byte[] array = new byte[num5];
				this.inputBuffer.ReadRawBuffer(array);
				string text = ZipConstants.ConvertToStringExt(this.flags, array);
				this.entry = new ZipEntry(text, (int)num2);
				this.entry.Flags = this.flags;
				this.entry.CompressionMethod = (CompressionMethod)this.method;
				bool flag7 = (this.flags & 8) == 0;
				if (flag7)
				{
					this.entry.Crc = (long)num4 & (long)((ulong)(-1));
					this.entry.Size = this.size & (long)((ulong)(-1));
					this.entry.CompressedSize = this.csize & (long)((ulong)(-1));
					this.entry.CryptoCheckValue = (byte)((num4 >> 24) & 255);
				}
				else
				{
					bool flag8 = num4 != 0;
					if (flag8)
					{
						this.entry.Crc = (long)num4 & (long)((ulong)(-1));
					}
					bool flag9 = this.size != 0L;
					if (flag9)
					{
						this.entry.Size = this.size & (long)((ulong)(-1));
					}
					bool flag10 = this.csize != 0L;
					if (flag10)
					{
						this.entry.CompressedSize = this.csize & (long)((ulong)(-1));
					}
					this.entry.CryptoCheckValue = (byte)((num3 >> 8) & 255U);
				}
				this.entry.DosTime = (long)((ulong)num3);
				bool flag11 = num6 > 0;
				if (flag11)
				{
					byte[] array2 = new byte[num6];
					this.inputBuffer.ReadRawBuffer(array2);
					this.entry.ExtraData = array2;
				}
				this.entry.ProcessExtraData(true);
				bool flag12 = this.entry.CompressedSize >= 0L;
				if (flag12)
				{
					this.csize = this.entry.CompressedSize;
				}
				bool flag13 = this.entry.Size >= 0L;
				if (flag13)
				{
					this.size = this.entry.Size;
				}
				bool flag14 = this.method == 0 && ((!flag6 && this.csize != this.size) || (flag6 && this.csize - 12L != this.size));
				if (flag14)
				{
					throw new ZipException("Stored, but compressed != uncompressed");
				}
				bool flag15 = this.entry.IsCompressionMethodSupported();
				if (flag15)
				{
					this.internalReader = new ZipInputStream.ReadDataHandler(this.InitialRead);
				}
				else
				{
					this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotSupported);
				}
				zipEntry = this.entry;
			}
			return zipEntry;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00026FD8 File Offset: 0x000251D8
		private void ReadDataDescriptor()
		{
			bool flag = this.inputBuffer.ReadLeInt() != 134695760;
			if (flag)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			this.entry.Crc = (long)this.inputBuffer.ReadLeInt() & (long)((ulong)(-1));
			bool localHeaderRequiresZip = this.entry.LocalHeaderRequiresZip64;
			if (localHeaderRequiresZip)
			{
				this.csize = this.inputBuffer.ReadLeLong();
				this.size = this.inputBuffer.ReadLeLong();
			}
			else
			{
				this.csize = (long)this.inputBuffer.ReadLeInt();
				this.size = (long)this.inputBuffer.ReadLeInt();
			}
			this.entry.CompressedSize = this.csize;
			this.entry.Size = this.size;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000270A8 File Offset: 0x000252A8
		private void CompleteCloseEntry(bool testCrc)
		{
			base.StopDecrypting();
			bool flag = (this.flags & 8) != 0;
			if (flag)
			{
				this.ReadDataDescriptor();
			}
			this.size = 0L;
			bool flag2 = testCrc && (this.crc.Value & (long)((ulong)(-1))) != this.entry.Crc && this.entry.Crc != -1L;
			if (flag2)
			{
				throw new ZipException("CRC mismatch");
			}
			this.crc.Reset();
			bool flag3 = this.method == 8;
			if (flag3)
			{
				this.inf.Reset();
			}
			this.entry = null;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00027150 File Offset: 0x00025350
		public void CloseEntry()
		{
			bool flag = this.crc == null;
			if (flag)
			{
				throw new InvalidOperationException("Closed");
			}
			bool flag2 = this.entry == null;
			if (!flag2)
			{
				bool flag3 = this.method == 8;
				if (flag3)
				{
					bool flag4 = (this.flags & 8) != 0;
					if (flag4)
					{
						byte[] array = new byte[4096];
						while (this.Read(array, 0, array.Length) > 0)
						{
						}
						return;
					}
					this.csize -= this.inf.TotalIn;
					this.inputBuffer.Available += this.inf.RemainingInput;
				}
				bool flag5 = (long)this.inputBuffer.Available > this.csize && this.csize >= 0L;
				if (flag5)
				{
					this.inputBuffer.Available = (int)((long)this.inputBuffer.Available - this.csize);
				}
				else
				{
					this.csize -= (long)this.inputBuffer.Available;
					this.inputBuffer.Available = 0;
					while (this.csize != 0L)
					{
						long num = base.Skip(this.csize);
						bool flag6 = num <= 0L;
						if (flag6)
						{
							throw new ZipException("Zip archive ends early.");
						}
						this.csize -= num;
					}
				}
				this.CompleteCloseEntry(false);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000272D4 File Offset: 0x000254D4
		public override int Available
		{
			get
			{
				return (this.entry != null) ? 1 : 0;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000272F4 File Offset: 0x000254F4
		public override long Length
		{
			get
			{
				bool flag = this.entry != null;
				if (!flag)
				{
					throw new InvalidOperationException("No current entry");
				}
				bool flag2 = this.entry.Size >= 0L;
				if (flag2)
				{
					return this.entry.Size;
				}
				throw new ZipException("Length not available for the current entry");
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00027350 File Offset: 0x00025550
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			bool flag = this.Read(array, 0, 1) <= 0;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				num = (int)(array[0] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002738A File Offset: 0x0002558A
		private int ReadingNotAvailable(byte[] destination, int offset, int count)
		{
			throw new InvalidOperationException("Unable to read from this stream");
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00027397 File Offset: 0x00025597
		private int ReadingNotSupported(byte[] destination, int offset, int count)
		{
			throw new ZipException("The compression method for this entry is not supported");
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000273A4 File Offset: 0x000255A4
		private int InitialRead(byte[] destination, int offset, int count)
		{
			bool flag = !this.CanDecompressEntry;
			if (flag)
			{
				throw new ZipException("Library cannot extract this entry. Version required is (" + this.entry.Version.ToString() + ")");
			}
			bool isCrypted = this.entry.IsCrypted;
			if (isCrypted)
			{
				bool flag2 = this.password == null;
				if (flag2)
				{
					throw new ZipException("No password set.");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				byte[] array = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(this.password));
				this.inputBuffer.CryptoTransform = pkzipClassicManaged.CreateDecryptor(array, null);
				byte[] array2 = new byte[12];
				this.inputBuffer.ReadClearTextBuffer(array2, 0, 12);
				bool flag3 = array2[11] != this.entry.CryptoCheckValue;
				if (flag3)
				{
					throw new ZipException("Invalid password");
				}
				bool flag4 = this.csize >= 12L;
				if (flag4)
				{
					this.csize -= 12L;
				}
				else
				{
					bool flag5 = (this.entry.Flags & 8) == 0;
					if (flag5)
					{
						throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", this.csize));
					}
				}
			}
			else
			{
				this.inputBuffer.CryptoTransform = null;
			}
			bool flag6 = this.csize > 0L || (this.flags & 8) != 0;
			int num;
			if (flag6)
			{
				bool flag7 = this.method == 8 && this.inputBuffer.Available > 0;
				if (flag7)
				{
					this.inputBuffer.SetInflaterInput(this.inf);
				}
				this.internalReader = new ZipInputStream.ReadDataHandler(this.BodyRead);
				num = this.BodyRead(destination, offset, count);
			}
			else
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
				num = 0;
			}
			return num;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002757C File Offset: 0x0002577C
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			bool flag4 = buffer.Length - offset < count;
			if (flag4)
			{
				throw new ArgumentException("Invalid offset/count combination");
			}
			return this.internalReader(buffer, offset, count);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000275FC File Offset: 0x000257FC
		private int BodyRead(byte[] buffer, int offset, int count)
		{
			bool flag = this.crc == null;
			if (flag)
			{
				throw new InvalidOperationException("Closed");
			}
			bool flag2 = this.entry == null || count <= 0;
			int num;
			if (flag2)
			{
				num = 0;
			}
			else
			{
				bool flag3 = offset + count > buffer.Length;
				if (flag3)
				{
					throw new ArgumentException("Offset + count exceeds buffer size");
				}
				bool flag4 = false;
				int num2 = this.method;
				if (num2 != 0)
				{
					if (num2 == 8)
					{
						count = base.Read(buffer, offset, count);
						bool flag5 = count <= 0;
						if (flag5)
						{
							bool flag6 = !this.inf.IsFinished;
							if (flag6)
							{
								throw new ZipException("Inflater not finished!");
							}
							this.inputBuffer.Available = this.inf.RemainingInput;
							bool flag7 = (this.flags & 8) == 0 && ((this.inf.TotalIn != this.csize && this.csize != (long)((ulong)(-1)) && this.csize != -1L) || this.inf.TotalOut != this.size);
							if (flag7)
							{
								throw new ZipException(string.Concat(new object[]
								{
									"Size mismatch: ",
									this.csize,
									";",
									this.size,
									" <-> ",
									this.inf.TotalIn,
									";",
									this.inf.TotalOut
								}));
							}
							this.inf.Reset();
							flag4 = true;
						}
					}
				}
				else
				{
					bool flag8 = (long)count > this.csize && this.csize >= 0L;
					if (flag8)
					{
						count = (int)this.csize;
					}
					bool flag9 = count > 0;
					if (flag9)
					{
						count = this.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
						bool flag10 = count > 0;
						if (flag10)
						{
							this.csize -= (long)count;
							this.size -= (long)count;
						}
					}
					bool flag11 = this.csize == 0L;
					if (flag11)
					{
						flag4 = true;
					}
					else
					{
						bool flag12 = count < 0;
						if (flag12)
						{
							throw new ZipException("EOF in stored block");
						}
					}
				}
				bool flag13 = count > 0;
				if (flag13)
				{
					this.crc.Update(buffer, offset, count);
				}
				bool flag14 = flag4;
				if (flag14)
				{
					this.CompleteCloseEntry(true);
				}
				num = count;
			}
			return num;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00027878 File Offset: 0x00025A78
		public override void Close()
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			this.crc = null;
			this.entry = null;
			base.Close();
		}

		// Token: 0x040003A9 RID: 937
		private ZipInputStream.ReadDataHandler internalReader;

		// Token: 0x040003AA RID: 938
		private Crc32 crc = new Crc32();

		// Token: 0x040003AB RID: 939
		private ZipEntry entry;

		// Token: 0x040003AC RID: 940
		private long size;

		// Token: 0x040003AD RID: 941
		private int method;

		// Token: 0x040003AE RID: 942
		private int flags;

		// Token: 0x040003AF RID: 943
		private string password;

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x06001C4E RID: 7246
		private delegate int ReadDataHandler(byte[] b, int offset, int length);
	}
}
