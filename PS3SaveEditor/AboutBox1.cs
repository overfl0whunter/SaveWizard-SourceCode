using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001AA RID: 426
	internal partial class AboutBox1 : Form
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x00067780 File Offset: 0x00065980
		public AboutBox1()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.Text = string.Format("About {0}", this.AssemblyTitle);
			this.pictureBox1.Image = Resources.ps3se1;
			this.lblDesc.Visible = false;
			this.linkLabel1.Text = (Util.IsHyperkin() ? "http://www.thesavewizard.com" : "http://www.savewizard.net/");
			this.lblVersion.Text = string.Format("Version {0}", AboutBox1.AssemblyVersion);
			this.osVersion.Text = Util.GetOSVersion();
			this.frameworkVersion.Text = Util.GetFramework();
			this.lblCopyright.Text = this.AssemblyCopyright;
			this.lblDesc.Text = this.AssemblyCompany + ((Util.CURRENT_SERVER == 0) ? "" : ".");
			this.btnOk.Text = Resources.btnOK;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00067894 File Offset: 0x00065A94
		public string AssemblyTitle
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				bool flag = customAttributes.Length != 0;
				if (flag)
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
					bool flag2 = assemblyTitleAttribute.Title != "";
					if (flag2)
					{
						return assemblyTitleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00067900 File Offset: 0x00065B00
		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00067928 File Offset: 0x00065B28
		public string AssemblyDescription
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				bool flag = customAttributes.Length == 0;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					text = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
				}
				return text;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00067970 File Offset: 0x00065B70
		public string AssemblyProduct
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				bool flag = customAttributes.Length == 0;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					text = ((AssemblyProductAttribute)customAttributes[0]).Product;
				}
				return text;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000679B8 File Offset: 0x00065BB8
		public string AssemblyCopyright
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				bool flag = customAttributes.Length == 0;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					text = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
				}
				return text;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00067A00 File Offset: 0x00065C00
		public string AssemblyCompany
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				bool flag = customAttributes.Length == 0;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					text = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
				}
				return text;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00067A47 File Offset: 0x00065C47
		private void btnOk_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00067A54 File Offset: 0x00065C54
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo(this.linkLabel1.Text);
			Process.Start(processStartInfo);
		}
	}
}
