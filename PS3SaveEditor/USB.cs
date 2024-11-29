using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

namespace PS3SaveEditor
{
	// Token: 0x020001D8 RID: 472
	public class USB
	{
		// Token: 0x06001858 RID: 6232 RVA: 0x000901EC File Offset: 0x0008E3EC
		public static List<USB.USBDevice> GetConnectedDevices()
		{
			List<USB.USBDevice> list = new List<USB.USBDevice>();
			foreach (USB.USBController usbcontroller in USB.GetHostControllers())
			{
				USB.ListHub(usbcontroller.GetRootHub(), list);
			}
			return list;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00090250 File Offset: 0x0008E450
		private static void ListHub(USB.USBHub Hub, List<USB.USBDevice> DevList)
		{
			foreach (USB.USBPort usbport in Hub.GetPorts())
			{
				bool isHub = usbport.IsHub;
				if (isHub)
				{
					USB.ListHub(usbport.GetHub(), DevList);
				}
				else
				{
					bool isDeviceConnected = usbport.IsDeviceConnected;
					if (isDeviceConnected)
					{
						DevList.Add(usbport.GetDevice());
					}
				}
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000902D4 File Offset: 0x0008E4D4
		public static USB.USBDevice FindDeviceByDriverKeyName(string DriverKeyName)
		{
			USB.USBDevice usbdevice = null;
			foreach (USB.USBController usbcontroller in USB.GetHostControllers())
			{
				USB.SearchHubDriverKeyName(usbcontroller.GetRootHub(), ref usbdevice, DriverKeyName);
				bool flag = usbdevice != null;
				if (flag)
				{
					break;
				}
			}
			return usbdevice;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00090340 File Offset: 0x0008E540
		private static void SearchHubDriverKeyName(USB.USBHub Hub, ref USB.USBDevice FoundDevice, string DriverKeyName)
		{
			foreach (USB.USBPort usbport in Hub.GetPorts())
			{
				bool isHub = usbport.IsHub;
				if (isHub)
				{
					USB.SearchHubDriverKeyName(usbport.GetHub(), ref FoundDevice, DriverKeyName);
				}
				else
				{
					bool isDeviceConnected = usbport.IsDeviceConnected;
					if (isDeviceConnected)
					{
						USB.USBDevice device = usbport.GetDevice();
						bool flag = device.DeviceDriverKey == DriverKeyName;
						if (flag)
						{
							FoundDevice = device;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000903D8 File Offset: 0x0008E5D8
		public static USB.USBDevice FindDeviceByInstanceID(string InstanceID)
		{
			USB.USBDevice usbdevice = null;
			foreach (USB.USBController usbcontroller in USB.GetHostControllers())
			{
				USB.SearchHubInstanceID(usbcontroller.GetRootHub(), ref usbdevice, InstanceID);
				bool flag = usbdevice != null;
				if (flag)
				{
					break;
				}
			}
			return usbdevice;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00090444 File Offset: 0x0008E644
		private static void SearchHubInstanceID(USB.USBHub Hub, ref USB.USBDevice FoundDevice, string InstanceID)
		{
			foreach (USB.USBPort usbport in Hub.GetPorts())
			{
				bool isHub = usbport.IsHub;
				if (isHub)
				{
					USB.SearchHubInstanceID(usbport.GetHub(), ref FoundDevice, InstanceID);
				}
				else
				{
					bool isDeviceConnected = usbport.IsDeviceConnected;
					if (isDeviceConnected)
					{
						USB.USBDevice device = usbport.GetDevice();
						bool flag = device.InstanceID == InstanceID;
						if (flag)
						{
							FoundDevice = device;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600185E RID: 6238
		[DllImport("setupapi.dll")]
		private static extern int CM_Get_Parent(out IntPtr pdnDevInst, int dnDevInst, int ulFlags);

		// Token: 0x0600185F RID: 6239
		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		private static extern int CM_Get_Device_ID(IntPtr dnDevInst, IntPtr Buffer, int BufferLen, int ulFlags);

		// Token: 0x06001860 RID: 6240 RVA: 0x000904DC File Offset: 0x0008E6DC
		public static USB.USBDevice FindDriveLetter(string DriveLetter)
		{
			USB.USBDevice usbdevice = null;
			string text = "";
			int deviceNumber = USB.GetDeviceNumber("\\\\.\\" + DriveLetter.TrimEnd(new char[] { '\\' }));
			bool flag = deviceNumber < 0;
			USB.USBDevice usbdevice2;
			if (flag)
			{
				usbdevice2 = usbdevice;
			}
			else
			{
				Guid guid = new Guid("53f56307-b6bf-11d0-94f2-00a0c91efb8b");
				IntPtr intPtr = USB.SetupDiGetClassDevs(ref guid, 0, IntPtr.Zero, 18);
				bool flag2 = intPtr.ToInt32() != -1;
				if (flag2)
				{
					int num = 0;
					USB.SP_DEVINFO_DATA sp_DEVINFO_DATA;
					int num3;
					for (;;)
					{
						USB.SP_DEVICE_INTERFACE_DATA sp_DEVICE_INTERFACE_DATA = default(USB.SP_DEVICE_INTERFACE_DATA);
						sp_DEVICE_INTERFACE_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DATA);
						bool flag3 = USB.SetupDiEnumDeviceInterfaces(intPtr, IntPtr.Zero, ref guid, num, ref sp_DEVICE_INTERFACE_DATA);
						bool flag4 = flag3;
						if (flag4)
						{
							sp_DEVINFO_DATA = default(USB.SP_DEVINFO_DATA);
							sp_DEVINFO_DATA.cbSize = Marshal.SizeOf(sp_DEVINFO_DATA);
							USB.SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA = default(USB.SP_DEVICE_INTERFACE_DETAIL_DATA);
							sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = ((IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);
							int num2 = 0;
							num3 = 2048;
							bool flag5 = USB.SetupDiGetDeviceInterfaceDetail(intPtr, ref sp_DEVICE_INTERFACE_DATA, ref sp_DEVICE_INTERFACE_DETAIL_DATA, num3, ref num2, ref sp_DEVINFO_DATA);
							if (flag5)
							{
								bool flag6 = USB.GetDeviceNumber(sp_DEVICE_INTERFACE_DETAIL_DATA.DevicePath) == deviceNumber;
								if (flag6)
								{
									break;
								}
							}
						}
						num++;
						if (!flag3)
						{
							goto IL_0173;
						}
					}
					IntPtr intPtr2;
					USB.CM_Get_Parent(out intPtr2, sp_DEVINFO_DATA.DevInst, 0);
					IntPtr intPtr3 = Marshal.AllocHGlobal(num3);
					USB.CM_Get_Device_ID(intPtr2, intPtr3, num3, 0);
					text = Marshal.PtrToStringAuto(intPtr3);
					Marshal.FreeHGlobal(intPtr3);
					IL_0173:
					USB.SetupDiDestroyDeviceInfoList(intPtr);
				}
				bool flag7 = text.StartsWith("USB\\");
				if (flag7)
				{
					usbdevice = USB.FindDeviceByInstanceID(text);
				}
				usbdevice2 = usbdevice;
			}
			return usbdevice2;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00090688 File Offset: 0x0008E888
		private static int GetDeviceNumber(string DevicePath)
		{
			int num = -1;
			IntPtr intPtr = USB.CreateFile(DevicePath.TrimEnd(new char[] { '\\' }), 0, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);
			bool flag = intPtr.ToInt32() != -1;
			if (flag)
			{
				int num2 = Marshal.SizeOf(default(USB.STORAGE_DEVICE_NUMBER));
				IntPtr intPtr2 = Marshal.AllocHGlobal(num2);
				int num3;
				bool flag2 = USB.DeviceIoControl(intPtr, 2953344, IntPtr.Zero, 0, intPtr2, num2, out num3, IntPtr.Zero);
				if (flag2)
				{
					USB.STORAGE_DEVICE_NUMBER storage_DEVICE_NUMBER = (USB.STORAGE_DEVICE_NUMBER)Marshal.PtrToStructure(intPtr2, typeof(USB.STORAGE_DEVICE_NUMBER));
					num = (storage_DEVICE_NUMBER.DeviceType << 8) + storage_DEVICE_NUMBER.DeviceNumber;
				}
				Marshal.FreeHGlobal(intPtr2);
				USB.CloseHandle(intPtr);
			}
			return num;
		}

		// Token: 0x06001862 RID: 6242
		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, int Enumerator, IntPtr hwndParent, int Flags);

		// Token: 0x06001863 RID: 6243
		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SetupDiGetClassDevs(int ClassGuid, string Enumerator, IntPtr hwndParent, int Flags);

		// Token: 0x06001864 RID: 6244
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref USB.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

		// Token: 0x06001865 RID: 6245
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref USB.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, ref USB.SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, ref USB.SP_DEVINFO_DATA DeviceInfoData);

		// Token: 0x06001866 RID: 6246
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref USB.SP_DEVINFO_DATA DeviceInfoData, int iProperty, ref int PropertyRegDataType, IntPtr PropertyBuffer, int PropertyBufferSize, ref int RequiredSize);

		// Token: 0x06001867 RID: 6247
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, int MemberIndex, ref USB.SP_DEVINFO_DATA DeviceInfoData);

		// Token: 0x06001868 RID: 6248
		[DllImport("setupapi.dll", SetLastError = true)]
		private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		// Token: 0x06001869 RID: 6249
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref USB.SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, int DeviceInstanceIdSize, out int RequiredSize);

		// Token: 0x0600186A RID: 6250
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool DeviceIoControl(IntPtr hDevice, int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x0600186B RID: 6251
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x0600186C RID: 6252
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600186D RID: 6253 RVA: 0x00090754 File Offset: 0x0008E954
		public static ReadOnlyCollection<USB.USBController> GetHostControllers()
		{
			List<USB.USBController> list = new List<USB.USBController>();
			Guid guid = new Guid("3abf6f2d-71c4-462a-8a92-1e6861e6af27");
			IntPtr intPtr = USB.SetupDiGetClassDevs(ref guid, 0, IntPtr.Zero, 18);
			bool flag = intPtr.ToInt32() != -1;
			if (flag)
			{
				IntPtr intPtr2 = Marshal.AllocHGlobal(2048);
				int num = 0;
				bool flag2;
				do
				{
					USB.USBController usbcontroller = new USB.USBController();
					usbcontroller.ControllerIndex = num;
					USB.SP_DEVICE_INTERFACE_DATA sp_DEVICE_INTERFACE_DATA = default(USB.SP_DEVICE_INTERFACE_DATA);
					sp_DEVICE_INTERFACE_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DATA);
					flag2 = USB.SetupDiEnumDeviceInterfaces(intPtr, IntPtr.Zero, ref guid, num, ref sp_DEVICE_INTERFACE_DATA);
					bool flag3 = flag2;
					if (flag3)
					{
						USB.SP_DEVINFO_DATA sp_DEVINFO_DATA = default(USB.SP_DEVINFO_DATA);
						sp_DEVINFO_DATA.cbSize = Marshal.SizeOf(sp_DEVINFO_DATA);
						USB.SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA = default(USB.SP_DEVICE_INTERFACE_DETAIL_DATA);
						sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = ((IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);
						int num2 = 0;
						int num3 = 2048;
						bool flag4 = USB.SetupDiGetDeviceInterfaceDetail(intPtr, ref sp_DEVICE_INTERFACE_DATA, ref sp_DEVICE_INTERFACE_DETAIL_DATA, num3, ref num2, ref sp_DEVINFO_DATA);
						if (flag4)
						{
							usbcontroller.ControllerDevicePath = sp_DEVICE_INTERFACE_DETAIL_DATA.DevicePath;
							int num4 = 0;
							int num5 = 1;
							bool flag5 = USB.SetupDiGetDeviceRegistryProperty(intPtr, ref sp_DEVINFO_DATA, 0, ref num5, intPtr2, 2048, ref num4);
							if (flag5)
							{
								usbcontroller.ControllerDeviceDesc = Marshal.PtrToStringAuto(intPtr2);
							}
							bool flag6 = USB.SetupDiGetDeviceRegistryProperty(intPtr, ref sp_DEVINFO_DATA, 9, ref num5, intPtr2, 2048, ref num4);
							if (flag6)
							{
								usbcontroller.ControllerDriverKeyName = Marshal.PtrToStringAuto(intPtr2);
							}
						}
						list.Add(usbcontroller);
					}
					num++;
				}
				while (flag2);
				Marshal.FreeHGlobal(intPtr2);
				USB.SetupDiDestroyDeviceInfoList(intPtr);
			}
			return new ReadOnlyCollection<USB.USBController>(list);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000908F4 File Offset: 0x0008EAF4
		private static string GetDescriptionByKeyName(string DriverKeyName)
		{
			string text = "";
			string text2 = "USB";
			IntPtr intPtr = USB.SetupDiGetClassDevs(0, text2, IntPtr.Zero, 6);
			bool flag = intPtr.ToInt32() != -1;
			if (flag)
			{
				IntPtr intPtr2 = Marshal.AllocHGlobal(2048);
				int num = 0;
				USB.SP_DEVINFO_DATA sp_DEVINFO_DATA;
				int num2;
				int num3;
				for (;;)
				{
					sp_DEVINFO_DATA = default(USB.SP_DEVINFO_DATA);
					sp_DEVINFO_DATA.cbSize = Marshal.SizeOf(sp_DEVINFO_DATA);
					bool flag2 = USB.SetupDiEnumDeviceInfo(intPtr, num, ref sp_DEVINFO_DATA);
					bool flag3 = flag2;
					if (flag3)
					{
						num2 = 0;
						num3 = 1;
						string text3 = "";
						bool flag4 = USB.SetupDiGetDeviceRegistryProperty(intPtr, ref sp_DEVINFO_DATA, 9, ref num3, intPtr2, 2048, ref num2);
						if (flag4)
						{
							text3 = Marshal.PtrToStringAuto(intPtr2);
						}
						bool flag5 = text3 == DriverKeyName;
						if (flag5)
						{
							break;
						}
					}
					num++;
					if (!flag2)
					{
						goto IL_00EB;
					}
				}
				bool flag6 = USB.SetupDiGetDeviceRegistryProperty(intPtr, ref sp_DEVINFO_DATA, 0, ref num3, intPtr2, 2048, ref num2);
				if (flag6)
				{
					text = Marshal.PtrToStringAuto(intPtr2);
				}
				IL_00EB:
				Marshal.FreeHGlobal(intPtr2);
				USB.SetupDiDestroyDeviceInfoList(intPtr);
			}
			return text;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00090A04 File Offset: 0x0008EC04
		private static string GetInstanceIDByKeyName(string DriverKeyName)
		{
			string text = "";
			string text2 = "USB";
			IntPtr intPtr = USB.SetupDiGetClassDevs(0, text2, IntPtr.Zero, 6);
			bool flag = intPtr.ToInt32() != -1;
			if (flag)
			{
				IntPtr intPtr2 = Marshal.AllocHGlobal(2048);
				int num = 0;
				USB.SP_DEVINFO_DATA sp_DEVINFO_DATA;
				int num2;
				for (;;)
				{
					sp_DEVINFO_DATA = default(USB.SP_DEVINFO_DATA);
					sp_DEVINFO_DATA.cbSize = Marshal.SizeOf(sp_DEVINFO_DATA);
					bool flag2 = USB.SetupDiEnumDeviceInfo(intPtr, num, ref sp_DEVINFO_DATA);
					bool flag3 = flag2;
					if (flag3)
					{
						num2 = 0;
						int num3 = 1;
						string text3 = "";
						bool flag4 = USB.SetupDiGetDeviceRegistryProperty(intPtr, ref sp_DEVINFO_DATA, 9, ref num3, intPtr2, 2048, ref num2);
						if (flag4)
						{
							text3 = Marshal.PtrToStringAuto(intPtr2);
						}
						bool flag5 = text3 == DriverKeyName;
						if (flag5)
						{
							break;
						}
					}
					num++;
					if (!flag2)
					{
						goto IL_00EE;
					}
				}
				int num4 = 2048;
				StringBuilder stringBuilder = new StringBuilder(num4);
				USB.SetupDiGetDeviceInstanceId(intPtr, ref sp_DEVINFO_DATA, stringBuilder, num4, out num2);
				text = stringBuilder.ToString();
				IL_00EE:
				Marshal.FreeHGlobal(intPtr2);
				USB.SetupDiDestroyDeviceInfoList(intPtr);
			}
			return text;
		}

		// Token: 0x04000BFC RID: 3068
		private const int IOCTL_STORAGE_GET_DEVICE_NUMBER = 2953344;

		// Token: 0x04000BFD RID: 3069
		private const string GUID_DEVINTERFACE_DISK = "53f56307-b6bf-11d0-94f2-00a0c91efb8b";

		// Token: 0x04000BFE RID: 3070
		private const int GENERIC_WRITE = 1073741824;

		// Token: 0x04000BFF RID: 3071
		private const int FILE_SHARE_READ = 1;

		// Token: 0x04000C00 RID: 3072
		private const int FILE_SHARE_WRITE = 2;

		// Token: 0x04000C01 RID: 3073
		private const int OPEN_EXISTING = 3;

		// Token: 0x04000C02 RID: 3074
		private const int INVALID_HANDLE_VALUE = -1;

		// Token: 0x04000C03 RID: 3075
		private const int IOCTL_GET_HCD_DRIVERKEY_NAME = 2229284;

		// Token: 0x04000C04 RID: 3076
		private const int IOCTL_USB_GET_ROOT_HUB_NAME = 2229256;

		// Token: 0x04000C05 RID: 3077
		private const int IOCTL_USB_GET_NODE_INFORMATION = 2229256;

		// Token: 0x04000C06 RID: 3078
		private const int IOCTL_USB_GET_NODE_CONNECTION_INFORMATION_EX = 2229320;

		// Token: 0x04000C07 RID: 3079
		private const int IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION = 2229264;

		// Token: 0x04000C08 RID: 3080
		private const int IOCTL_USB_GET_NODE_CONNECTION_NAME = 2229268;

		// Token: 0x04000C09 RID: 3081
		private const int IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME = 2229280;

		// Token: 0x04000C0A RID: 3082
		private const int USB_DEVICE_DESCRIPTOR_TYPE = 1;

		// Token: 0x04000C0B RID: 3083
		private const int USB_STRING_DESCRIPTOR_TYPE = 3;

		// Token: 0x04000C0C RID: 3084
		private const int BUFFER_SIZE = 2048;

		// Token: 0x04000C0D RID: 3085
		private const int MAXIMUM_USB_STRING_LENGTH = 255;

		// Token: 0x04000C0E RID: 3086
		private const string GUID_DEVINTERFACE_HUBCONTROLLER = "3abf6f2d-71c4-462a-8a92-1e6861e6af27";

		// Token: 0x04000C0F RID: 3087
		private const string REGSTR_KEY_USB = "USB";

		// Token: 0x04000C10 RID: 3088
		private const int DIGCF_PRESENT = 2;

		// Token: 0x04000C11 RID: 3089
		private const int DIGCF_ALLCLASSES = 4;

		// Token: 0x04000C12 RID: 3090
		private const int DIGCF_DEVICEINTERFACE = 16;

		// Token: 0x04000C13 RID: 3091
		private const int SPDRP_DRIVER = 9;

		// Token: 0x04000C14 RID: 3092
		private const int SPDRP_DEVICEDESC = 0;

		// Token: 0x04000C15 RID: 3093
		private const int REG_SZ = 1;

		// Token: 0x020002A1 RID: 673
		private struct STORAGE_DEVICE_NUMBER
		{
			// Token: 0x04000FF3 RID: 4083
			public int DeviceType;

			// Token: 0x04000FF4 RID: 4084
			public int DeviceNumber;

			// Token: 0x04000FF5 RID: 4085
			public int PartitionNumber;
		}

		// Token: 0x020002A2 RID: 674
		private enum USB_HUB_NODE
		{
			// Token: 0x04000FF7 RID: 4087
			UsbHub,
			// Token: 0x04000FF8 RID: 4088
			UsbMIParent
		}

		// Token: 0x020002A3 RID: 675
		private enum USB_CONNECTION_STATUS
		{
			// Token: 0x04000FFA RID: 4090
			NoDeviceConnected,
			// Token: 0x04000FFB RID: 4091
			DeviceConnected,
			// Token: 0x04000FFC RID: 4092
			DeviceFailedEnumeration,
			// Token: 0x04000FFD RID: 4093
			DeviceGeneralFailure,
			// Token: 0x04000FFE RID: 4094
			DeviceCausedOvercurrent,
			// Token: 0x04000FFF RID: 4095
			DeviceNotEnoughPower,
			// Token: 0x04001000 RID: 4096
			DeviceNotEnoughBandwidth,
			// Token: 0x04001001 RID: 4097
			DeviceHubNestedTooDeeply,
			// Token: 0x04001002 RID: 4098
			DeviceInLegacyHub
		}

		// Token: 0x020002A4 RID: 676
		private enum USB_DEVICE_SPEED : byte
		{
			// Token: 0x04001004 RID: 4100
			UsbLowSpeed,
			// Token: 0x04001005 RID: 4101
			UsbFullSpeed,
			// Token: 0x04001006 RID: 4102
			UsbHighSpeed
		}

		// Token: 0x020002A5 RID: 677
		private struct SP_DEVINFO_DATA
		{
			// Token: 0x04001007 RID: 4103
			public int cbSize;

			// Token: 0x04001008 RID: 4104
			public Guid ClassGuid;

			// Token: 0x04001009 RID: 4105
			public int DevInst;

			// Token: 0x0400100A RID: 4106
			public IntPtr Reserved;
		}

		// Token: 0x020002A6 RID: 678
		private struct SP_DEVICE_INTERFACE_DATA
		{
			// Token: 0x0400100B RID: 4107
			public int cbSize;

			// Token: 0x0400100C RID: 4108
			public Guid InterfaceClassGuid;

			// Token: 0x0400100D RID: 4109
			public int Flags;

			// Token: 0x0400100E RID: 4110
			public IntPtr Reserved;
		}

		// Token: 0x020002A7 RID: 679
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct SP_DEVICE_INTERFACE_DETAIL_DATA
		{
			// Token: 0x0400100F RID: 4111
			public int cbSize;

			// Token: 0x04001010 RID: 4112
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
			public string DevicePath;
		}

		// Token: 0x020002A8 RID: 680
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct USB_HCD_DRIVERKEY_NAME
		{
			// Token: 0x04001011 RID: 4113
			public int ActualLength;

			// Token: 0x04001012 RID: 4114
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
			public string DriverKeyName;
		}

		// Token: 0x020002A9 RID: 681
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct USB_ROOT_HUB_NAME
		{
			// Token: 0x04001013 RID: 4115
			public int ActualLength;

			// Token: 0x04001014 RID: 4116
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
			public string RootHubName;
		}

		// Token: 0x020002AA RID: 682
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct USB_HUB_DESCRIPTOR
		{
			// Token: 0x04001015 RID: 4117
			public byte bDescriptorLength;

			// Token: 0x04001016 RID: 4118
			public byte bDescriptorType;

			// Token: 0x04001017 RID: 4119
			public byte bNumberOfPorts;

			// Token: 0x04001018 RID: 4120
			public short wHubCharacteristics;

			// Token: 0x04001019 RID: 4121
			public byte bPowerOnToPowerGood;

			// Token: 0x0400101A RID: 4122
			public byte bHubControlCurrent;

			// Token: 0x0400101B RID: 4123
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public byte[] bRemoveAndPowerMask;
		}

		// Token: 0x020002AB RID: 683
		private struct USB_HUB_INFORMATION
		{
			// Token: 0x0400101C RID: 4124
			public USB.USB_HUB_DESCRIPTOR HubDescriptor;

			// Token: 0x0400101D RID: 4125
			public byte HubIsBusPowered;
		}

		// Token: 0x020002AC RID: 684
		private struct USB_NODE_INFORMATION
		{
			// Token: 0x0400101E RID: 4126
			public int NodeType;

			// Token: 0x0400101F RID: 4127
			public USB.USB_HUB_INFORMATION HubInformation;
		}

		// Token: 0x020002AD RID: 685
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct USB_NODE_CONNECTION_INFORMATION_EX
		{
			// Token: 0x04001020 RID: 4128
			public int ConnectionIndex;

			// Token: 0x04001021 RID: 4129
			public USB.USB_DEVICE_DESCRIPTOR DeviceDescriptor;

			// Token: 0x04001022 RID: 4130
			public byte CurrentConfigurationValue;

			// Token: 0x04001023 RID: 4131
			public byte Speed;

			// Token: 0x04001024 RID: 4132
			public byte DeviceIsHub;

			// Token: 0x04001025 RID: 4133
			public short DeviceAddress;

			// Token: 0x04001026 RID: 4134
			public int NumberOfOpenPipes;

			// Token: 0x04001027 RID: 4135
			public int ConnectionStatus;
		}

		// Token: 0x020002AE RID: 686
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct USB_DEVICE_DESCRIPTOR
		{
			// Token: 0x04001028 RID: 4136
			public byte bLength;

			// Token: 0x04001029 RID: 4137
			public byte bDescriptorType;

			// Token: 0x0400102A RID: 4138
			public short bcdUSB;

			// Token: 0x0400102B RID: 4139
			public byte bDeviceClass;

			// Token: 0x0400102C RID: 4140
			public byte bDeviceSubClass;

			// Token: 0x0400102D RID: 4141
			public byte bDeviceProtocol;

			// Token: 0x0400102E RID: 4142
			public byte bMaxPacketSize0;

			// Token: 0x0400102F RID: 4143
			public short idVendor;

			// Token: 0x04001030 RID: 4144
			public short idProduct;

			// Token: 0x04001031 RID: 4145
			public short bcdDevice;

			// Token: 0x04001032 RID: 4146
			public byte iManufacturer;

			// Token: 0x04001033 RID: 4147
			public byte iProduct;

			// Token: 0x04001034 RID: 4148
			public byte iSerialNumber;

			// Token: 0x04001035 RID: 4149
			public byte bNumConfigurations;
		}

		// Token: 0x020002AF RID: 687
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct USB_STRING_DESCRIPTOR
		{
			// Token: 0x04001036 RID: 4150
			public byte bLength;

			// Token: 0x04001037 RID: 4151
			public byte bDescriptorType;

			// Token: 0x04001038 RID: 4152
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
			public string bString;
		}

		// Token: 0x020002B0 RID: 688
		private struct USB_SETUP_PACKET
		{
			// Token: 0x04001039 RID: 4153
			public byte bmRequest;

			// Token: 0x0400103A RID: 4154
			public byte bRequest;

			// Token: 0x0400103B RID: 4155
			public short wValue;

			// Token: 0x0400103C RID: 4156
			public short wIndex;

			// Token: 0x0400103D RID: 4157
			public short wLength;
		}

		// Token: 0x020002B1 RID: 689
		private struct USB_DESCRIPTOR_REQUEST
		{
			// Token: 0x0400103E RID: 4158
			public int ConnectionIndex;

			// Token: 0x0400103F RID: 4159
			public USB.USB_SETUP_PACKET SetupPacket;
		}

		// Token: 0x020002B2 RID: 690
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct USB_NODE_CONNECTION_NAME
		{
			// Token: 0x04001040 RID: 4160
			public int ConnectionIndex;

			// Token: 0x04001041 RID: 4161
			public int ActualLength;

			// Token: 0x04001042 RID: 4162
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
			public string NodeName;
		}

		// Token: 0x020002B3 RID: 691
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct USB_NODE_CONNECTION_DRIVERKEY_NAME
		{
			// Token: 0x04001043 RID: 4163
			public int ConnectionIndex;

			// Token: 0x04001044 RID: 4164
			public int ActualLength;

			// Token: 0x04001045 RID: 4165
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
			public string DriverKeyName;
		}

		// Token: 0x020002B4 RID: 692
		public class USBController
		{
			// Token: 0x06001E4E RID: 7758 RVA: 0x000B8EE4 File Offset: 0x000B70E4
			public USBController()
			{
				this.ControllerIndex = 0;
				this.ControllerDevicePath = "";
				this.ControllerDeviceDesc = "";
				this.ControllerDriverKeyName = "";
			}

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000B8F18 File Offset: 0x000B7118
			public int Index
			{
				get
				{
					return this.ControllerIndex;
				}
			}

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000B8F30 File Offset: 0x000B7130
			public string DevicePath
			{
				get
				{
					return this.ControllerDevicePath;
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000B8F48 File Offset: 0x000B7148
			public string DriverKeyName
			{
				get
				{
					return this.ControllerDriverKeyName;
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000B8F60 File Offset: 0x000B7160
			public string Name
			{
				get
				{
					return this.ControllerDeviceDesc;
				}
			}

			// Token: 0x06001E53 RID: 7763 RVA: 0x000B8F78 File Offset: 0x000B7178
			public USB.USBHub GetRootHub()
			{
				USB.USBHub usbhub = new USB.USBHub();
				usbhub.HubIsRootHub = true;
				usbhub.HubDeviceDesc = "Root Hub";
				IntPtr intPtr = USB.CreateFile(this.ControllerDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
				bool flag = intPtr.ToInt32() != -1;
				if (flag)
				{
					int num = Marshal.SizeOf(default(USB.USB_ROOT_HUB_NAME));
					IntPtr intPtr2 = Marshal.AllocHGlobal(num);
					int num2;
					bool flag2 = USB.DeviceIoControl(intPtr, 2229256, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero);
					if (flag2)
					{
						USB.USB_ROOT_HUB_NAME usb_ROOT_HUB_NAME = (USB.USB_ROOT_HUB_NAME)Marshal.PtrToStructure(intPtr2, typeof(USB.USB_ROOT_HUB_NAME));
						usbhub.HubDevicePath = "\\\\.\\" + usb_ROOT_HUB_NAME.RootHubName;
					}
					IntPtr intPtr3 = USB.CreateFile(usbhub.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
					bool flag3 = intPtr3.ToInt32() != -1;
					if (flag3)
					{
						USB.USB_NODE_INFORMATION usb_NODE_INFORMATION = new USB.USB_NODE_INFORMATION
						{
							NodeType = 0
						};
						num = Marshal.SizeOf(usb_NODE_INFORMATION);
						IntPtr intPtr4 = Marshal.AllocHGlobal(num);
						Marshal.StructureToPtr(usb_NODE_INFORMATION, intPtr4, true);
						bool flag4 = USB.DeviceIoControl(intPtr3, 2229256, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero);
						if (flag4)
						{
							usb_NODE_INFORMATION = (USB.USB_NODE_INFORMATION)Marshal.PtrToStructure(intPtr4, typeof(USB.USB_NODE_INFORMATION));
							usbhub.HubIsBusPowered = Convert.ToBoolean(usb_NODE_INFORMATION.HubInformation.HubIsBusPowered);
							usbhub.HubPortCount = (int)usb_NODE_INFORMATION.HubInformation.HubDescriptor.bNumberOfPorts;
						}
						Marshal.FreeHGlobal(intPtr4);
						USB.CloseHandle(intPtr3);
					}
					Marshal.FreeHGlobal(intPtr2);
					USB.CloseHandle(intPtr);
				}
				return usbhub;
			}

			// Token: 0x04001046 RID: 4166
			internal int ControllerIndex;

			// Token: 0x04001047 RID: 4167
			internal string ControllerDriverKeyName;

			// Token: 0x04001048 RID: 4168
			internal string ControllerDevicePath;

			// Token: 0x04001049 RID: 4169
			internal string ControllerDeviceDesc;
		}

		// Token: 0x020002B5 RID: 693
		public class USBHub
		{
			// Token: 0x06001E54 RID: 7764 RVA: 0x000B913C File Offset: 0x000B733C
			public USBHub()
			{
				this.HubPortCount = 0;
				this.HubDevicePath = "";
				this.HubDeviceDesc = "";
				this.HubDriverKey = "";
				this.HubIsBusPowered = false;
				this.HubIsRootHub = false;
				this.HubManufacturer = "";
				this.HubProduct = "";
				this.HubSerialNumber = "";
				this.HubInstanceID = "";
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000B91B4 File Offset: 0x000B73B4
			public int PortCount
			{
				get
				{
					return this.HubPortCount;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000B91CC File Offset: 0x000B73CC
			public string DevicePath
			{
				get
				{
					return this.HubDevicePath;
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000B91E4 File Offset: 0x000B73E4
			public string DriverKey
			{
				get
				{
					return this.HubDriverKey;
				}
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000B91FC File Offset: 0x000B73FC
			public string Name
			{
				get
				{
					return this.HubDeviceDesc;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000B9214 File Offset: 0x000B7414
			public string InstanceID
			{
				get
				{
					return this.HubInstanceID;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000B922C File Offset: 0x000B742C
			public bool IsBusPowered
			{
				get
				{
					return this.HubIsBusPowered;
				}
			}

			// Token: 0x170007F7 RID: 2039
			// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000B9244 File Offset: 0x000B7444
			public bool IsRootHub
			{
				get
				{
					return this.HubIsRootHub;
				}
			}

			// Token: 0x170007F8 RID: 2040
			// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000B925C File Offset: 0x000B745C
			public string Manufacturer
			{
				get
				{
					return this.HubManufacturer;
				}
			}

			// Token: 0x170007F9 RID: 2041
			// (get) Token: 0x06001E5D RID: 7773 RVA: 0x000B9274 File Offset: 0x000B7474
			public string Product
			{
				get
				{
					return this.HubProduct;
				}
			}

			// Token: 0x170007FA RID: 2042
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x000B928C File Offset: 0x000B748C
			public string SerialNumber
			{
				get
				{
					return this.HubSerialNumber;
				}
			}

			// Token: 0x06001E5F RID: 7775 RVA: 0x000B92A4 File Offset: 0x000B74A4
			public ReadOnlyCollection<USB.USBPort> GetPorts()
			{
				List<USB.USBPort> list = new List<USB.USBPort>();
				IntPtr intPtr = USB.CreateFile(this.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
				bool flag = intPtr.ToInt32() != -1;
				if (flag)
				{
					int num = Marshal.SizeOf(typeof(USB.USB_NODE_CONNECTION_INFORMATION_EX));
					IntPtr intPtr2 = Marshal.AllocHGlobal(num);
					for (int i = 1; i <= this.HubPortCount; i++)
					{
						Marshal.StructureToPtr(new USB.USB_NODE_CONNECTION_INFORMATION_EX
						{
							ConnectionIndex = i
						}, intPtr2, true);
						int num2;
						bool flag2 = USB.DeviceIoControl(intPtr, 2229320, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero);
						if (flag2)
						{
							USB.USB_NODE_CONNECTION_INFORMATION_EX usb_NODE_CONNECTION_INFORMATION_EX = (USB.USB_NODE_CONNECTION_INFORMATION_EX)Marshal.PtrToStructure(intPtr2, typeof(USB.USB_NODE_CONNECTION_INFORMATION_EX));
							USB.USBPort usbport = new USB.USBPort();
							usbport.PortPortNumber = i;
							usbport.PortHubDevicePath = this.HubDevicePath;
							USB.USB_CONNECTION_STATUS connectionStatus = (USB.USB_CONNECTION_STATUS)usb_NODE_CONNECTION_INFORMATION_EX.ConnectionStatus;
							usbport.PortStatus = connectionStatus.ToString();
							USB.USB_DEVICE_SPEED speed = (USB.USB_DEVICE_SPEED)usb_NODE_CONNECTION_INFORMATION_EX.Speed;
							usbport.PortSpeed = speed.ToString();
							usbport.PortIsDeviceConnected = usb_NODE_CONNECTION_INFORMATION_EX.ConnectionStatus == 1;
							usbport.PortIsHub = Convert.ToBoolean(usb_NODE_CONNECTION_INFORMATION_EX.DeviceIsHub);
							usbport.PortDeviceDescriptor = usb_NODE_CONNECTION_INFORMATION_EX.DeviceDescriptor;
							list.Add(usbport);
						}
					}
					Marshal.FreeHGlobal(intPtr2);
					USB.CloseHandle(intPtr);
				}
				return new ReadOnlyCollection<USB.USBPort>(list);
			}

			// Token: 0x0400104A RID: 4170
			internal int HubPortCount;

			// Token: 0x0400104B RID: 4171
			internal string HubDriverKey;

			// Token: 0x0400104C RID: 4172
			internal string HubDevicePath;

			// Token: 0x0400104D RID: 4173
			internal string HubDeviceDesc;

			// Token: 0x0400104E RID: 4174
			internal string HubManufacturer;

			// Token: 0x0400104F RID: 4175
			internal string HubProduct;

			// Token: 0x04001050 RID: 4176
			internal string HubSerialNumber;

			// Token: 0x04001051 RID: 4177
			internal string HubInstanceID;

			// Token: 0x04001052 RID: 4178
			internal bool HubIsBusPowered;

			// Token: 0x04001053 RID: 4179
			internal bool HubIsRootHub;
		}

		// Token: 0x020002B6 RID: 694
		public class USBPort
		{
			// Token: 0x06001E60 RID: 7776 RVA: 0x000B9433 File Offset: 0x000B7633
			public USBPort()
			{
				this.PortPortNumber = 0;
				this.PortStatus = "";
				this.PortHubDevicePath = "";
				this.PortSpeed = "";
				this.PortIsHub = false;
				this.PortIsDeviceConnected = false;
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000B9474 File Offset: 0x000B7674
			public int PortNumber
			{
				get
				{
					return this.PortPortNumber;
				}
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000B948C File Offset: 0x000B768C
			public string HubDevicePath
			{
				get
				{
					return this.PortHubDevicePath;
				}
			}

			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000B94A4 File Offset: 0x000B76A4
			public string Status
			{
				get
				{
					return this.PortStatus;
				}
			}

			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000B94BC File Offset: 0x000B76BC
			public string Speed
			{
				get
				{
					return this.PortSpeed;
				}
			}

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x06001E65 RID: 7781 RVA: 0x000B94D4 File Offset: 0x000B76D4
			public bool IsHub
			{
				get
				{
					return this.PortIsHub;
				}
			}

			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000B94EC File Offset: 0x000B76EC
			public bool IsDeviceConnected
			{
				get
				{
					return this.PortIsDeviceConnected;
				}
			}

			// Token: 0x06001E67 RID: 7783 RVA: 0x000B9504 File Offset: 0x000B7704
			public USB.USBDevice GetDevice()
			{
				bool flag = !this.PortIsDeviceConnected;
				USB.USBDevice usbdevice;
				if (flag)
				{
					usbdevice = null;
				}
				else
				{
					USB.USBDevice usbdevice2 = new USB.USBDevice();
					usbdevice2.DevicePortNumber = this.PortPortNumber;
					usbdevice2.DeviceHubDevicePath = this.PortHubDevicePath;
					usbdevice2.DeviceDescriptor = this.PortDeviceDescriptor;
					IntPtr intPtr = USB.CreateFile(this.PortHubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
					bool flag2 = intPtr.ToInt32() != -1;
					if (flag2)
					{
						int num = 2048;
						string text = new string('\0', 2048 / Marshal.SystemDefaultCharSize);
						bool flag3 = this.PortDeviceDescriptor.iManufacturer > 0;
						int num2;
						if (flag3)
						{
							USB.USB_DESCRIPTOR_REQUEST usb_DESCRIPTOR_REQUEST = default(USB.USB_DESCRIPTOR_REQUEST);
							usb_DESCRIPTOR_REQUEST.ConnectionIndex = this.PortPortNumber;
							usb_DESCRIPTOR_REQUEST.SetupPacket.wValue = (short)(768 + (int)this.PortDeviceDescriptor.iManufacturer);
							usb_DESCRIPTOR_REQUEST.SetupPacket.wLength = (short)(num - Marshal.SizeOf(usb_DESCRIPTOR_REQUEST));
							usb_DESCRIPTOR_REQUEST.SetupPacket.wIndex = 1033;
							IntPtr intPtr2 = Marshal.StringToHGlobalAuto(text);
							Marshal.StructureToPtr(usb_DESCRIPTOR_REQUEST, intPtr2, true);
							bool flag4 = USB.DeviceIoControl(intPtr, 2229264, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero);
							if (flag4)
							{
								IntPtr intPtr3 = new IntPtr(intPtr2.ToInt32() + Marshal.SizeOf(usb_DESCRIPTOR_REQUEST));
								USB.USB_STRING_DESCRIPTOR usb_STRING_DESCRIPTOR = (USB.USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(intPtr3, typeof(USB.USB_STRING_DESCRIPTOR));
								usbdevice2.DeviceManufacturer = usb_STRING_DESCRIPTOR.bString;
							}
							Marshal.FreeHGlobal(intPtr2);
						}
						bool flag5 = this.PortDeviceDescriptor.iProduct > 0;
						if (flag5)
						{
							USB.USB_DESCRIPTOR_REQUEST usb_DESCRIPTOR_REQUEST2 = default(USB.USB_DESCRIPTOR_REQUEST);
							usb_DESCRIPTOR_REQUEST2.ConnectionIndex = this.PortPortNumber;
							usb_DESCRIPTOR_REQUEST2.SetupPacket.wValue = (short)(768 + (int)this.PortDeviceDescriptor.iProduct);
							usb_DESCRIPTOR_REQUEST2.SetupPacket.wLength = (short)(num - Marshal.SizeOf(usb_DESCRIPTOR_REQUEST2));
							usb_DESCRIPTOR_REQUEST2.SetupPacket.wIndex = 1033;
							IntPtr intPtr4 = Marshal.StringToHGlobalAuto(text);
							Marshal.StructureToPtr(usb_DESCRIPTOR_REQUEST2, intPtr4, true);
							bool flag6 = USB.DeviceIoControl(intPtr, 2229264, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero);
							if (flag6)
							{
								IntPtr intPtr5 = new IntPtr(intPtr4.ToInt32() + Marshal.SizeOf(usb_DESCRIPTOR_REQUEST2));
								USB.USB_STRING_DESCRIPTOR usb_STRING_DESCRIPTOR2 = (USB.USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(intPtr5, typeof(USB.USB_STRING_DESCRIPTOR));
								usbdevice2.DeviceProduct = usb_STRING_DESCRIPTOR2.bString;
							}
							Marshal.FreeHGlobal(intPtr4);
						}
						bool flag7 = this.PortDeviceDescriptor.iSerialNumber > 0;
						if (flag7)
						{
							USB.USB_DESCRIPTOR_REQUEST usb_DESCRIPTOR_REQUEST3 = default(USB.USB_DESCRIPTOR_REQUEST);
							usb_DESCRIPTOR_REQUEST3.ConnectionIndex = this.PortPortNumber;
							usb_DESCRIPTOR_REQUEST3.SetupPacket.wValue = (short)(768 + (int)this.PortDeviceDescriptor.iSerialNumber);
							usb_DESCRIPTOR_REQUEST3.SetupPacket.wLength = (short)(num - Marshal.SizeOf(usb_DESCRIPTOR_REQUEST3));
							usb_DESCRIPTOR_REQUEST3.SetupPacket.wIndex = 1033;
							IntPtr intPtr6 = Marshal.StringToHGlobalAuto(text);
							Marshal.StructureToPtr(usb_DESCRIPTOR_REQUEST3, intPtr6, true);
							bool flag8 = USB.DeviceIoControl(intPtr, 2229264, intPtr6, num, intPtr6, num, out num2, IntPtr.Zero);
							if (flag8)
							{
								IntPtr intPtr7 = new IntPtr(intPtr6.ToInt32() + Marshal.SizeOf(usb_DESCRIPTOR_REQUEST3));
								USB.USB_STRING_DESCRIPTOR usb_STRING_DESCRIPTOR3 = (USB.USB_STRING_DESCRIPTOR)Marshal.PtrToStructure(intPtr7, typeof(USB.USB_STRING_DESCRIPTOR));
								usbdevice2.DeviceSerialNumber = usb_STRING_DESCRIPTOR3.bString;
							}
							Marshal.FreeHGlobal(intPtr6);
						}
						USB.USB_NODE_CONNECTION_DRIVERKEY_NAME usb_NODE_CONNECTION_DRIVERKEY_NAME = new USB.USB_NODE_CONNECTION_DRIVERKEY_NAME
						{
							ConnectionIndex = this.PortPortNumber
						};
						num = Marshal.SizeOf(usb_NODE_CONNECTION_DRIVERKEY_NAME);
						IntPtr intPtr8 = Marshal.AllocHGlobal(num);
						Marshal.StructureToPtr(usb_NODE_CONNECTION_DRIVERKEY_NAME, intPtr8, true);
						bool flag9 = USB.DeviceIoControl(intPtr, 2229280, intPtr8, num, intPtr8, num, out num2, IntPtr.Zero);
						if (flag9)
						{
							usb_NODE_CONNECTION_DRIVERKEY_NAME = (USB.USB_NODE_CONNECTION_DRIVERKEY_NAME)Marshal.PtrToStructure(intPtr8, typeof(USB.USB_NODE_CONNECTION_DRIVERKEY_NAME));
							usbdevice2.DeviceDriverKey = usb_NODE_CONNECTION_DRIVERKEY_NAME.DriverKeyName;
							usbdevice2.DeviceName = USB.GetDescriptionByKeyName(usbdevice2.DeviceDriverKey);
							usbdevice2.DeviceInstanceID = USB.GetInstanceIDByKeyName(usbdevice2.DeviceDriverKey);
						}
						Marshal.FreeHGlobal(intPtr8);
						USB.CloseHandle(intPtr);
					}
					usbdevice = usbdevice2;
				}
				return usbdevice;
			}

			// Token: 0x06001E68 RID: 7784 RVA: 0x000B9950 File Offset: 0x000B7B50
			public USB.USBHub GetHub()
			{
				bool flag = !this.PortIsHub;
				USB.USBHub usbhub;
				if (flag)
				{
					usbhub = null;
				}
				else
				{
					USB.USBHub usbhub2 = new USB.USBHub();
					usbhub2.HubIsRootHub = false;
					usbhub2.HubDeviceDesc = "External Hub";
					IntPtr intPtr = USB.CreateFile(this.PortHubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
					bool flag2 = intPtr.ToInt32() != -1;
					if (flag2)
					{
						USB.USB_NODE_CONNECTION_NAME usb_NODE_CONNECTION_NAME = new USB.USB_NODE_CONNECTION_NAME
						{
							ConnectionIndex = this.PortPortNumber
						};
						int num = Marshal.SizeOf(usb_NODE_CONNECTION_NAME);
						IntPtr intPtr2 = Marshal.AllocHGlobal(num);
						Marshal.StructureToPtr(usb_NODE_CONNECTION_NAME, intPtr2, true);
						int num2;
						bool flag3 = USB.DeviceIoControl(intPtr, 2229268, intPtr2, num, intPtr2, num, out num2, IntPtr.Zero);
						if (flag3)
						{
							usb_NODE_CONNECTION_NAME = (USB.USB_NODE_CONNECTION_NAME)Marshal.PtrToStructure(intPtr2, typeof(USB.USB_NODE_CONNECTION_NAME));
							usbhub2.HubDevicePath = "\\\\.\\" + usb_NODE_CONNECTION_NAME.NodeName;
						}
						IntPtr intPtr3 = USB.CreateFile(usbhub2.HubDevicePath, 1073741824, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
						bool flag4 = intPtr3.ToInt32() != -1;
						if (flag4)
						{
							USB.USB_NODE_INFORMATION usb_NODE_INFORMATION = new USB.USB_NODE_INFORMATION
							{
								NodeType = 0
							};
							num = Marshal.SizeOf(usb_NODE_INFORMATION);
							IntPtr intPtr4 = Marshal.AllocHGlobal(num);
							Marshal.StructureToPtr(usb_NODE_INFORMATION, intPtr4, true);
							bool flag5 = USB.DeviceIoControl(intPtr3, 2229256, intPtr4, num, intPtr4, num, out num2, IntPtr.Zero);
							if (flag5)
							{
								usb_NODE_INFORMATION = (USB.USB_NODE_INFORMATION)Marshal.PtrToStructure(intPtr4, typeof(USB.USB_NODE_INFORMATION));
								usbhub2.HubIsBusPowered = Convert.ToBoolean(usb_NODE_INFORMATION.HubInformation.HubIsBusPowered);
								usbhub2.HubPortCount = (int)usb_NODE_INFORMATION.HubInformation.HubDescriptor.bNumberOfPorts;
							}
							Marshal.FreeHGlobal(intPtr4);
							USB.CloseHandle(intPtr3);
						}
						USB.USBDevice device = this.GetDevice();
						usbhub2.HubInstanceID = device.DeviceInstanceID;
						usbhub2.HubManufacturer = device.Manufacturer;
						usbhub2.HubProduct = device.Product;
						usbhub2.HubSerialNumber = device.SerialNumber;
						usbhub2.HubDriverKey = device.DriverKey;
						Marshal.FreeHGlobal(intPtr2);
						USB.CloseHandle(intPtr);
					}
					usbhub = usbhub2;
				}
				return usbhub;
			}

			// Token: 0x04001054 RID: 4180
			internal int PortPortNumber;

			// Token: 0x04001055 RID: 4181
			internal string PortStatus;

			// Token: 0x04001056 RID: 4182
			internal string PortHubDevicePath;

			// Token: 0x04001057 RID: 4183
			internal string PortSpeed;

			// Token: 0x04001058 RID: 4184
			internal bool PortIsHub;

			// Token: 0x04001059 RID: 4185
			internal bool PortIsDeviceConnected;

			// Token: 0x0400105A RID: 4186
			internal USB.USB_DEVICE_DESCRIPTOR PortDeviceDescriptor;
		}

		// Token: 0x020002B7 RID: 695
		public class USBDevice
		{
			// Token: 0x06001E69 RID: 7785 RVA: 0x000B9B94 File Offset: 0x000B7D94
			public USBDevice()
			{
				this.DevicePortNumber = 0;
				this.DeviceHubDevicePath = "";
				this.DeviceDriverKey = "";
				this.DeviceManufacturer = "";
				this.DeviceProduct = "Unknown USB Device";
				this.DeviceSerialNumber = "";
				this.DeviceName = "";
				this.DeviceInstanceID = "";
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000B9C00 File Offset: 0x000B7E00
			public int PortNumber
			{
				get
				{
					return this.DevicePortNumber;
				}
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000B9C18 File Offset: 0x000B7E18
			public string HubDevicePath
			{
				get
				{
					return this.DeviceHubDevicePath;
				}
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000B9C30 File Offset: 0x000B7E30
			public string DriverKey
			{
				get
				{
					return this.DeviceDriverKey;
				}
			}

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000B9C48 File Offset: 0x000B7E48
			public string InstanceID
			{
				get
				{
					return this.DeviceInstanceID;
				}
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000B9C60 File Offset: 0x000B7E60
			public string Name
			{
				get
				{
					return this.DeviceName;
				}
			}

			// Token: 0x17000806 RID: 2054
			// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000B9C78 File Offset: 0x000B7E78
			public string Manufacturer
			{
				get
				{
					return this.DeviceManufacturer;
				}
			}

			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000B9C90 File Offset: 0x000B7E90
			public string Product
			{
				get
				{
					return this.DeviceProduct;
				}
			}

			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
			public string SerialNumber
			{
				get
				{
					return this.DeviceSerialNumber;
				}
			}

			// Token: 0x0400105B RID: 4187
			internal int DevicePortNumber;

			// Token: 0x0400105C RID: 4188
			internal string DeviceDriverKey;

			// Token: 0x0400105D RID: 4189
			internal string DeviceHubDevicePath;

			// Token: 0x0400105E RID: 4190
			internal string DeviceInstanceID;

			// Token: 0x0400105F RID: 4191
			internal string DeviceName;

			// Token: 0x04001060 RID: 4192
			internal string DeviceManufacturer;

			// Token: 0x04001061 RID: 4193
			internal string DeviceProduct;

			// Token: 0x04001062 RID: 4194
			internal string DeviceSerialNumber;

			// Token: 0x04001063 RID: 4195
			internal USB.USB_DEVICE_DESCRIPTOR DeviceDescriptor;
		}
	}
}
