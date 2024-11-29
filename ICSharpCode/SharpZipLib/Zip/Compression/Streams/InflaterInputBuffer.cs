using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200008C RID: 140
	public class InflaterInputBuffer
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0002CC50 File Offset: 0x0002AE50
		public InflaterInputBuffer(Stream stream)
			: this(stream, 4096)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002CC60 File Offset: 0x0002AE60
		public InflaterInputBuffer(Stream stream, int bufferSize)
		{
			this.inputStream = stream;
			bool flag = bufferSize < 1024;
			if (flag)
			{
				bufferSize = 1024;
			}
			this.rawData = new byte[bufferSize];
			this.clearText = this.rawData;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0002CCAC File Offset: 0x0002AEAC
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		public int ClearTextLength
		{
			get
			{
				return this.clearTextLength;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0002CCF4 File Offset: 0x0002AEF4
		public byte[] ClearText
		{
			get
			{
				return this.clearText;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0002CD24 File Offset: 0x0002AF24
		public int Available
		{
			get
			{
				return this.available;
			}
			set
			{
				this.available = value;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002CD30 File Offset: 0x0002AF30
		public void SetInflaterInput(Inflater inflater)
		{
			bool flag = this.available > 0;
			if (flag)
			{
				inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
				this.available = 0;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002CD74 File Offset: 0x0002AF74
		public void Fill()
		{
			this.rawLength = 0;
			int num;
			for (int i = this.rawData.Length; i > 0; i -= num)
			{
				num = this.inputStream.Read(this.rawData, this.rawLength, i);
				bool flag = num <= 0;
				if (flag)
				{
					break;
				}
				this.rawLength += num;
			}
			bool flag2 = this.cryptoTransform != null;
			if (flag2)
			{
				this.clearTextLength = this.cryptoTransform.TransformBlock(this.rawData, 0, this.rawLength, this.clearText, 0);
			}
			else
			{
				this.clearTextLength = this.rawLength;
			}
			this.available = this.clearTextLength;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002CE2C File Offset: 0x0002B02C
		public int ReadRawBuffer(byte[] buffer)
		{
			return this.ReadRawBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002CE4C File Offset: 0x0002B04C
		public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				bool flag2 = this.available <= 0;
				if (flag2)
				{
					this.Fill();
					bool flag3 = this.available <= 0;
					if (flag3)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.rawData, this.rawLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				bool flag2 = this.available <= 0;
				if (flag2)
				{
					this.Fill();
					bool flag3 = this.available <= 0;
					if (flag3)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.clearText, this.clearTextLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		public int ReadLeByte()
		{
			bool flag = this.available <= 0;
			if (flag)
			{
				this.Fill();
				bool flag2 = this.available <= 0;
				if (flag2)
				{
					throw new ZipException("EOF in header");
				}
			}
			byte b = this.rawData[this.rawLength - this.available];
			this.available--;
			return (int)b;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002D018 File Offset: 0x0002B218
		public int ReadLeShort()
		{
			return this.ReadLeByte() | (this.ReadLeByte() << 8);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002D03C File Offset: 0x0002B23C
		public int ReadLeInt()
		{
			return this.ReadLeShort() | (this.ReadLeShort() << 16);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0002D060 File Offset: 0x0002B260
		public long ReadLeLong()
		{
			return (long)((ulong)this.ReadLeInt() | (ulong)((ulong)((long)this.ReadLeInt()) << 32));
		}

		// Token: 0x170001A1 RID: 417
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0002D084 File Offset: 0x0002B284
		public ICryptoTransform CryptoTransform
		{
			set
			{
				this.cryptoTransform = value;
				bool flag = this.cryptoTransform != null;
				if (flag)
				{
					bool flag2 = this.rawData == this.clearText;
					if (flag2)
					{
						bool flag3 = this.internalClearText == null;
						if (flag3)
						{
							this.internalClearText = new byte[this.rawData.Length];
						}
						this.clearText = this.internalClearText;
					}
					this.clearTextLength = this.rawLength;
					bool flag4 = this.available > 0;
					if (flag4)
					{
						this.cryptoTransform.TransformBlock(this.rawData, this.rawLength - this.available, this.available, this.clearText, this.rawLength - this.available);
					}
				}
				else
				{
					this.clearText = this.rawData;
					this.clearTextLength = this.rawLength;
				}
			}
		}

		// Token: 0x04000468 RID: 1128
		private int rawLength;

		// Token: 0x04000469 RID: 1129
		private byte[] rawData;

		// Token: 0x0400046A RID: 1130
		private int clearTextLength;

		// Token: 0x0400046B RID: 1131
		private byte[] clearText;

		// Token: 0x0400046C RID: 1132
		private byte[] internalClearText;

		// Token: 0x0400046D RID: 1133
		private int available;

		// Token: 0x0400046E RID: 1134
		private ICryptoTransform cryptoTransform;

		// Token: 0x0400046F RID: 1135
		private Stream inputStream;
	}
}
