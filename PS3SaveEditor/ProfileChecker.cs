using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001D1 RID: 465
	public partial class ProfileChecker : Form
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x0008D188 File Offset: 0x0008B388
		public ProfileChecker(int regMode = 0, string psn = null, string drive = null)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.CenterToScreen();
			this.txtProfileName.MaxLength = 32;
			this.Text = string.Format(Resources.titlePSNAdd, Util.PRODUCT_NAME);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblTitle1.Text = Resources.lblPSNAddTitle;
			this.lblInstructions.Text = Resources.lblInstructionsPage1;
			this.lblInstruction1.Text = Resources.lblInstruction1;
			this.lblInstrucionRed.Text = Resources.lblInstruction1Red;
			this.lblInstruction2.Text = Resources.lblInstruction_2;
			this.lblInstruciton3.Text = Resources.lblInstruction3;
			this.lblInstructionPage1.Text = Resources.lblPage1;
			this.btnNext.Text = Resources.btnPage1;
			this.lblPageTitle.Text = Resources.lblPSNAddTitle;
			this.lblInstructionPage2.Text = Resources.lblPage2;
			this.lblUserName.Text = Resources.lblUserName;
			this.lblInstruction2Page2.Text = Resources.lblPage21;
			this.lblFooter2.Text = Resources.lblInstructionPage2;
			this.panelProfileName.Visible = false;
			this.panelFinish.Visible = false;
			this.lblTitleFinish.Text = Resources.titlePSNAdd;
			this.lblFinish.Text = Resources.lblInstuctionPage3;
			Control.CheckForIllegalCrossThreadCalls = false;
			this.btnNext.Click += this.btnNext_Click;
			this.txtProfileName.TextChanged += this.txtProfileName_TextChanged;
			base.DialogResult = DialogResult.Cancel;
			this.m_registerMode = regMode;
			bool flag = regMode > 0;
			if (flag)
			{
				bool flag2 = regMode == 1;
				if (flag2)
				{
					this.panelInstructions.Visible = false;
					this.panelProfileName.Visible = true;
					this.btnNext.Enabled = false;
					this.btnNext.Text = Resources.btnPage2;
				}
				this.psnId = psn;
				this.lblDriveLetter.Text = string.Format(Resources.lblDrive, drive);
			}
			else
			{
				base.Load += this.ProfileChecker_Load;
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0008D3F8 File Offset: 0x0008B5F8
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0008D464 File Offset: 0x0008B664
		private void txtProfileName_TextChanged(object sender, EventArgs e)
		{
			this.txtProfileName.Text = this.txtProfileName.Text.Trim(new char[]
			{
				'.', '"', '/', '\\', '[', ']', ':', ';', '|', '=',
				',', '?', '%', '<', '>', '&'
			});
			this.txtProfileName.SelectionStart = this.txtProfileName.Text.Length;
			this.btnNext.Enabled = this.txtProfileName.Text.Length > 0;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0008D4DC File Offset: 0x0008B6DC
		private void btnNext_Click(object sender, EventArgs e)
		{
			bool visible = this.panelInstructions.Visible;
			if (visible)
			{
				this.panelInstructions.Visible = false;
				this.panelProfileName.Visible = true;
				this.btnNext.Enabled = false;
				this.btnNext.Text = Resources.btnPage2;
			}
			else
			{
				bool visible2 = this.panelProfileName.Visible;
				if (visible2)
				{
					int num;
					bool flag = this.RegisterPSNID(this.psnId, this.txtProfileName.Text, out num);
					if (flag)
					{
						bool flag2 = this.m_registerMode > 0;
						if (flag2)
						{
							base.DialogResult = DialogResult.OK;
							base.Close();
						}
						else
						{
							this.panelProfileName.Visible = false;
							this.panelFinish.Visible = true;
							this.btnNext.Text = Resources.btnOK;
						}
					}
					else
					{
						bool flag3 = num == 4121;
						if (flag3)
						{
							Util.ShowMessage(Resources.errPSNNameUsed, Resources.msgError);
						}
						else
						{
							Util.ShowMessage("Error occurred while registering PSN ID.");
						}
					}
				}
				else
				{
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0008D5FC File Offset: 0x0008B7FC
		private bool RegisterPSNID(string psnId, string name, out int errorCode)
		{
			errorCode = 0;
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			webClientEx.Encoding = Encoding.UTF8;
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.UTF8.GetBytes(string.Format("{{\"action\":\"REGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}", Util.GetUserId(), psnId.Trim(), name.Trim())));
			string @string = Encoding.UTF8.GetString(array);
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
			bool flag = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = dictionary.ContainsKey("code");
				if (flag3)
				{
					errorCode = Convert.ToInt32(dictionary["code"]);
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0008D700 File Offset: 0x0008B900
		private void ProfileChecker_Load(object sender, EventArgs e)
		{
			this.btnNext.Enabled = false;
			this.CheckDrives();
			bool flag = Util.IsUnixOrMacOSX() && !this.btnNext.Enabled;
			if (flag)
			{
				global::System.Timers.Timer timer = new global::System.Timers.Timer();
				this.previousDriveNum = DriveInfo.GetDrives().Length;
				timer.Elapsed += delegate(object s, ElapsedEventArgs e2)
				{
					DriveInfo[] drives = DriveInfo.GetDrives();
					bool flag2 = this.previousDriveNum != drives.Length;
					if (flag2)
					{
						this.previousDriveNum = drives.Length;
						this.CheckDrives();
					}
				};
				timer.Interval = 10000.0;
				timer.Enabled = true;
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008D780 File Offset: 0x0008B980
		private void CheckDrives()
		{
			this.btnNext.Enabled = false;
			this.lblDriveLetter.Text = string.Format(Resources.lblDrive, "---");
			DriveInfo[] drives = DriveInfo.GetDrives();
			DriveInfo[] array = drives;
			int i = 0;
			while (i < array.Length)
			{
				DriveInfo driveInfo = array[i];
				bool flag = Util.IsUnixOrMacOSX();
				bool flag2;
				if (flag)
				{
					flag2 = (driveInfo.Name.Contains("media") || driveInfo.Name.Contains("Volumes")) && Directory.Exists(driveInfo + "/PS4");
					flag2 = driveInfo.IsReady && (driveInfo.DriveType == DriveType.Removable || flag2);
				}
				else
				{
					flag2 = driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					bool flag4 = Util.IsPathToCheats(driveInfo.Name);
					if (flag4)
					{
						string text = Path.Combine(driveInfo.Name, "PS4", "SAVEDATA");
						string text2 = Directory.GetDirectories(text)[0];
						this.psnId = Path.GetFileName(text2);
						long num = 0L;
						bool flag5 = !long.TryParse(this.psnId, NumberStyles.HexNumber, null, out num);
						if (!flag5)
						{
							this.btnNext.Enabled = true;
							this.lblDriveLetter.Text = string.Format(Resources.lblDrive, driveInfo.RootDirectory.Name);
							break;
						}
					}
				}
				IL_0162:
				i++;
				continue;
				goto IL_0162;
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0008D8FC File Offset: 0x0008BAFC
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 537;
			if (flag)
			{
				Thread thread = new Thread(new ThreadStart(this.CheckDrives));
				thread.Start();
			}
			base.WndProc(ref m);
		}

		// Token: 0x04000BC2 RID: 3010
		private string psnId;

		// Token: 0x04000BC3 RID: 3011
		private int m_registerMode = 0;

		// Token: 0x04000BC4 RID: 3012
		private int previousDriveNum = 0;

		// Token: 0x04000BC5 RID: 3013
		private const string REGISTER_PSNID = "{{\"action\":\"REGISTER_PSNID\",\"userid\":\"{0}\",\"psnid\":\"{1}\",\"friendly_name\":\"{2}\"}}";
	}
}
