using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000063 RID: 99
	public class ZipEntry : ICloneable
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0001FA13 File Offset: 0x0001DC13
		public ZipEntry(string name)
			: this(name, 0, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001FA22 File Offset: 0x0001DC22
		internal ZipEntry(string name, int versionRequiredToExtract)
			: this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001FA34 File Offset: 0x0001DC34
		internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name.Length > 65535;
			if (flag2)
			{
				throw new ArgumentException("Name is too long", "name");
			}
			bool flag3 = versionRequiredToExtract != 0 && versionRequiredToExtract < 10;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("versionRequiredToExtract");
			}
			this.DateTime = DateTime.Now;
			this.name = name;
			this.versionMadeBy = (ushort)madeByInfo;
			this.versionToExtract = (ushort)versionRequiredToExtract;
			this.method = method;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001FADC File Offset: 0x0001DCDC
		[Obsolete("Use Clone instead")]
		public ZipEntry(ZipEntry entry)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			bool flag = entry == null;
			if (flag)
			{
				throw new ArgumentNullException("entry");
			}
			this.known = entry.known;
			this.name = entry.name;
			this.size = entry.size;
			this.compressedSize = entry.compressedSize;
			this.crc = entry.crc;
			this.dosTime = entry.dosTime;
			this.method = entry.method;
			this.comment = entry.comment;
			this.versionToExtract = entry.versionToExtract;
			this.versionMadeBy = entry.versionMadeBy;
			this.externalFileAttributes = entry.externalFileAttributes;
			this.flags = entry.flags;
			this.zipFileIndex = entry.zipFileIndex;
			this.offset = entry.offset;
			this.forceZip64_ = entry.forceZip64_;
			bool flag2 = entry.extra != null;
			if (flag2)
			{
				this.extra = new byte[entry.extra.Length];
				Array.Copy(entry.extra, 0, this.extra, 0, entry.extra.Length);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001FC10 File Offset: 0x0001DE10
		public bool HasCrc
		{
			get
			{
				return (this.known & ZipEntry.Known.Crc) > ZipEntry.Known.None;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001FC30 File Offset: 0x0001DE30
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public bool IsCrypted
		{
			get
			{
				return (this.flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
				}
				else
				{
					this.flags &= -2;
				}
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001FC88 File Offset: 0x0001DE88
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public bool IsUnicodeText
		{
			get
			{
				return (this.flags & 2048) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 2048;
				}
				else
				{
					this.flags &= -2049;
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0001FD04 File Offset: 0x0001DF04
		internal byte CryptoCheckValue
		{
			get
			{
				return this.cryptoCheckValue_;
			}
			set
			{
				this.cryptoCheckValue_ = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001FD10 File Offset: 0x0001DF10
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0001FD28 File Offset: 0x0001DF28
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001FD34 File Offset: 0x0001DF34
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		public long ZipFileIndex
		{
			get
			{
				return this.zipFileIndex;
			}
			set
			{
				this.zipFileIndex = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001FD58 File Offset: 0x0001DF58
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0001FD70 File Offset: 0x0001DF70
		public long Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001FDAA File Offset: 0x0001DFAA
		public int ExternalFileAttributes
		{
			get
			{
				bool flag = (this.known & ZipEntry.Known.ExternalAttributes) == ZipEntry.Known.None;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					num = this.externalFileAttributes;
				}
				return num;
			}
			set
			{
				this.externalFileAttributes = value;
				this.known |= ZipEntry.Known.ExternalAttributes;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		public int VersionMadeBy
		{
			get
			{
				return (int)(this.versionMadeBy & 255);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
		public bool IsDOSEntry
		{
			get
			{
				return this.HostSystem == 0 || this.HostSystem == 10;
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private bool HasDosAttributes(int attributes)
		{
			bool flag = false;
			bool flag2 = (this.known & ZipEntry.Known.ExternalAttributes) > ZipEntry.Known.None;
			if (flag2)
			{
				bool flag3 = (this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes;
				if (flag3)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001FE5C File Offset: 0x0001E05C
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0001FE7C File Offset: 0x0001E07C
		public int HostSystem
		{
			get
			{
				return (this.versionMadeBy >> 8) & 255;
			}
			set
			{
				this.versionMadeBy &= 255;
				this.versionMadeBy |= (ushort)((value & 255) << 8);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		public int Version
		{
			get
			{
				bool flag = this.versionToExtract > 0;
				int num;
				if (flag)
				{
					num = (int)this.versionToExtract;
				}
				else
				{
					int num2 = 10;
					bool flag2 = this.AESKeySize > 0;
					if (flag2)
					{
						num2 = 51;
					}
					else
					{
						bool centralHeaderRequiresZip = this.CentralHeaderRequiresZip64;
						if (centralHeaderRequiresZip)
						{
							num2 = 45;
						}
						else
						{
							bool flag3 = CompressionMethod.Deflated == this.method;
							if (flag3)
							{
								num2 = 20;
							}
							else
							{
								bool isDirectory = this.IsDirectory;
								if (isDirectory)
								{
									num2 = 20;
								}
								else
								{
									bool isCrypted = this.IsCrypted;
									if (isCrypted)
									{
										num2 = 20;
									}
									else
									{
										bool flag4 = this.HasDosAttributes(8);
										if (flag4)
										{
											num2 = 11;
										}
									}
								}
							}
						}
					}
					num = num2;
				}
				return num;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001FF50 File Offset: 0x0001E150
		public bool CanDecompress
		{
			get
			{
				return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 51) && this.IsCompressionMethodSupported();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001FFA7 File Offset: 0x0001E1A7
		public void ForceZip64()
		{
			this.forceZip64_ = true;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		public bool IsZip64Forced()
		{
			return this.forceZip64_;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001FFCC File Offset: 0x0001E1CC
		public bool LocalHeaderRequiresZip64
		{
			get
			{
				bool flag = this.forceZip64_;
				bool flag2 = !flag;
				if (flag2)
				{
					ulong num = this.compressedSize;
					bool flag3 = this.versionToExtract == 0 && this.IsCrypted;
					if (flag3)
					{
						num += 12UL;
					}
					flag = (this.size >= (ulong)(-1) || num >= (ulong)(-1)) && (this.versionToExtract == 0 || this.versionToExtract >= 45);
				}
				return flag;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00020044 File Offset: 0x0001E244
		public bool CentralHeaderRequiresZip64
		{
			get
			{
				return this.LocalHeaderRequiresZip64 || this.offset >= (long)((ulong)(-1));
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00020070 File Offset: 0x0001E270
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0002009F File Offset: 0x0001E29F
		public long DosTime
		{
			get
			{
				bool flag = (this.known & ZipEntry.Known.Time) == ZipEntry.Known.None;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = (long)((ulong)this.dosTime);
				}
				return num;
			}
			set
			{
				this.dosTime = (uint)value;
				this.known |= ZipEntry.Known.Time;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000200BC File Offset: 0x0001E2BC
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00020168 File Offset: 0x0001E368
		public DateTime DateTime
		{
			get
			{
				uint num = Math.Min(59U, 2U * (this.dosTime & 31U));
				uint num2 = Math.Min(59U, (this.dosTime >> 5) & 63U);
				uint num3 = Math.Min(23U, (this.dosTime >> 11) & 31U);
				uint num4 = Math.Max(1U, Math.Min(12U, (this.dosTime >> 21) & 15U));
				uint num5 = ((this.dosTime >> 25) & 127U) + 1980U;
				int num6 = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num5, (int)num4), (int)((this.dosTime >> 16) & 31U)));
				return new DateTime((int)num5, (int)num4, num6, (int)num3, (int)num2, (int)num);
			}
			set
			{
				uint num = (uint)value.Year;
				uint num2 = (uint)value.Month;
				uint num3 = (uint)value.Day;
				uint num4 = (uint)value.Hour;
				uint num5 = (uint)value.Minute;
				uint num6 = (uint)value.Second;
				bool flag = num < 1980U;
				if (flag)
				{
					num = 1980U;
					num2 = 1U;
					num3 = 1U;
					num4 = 0U;
					num5 = 0U;
					num6 = 0U;
				}
				else
				{
					bool flag2 = num > 2107U;
					if (flag2)
					{
						num = 2107U;
						num2 = 12U;
						num3 = 31U;
						num4 = 23U;
						num5 = 59U;
						num6 = 59U;
					}
				}
				this.DosTime = (long)((ulong)((((num - 1980U) & 127U) << 25) | (num2 << 21) | (num3 << 16) | (num4 << 11) | (num5 << 5) | (num6 >> 1)));
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00020224 File Offset: 0x0001E424
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0002023C File Offset: 0x0001E43C
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00020262 File Offset: 0x0001E462
		public long Size
		{
			get
			{
				return (long)(((this.known & ZipEntry.Known.Size) != ZipEntry.Known.None) ? this.size : ulong.MaxValue);
			}
			set
			{
				this.size = (ulong)value;
				this.known |= ZipEntry.Known.Size;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0002027C File Offset: 0x0001E47C
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000202A2 File Offset: 0x0001E4A2
		public long CompressedSize
		{
			get
			{
				return (long)(((this.known & ZipEntry.Known.CompressedSize) != ZipEntry.Known.None) ? this.compressedSize : ulong.MaxValue);
			}
			set
			{
				this.compressedSize = (ulong)value;
				this.known |= ZipEntry.Known.CompressedSize;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000202BC File Offset: 0x0001E4BC
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000202E8 File Offset: 0x0001E4E8
		public long Crc
		{
			get
			{
				return (long)(((this.known & ZipEntry.Known.Crc) != ZipEntry.Known.None) ? ((ulong)this.crc & (ulong)(-1)) : ulong.MaxValue);
			}
			set
			{
				bool flag = ((ulong)this.crc & 18446744069414584320UL) > 0UL;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.crc = (uint)value;
				this.known |= ZipEntry.Known.Crc;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00020334 File Offset: 0x0001E534
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0002034C File Offset: 0x0001E54C
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.method;
			}
			set
			{
				bool flag = !ZipEntry.IsCompressionMethodSupported(value);
				if (flag)
				{
					throw new NotSupportedException("Compression method not supported");
				}
				this.method = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0002037C File Offset: 0x0001E57C
		internal CompressionMethod CompressionMethodForHeader
		{
			get
			{
				return (this.AESKeySize > 0) ? CompressionMethod.WinZipAES : this.method;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000203A4 File Offset: 0x0001E5A4
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000203BC File Offset: 0x0001E5BC
		public byte[] ExtraData
		{
			get
			{
				return this.extra;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.extra = null;
				}
				else
				{
					bool flag2 = value.Length > 65535;
					if (flag2)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this.extra = new byte[value.Length];
					Array.Copy(value, 0, this.extra, 0, value.Length);
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0002041C File Offset: 0x0001E61C
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00020484 File Offset: 0x0001E684
		public int AESKeySize
		{
			get
			{
				int num;
				switch (this._aesEncryptionStrength)
				{
				case 0:
					num = 0;
					break;
				case 1:
					num = 128;
					break;
				case 2:
					num = 192;
					break;
				case 3:
					num = 256;
					break;
				default:
					throw new ZipException("Invalid AESEncryptionStrength " + this._aesEncryptionStrength);
				}
				return num;
			}
			set
			{
				if (value != 0)
				{
					if (value != 128)
					{
						if (value != 256)
						{
							throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
						}
						this._aesEncryptionStrength = 3;
					}
					else
					{
						this._aesEncryptionStrength = 1;
					}
				}
				else
				{
					this._aesEncryptionStrength = 0;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000204E0 File Offset: 0x0001E6E0
		internal byte AESEncryptionStrength
		{
			get
			{
				return (byte)this._aesEncryptionStrength;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000204FC File Offset: 0x0001E6FC
		internal int AESSaltLen
		{
			get
			{
				return this.AESKeySize / 16;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00020518 File Offset: 0x0001E718
		internal int AESOverheadSize
		{
			get
			{
				return 12 + this.AESSaltLen;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00020534 File Offset: 0x0001E734
		internal void ProcessExtraData(bool localHeader)
		{
			ZipExtraData zipExtraData = new ZipExtraData(this.extra);
			bool flag = zipExtraData.Find(1);
			if (flag)
			{
				this.forceZip64_ = true;
				bool flag2 = zipExtraData.ValueLength < 4;
				if (flag2)
				{
					throw new ZipException("Extra data extended Zip64 information length is invalid");
				}
				bool flag3 = localHeader || this.size == (ulong)(-1);
				if (flag3)
				{
					this.size = (ulong)zipExtraData.ReadLong();
				}
				bool flag4 = localHeader || this.compressedSize == (ulong)(-1);
				if (flag4)
				{
					this.compressedSize = (ulong)zipExtraData.ReadLong();
				}
				bool flag5 = !localHeader && this.offset == (long)((ulong)(-1));
				if (flag5)
				{
					this.offset = zipExtraData.ReadLong();
				}
			}
			else
			{
				bool flag6 = (this.versionToExtract & 255) >= 45 && (this.size == (ulong)(-1) || this.compressedSize == (ulong)(-1));
				if (flag6)
				{
					throw new ZipException("Zip64 Extended information required but is missing.");
				}
			}
			bool flag7 = zipExtraData.Find(10);
			if (flag7)
			{
				bool flag8 = zipExtraData.ValueLength < 4;
				if (flag8)
				{
					throw new ZipException("NTFS Extra data invalid");
				}
				zipExtraData.ReadInt();
				while (zipExtraData.UnreadCount >= 4)
				{
					int num = zipExtraData.ReadShort();
					int num2 = zipExtraData.ReadShort();
					bool flag9 = num == 1;
					if (flag9)
					{
						bool flag10 = num2 >= 24;
						if (flag10)
						{
							long num3 = zipExtraData.ReadLong();
							long num4 = zipExtraData.ReadLong();
							long num5 = zipExtraData.ReadLong();
							this.DateTime = DateTime.FromFileTime(num3);
						}
						break;
					}
					zipExtraData.Skip(num2);
				}
			}
			else
			{
				bool flag11 = zipExtraData.Find(21589);
				if (flag11)
				{
					int valueLength = zipExtraData.ValueLength;
					int num6 = zipExtraData.ReadByte();
					bool flag12 = (num6 & 1) != 0 && valueLength >= 5;
					if (flag12)
					{
						int num7 = zipExtraData.ReadInt();
						this.DateTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, num7, 0)).ToLocalTime();
					}
				}
			}
			bool flag13 = this.method == CompressionMethod.WinZipAES;
			if (flag13)
			{
				this.ProcessAESExtraData(zipExtraData);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002076C File Offset: 0x0001E96C
		private void ProcessAESExtraData(ZipExtraData extraData)
		{
			bool flag = extraData.Find(39169);
			if (!flag)
			{
				throw new ZipException("AES Extra Data missing");
			}
			this.versionToExtract = 51;
			this.Flags |= 64;
			int valueLength = extraData.ValueLength;
			bool flag2 = valueLength < 7;
			if (flag2)
			{
				throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
			}
			int num = extraData.ReadShort();
			int num2 = extraData.ReadShort();
			int num3 = extraData.ReadByte();
			int num4 = extraData.ReadShort();
			this._aesVer = num;
			this._aesEncryptionStrength = num3;
			this.method = (CompressionMethod)num4;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00020814 File Offset: 0x0001EA14
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0002082C File Offset: 0x0001EA2C
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				bool flag = value != null && value.Length > 65535;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
				}
				this.comment = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0002086C File Offset: 0x0001EA6C
		public bool IsDirectory
		{
			get
			{
				int length = this.name.Length;
				return (length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\')) || this.HasDosAttributes(16);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000208C0 File Offset: 0x0001EAC0
		public bool IsFile
		{
			get
			{
				return !this.IsDirectory && !this.HasDosAttributes(8);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public bool IsCompressionMethodSupported()
		{
			return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00020908 File Offset: 0x0001EB08
		public object Clone()
		{
			ZipEntry zipEntry = (ZipEntry)base.MemberwiseClone();
			bool flag = this.extra != null;
			if (flag)
			{
				zipEntry.extra = new byte[this.extra.Length];
				Array.Copy(this.extra, 0, zipEntry.extra, 0, this.extra.Length);
			}
			return zipEntry;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00020968 File Offset: 0x0001EB68
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00020980 File Offset: 0x0001EB80
		public static bool IsCompressionMethodSupported(CompressionMethod method)
		{
			return method == CompressionMethod.Deflated || method == CompressionMethod.Stored;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000209A0 File Offset: 0x0001EBA0
		public static string CleanName(string name)
		{
			bool flag = name == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = Path.IsPathRooted(name);
				if (flag2)
				{
					name = name.Substring(Path.GetPathRoot(name).Length);
				}
				name = name.Replace("\\", "/");
				while (name.Length > 0 && name[0] == '/')
				{
					name = name.Remove(0, 1);
				}
				text = name;
			}
			return text;
		}

		// Token: 0x04000347 RID: 839
		private ZipEntry.Known known;

		// Token: 0x04000348 RID: 840
		private int externalFileAttributes;

		// Token: 0x04000349 RID: 841
		private ushort versionMadeBy;

		// Token: 0x0400034A RID: 842
		private string name;

		// Token: 0x0400034B RID: 843
		private ulong size;

		// Token: 0x0400034C RID: 844
		private ulong compressedSize;

		// Token: 0x0400034D RID: 845
		private ushort versionToExtract;

		// Token: 0x0400034E RID: 846
		private uint crc;

		// Token: 0x0400034F RID: 847
		private uint dosTime;

		// Token: 0x04000350 RID: 848
		private CompressionMethod method;

		// Token: 0x04000351 RID: 849
		private byte[] extra;

		// Token: 0x04000352 RID: 850
		private string comment;

		// Token: 0x04000353 RID: 851
		private int flags;

		// Token: 0x04000354 RID: 852
		private long zipFileIndex;

		// Token: 0x04000355 RID: 853
		private long offset;

		// Token: 0x04000356 RID: 854
		private bool forceZip64_;

		// Token: 0x04000357 RID: 855
		private byte cryptoCheckValue_;

		// Token: 0x04000358 RID: 856
		private int _aesVer;

		// Token: 0x04000359 RID: 857
		private int _aesEncryptionStrength;

		// Token: 0x0200020A RID: 522
		[Flags]
		private enum Known : byte
		{
			// Token: 0x04000DAC RID: 3500
			None = 0,
			// Token: 0x04000DAD RID: 3501
			Size = 1,
			// Token: 0x04000DAE RID: 3502
			CompressedSize = 2,
			// Token: 0x04000DAF RID: 3503
			Crc = 4,
			// Token: 0x04000DB0 RID: 3504
			Time = 8,
			// Token: 0x04000DB1 RID: 3505
			ExternalAttributes = 16
		}
	}
}
