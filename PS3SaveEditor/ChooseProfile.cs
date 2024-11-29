using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001C6 RID: 454
	public partial class ChooseProfile : Form
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00072F3F File Offset: 0x0007113F
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x00072F47 File Offset: 0x00071147
		public string SelectedAccount { get; set; }

		// Token: 0x06001737 RID: 5943 RVA: 0x00072F50 File Offset: 0x00071150
		public ChooseProfile(Dictionary<string, object> accounts, string curProfile)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = Resources.titleChooseProfile;
			this.btnSelect.Text = Resources.btnApplyPatch;
			this.btnCancel.Text = Resources.btnCancel;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			List<ProfileItem> list = new List<ProfileItem>();
			foreach (string text in accounts.Keys)
			{
				bool flag = text == curProfile;
				if (!flag)
				{
					list.Add(new ProfileItem
					{
						PSNID = text,
						Name = ((accounts[text] as Dictionary<string, object>)["friendly_name"] as string)
					});
				}
			}
			this.cbProfiles.DisplayMember = "Name";
			this.cbProfiles.ValueMember = "PSNID";
			this.cbProfiles.DataSource = list;
			bool flag2 = this.cbProfiles.Items.Count > 0;
			if (flag2)
			{
				this.cbProfiles.SelectedIndex = 0;
			}
			this.btnSelect.Click += this.btnSelect_Click;
			base.Load += this.ChooseProfile_Load;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000730E8 File Offset: 0x000712E8
		private void ChooseProfile_Load(object sender, EventArgs e)
		{
			bool flag = this.cbProfiles.Items.Count == 0;
			if (flag)
			{
				Util.ShowMessage(Resources.msgNoAltProfile);
				base.Close();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00073124 File Offset: 0x00071324
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00073190 File Offset: 0x00071390
		private void btnSelect_Click(object sender, EventArgs e)
		{
			bool flag = this.cbProfiles.SelectedValue != null;
			if (flag)
			{
				this.SelectedAccount = this.cbProfiles.SelectedValue as string;
			}
		}
	}
}
