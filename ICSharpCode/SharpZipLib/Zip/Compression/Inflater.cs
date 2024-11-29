using System;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000087 RID: 135
	public class Inflater
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0002AE0A File Offset: 0x0002900A
		public Inflater()
			: this(false)
		{
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002AE15 File Offset: 0x00029015
		public Inflater(bool noHeader)
		{
			this.noHeader = noHeader;
			this.adler = new Adler32();
			this.input = new StreamManipulator();
			this.outputWindow = new OutputWindow();
			this.mode = (noHeader ? 2 : 0);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002AE54 File Offset: 0x00029054
		public void Reset()
		{
			this.mode = (this.noHeader ? 2 : 0);
			this.totalIn = 0L;
			this.totalOut = 0L;
			this.input.Reset();
			this.outputWindow.Reset();
			this.dynHeader = null;
			this.litlenTree = null;
			this.distTree = null;
			this.isLastBlock = false;
			this.adler.Reset();
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002AEC4 File Offset: 0x000290C4
		private bool DecodeHeader()
		{
			int num = this.input.PeekBits(16);
			bool flag = num < 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.input.DropBits(16);
				num = ((num << 8) | (num >> 8)) & 65535;
				bool flag3 = num % 31 != 0;
				if (flag3)
				{
					throw new SharpZipBaseException("Header checksum illegal");
				}
				bool flag4 = (num & 3840) != 2048;
				if (flag4)
				{
					throw new SharpZipBaseException("Compression Method unknown");
				}
				bool flag5 = (num & 32) == 0;
				if (flag5)
				{
					this.mode = 2;
				}
				else
				{
					this.mode = 1;
					this.neededBits = 32;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002AF74 File Offset: 0x00029174
		private bool DecodeDict()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				bool flag = num < 0;
				if (flag)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8) | num;
				this.neededBits -= 8;
			}
			return false;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0002AFE0 File Offset: 0x000291E0
		private bool DecodeHuffman()
		{
			int i = this.outputWindow.GetFreeSpace();
			while (i >= 258)
			{
				int num;
				switch (this.mode)
				{
				case 7:
				{
					while (((num = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
					{
						this.outputWindow.Write(num);
						bool flag = --i < 258;
						if (flag)
						{
							return true;
						}
					}
					bool flag2 = num < 257;
					if (!flag2)
					{
						try
						{
							this.repLength = Inflater.CPLENS[num - 257];
							this.neededBits = Inflater.CPLEXT[num - 257];
						}
						catch (Exception)
						{
							throw new SharpZipBaseException("Illegal rep length code");
						}
						goto IL_00FD;
					}
					bool flag3 = num < 0;
					if (flag3)
					{
						return false;
					}
					this.distTree = null;
					this.litlenTree = null;
					this.mode = 2;
					return true;
				}
				case 8:
					goto IL_00FD;
				case 9:
					goto IL_0167;
				case 10:
					break;
				default:
					throw new SharpZipBaseException("Inflater unknown mode");
				}
				IL_01BA:
				bool flag4 = this.neededBits > 0;
				if (flag4)
				{
					this.mode = 10;
					int num2 = this.input.PeekBits(this.neededBits);
					bool flag5 = num2 < 0;
					if (flag5)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repDist += num2;
				}
				this.outputWindow.Repeat(this.repLength, this.repDist);
				i -= this.repLength;
				this.mode = 7;
				continue;
				IL_0167:
				num = this.distTree.GetSymbol(this.input);
				bool flag6 = num < 0;
				if (flag6)
				{
					return false;
				}
				try
				{
					this.repDist = Inflater.CPDIST[num];
					this.neededBits = Inflater.CPDEXT[num];
				}
				catch (Exception)
				{
					throw new SharpZipBaseException("Illegal rep dist code");
				}
				goto IL_01BA;
				IL_00FD:
				bool flag7 = this.neededBits > 0;
				if (flag7)
				{
					this.mode = 8;
					int num3 = this.input.PeekBits(this.neededBits);
					bool flag8 = num3 < 0;
					if (flag8)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repLength += num3;
				}
				this.mode = 9;
				goto IL_0167;
			}
			return true;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0002B274 File Offset: 0x00029474
		private bool DecodeChksum()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				bool flag = num < 0;
				if (flag)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8) | num;
				this.neededBits -= 8;
			}
			bool flag2 = (int)this.adler.Value != this.readAdler;
			if (flag2)
			{
				throw new SharpZipBaseException(string.Concat(new object[]
				{
					"Adler chksum doesn't match: ",
					(int)this.adler.Value,
					" vs. ",
					this.readAdler
				}));
			}
			this.mode = 12;
			return false;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0002B34C File Offset: 0x0002954C
		private bool Decode()
		{
			switch (this.mode)
			{
			case 0:
				return this.DecodeHeader();
			case 1:
				return this.DecodeDict();
			case 2:
			{
				bool flag = this.isLastBlock;
				if (flag)
				{
					bool flag2 = this.noHeader;
					if (flag2)
					{
						this.mode = 12;
						return false;
					}
					this.input.SkipToByteBoundary();
					this.neededBits = 32;
					this.mode = 11;
					return true;
				}
				else
				{
					int num = this.input.PeekBits(3);
					bool flag3 = num < 0;
					if (flag3)
					{
						return false;
					}
					this.input.DropBits(3);
					bool flag4 = (num & 1) != 0;
					if (flag4)
					{
						this.isLastBlock = true;
					}
					switch (num >> 1)
					{
					case 0:
						this.input.SkipToByteBoundary();
						this.mode = 3;
						break;
					case 1:
						this.litlenTree = InflaterHuffmanTree.defLitLenTree;
						this.distTree = InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new InflaterDynHeader();
						this.mode = 6;
						break;
					default:
						throw new SharpZipBaseException("Unknown block type " + num);
					}
					return true;
				}
				break;
			}
			case 3:
			{
				bool flag5 = (this.uncomprLen = this.input.PeekBits(16)) < 0;
				if (flag5)
				{
					return false;
				}
				this.input.DropBits(16);
				this.mode = 4;
				break;
			}
			case 4:
				break;
			case 5:
				goto IL_0218;
			case 6:
			{
				bool flag6 = !this.dynHeader.Decode(this.input);
				if (flag6)
				{
					return false;
				}
				this.litlenTree = this.dynHeader.BuildLitLenTree();
				this.distTree = this.dynHeader.BuildDistTree();
				this.mode = 7;
				goto IL_02B7;
			}
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_02B7;
			case 11:
				return this.DecodeChksum();
			case 12:
				return false;
			default:
				throw new SharpZipBaseException("Inflater.Decode unknown mode");
			}
			int num2 = this.input.PeekBits(16);
			bool flag7 = num2 < 0;
			if (flag7)
			{
				return false;
			}
			this.input.DropBits(16);
			bool flag8 = num2 != (this.uncomprLen ^ 65535);
			if (flag8)
			{
				throw new SharpZipBaseException("broken uncompressed block");
			}
			this.mode = 5;
			IL_0218:
			int num3 = this.outputWindow.CopyStored(this.input, this.uncomprLen);
			this.uncomprLen -= num3;
			bool flag9 = this.uncomprLen == 0;
			if (flag9)
			{
				this.mode = 2;
				return true;
			}
			return !this.input.IsNeedingInput;
			IL_02B7:
			return this.DecodeHuffman();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0002B629 File Offset: 0x00029829
		public void SetDictionary(byte[] buffer)
		{
			this.SetDictionary(buffer, 0, buffer.Length);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0002B638 File Offset: 0x00029838
		public void SetDictionary(byte[] buffer, int index, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool flag4 = !this.IsNeedingDictionary;
			if (flag4)
			{
				throw new InvalidOperationException("Dictionary is not needed");
			}
			this.adler.Update(buffer, index, count);
			bool flag5 = (int)this.adler.Value != this.readAdler;
			if (flag5)
			{
				throw new SharpZipBaseException("Wrong adler checksum");
			}
			this.adler.Reset();
			this.outputWindow.CopyDict(buffer, index, count);
			this.mode = 2;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002B6F5 File Offset: 0x000298F5
		public void SetInput(byte[] buffer)
		{
			this.SetInput(buffer, 0, buffer.Length);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0002B704 File Offset: 0x00029904
		public void SetInput(byte[] buffer, int index, int count)
		{
			this.input.SetInput(buffer, index, count);
			this.totalIn += (long)count;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002B728 File Offset: 0x00029928
		public int Inflate(byte[] buffer)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Inflate(buffer, 0, buffer.Length);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002B75C File Offset: 0x0002995C
		public int Inflate(byte[] buffer, int offset, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = count < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("count", "count cannot be negative");
			}
			bool flag3 = offset < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
			}
			bool flag4 = offset + count > buffer.Length;
			if (flag4)
			{
				throw new ArgumentException("count exceeds buffer bounds");
			}
			bool flag5 = count == 0;
			int num;
			if (flag5)
			{
				bool flag6 = !this.IsFinished;
				if (flag6)
				{
					this.Decode();
				}
				num = 0;
			}
			else
			{
				int num2 = 0;
				for (;;)
				{
					bool flag7 = this.mode != 11;
					if (flag7)
					{
						int num3 = this.outputWindow.CopyOutput(buffer, offset, count);
						bool flag8 = num3 > 0;
						if (flag8)
						{
							this.adler.Update(buffer, offset, num3);
							offset += num3;
							num2 += num3;
							this.totalOut += (long)num3;
							count -= num3;
							bool flag9 = count == 0;
							if (flag9)
							{
								break;
							}
						}
					}
					if (!this.Decode() && (this.outputWindow.GetAvailable() <= 0 || this.mode == 11))
					{
						goto Block_12;
					}
				}
				return num2;
				Block_12:
				num = num2;
			}
			return num;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		public bool IsNeedingInput
		{
			get
			{
				return this.input.IsNeedingInput;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0002B8C8 File Offset: 0x00029AC8
		public bool IsNeedingDictionary
		{
			get
			{
				return this.mode == 1 && this.neededBits == 0;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002B8F0 File Offset: 0x00029AF0
		public bool IsFinished
		{
			get
			{
				return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0002B920 File Offset: 0x00029B20
		public int Adler
		{
			get
			{
				return this.IsNeedingDictionary ? this.readAdler : ((int)this.adler.Value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002B950 File Offset: 0x00029B50
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0002B968 File Offset: 0x00029B68
		public long TotalIn
		{
			get
			{
				return this.totalIn - (long)this.RemainingInput;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002B988 File Offset: 0x00029B88
		public int RemainingInput
		{
			get
			{
				return this.input.AvailableBytes;
			}
		}

		// Token: 0x04000421 RID: 1057
		private static readonly int[] CPLENS = new int[]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258
		};

		// Token: 0x04000422 RID: 1058
		private static readonly int[] CPLEXT = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		// Token: 0x04000423 RID: 1059
		private static readonly int[] CPDIST = new int[]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577
		};

		// Token: 0x04000424 RID: 1060
		private static readonly int[] CPDEXT = new int[]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		// Token: 0x04000425 RID: 1061
		private const int DECODE_HEADER = 0;

		// Token: 0x04000426 RID: 1062
		private const int DECODE_DICT = 1;

		// Token: 0x04000427 RID: 1063
		private const int DECODE_BLOCKS = 2;

		// Token: 0x04000428 RID: 1064
		private const int DECODE_STORED_LEN1 = 3;

		// Token: 0x04000429 RID: 1065
		private const int DECODE_STORED_LEN2 = 4;

		// Token: 0x0400042A RID: 1066
		private const int DECODE_STORED = 5;

		// Token: 0x0400042B RID: 1067
		private const int DECODE_DYN_HEADER = 6;

		// Token: 0x0400042C RID: 1068
		private const int DECODE_HUFFMAN = 7;

		// Token: 0x0400042D RID: 1069
		private const int DECODE_HUFFMAN_LENBITS = 8;

		// Token: 0x0400042E RID: 1070
		private const int DECODE_HUFFMAN_DIST = 9;

		// Token: 0x0400042F RID: 1071
		private const int DECODE_HUFFMAN_DISTBITS = 10;

		// Token: 0x04000430 RID: 1072
		private const int DECODE_CHKSUM = 11;

		// Token: 0x04000431 RID: 1073
		private const int FINISHED = 12;

		// Token: 0x04000432 RID: 1074
		private int mode;

		// Token: 0x04000433 RID: 1075
		private int readAdler;

		// Token: 0x04000434 RID: 1076
		private int neededBits;

		// Token: 0x04000435 RID: 1077
		private int repLength;

		// Token: 0x04000436 RID: 1078
		private int repDist;

		// Token: 0x04000437 RID: 1079
		private int uncomprLen;

		// Token: 0x04000438 RID: 1080
		private bool isLastBlock;

		// Token: 0x04000439 RID: 1081
		private long totalOut;

		// Token: 0x0400043A RID: 1082
		private long totalIn;

		// Token: 0x0400043B RID: 1083
		private bool noHeader;

		// Token: 0x0400043C RID: 1084
		private StreamManipulator input;

		// Token: 0x0400043D RID: 1085
		private OutputWindow outputWindow;

		// Token: 0x0400043E RID: 1086
		private InflaterDynHeader dynHeader;

		// Token: 0x0400043F RID: 1087
		private InflaterHuffmanTree litlenTree;

		// Token: 0x04000440 RID: 1088
		private InflaterHuffmanTree distTree;

		// Token: 0x04000441 RID: 1089
		private Adler32 adler;
	}
}
