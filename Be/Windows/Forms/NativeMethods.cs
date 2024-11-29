using System;
using System.Runtime.InteropServices;

namespace Be.Windows.Forms
{
	// Token: 0x020000E6 RID: 230
	internal static class NativeMethods
	{
		// Token: 0x06000A64 RID: 2660
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);

		// Token: 0x06000A65 RID: 2661
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool ShowCaret(IntPtr hWnd);

		// Token: 0x06000A66 RID: 2662
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool DestroyCaret();

		// Token: 0x06000A67 RID: 2663
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetCaretPos(int X, int Y);

		// Token: 0x040005C4 RID: 1476
		public const int WM_KEYDOWN = 256;

		// Token: 0x040005C5 RID: 1477
		public const int WM_KEYUP = 257;

		// Token: 0x040005C6 RID: 1478
		public const int WM_CHAR = 258;
	}
}
