using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D4 RID: 468
	public partial class ResignMessage : Form
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0008EDF1 File Offset: 0x0008CFF1
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0008EDF9 File Offset: 0x0008CFF9
		public bool DeleteExisting { get; set; }

		// Token: 0x06001835 RID: 6197 RVA: 0x0008EE04 File Offset: 0x0008D004
		public ResignMessage()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleResignInfo;
			this.lblResignSuccess.Text = Resources.lblResignSuccess;
			this.chkDeleteExisting.Text = Resources.chkDeleteExisting;
			this.btnOK.Text = Resources.btnOK;
			base.CenterToScreen();
			this.btnOK.Click += this.btnOK_Click;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0008EE97 File Offset: 0x0008D097
		public ResignMessage(bool showDelete)
			: this()
		{
			this.chkDeleteExisting.Visible = showDelete;
			this.chkDeleteExisting.Checked = showDelete;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0008EEBB File Offset: 0x0008D0BB
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DeleteExisting = this.chkDeleteExisting.Checked;
			base.Close();
		}
	}
}
