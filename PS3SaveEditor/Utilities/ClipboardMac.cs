using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PS3SaveEditor.Utilities
{
	// Token: 0x020001EE RID: 494
	public static class ClipboardMac
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x000AC020 File Offset: 0x000AA220
		public static void CopyToClipboard(TextBox textBoxSource)
		{
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("pbcopy", "-pboard general -Prefer txt");
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = false;
					process.StartInfo.RedirectStandardInput = true;
					process.Start();
					process.StandardInput.Write(textBoxSource.SelectedText);
					process.StandardInput.Close();
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000AC0D4 File Offset: 0x000AA2D4
		public static void PasteFromClipboard(TextBox textBoxTarget)
		{
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("pbpaste", "-pboard general");
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.Start();
					string text = process.StandardOutput.ReadToEnd();
					textBoxTarget.Paste(text);
					process.StandardInput.Close();
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}
