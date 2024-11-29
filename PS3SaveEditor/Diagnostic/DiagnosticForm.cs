using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using PS3SaveEditor.Resources;
using PS3SaveEditor.SubControls;

namespace PS3SaveEditor.Diagnostic
{
	// Token: 0x020001F6 RID: 502
	public partial class DiagnosticForm : Form
	{
		// Token: 0x06001BE5 RID: 7141 RVA: 0x000B1340 File Offset: 0x000AF540
		public DiagnosticForm()
		{
			this.Font = Util.GetFontForPlatform(this.Font);
			this.InitializeComponent();
			this.FillDiagnosticInfo();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000B1374 File Offset: 0x000AF574
		private void FillDiagnosticInfo()
		{
			string text = string.Format("App version - {0}\r\nOS version - {1}\r\nFramework - {2}\r\nProduct version - {3}", new object[]
			{
				Assembly.GetExecutingAssembly().GetName().Version.ToString(),
				Util.GetOSVersion(),
				Util.GetFramework(),
				Util.pid
			});
			this.infoBox.Text = text;
		}
	}
}
