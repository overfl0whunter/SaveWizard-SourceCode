using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200005C RID: 92
	public class WindowsNameTransform : INameTransform
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0001F454 File Offset: 0x0001D654
		public WindowsNameTransform(string baseDirectory)
		{
			bool flag = baseDirectory == null;
			if (flag)
			{
				throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
			}
			this.BaseDirectory = baseDirectory;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001F492 File Offset: 0x0001D692
		public WindowsNameTransform()
		{
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				this._baseDirectory = Path.GetFullPath(value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001F4EC File Offset: 0x0001D6EC
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0001F504 File Offset: 0x0001D704
		public bool TrimIncomingPaths
		{
			get
			{
				return this._trimIncomingPaths;
			}
			set
			{
				this._trimIncomingPaths = value;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001F510 File Offset: 0x0001D710
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			bool flag = name.Length > 0;
			if (flag)
			{
				while (name.EndsWith("\\"))
				{
					name = name.Remove(name.Length - 1, 1);
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001F570 File Offset: 0x0001D770
		public string TransformFile(string name)
		{
			bool flag = name != null;
			if (flag)
			{
				name = WindowsNameTransform.MakeValidName(name, this._replacementChar);
				bool trimIncomingPaths = this._trimIncomingPaths;
				if (trimIncomingPaths)
				{
					name = Path.GetFileName(name);
				}
				bool flag2 = this._baseDirectory != null;
				if (flag2)
				{
					name = Path.Combine(this._baseDirectory, name);
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		public static bool IsValidName(string name)
		{
			return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_')) == 0;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001F610 File Offset: 0x0001D810
		static WindowsNameTransform()
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			int num = invalidPathChars.Length + 3;
			WindowsNameTransform.InvalidEntryChars = new char[num];
			Array.Copy(invalidPathChars, 0, WindowsNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
			WindowsNameTransform.InvalidEntryChars[num - 1] = '*';
			WindowsNameTransform.InvalidEntryChars[num - 2] = '?';
			WindowsNameTransform.InvalidEntryChars[num - 3] = ':';
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001F668 File Offset: 0x0001D868
		public static string MakeValidName(string name, char replacement)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			name = WindowsPathUtils.DropPathRoot(name.Replace("/", "\\"));
			while (name.Length > 0 && name[0] == '\\')
			{
				name = name.Remove(0, 1);
			}
			while (name.Length > 0 && name[name.Length - 1] == '\\')
			{
				name = name.Remove(name.Length - 1, 1);
			}
			int i;
			for (i = name.IndexOf("\\\\"); i >= 0; i = name.IndexOf("\\\\"))
			{
				name = name.Remove(i, 1);
			}
			i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
			bool flag2 = i >= 0;
			if (flag2)
			{
				StringBuilder stringBuilder = new StringBuilder(name);
				while (i >= 0)
				{
					stringBuilder[i] = replacement;
					bool flag3 = i >= name.Length;
					if (flag3)
					{
						i = -1;
					}
					else
					{
						i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			bool flag4 = name.Length > 260;
			if (flag4)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001F7E0 File Offset: 0x0001D9E0
		public char Replacement
		{
			get
			{
				return this._replacementChar;
			}
			set
			{
				for (int i = 0; i < WindowsNameTransform.InvalidEntryChars.Length; i++)
				{
					bool flag = WindowsNameTransform.InvalidEntryChars[i] == value;
					if (flag)
					{
						throw new ArgumentException("invalid path character");
					}
				}
				bool flag2 = value == '\\' || value == '/';
				if (flag2)
				{
					throw new ArgumentException("invalid replacement character");
				}
				this._replacementChar = value;
			}
		}

		// Token: 0x040002DF RID: 735
		private const int MaxPath = 260;

		// Token: 0x040002E0 RID: 736
		private string _baseDirectory;

		// Token: 0x040002E1 RID: 737
		private bool _trimIncomingPaths;

		// Token: 0x040002E2 RID: 738
		private char _replacementChar = '_';

		// Token: 0x040002E3 RID: 739
		private static readonly char[] InvalidEntryChars;
	}
}
