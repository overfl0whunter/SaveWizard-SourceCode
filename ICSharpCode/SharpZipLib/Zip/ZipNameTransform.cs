using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007F RID: 127
	public class ZipNameTransform : INameTransform
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00003ED8 File Offset: 0x000020D8
		public ZipNameTransform()
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000278A2 File Offset: 0x00025AA2
		public ZipNameTransform(string trimPrefix)
		{
			this.TrimPrefix = trimPrefix;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000278B4 File Offset: 0x00025AB4
		static ZipNameTransform()
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			int num = invalidPathChars.Length + 2;
			ZipNameTransform.InvalidEntryCharsRelaxed = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 1] = '*';
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 2] = '?';
			num = invalidPathChars.Length + 4;
			ZipNameTransform.InvalidEntryChars = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryChars[num - 1] = ':';
			ZipNameTransform.InvalidEntryChars[num - 2] = '\\';
			ZipNameTransform.InvalidEntryChars[num - 3] = '*';
			ZipNameTransform.InvalidEntryChars[num - 4] = '?';
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00027950 File Offset: 0x00025B50
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			bool flag = name.Length > 0;
			if (flag)
			{
				bool flag2 = !name.EndsWith("/");
				if (flag2)
				{
					name += "/";
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000279AC File Offset: 0x00025BAC
		public string TransformFile(string name)
		{
			bool flag = name != null;
			if (flag)
			{
				string text = name.ToLower();
				bool flag2 = this.trimPrefix_ != null && text.IndexOf(this.trimPrefix_) == 0;
				if (flag2)
				{
					name = name.Substring(this.trimPrefix_.Length);
				}
				name = name.Replace("\\", "/");
				name = WindowsPathUtils.DropPathRoot(name);
				while (name.Length > 0 && name[0] == '/')
				{
					name = name.Remove(0, 1);
				}
				while (name.Length > 0 && name[name.Length - 1] == '/')
				{
					name = name.Remove(name.Length - 1, 1);
				}
				for (int i = name.IndexOf("//"); i >= 0; i = name.IndexOf("//"))
				{
					name = name.Remove(i, 1);
				}
				name = ZipNameTransform.MakeValidName(name, '_');
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00027AD0 File Offset: 0x00025CD0
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public string TrimPrefix
		{
			get
			{
				return this.trimPrefix_;
			}
			set
			{
				this.trimPrefix_ = value;
				bool flag = this.trimPrefix_ != null;
				if (flag)
				{
					this.trimPrefix_ = this.trimPrefix_.ToLower();
				}
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00027B20 File Offset: 0x00025D20
		private static string MakeValidName(string name, char replacement)
		{
			int i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars);
			bool flag = i >= 0;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder(name);
				while (i >= 0)
				{
					stringBuilder[i] = replacement;
					bool flag2 = i >= name.Length;
					if (flag2)
					{
						i = -1;
					}
					else
					{
						i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			bool flag3 = name.Length > 65535;
			if (flag3)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00027BB8 File Offset: 0x00025DB8
		public static bool IsValidName(string name, bool relaxed)
		{
			bool flag = name != null;
			bool flag2 = flag;
			if (flag2)
			{
				if (relaxed)
				{
					flag = name.IndexOfAny(ZipNameTransform.InvalidEntryCharsRelaxed) < 0;
				}
				else
				{
					flag = name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
				}
			}
			return flag;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00027C10 File Offset: 0x00025E10
		public static bool IsValidName(string name)
		{
			return name != null && name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
		}

		// Token: 0x040003B0 RID: 944
		private string trimPrefix_;

		// Token: 0x040003B1 RID: 945
		private static readonly char[] InvalidEntryChars;

		// Token: 0x040003B2 RID: 946
		private static readonly char[] InvalidEntryCharsRelaxed;
	}
}
