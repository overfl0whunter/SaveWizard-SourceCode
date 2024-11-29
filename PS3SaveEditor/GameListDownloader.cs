using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using PS3SaveEditor.Resources;
using Rss;

namespace PS3SaveEditor
{
	// Token: 0x020001DA RID: 474
	public partial class GameListDownloader : Form
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00091279 File Offset: 0x0008F479
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x00091281 File Offset: 0x0008F481
		public string GameListXml { get; set; }

		// Token: 0x0600187D RID: 6269 RVA: 0x0009128C File Offset: 0x0008F48C
		public GameListDownloader()
		{
			string registryValue = Util.GetRegistryValue("Language");
			bool flag = registryValue != null;
			if (flag)
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(registryValue);
			}
			this.InitializeComponent();
			base.CenterToScreen();
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.Font = Util.GetFontForPlatform(this.Font);
			base.ControlBox = false;
			this.BackColor = Color.FromArgb(80, 29, 11);
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblStatus.BackColor = Color.Transparent;
			this.lblStatus.Text = Resources.gamelistDownloaderMsg;
			this.Text = Resources.gamelistDownloaderTitle;
			base.Visible = false;
			base.Load += this.GameListDownloader_Load;
			this.UpdateProgress = new GameListDownloader.UpdateProgressDelegate(this.UpdateProgressSafe);
			this.UpdateStatus = new GameListDownloader.UpdateStatusDelegate(this.UpdateStatusSafe);
			this.CloseForm = new GameListDownloader.CloseDelegate(this.CloseFormSafe);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000913C4 File Offset: 0x0008F5C4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00091430 File Offset: 0x0008F630
		private void CloseThisForm(bool bSuccess)
		{
			bool flag = !base.IsDisposed;
			if (flag)
			{
				base.Invoke(this.CloseForm, new object[] { bSuccess });
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00091467 File Offset: 0x0008F667
		private void CloseFormSafe(bool bSuccess)
		{
			this.appClosing = true;
			base.DialogResult = (bSuccess ? DialogResult.OK : DialogResult.Abort);
			base.Close();
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00091486 File Offset: 0x0008F686
		private void SetStatus(string status)
		{
			this.lblStatus.Invoke(this.UpdateStatus, new object[] { status });
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000914A5 File Offset: 0x0008F6A5
		private void UpdateStatusSafe(string status)
		{
			this.lblStatus.Text = status;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000914B5 File Offset: 0x0008F6B5
		private void SetProgress(int val)
		{
			this.pbProgress.Invoke(this.UpdateProgress, new object[] { val });
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000914D9 File Offset: 0x0008F6D9
		private void UpdateProgressSafe(int val)
		{
			this.pbProgress.Value = val;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000914EC File Offset: 0x0008F6EC
		private void GameListDownloader_Load(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.GetOnlineGamesList));
			string registryValue = Util.GetRegistryValue("Language");
			bool flag = registryValue != null;
			if (flag)
			{
				thread.CurrentUICulture = new CultureInfo(registryValue);
			}
			thread.Start();
			try
			{
				long ticks = DateTime.Now.Ticks;
				bool flag2 = !Util.IsHyperkin();
				if (flag2)
				{
					GameListDownloader.RSS_URL = string.Format("{0}/ps4/rss?token={1}", Util.GetBaseUrl(), Util.GetAuthToken());
				}
				RssFeed rssFeed = RssFeed.Read(GameListDownloader.RSS_URL);
				RssChannel rssChannel = rssFeed.Channels[0];
				bool flag3 = rssChannel.Items.Count > 0;
				if (flag3)
				{
					RSSForm rssform = new RSSForm(rssChannel);
					rssform.ShowDialog();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000915C8 File Offset: 0x0008F7C8
		private void GetOnlineGamesList()
		{
			string gamelistPath = Util.GetGamelistPath();
			string text = "";
			bool flag = File.Exists(gamelistPath);
			if (flag)
			{
				text = Util.GetHash(gamelistPath);
			}
			try
			{
				string text2 = string.Format(GameListDownloader.GAME_LIST_URL + "&checksum={2}", Util.GetBaseUrl(), Util.GetAuthToken(), text);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text2);
				httpWebRequest.Method = "GET";
				httpWebRequest.Credentials = Util.GetNetworkCredential();
				string text3 = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
				httpWebRequest.UserAgent = Util.GetUserAgent();
				httpWebRequest.Headers.Add("Authorization", text3);
				httpWebRequest.Headers.Add("accept-charset", "UTF-8");
				this.SetStatus(Resources.msgConnecting);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				bool flag2 = HttpStatusCode.OK == httpWebResponse.StatusCode;
				if (!flag2)
				{
					Util.ShowMessage(Resources.errInvalidResponse);
					this.GameListXml = "";
					this.CloseThisForm(false);
					return;
				}
				this.SetStatus(Resources.msgDownloadingList);
				Stream responseStream = httpWebResponse.GetResponseStream();
				int num = 0;
				int num2 = 60;
				bool flag3 = httpWebResponse.ContentLength > (long)num2 && File.Exists(gamelistPath);
				if (flag3)
				{
					File.Delete(gamelistPath);
				}
				FileStream fileStream = new FileStream(gamelistPath, FileMode.OpenOrCreate, FileAccess.Write);
				byte[] array = new byte[1024];
				bool flag4 = httpWebResponse.ContentLength < (long)num2;
				if (flag4)
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text4 = streamReader.ReadToEnd();
						Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(text4, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
						bool flag5 = dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
						if (!flag5)
						{
							int num3 = responseStream.Read(array, 0, (int)httpWebResponse.ContentLength);
							fileStream.Write(array, 0, num3);
						}
					}
				}
				else
				{
					while ((long)num < httpWebResponse.ContentLength)
					{
						int num3 = responseStream.Read(array, 0, Math.Min(1024, (int)httpWebResponse.ContentLength - num));
						fileStream.Write(array, 0, num3);
						num += num3;
						this.SetProgress((int)((long)(num * 100) / httpWebResponse.ContentLength));
					}
				}
				this.SetProgress(100);
				fileStream.Close();
				httpWebResponse.Close();
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Resources.errConnection, Resources.msgError);
				this.GameListXml = "";
				this.CloseThisForm(false);
				throw ex;
			}
			this.GameListXml = "";
			bool flag6 = File.Exists(gamelistPath);
			if (flag6)
			{
				this.GameListXml = File.ReadAllText(gamelistPath);
			}
			bool flag7 = this.GameListXml == "" || this.GameListXml.IndexOf("ERROR") > 0;
			if (flag7)
			{
				Util.ShowMessage(Resources.errServer);
				this.GameListXml = "";
				this.CloseThisForm(false);
			}
			else
			{
				try
				{
					using (FileStream fileStream2 = new FileStream(gamelistPath, FileMode.Open, FileAccess.Read))
					{
						fileStream2.Seek(0L, SeekOrigin.Begin);
						using (GZipStream gzipStream = new GZipStream(fileStream2, CompressionMode.Decompress))
						{
							byte[] array2 = new byte[4096];
							using (MemoryStream memoryStream = new MemoryStream())
							{
								int num4;
								do
								{
									num4 = gzipStream.Read(array2, 0, 4096);
									bool flag8 = num4 > 0;
									if (flag8)
									{
										memoryStream.Write(array2, 0, num4);
									}
								}
								while (num4 > 0);
								this.GameListXml = Encoding.UTF8.GetString(memoryStream.ToArray());
							}
						}
					}
				}
				catch (Exception)
				{
				}
				Thread.Sleep(500);
				this.CloseThisForm(true);
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00091A98 File Offset: 0x0008FC98
		private void GameListDownloader_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = !this.appClosing;
			if (flag)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x04000C1D RID: 3101
		private GameListDownloader.UpdateProgressDelegate UpdateProgress;

		// Token: 0x04000C1E RID: 3102
		private GameListDownloader.UpdateStatusDelegate UpdateStatus;

		// Token: 0x04000C1F RID: 3103
		private GameListDownloader.CloseDelegate CloseForm;

		// Token: 0x04000C20 RID: 3104
		private bool appClosing = false;

		// Token: 0x04000C22 RID: 3106
		private static string GAME_LIST_URL = "{0}/games?token={1}";

		// Token: 0x04000C23 RID: 3107
		public static string RSS_URL = "http://www.thesavewizard.com/app/rss";

		// Token: 0x020002B8 RID: 696
		// (Invoke) Token: 0x06001E73 RID: 7795
		private delegate void UpdateProgressDelegate(int value);

		// Token: 0x020002B9 RID: 697
		// (Invoke) Token: 0x06001E77 RID: 7799
		private delegate void UpdateStatusDelegate(string status);

		// Token: 0x020002BA RID: 698
		// (Invoke) Token: 0x06001E7B RID: 7803
		private delegate void CloseDelegate(bool bSuccess);
	}
}
