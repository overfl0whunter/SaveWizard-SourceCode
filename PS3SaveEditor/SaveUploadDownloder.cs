using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Core;
using Ionic.Crc;
using Ionic.Zip;
using Ionic.Zlib;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E1 RID: 481
	public class SaveUploadDownloder : UserControl
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00094EBC File Offset: 0x000930BC
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x00094EC4 File Offset: 0x000930C4
		public ManualResetEvent AbortEvent { get; set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00094ECD File Offset: 0x000930CD
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x00094ED5 File Offset: 0x000930D5
		public ProgressBar ProgressBar { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00094EDE File Offset: 0x000930DE
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00094EE6 File Offset: 0x000930E6
		public Label StatusLabel { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00094EEF File Offset: 0x000930EF
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00094EF7 File Offset: 0x000930F7
		public string Action { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00094F00 File Offset: 0x00093100
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00094F08 File Offset: 0x00093108
		public game Game { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00094F11 File Offset: 0x00093111
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00094F19 File Offset: 0x00093119
		public bool IsUpload { get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00094F22 File Offset: 0x00093122
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00094F2A File Offset: 0x0009312A
		public string FilePath { get; set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00094F33 File Offset: 0x00093133
		// (set) Token: 0x060018E2 RID: 6370 RVA: 0x00094F3B File Offset: 0x0009313B
		public string[] Files { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00094F44 File Offset: 0x00093144
		// (set) Token: 0x060018E4 RID: 6372 RVA: 0x00094F4C File Offset: 0x0009314C
		public string SaveId { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00094F55 File Offset: 0x00093155
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x00094F5D File Offset: 0x0009315D
		public string OutputFolder { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00094F66 File Offset: 0x00093166
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x00094F6E File Offset: 0x0009316E
		public string ListResult { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00094F77 File Offset: 0x00093177
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x00094F7F File Offset: 0x0009317F
		public List<string> OrderedEntries { get; set; }

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060018EB RID: 6379 RVA: 0x00094F88 File Offset: 0x00093188
		// (remove) Token: 0x060018EC RID: 6380 RVA: 0x00094FC0 File Offset: 0x000931C0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SaveUploadDownloder.DownloadStartEventHandler DownloadStart;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060018ED RID: 6381 RVA: 0x00094FF8 File Offset: 0x000931F8
		// (remove) Token: 0x060018EE RID: 6382 RVA: 0x00095030 File Offset: 0x00093230
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SaveUploadDownloder.UploadStartEventHandler UploadStart;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060018EF RID: 6383 RVA: 0x00095068 File Offset: 0x00093268
		// (remove) Token: 0x060018F0 RID: 6384 RVA: 0x000950A0 File Offset: 0x000932A0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SaveUploadDownloder.DownloadFinishEventHandler DownloadFinish;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060018F1 RID: 6385 RVA: 0x000950D8 File Offset: 0x000932D8
		// (remove) Token: 0x060018F2 RID: 6386 RVA: 0x00095110 File Offset: 0x00093310
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SaveUploadDownloder.UploadFinishEventHandler UploadFinish;

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00095145 File Offset: 0x00093345
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0009514D File Offset: 0x0009334D
		public string Profile { get; set; }

		// Token: 0x060018F5 RID: 6389 RVA: 0x00095158 File Offset: 0x00093358
		public SaveUploadDownloder()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.BackColor = Color.FromArgb(200, 100, 10);
			this.AbortEvent = new ManualResetEvent(false);
			this.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblStatus.BackColor = Color.Transparent;
			this.lblCurrentProgress.BackColor = Color.Transparent;
			base.Disposed += this.SaveUploadDownloder_Disposed;
			this.lblTotalProgress.BackColor = Color.Transparent;
			this.lblStatus.ForeColor = Color.White;
			this.lblCurrentProgress.ForeColor = Color.White;
			this.lblTotalProgress.ForeColor = Color.White;
			this.UpdateProgress = new SaveUploadDownloder.UpdateProgressDelegate(this.UpdateProgressSafe);
			this.UpdateStatus = new SaveUploadDownloder.UpdateStatusDelegate(this.UpdateStatusSafe);
			this.ProgressBar = this.pbProgress;
			this.StatusLabel = this.lblStatus;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00095292 File Offset: 0x00093492
		private void SaveUploadDownloder_Disposed(object sender, EventArgs e)
		{
			this.AbortEvent.Dispose();
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000952A4 File Offset: 0x000934A4
		private bool CheckCompressability()
		{
			string text = this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4);
			bool flag2;
			using (FileStream fileStream = File.OpenRead(text))
			{
				bool flag = fileStream.Length < 1048576L;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					fileStream.Seek(fileStream.Length - 1048576L, SeekOrigin.Begin);
					byte[] array = new byte[1048576];
					byte[] array2 = new byte[1048576];
					fileStream.Read(array, 0, 1048576);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (MemoryStream memoryStream2 = new MemoryStream(array))
						{
							using (ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream))
							{
								zipOutputStream.CompressionLevel = CompressionLevel.BestCompression;
								zipOutputStream.PutNextEntry("temp");
								StreamUtils.Copy(memoryStream2, zipOutputStream, array2);
								flag2 = (double)memoryStream.Length / (double)array.Length < 0.8;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000953F4 File Offset: 0x000935F4
		private bool BackupSaveData()
		{
			bool flag = Util.GetRegistryValue("BackupSaves") == "false" || this.Action == "resign";
			bool flag2;
			if (flag)
			{
				flag2 = this.CheckCompressability();
			}
			else
			{
				string text = null;
				bool flag3 = this.Game != null;
				if (flag3)
				{
					text = Path.GetDirectoryName(this.Game.LocalSaveFolder);
				}
				bool flag4 = File.Exists(this.FilePath);
				if (flag4)
				{
					this.SetStatus(Resources.lblBackingUp);
					string text2 = string.Concat(new string[]
					{
						Util.GetBackupLocation(),
						Path.DirectorySeparatorChar.ToString(),
						this.Game.PSN_ID,
						"_",
						Path.GetFileName(text),
						"_",
						Path.GetFileNameWithoutExtension(this.Game.LocalSaveFolder),
						"_",
						DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"),
						".bak"
					});
					this.SetProgress(0, new int?(30));
					string asZipFile = ZipUtil.GetAsZipFile(new string[]
					{
						this.Game.LocalSaveFolder,
						this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4)
					}, new ZipUtil.OnZipProgress(this.OnProgress));
					File.Copy(asZipFile, text2, true);
					File.Delete(asZipFile);
					flag2 = (double)(new FileInfo(text2).Length / new FileInfo(this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4)).Length) < 0.7;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000955C2 File Offset: 0x000937C2
		protected virtual void RaiseDownloadFinishEvent(bool bSuccess, string error)
		{
			this.DownloadFinish(this, new DownloadFinishEventArgs(bSuccess, error));
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000955D9 File Offset: 0x000937D9
		protected virtual void RaiseUploadFinishEvent()
		{
			this.UploadFinish(this, new EventArgs());
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000955EE File Offset: 0x000937EE
		protected virtual void RaiseUploadStartEvent()
		{
			this.UploadStart(this, new EventArgs());
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00095603 File Offset: 0x00093803
		protected virtual void RaiseDownloadStartEvent()
		{
			this.DownloadStart(this, new EventArgs());
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00095618 File Offset: 0x00093818
		public void Start()
		{
			this.t = new Thread(new ThreadStart(this.UploadFile));
			string registryValue = Util.GetRegistryValue("Language");
			bool flag = registryValue != null;
			if (flag)
			{
				this.t.CurrentUICulture = new CultureInfo(registryValue);
			}
			this.t.Start();
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00095670 File Offset: 0x00093870
		public void SetStatus(string status)
		{
			bool isHandleCreated = base.IsHandleCreated;
			if (isHandleCreated)
			{
				this.lblStatus.Invoke(this.UpdateStatus, new object[] { status });
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000956A4 File Offset: 0x000938A4
		private void UpdateStatusSafe(string status)
		{
			this.lblStatus.Text = status;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000956B4 File Offset: 0x000938B4
		private void SetProgress(int val, int? overall)
		{
			bool flag = this.start > 0L;
			if (flag)
			{
			}
			bool isHandleCreated = base.IsHandleCreated;
			if (isHandleCreated)
			{
				this.pbProgress.Invoke(this.UpdateProgress, new object[] { val, overall });
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00095708 File Offset: 0x00093908
		private void UpdateProgressSafe(int val, int? overall)
		{
			this.pbProgress.Value = val;
			bool flag = overall != null;
			if (flag)
			{
				this.pbOverallProgress.Value = overall.Value;
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00095744 File Offset: 0x00093944
		public bool OnProgress(int progress)
		{
			bool flag = this.AbortEvent.WaitOne(0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.SetProgress(progress, null);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0009577C File Offset: 0x0009397C
		public static void ErrorMessage(Form Parent, string errorMessage, string title = null)
		{
			bool flag = Parent != null && Parent.InvokeRequired;
			if (flag)
			{
				bool flag2 = title != null;
				if (flag2)
				{
					Parent.Invoke(new Action(delegate
					{
						Util.ShowMessage(Parent, errorMessage, title);
					}));
				}
				else
				{
					Parent.Invoke(new Action(delegate
					{
						Util.ShowMessage(Parent, errorMessage);
					}));
				}
			}
			else
			{
				bool flag3 = title != null;
				if (flag3)
				{
					Util.ShowMessage(Parent, errorMessage, title);
				}
				else
				{
					Util.ShowMessage(Parent, errorMessage);
				}
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00095840 File Offset: 0x00093A40
		private void UploadFile()
		{
			this.SetStatus(Resources.lblCheckSession);
			this.SetStatus(Resources.lblPreparing);
			bool flag = this.Game.PFSZipEntry != null;
			if (flag)
			{
				string text = Path.Combine(Util.GetTempFolder(), Path.GetFileName(this.Game.PFSZipEntry.FileName));
				using (CrcCalculatorStream crcCalculatorStream = this.Game.PFSZipEntry.OpenReader())
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
					{
						byte[] array = new byte[4096];
						StreamUtils.Copy(crcCalculatorStream, fileStream, array, delegate(object a, ProgressEventArgs e)
						{
							this.OnProgress((int)e.PercentComplete);
						}, TimeSpan.FromSeconds(1.0), this, "Extract");
					}
				}
				this.Game.LocalSaveFolder = text + ".bin";
				using (CrcCalculatorStream crcCalculatorStream2 = this.Game.BinZipEntry.OpenReader())
				{
					using (FileStream fileStream2 = new FileStream(this.Game.LocalSaveFolder, FileMode.Create, FileAccess.Write))
					{
						byte[] array2 = new byte[4096];
						StreamUtils.Copy(crcCalculatorStream2, fileStream2, array2);
					}
				}
			}
			string text2 = null;
			bool flag2 = this.Files != null;
			if (flag2)
			{
				this.SetProgress(0, new int?(0));
				bool flag3 = this.Action == "decrypt" || this.Action == "list" || this.Action == "patch" || this.Action == "resign";
				if (flag3)
				{
					int num = 4096;
					bool flag4 = Util.CurrentPlatform == Util.Platform.MacOS;
					bool flag5 = flag4;
					if (flag5)
					{
						num = 4194304;
					}
					long num2 = 0L;
					using (FileStream fileStream3 = File.OpenRead(this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4)))
					{
						using (HashAlgorithm hashAlgorithm = MD5.Create())
						{
							long length = fileStream3.Length;
							byte[] array3 = new byte[num];
							int num3 = fileStream3.Read(array3, 0, array3.Length);
							num2 += (long)num3;
							for (;;)
							{
								int num4 = num3;
								byte[] array4 = array3;
								array3 = new byte[num];
								num3 = fileStream3.Read(array3, 0, array3.Length);
								num2 += (long)num3;
								bool flag6 = num3 == 0;
								if (flag6)
								{
									hashAlgorithm.TransformFinalBlock(array4, 0, num4);
								}
								else
								{
									hashAlgorithm.TransformBlock(array4, 0, num4, array4, 0);
								}
								this.SetProgress((int)((double)num2 * 100.0 / (double)length), new int?(0));
								bool flag7 = this.AbortEvent.WaitOne(0);
								if (flag7)
								{
									break;
								}
								if (num3 == 0)
								{
									goto Block_49;
								}
							}
							fileStream3.Close();
							this.RaiseDownloadFinishEvent(false, "Abort");
							return;
							Block_49:
							text2 = BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToLower();
							List<string> list = new List<string>(this.Files);
							bool flag8 = this.Action == "decrypt" || this.Action == "list" || this.Action == "patch" || this.Action == "resign";
							string text3;
							if (flag8)
							{
								bool flag9 = this.Action == "decrypt" || this.Action == "list";
								if (flag9)
								{
									text3 = this.Game.ToString(new List<string>(), "decrypt");
								}
								else
								{
									text3 = this.Game.ToString(true, list);
								}
								text3 = text3.Replace("<name>" + Path.GetFileNameWithoutExtension(this.Game.LocalSaveFolder) + "</name>", string.Concat(new string[]
								{
									"<name>",
									Path.GetFileNameWithoutExtension(this.Game.LocalSaveFolder),
									"</name><md5>",
									text2,
									"</md5>"
								}));
								bool flag10 = this.Action == "resign";
								if (flag10)
								{
									text3 = text3.Replace("mode=\"patch\"", "mode=\"resign\"").Replace("</pfs>", "</pfs><psnid>" + this.Profile + "</psnid>");
								}
								bool flag11 = list.IndexOf(this.Game.LocalSaveFolder) < 0;
								if (flag11)
								{
									list.Add(this.Game.LocalSaveFolder);
								}
							}
							else
							{
								text3 = this.Game.ToString(true, list);
								text3 = text3.Replace("<name>" + Path.GetFileNameWithoutExtension(this.Game.LocalSaveFolder) + "</name>", string.Concat(new string[]
								{
									"<name>",
									Path.GetFileNameWithoutExtension(this.Game.LocalSaveFolder),
									"</name><md5>",
									text2,
									"</md5>"
								}));
								list = this.Game.GetContainerFiles();
							}
							string tempFolder = Util.GetTempFolder();
							string text4 = Path.Combine(tempFolder, "ps4_list.xml");
							File.WriteAllText(text4, text3);
							list.Add(text4);
							this.Files = list.ToArray();
						}
					}
					this.SetProgress(0, new int?(20));
				}
				this.FilePath = ZipUtil.GetAsZipFile(this.Files, this.Profile, new ZipUtil.OnZipProgress(this.OnProgress));
			}
			bool flag12 = false;
			bool flag13 = this.Action == "patch" || this.Action == "resign" || this.Action == "encrypt";
			if (flag13)
			{
				flag12 = this.BackupSaveData();
			}
			bool flag14 = this.Action == "decrypt" || this.Action == "list";
			if (flag14)
			{
				flag12 = this.CheckCompressability();
			}
			this.SetProgress(0, new int?(40));
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("form_id", "request_form");
			bool flag15 = this.Game != null;
			if (flag15)
			{
				nameValueCollection.Add("gamecode", this.Game.id);
				bool flag16 = !string.IsNullOrEmpty(this.Game.diskcode);
				if (flag16)
				{
					nameValueCollection.Add("diskcode", this.Game.diskcode);
				}
			}
			else
			{
				bool flag17 = !string.IsNullOrEmpty(this.OutputFolder) && this.Action != "download";
				if (flag17)
				{
					nameValueCollection.Add("gameid", Path.GetFileName(this.OutputFolder).Substring(0, 9));
				}
			}
			bool flag18 = this.SaveId != null;
			if (flag18)
			{
				nameValueCollection.Add("saveid", this.SaveId);
			}
			nameValueCollection.Add("action", this.Action);
			string text5 = this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4);
			bool flag19 = this.Action == "decrypt" || this.Action == "list" || this.Action == "patch" || this.Action == "resign";
			if (flag19)
			{
				bool flag20 = flag12;
				if (flag20)
				{
					this.SetStatus(Resources.lblCompressing);
					this.SetProgress(0, new int?(50));
					text5 = ZipUtil.GetAsZipFile(new string[] { text5 }, new ZipUtil.OnZipProgress(this.OnProgress));
				}
				this.RaiseUploadStartEvent();
				bool flag21 = !this.UploadChunks(text5, text2);
				if (flag21)
				{
					bool flag22 = flag12;
					if (flag22)
					{
						File.Delete(text5);
					}
					bool flag23 = !this.AbortEvent.WaitOne(0);
					if (!flag23)
					{
						this.RaiseDownloadFinishEvent(false, "Abort");
					}
					return;
				}
				bool flag24 = flag12;
				if (flag24)
				{
					File.Delete(text5);
				}
			}
			this.HttpUploadFile(string.Format("{0}{1}", Util.GetBaseUrl(), string.Format(SaveUploadDownloder.UPLOAD_URL, Util.GetAuthToken())), this.FilePath, "files[input_zip_file]", "application/x-zip-compressed", nameValueCollection);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00096158 File Offset: 0x00094358
		private bool CheckSession()
		{
			byte[] array = new WebClientEx
			{
				Credentials = Util.GetNetworkCredential()
			}.UploadData(Util.GetBaseUrl() + "/ps4auth", Encoding.ASCII.GetBytes(string.Format("{{\"action\":\"SESSION_REFRESH\",\"userid\":\"{0}\",\"token\":\"{1}\"}}", Util.GetUserId(), Util.GetAuthToken())));
			string @string = Encoding.ASCII.GetString(array);
			return !@string.Contains("ERROR");
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000961CC File Offset: 0x000943CC
		protected override void OnHandleDestroyed(EventArgs e)
		{
			bool flag = this.t != null;
			if (flag)
			{
				this.t.Abort();
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000961FC File Offset: 0x000943FC
		public bool UploadChunks_(string file)
		{
			int num = 8;
			int num2 = 1048576;
			string fileName = Path.GetFileName(file);
			string hash = Util.GetHash(file);
			string text = "---------------------------" + DateTime.Now.Ticks.ToString("x");
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + text + "\r\n");
			long length = new FileInfo(file).Length;
			int num3 = (int)Math.Ceiling((double)length / 1024.0 * 1024.0);
			List<int> remainingChunks = this.GetRemainingChunks(fileName, hash, text, length);
			bool flag = remainingChunks != null && remainingChunks.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = remainingChunks == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					long num4 = (long)(remainingChunks.Count * num2);
					long num5 = 0L;
					using (FileStream fileStream = File.OpenRead(file))
					{
						for (int i = 0; i < remainingChunks.Count; i += num)
						{
							HttpWebRequest webRequest = this.GetWebRequest(text);
							string text2 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
							Stream requestStream = webRequest.GetRequestStream();
							int j = i;
							int num6 = 1;
							string text3;
							byte[] array2;
							while (j < Math.Min(remainingChunks.Count, i + num))
							{
								fileStream.Seek((long)(j * 1024 * 1024), SeekOrigin.Begin);
								requestStream.Write(bytes, 0, bytes.Length);
								int num7 = (int)Math.Min((long)num2, length - (long)(j * 1024 * 1024));
								byte[] array = new byte[num7];
								int num8 = fileStream.Read(array, 0, num2);
								string hash2 = Util.GetHash(array);
								text3 = string.Format(text2, "chunk" + num6 + "_md5", hash2);
								array2 = Encoding.UTF8.GetBytes(text3);
								requestStream.Write(array2, 0, array2.Length);
								requestStream.Write(bytes, 0, bytes.Length);
								text3 = string.Format(text2, "chunk" + num6 + "id", string.Concat(remainingChunks[j]));
								array2 = Encoding.UTF8.GetBytes(text3);
								requestStream.Write(array2, 0, array2.Length);
								string text4 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
								string text5 = string.Format(text4, "files[chunk" + num6 + "]", "chunk" + remainingChunks[j], "application/octet-stream");
								requestStream.Write(bytes, 0, bytes.Length);
								byte[] bytes2 = Encoding.UTF8.GetBytes(text5);
								requestStream.Write(bytes2, 0, bytes2.Length);
								requestStream.Write(array, 0, num8);
								num5 += (long)num8;
								int num9 = (int)(num5 * 100L / num4);
								this.SetProgress(num9, new int?(40));
								j++;
								num6++;
							}
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "op", "Submit");
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "form_id", "chunk_upload_form");
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "pfs_md5", hash);
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "total_chunks", num3);
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "gamecode", this.Game.id);
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							bool flag4 = !string.IsNullOrEmpty(this.Game.diskcode);
							if (flag4)
							{
								requestStream.Write(bytes, 0, bytes.Length);
								text3 = string.Format(text2, "diskcode", this.Game.diskcode);
								array2 = Encoding.UTF8.GetBytes(text3);
								requestStream.Write(array2, 0, array2.Length);
							}
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "pfs", fileName);
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text3 = string.Format(text2, "pfs_size", length);
							array2 = Encoding.UTF8.GetBytes(text3);
							requestStream.Write(array2, 0, array2.Length);
							byte[] bytes3 = Encoding.ASCII.GetBytes("\r\n--" + text + "--\r\n");
							requestStream.Write(bytes3, 0, bytes3.Length);
							requestStream.Close();
							HttpWebResponse httpWebResponse = webRequest.GetResponse() as HttpWebResponse;
							bool flag5 = httpWebResponse.StatusCode == HttpStatusCode.OK;
							if (flag5)
							{
								using (Stream responseStream = httpWebResponse.GetResponseStream())
								{
									using (StreamReader streamReader = new StreamReader(responseStream))
									{
										long contentLength = httpWebResponse.ContentLength;
										string text6 = streamReader.ReadToEnd();
										bool flag6 = text6.IndexOf("true") > 0;
										if (flag6)
										{
											return true;
										}
									}
								}
							}
						}
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00096840 File Offset: 0x00094A40
		private List<int> GetRemainingChunks(string pfsFileName, string pfsHash, string boundary, long fileSize)
		{
			try
			{
				List<int> list = new List<int>();
				int num = (int)Math.Ceiling((double)fileSize / 1024.0 * 1024.0);
				HttpWebRequest webRequest = this.GetWebRequest(boundary);
				Stream requestStream = webRequest.GetRequestStream();
				string text = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
				byte[] bytes = Encoding.ASCII.GetBytes(boundary);
				requestStream.Write(bytes, 0, bytes.Length);
				string text2 = string.Format(text, "op", "Submit");
				byte[] array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "form_id", "chunk_upload_form");
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "pfs_md5", pfsHash);
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "total_chunks", num);
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "gamecode", this.Game.id);
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				bool flag = !string.IsNullOrEmpty(this.Game.diskcode);
				if (flag)
				{
					requestStream.Write(bytes, 0, bytes.Length);
					text2 = string.Format(text, "diskcode", this.Game.diskcode);
					array = Encoding.UTF8.GetBytes(text2);
					requestStream.Write(array, 0, array.Length);
				}
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "pfs", pfsFileName);
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				requestStream.Write(bytes, 0, bytes.Length);
				text2 = string.Format(text, "pfs_size", fileSize);
				array = Encoding.UTF8.GetBytes(text2);
				requestStream.Write(array, 0, array.Length);
				byte[] bytes2 = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
				requestStream.Write(bytes2, 0, bytes2.Length);
				requestStream.Close();
				HttpWebResponse httpWebResponse = webRequest.GetResponse() as HttpWebResponse;
				bool flag2 = httpWebResponse.StatusCode == HttpStatusCode.OK;
				if (flag2)
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							long contentLength = httpWebResponse.ContentLength;
							string text3 = streamReader.ReadToEnd();
							bool flag3 = text3.IndexOf("true") > 0;
							if (flag3)
							{
								return list;
							}
							Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(text3, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
							bool flag4 = dictionary.ContainsKey("remaining_chunks");
							if (flag4)
							{
								Dictionary<string, object> dictionary2 = dictionary["remaining_chunks"] as Dictionary<string, object>;
								list = new List<int>();
								foreach (string text4 in dictionary2.Keys)
								{
									bool flag5 = !(bool)dictionary2[text4];
									if (flag5)
									{
										list.Add(int.Parse(text4));
									}
								}
								return list;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
			return null;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00096C7C File Offset: 0x00094E7C
		private HttpWebRequest GetWebRequest(string boundary)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Util.GetBaseUrl() + "/chunk_upload?token=" + Util.GetAuthToken());
			httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
			httpWebRequest.AllowWriteStreamBuffering = true;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
			httpWebRequest.Method = "POST";
			httpWebRequest.UserAgent = Util.GetUserAgent();
			httpWebRequest.ProtocolVersion = new Version(1, 1);
			httpWebRequest.KeepAlive = true;
			ServicePointManager.Expect100Continue = false;
			httpWebRequest.Credentials = Util.GetNetworkCredential();
			httpWebRequest.Timeout = 600000;
			httpWebRequest.ReadWriteTimeout = 600000;
			httpWebRequest.SendChunked = true;
			string text = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
			httpWebRequest.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
			httpWebRequest.Headers.Add("Authorization", text);
			return httpWebRequest;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00096D88 File Offset: 0x00094F88
		public bool UploadChunks(string file, string pfsHash)
		{
			int num = 8;
			int num2 = 1048576;
			string text = this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4);
			string fileName = Path.GetFileName(text);
			bool flag = string.IsNullOrEmpty(pfsHash);
			if (flag)
			{
				pfsHash = Util.GetHash(text);
			}
			List<int> list = new List<int>();
			string text2 = "---------------------------" + DateTime.Now.Ticks.ToString("x");
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + text2 + "\r\n");
			int num3 = 0;
			try
			{
				using (FileStream fileStream = File.Open(file, FileMode.Open))
				{
					int num4 = (int)Math.Ceiling((double)fileStream.Length / (double)num2);
					long num5 = 0L;
					long length = fileStream.Length;
					long num6 = length;
					bool flag2 = true;
					this.SetProgress(0, new int?(60));
					for (;;)
					{
						try
						{
							bool flag3 = this.AbortEvent.WaitOne(0);
							if (flag3)
							{
								fileStream.Close();
								return false;
							}
							HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Util.GetBaseUrl() + "/chunk_upload?token=" + Util.GetAuthToken());
							httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
							httpWebRequest.AllowWriteStreamBuffering = true;
							httpWebRequest.PreAuthenticate = true;
							httpWebRequest.ContentType = "multipart/form-data; boundary=" + text2;
							httpWebRequest.Method = "POST";
							httpWebRequest.UserAgent = Util.GetUserAgent();
							httpWebRequest.ProtocolVersion = new Version(1, 1);
							httpWebRequest.KeepAlive = true;
							ServicePointManager.Expect100Continue = false;
							httpWebRequest.Credentials = Util.GetNetworkCredential();
							httpWebRequest.Timeout = 600000;
							httpWebRequest.ReadWriteTimeout = 600000;
							httpWebRequest.SendChunked = true;
							string text3 = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
							httpWebRequest.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
							httpWebRequest.Headers.Add("Authorization", text3);
							string text4 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
							long num7 = 0L;
							num7 += (long)bytes.Length;
							string text5 = string.Format(text4, "form_id", "chunk_upload_form");
							byte[] array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "op", "Submit");
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "pfs_md5", pfsHash);
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "total_chunks", num4);
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "gamecode", this.Game.id);
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							bool flag4 = !string.IsNullOrEmpty(this.Game.diskcode);
							if (flag4)
							{
								num7 += (long)bytes.Length;
								text5 = string.Format(text4, "diskcode", this.Game.diskcode);
								array = Encoding.UTF8.GetBytes(text5);
								num7 += (long)array.Length;
							}
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "pfs", fileName);
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							num7 += (long)bytes.Length;
							text5 = string.Format(text4, "pfs_size", length);
							array = Encoding.UTF8.GetBytes(text5);
							num7 += (long)array.Length;
							Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>();
							bool flag5 = !flag2;
							if (flag5)
							{
								int i = 0;
								int num8 = 1;
								while (i < Math.Min(list.Count, num))
								{
									byte[] array2 = new byte[num2];
									fileStream.Seek((long)((list[i] - 1) * num2), SeekOrigin.Begin);
									int num9 = fileStream.Read(array2, 0, num2);
									bool flag6 = num9 < num2;
									if (flag6)
									{
										byte[] array3 = new byte[num9];
										Array.Copy(array2, array3, num9);
										array2 = array3;
									}
									dictionary.Add(list[i], array2);
									string hash = Util.GetHash(array2);
									num7 += (long)bytes.Length;
									text5 = string.Format(text4, "chunk" + num8 + "_md5", hash);
									array = Encoding.UTF8.GetBytes(text5);
									num7 += (long)array.Length;
									num7 += (long)bytes.Length;
									text5 = string.Format(text4, "chunk" + num8 + "id", string.Concat(list[i]));
									array = Encoding.UTF8.GetBytes(text5);
									num7 += (long)array.Length;
									num7 += (long)bytes.Length;
									string text6 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
									string text7 = string.Format(text6, "files[chunk" + num8 + "]", "chunk" + list[i], "application/octet-stream");
									byte[] bytes2 = Encoding.UTF8.GetBytes(text7);
									num7 += (long)bytes2.Length;
									num7 += (long)num9;
									i++;
									num8++;
								}
							}
							bool flag7 = !flag2 && dictionary.Count == 0;
							if (flag7)
							{
								return false;
							}
							byte[] array4 = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
							num7 += (long)array4.Length;
							httpWebRequest.ContentLength = num7;
							Stream requestStream = httpWebRequest.GetRequestStream();
							text4 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
							bool flag8 = !flag2;
							if (flag8)
							{
								int j = 0;
								int num10 = 1;
								while (j < Math.Min(list.Count, num))
								{
									requestStream.Write(bytes, 0, bytes.Length);
									byte[] array5 = dictionary[list[j]];
									int num11 = array5.Length;
									string hash2 = Util.GetHash(array5);
									text5 = string.Format(text4, "chunk" + num10 + "_md5", hash2);
									array = Encoding.UTF8.GetBytes(text5);
									requestStream.Write(array, 0, array.Length);
									requestStream.Write(bytes, 0, bytes.Length);
									text5 = string.Format(text4, "chunk" + num10 + "id", string.Concat(list[j]));
									array = Encoding.UTF8.GetBytes(text5);
									requestStream.Write(array, 0, array.Length);
									string text8 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
									string text9 = string.Format(text8, "files[chunk" + num10 + "]", "chunk" + list[j], "application/octet-stream");
									requestStream.Write(bytes, 0, bytes.Length);
									byte[] bytes3 = Encoding.UTF8.GetBytes(text9);
									requestStream.Write(bytes3, 0, bytes3.Length);
									requestStream.Write(array5, 0, num11);
									num5 += (long)num11;
									int num12 = Math.Min(100, (int)(num5 * 100L / num6));
									this.SetProgress(num12, null);
									j++;
									num10++;
								}
							}
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "op", "Submit");
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "form_id", "chunk_upload_form");
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "pfs_md5", pfsHash);
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "total_chunks", num4);
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "gamecode", this.Game.id);
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							bool flag9 = !string.IsNullOrEmpty(this.Game.diskcode);
							if (flag9)
							{
								requestStream.Write(bytes, 0, bytes.Length);
								text5 = string.Format(text4, "diskcode", this.Game.diskcode);
								array = Encoding.UTF8.GetBytes(text5);
								requestStream.Write(array, 0, array.Length);
							}
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "pfs", fileName);
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							requestStream.Write(bytes, 0, bytes.Length);
							text5 = string.Format(text4, "pfs_size", length);
							array = Encoding.UTF8.GetBytes(text5);
							requestStream.Write(array, 0, array.Length);
							array4 = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
							requestStream.Write(array4, 0, array4.Length);
							requestStream.Close();
							HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
							bool flag10 = httpWebResponse.StatusCode == HttpStatusCode.OK;
							if (flag10)
							{
								using (Stream responseStream = httpWebResponse.GetResponseStream())
								{
									using (StreamReader streamReader = new StreamReader(responseStream))
									{
										long contentLength = httpWebResponse.ContentLength;
										string text10 = streamReader.ReadToEnd();
										bool flag11 = text10.IndexOf("true") > 0;
										if (flag11)
										{
											httpWebResponse.Close();
											requestStream.Dispose();
											return true;
										}
										Dictionary<string, object> dictionary2 = new JavaScriptSerializer().Deserialize(text10, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
										bool flag12 = dictionary2.ContainsKey("remaining_chunks");
										if (flag12)
										{
											Dictionary<string, object> dictionary3 = dictionary2["remaining_chunks"] as Dictionary<string, object>;
											list = new List<int>();
											foreach (string text11 in dictionary3.Keys)
											{
												bool flag13 = !(bool)dictionary3[text11];
												if (flag13)
												{
													list.Add(int.Parse(text11));
												}
											}
											bool flag14 = list.Count == 0;
											if (flag14)
											{
												return false;
											}
										}
										bool flag15 = dictionary2.ContainsKey("status") && (string)dictionary2["status"] == "ERROR";
										if (flag15)
										{
											Util.ShowErrorMessage(dictionary2, Resources.errUnknown);
											this.RaiseDownloadFinishEvent(false, "Abort");
											return false;
										}
									}
								}
								httpWebResponse.Close();
								requestStream.Dispose();
								bool flag16 = flag2;
								if (flag16)
								{
									num6 = (long)(list.Count * num2);
									flag2 = false;
								}
							}
							else
							{
								num3++;
								bool flag17 = num3 > 3;
								if (flag17)
								{
									return false;
								}
							}
						}
						catch (Exception ex)
						{
							num3++;
							bool flag18 = num3 > 3;
							if (flag18)
							{
								SaveUploadDownloder.ErrorMessage(base.ParentForm, Resources.errUnknown, null);
								this.RaiseDownloadFinishEvent(false, "Abort");
								return false;
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException ex2)
			{
				SaveUploadDownloder.ErrorMessage(base.ParentForm, ex2.Message, "UnauthorizedAccess");
				this.RaiseDownloadFinishEvent(false, "Abort");
			}
			return false;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00097AB4 File Offset: 0x00095CB4
		public void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
		{
			string text = "";
			string text2 = "---------------------------" + DateTime.Now.Ticks.ToString("x");
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + text2 + "\r\n");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
			httpWebRequest.AllowWriteStreamBuffering = true;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + text2;
			httpWebRequest.Method = "POST";
			httpWebRequest.UserAgent = Util.GetUserAgent();
			httpWebRequest.ProtocolVersion = new Version(1, 1);
			httpWebRequest.KeepAlive = true;
			ServicePointManager.Expect100Continue = false;
			httpWebRequest.Credentials = Util.GetNetworkCredential();
			httpWebRequest.Timeout = 600000;
			httpWebRequest.ReadWriteTimeout = 600000;
			httpWebRequest.SendChunked = true;
			string text3 = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
			httpWebRequest.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
			httpWebRequest.Headers.Add("Authorization", text3);
			string text4 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
			long num = 0L;
			foreach (object obj in nvc.Keys)
			{
				string text5 = (string)obj;
				num += (long)bytes.Length;
				string text6 = string.Format(text4, text5, nvc[text5]);
				byte[] bytes2 = Encoding.UTF8.GetBytes(text6);
				num += (long)bytes2.Length;
			}
			num += (long)bytes.Length;
			string text7 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
			string text8 = string.Format(text7, paramName, file, contentType);
			byte[] array = Encoding.UTF8.GetBytes(text8);
			num += (long)array.Length;
			bool flag = true;
			bool flag2 = file != null;
			if (flag2)
			{
				num += new FileInfo(file).Length;
				byte[] array2 = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
				num += (long)array2.Length;
				httpWebRequest.ContentLength = num;
				this.start = DateTime.Now.Ticks;
				try
				{
					Stream requestStream = httpWebRequest.GetRequestStream();
					text4 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
					foreach (object obj2 in nvc.Keys)
					{
						string text9 = (string)obj2;
						requestStream.Write(bytes, 0, bytes.Length);
						string text10 = string.Format(text4, text9, nvc[text9]);
						byte[] bytes3 = Encoding.UTF8.GetBytes(text10);
						requestStream.Write(bytes3, 0, bytes3.Length);
					}
					requestStream.Write(bytes, 0, bytes.Length);
					text7 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
					text8 = string.Format(text7, paramName, Path.GetFileName(file), contentType);
					array = Encoding.UTF8.GetBytes(text8);
					requestStream.Write(array, 0, array.Length);
					FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
					byte[] array3 = new byte[4096];
					long num2 = 0L;
					long length = fileStream.Length;
					int num3;
					while ((num3 = fileStream.Read(array3, 0, array3.Length)) != 0)
					{
						num2 += (long)num3;
						this.SetProgress((int)(num2 * 100L / length), new int?(80));
						requestStream.Write(array3, 0, num3);
					}
					fileStream.Close();
					array2 = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
					requestStream.Write(array2, 0, array2.Length);
					requestStream.Close();
					File.Delete(file);
				}
				catch (Exception)
				{
					flag = false;
					this.RaiseDownloadFinishEvent(flag, Resources.errConnection);
					return;
				}
			}
			else
			{
				Stream requestStream2 = httpWebRequest.GetRequestStream();
				text4 = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
				foreach (object obj3 in nvc.Keys)
				{
					string text11 = (string)obj3;
					requestStream2.Write(bytes, 0, bytes.Length);
					string text12 = string.Format(text4, text11, nvc[text11]);
					byte[] bytes4 = Encoding.UTF8.GetBytes(text12);
					requestStream2.Write(bytes4, 0, bytes4.Length);
				}
				requestStream2.Write(bytes, 0, bytes.Length);
				requestStream2.Close();
			}
			this.RaiseUploadFinishEvent();
			this.SetProgress(0, new int?(80));
			WebResponse webResponse = null;
			try
			{
				webResponse = httpWebRequest.GetResponse();
				long contentLength = webResponse.ContentLength;
				this.RaiseDownloadStartEvent();
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text13 = streamReader.ReadToEnd();
						bool flag3 = this.Action == "list" && text13.IndexOf("[") == 0;
						if (flag3)
						{
							this.ListResult = text13;
						}
						else
						{
							try
							{
								Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize(text13, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
								bool flag4 = dictionary != null && dictionary.ContainsKey("status") && (string)dictionary["status"] == "OK";
								if (flag4)
								{
									string text14 = (string)dictionary["zip"];
									this.SetProgress(0, new int?(80));
									flag = this.DownloadZip(text14, 0L, 0);
								}
								else
								{
									bool flag5 = dictionary != null && dictionary.ContainsKey("code");
									if (flag5)
									{
										string text15 = dictionary["code"].ToString();
									}
									Util.ShowErrorMessage(dictionary, Resources.errUnknown);
									this.RaiseDownloadFinishEvent(false, "Abort");
									text = null;
									flag = false;
								}
							}
							catch (Exception ex)
							{
								text = Resources.errUnknown;
								flag = false;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				bool flag6 = webResponse != null;
				if (flag6)
				{
					webResponse.Close();
					webResponse = null;
				}
				flag = false;
			}
			finally
			{
				httpWebRequest = null;
			}
			this.RaiseDownloadFinishEvent(flag, text);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00098204 File Offset: 0x00096404
		private bool DownloadZip(string zipFile, long start, int retry = 0)
		{
			long num = 0L;
			long num2 = start;
			bool flag7;
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(Util.GetBaseUrl() + zipFile) as HttpWebRequest;
				httpWebRequest.Method = "GET";
				httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
				httpWebRequest.PreAuthenticate = true;
				httpWebRequest.UserAgent = Util.GetUserAgent();
				httpWebRequest.Credentials = Util.GetNetworkCredential();
				httpWebRequest.Timeout = 300000;
				httpWebRequest.ReadWriteTimeout = 300000;
				string text = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
				httpWebRequest.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
				httpWebRequest.Headers.Add("Authorization", text);
				httpWebRequest.AddRange(start);
				WebResponse response = httpWebRequest.GetResponse();
				num = response.ContentLength;
				using (Stream responseStream = response.GetResponseStream())
				{
					byte[] array = new byte[4096];
					string tempFileName = Path.GetTempFileName();
					using (FileStream fileStream = File.OpenWrite(tempFileName))
					{
						for (;;)
						{
							int num3 = responseStream.Read(array, 0, array.Length);
							bool flag = num3 == 0;
							if (flag)
							{
								break;
							}
							fileStream.Write(array, 0, num3);
							num2 += (long)num3;
							this.SetProgress((int)(num2 * 100L / num), null);
						}
					}
					this.SetProgress(0, new int?(90));
					string text2 = this.EnsureSpace();
					try
					{
						this.OrderedEntries = this.ExtractZip(tempFileName);
						bool flag2 = this.Action == "resign" && !this.OrderedEntries.Contains(Path.GetFileName(this.Game.LocalSaveFolder));
						if (flag2)
						{
							bool flag3 = !Directory.Exists(Path.GetPathRoot(this.OutputFolder));
							if (flag3)
							{
								Util.ShowMessage(Resources.errResignNoUSB);
								return false;
							}
							Directory.CreateDirectory(this.OutputFolder);
							File.Copy(this.Game.LocalSaveFolder, Path.Combine(this.OutputFolder, Path.GetFileName(this.Game.LocalSaveFolder)), true);
						}
					}
					catch (Exception)
					{
						bool flag4 = text2 != null;
						if (flag4)
						{
							bool flag5 = this.Action == "resign";
							if (flag5)
							{
								File.Copy(text2, this.OutputFolder);
							}
							else
							{
								File.Copy(text2, this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4));
							}
						}
					}
					bool flag6 = text2 != null;
					if (flag6)
					{
						File.Delete(text2);
					}
					flag7 = true;
				}
			}
			catch (Exception ex)
			{
				bool flag8 = (num > 0L && num2 == num) || retry > 3;
				if (flag8)
				{
					flag7 = false;
				}
				else
				{
					flag7 = this.DownloadZip(zipFile, num2, retry++);
				}
			}
			return flag7;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00098554 File Offset: 0x00096754
		private string EnsureSpace()
		{
			bool flag = this.Action == "decrypt" || this.Action == "list";
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(this.Game.LocalSaveFolder));
				string text2 = this.Game.LocalSaveFolder.Substring(0, this.Game.LocalSaveFolder.Length - 4);
				bool flag2 = driveInfo.AvailableFreeSpace > new FileInfo(text2).Length;
				if (flag2)
				{
					text = null;
				}
				else
				{
					string text3 = Path.GetTempFileName();
					bool flag3 = Util.GetRegistryValue("BackupSaves") == "false";
					if (flag3)
					{
						File.Copy(text2, text3, true);
					}
					else
					{
						text3 = null;
					}
					File.Delete(text2);
					text = text3;
				}
			}
			return text;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00098628 File Offset: 0x00096828
		private List<string> ExtractZip(string tempFile)
		{
			List<string> list = new List<string>();
			ZipFile zipFile = new ZipFile(tempFile);
			foreach (ZipEntry zipEntry in zipFile.Entries)
			{
				list.Add(zipEntry.FileName);
			}
			zipFile.ExtractProgress += this.zipFile_ExtractProgress;
			zipFile.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
			zipFile.ExtractAll(this.OutputFolder);
			list.Reverse();
			return list;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000986C4 File Offset: 0x000968C4
		private void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e)
		{
			bool flag = e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten;
			if (flag)
			{
				this.SetProgress((int)(e.BytesTransferred * 100L / e.TotalBytesToTransfer), null);
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00098704 File Offset: 0x00096904
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0009873C File Offset: 0x0009693C
		private void InitializeComponent()
		{
			this.lblStatus = new Label();
			this.lblCurrentProgress = new Label();
			this.lblTotalProgress = new Label();
			this.pbOverallProgress = new PS4ProgressBar();
			this.pbProgress = new PS4ProgressBar();
			base.SuspendLayout();
			this.lblStatus.ForeColor = Color.White;
			this.lblStatus.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(10));
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = Util.ScaleSize(new Size(400, 13));
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "Text";
			this.lblCurrentProgress.AutoSize = true;
			this.lblCurrentProgress.ForeColor = Color.White;
			this.lblCurrentProgress.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(30));
			this.lblCurrentProgress.Name = "lblCurrentProgress";
			this.lblCurrentProgress.Size = Util.ScaleSize(new Size(85, 13));
			this.lblCurrentProgress.TabIndex = 3;
			this.lblCurrentProgress.Text = "Current Progress";
			this.lblTotalProgress.AutoSize = true;
			this.lblTotalProgress.ForeColor = Color.White;
			this.lblTotalProgress.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(91));
			this.lblTotalProgress.Name = "lblTotalProgress";
			this.lblTotalProgress.Size = Util.ScaleSize(new Size(75, 13));
			this.lblTotalProgress.TabIndex = 4;
			this.lblTotalProgress.Text = "Total Progress";
			this.pbOverallProgress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.pbOverallProgress.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(113));
			this.pbOverallProgress.Name = "pbOverallProgress";
			this.pbOverallProgress.Size = Util.ScaleSize(new Size(424, 23));
			this.pbOverallProgress.TabIndex = 2;
			this.pbProgress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.pbProgress.Location = new Point(Util.ScaleSize(11), Util.ScaleSize(52));
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = Util.ScaleSize(new Size(424, 23));
			this.pbProgress.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(Util.ScaleSize(6f), Util.ScaleSize(13f));
			base.AutoScaleMode = AutoScaleMode.None;
			this.BackColor = Color.FromArgb(102, 102, 102);
			base.Controls.Add(this.lblTotalProgress);
			base.Controls.Add(this.lblCurrentProgress);
			base.Controls.Add(this.pbOverallProgress);
			base.Controls.Add(this.pbProgress);
			base.Controls.Add(this.lblStatus);
			base.Name = "SaveUploadDownloder";
			base.Size = Util.ScaleSize(new Size(446, 151));
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000C53 RID: 3155
		private SaveUploadDownloder.UpdateProgressDelegate UpdateProgress;

		// Token: 0x04000C54 RID: 3156
		private SaveUploadDownloder.UpdateStatusDelegate UpdateStatus;

		// Token: 0x04000C56 RID: 3158
		private static string UPLOAD_URL = "/request?token={0}";

		// Token: 0x04000C57 RID: 3159
		private const string SESSION_CHECK_URL = "{{\"action\":\"SESSION_REFRESH\",\"userid\":\"{0}\",\"token\":\"{1}\"}}";

		// Token: 0x04000C68 RID: 3176
		private Thread t = null;

		// Token: 0x04000C69 RID: 3177
		private long start = 0L;

		// Token: 0x04000C6A RID: 3178
		private IContainer components = null;

		// Token: 0x04000C6B RID: 3179
		private Label lblStatus;

		// Token: 0x04000C6C RID: 3180
		private PS4ProgressBar pbProgress;

		// Token: 0x04000C6D RID: 3181
		private PS4ProgressBar pbOverallProgress;

		// Token: 0x04000C6E RID: 3182
		private Label lblCurrentProgress;

		// Token: 0x04000C6F RID: 3183
		private Label lblTotalProgress;

		// Token: 0x020002BF RID: 703
		// (Invoke) Token: 0x06001E8E RID: 7822
		private delegate void UpdateProgressDelegate(int value, int? overall);

		// Token: 0x020002C0 RID: 704
		// (Invoke) Token: 0x06001E92 RID: 7826
		private delegate void UpdateStatusDelegate(string status);

		// Token: 0x020002C1 RID: 705
		// (Invoke) Token: 0x06001E96 RID: 7830
		public delegate void DownloadStartEventHandler(object sender, EventArgs e);

		// Token: 0x020002C2 RID: 706
		// (Invoke) Token: 0x06001E9A RID: 7834
		public delegate void UploadStartEventHandler(object sender, EventArgs e);

		// Token: 0x020002C3 RID: 707
		// (Invoke) Token: 0x06001E9E RID: 7838
		public delegate void DownloadFinishEventHandler(object sender, DownloadFinishEventArgs e);

		// Token: 0x020002C4 RID: 708
		// (Invoke) Token: 0x06001EA2 RID: 7842
		public delegate void UploadFinishEventHandler(object sender, EventArgs e);
	}
}
