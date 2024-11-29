using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PS3SaveEditor
{
	// Token: 0x020001E9 RID: 489
	public static class CurrentOS
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000A4349 File Offset: 0x000A2549
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000A4350 File Offset: 0x000A2550
		public static bool IsWindows { get; private set; } = Path.DirectorySeparatorChar == '\\';

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000A4358 File Offset: 0x000A2558
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x000A435F File Offset: 0x000A255F
		public static bool IsUnix { get; private set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000A4367 File Offset: 0x000A2567
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x000A436E File Offset: 0x000A256E
		public static bool IsMac { get; private set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000A4376 File Offset: 0x000A2576
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x000A437D File Offset: 0x000A257D
		public static bool IsLinux { get; private set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x000A4385 File Offset: 0x000A2585
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x000A438C File Offset: 0x000A258C
		public static bool IsUnknown { get; private set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x000A4394 File Offset: 0x000A2594
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x000A439B File Offset: 0x000A259B
		public static bool Is32bit { get; private set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000A43A3 File Offset: 0x000A25A3
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x000A43AA File Offset: 0x000A25AA
		public static bool Is64bit { get; private set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000A43B4 File Offset: 0x000A25B4
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000A43D0 File Offset: 0x000A25D0
		public static bool Is32BitProcess
		{
			get
			{
				return IntPtr.Size == 4;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x000A43EA File Offset: 0x000A25EA
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x000A43F1 File Offset: 0x000A25F1
		public static string Name { get; private set; }

		// Token: 0x060019ED RID: 6637
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000A43FC File Offset: 0x000A25FC
		private static bool Is64bitWindows
		{
			get
			{
				bool flag = (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6;
				if (flag)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						bool flag3;
						bool flag2 = !CurrentOS.IsWow64Process(currentProcess.Handle, out flag3);
						if (flag2)
						{
							return false;
						}
						return flag3;
					}
				}
				return false;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000A4494 File Offset: 0x000A2694
		static CurrentOS()
		{
			bool isWindows = CurrentOS.IsWindows;
			if (isWindows)
			{
				CurrentOS.Name = Environment.OSVersion.VersionString;
				CurrentOS.Name = CurrentOS.Name.Replace("Microsoft ", "");
				CurrentOS.Name = CurrentOS.Name.Replace("  ", " ");
				CurrentOS.Name = CurrentOS.Name.Replace(" )", ")");
				CurrentOS.Name = CurrentOS.Name.Trim();
				CurrentOS.Name = CurrentOS.Name.Replace("NT 6.2", "8 %bit 6.2");
				CurrentOS.Name = CurrentOS.Name.Replace("NT 6.1", "7 %bit 6.1");
				CurrentOS.Name = CurrentOS.Name.Replace("NT 6.0", "Vista %bit 6.0");
				CurrentOS.Name = CurrentOS.Name.Replace("NT 5.", "XP %bit 5.");
				CurrentOS.Name = CurrentOS.Name.Replace("%bit", CurrentOS.Is64bitWindows ? "64bit" : "32bit");
				bool is64bitWindows = CurrentOS.Is64bitWindows;
				if (is64bitWindows)
				{
					CurrentOS.Is64bit = true;
				}
				else
				{
					CurrentOS.Is32bit = true;
				}
			}
			else
			{
				string text = CurrentOS.ReadProcessOutput("uname");
				bool flag = text.Contains("Darwin");
				if (flag)
				{
					CurrentOS.IsUnix = true;
					CurrentOS.IsMac = true;
					CurrentOS.Name = "MacOS X " + CurrentOS.ReadProcessOutput("sw_vers", "-productVersion");
					CurrentOS.Name = CurrentOS.Name.Trim();
					string text2 = CurrentOS.ReadProcessOutput("uname", "-m");
					bool flag2 = text2.Contains("x86_64");
					if (flag2)
					{
						CurrentOS.Is64bit = true;
					}
					else
					{
						CurrentOS.Is32bit = true;
					}
					CurrentOS.Name = CurrentOS.Name + " " + (CurrentOS.Is32bit ? "32bit" : "64bit");
				}
				else
				{
					bool flag3 = text.Contains("Linux");
					if (flag3)
					{
						CurrentOS.IsUnix = true;
						CurrentOS.IsLinux = true;
						CurrentOS.Name = CurrentOS.ReadProcessOutput("lsb_release", "-d");
						CurrentOS.Name = CurrentOS.Name.Substring(CurrentOS.Name.IndexOf(":") + 1);
						CurrentOS.Name = CurrentOS.Name.Trim();
						string text3 = CurrentOS.ReadProcessOutput("uname", "-m");
						bool flag4 = text3.Contains("x86_64");
						if (flag4)
						{
							CurrentOS.Is64bit = true;
						}
						else
						{
							CurrentOS.Is32bit = true;
						}
						CurrentOS.Name = CurrentOS.Name + " " + (CurrentOS.Is32bit ? "32bit" : "64bit");
					}
					else
					{
						bool flag5 = text != "";
						if (flag5)
						{
							CurrentOS.IsUnix = true;
						}
						else
						{
							CurrentOS.IsUnknown = true;
						}
					}
				}
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000A4788 File Offset: 0x000A2988
		private static string ReadProcessOutput(string name)
		{
			return CurrentOS.ReadProcessOutput(name, null);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000A47A4 File Offset: 0x000A29A4
		private static string ReadProcessOutput(string name, string args)
		{
			string text2;
			try
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				bool flag = args != null && args != "";
				if (flag)
				{
					process.StartInfo.Arguments = " " + args;
				}
				process.StartInfo.FileName = name;
				process.Start();
				string text = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				bool flag2 = text == null;
				if (flag2)
				{
					text = "";
				}
				text = text.Trim();
				text2 = text;
			}
			catch
			{
				text2 = "";
			}
			return text2;
		}
	}
}
