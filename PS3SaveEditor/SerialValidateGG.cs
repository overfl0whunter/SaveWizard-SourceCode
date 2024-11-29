using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Win32;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E2 RID: 482
	public partial class SerialValidateGG : Form
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x00098AC8 File Offset: 0x00096CC8
		public SerialValidateGG()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.btnCancel.Text = Resources.btnCancel;
			this.btnOk.Text = Resources.btnOK;
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.panel1.BackColor = Color.FromArgb(200, 100, 10);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblInstruction.BackColor = Color.Transparent;
			this.lblInstruction.TextAlign = ContentAlignment.MiddleCenter;
			this.lblInstruction2.BackColor = Color.Transparent;
			this.label1.BackColor = (this.label2.BackColor = (this.label3.BackColor = (this.label4.BackColor = Color.Transparent)));
			this.UpdateStatus = new SerialValidateGG.UpdateStatusDelegate(this.UpdateStatusSafe);
			this.CloseForm = new SerialValidateGG.CloseDelegate(this.CloseFormSafe);
			this.EnableOk = new SerialValidateGG.EnableOkDelegate(this.EnableOkSafe);
			bool flag = Util.IsUnixOrMacOSX();
			if (flag)
			{
				bool flag2 = base.WindowState == FormWindowState.Minimized;
				if (flag2)
				{
					base.WindowState = FormWindowState.Normal;
				}
				base.Activate();
			}
			else
			{
				Util.SetForegroundWindow(base.Handle);
			}
			base.CenterToScreen();
			this.txtSerial1.TextChanged += this.txtSerial_TextChanged;
			this.txtSerial1.KeyDown += this.txtSerial_KeyDown;
			this.txtSerial1.KeyPress += this.txtSerial_KeyPress;
			this.txtSerial2.TextChanged += this.txtSerial_TextChanged;
			this.txtSerial2.KeyDown += this.txtSerial_KeyDown;
			this.txtSerial2.KeyPress += this.txtSerial_KeyPress;
			this.txtSerial3.TextChanged += this.txtSerial_TextChanged;
			this.txtSerial3.KeyDown += this.txtSerial_KeyDown;
			this.txtSerial3.KeyPress += this.txtSerial_KeyPress;
			this.txtSerial4.TextChanged += this.txtSerial_TextChanged;
			this.txtSerial4.KeyDown += this.txtSerial_KeyDown;
			this.txtSerial4.KeyPress += this.txtSerial_KeyPress;
			this.Text = string.Format(Resources.titleSerialEntry, Util.PRODUCT_NAME);
			this.lblInstruction2.Text = Resources.lblInstruction2;
			this.lblInstruction.Text = "";
			this.lblInstruction2.Text = Resources.lblEnterSerial;
			this.lnkLicSupport.BackColor = (this.lblLicHelp.BackColor = Color.Transparent);
			this.lblLicHelp.Text = Resources.lblLicHelp;
			this.lnkLicSupport.LinkClicked += this.linkLblSupport_LinkClicked;
			base.Load += this.SerialValidateGG_Load;
			this.btnOk.Enabled = false;
			bool flag3 = this.m_serial == null;
			if (flag3)
			{
				this.label1.Text = "";
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00098E60 File Offset: 0x00097060
		private void linkLblSupport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo("http://" + this.lnkLicSupport.Text);
			Process.Start(processStartInfo);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00098E90 File Offset: 0x00097090
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00098EFC File Offset: 0x000970FC
		private void CheckForDevice()
		{
			Thread.Sleep(10000);
			this.m_serial = null;
			this.FindGGUSB();
			bool flag = this.m_serial != null;
			if (flag)
			{
				bool isHandleCreated = this.label1.IsHandleCreated;
				if (isHandleCreated)
				{
					this.label1.Invoke(this.UpdateStatus, new object[] { "Please wait. Registering Game Genie Save Editor for PS3." });
				}
				this.RegisterSerial();
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00098F68 File Offset: 0x00097168
		private void UpdateStatusSafe(string status)
		{
			this.label1.Text = status;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00098F78 File Offset: 0x00097178
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 537;
			if (flag)
			{
				bool flag2 = m.WParam.ToInt32() == 32768;
				if (flag2)
				{
					bool flag3 = m.LParam != IntPtr.Zero;
					if (flag3)
					{
					}
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00098FD4 File Offset: 0x000971D4
		private void SerialValidateGG_Load(object sender, EventArgs e)
		{
			this.txtSerial1.Select();
			bool flag = this.m_serial != null;
			if (flag)
			{
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00099000 File Offset: 0x00097200
		private void RegisterSerial()
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				string serial = this.m_serial;
				this.m_hash = SerialValidateGG.ComputeHash(serial);
				string uid = Util.GetUID(false, true);
				bool flag = string.IsNullOrEmpty(uid);
				if (flag)
				{
					Util.ShowMessage("There appears to have been an issue activating. Please contact support.");
				}
				else
				{
					string text = string.Format("{0}/ps4auth", Util.GetBaseUrl(), this.m_hash);
					webClientEx.DownloadStringAsync(new Uri(text, UriKind.Absolute));
					webClientEx.DownloadStringCompleted += this.client_DownloadStringCompleted;
				}
			}
			catch (Exception ex)
			{
				Util.ShowMessage(ex.Message, ex.StackTrace);
				Util.ShowMessage(Resources.errSerial, Resources.msgError);
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000990CC File Offset: 0x000972CC
		public static string ComputeHash(string serial)
		{
			string text = "";
			byte[] array = new byte[32];
			byte[] array2 = new byte[16];
			byte[] array3 = new byte[]
			{
				59, 67, 235, 54, 183, 124, 22, 65, 172, 154,
				31, 14, 188, 91, 48, 41
			};
			long num = long.Parse(serial, NumberStyles.HexNumber);
			byte[] array4 = null;
			bool flag = serial.Length == 16;
			if (flag)
			{
				byte[] bytes = BitConverter.GetBytes(num);
				Array.Reverse(bytes, 0, bytes.Length);
				Array.Copy(bytes, array2, bytes.Length);
				for (int i = 0; i < 8; i++)
				{
					array[i] = array2[i] ^ array3[i];
				}
				Array.Copy(Encoding.ASCII.GetBytes("GameGenie"), 0, array, 8, "GameGenie".Length);
				array4 = SHA1.Create().ComputeHash(array, 0, 8 + "GameGenie".Length);
			}
			else
			{
				bool flag2 = serial.Length == 20;
				if (flag2)
				{
					byte[] bytes2 = BitConverter.GetBytes(num);
					Array.Reverse(bytes2, 0, bytes2.Length);
					Array.Copy(bytes2, 0, array2, 4, bytes2.Length);
					for (int j = 0; j < 12; j++)
					{
						array[j] = array2[j] ^ array3[j];
					}
					Array.Copy(Encoding.ASCII.GetBytes("GameGenie"), 0, array, 12, "GameGenie".Length);
					array4 = SHA1.Create().ComputeHash(array, 0, 12 + "GameGenie".Length);
				}
			}
			bool flag3 = array4 != null;
			if (flag3)
			{
				for (int k = 0; k < array4.Length; k++)
				{
					text += array4[k].ToString("X2");
				}
			}
			return text;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00099298 File Offset: 0x00097498
		private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			bool flag = e.Error != null;
			if (flag)
			{
				Util.ShowMessage(Resources.errSerial, Resources.msgError);
				base.Invoke(this.CloseForm, new object[] { false });
			}
			else
			{
				string result = e.Result;
				bool flag2 = result == null;
				if (flag2)
				{
					Util.ShowMessage(Resources.errInvalidSerial, Resources.msgError);
					base.Invoke(this.CloseForm, new object[] { false });
				}
				else
				{
					bool flag3 = result.IndexOf('#') > 0;
					if (flag3)
					{
						string[] array = result.Split(new char[] { '#' });
						bool flag4 = array.Length > 1;
						if (flag4)
						{
							bool flag5 = array[0] == "4";
							if (flag5)
							{
								Util.ShowMessage(Resources.errInvalidSerial, Resources.msgError);
								base.Invoke(this.CloseForm, new object[] { false });
								return;
							}
							bool flag6 = array[0] == "5";
							if (flag6)
							{
								Util.ShowMessage(Resources.errTooManyTimes, Resources.msgError);
								base.Invoke(this.CloseForm, new object[] { false });
								return;
							}
						}
					}
					else
					{
						bool flag7 = result.ToLower() == "toomanytimes" || result.ToLower().Contains("too many");
						if (flag7)
						{
							Util.ShowMessage(Resources.errTooManyTimes, Resources.msgError);
							base.Invoke(this.CloseForm, new object[] { false });
							return;
						}
						bool flag8 = result == null || result.ToLower().Contains("error") || result.ToLower().Contains("not found");
						if (flag8)
						{
							string text = result.Replace("ERROR", "");
							bool flag9 = !text.Contains("1002");
							if (flag9)
							{
								bool flag10 = text.Contains("1014");
								if (flag10)
								{
									Util.ShowMessage(Resources.errOffline, Resources.msgInfo);
									base.Invoke(this.CloseForm, new object[] { false });
									return;
								}
								bool flag11 = text.Contains("1005");
								if (flag11)
								{
									Util.ShowMessage(Resources.errTooManyTimes + text, Resources.msgError);
									base.Invoke(this.CloseForm, new object[] { false });
									return;
								}
								bool flag12 = text.Contains("1007");
								if (flag12)
								{
									Util.GetUID(true, true);
									this.RegisterSerial();
								}
								else
								{
									bool flag13 = this.m_serial == null;
									if (flag13)
									{
										Util.ShowMessage(Resources.errInvalidSerial + text, Resources.msgError);
									}
									else
									{
										Util.ShowMessage(Resources.errInvalidUSB + text, Resources.msgError);
									}
									this.m_retryCount++;
									bool flag14 = this.m_retryCount >= 3;
									if (flag14)
									{
										base.Invoke(this.CloseForm, new object[] { false });
										return;
									}
									bool flag15 = this.m_serial == null;
									if (flag15)
									{
										this.btnOk.Invoke(this.EnableOk, new object[] { true });
									}
									else
									{
										this.btnOk.Invoke(this.EnableOk, new object[] { false });
									}
									this.label1.Invoke(this.UpdateStatus, new object[] { "" });
									return;
								}
							}
						}
					}
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey = currentUser.CreateSubKey(Util.GetRegistryBase());
					bool flag16 = this.m_serial == null;
					if (flag16)
					{
						string text2 = string.Format("{0}-{1}-{2}-{3}", new object[]
						{
							this.txtSerial1.Text,
							this.txtSerial2.Text,
							this.txtSerial3.Text,
							this.txtSerial4.Text
						});
						this.m_hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(text2)));
						this.m_hash = this.m_hash.Replace("-", "");
					}
					else
					{
						this.m_hash = SerialValidateGG.ComputeHash(this.m_serial);
					}
					registryKey.SetValue("Hash", this.m_hash.ToUpper());
					registryKey.SetValue("BackupSaves", "true");
					string text3 = string.Format("{0}-{1}-{2}-{3}", new object[]
					{
						this.txtSerial1.Text,
						this.txtSerial2.Text,
						this.txtSerial3.Text,
						this.txtSerial4.Text
					});
					registryKey.SetValue("Serial", text3);
					registryKey.Close();
					currentUser.Close();
					try
					{
						bool isHandleCreated = base.IsHandleCreated;
						if (isHandleCreated)
						{
							base.Invoke(this.CloseForm, new object[] { true });
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000997FC File Offset: 0x000979FC
		private void EnableOkSafe(bool bEnable)
		{
			this.btnOk.Enabled = bEnable;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0009980C File Offset: 0x00097A0C
		private void CloseFormSafe(bool bSuccess)
		{
			bool flag = !bSuccess;
			if (flag)
			{
				base.DialogResult = DialogResult.Abort;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0009983C File Offset: 0x00097A3C
		private void FindGGUSB()
		{
			foreach (USB.USBController usbcontroller in USB.GetHostControllers())
			{
				USB.USBHub rootHub = usbcontroller.GetRootHub();
				this.ProcessHub(rootHub);
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00099898 File Offset: 0x00097A98
		private void ProcessHub(USB.USBHub hub)
		{
			foreach (USB.USBPort usbport in hub.GetPorts())
			{
				bool isHub = usbport.IsHub;
				if (isHub)
				{
					this.ProcessHub(usbport.GetHub());
				}
				USB.USBDevice device = usbport.GetDevice();
				bool flag = device == null;
				if (!flag)
				{
					bool flag2 = device.DeviceManufacturer != null && device.DeviceManufacturer.ToLower() == "dpdev" && device.DeviceProduct != null && device.DeviceProduct.ToLower() == "gamegenie";
					if (flag2)
					{
						this.m_serial = device.SerialNumber;
					}
				}
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00099968 File Offset: 0x00097B68
		private bool ValidateSerial()
		{
			for (int i = 1; i <= 4; i++)
			{
				TextBox textBox = base.Controls.Find("txtSerial" + i, true)[0] as TextBox;
				bool flag = textBox.Text.Length < 4;
				if (flag)
				{
					textBox.Focus();
					Util.ShowMessage(Resources.errInvalidSerial, this.Text);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000999E8 File Offset: 0x00097BE8
		private void btnOk_Click(object sender, EventArgs e)
		{
			this.m_serial = null;
			try
			{
				bool flag = !this.ValidateSerial();
				if (!flag)
				{
					this.btnOk.Invoke(this.EnableOk, new object[] { false });
					this.RegisterLicense();
				}
			}
			catch (Exception)
			{
				Util.ShowMessage(Resources.errConnection);
				bool flag2 = this.m_serial == null;
				if (flag2)
				{
					this.btnOk.Enabled = true;
				}
				this.btnCancel.Enabled = true;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00099A80 File Offset: 0x00097C80
		private void RegisterLicense()
		{
			WebClientEx webClientEx = new WebClientEx();
			webClientEx.Credentials = Util.GetNetworkCredential();
			this.label1.Text = Resources.msgWaitSerial;
			webClientEx.Headers[HttpRequestHeader.ContentType] = "application/json";
			webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
			webClientEx.UploadDataCompleted += this.client_UploadDataCompleted;
			webClientEx.UploadDataAsync(new Uri(string.Format("{0}/ps4auth", Util.GetAuthBaseUrl()), UriKind.Absolute), "POST", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"ACTIVATE_LICENSE\",\"license\":\"{0}\"}}", string.Format("{0}-{1}-{2}-{3}", new object[]
			{
				this.txtSerial1.Text,
				this.txtSerial2.Text,
				this.txtSerial3.Text,
				this.txtSerial4.Text
			}))));
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00099B68 File Offset: 0x00097D68
		private void client_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
		{
			this.m_userid = "";
			bool flag = e.Error is WebException && !this.m_bRetry;
			if (flag)
			{
				this.m_bRetry = true;
				Util.ChangeAuthServer();
				this.btnOk_Click(null, null);
			}
			else
			{
				bool flag2 = e.Error != null;
				if (flag2)
				{
					string text = "";
					bool flag3 = e.Error is WebException;
					if (flag3)
					{
						WebException ex = e.Error as WebException;
						bool flag4 = ex.Response is HttpWebResponse;
						if (flag4)
						{
							text = Convert.ToString((ex.Response as HttpWebResponse).StatusCode);
						}
					}
					this.btnOk.Invoke(this.EnableOk, new object[] { true });
					bool flag5 = string.IsNullOrEmpty(text);
					if (flag5)
					{
						Util.ShowMessage(Resources.errConnection);
					}
					else
					{
						Util.ShowMessage(Resources.errConnection + " (" + text + ")");
					}
				}
				else
				{
					string @string = Encoding.ASCII.GetString(e.Result);
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
					Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
					bool flag6 = (string)dictionary["status"] == "ERROR" && dictionary["code"].ToString() != "4020";
					if (flag6)
					{
						this.btnOk.Invoke(this.EnableOk, new object[] { true });
						this.label1.Invoke(this.UpdateStatus, new object[] { "" });
						Util.ShowErrorMessage(dictionary, Resources.errSerial);
					}
					else
					{
						this.m_userid = (string)dictionary["userid"];
						this.RegisterUID();
					}
				}
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00099D64 File Offset: 0x00097F64
		private void RegisterUID()
		{
			string uid = Util.GetUID(false, false);
			bool flag = string.IsNullOrEmpty(uid);
			if (flag)
			{
				Util.ShowMessage(Resources.errUnknown + " (2000)");
			}
			else
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Credentials = Util.GetNetworkCredential();
				webClientEx.Headers[HttpRequestHeader.ContentType] = "application/json";
				webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
				webClientEx.UploadDataCompleted += this.client2_UploadDataCompleted;
				webClientEx.UploadDataAsync(new Uri(string.Format("{0}/ps4auth", Util.GetAuthBaseUrl()), UriKind.Absolute), "POST", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"REGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}", this.m_userid, uid)));
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00099E28 File Offset: 0x00098028
		private void client2_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
		{
			bool flag = e.Error != null;
			if (flag)
			{
				string text = "";
				bool flag2 = e.Error is WebException;
				if (flag2)
				{
					WebException ex = (WebException)e.Error;
					bool flag3 = ex.Response is HttpWebResponse;
					if (flag3)
					{
						text = Convert.ToString(((HttpWebResponse)ex.Response).StatusCode);
					}
				}
				bool flag4 = string.IsNullOrEmpty(text);
				if (flag4)
				{
					Util.ShowMessage(Resources.errUnknown);
				}
				else
				{
					Util.ShowMessage(Resources.errUnknown + "(" + text + ")");
				}
				this.btnOk.Invoke(this.EnableOk, new object[] { true });
				this.m_userid = "";
			}
			else
			{
				string @string = Encoding.ASCII.GetString(e.Result);
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize(@string, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
				bool flag5 = (string)dictionary["status"] == "ERROR" && dictionary["code"].ToString() != "4021";
				if (flag5)
				{
					this.m_userid = "";
					this.btnOk.Invoke(this.EnableOk, new object[] { true });
					this.label1.Invoke(this.UpdateStatus, new object[] { "" });
					Util.ShowErrorMessage(dictionary, Resources.errUnknown);
				}
				else
				{
					Util.SetRegistryValue("User", this.m_userid);
					base.Invoke(this.CloseForm, new object[] { true });
				}
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0009A004 File Offset: 0x00098204
		private void txtSerial_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete;
			if (!flag)
			{
				TextBox textBox = sender as TextBox;
				bool flag2 = textBox.Text.Length == 4;
				if (flag2)
				{
					e.SuppressKeyPress = true;
				}
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0009A050 File Offset: 0x00098250
		private void txtSerial_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			bool flag = textBox.Name == "txtSerial1";
			if (!flag)
			{
				bool flag2 = textBox.Text.Length == 0 && e.KeyChar == '\b';
				if (flag2)
				{
					Control[] array = textBox.Parent.Controls.Find("txtSerial" + (textBox.Name[9] - '\u0001').ToString(), true);
					bool flag3 = array.Length == 1;
					if (flag3)
					{
						TextBox textBox2 = array[0] as TextBox;
						bool flag4 = textBox2.Text.Length > 0;
						if (flag4)
						{
							textBox2.SelectionStart = textBox2.Text.Length;
						}
						array[0].Focus();
					}
				}
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0009A120 File Offset: 0x00098320
		private void txtSerial_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			int selectionStart = textBox.SelectionStart;
			textBox.Text = Regex.Replace(textBox.Text, "[^0-9a-zA-Z ]", "").ToUpperInvariant();
			textBox.SelectionStart = selectionStart;
			bool flag = textBox.Name == "txtSerial1";
			if (flag)
			{
				string text = Clipboard.GetText();
				string[] array = text.Split(new char[] { '-' });
				bool flag2 = array.Length == 4;
				if (flag2)
				{
					array[0] = array[0].Trim();
					array[1] = array[1].Trim();
					array[2] = array[2].Trim();
					array[3] = array[3].Trim();
					bool flag3 = array[0].Length != 4 || array[1].Length != 4 || array[2].Length != 4 || array[3].Length != 4;
					if (flag3)
					{
						return;
					}
					Clipboard.Clear();
					this.txtSerial1.Text = array[0];
					this.txtSerial2.Text = array[1];
					this.txtSerial3.Text = array[2];
					this.txtSerial4.Text = array[3];
				}
			}
			bool flag4 = textBox.Text.Length == 4;
			if (flag4)
			{
				Control[] array2 = textBox.Parent.Controls.Find("txtSerial" + (textBox.Name[9] + '\u0001').ToString(), true);
				bool flag5 = array2.Length == 1;
				if (flag5)
				{
					array2[0].Focus();
				}
			}
			bool flag6 = this.txtSerial1.Text.Length == 4 && this.txtSerial2.Text.Length == 4 && this.txtSerial3.Text.Length == 4 && this.txtSerial4.Text.Length == 4;
			if (flag6)
			{
				this.btnOk.Enabled = true;
				this.btnOk.Focus();
			}
			else
			{
				this.btnOk.Enabled = false;
			}
		}

		// Token: 0x04000C70 RID: 3184
		public const string SERIAL_VALIDATE_URL = "{0}/ps4auth";

		// Token: 0x04000C71 RID: 3185
		public const string LICNESE_INFO = "{{\"action\":\"ACTIVATE_LICENSE\",\"license\":\"{0}\"}}";

		// Token: 0x04000C72 RID: 3186
		private const string REGISTER_UID = "{{\"action\":\"REGISTER_UUID\",\"userid\":\"{0}\",\"uuid\":\"{1}\"}}";

		// Token: 0x04000C73 RID: 3187
		private string m_serial;

		// Token: 0x04000C74 RID: 3188
		private string m_hash;

		// Token: 0x04000C75 RID: 3189
		private SerialValidateGG.CloseDelegate CloseForm;

		// Token: 0x04000C76 RID: 3190
		private SerialValidateGG.UpdateStatusDelegate UpdateStatus;

		// Token: 0x04000C77 RID: 3191
		private SerialValidateGG.EnableOkDelegate EnableOk;

		// Token: 0x04000C78 RID: 3192
		private int m_retryCount = 0;

		// Token: 0x04000C79 RID: 3193
		private string m_userid = "";

		// Token: 0x04000C7A RID: 3194
		private bool m_bRetry = false;

		// Token: 0x020002C6 RID: 710
		// (Invoke) Token: 0x06001EA9 RID: 7849
		private delegate void CloseDelegate(bool bSuccess);

		// Token: 0x020002C7 RID: 711
		// (Invoke) Token: 0x06001EAD RID: 7853
		private delegate void UpdateStatusDelegate(string status);

		// Token: 0x020002C8 RID: 712
		// (Invoke) Token: 0x06001EB1 RID: 7857
		private delegate void EnableOkDelegate(bool bEnable);
	}
}
