using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor
{
	// Token: 0x020001E8 RID: 488
	internal class Util
	{
		// Token: 0x0600198D RID: 6541
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600198E RID: 6542
		[DllImport("__Internal", EntryPoint = "mono_get_runtime_build_info")]
		public static extern string GetMonoVersion();

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x000A273F File Offset: 0x000A093F
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x000A2746 File Offset: 0x000A0946
		public static Util.Platform CurrentPlatform { get; private set; } = Util.GetRunningPlatform();

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x000A274E File Offset: 0x000A094E
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x000A2755 File Offset: 0x000A0955
		public static bool IsHyperkinMode { get; set; } = false;

		// Token: 0x06001993 RID: 6547 RVA: 0x000A2760 File Offset: 0x000A0960
		public static bool IsHyperkin()
		{
			return Util.IsHyperkinMode;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000A2778 File Offset: 0x000A0978
		private static Util.Platform GetRunningPlatform()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.Win32NT:
			case PlatformID.WinCE:
				return Util.Platform.Windows;
			case PlatformID.Unix:
			{
				bool flag = Directory.Exists("/Applications") & Directory.Exists("/System") & Directory.Exists("/Users") & Directory.Exists("/Volumes");
				if (flag)
				{
					return Util.Platform.MacOS;
				}
				return Util.Platform.Linux;
			}
			case PlatformID.MacOSX:
				return Util.Platform.MacOS;
			}
			return Util.Platform.Other;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000A27FC File Offset: 0x000A09FC
		internal static bool IsUnixOrMacOSX()
		{
			int platform = (int)Environment.OSVersion.Platform;
			return platform == 4 || platform == 6 || platform == 128;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000A2838 File Offset: 0x000A0A38
		public static bool IsOldMono()
		{
			bool flag = Util.CurrentPlatform == Util.Platform.Windows;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = Util.GetMonoVersion();
				text = text.Substring(0, text.IndexOf("("));
				Version version;
				Version.TryParse(text, out version);
				flag2 = version < Version.Parse("5.12.0.226");
			}
			return flag2;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000A288C File Offset: 0x000A0A8C
		public static bool IsBigScreen()
		{
			return Screen.PrimaryScreen.Bounds.Width > 2000;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000A28C0 File Offset: 0x000A0AC0
		public static string GetFontFamily()
		{
			bool flag = Util.CurrentPlatform == Util.Platform.Linux;
			string text;
			if (flag)
			{
				text = "Noto Sans CJK SC";
			}
			else
			{
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					text = "Arial Unicode MS";
				}
				else
				{
					text = "Microsoft Sans Serif";
				}
			}
			return text;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000A2900 File Offset: 0x000A0B00
		public static Font GetFontForPlatform(Font defaultFont)
		{
			float num = Util.ScaleSize(defaultFont.Size);
			bool flag = Util.CurrentPlatform == Util.Platform.Linux;
			Font font;
			if (flag)
			{
				font = new Font("Noto Sans CJK SC", num, defaultFont.Style, defaultFont.Unit, defaultFont.GdiCharSet);
			}
			else
			{
				bool flag2 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag2)
				{
					font = new Font("Arial Unicode MS", num, defaultFont.Style, defaultFont.Unit, defaultFont.GdiCharSet);
				}
				else
				{
					font = new Font("Microsoft Sans Serif", num, defaultFont.Style, defaultFont.Unit, defaultFont.GdiCharSet);
				}
			}
			return font;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000A2994 File Offset: 0x000A0B94
		public static string GetBackupLocation()
		{
			string registryValue = Util.GetRegistryValue("Location");
			bool flag = !string.IsNullOrEmpty(registryValue);
			if (flag)
			{
				try
				{
					Directory.CreateDirectory(registryValue);
					string fullPath = Path.GetFullPath(registryValue);
					bool flag2 = Path.IsPathRooted(registryValue);
					if (flag2)
					{
						return registryValue;
					}
					throw new Exception();
				}
				catch
				{
					Util.DeleteRegistryValue("Location");
					bool flag3 = Util.GetRegistryValue("BackupSaves") != "false";
					if (flag3)
					{
						Util.ShowMessage(Resources.msgBadBackupPath, Resources.msgInfo);
					}
				}
			}
			string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar.ToString();
			string text2 = text + "PS4Saves_Backup";
			text2 = text + "Save Wizard for PS4" + Path.DirectorySeparatorChar.ToString() + "PS4 Saves Backup";
			Directory.CreateDirectory(text2);
			return text2;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000A2A84 File Offset: 0x000A0C84
		public static string GetGamelistPath()
		{
			string tempPath = Path.GetTempPath();
			string text = "SWP";
			string text2 = Path.Combine(tempPath, text);
			Directory.CreateDirectory(text2);
			return text2 + Path.DirectorySeparatorChar.ToString() + "gamelist";
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000A2ACC File Offset: 0x000A0CCC
		internal static string GetOSVersion()
		{
			string text = string.Empty;
			try
			{
				bool flag = Util.IsUnixOrMacOSX();
				if (flag)
				{
					text = CurrentOS.Name;
				}
				else
				{
					text = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "").ToString();
				}
			}
			catch
			{
			}
			return string.Format("{0} ({1})", text, Environment.OSVersion.ToString());
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000A2B40 File Offset: 0x000A0D40
		public static string GetProductID()
		{
			string empty = string.Empty;
			try
			{
			}
			catch
			{
			}
			return string.Format("{0} ({1})", empty, Util.pid.ToString());
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000A2B88 File Offset: 0x000A0D88
		internal static string GetFramework()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			try
			{
				bool flag = Type.GetType("Mono.Runtime") != null && Util.IsUnixOrMacOSX();
				if (flag)
				{
					return string.Format("Mono {0}", Util.GetMonoVersion());
				}
				bool flag2 = Type.GetType("Core.Runtime") != null;
				if (flag2)
				{
					return string.Format(".Net Core {0}", entryAssembly.ImageRuntimeVersion);
				}
			}
			catch
			{
			}
			string text = Util.Get45or451FromRegistry();
			bool flag3 = text == null;
			if (flag3)
			{
				text = "< 4.5";
			}
			return string.Format(".Net Framework {0} ({1})", text, entryAssembly.ImageRuntimeVersion);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000A2C3C File Offset: 0x000A0E3C
		private static string Get45or451FromRegistry()
		{
			string text;
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
			{
				int num = Convert.ToInt32(registryKey.GetValue("Release"));
				text = Util.CheckFor45DotVersion(num);
			}
			return text;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000A2C9C File Offset: 0x000A0E9C
		private static string CheckFor45DotVersion(int releaseKey)
		{
			bool flag = releaseKey >= 461808;
			string text;
			if (flag)
			{
				text = "4.7.2 or later";
			}
			else
			{
				bool flag2 = releaseKey >= 461308;
				if (flag2)
				{
					text = "4.7.1 or later";
				}
				else
				{
					bool flag3 = releaseKey >= 460798;
					if (flag3)
					{
						text = "4.7 or later";
					}
					else
					{
						bool flag4 = releaseKey >= 394802;
						if (flag4)
						{
							text = "4.6.2 or later";
						}
						else
						{
							bool flag5 = releaseKey >= 394254;
							if (flag5)
							{
								text = "4.6.1 or later";
							}
							else
							{
								bool flag6 = releaseKey >= 393295;
								if (flag6)
								{
									text = "4.6 or later";
								}
								else
								{
									bool flag7 = releaseKey >= 393273;
									if (flag7)
									{
										text = "4.6 RC or later";
									}
									else
									{
										bool flag8 = releaseKey >= 379893;
										if (flag8)
										{
											text = "4.5.2 or later";
										}
										else
										{
											bool flag9 = releaseKey >= 378675;
											if (flag9)
											{
												text = "4.5.1 or later";
											}
											else
											{
												bool flag10 = releaseKey >= 378389;
												if (flag10)
												{
													text = "4.5 or later";
												}
												else
												{
													text = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000A2DBC File Offset: 0x000A0FBC
		internal static string GetUserId()
		{
			return Util.GetRegistryValue("User");
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000A2DD8 File Offset: 0x000A0FD8
		internal static void ChangeServer()
		{
			Util.CURRENT_SERVER = new Random().Next(0, Util.SERVERS.Length);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000A2DF2 File Offset: 0x000A0FF2
		internal static void ChangeAuthServer()
		{
			Util.CURRENT_AUTH_SERVER = new Random().Next(0, Util.AUTH_SERVERS.Length);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000A2E0C File Offset: 0x000A100C
		internal static string GetRegistryValue(string key)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase());
			bool flag = registryKey == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				try
				{
					string text2 = registryKey.GetValue(key) as string;
					registryKey.Close();
					currentUser.Close();
					return text2;
				}
				catch (Exception)
				{
				}
				text = null;
			}
			return text;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000A2E78 File Offset: 0x000A1078
		internal static void DeleteRegistryValue(string key)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			RegistryKey registryKey = currentUser.OpenSubKey(Util.GetRegistryBase(), true);
			bool flag = registryKey == null;
			if (!flag)
			{
				try
				{
					registryKey.DeleteValue(key);
				}
				catch (Exception)
				{
				}
				finally
				{
					registryKey.Close();
					currentUser.Close();
				}
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000A2EE4 File Offset: 0x000A10E4
		internal static void SetRegistryValue(string key, string value)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			RegistryKey registryKey = currentUser.CreateSubKey(Util.GetRegistryBase());
			bool flag = registryKey == null;
			if (!flag)
			{
				registryKey.SetValue(key, value);
				registryKey.Close();
				currentUser.Close();
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000A2F28 File Offset: 0x000A1128
		internal static bool IsMatch(string a, string pattern)
		{
			return Regex.IsMatch(a, "^" + pattern + "$");
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000A2F50 File Offset: 0x000A1150
		internal static string GetAdapterName(bool blackListed = false)
		{
			string text;
			if (blackListed)
			{
				text = null;
			}
			else
			{
				text = Util.GetRegistryValue("Adapter");
			}
			return text;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000A2F78 File Offset: 0x000A1178
		internal static string GetUID(bool blackListed = false, bool register = false)
		{
			bool flag = Util.IsUnixOrMacOSX();
			string text;
			if (flag)
			{
				text = Util.GetUIDInMonoRunning();
			}
			else
			{
				string text2 = Environment.ExpandEnvironmentVariables("%SYSTEMDRIVE%");
				string text3 = Util.GetUIDFromWMI(text2);
				text3 = text3.Substring(text3.IndexOf('{') + 1, text3.IndexOf('}') - text3.IndexOf('{') - 1);
				text = text3;
			}
			return text;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000A2FD4 File Offset: 0x000A11D4
		internal static string GetDataPath(string strBase)
		{
			bool flag = string.IsNullOrWhiteSpace(strBase);
			string text;
			if (flag)
			{
				text = strBase;
			}
			else
			{
				bool flag2 = Util.IsUnixOrMacOSX();
				string text2;
				if (flag2)
				{
					text2 = strBase + "/PS4/SAVEDATA";
				}
				else
				{
					bool flag3 = strBase.Substring(strBase.Length - 1, 1) != "\\" && !strBase.ToLowerInvariant().Contains("ps4");
					if (flag3)
					{
						text2 = strBase + "\\PS4\\SAVEDATA";
					}
					else
					{
						bool flag4 = strBase.Substring(strBase.Length - 1, 1) != "\\" && strBase.ToLowerInvariant().Contains("ps4") && !strBase.ToLowerInvariant().Contains("savedata");
						if (flag4)
						{
							text2 = strBase + "\\PS4\\SAVEDATA";
						}
						else
						{
							bool flag5 = strBase.Substring(strBase.Length - 1, 1) != "\\" && strBase.ToLowerInvariant().Contains("ps4") && strBase.ToLowerInvariant().Contains("savedata");
							if (flag5)
							{
								return strBase;
							}
							text2 = strBase + "PS4\\SAVEDATA";
						}
					}
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000A3108 File Offset: 0x000A1308
		private static string GetUIDInMonoRunning()
		{
			try
			{
				bool flag = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag)
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo
					{
						FileName = "sh",
						Arguments = "-c \"ioreg -rd1 -c IOPlatformExpertDevice | awk '/IOPlatformUUID/'\"",
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						RedirectStandardInput = true,
						UserName = Environment.UserName
					};
					StringBuilder stringBuilder = new StringBuilder();
					using (Process process = Process.Start(processStartInfo))
					{
						process.WaitForExit();
						stringBuilder.Append(process.StandardOutput.ReadToEnd());
					}
					return stringBuilder.ToString(26, 36);
				}
				return "ade15a18-a80b-469c-ab20-eb2df3f88156";
			}
			catch (Exception)
			{
				Util.ShowMessage(Util.PRODUCT_NAME + " can not start. It didn't get unique device id");
			}
			return null;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000A3204 File Offset: 0x000A1404
		internal static string GetSerial()
		{
			return Util.GetRegistryValue("Serial");
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000A3220 File Offset: 0x000A1420
		internal static string GetHtaccessUser()
		{
			return Program.HTACCESS_USER[Util.CURRENT_USER_PWD];
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000A3240 File Offset: 0x000A1440
		internal static string GetHtaccessPwd()
		{
			return Program.HTACCESS_PWD[Util.CURRENT_USER_PWD];
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000A3260 File Offset: 0x000A1460
		internal static NetworkCredential GetNetworkCredential()
		{
			return new NetworkCredential(Util.GetHtaccessUser(), Util.GetHtaccessPwd());
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000A3284 File Offset: 0x000A1484
		internal static string GetBaseUrl()
		{
			bool flag = string.IsNullOrEmpty(Util.forceServer);
			string text;
			if (flag)
			{
				text = "http://" + Util.SERVERS[Util.CURRENT_SERVER];
			}
			else
			{
				text = "http://" + Util.forceServer;
			}
			return text;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000A32CC File Offset: 0x000A14CC
		internal static string GetAuthBaseUrl()
		{
			bool flag = string.IsNullOrEmpty(Util.forceAuthServer);
			string text;
			if (flag)
			{
				text = "http://" + Util.AUTH_SERVERS[Util.CURRENT_AUTH_SERVER];
			}
			else
			{
				text = "http://" + Util.forceAuthServer;
			}
			return text;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000A3313 File Offset: 0x000A1513
		internal static void SetAuthToken(string Token)
		{
			Util.SessionToken = Token;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000A331C File Offset: 0x000A151C
		internal static string GetAuthToken()
		{
			return Util.SessionToken;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000A3334 File Offset: 0x000A1534
		internal static string GetUserAgent()
		{
			string text = "Save Wizard for PS4 ";
			text += "1.0.7422.29556";
			return text + " (" + Util.GetRunningPlatform().ToString() + ")";
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000A3384 File Offset: 0x000A1584
		private static string GetUIDFromWMI(string sysDrive)
		{
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM   Win32_Volume WHERE  DriveLetter = '" + sysDrive + "'");
				ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
				string text = null;
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					bool flag = text == null;
					if (!flag)
					{
						break;
					}
					object propertyValue = managementObject.GetPropertyValue("DeviceID");
					bool flag2 = propertyValue != null;
					if (flag2)
					{
						text = propertyValue.ToString();
					}
					text.Substring(text.IndexOf('{') + 1).TrimEnd(new char[] { '\\' }).TrimEnd(new char[] { '}' });
				}
				return text;
			}
			catch (Exception)
			{
				Util.ShowMessage(Util.PRODUCT_NAME + " can not start. Please make sure Windows Management Instrumentation service is running.");
			}
			return null;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000A3488 File Offset: 0x000A1688
		internal static void ClearTemp()
		{
			try
			{
				string tempFolder = Util.GetTempFolder();
				string[] files = Directory.GetFiles(tempFolder);
				foreach (string text in files)
				{
					bool flag = text.IndexOf("Log.txt") > 0;
					if (!flag)
					{
						File.Delete(text);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000A34F4 File Offset: 0x000A16F4
		internal static string GetTempFolder()
		{
			string text = Path.GetTempPath();
			string text2 = "SWPS4MAX";
			text = Path.Combine(text, text2);
			Directory.CreateDirectory(text);
			return text + "/";
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000A352C File Offset: 0x000A172C
		internal static string GetRegistryBase()
		{
			return "SOFTWARE\\DataPower\\Save Wizard for PS4";
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000A3548 File Offset: 0x000A1748
		internal static string GetDevLogFile()
		{
			return Path.Combine(Util.GetTempFolder(), "DevLog.txt");
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000A356C File Offset: 0x000A176C
		internal static string GetRegion(Dictionary<int, string> regionMap, int p, string exregions)
		{
			string text = "";
			foreach (int num in regionMap.Keys)
			{
				bool flag = (p & num) > 0 && exregions.IndexOf(regionMap[num]) < 0;
				if (flag)
				{
					text = text + "[" + regionMap[num] + "]";
				}
			}
			return text;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000A3604 File Offset: 0x000A1804
		internal static byte[] GetPSNId(string saveFolder)
		{
			string text = Path.Combine(saveFolder, "PARAM.SFO");
			return Encoding.UTF8.GetBytes(MainForm.GetParamInfo(text, "ACCOUNT_ID"));
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000A3638 File Offset: 0x000A1838
		internal static bool GetCache(string hash)
		{
			try
			{
				WebClientEx webClientEx = new WebClientEx();
				webClientEx.Headers[HttpRequestHeader.UserAgent] = Util.GetUserAgent();
				webClientEx.Credentials = Util.GetNetworkCredential();
				byte[] array = webClientEx.UploadData(Util.GetBaseUrl() + "/request_cache?token=" + Util.GetAuthToken(), Encoding.ASCII.GetBytes("{\"pfs\":\"" + hash + "\"}"));
				string @string = Encoding.ASCII.GetString(array);
				return @string.IndexOf("true") > 0;
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000A36D8 File Offset: 0x000A18D8
		internal static string GetHash(byte[] buf)
		{
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(buf);
			return BitConverter.ToString(array).Replace("-", "").ToLower();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000A3714 File Offset: 0x000A1914
		internal static string GetHash(string file)
		{
			MD5 md = MD5.Create();
			string text;
			using (FileStream fileStream = File.OpenRead(file))
			{
				byte[] array = md.ComputeHash(fileStream);
				text = BitConverter.ToString(array).Replace("-", "").ToLower();
			}
			return text;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000A3774 File Offset: 0x000A1974
		internal static string GetCacheFolder(game game, string curCacheFolder)
		{
			string text = Path.Combine(Util.GetBackupLocation(), "cache");
			string text2 = Path.Combine(text, game.id);
			bool flag = string.IsNullOrEmpty(curCacheFolder);
			string text3;
			if (flag)
			{
				text3 = text2;
			}
			else
			{
				text3 = Path.Combine(text2, curCacheFolder);
			}
			return text3;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000A37B8 File Offset: 0x000A19B8
		internal static void ShowErrorMessage(Dictionary<string, object> res, string msgfallback)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			bool flag = res != null;
			if (flag)
			{
				bool flag2 = res.ContainsKey("code");
				if (flag2)
				{
					text = Convert.ToString(res["code"]);
				}
				bool flag3 = res.ContainsKey("id");
				if (flag3)
				{
					text2 = res["id"] as string;
				}
				bool flag4 = res.ContainsKey("pid");
				if (flag4)
				{
					text3 = res["pid"] as string;
					Util.pid = Convert.ToInt32(text3);
				}
				bool flag5 = res.ContainsKey("cid");
				if (flag5)
				{
					string text4 = res["cid"] as string;
				}
			}
			string text5 = Resources.ResourceManager.GetString("err" + text);
			bool flag6 = res.ContainsKey("msg");
			if (flag6)
			{
				Dictionary<string, object> dictionary = res["msg"] as Dictionary<string, object>;
				string name = Thread.CurrentThread.CurrentUICulture.Name;
				string twoLetterISOLanguageName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
				bool flag7 = dictionary != null && dictionary.ContainsKey(name);
				if (flag7)
				{
					text5 = dictionary[name] as string;
				}
				else
				{
					bool flag8 = dictionary != null && dictionary.ContainsKey(twoLetterISOLanguageName);
					if (flag8)
					{
						text5 = dictionary[twoLetterISOLanguageName] as string;
					}
				}
			}
			bool flag9 = string.IsNullOrEmpty(text5);
			if (flag9)
			{
				text5 = msgfallback;
			}
			bool flag10 = res.ContainsKey("onhold") && text5.IndexOf("onhold", StringComparison.InvariantCultureIgnoreCase) >= 0;
			if (flag10)
			{
				int num = 0;
				bool flag11 = int.TryParse(res["onhold"] as string, out num);
				if (flag11)
				{
					num /= 3600000;
					text5 = text5.Replace("{onhold}", num.ToString());
				}
			}
			string text6 = null;
			bool flag12 = text5 != null;
			if (flag12)
			{
				bool flag13 = text5.IndexOf("%support%", StringComparison.InvariantCultureIgnoreCase) > 0;
				if (flag13)
				{
					text6 = ((!Util.IsHyperkin()) ? "https://www.savewizard.net/support-sw-issue.php" : "http://www.thesavewizard.com/support.php");
				}
				bool flag14 = text2 != null && !string.IsNullOrEmpty(text2.ToString());
				if (flag14)
				{
					text6 = ((!Util.IsHyperkin()) ? ("https://www.savewizard.net/support-sw-issue.php?error=" + text2) : "http://www.thesavewizard.com/support.php");
				}
				text5 = text5.Replace("%prod_ln%", Util.PRODUCT_NAME).Replace("%support%", Resources.support);
				bool flag15 = text2 != null;
				if (flag15)
				{
					text5 += string.Format(" ({0}:{1})", text, text2);
				}
				else
				{
					text5 += string.Format(" ({0})", text);
				}
				text5 += string.Format("{0}", Environment.NewLine);
				text5 += string.Format("PID is ({0})", text3);
				LinkMessageBox linkMessageBox = new LinkMessageBox(text5, text6);
				linkMessageBox.ShowDialog(Form.ActiveForm);
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000A3ABF File Offset: 0x000A1CBF
		internal static void SetMinFileSize(int v)
		{
			Util.MinFileSize = v;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000A3AC8 File Offset: 0x000A1CC8
		internal static void SetMaxFileSize(int v)
		{
			Util.MaxFileSize = v;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000A3AD4 File Offset: 0x000A1CD4
		internal static int GetMinFileSize()
		{
			return Util.MinFileSize;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000A3AEC File Offset: 0x000A1CEC
		internal static int GetMaxFileSize()
		{
			return Util.MaxFileSize;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000A3B04 File Offset: 0x000A1D04
		internal static bool HasWritePermission(string folderPath)
		{
			string text = Path.Combine(Path.GetDirectoryName(folderPath), "file.test");
			try
			{
				File.WriteAllText(text, "test text");
				File.Delete(text);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x000A3B58 File Offset: 0x000A1D58
		internal static bool IsNeedToShowUpdateScreen
		{
			get
			{
				bool flag = Util.AvailableVersion == "0.0";
				if (flag)
				{
					Util.AvailableVersion = Util.readVersionFromSite();
				}
				Version version = new Version(Util.AvailableVersion);
				Version version2 = new Version(AboutBox1.AssemblyVersion);
				return version > version2;
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000A3BA4 File Offset: 0x000A1DA4
		private static string readVersionFromSite()
		{
			string text = "0.0";
			try
			{
				WebClient webClient = new WebClient();
				Stream stream = webClient.OpenRead(Util.VERSION_FILE_URL);
				StreamReader streamReader = new StreamReader(stream);
				text = streamReader.ReadToEnd();
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000A3BF8 File Offset: 0x000A1DF8
		internal static string GetCheatsLocationFromRegistry()
		{
			string text = string.Empty;
			try
			{
				text = Util.GetRegistryValue("CheatsLocalPath");
				bool flag = (Directory.Exists(text) && Util.IsPathToCheats(text)) || text == null;
				if (flag)
				{
					return text;
				}
				Util.DeleteRegistryValue("CheatsLocalPath");
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000A3C60 File Offset: 0x000A1E60
		internal static bool IsPathToCheats(string pathToCheats)
		{
			bool flag = string.IsNullOrEmpty(pathToCheats);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = Util.CurrentPlatform == Util.Platform.MacOS && !Directory.Exists(pathToCheats);
				if (flag3)
				{
					pathToCheats = string.Format("/Volumes{0}", pathToCheats);
				}
				else
				{
					bool flag4 = Util.CurrentPlatform == Util.Platform.Linux && !Directory.Exists(pathToCheats);
					if (flag4)
					{
						pathToCheats = string.Format("/media/{0}{1}", Environment.UserName, pathToCheats);
					}
				}
				bool flag5 = !Directory.Exists(pathToCheats);
				if (flag5)
				{
					flag2 = false;
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(pathToCheats);
					bool flag6 = !directoryInfo.Exists;
					if (flag6)
					{
						flag2 = false;
					}
					else
					{
						DirectoryInfo[] directories = directoryInfo.GetDirectories();
						bool flag7 = directories != null && directories.Length != 0;
						if (flag7)
						{
							foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
							{
								bool flag8 = directoryInfo2.FullName.ToLowerInvariant().Contains("ps4");
								if (flag8)
								{
									foreach (DirectoryInfo directoryInfo3 in directoryInfo2.GetDirectories())
									{
										bool flag9 = directoryInfo3.FullName.ToLowerInvariant().Contains("savedata");
										if (flag9)
										{
											return true;
										}
									}
								}
							}
						}
						flag2 = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000A3DB8 File Offset: 0x000A1FB8
		internal static string GetShortPath(string folderPath)
		{
			int num = folderPath.ToLowerInvariant().LastIndexOf("ps4", StringComparison.InvariantCultureIgnoreCase);
			bool flag = num < 0;
			string text;
			if (flag)
			{
				text = folderPath;
			}
			else
			{
				string text2 = folderPath.Substring(0, num);
				bool flag2 = Util.IsPathToCheats(text2);
				if (flag2)
				{
					text = text2;
				}
				else
				{
					text = folderPath;
				}
			}
			return text;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000A3E02 File Offset: 0x000A2002
		internal static void SaveCheatsPathToRegistry(string folderPath)
		{
			Util.SetRegistryValue("CheatsLocalPath", folderPath);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000A3E14 File Offset: 0x000A2014
		public static DialogResult ShowMessage(string text)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(text);
			}
			else
			{
				dialogResult = MessageBox.Show(text);
			}
			return dialogResult;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000A3E4C File Offset: 0x000A204C
		public static DialogResult ShowMessage(string text, string caption)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
			}
			else
			{
				dialogResult = MessageBox.Show(text, caption);
			}
			return dialogResult;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000A3E88 File Offset: 0x000A2088
		public static DialogResult ShowMessage(Form owner, string text)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(owner, text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
			}
			else
			{
				dialogResult = MessageBox.Show(owner, text);
			}
			return dialogResult;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000A3ECC File Offset: 0x000A20CC
		public static DialogResult ShowMessage(string text, string caption, MessageBoxButtons buttons)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
			}
			else
			{
				dialogResult = MessageBox.Show(text, caption, buttons);
			}
			return dialogResult;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000A3F0C File Offset: 0x000A210C
		public static DialogResult ShowMessage(Form owner, string text, string caption)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
			}
			else
			{
				dialogResult = MessageBox.Show(owner, text, caption);
			}
			return dialogResult;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000A3F4C File Offset: 0x000A214C
		public static DialogResult ShowMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
			}
			else
			{
				dialogResult = MessageBox.Show(text, caption, buttons, icon);
			}
			return dialogResult;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000A3F8C File Offset: 0x000A218C
		public static DialogResult ShowMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			bool flag = Util.IsUnixOrMacOSX() || Util.ScaleIndex > 1;
			DialogResult dialogResult;
			if (flag)
			{
				dialogResult = CustomMsgBox.Show(text, caption, buttons, icon, defaultButton);
			}
			else
			{
				dialogResult = MessageBox.Show(text, caption, buttons, icon, defaultButton);
			}
			return dialogResult;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000A3FD0 File Offset: 0x000A21D0
		internal static void ProcedArguments(string[] args)
		{
			foreach (string text in args)
			{
				bool flag = text.ToLowerInvariant().Contains("tryserver=");
				if (flag)
				{
					bool flag2 = text.Contains("/");
					if (flag2)
					{
						Util.SERVERS = text.Substring(10).Split(new char[] { '/' });
					}
					else
					{
						Util.forceServer = text.Substring(10);
					}
				}
				bool flag3 = text.ToLowerInvariant().Contains("tryauthserver=");
				if (flag3)
				{
					bool flag4 = text.Contains("/");
					if (flag4)
					{
						Util.AUTH_SERVERS = text.Substring(14).Split(new char[] { '/' });
					}
					else
					{
						Util.forceAuthServer = text.Substring(14);
					}
				}
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x000A40A4 File Offset: 0x000A22A4
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x000A4124 File Offset: 0x000A2324
		public static int ScaleIndex
		{
			get
			{
				bool flag = Util._scaleIndex >= 0;
				int num;
				if (flag)
				{
					num = Util._scaleIndex;
				}
				else
				{
					try
					{
						string registryValue = Util.GetRegistryValue("SelectedScaleIndex");
						int.TryParse(registryValue, out Util._scaleIndex);
						bool flag2 = Util._scaleIndex == 0;
						if (flag2)
						{
							Util._scaleIndex = (Util.IsBigScreen() ? 5 : 1);
						}
					}
					catch
					{
					}
					num = Util._scaleIndex;
				}
				return num;
			}
			set
			{
				Util._scaleIndex = value;
				Util.SetRegistryValue("SelectedScaleIndex", value.ToString());
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000A4140 File Offset: 0x000A2340
		public static int ScaleSize(int size)
		{
			int num;
			switch (Util.ScaleIndex)
			{
			case 0:
				num = size * 75 / 100;
				break;
			case 1:
				num = size;
				break;
			case 2:
				num = size * 125 / 100;
				break;
			case 3:
				num = size * 150 / 100;
				break;
			case 4:
				num = size * 175 / 100;
				break;
			case 5:
				num = size * 200 / 100;
				break;
			default:
				num = size;
				break;
			}
			return num;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000A41B8 File Offset: 0x000A23B8
		public static float ScaleSize(float size)
		{
			return (float)Util.ScaleSize((int)size);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000A41D4 File Offset: 0x000A23D4
		public static Size ScaleSize(Size size)
		{
			return new Size(Util.ScaleSize(size.Width), Util.ScaleSize(size.Height));
		}

		// Token: 0x04000CC5 RID: 3269
		public static string PRODUCT_NAME = (Util.IsHyperkin() ? "Save Wizard for PS4" : "Save Wizard for PS4 MAX");

		// Token: 0x04000CC6 RID: 3270
		public static string[] AUTH_SERVERS = new string[] { "ps4as1.savewizard.net:8082", "ps4as2.savewizard.net:8082", "ps4as3.savewizard.net:8082", "ps4as4.savewizard.net:8082", "ps4as5.savewizard.net:8082" };

		// Token: 0x04000CC7 RID: 3271
		public static string[] SERVERS = new string[] { "ps4gs1.savewizard.net:8082", "ps4gs2.savewizard.net:8082", "ps4gs3.savewizard.net:8082", "ps4gs4.savewizard.net:8082", "ps4gs5.savewizard.net:8082", "ps4gs6.savewizard.net:8082", "ps4gs7.savewizard.net:8082", "ps4gs8.savewizard.net:8082", "ps4gs9.savewizard.net:8082" };

		// Token: 0x04000CC8 RID: 3272
		public static int CURRENT_SERVER = new Random().Next(0, Util.SERVERS.Length);

		// Token: 0x04000CC9 RID: 3273
		public static int CURRENT_AUTH_SERVER = new Random().Next(0, Util.AUTH_SERVERS.Length);

		// Token: 0x04000CCA RID: 3274
		public static int CURRENT_USER_PWD = new Random().Next(0, Program.HTACCESS_USER.Length);

		// Token: 0x04000CCB RID: 3275
		public static string AvailableVersion = "0.0";

		// Token: 0x04000CCC RID: 3276
		private static string VERSION_FILE_URL = "https://www.savewizard.net/downloads/swps4mbeta.txt";

		// Token: 0x04000CCE RID: 3278
		public static string forceServer = string.Empty;

		// Token: 0x04000CCF RID: 3279
		public static string forceAuthServer = string.Empty;

		// Token: 0x04000CD1 RID: 3281
		public static int pid = 0;

		// Token: 0x04000CD2 RID: 3282
		private static string SessionToken;

		// Token: 0x04000CD3 RID: 3283
		private static int MinFileSize = 0;

		// Token: 0x04000CD4 RID: 3284
		private static int MaxFileSize = int.MaxValue;

		// Token: 0x04000CD5 RID: 3285
		private static int _scaleIndex = -1;

		// Token: 0x020002CC RID: 716
		public enum Platform
		{
			// Token: 0x04001077 RID: 4215
			Windows,
			// Token: 0x04001078 RID: 4216
			Linux,
			// Token: 0x04001079 RID: 4217
			MacOS,
			// Token: 0x0400107A RID: 4218
			Other
		}
	}
}
