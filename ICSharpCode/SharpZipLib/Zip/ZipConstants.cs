using System;
using System.Text;
using System.Threading;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000061 RID: 97
	public sealed class ZipConstants
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001F844 File Offset: 0x0001DA44
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001F872 File Offset: 0x0001DA72
		public static int DefaultCodePage
		{
			get
			{
				bool flag = ZipConstants.defaultCodePage != 1;
				int num;
				if (flag)
				{
					num = ZipConstants.defaultCodePage;
				}
				else
				{
					num = 437;
				}
				return num;
			}
			set
			{
				ZipConstants.defaultCodePage = value;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001F87C File Offset: 0x0001DA7C
		public static string ConvertToString(byte[] data, int count)
		{
			bool flag = data == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				text = Encoding.GetEncoding(ZipConstants.DefaultCodePage).GetString(data, 0, count);
			}
			return text;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001F8B4 File Offset: 0x0001DAB4
		public static string ConvertToString(byte[] data)
		{
			bool flag = data == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				text = ZipConstants.ConvertToString(data, data.Length);
			}
			return text;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			bool flag = data == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = (flags & 2048) != 0;
				if (flag2)
				{
					text = Encoding.UTF8.GetString(data, 0, count);
				}
				else
				{
					text = ZipConstants.ConvertToString(data, count);
				}
			}
			return text;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001F92C File Offset: 0x0001DB2C
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			bool flag = data == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = (flags & 2048) != 0;
				if (flag2)
				{
					text = Encoding.UTF8.GetString(data, 0, data.Length);
				}
				else
				{
					text = ZipConstants.ConvertToString(data, data.Length);
				}
			}
			return text;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001F97C File Offset: 0x0001DB7C
		public static byte[] ConvertToArray(string str)
		{
			bool flag = str == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				array = Encoding.GetEncoding(ZipConstants.DefaultCodePage).GetBytes(str);
			}
			return array;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public static byte[] ConvertToArray(int flags, string str)
		{
			bool flag = str == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				bool flag2 = (flags & 2048) != 0;
				if (flag2)
				{
					array = Encoding.UTF8.GetBytes(str);
				}
				else
				{
					array = ZipConstants.ConvertToArray(str);
				}
			}
			return array;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00003ED8 File Offset: 0x000020D8
		private ZipConstants()
		{
		}

		// Token: 0x0400030D RID: 781
		public const int VersionMadeBy = 51;

		// Token: 0x0400030E RID: 782
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 51;

		// Token: 0x0400030F RID: 783
		public const int VersionStrongEncryption = 50;

		// Token: 0x04000310 RID: 784
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;

		// Token: 0x04000311 RID: 785
		public const int VERSION_AES = 51;

		// Token: 0x04000312 RID: 786
		public const int VersionZip64 = 45;

		// Token: 0x04000313 RID: 787
		public const int LocalHeaderBaseSize = 30;

		// Token: 0x04000314 RID: 788
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;

		// Token: 0x04000315 RID: 789
		public const int Zip64DataDescriptorSize = 24;

		// Token: 0x04000316 RID: 790
		public const int DataDescriptorSize = 16;

		// Token: 0x04000317 RID: 791
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;

		// Token: 0x04000318 RID: 792
		public const int CentralHeaderBaseSize = 46;

		// Token: 0x04000319 RID: 793
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;

		// Token: 0x0400031A RID: 794
		public const int EndOfCentralRecordBaseSize = 22;

		// Token: 0x0400031B RID: 795
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;

		// Token: 0x0400031C RID: 796
		public const int CryptoHeaderSize = 12;

		// Token: 0x0400031D RID: 797
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;

		// Token: 0x0400031E RID: 798
		public const int LocalHeaderSignature = 67324752;

		// Token: 0x0400031F RID: 799
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 67324752;

		// Token: 0x04000320 RID: 800
		public const int SpanningSignature = 134695760;

		// Token: 0x04000321 RID: 801
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 134695760;

		// Token: 0x04000322 RID: 802
		public const int SpanningTempSignature = 808471376;

		// Token: 0x04000323 RID: 803
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 808471376;

		// Token: 0x04000324 RID: 804
		public const int DataDescriptorSignature = 134695760;

		// Token: 0x04000325 RID: 805
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 134695760;

		// Token: 0x04000326 RID: 806
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 33639248;

		// Token: 0x04000327 RID: 807
		public const int CentralHeaderSignature = 33639248;

		// Token: 0x04000328 RID: 808
		public const int Zip64CentralFileHeaderSignature = 101075792;

		// Token: 0x04000329 RID: 809
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 101075792;

		// Token: 0x0400032A RID: 810
		public const int Zip64CentralDirLocatorSignature = 117853008;

		// Token: 0x0400032B RID: 811
		public const int ArchiveExtraDataSignature = 117853008;

		// Token: 0x0400032C RID: 812
		public const int CentralHeaderDigitalSignature = 84233040;

		// Token: 0x0400032D RID: 813
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 84233040;

		// Token: 0x0400032E RID: 814
		public const int EndOfCentralDirectorySignature = 101010256;

		// Token: 0x0400032F RID: 815
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 101010256;

		// Token: 0x04000330 RID: 816
		private static int defaultCodePage = Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage;
	}
}
