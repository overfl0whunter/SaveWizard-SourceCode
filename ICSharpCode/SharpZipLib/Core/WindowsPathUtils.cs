using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A9 RID: 169
	public abstract class WindowsPathUtils
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal WindowsPathUtils()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002F9AC File Offset: 0x0002DBAC
		public static string DropPathRoot(string path)
		{
			string text = path;
			bool flag = path != null && path.Length > 0;
			if (flag)
			{
				bool flag2 = path[0] == '\\' || path[0] == '/';
				if (flag2)
				{
					bool flag3 = path.Length > 1 && (path[1] == '\\' || path[1] == '/');
					if (flag3)
					{
						int num = 2;
						int num2 = 2;
						while (num <= path.Length && ((path[num] != '\\' && path[num] != '/') || --num2 > 0))
						{
							num++;
						}
						num++;
						bool flag4 = num < path.Length;
						if (flag4)
						{
							text = path.Substring(num);
						}
						else
						{
							text = "";
						}
					}
				}
				else
				{
					bool flag5 = path.Length > 1 && path[1] == ':';
					if (flag5)
					{
						int num3 = 2;
						bool flag6 = path.Length > 2 && (path[2] == '\\' || path[2] == '/');
						if (flag6)
						{
							num3 = 3;
						}
						text = text.Remove(0, num3);
					}
				}
			}
			return text;
		}
	}
}
