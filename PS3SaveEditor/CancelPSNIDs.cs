using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001B1 RID: 433
	public partial class CancelPSNIDs : Form
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x0006EE98 File Offset: 0x0006D098
		public CancelPSNIDs(Dictionary<string, object> registered)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleCancelAccount;
			base.CenterToScreen();
			this.btnCancel.Text = Resources.btnCancellation;
			this.btnClose.Text = Resources.btnClose;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.dataGridView1.SelectionChanged += this.dataGridView1_SelectionChanged;
			this.dataGridView1.MultiSelect = false;
			this.btnCancel.Enabled = false;
			foreach (string text in registered.Keys)
			{
				Dictionary<string, object> dictionary = registered[text] as Dictionary<string, object>;
				int num = this.dataGridView1.Rows.Add();
				this.dataGridView1.Rows[num].Cells[0].Value = dictionary["friendly_name"];
				this.dataGridView1.Rows[num].Tag = text;
				this.dataGridView1.Rows[num].Cells[0].Tag = true;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006F02C File Offset: 0x0006D22C
		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			this.btnCancel.Enabled = false;
			foreach (object obj in this.dataGridView1.SelectedRows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = (bool)dataGridViewRow.Cells[0].Tag;
				if (flag)
				{
					this.btnCancel.Enabled = true;
					break;
				}
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0006F0C0 File Offset: 0x0006D2C0
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000021C5 File Offset: 0x000003C5
		private void CancelPSNIDs_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0006F12C File Offset: 0x0006D32C
		private void btnCancel_Click(object sender, EventArgs e)
		{
			bool flag = Util.ShowMessage(Resources.msgConfirmDeactivateAccount, Resources.warnTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
			bool flag2 = flag;
			if (flag2)
			{
				foreach (object obj in ((IEnumerable)this.dataGridView1.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					bool flag3 = dataGridViewRow.Selected && (bool)dataGridViewRow.Cells[0].Tag;
					if (flag3)
					{
						this.UnregisterPSNID((string)dataGridViewRow.Tag);
					}
				}
				base.DialogResult = DialogResult.Yes;
				base.Close();
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0006F1F4 File Offset: 0x0006D3F4
		private bool UnregisterPSNID(string psnId)
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Encoding = Encoding.UTF8;
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"UNREGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\"}}", Util.GetUserId(), psnId)));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			return dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00063981 File Offset: 0x00061B81
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x04000A59 RID: 2649
		private const string UNREGISTER_PSNID = "{{\"action\":\"UNREGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\"}}";
	}
}
