using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001CD RID: 461
	public partial class LinkMessageBox : Form
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x00075550 File Offset: 0x00073750
		public LinkMessageBox(string message, string linkUrl)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.m_url = linkUrl;
			this.Text = Util.PRODUCT_NAME;
			bool flag = !string.IsNullOrEmpty(linkUrl);
			if (flag)
			{
				this.linkLabel1.Click += this.linkLabel1_Click;
			}
			else
			{
				this.linkLabel1.Visible = false;
			}
			this.lblMessage.Text = message;
			this.linkLabel1.Text = Resources.lnkContactSupport;
			this.btnOK.Text = Resources.btnOK;
			this.btnOK.Click += this.btnOK_Click;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnOK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00075613 File Offset: 0x00073813
		private void linkLabel1_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				Verb = "open",
				FileName = this.m_url,
				UseShellExecute = true
			});
		}

		// Token: 0x04000ADA RID: 2778
		private string m_url;
	}
}
