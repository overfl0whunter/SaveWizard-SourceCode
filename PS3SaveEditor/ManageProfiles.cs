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
	// Token: 0x020001DB RID: 475
	public partial class ManageProfiles : Form
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00091E3A File Offset: 0x0009003A
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x00091E42 File Offset: 0x00090042
		public string PsnIDResponse { get; set; }

		// Token: 0x0600188D RID: 6285 RVA: 0x00091E4C File Offset: 0x0009004C
		public ManageProfiles(string psnid, Dictionary<string, object> registered)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.m_registered = registered;
			this.btnClose.Text = Resources.btnClose;
			this.Text = Resources.titleManageProfiles;
			this.dgProfiles.Columns[0].HeaderText = Resources.colProfileName;
			this.dgProfiles.Font = this.Font;
			this.deleteToolStripMenuItem.Text = Resources.lblDeleteProfile;
			this.renameToolStripMenuItem.Text = Resources.mnuRenameProfile;
			base.CenterToScreen();
			this.btnClose.BackColor = SystemColors.ButtonFace;
			this.btnClose.ForeColor = Color.Black;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.dgProfiles.CellValidated += this.dgProfiles_CellValidated;
			this.dgProfiles.CurrentCellDirtyStateChanged += this.dgProfiles_CurrentCellDirtyStateChanged;
			this.dgProfiles.CellValueChanged += this.dgProfiles_CellValueChanged;
			this.dgProfiles.EditingControlShowing += this.dgProfiles_EditingControlShowing;
			this.dgProfiles.CellMouseDown += this.dgProfiles_CellMouseDown;
			this.m_newPSN_ID = psnid;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00091FC8 File Offset: 0x000901C8
		private void dgProfiles_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			e.Control.KeyPress -= this.Control_KeyPress;
			bool flag = this.dgProfiles.CurrentCell.ColumnIndex == 0;
			if (flag)
			{
				e.Control.KeyPress += this.Control_KeyPress;
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00092020 File Offset: 0x00090220
		private void Control_KeyPress(object sender, KeyPressEventArgs e)
		{
			((DataGridViewTextBoxEditingControl)sender).MaxLength = 32;
			bool flag = (((DataGridViewTextBoxEditingControl)sender).TextLength >= 32 && e.KeyChar != '\b') || e.KeyChar == '\u0016';
			if (flag)
			{
				e.KeyChar = '\0';
				e.Handled = true;
			}
			else
			{
				bool flag2 = e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '\\' || e.KeyChar == '%' || e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == ':' || e.KeyChar == ';' || e.KeyChar == '|' || e.KeyChar == '=' || e.KeyChar == ',' || e.KeyChar == '?' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '&';
				if (flag2)
				{
					e.KeyChar = '\0';
					e.Handled = true;
				}
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00092130 File Offset: 0x00090330
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool flag = base.ClientRectangle.Width == 0 || base.ClientRectangle.Height == 0;
			if (!flag)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
				}
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000921C8 File Offset: 0x000903C8
		private void dgProfiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right;
			if (flag)
			{
				this.dgProfiles.ClearSelection();
				this.dgProfiles.Rows[e.RowIndex].Selected = true;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00092228 File Offset: 0x00090428
		private void dgProfiles_MouseClick(object sender, MouseEventArgs e)
		{
			int rowIndex = this.dgProfiles.HitTest(e.X, e.Y).RowIndex;
			this.dgProfiles.ClearSelection();
			this.dgProfiles.Rows[rowIndex].Selected = true;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00092278 File Offset: 0x00090478
		private void dgProfiles_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			bool flag = e.Row.Index == 0;
			if (flag)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgProfiles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000021C5 File Offset: 0x000003C5
		private void dgProfiles_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000922A0 File Offset: 0x000904A0
		private void dgProfiles_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			bool isCurrentCellDirty = this.dgProfiles.IsCurrentCellDirty;
			if (isCurrentCellDirty)
			{
				this.dgProfiles.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
			bool flag = this.dgProfiles.CurrentCell.ColumnIndex == 2;
			if (flag)
			{
				foreach (object obj in ((IEnumerable)this.dgProfiles.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.Cells[2].Value = false;
				}
			}
			bool flag2 = this.dgProfiles.CurrentCell.ColumnIndex == 2;
			if (flag2)
			{
				foreach (object obj2 in ((IEnumerable)this.dgProfiles.Rows))
				{
					DataGridViewRow dataGridViewRow2 = (DataGridViewRow)obj2;
					bool flag3 = dataGridViewRow2.Index == this.dgProfiles.CurrentCell.RowIndex;
					if (flag3)
					{
						dataGridViewRow2.Cells[2].Value = true;
					}
				}
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000923FC File Offset: 0x000905FC
		private DateTime TimeStampToDateTime(double unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00092434 File Offset: 0x00090634
		private void ManageProfiles_Load(object sender, EventArgs e)
		{
			foreach (string text in this.m_registered.Keys)
			{
				Dictionary<string, object> dictionary = this.m_registered[text] as Dictionary<string, object>;
				bool flag = !dictionary.ContainsKey("friendly_name") || !dictionary.ContainsKey("registration_ts");
				if (!flag)
				{
					int num = this.dgProfiles.Rows.Add();
					this.dgProfiles.Rows[num].Cells[0].Value = dictionary["friendly_name"];
					this.dgProfiles.Rows[num].Cells[1].Value = this.TimeStampToDateTime(Convert.ToDouble(dictionary["registration_ts"])).AddDays(30.0).ToString("dd/MM/yyyy");
					this.dgProfiles.Rows[num].Cells[2].Value = text;
					this.dgProfiles.Rows[num].Tag = dictionary.ContainsKey("replaceable") && (bool)dictionary["replaceable"];
					this.dgProfiles.Rows[num].Cells[1].Style.ForeColor = ((!(bool)this.dgProfiles.Rows[num].Tag) ? Color.Red : Color.Green);
				}
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_newPSN_ID);
			if (flag2)
			{
				int num2 = this.dgProfiles.Rows.Add();
				this.dgProfiles.Rows[num2].Cells[0].Value = "Enter Name";
				this.dgProfiles.Rows[num2].Cells[1].Value = DateTime.Now.AddDays(30.0).ToString("dd/MM/yyyy");
				this.dgProfiles.Rows[num2].Cells[1].Style.ForeColor = Color.Red;
				this.dgProfiles.Rows[num2].Cells[2].Value = this.m_newPSN_ID;
				this.dgProfiles.CurrentCell = this.dgProfiles.Rows[num2].Cells[0];
				this.dgProfiles.BeginEdit(true);
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00092744 File Offset: 0x00090944
		private int CheckExistingKey(byte[] key)
		{
			foreach (object obj in ((IEnumerable)this.dgProfiles.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				bool flag = dataGridViewRow.Tag.ToString() == Convert.ToBase64String(key);
				if (flag)
				{
					return dataGridViewRow.Index;
				}
			}
			for (int i = 0; i < key.Length; i++)
			{
				bool flag2 = key[i] > 0;
				if (flag2)
				{
					return -2;
				}
			}
			return -1;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00063981 File Offset: 0x00061B81
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000927F8 File Offset: 0x000909F8
		private void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = !this.ValidateProfiles();
			if (flag)
			{
				Util.ShowMessage(Resources.errDuplicateProfile);
			}
			else
			{
				foreach (object obj in ((IEnumerable)this.dgProfiles.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					string text = (string)dataGridViewRow.Cells[2].Value;
					string text2 = (string)dataGridViewRow.Cells[0].Value;
					bool flag2 = text2.Trim().Length == 0 || text2.Trim() == "Enter Name";
					if (flag2)
					{
						this.dgProfiles.CurrentCell = dataGridViewRow.Cells[0];
						Util.ShowMessage("Please enter valid name for the profile.");
						return;
					}
					bool flag3 = this.m_registered.ContainsKey(text);
					if (flag3)
					{
						Dictionary<string, object> dictionary = this.m_registered[text] as Dictionary<string, object>;
						bool flag4 = (string)dictionary["friendly_name"] != text2;
						if (flag4)
						{
							this.RenamePSNID(text, text2);
						}
					}
					else
					{
						bool flag5 = !this.RegisterPSNID(text, text2);
						if (flag5)
						{
							Util.ShowMessage("Error occurred while updating PSN ID " + text);
						}
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00092990 File Offset: 0x00090B90
		private bool RegisterPSNID(string psnId, string name)
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			webClientEx.Encoding = Encoding.UTF8;
			byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"REGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}", Util.GetUserId(), psnId.Trim(), name.Trim())));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			bool flag2;
			if (flag)
			{
				this.PsnIDResponse = @string;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00092A78 File Offset: 0x00090C78
		private bool UnregisterPSNID(string psnId)
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Encoding = Encoding.UTF8;
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"UNREGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\"}}", Util.GetUserId(), psnId)));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			bool flag2;
			if (flag)
			{
				this.PsnIDResponse = @string;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00092B54 File Offset: 0x00090D54
		private bool ValidateProfiles()
		{
			for (int i = 0; i < this.dgProfiles.Rows.Count; i++)
			{
				for (int j = i + 1; j < this.dgProfiles.Rows.Count; j++)
				{
					bool flag = this.dgProfiles.Rows[i].Cells[0].Value.ToString() == this.dgProfiles.Rows[j].Cells[0].Value.ToString();
					if (flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00092C10 File Offset: 0x00090E10
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgProfiles.SelectedRows.Count == 1;
			if (flag)
			{
				bool flag2 = this.UnregisterPSNID((string)this.dgProfiles.SelectedRows[0].Cells[2].Value);
				if (flag2)
				{
					this.dgProfiles.Rows.Remove(this.dgProfiles.SelectedRows[0]);
				}
				else
				{
					Util.ShowMessage("Can not unregister PSN ID");
				}
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00092C98 File Offset: 0x00090E98
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.dgProfiles.SelectedRows.Count == 1;
			if (flag)
			{
				this.dgProfiles.CurrentCell = this.dgProfiles.SelectedRows[0].Cells[0];
				this.dgProfiles.BeginEdit(true);
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00092CF4 File Offset: 0x00090EF4
		private bool RenamePSNID(string psnId, string name)
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Encoding = Encoding.UTF8;
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetAuthBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"RENAME_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}", Util.GetUserId(), psnId, name)));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			this.PsnIDResponse = @string;
			return true;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00092D9C File Offset: 0x00090F9C
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool flag = this.dgProfiles.SelectedRows.Count != 1;
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				bool flag2 = !(bool)this.dgProfiles.SelectedRows[0].Tag;
				if (flag2)
				{
					this.deleteToolStripMenuItem.Enabled = false;
				}
				else
				{
					this.deleteToolStripMenuItem.Enabled = true;
				}
			}
		}

		// Token: 0x04000C28 RID: 3112
		private string m_newPSN_ID;

		// Token: 0x04000C29 RID: 3113
		private Dictionary<string, object> m_registered = null;

		// Token: 0x04000C2A RID: 3114
		private const string REGISTER_PSNID = "{{\"action\":\"REGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}";

		// Token: 0x04000C2B RID: 3115
		private const string UNREGISTER_PSNID = "{{\"action\":\"UNREGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\"}}";

		// Token: 0x04000C2C RID: 3116
		private const string RENAME_PSNID = "{{\"action\":\"RENAME_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}";
	}
}
