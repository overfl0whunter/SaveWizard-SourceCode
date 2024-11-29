using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D3 RID: 467
	public partial class ResignInfo : Form
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x0008E970 File Offset: 0x0008CB70
		public ResignInfo()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.CenterToScreen();
			base.Load += this.ResignInfo_Load;
			this.textBox1.Text = Resources.descResign;
			this.Text = Resources.titleResignMessage;
			this.chkDontShow.Text = Resources.chkDontShowResign;
			this.btnOk.Text = Resources.btnOK;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0008E9FE File Offset: 0x0008CBFE
		private void ResignInfo_Load(object sender, EventArgs e)
		{
			this.btnOk.Focus();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0008EA10 File Offset: 0x0008CC10
		private void btnOk_Click(object sender, EventArgs e)
		{
			bool @checked = this.chkDontShow.Checked;
			if (@checked)
			{
				Util.SetRegistryValue("NoResignMessage", "yes");
			}
			base.Close();
		}
	}
}
