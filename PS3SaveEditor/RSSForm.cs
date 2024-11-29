using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3SaveEditor.Resources;
using Rss;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace PS3SaveEditor
{
	// Token: 0x020001E0 RID: 480
	public partial class RSSForm : Form
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x00094314 File Offset: 0x00092514
		public RSSForm(RssChannel channel)
		{
			string registryValue = Util.GetRegistryValue("Language");
			bool flag = registryValue != null;
			if (flag)
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(registryValue);
			}
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lstRSSFeeds.DrawMode = DrawMode.OwnerDrawFixed;
			this.lstRSSFeeds.DrawItem += this.lstRSSFeeds_DrawItem;
			base.CenterToScreen();
			this.Text = Util.PRODUCT_NAME;
			base.Load += this.RSSForm_Load;
			this.btnOk.Text = Resources.btnOK;
			this.btnOk.Click += this.btnOk_Click;
			base.LostFocus += this.RSSForm_LostFocus;
			this.lstRSSFeeds.SelectedIndexChanged += this.lstRSSFeeds_SelectedIndexChanged;
			this.lnkTitle.LinkClicked += this.lnkTitle_LinkClicked;
			bool flag2 = Util.CurrentPlatform == Util.Platform.Linux;
			if (flag2)
			{
				this.htmlPanel1.Scroll += this.HtmlPanel_Scroll;
			}
			try
			{
				bool flag3 = channel.Items.Count > 0;
				if (flag3)
				{
					this.lstRSSFeeds.DataSource = channel.Items;
					this.lstRSSFeeds.Refresh();
				}
				else
				{
					this.lstRSSFeeds.DataSource = null;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000944DC File Offset: 0x000926DC
		private void lstRSSFeeds_DrawItem(object sender, DrawItemEventArgs e)
		{
			bool flag = e.Index < 0;
			if (!flag)
			{
				e.DrawBackground();
				bool flag2 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag2)
				{
					e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected, e.ForeColor, Color.FromArgb(0, 175, 255));
					e.Graphics.DrawString(this.lstRSSFeeds.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
				}
				else
				{
					e.Graphics.DrawString(this.lstRSSFeeds.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
				}
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000945E4 File Offset: 0x000927E4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00094650 File Offset: 0x00092850
		private void lnkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo(this.lnkTitle.Links[0].LinkData as string);
			Process.Start(processStartInfo);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00094686 File Offset: 0x00092886
		private void RSSForm_LostFocus(object sender, EventArgs e)
		{
			base.Focus();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00094690 File Offset: 0x00092890
		private void lstRSSFeeds_SelectedIndexChanged(object sender, EventArgs e)
		{
			RssItem rssItem = (RssItem)this.lstRSSFeeds.SelectedItem;
			bool flag = rssItem.Link != null;
			if (flag)
			{
				this.lnkTitle.Text = rssItem.Title;
				this.lnkTitle.Links.Clear();
				this.lnkTitle.Links.Add(0, rssItem.Title.Length, rssItem.Link.ToString());
				this.lnkTitle.Visible = true;
				this.lblTitle.Visible = false;
			}
			else
			{
				this.lblTitle.Text = rssItem.Title;
				this.lnkTitle.Visible = false;
				this.lblTitle.Visible = true;
			}
			string text = Util.ScaleSize(14) + "px";
			this.htmlPanel1.Text = string.Concat(new string[]
			{
				"<style>*{font-family: '",
				Util.GetFontFamily(),
				"'font-size:",
				text,
				";color:#000;} body{padding:4px;} p,div{margin:0px;}</style><body>",
				rssItem.Description,
				" </body>"
			});
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnOk_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000947BC File Offset: 0x000929BC
		private void RSSForm_Load(object sender, EventArgs e)
		{
			bool flag = this.lstRSSFeeds.DataSource == null;
			if (flag)
			{
				base.Close();
			}
			else
			{
				base.Show();
				bool flag2 = base.WindowState == FormWindowState.Minimized;
				if (flag2)
				{
					base.WindowState = FormWindowState.Normal;
				}
				base.Activate();
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000021C5 File Offset: 0x000003C5
		private void RSSForm_ResizeEnd(object sender, EventArgs e)
		{
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0009480C File Offset: 0x00092A0C
		private async void HtmlPanel_Scroll(object sender, ScrollEventArgs e)
		{
			await Task.Delay(20);
			this.htmlPanel1.ClearSelection();
		}
	}
}
