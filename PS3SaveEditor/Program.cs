using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001DD RID: 477
	public static class Program
	{
		// Token: 0x060018AA RID: 6314 RVA: 0x0009367C File Offset: 0x0009187C
		[STAThread]
		public static void Main(string[] args)
		{
			bool flag = args.Length != 0;
			if (flag)
			{
				foreach (string text in args)
				{
					bool flag2 = text == "--version";
					if (flag2)
					{
						bool flag3 = Util.IsUnixOrMacOSX();
						if (flag3)
						{
							Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
						}
						else
						{
							Util.ShowMessage(Assembly.GetExecutingAssembly().GetName().Version.ToString());
						}
						return;
					}
				}
				Util.ProcedArguments(args);
			}
			SingleInstanceApplication singleInstanceApplication = new SingleInstanceApplication();
			singleInstanceApplication.StartupNextInstance += Program.OnAppStartupNextInstance;
			Program.mainForm = new MainForm3();
			bool flag4 = Util.IsUnixOrMacOSX() && Util.IsOldMono();
			if (flag4)
			{
				CustomMsgBox.Show(Resources.OldMonoMsg);
			}
			singleInstanceApplication.Run(Program.mainForm);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0009375C File Offset: 0x0009195C
		private static void OnAppStartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
			bool flag = Program.mainForm.WindowState == FormWindowState.Minimized;
			if (flag)
			{
				Program.mainForm.WindowState = FormWindowState.Normal;
			}
			Program.mainForm.Activate();
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000021C5 File Offset: 0x000003C5
		private static void CreateMacMenu()
		{
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000021C5 File Offset: 0x000003C5
		private static void Terminate()
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000021C5 File Offset: 0x000003C5
		private static void MaxOpenFiles()
		{
		}

		// Token: 0x04000C39 RID: 3129
		private static Form mainForm;

		// Token: 0x04000C3A RID: 3130
		public static string[] HTACCESS_USER = new string[] { "savewizard_1", "savewizard_1" };

		// Token: 0x04000C3B RID: 3131
		public static string[] HTACCESS_PWD = new string[] { "Wd2l#@vqjun)3K", "Wd2l#@vqjun)3K" };
	}
}
