using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PS3SaveEditor;

namespace Be.Windows.Forms
{
	// Token: 0x020000E0 RID: 224
	[ToolboxBitmap(typeof(HexBox), "HexBox.bmp")]
	public class HexBox : Control
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00037ABC File Offset: 0x00035CBC
		public long ScrollVMax
		{
			get
			{
				return this._scrollVmax;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00037AD4 File Offset: 0x00035CD4
		public long ScrollVMin
		{
			get
			{
				return this._scrollVmin;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00037AEC File Offset: 0x00035CEC
		public long ScrollHMax
		{
			get
			{
				return this._scrollHmax;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00037B04 File Offset: 0x00035D04
		public long ScrollHMin
		{
			get
			{
				return this._scrollHmin;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00037B1C File Offset: 0x00035D1C
		public VScrollBar VScrollBar
		{
			get
			{
				return this._vScrollBar;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00037B34 File Offset: 0x00035D34
		public HScrollBar HScrollBar
		{
			get
			{
				return this._hScrollBar;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000972 RID: 2418 RVA: 0x00037B4C File Offset: 0x00035D4C
		// (remove) Token: 0x06000973 RID: 2419 RVA: 0x00037B84 File Offset: 0x00035D84
		[Description("Occurs, when the value of InsertActive property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler InsertActiveChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000974 RID: 2420 RVA: 0x00037BBC File Offset: 0x00035DBC
		// (remove) Token: 0x06000975 RID: 2421 RVA: 0x00037BF4 File Offset: 0x00035DF4
		[Description("Occurs, when the value of ReadOnly property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ReadOnlyChanged;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000976 RID: 2422 RVA: 0x00037C2C File Offset: 0x00035E2C
		// (remove) Token: 0x06000977 RID: 2423 RVA: 0x00037C64 File Offset: 0x00035E64
		[Description("Occurs, when the value of ByteProvider property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ByteProviderChanged;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000978 RID: 2424 RVA: 0x00037C9C File Offset: 0x00035E9C
		// (remove) Token: 0x06000979 RID: 2425 RVA: 0x00037CD4 File Offset: 0x00035ED4
		[Description("Occurs, when the value of SelectionStart property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler SelectionStartChanged;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600097A RID: 2426 RVA: 0x00037D0C File Offset: 0x00035F0C
		// (remove) Token: 0x0600097B RID: 2427 RVA: 0x00037D44 File Offset: 0x00035F44
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler VScroll;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600097C RID: 2428 RVA: 0x00037D7C File Offset: 0x00035F7C
		// (remove) Token: 0x0600097D RID: 2429 RVA: 0x00037DB4 File Offset: 0x00035FB4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler HScroll;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600097E RID: 2430 RVA: 0x00037DEC File Offset: 0x00035FEC
		// (remove) Token: 0x0600097F RID: 2431 RVA: 0x00037E24 File Offset: 0x00036024
		[Description("Occurs, when the value of SelectionLength property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler SelectionLengthChanged;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000980 RID: 2432 RVA: 0x00037E5C File Offset: 0x0003605C
		// (remove) Token: 0x06000981 RID: 2433 RVA: 0x00037E94 File Offset: 0x00036094
		[Description("Occurs, when the value of LineInfoVisible property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LineInfoVisibleChanged;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000982 RID: 2434 RVA: 0x00037ECC File Offset: 0x000360CC
		// (remove) Token: 0x06000983 RID: 2435 RVA: 0x00037F04 File Offset: 0x00036104
		[Description("Occurs, when the value of StringViewVisible property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler StringViewVisibleChanged;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000984 RID: 2436 RVA: 0x00037F3C File Offset: 0x0003613C
		// (remove) Token: 0x06000985 RID: 2437 RVA: 0x00037F74 File Offset: 0x00036174
		[Description("Occurs, when the value of BorderStyle property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler BorderStyleChanged;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000986 RID: 2438 RVA: 0x00037FAC File Offset: 0x000361AC
		// (remove) Token: 0x06000987 RID: 2439 RVA: 0x00037FE4 File Offset: 0x000361E4
		[Description("Occurs, when the value of BytesPerLine property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler BytesPerLineChanged;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000988 RID: 2440 RVA: 0x0003801C File Offset: 0x0003621C
		// (remove) Token: 0x06000989 RID: 2441 RVA: 0x00038054 File Offset: 0x00036254
		[Description("Occurs, when the value of UseFixedBytesPerLine property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler UseFixedBytesPerLineChanged;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600098A RID: 2442 RVA: 0x0003808C File Offset: 0x0003628C
		// (remove) Token: 0x0600098B RID: 2443 RVA: 0x000380C4 File Offset: 0x000362C4
		[Description("Occurs, when the value of VScrollBarVisible property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler VScrollBarVisibleChanged;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600098C RID: 2444 RVA: 0x000380FC File Offset: 0x000362FC
		// (remove) Token: 0x0600098D RID: 2445 RVA: 0x00038134 File Offset: 0x00036334
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler HScrollBarVisibleChanged;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600098E RID: 2446 RVA: 0x0003816C File Offset: 0x0003636C
		// (remove) Token: 0x0600098F RID: 2447 RVA: 0x000381A4 File Offset: 0x000363A4
		[Description("Occurs, when the value of HexCasing property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler HexCasingChanged;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000990 RID: 2448 RVA: 0x000381DC File Offset: 0x000363DC
		// (remove) Token: 0x06000991 RID: 2449 RVA: 0x00038214 File Offset: 0x00036414
		[Description("Occurs, when the value of HorizontalByteCount property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler HorizontalByteCountChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000992 RID: 2450 RVA: 0x0003824C File Offset: 0x0003644C
		// (remove) Token: 0x06000993 RID: 2451 RVA: 0x00038284 File Offset: 0x00036484
		[Description("Occurs, when the value of VerticalByteCount property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler VerticalByteCountChanged;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000994 RID: 2452 RVA: 0x000382BC File Offset: 0x000364BC
		// (remove) Token: 0x06000995 RID: 2453 RVA: 0x000382F4 File Offset: 0x000364F4
		[Description("Occurs, when the value of CurrentLine property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CurrentLineChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000996 RID: 2454 RVA: 0x0003832C File Offset: 0x0003652C
		// (remove) Token: 0x06000997 RID: 2455 RVA: 0x00038364 File Offset: 0x00036564
		[Description("Occurs, when the value of CurrentPositionInLine property has changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CurrentPositionInLineChanged;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000998 RID: 2456 RVA: 0x0003839C File Offset: 0x0003659C
		// (remove) Token: 0x06000999 RID: 2457 RVA: 0x000383D4 File Offset: 0x000365D4
		[Description("Occurs, when Copy method was invoked and ClipBoardData changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler Copied;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600099A RID: 2458 RVA: 0x0003840C File Offset: 0x0003660C
		// (remove) Token: 0x0600099B RID: 2459 RVA: 0x00038444 File Offset: 0x00036644
		[Description("Occurs, when CopyHex method was invoked and ClipBoardData changed.")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CopiedHex;

		// Token: 0x0600099C RID: 2460 RVA: 0x0003847C File Offset: 0x0003667C
		public HexBox()
		{
			this.SelectAddresses = new Dictionary<long, byte>();
			this._vScrollBar = new VScrollBar();
			this._vScrollBar.Scroll += this._vScrollBar_Scroll;
			this._hScrollBar = new HScrollBar();
			this._hScrollBar.Scroll += this._hScrollBar_Scroll;
			this.tip = new ToolTip();
			this.tip.ReshowDelay = 0;
			this.tip.Disposed += this.tip_Disposed;
			this._builtInContextMenu = null;
			this.BackColor = Color.White;
			this.Font = new Font("Courier New", Util.ScaleSize(9f), FontStyle.Regular, GraphicsUnit.Point, 0);
			this._stringFormat = new StringFormat(StringFormat.GenericTypographic);
			this._stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
			this.ActivateEmptyKeyInterpreter();
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this._thumbTrackTimer.Interval = 50;
			this._thumbTrackTimer.Tick += this.PerformScrollThumbTrack;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000386A8 File Offset: 0x000368A8
		private void _hScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			switch (e.Type)
			{
			case ScrollEventType.SmallDecrement:
			case ScrollEventType.SmallIncrement:
			case ScrollEventType.LargeDecrement:
			case ScrollEventType.LargeIncrement:
			{
				long num = this.FromHScrollPos(e.NewValue);
				this.PerformHScrollThumpPosition(num);
				break;
			}
			case ScrollEventType.ThumbPosition:
			{
				long num2 = this.FromHScrollPos(e.NewValue);
				this.PerformHScrollThumpPosition(num2);
				break;
			}
			case ScrollEventType.ThumbTrack:
			{
				long num3 = this.FromHScrollPos(e.NewValue);
				this.PerformHScrollThumpPosition(num3);
				break;
			}
			}
			e.NewValue = this.ToHScrollPos(this._scrollHpos);
			this.OnHScroll(EventArgs.Empty);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000021C5 File Offset: 0x000003C5
		private void tip_Disposed(object sender, EventArgs e)
		{
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00038750 File Offset: 0x00036950
		private void _vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			switch (e.Type)
			{
			case ScrollEventType.SmallDecrement:
				this.PerformScrollLineUp();
				break;
			case ScrollEventType.SmallIncrement:
				this.PerformScrollLineDown();
				break;
			case ScrollEventType.LargeDecrement:
				this.PerformScrollPageUp();
				break;
			case ScrollEventType.LargeIncrement:
				this.PerformScrollPageDown();
				break;
			case ScrollEventType.ThumbPosition:
			{
				long num = this.FromVScrollPos(e.NewValue);
				this.PerformVScrollThumpPosition(num);
				break;
			}
			case ScrollEventType.ThumbTrack:
			{
				int tickCount = Environment.TickCount;
				this._thumbTrackPosition = this.FromVScrollPos(e.NewValue);
				this.PerformScrollThumbTrack(null, null);
				this._lastThumbtrack = tickCount;
				break;
			}
			}
			e.NewValue = this.ToVScrollPos(this._scrollVpos);
			this.OnScroll(EventArgs.Empty);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003881E File Offset: 0x00036A1E
		private void PerformScrollThumbTrack(object sender, EventArgs e)
		{
			this._thumbTrackTimer.Enabled = false;
			this.PerformVScrollThumpPosition(this._thumbTrackPosition);
			this._lastThumbtrack = Environment.TickCount;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00038848 File Offset: 0x00036A48
		private void UpdateVScrollSize()
		{
			bool flag = this.VScrollBarVisible && this._byteProvider != null && this._byteProvider.Length > 0L && this._iHexMaxHBytes != 0;
			if (flag)
			{
				long num = (long)Math.Ceiling((double)(this._byteProvider.Length + 1L) / (double)this._iHexMaxHBytes - (double)this._iHexMaxVBytes);
				num = Math.Max(0L, num);
				long num2 = this._startByte / (long)this._iHexMaxHBytes;
				bool flag2 = num < this._scrollVmax;
				if (flag2)
				{
					bool flag3 = this._scrollVpos == this._scrollVmax;
					if (flag3)
					{
						this.PerformScrollLineUp();
					}
				}
				bool flag4 = num == this._scrollVmax && num2 == this._scrollVpos;
				if (!flag4)
				{
					this._scrollVmin = 0L;
					this._scrollVmax = num;
					this._scrollVpos = Math.Min(num2, num);
					this.UpdateVScroll();
				}
			}
			else
			{
				bool vscrollBarVisible = this.VScrollBarVisible;
				if (vscrollBarVisible)
				{
					this._scrollVmin = 0L;
					this._scrollVmax = 0L;
					this._scrollVpos = 0L;
					this.UpdateVScroll();
				}
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00038964 File Offset: 0x00036B64
		private void UpdateHScrollSize()
		{
			bool flag = this.HScrollBarVisible && this._byteProvider != null && this._byteProvider.Length > 0L && this._iHexMaxHBytes != 0;
			if (flag)
			{
				long num = (long)(this._recHex.Width + (this.StringViewVisible ? this._recStringView.Width : 0) + (this.LineInfoVisible ? this._recLineInfo.Width : 0) - this._recContent.Width + 15);
				num = Math.Max(0L, num);
				long num2 = 0L;
				bool flag2 = num < this._scrollHmax;
				if (flag2)
				{
				}
				bool flag3 = num == this._scrollHmax && num2 == this._scrollHpos;
				if (!flag3)
				{
					this._scrollHmin = 0L;
					this._scrollHmax = num;
					this._scrollHpos = Math.Min(num2, num);
					this.UpdateHScroll();
				}
			}
			else
			{
				bool hscrollBarVisible = this.HScrollBarVisible;
				if (hscrollBarVisible)
				{
					this._scrollHmin = 0L;
					this._scrollHmax = 0L;
					this._scrollHpos = 0L;
					this.UpdateHScroll();
				}
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00038A7C File Offset: 0x00036C7C
		private void UpdateVScroll()
		{
			int num = this.ToScrollMax(this._scrollVmax);
			bool flag = num > 0;
			if (flag)
			{
				this._vScrollBar.Minimum = 0;
				this._vScrollBar.Maximum = num;
				this._vScrollBar.Value = this.ToVScrollPos(this._scrollVpos);
				this._vScrollBar.Enabled = true;
			}
			else
			{
				this._vScrollBar.Enabled = false;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00038AF4 File Offset: 0x00036CF4
		private void UpdateHScroll()
		{
			int num = this.ToHScrollMax(this._scrollHmax);
			bool flag = num > 0;
			if (flag)
			{
				this._hScrollBar.Minimum = 0;
				this._hScrollBar.Maximum = num;
				this._hScrollBar.Value = this.ToHScrollPos(this._scrollHpos);
				this._hScrollBar.Enabled = true;
			}
			else
			{
				this._hScrollBar.Enabled = false;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00038B6C File Offset: 0x00036D6C
		private int ToHScrollPos(long value)
		{
			int num = 200;
			bool flag = this._scrollHmax < (long)num;
			int num2;
			if (flag)
			{
				num2 = (int)value;
			}
			else
			{
				double num3 = (double)value / (double)this._scrollHmax * 100.0;
				int num4 = (int)Math.Floor((double)num / 100.0 * num3);
				num4 = (int)Math.Max(this._scrollHmin, (long)num4);
				num4 = (int)Math.Min(this._scrollHmax, (long)num4);
				num2 = num4;
			}
			return num2;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00038BE8 File Offset: 0x00036DE8
		private int ToVScrollPos(long value)
		{
			int num = 65535;
			bool flag = this._scrollVmax < (long)num;
			int num2;
			if (flag)
			{
				num2 = (int)value;
			}
			else
			{
				double num3 = (double)value / (double)this._scrollVmax * 100.0;
				int num4 = (int)Math.Floor((double)num / 100.0 * num3);
				num4 = (int)Math.Max(this._scrollVmin, (long)num4);
				num4 = (int)Math.Min(this._scrollVmax, (long)num4);
				num2 = num4;
			}
			return num2;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00038C64 File Offset: 0x00036E64
		private long FromVScrollPos(int value)
		{
			int num = 65535;
			bool flag = this._scrollVmax < (long)num;
			long num2;
			if (flag)
			{
				num2 = (long)value;
			}
			else
			{
				double num3 = (double)value / (double)num * 100.0;
				long num4 = (long)((int)Math.Floor((double)this._scrollVmax / 100.0 * num3));
				num2 = num4;
			}
			return num2;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00038CC0 File Offset: 0x00036EC0
		private long FromHScrollPos(int value)
		{
			int num = 200;
			bool flag = this._scrollHmax < (long)num;
			long num2;
			if (flag)
			{
				num2 = (long)value;
			}
			else
			{
				double num3 = (double)value / (double)num * 100.0;
				long num4 = (long)((int)Math.Floor((double)this._scrollHmax / 100.0 * num3));
				num2 = num4;
			}
			return num2;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00038D1C File Offset: 0x00036F1C
		private int ToScrollMax(long value)
		{
			long num = 65535L;
			bool flag = value > num;
			int num2;
			if (flag)
			{
				num2 = (int)num;
			}
			else
			{
				num2 = (int)value;
			}
			return num2;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00038D44 File Offset: 0x00036F44
		private int ToHScrollMax(long value)
		{
			long num = 200L;
			bool flag = value > num;
			int num2;
			if (flag)
			{
				num2 = (int)num;
			}
			else
			{
				num2 = (int)value;
			}
			return num2;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00038D6C File Offset: 0x00036F6C
		public void PerformScrollToLine(long pos)
		{
			bool flag = pos < this._scrollVmin || pos > this._scrollVmax || pos == this._scrollVpos;
			if (!flag)
			{
				this._scrollVpos = pos;
				this.UpdateVScroll();
				this.UpdateVisibilityBytes();
				this.UpdateCaret();
				this.Refresh();
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00038DC4 File Offset: 0x00036FC4
		public void PerformHScroll(long pos)
		{
			bool flag = pos < this._scrollHmin || pos > this._scrollHmax || pos == this._scrollHpos;
			if (!flag)
			{
				this._scrollHpos = pos;
				this.UpdateHScroll();
				this.UpdateVisibilityBytes();
				this.UpdateCaret();
				base.Invalidate();
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00038E1C File Offset: 0x0003701C
		private void PerformScrollLines(int lines)
		{
			bool flag = lines > 0;
			long num;
			if (flag)
			{
				num = Math.Min(this._scrollVmax, this._scrollVpos + (long)lines);
			}
			else
			{
				bool flag2 = lines < 0;
				if (!flag2)
				{
					return;
				}
				num = Math.Max(this._scrollVmin, this._scrollVpos + (long)lines);
			}
			this.PerformScrollToLine(num);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00038E77 File Offset: 0x00037077
		private void PerformScrollLineDown()
		{
			this.PerformScrollLines(1);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00038E82 File Offset: 0x00037082
		private void PerformScrollLineUp()
		{
			this.PerformScrollLines(-1);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00038E8D File Offset: 0x0003708D
		private void PerformScrollPageDown()
		{
			this.PerformScrollLines(this._iHexMaxVBytes);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00038E9D File Offset: 0x0003709D
		private void PerformScrollPageUp()
		{
			this.PerformScrollLines(-this._iHexMaxVBytes);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00038EB0 File Offset: 0x000370B0
		private void PerformVScrollThumpPosition(long pos)
		{
			int num = ((this._scrollVmax > 65535L) ? 10 : 9);
			bool flag = this.ToVScrollPos(pos) == this.ToScrollMax(this._scrollVmax) - num;
			if (flag)
			{
				pos = this._scrollVmax;
			}
			this.PerformScrollToLine(pos);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00038F00 File Offset: 0x00037100
		private void PerformHScrollThumpPosition(long pos)
		{
			bool flag = this.ToHScrollPos(pos) == this.ToHScrollMax(this._scrollHmax);
			if (flag)
			{
				pos = this._scrollHmax;
			}
			this.PerformHScroll(pos);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00038F37 File Offset: 0x00037137
		public void ScrollByteIntoView()
		{
			this.ScrollByteIntoView(this._bytePos);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00038F48 File Offset: 0x00037148
		public void ScrollByteIntoView(long index)
		{
			bool flag = this._byteProvider == null || this._keyInterpreter == null;
			if (!flag)
			{
				bool flag2 = index < this._startByte;
				if (flag2)
				{
					long num = (long)Math.Floor((double)index / (double)this._iHexMaxHBytes);
					this.PerformVScrollThumpPosition(num);
				}
				else
				{
					bool flag3 = index > this._endByte;
					if (flag3)
					{
						long num2 = (long)Math.Floor((double)index / (double)this._iHexMaxHBytes);
						num2 -= (long)(this._iHexMaxVBytes - 1);
						this.PerformVScrollThumpPosition(num2);
					}
				}
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00038FD4 File Offset: 0x000371D4
		private void ReleaseSelection()
		{
			bool flag = this._selectionLength == 0L;
			if (!flag)
			{
				this._selectionLength = 0L;
				this.OnSelectionLengthChanged(EventArgs.Empty);
				bool flag2 = !this._caretVisible;
				if (flag2)
				{
					this.CreateCaret();
				}
				else
				{
					this.UpdateCaret();
				}
				base.Invalidate();
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003902C File Offset: 0x0003722C
		public bool CanSelectAll()
		{
			bool flag = !base.Enabled;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this._byteProvider == null;
				flag2 = !flag3;
			}
			return flag2;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00039064 File Offset: 0x00037264
		public void SelectAll()
		{
			bool flag = this.ByteProvider == null;
			if (!flag)
			{
				this.Select(0L, this.ByteProvider.Length);
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00039098 File Offset: 0x00037298
		public void Select(long start, long length)
		{
			bool flag = this.ByteProvider == null;
			if (!flag)
			{
				bool flag2 = !base.Enabled;
				if (!flag2)
				{
					this.InternalSelect(start, length);
					this.ScrollByteIntoView();
				}
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000390D4 File Offset: 0x000372D4
		private void InternalSelect(long start, long length)
		{
			int num = 0;
			bool flag = length > 0L && this._caretVisible;
			if (flag)
			{
				this.DestroyCaret();
			}
			else
			{
				bool flag2 = length == 0L && !this._caretVisible;
				if (flag2)
				{
					this.CreateCaret();
				}
			}
			this.SetPosition(start, num);
			this.SetSelectionLength(length);
			this.UpdateCaret();
			base.Invalidate();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00039140 File Offset: 0x00037340
		private void ActivateEmptyKeyInterpreter()
		{
			bool flag = this._eki == null;
			if (flag)
			{
				this._eki = new HexBox.EmptyKeyInterpreter(this);
			}
			bool flag2 = this._eki == this._keyInterpreter;
			if (!flag2)
			{
				bool flag3 = this._keyInterpreter != null;
				if (flag3)
				{
					this._keyInterpreter.Deactivate();
				}
				this._keyInterpreter = this._eki;
				this._keyInterpreter.Activate();
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000391AC File Offset: 0x000373AC
		private void ActivateKeyInterpreter()
		{
			bool flag = this._ki == null;
			if (flag)
			{
				this._ki = new HexBox.KeyInterpreter(this);
			}
			bool flag2 = this._ki == this._keyInterpreter;
			if (!flag2)
			{
				bool flag3 = this._keyInterpreter != null;
				if (flag3)
				{
					this._keyInterpreter.Deactivate();
				}
				this._keyInterpreter = this._ki;
				this._keyInterpreter.Activate();
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00039218 File Offset: 0x00037418
		private void ActivateStringKeyInterpreter()
		{
			bool flag = this._ski == null;
			if (flag)
			{
				this._ski = new HexBox.StringKeyInterpreter(this);
			}
			bool flag2 = this._ski == this._keyInterpreter;
			if (!flag2)
			{
				bool flag3 = this._keyInterpreter != null;
				if (flag3)
				{
					this._keyInterpreter.Deactivate();
				}
				this._keyInterpreter = this._ski;
				this._keyInterpreter.Activate();
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00039284 File Offset: 0x00037484
		private void CreateCaret()
		{
			bool flag = this._byteProvider == null || this._keyInterpreter == null || this._caretVisible || !this.Focused;
			if (!flag)
			{
				int num = (this.InsertActive ? 1 : ((int)this._charSize.Width));
				int num2 = (int)this._charSize.Height;
				bool flag2 = Util.IsUnixOrMacOSX();
				if (!flag2)
				{
					NativeMethods.CreateCaret(base.Handle, this.m_hCaret, num, num2);
				}
				this.UpdateCaret();
				bool flag3 = Util.IsUnixOrMacOSX();
				if (!flag3)
				{
					NativeMethods.ShowCaret(base.Handle);
				}
				this._caretVisible = true;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00039330 File Offset: 0x00037530
		private void UpdateCaret()
		{
			bool flag = this._byteProvider == null || this._keyInterpreter == null;
			if (!flag)
			{
				long num = this._bytePos - this._startByte;
				PointF caretPointF = this._keyInterpreter.GetCaretPointF(num);
				caretPointF.X += (float)this._byteCharacterPos * this._charSize.Width;
				bool flag2 = Util.IsUnixOrMacOSX();
				if (!flag2)
				{
					NativeMethods.SetCaretPos((int)caretPointF.X, (int)caretPointF.Y);
				}
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000393BC File Offset: 0x000375BC
		private void DestroyCaret()
		{
			bool flag = !this._caretVisible;
			if (!flag)
			{
				bool flag2 = Util.IsUnixOrMacOSX();
				if (!flag2)
				{
					NativeMethods.DestroyCaret();
				}
				this._caretVisible = false;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000393F8 File Offset: 0x000375F8
		private void SetCaretPosition(Point p)
		{
			bool flag = this._byteProvider == null || this._keyInterpreter == null;
			if (!flag)
			{
				long num = this._bytePos;
				int num2 = this._byteCharacterPos;
				bool flag2 = this._recHex.Contains(p);
				if (flag2)
				{
					BytePositionInfo hexBytePositionInfo = this.GetHexBytePositionInfo(p);
					bool flag3 = hexBytePositionInfo.Index - (long)hexBytePositionInfo.CharacterPosition < this._byteProvider.Length;
					if (flag3)
					{
						num = hexBytePositionInfo.Index;
						num2 = hexBytePositionInfo.CharacterPosition;
						this.SetPosition(num, num2);
						this.ActivateKeyInterpreter();
						this.UpdateCaret();
						base.Invalidate();
					}
				}
				else
				{
					bool flag4 = this._recStringView.Contains(p);
					if (flag4)
					{
						BytePositionInfo stringBytePositionInfo = this.GetStringBytePositionInfo(p);
						bool flag5 = stringBytePositionInfo.Index - (long)stringBytePositionInfo.CharacterPosition < this._byteProvider.Length;
						if (flag5)
						{
							num = stringBytePositionInfo.Index;
							num2 = stringBytePositionInfo.CharacterPosition;
							this.SetPosition(num, num2);
							this.ActivateStringKeyInterpreter();
							this.UpdateCaret();
							base.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00039514 File Offset: 0x00037714
		private BytePositionInfo GetHexBytePositionInfo(Point p)
		{
			float num = (float)(p.X - this._recHex.X) / this._charSize.Width;
			float num2 = (float)(p.Y - this._recHex.Y) / this._charSize.Height;
			int num3 = (int)num;
			int num4 = (int)num2;
			int num5 = num3 / 3 + 1;
			long num6 = Math.Min(this._byteProvider.Length, this._startByte + (long)(this._iHexMaxHBytes * (num4 + 1) - this._iHexMaxHBytes) + (long)num5 - 1L);
			int num7 = num3 % 3;
			bool flag = num7 > 1;
			if (flag)
			{
				num7 = 1;
			}
			bool flag2 = num6 == this._byteProvider.Length;
			if (flag2)
			{
				num7 = 0;
			}
			bool flag3 = num6 < 0L;
			BytePositionInfo bytePositionInfo;
			if (flag3)
			{
				bytePositionInfo = new BytePositionInfo(0L, 0);
			}
			else
			{
				bytePositionInfo = new BytePositionInfo(num6, num7);
			}
			return bytePositionInfo;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000395F4 File Offset: 0x000377F4
		private BytePositionInfo GetStringBytePositionInfo(Point p)
		{
			float num = (float)(p.X - this._recStringView.X) / this._charSize.Width;
			float num2 = (float)(p.Y - this._recStringView.Y) / this._charSize.Height;
			int num3 = (int)num;
			int num4 = (int)num2;
			int num5 = num3 + 1;
			long num6 = Math.Min(this._byteProvider.Length, this._startByte + (long)(this._iHexMaxHBytes * (num4 + 1) - this._iHexMaxHBytes) + (long)num5 - 1L);
			int num7 = 0;
			bool flag = num6 < 0L;
			BytePositionInfo bytePositionInfo;
			if (flag)
			{
				bytePositionInfo = new BytePositionInfo(0L, 0);
			}
			else
			{
				bytePositionInfo = new BytePositionInfo(num6, num7);
			}
			return bytePositionInfo;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000396AC File Offset: 0x000378AC
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
		public override bool PreProcessMessage(ref Message m)
		{
			bool flag;
			switch (m.Msg)
			{
			case 256:
				flag = this._keyInterpreter.PreProcessWmKeyDown(ref m);
				break;
			case 257:
				flag = this._keyInterpreter.PreProcessWmKeyUp(ref m);
				break;
			case 258:
				flag = this._keyInterpreter.PreProcessWmChar(ref m);
				break;
			default:
				flag = base.PreProcessMessage(ref m);
				break;
			}
			return flag;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00039714 File Offset: 0x00037914
		private bool BasePreProcessMessage(ref Message m)
		{
			return base.PreProcessMessage(ref m);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00039730 File Offset: 0x00037930
		public long Find(byte[] bytes, long startIndex)
		{
			int num = 0;
			int num2 = bytes.Length;
			this._abortFind = false;
			long num3 = startIndex;
			while (num3 < this._byteProvider.Length)
			{
				bool abortFind = this._abortFind;
				if (!abortFind)
				{
					bool flag = num3 % 1000L == 0L;
					if (flag)
					{
						Application.DoEvents();
					}
					bool flag2 = this._byteProvider.ReadByte(num3) != bytes[num];
					if (flag2)
					{
						num3 -= (long)num;
						num = 0;
						this._findingPos = num3;
					}
					else
					{
						num++;
						bool flag3 = num == num2;
						if (flag3)
						{
							long num4 = num3 - (long)num2 + 1L;
							this.Select(num4, (long)num2);
							this.ScrollByteIntoView(this._bytePos + this._selectionLength);
							this.ScrollByteIntoView(this._bytePos);
							return num4;
						}
					}
					num3 += 1L;
					continue;
				}
				return -2L;
			}
			return -1L;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00039819 File Offset: 0x00037A19
		public void AbortFind()
		{
			this._abortFind = true;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00039824 File Offset: 0x00037A24
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long CurrentFindingPosition
		{
			get
			{
				return this._findingPos;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0003983C File Offset: 0x00037A3C
		private byte[] GetCopyData()
		{
			bool flag = !this.CanCopy();
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				byte[] array2 = new byte[this._selectionLength];
				int num = -1;
				for (long num2 = this._bytePos; num2 < this._bytePos + this._selectionLength; num2 += 1L)
				{
					num++;
					array2[num] = this._byteProvider.ReadByte(num2);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000398B4 File Offset: 0x00037AB4
		public void Copy()
		{
			bool flag = !this.CanCopy();
			if (!flag)
			{
				byte[] copyData = this.GetCopyData();
				DataObject dataObject = new DataObject();
				string @string = Encoding.ASCII.GetString(copyData, 0, copyData.Length);
				dataObject.SetData(typeof(string), @string);
				MemoryStream memoryStream = new MemoryStream(copyData, 0, copyData.Length, false, true);
				dataObject.SetData("BinaryData", memoryStream);
				Clipboard.SetDataObject(dataObject, true);
				this.UpdateCaret();
				this.ScrollByteIntoView();
				base.Invalidate();
				this.OnCopied(EventArgs.Empty);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00039948 File Offset: 0x00037B48
		public bool CanCopy()
		{
			bool flag = this._selectionLength < 1L || this._byteProvider == null;
			return !flag;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003997C File Offset: 0x00037B7C
		public void Cut()
		{
			bool flag = !this.CanCut();
			if (!flag)
			{
				this.Copy();
				this._byteProvider.DeleteBytes(this._bytePos, this._selectionLength);
				this._byteCharacterPos = 0;
				this.UpdateCaret();
				this.ScrollByteIntoView();
				this.ReleaseSelection();
				base.Invalidate();
				this.Refresh();
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000399E4 File Offset: 0x00037BE4
		public bool CanCut()
		{
			bool flag = this.ReadOnly || !base.Enabled;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this._byteProvider == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = this._selectionLength < 1L || !this._byteProvider.SupportsDeleteBytes();
					flag2 = !flag4;
				}
			}
			return flag2;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00039A48 File Offset: 0x00037C48
		public void Paste()
		{
			bool flag = !this.CanPaste();
			if (!flag)
			{
				bool flag2 = this._selectionLength > 0L;
				if (flag2)
				{
					this._byteProvider.DeleteBytes(this._bytePos, this._selectionLength);
				}
				IDataObject dataObject = Clipboard.GetDataObject();
				bool dataPresent = dataObject.GetDataPresent("BinaryData");
				byte[] array;
				if (dataPresent)
				{
					MemoryStream memoryStream = (MemoryStream)dataObject.GetData("BinaryData");
					array = new byte[memoryStream.Length];
					memoryStream.Read(array, 0, array.Length);
				}
				else
				{
					bool dataPresent2 = dataObject.GetDataPresent(typeof(string));
					if (!dataPresent2)
					{
						return;
					}
					string text = (string)dataObject.GetData(typeof(string));
					array = Encoding.ASCII.GetBytes(text);
				}
				this._byteProvider.InsertBytes(this._bytePos, array);
				this.SetPosition(this._bytePos + (long)array.Length, 0);
				this.ReleaseSelection();
				this.ScrollByteIntoView();
				this.UpdateCaret();
				base.Invalidate();
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00039B5C File Offset: 0x00037D5C
		public bool CanPaste()
		{
			bool flag = this.ReadOnly || !base.Enabled;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this._byteProvider == null || !this._byteProvider.SupportsInsertBytes();
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = !this._byteProvider.SupportsDeleteBytes() && this._selectionLength > 0L;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						IDataObject dataObject = Clipboard.GetDataObject();
						bool dataPresent = dataObject.GetDataPresent("BinaryData");
						if (dataPresent)
						{
							flag2 = true;
						}
						else
						{
							bool dataPresent2 = dataObject.GetDataPresent(typeof(string));
							flag2 = dataPresent2;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00039C08 File Offset: 0x00037E08
		public bool CanPasteHex()
		{
			bool flag = !this.CanPaste();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				IDataObject dataObject = Clipboard.GetDataObject();
				bool dataPresent = dataObject.GetDataPresent(typeof(string));
				if (dataPresent)
				{
					string text = (string)dataObject.GetData(typeof(string));
					byte[] array = this.ConvertHexToBytes(text);
					flag2 = array != null;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00039C74 File Offset: 0x00037E74
		public void PasteHex()
		{
			bool flag = !this.CanPaste();
			if (!flag)
			{
				IDataObject dataObject = Clipboard.GetDataObject();
				bool dataPresent = dataObject.GetDataPresent(typeof(string));
				if (dataPresent)
				{
					string text = (string)dataObject.GetData(typeof(string));
					byte[] array = this.ConvertHexToBytes(text);
					bool flag2 = array == null;
					if (!flag2)
					{
						bool flag3 = this._selectionLength > 0L;
						if (flag3)
						{
							this._byteProvider.DeleteBytes(this._bytePos, this._selectionLength);
						}
						this._byteProvider.InsertBytes(this._bytePos, array);
						this.SetPosition(this._bytePos + (long)array.Length, 0);
						this.ReleaseSelection();
						this.ScrollByteIntoView();
						this.UpdateCaret();
						base.Invalidate();
					}
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00039D50 File Offset: 0x00037F50
		public void CopyHex()
		{
			bool flag = !this.CanCopy();
			if (!flag)
			{
				byte[] copyData = this.GetCopyData();
				DataObject dataObject = new DataObject();
				string text = this.ConvertBytesToHex(copyData);
				dataObject.SetData(typeof(string), text);
				MemoryStream memoryStream = new MemoryStream(copyData, 0, copyData.Length, false, true);
				dataObject.SetData("BinaryData", memoryStream);
				Clipboard.SetDataObject(dataObject, true);
				this.UpdateCaret();
				this.ScrollByteIntoView();
				base.Invalidate();
				this.OnCopiedHex(EventArgs.Empty);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00039DDC File Offset: 0x00037FDC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			BorderStyle borderStyle = this._borderStyle;
			if (borderStyle != BorderStyle.FixedSingle)
			{
				if (borderStyle == BorderStyle.Fixed3D)
				{
					bool isSupported = TextBoxRenderer.IsSupported;
					if (isSupported)
					{
						VisualStyleElement visualStyleElement = VisualStyleElement.TextBox.TextEdit.Normal;
						Color color = this.BackColor;
						bool enabled = base.Enabled;
						if (enabled)
						{
							bool readOnly = this.ReadOnly;
							if (readOnly)
							{
								visualStyleElement = VisualStyleElement.TextBox.TextEdit.ReadOnly;
							}
							else
							{
								bool focused = this.Focused;
								if (focused)
								{
									visualStyleElement = VisualStyleElement.TextBox.TextEdit.Focused;
								}
							}
						}
						else
						{
							visualStyleElement = VisualStyleElement.TextBox.TextEdit.Disabled;
							color = this.BackColorDisabled;
						}
						VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
						visualStyleRenderer.DrawBackground(e.Graphics, base.ClientRectangle);
						Rectangle backgroundContentRectangle = visualStyleRenderer.GetBackgroundContentRectangle(e.Graphics, base.ClientRectangle);
						e.Graphics.FillRectangle(new SolidBrush(color), backgroundContentRectangle);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
						ControlPaint.DrawBorder3D(e.Graphics, base.ClientRectangle, Border3DStyle.Sunken);
					}
				}
			}
			else
			{
				e.Graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
				ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00039F18 File Offset: 0x00038118
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			bool flag = this._byteProvider == null;
			if (!flag)
			{
				Region region = new Region(base.ClientRectangle);
				region.Exclude(this._recContent);
				e.Graphics.ExcludeClip(region);
				this.UpdateVisibilityBytes();
				bool lineInfoVisible = this._lineInfoVisible;
				if (lineInfoVisible)
				{
					this.PaintLineInfo(e.Graphics, this._startByte, this._endByte);
				}
				bool flag2 = !this._stringViewVisible;
				if (flag2)
				{
					this.PaintHex(e.Graphics, this._startByte, this._endByte);
				}
				else
				{
					bool shadowSelectionVisible = this._shadowSelectionVisible;
					if (shadowSelectionVisible)
					{
						this.PaintCurrentBytesSign(e.Graphics);
					}
					this.PaintHexAndStringView(e.Graphics, this._startByte, this._endByte);
				}
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00039FF0 File Offset: 0x000381F0
		private void PaintLineInfo(Graphics g, long startByte, long endByte)
		{
			endByte = Math.Min(this._byteProvider.Length - 1L, endByte);
			Color color = ((this.LineInfoForeColor != Color.Empty) ? this.LineInfoForeColor : this.ForeColor);
			Brush brush = new SolidBrush(color);
			int num = this.GetGridBytePoint(endByte - startByte).Y + 1;
			for (int i = 0; i < num; i++)
			{
				long num2 = startByte + (long)(this._iHexMaxHBytes * i) + this._lineInfoOffset;
				PointF bytePointF = this.GetBytePointF(new Point(0, i));
				string text = num2.ToString(this._hexStringFormat, Thread.CurrentThread.CurrentCulture);
				int num3 = 8 - text.Length;
				bool flag = num3 > -1;
				string text2;
				if (flag)
				{
					text2 = new string('0', 8 - text.Length) + text;
				}
				else
				{
					text2 = new string('~', 8);
				}
				g.DrawString(text2, this.Font, brush, new PointF((float)((long)this._recLineInfo.X - this._scrollHpos), bytePointF.Y), this._stringFormat);
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003A124 File Offset: 0x00038324
		private void PaintHex(Graphics g, long startByte, long endByte)
		{
			Brush brush = new SolidBrush(this.GetDefaultForeColor());
			Brush brush2 = new SolidBrush(this._selectionForeColor);
			Brush brush3 = new SolidBrush(this._selectionBackColor);
			int num = -1;
			long num2 = Math.Min(this._byteProvider.Length - 1L, endByte + (long)this._iHexMaxHBytes);
			bool flag = this._keyInterpreter == null || this._keyInterpreter.GetType() == typeof(HexBox.KeyInterpreter);
			for (long num3 = startByte; num3 < num2 + 1L; num3 += 1L)
			{
				num++;
				Point gridBytePoint = this.GetGridBytePoint((long)num);
				byte b = this._byteProvider.ReadByte(num3);
				byte b2 = this.DifferDict[num3];
				bool flag2 = num3 >= this._bytePos && num3 <= this._bytePos + this._selectionLength - 1L && this._selectionLength != 0L;
				bool flag3 = num3 == this._bytePos + this._selectionLength - 1L;
				bool flag4 = flag2 && flag;
				if (flag4)
				{
					this.PaintHexStringSelected(g, b, brush2, brush3, gridBytePoint, flag3);
				}
				else
				{
					this.PaintHexString(g, b, brush, gridBytePoint, b2 != b);
				}
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003A26C File Offset: 0x0003846C
		private void PaintHexString(Graphics g, byte b, Brush brush, Point gridPoint, bool bChanged)
		{
			PointF bytePointF = this.GetBytePointF(gridPoint);
			string text = this.ConvertByteToHex(b);
			if (bChanged)
			{
				g.DrawString(text.Substring(0, 1), this.BoldFont, brush, bytePointF, this._stringFormat);
				bytePointF.X += this._charSize.Width;
				g.DrawString(text.Substring(1, 1), this.BoldFont, brush, bytePointF, this._stringFormat);
			}
			else
			{
				g.DrawString(text.Substring(0, 1), this.Font, brush, bytePointF, this._stringFormat);
				bytePointF.X += this._charSize.Width;
				g.DrawString(text.Substring(1, 1), this.Font, brush, bytePointF, this._stringFormat);
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0003A340 File Offset: 0x00038540
		private void PaintHexStringSelected(Graphics g, byte b, Brush brush, Brush brushBack, Point gridPoint, bool bLastSel)
		{
			string text = b.ToString(this._hexStringFormat, Thread.CurrentThread.CurrentCulture);
			bool flag = text.Length == 1;
			if (flag)
			{
				text = "0" + text;
			}
			PointF bytePointF = this.GetBytePointF(gridPoint);
			bool flag2 = gridPoint.X + 1 == this._iHexMaxHBytes;
			float num = ((flag2 || bLastSel) ? (this._charSize.Width * 2f) : (this._charSize.Width * 3f));
			g.FillRectangle(brushBack, bytePointF.X, bytePointF.Y, num, this._charSize.Height);
			g.DrawString(text.Substring(0, 1), this.Font, brush, bytePointF, this._stringFormat);
			bytePointF.X += this._charSize.Width;
			g.DrawString(text.Substring(1, 1), this.Font, brush, bytePointF, this._stringFormat);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003A440 File Offset: 0x00038640
		private void PaintHexAndStringView(Graphics g, long startByte, long endByte)
		{
			Brush brush = new SolidBrush(this.GetDefaultForeColor());
			Brush brush2 = new SolidBrush(this._selectionForeColor);
			Brush brush3 = new SolidBrush(Color.Black);
			Brush brush4 = brush2;
			Brush brush5 = new SolidBrush(this._selectionBackColor);
			Brush brush6 = new SolidBrush(Color.FromArgb(255, 0, 0));
			Brush brush7 = brush5;
			int num = -1;
			long num2 = Math.Min(this._byteProvider.Length - 1L, endByte + (long)this._iHexMaxHBytes);
			bool flag = this._keyInterpreter == null || this._keyInterpreter.GetType() == typeof(HexBox.KeyInterpreter);
			bool flag2 = this._keyInterpreter != null && this._keyInterpreter.GetType() == typeof(HexBox.StringKeyInterpreter);
			for (long num3 = startByte; num3 < num2 + 1L; num3 += 1L)
			{
				num++;
				Point gridBytePoint = this.GetGridBytePoint((long)num);
				PointF byteStringPointF = this.GetByteStringPointF(gridBytePoint);
				byte b = this._byteProvider.ReadByte(num3);
				bool flag3 = num3 >= this._bytePos && num3 <= this._bytePos + this._selectionLength - 1L && this._selectionLength != 0L;
				bool flag4 = num3 == this._bytePos + this._selectionLength - 1L;
				bool flag5 = false;
				bool flag6 = this.DifferDict.ContainsKey(num3);
				if (flag6)
				{
					byte b2 = this.DifferDict[num3];
					flag5 = b2 != b;
					bool flag7 = this.IsInSelectedAddresses(num3);
					if (flag7)
					{
						brush4 = brush3;
						brush7 = brush6;
						flag3 = true;
					}
					else
					{
						brush4 = brush2;
						brush = new SolidBrush(this.GetDefaultForeColor());
						brush7 = brush5;
					}
				}
				bool flag8 = flag3 && flag;
				if (flag8)
				{
					this.PaintHexStringSelected(g, b, brush4, brush7, gridBytePoint, flag4);
				}
				else
				{
					this.PaintHexString(g, b, brush, gridBytePoint, flag5);
				}
				string text = new string(this.ByteCharConverter.ToChar(b), 1);
				bool flag9 = flag3 && flag2;
				if (flag9)
				{
					g.FillRectangle(brush7, byteStringPointF.X, byteStringPointF.Y, this._charSize.Width, this._charSize.Height);
					g.DrawString(text, this.Font, brush4, byteStringPointF, this._stringFormat);
				}
				else
				{
					g.DrawString(text, this.Font, brush, byteStringPointF, this._stringFormat);
				}
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0003A6B0 File Offset: 0x000388B0
		private bool IsInSelectedAddresses(long bytePos)
		{
			foreach (long num in this.SelectAddresses.Keys)
			{
				bool flag = bytePos >= num && bytePos < num + (long)((ulong)this.SelectAddresses[num]);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0003A72C File Offset: 0x0003892C
		private void PaintCurrentBytesSign(Graphics g)
		{
			bool flag = this._keyInterpreter != null && this.Focused && this._bytePos != -1L && base.Enabled;
			if (flag)
			{
				bool flag2 = this._keyInterpreter.GetType() == typeof(HexBox.KeyInterpreter);
				if (flag2)
				{
					bool flag3 = this._selectionLength == 0L;
					if (flag3)
					{
						Point gridBytePoint = this.GetGridBytePoint(this._bytePos - this._startByte);
						PointF byteStringPointF = this.GetByteStringPointF(gridBytePoint);
						Size size = new Size((int)this._charSize.Width, (int)this._charSize.Height);
						Rectangle rectangle = new Rectangle((int)byteStringPointF.X, (int)byteStringPointF.Y, size.Width, size.Height);
						bool flag4 = rectangle.IntersectsWith(this._recStringView);
						if (flag4)
						{
							rectangle.Intersect(this._recStringView);
							this.PaintCurrentByteSign(g, rectangle);
						}
					}
					else
					{
						int num = (int)((float)this._recStringView.Width - this._charSize.Width);
						Point gridBytePoint2 = this.GetGridBytePoint(this._bytePos - this._startByte);
						PointF byteStringPointF2 = this.GetByteStringPointF(gridBytePoint2);
						Point gridBytePoint3 = this.GetGridBytePoint(this._bytePos - this._startByte + this._selectionLength - 1L);
						PointF byteStringPointF3 = this.GetByteStringPointF(gridBytePoint3);
						int num2 = gridBytePoint3.Y - gridBytePoint2.Y;
						bool flag5 = num2 == 0;
						if (flag5)
						{
							Rectangle rectangle2 = new Rectangle((int)byteStringPointF2.X, (int)byteStringPointF2.Y, (int)(byteStringPointF3.X - byteStringPointF2.X + this._charSize.Width), (int)this._charSize.Height);
							bool flag6 = rectangle2.IntersectsWith(this._recStringView);
							if (flag6)
							{
								rectangle2.Intersect(this._recStringView);
								this.PaintCurrentByteSign(g, rectangle2);
							}
						}
						else
						{
							Rectangle rectangle3 = new Rectangle((int)byteStringPointF2.X, (int)byteStringPointF2.Y, (int)((float)(this._recStringView.X + num) - byteStringPointF2.X + this._charSize.Width), (int)this._charSize.Height);
							bool flag7 = rectangle3.IntersectsWith(this._recStringView);
							if (flag7)
							{
								rectangle3.Intersect(this._recStringView);
								this.PaintCurrentByteSign(g, rectangle3);
							}
							bool flag8 = num2 > 1;
							if (flag8)
							{
								Rectangle rectangle4 = new Rectangle(this._recStringView.X, (int)(byteStringPointF2.Y + this._charSize.Height), this._recStringView.Width, (int)(this._charSize.Height * (float)(num2 - 1)));
								bool flag9 = rectangle4.IntersectsWith(this._recStringView);
								if (flag9)
								{
									rectangle4.Intersect(this._recStringView);
									this.PaintCurrentByteSign(g, rectangle4);
								}
							}
							Rectangle rectangle5 = new Rectangle(this._recStringView.X, (int)byteStringPointF3.Y, (int)(byteStringPointF3.X - (float)this._recStringView.X + this._charSize.Width), (int)this._charSize.Height);
							bool flag10 = rectangle5.IntersectsWith(this._recStringView);
							if (flag10)
							{
								rectangle5.Intersect(this._recStringView);
								this.PaintCurrentByteSign(g, rectangle5);
							}
						}
					}
				}
				else
				{
					bool flag11 = this._selectionLength == 0L;
					if (flag11)
					{
						Point gridBytePoint4 = this.GetGridBytePoint(this._bytePos - this._startByte);
						PointF bytePointF = this.GetBytePointF(gridBytePoint4);
						Size size2 = new Size((int)this._charSize.Width * 2, (int)this._charSize.Height);
						Rectangle rectangle6 = new Rectangle((int)bytePointF.X, (int)bytePointF.Y, size2.Width, size2.Height);
						this.PaintCurrentByteSign(g, rectangle6);
					}
					else
					{
						int num3 = (int)((float)this._recHex.Width - this._charSize.Width * 5f);
						Point gridBytePoint5 = this.GetGridBytePoint(this._bytePos - this._startByte);
						PointF bytePointF2 = this.GetBytePointF(gridBytePoint5);
						Point gridBytePoint6 = this.GetGridBytePoint(this._bytePos - this._startByte + this._selectionLength - 1L);
						PointF bytePointF3 = this.GetBytePointF(gridBytePoint6);
						int num4 = gridBytePoint6.Y - gridBytePoint5.Y;
						bool flag12 = num4 == 0;
						if (flag12)
						{
							Rectangle rectangle7 = new Rectangle((int)bytePointF2.X, (int)bytePointF2.Y, (int)(bytePointF3.X - bytePointF2.X + this._charSize.Width * 2f), (int)this._charSize.Height);
							bool flag13 = rectangle7.IntersectsWith(this._recHex);
							if (flag13)
							{
								rectangle7.Intersect(this._recHex);
								this.PaintCurrentByteSign(g, rectangle7);
							}
						}
						else
						{
							Rectangle rectangle8 = new Rectangle((int)bytePointF2.X, (int)bytePointF2.Y, (int)((float)(this._recHex.X + num3) - bytePointF2.X + this._charSize.Width * 2f), (int)this._charSize.Height);
							bool flag14 = rectangle8.IntersectsWith(this._recHex);
							if (flag14)
							{
								rectangle8.Intersect(this._recHex);
								this.PaintCurrentByteSign(g, rectangle8);
							}
							bool flag15 = num4 > 1;
							if (flag15)
							{
								Rectangle rectangle9 = new Rectangle(this._recHex.X, (int)(bytePointF2.Y + this._charSize.Height), (int)((float)num3 + this._charSize.Width * 2f), (int)(this._charSize.Height * (float)(num4 - 1)));
								bool flag16 = rectangle9.IntersectsWith(this._recHex);
								if (flag16)
								{
									rectangle9.Intersect(this._recHex);
									this.PaintCurrentByteSign(g, rectangle9);
								}
							}
							Rectangle rectangle10 = new Rectangle(this._recHex.X, (int)bytePointF3.Y, (int)(bytePointF3.X - (float)this._recHex.X + this._charSize.Width * 2f), (int)this._charSize.Height);
							bool flag17 = rectangle10.IntersectsWith(this._recHex);
							if (flag17)
							{
								rectangle10.Intersect(this._recHex);
								this.PaintCurrentByteSign(g, rectangle10);
							}
						}
					}
				}
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0003AD98 File Offset: 0x00038F98
		private void PaintCurrentByteSign(Graphics g, Rectangle rec)
		{
			bool flag = rec.Top < 0 || rec.Left < 0 || rec.Width <= 0 || rec.Height <= 0;
			if (!flag)
			{
				using (Bitmap bitmap = new Bitmap(rec.Width, rec.Height))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						SolidBrush solidBrush = new SolidBrush(this._shadowSelectionColor);
						graphics.FillRectangle(solidBrush, 0, 0, rec.Width, rec.Height);
						g.DrawImage(bitmap, rec.Left, rec.Top);
					}
				}
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0003AE6C File Offset: 0x0003906C
		private Color GetDefaultForeColor()
		{
			bool enabled = base.Enabled;
			Color color;
			if (enabled)
			{
				color = this.ForeColor;
			}
			else
			{
				color = Color.Gray;
			}
			return color;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0003AE98 File Offset: 0x00039098
		private void UpdateVisibilityBytes()
		{
			bool flag = this._byteProvider == null || this._byteProvider.Length == 0L;
			if (!flag)
			{
				this._startByte = (this._scrollVpos + 1L) * (long)this._iHexMaxHBytes - (long)this._iHexMaxHBytes;
				this._endByte = Math.Min(this._byteProvider.Length - 1L, this._startByte + (long)this._iHexMaxBytes);
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0003AF0C File Offset: 0x0003910C
		private void UpdateRectanglePositioning()
		{
			SizeF sizeF = base.CreateGraphics().MeasureString("A", this.Font, 100, this._stringFormat);
			this._charSize = new SizeF((float)Math.Ceiling((double)sizeF.Width), (float)Math.Ceiling((double)sizeF.Height));
			bool flag = this.m_hCaret != IntPtr.Zero;
			if (flag)
			{
				Image.FromHbitmap(this.m_hCaret).Dispose();
				this.m_hCaret = IntPtr.Zero;
			}
			Bitmap bitmap = new Bitmap((int)this._charSize.Width, (int)this._charSize.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(102, 102, 102)), new Rectangle(0, 0, (int)this._charSize.Width, (int)this._charSize.Height));
			}
			bool flag2 = Util.CurrentPlatform != Util.Platform.MacOS;
			if (flag2)
			{
				this.m_hCaret = bitmap.GetHbitmap();
			}
			this._recContent = base.ClientRectangle;
			this._recContent.X = this._recContent.X + this._recBorderLeft;
			this._recContent.Y = this._recContent.Y + this._recBorderTop;
			this._recContent.Width = this._recContent.Width - (this._recBorderRight + this._recBorderLeft);
			this._recContent.Height = this._recContent.Height - (this._recBorderBottom + this._recBorderTop);
			bool vScrollBarVisible = this._vScrollBarVisible;
			if (vScrollBarVisible)
			{
				this._recContent.Width = this._recContent.Width - this._vScrollBar.Width;
				this._vScrollBar.Left = this._recContent.X + this._recContent.Width;
				this._vScrollBar.Top = this._recContent.Y;
				this._vScrollBar.Height = this._recContent.Height;
			}
			bool hScrollBarVisible = this._hScrollBarVisible;
			if (hScrollBarVisible)
			{
				this._recContent.Height = this._recContent.Height - this._hScrollBar.Height;
				this._hScrollBar.Left = this._recContent.X;
				this._hScrollBar.Top = this._recContent.Y + this._recContent.Height;
				this._hScrollBar.Width = this._recContent.Width;
			}
			int num = 4;
			bool lineInfoVisible = this._lineInfoVisible;
			if (lineInfoVisible)
			{
				this._recLineInfo = new Rectangle(this._recContent.X + num, this._recContent.Y, (int)(this._charSize.Width * 10f), this._recContent.Height);
			}
			else
			{
				this._recLineInfo = Rectangle.Empty;
				this._recLineInfo.X = num;
			}
			this._recHex = new Rectangle(this._recLineInfo.X + this._recLineInfo.Width, this._recLineInfo.Y, this._recContent.Width - this._recLineInfo.Width, this._recContent.Height);
			bool useFixedBytesPerLine = this.UseFixedBytesPerLine;
			if (useFixedBytesPerLine)
			{
				this.SetHorizontalByteCount(this._bytesPerLine);
				this._recHex.Width = (int)Math.Floor((double)this._iHexMaxHBytes * (double)this._charSize.Width * 3.0 + (double)(2f * this._charSize.Width));
			}
			else
			{
				int num2 = (int)Math.Floor((double)this._recHex.Width / (double)this._charSize.Width);
				bool flag3 = num2 > 1;
				if (flag3)
				{
					this.SetHorizontalByteCount((int)Math.Floor((double)num2 / 3.0));
				}
				else
				{
					this.SetHorizontalByteCount(num2);
				}
			}
			bool stringViewVisible = this._stringViewVisible;
			if (stringViewVisible)
			{
				this._recStringView = new Rectangle(this._recHex.X + this._recHex.Width, this._recHex.Y, (int)(this._charSize.Width * (float)this._iHexMaxHBytes), this._recHex.Height);
			}
			else
			{
				this._recStringView = Rectangle.Empty;
			}
			int num3 = (int)Math.Floor((double)this._recHex.Height / (double)this._charSize.Height);
			this.SetVerticalByteCount(num3);
			this._iHexMaxBytes = this._iHexMaxHBytes * this._iHexMaxVBytes;
			this.UpdateVScrollSize();
			this.UpdateHScrollSize();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003B3C8 File Offset: 0x000395C8
		private PointF GetBytePointF(long byteIndex)
		{
			Point gridBytePoint = this.GetGridBytePoint(byteIndex);
			return this.GetBytePointF(gridBytePoint);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003B3EC File Offset: 0x000395EC
		private PointF GetBytePointF(Point gp)
		{
			float num = 3f * this._charSize.Width * (float)gp.X + (float)this._recHex.X;
			float num2 = (float)(gp.Y + 1) * this._charSize.Height - this._charSize.Height + (float)this._recHex.Y;
			return new PointF(num - (float)this._scrollHpos, num2);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003B468 File Offset: 0x00039668
		private PointF GetByteStringPointF(Point gp)
		{
			float num = this._charSize.Width * (float)gp.X + (float)this._recStringView.X;
			float num2 = (float)(gp.Y + 1) * this._charSize.Height - this._charSize.Height + (float)this._recStringView.Y;
			return new PointF(num - (float)this._scrollHpos, num2);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003B4DC File Offset: 0x000396DC
		private Point GetGridBytePoint(long byteIndex)
		{
			int num = (int)Math.Floor((double)byteIndex / (double)this._iHexMaxHBytes);
			int num2 = (int)(byteIndex + (long)this._iHexMaxHBytes - (long)(this._iHexMaxHBytes * (num + 1)));
			Point point = new Point(num2, num);
			return point;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0003B520 File Offset: 0x00039720
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0003B538 File Offset: 0x00039738
		[DefaultValue(typeof(Color), "White")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0003B544 File Offset: 0x00039744
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0003B55C File Offset: 0x0003975C
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				this._boldFont = new Font(base.Font, FontStyle.Underline);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0003B57C File Offset: 0x0003977C
		public Font BoldFont
		{
			get
			{
				return this._boldFont;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0003B594 File Offset: 0x00039794
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x0003B5AC File Offset: 0x000397AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0003B5B8 File Offset: 0x000397B8
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0003B5D0 File Offset: 0x000397D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0003B5DC File Offset: 0x000397DC
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0003B5F4 File Offset: 0x000397F4
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "WhiteSmoke")]
		public Color BackColorDisabled
		{
			get
			{
				return this._backColorDisabled;
			}
			set
			{
				this._backColorDisabled = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0003B600 File Offset: 0x00039800
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0003B618 File Offset: 0x00039818
		[DefaultValue(false)]
		[Category("Hex")]
		[Description("Gets or sets if the count of bytes in one line is fix.")]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				bool flag = this._readOnly == value;
				if (!flag)
				{
					this._readOnly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0003B650 File Offset: 0x00039850
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0003B668 File Offset: 0x00039868
		[DefaultValue(16)]
		[Category("Hex")]
		[Description("Gets or sets the maximum count of bytes in one line.")]
		public int BytesPerLine
		{
			get
			{
				return this._bytesPerLine;
			}
			set
			{
				bool flag = this._bytesPerLine == value;
				if (!flag)
				{
					this._bytesPerLine = value;
					this.OnBytesPerLineChanged(EventArgs.Empty);
					this.UpdateRectanglePositioning();
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0003B6A8 File Offset: 0x000398A8
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0003B6C0 File Offset: 0x000398C0
		[DefaultValue(false)]
		[Category("Hex")]
		[Description("Gets or sets if the count of bytes in one line is fix.")]
		public bool UseFixedBytesPerLine
		{
			get
			{
				return this._useFixedBytesPerLine;
			}
			set
			{
				bool flag = this._useFixedBytesPerLine == value;
				if (!flag)
				{
					this._useFixedBytesPerLine = value;
					this.OnUseFixedBytesPerLineChanged(EventArgs.Empty);
					this.UpdateRectanglePositioning();
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0003B700 File Offset: 0x00039900
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0003B718 File Offset: 0x00039918
		[DefaultValue(false)]
		[Category("Hex")]
		[Description("Gets or sets the visibility of a vertical scroll bar.")]
		public bool VScrollBarVisible
		{
			get
			{
				return this._vScrollBarVisible;
			}
			set
			{
				bool flag = this._vScrollBarVisible == value;
				if (!flag)
				{
					this._vScrollBarVisible = value;
					bool vScrollBarVisible = this._vScrollBarVisible;
					if (vScrollBarVisible)
					{
						base.Controls.Add(this._vScrollBar);
					}
					else
					{
						base.Controls.Remove(this._vScrollBar);
					}
					this.UpdateRectanglePositioning();
					this.UpdateVScrollSize();
					this.OnVScrollBarVisibleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0003B788 File Offset: 0x00039988
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0003B7A0 File Offset: 0x000399A0
		public bool HScrollBarVisible
		{
			get
			{
				return this._hScrollBarVisible;
			}
			set
			{
				bool flag = this._hScrollBarVisible == value;
				if (!flag)
				{
					this._hScrollBarVisible = value;
					bool hScrollBarVisible = this._hScrollBarVisible;
					if (hScrollBarVisible)
					{
						base.Controls.Add(this._hScrollBar);
					}
					else
					{
						base.Controls.Remove(this._hScrollBar);
					}
					this.OnHScrollBarVisibleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0003B800 File Offset: 0x00039A00
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0003B818 File Offset: 0x00039A18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IByteProvider ByteProvider
		{
			get
			{
				return this._byteProvider;
			}
			set
			{
				bool flag = this._byteProvider == value;
				if (!flag)
				{
					bool flag2 = value == null;
					if (flag2)
					{
						this.ActivateEmptyKeyInterpreter();
					}
					else
					{
						this.ActivateKeyInterpreter();
					}
					bool flag3 = this._byteProvider != null;
					if (flag3)
					{
						this._byteProvider.LengthChanged -= this._byteProvider_LengthChanged;
					}
					this._byteProvider = value;
					bool flag4 = this._byteProvider != null;
					if (flag4)
					{
						this._byteProvider.LengthChanged += this._byteProvider_LengthChanged;
					}
					this.OnByteProviderChanged(EventArgs.Empty);
					bool flag5 = value == null;
					if (flag5)
					{
						this._bytePos = -1L;
						this._byteCharacterPos = 0;
						this._selectionLength = 0L;
						this.DestroyCaret();
					}
					else
					{
						this.SetPosition(0L, 0);
						this.SetSelectionLength(0L);
						bool flag6 = this._caretVisible && this.Focused;
						if (flag6)
						{
							this.UpdateCaret();
						}
						else
						{
							this.CreateCaret();
						}
					}
					this.CheckCurrentLineChanged();
					this.CheckCurrentPositionInLineChanged();
					this._scrollVpos = 0L;
					this._scrollHpos = 0L;
					this.UpdateVisibilityBytes();
					this.UpdateRectanglePositioning();
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0003B948 File Offset: 0x00039B48
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0003B960 File Offset: 0x00039B60
		[DefaultValue(false)]
		[Category("Hex")]
		[Description("Gets or sets the visibility of a line info.")]
		public bool LineInfoVisible
		{
			get
			{
				return this._lineInfoVisible;
			}
			set
			{
				bool flag = this._lineInfoVisible == value;
				if (!flag)
				{
					this._lineInfoVisible = value;
					this.OnLineInfoVisibleChanged(EventArgs.Empty);
					this.UpdateRectanglePositioning();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		[DefaultValue(0L)]
		[Category("Hex")]
		[Description("Gets or sets the offset of the line info.")]
		public long LineInfoOffset
		{
			get
			{
				return this._lineInfoOffset;
			}
			set
			{
				bool flag = this._lineInfoOffset == value;
				if (!flag)
				{
					this._lineInfoOffset = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0003B9E4 File Offset: 0x00039BE4
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0003B9FC File Offset: 0x00039BFC
		[DefaultValue(typeof(BorderStyle), "Fixed3D")]
		[Category("Hex")]
		[Description("Gets or sets the hex box\u00b4s border style.")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				bool flag = this._borderStyle == value;
				if (!flag)
				{
					this._borderStyle = value;
					switch (this._borderStyle)
					{
					case BorderStyle.None:
						this._recBorderLeft = (this._recBorderTop = (this._recBorderRight = (this._recBorderBottom = 0)));
						break;
					case BorderStyle.FixedSingle:
						this._recBorderLeft = (this._recBorderTop = (this._recBorderRight = (this._recBorderBottom = 1)));
						break;
					case BorderStyle.Fixed3D:
						this._recBorderLeft = (this._recBorderRight = SystemInformation.Border3DSize.Width);
						this._recBorderTop = (this._recBorderBottom = SystemInformation.Border3DSize.Height);
						break;
					}
					this.UpdateRectanglePositioning();
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0003BAD8 File Offset: 0x00039CD8
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0003BAF0 File Offset: 0x00039CF0
		[DefaultValue(false)]
		[Category("Hex")]
		[Description("Gets or sets the visibility of the string view.")]
		public bool StringViewVisible
		{
			get
			{
				return this._stringViewVisible;
			}
			set
			{
				bool flag = this._stringViewVisible == value;
				if (!flag)
				{
					this._stringViewVisible = value;
					this.OnStringViewVisibleChanged(EventArgs.Empty);
					this.UpdateRectanglePositioning();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0003BB30 File Offset: 0x00039D30
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0003BB5C File Offset: 0x00039D5C
		[DefaultValue(typeof(HexCasing), "Upper")]
		[Category("Hex")]
		[Description("Gets or sets whether the HexBox control displays the hex characters in upper or lower case.")]
		public HexCasing HexCasing
		{
			get
			{
				bool flag = this._hexStringFormat == "X";
				HexCasing hexCasing;
				if (flag)
				{
					hexCasing = HexCasing.Upper;
				}
				else
				{
					hexCasing = HexCasing.Lower;
				}
				return hexCasing;
			}
			set
			{
				bool flag = value == HexCasing.Upper;
				string text;
				if (flag)
				{
					text = "X";
				}
				else
				{
					text = "x";
				}
				bool flag2 = this._hexStringFormat == text;
				if (!flag2)
				{
					this._hexStringFormat = text;
					this.OnHexCasingChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0003BBAC File Offset: 0x00039DAC
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0003BBC4 File Offset: 0x00039DC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long SelectionStart
		{
			get
			{
				return this._bytePos;
			}
			set
			{
				this.SetPosition(value, 0);
				this.ScrollByteIntoView();
				base.Invalidate();
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0003BBE0 File Offset: 0x00039DE0
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0003BBF8 File Offset: 0x00039DF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long SelectionLength
		{
			get
			{
				return this._selectionLength;
			}
			set
			{
				this.SetSelectionLength(value);
				this.ScrollByteIntoView();
				base.Invalidate();
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0003BC14 File Offset: 0x00039E14
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0003BC2C File Offset: 0x00039E2C
		[DefaultValue(typeof(Color), "Empty")]
		[Category("Hex")]
		[Description("Gets or sets the line info color. When this property is null, then ForeColor property is used.")]
		public Color LineInfoForeColor
		{
			get
			{
				return this._lineInfoForeColor;
			}
			set
			{
				this._lineInfoForeColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0003BC40 File Offset: 0x00039E40
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0003BC58 File Offset: 0x00039E58
		[DefaultValue(typeof(Color), "Blue")]
		[Category("Hex")]
		[Description("Gets or sets the background color for the selected bytes.")]
		public Color SelectionBackColor
		{
			get
			{
				return this._selectionBackColor;
			}
			set
			{
				this._selectionBackColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0003BC6C File Offset: 0x00039E6C
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0003BC84 File Offset: 0x00039E84
		[DefaultValue(typeof(Color), "White")]
		[Category("Hex")]
		[Description("Gets or sets the foreground color for the selected bytes.")]
		public Color SelectionForeColor
		{
			get
			{
				return this._selectionForeColor;
			}
			set
			{
				this._selectionForeColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0003BC98 File Offset: 0x00039E98
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		[DefaultValue(true)]
		[Category("Hex")]
		[Description("Gets or sets the visibility of a shadow selection.")]
		public bool ShadowSelectionVisible
		{
			get
			{
				return this._shadowSelectionVisible;
			}
			set
			{
				bool flag = this._shadowSelectionVisible == value;
				if (!flag)
				{
					this._shadowSelectionVisible = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0003BCDC File Offset: 0x00039EDC
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		[Category("Hex")]
		[Description("Gets or sets the color of the shadow selection.")]
		public Color ShadowSelectionColor
		{
			get
			{
				return this._shadowSelectionColor;
			}
			set
			{
				this._shadowSelectionColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0003BD08 File Offset: 0x00039F08
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int HorizontalByteCount
		{
			get
			{
				return this._iHexMaxHBytes;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0003BD20 File Offset: 0x00039F20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VerticalByteCount
		{
			get
			{
				return this._iHexMaxVBytes;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0003BD38 File Offset: 0x00039F38
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long CurrentLine
		{
			get
			{
				return this._currentLine;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0003BD50 File Offset: 0x00039F50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long CurrentPositionInLine
		{
			get
			{
				return (long)this._currentPositionInLine;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0003BD6C File Offset: 0x00039F6C
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0003BD84 File Offset: 0x00039F84
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool InsertActive
		{
			get
			{
				return this._insertActive;
			}
			set
			{
				bool flag = this._insertActive == value;
				if (!flag)
				{
					this._insertActive = value;
					this.DestroyCaret();
					this.CreateCaret();
					this.OnInsertActiveChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BuiltInContextMenu BuiltInContextMenu
		{
			get
			{
				return this._builtInContextMenu;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0003BDDC File Offset: 0x00039FDC
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0003BE0C File Offset: 0x0003A00C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IByteCharConverter ByteCharConverter
		{
			get
			{
				bool flag = this._byteCharConverter == null;
				if (flag)
				{
					this._byteCharConverter = new DefaultByteCharConverter();
				}
				return this._byteCharConverter;
			}
			set
			{
				bool flag = value != null && value != this._byteCharConverter;
				if (flag)
				{
					this._byteCharConverter = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0003BE40 File Offset: 0x0003A040
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0003BE48 File Offset: 0x0003A048
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<long, byte> SelectAddresses { get; set; }

		// Token: 0x06000A1E RID: 2590 RVA: 0x0003BE54 File Offset: 0x0003A054
		private string ConvertBytesToHex(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in data)
			{
				string text = this.ConvertByteToHex(b);
				stringBuilder.Append(text);
				stringBuilder.Append(" ");
			}
			bool flag = stringBuilder.Length > 0;
			if (flag)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0003BECC File Offset: 0x0003A0CC
		private string ConvertByteToHex(byte b)
		{
			string text = b.ToString(this._hexStringFormat, Thread.CurrentThread.CurrentCulture);
			bool flag = text.Length == 1;
			if (flag)
			{
				text = "0" + text;
			}
			return text;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0003BF10 File Offset: 0x0003A110
		private byte[] ConvertHexToBytes(string hex)
		{
			bool flag = string.IsNullOrEmpty(hex);
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				hex = hex.Trim();
				string[] array2 = hex.Split(new char[] { ' ' });
				byte[] array3 = new byte[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					byte b;
					bool flag2 = this.ConvertHexToByte(text, out b);
					bool flag3 = !flag2;
					if (flag3)
					{
						return null;
					}
					array3[i] = b;
				}
				array = array3;
			}
			return array;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0003BF98 File Offset: 0x0003A198
		private bool ConvertHexToByte(string hex, out byte b)
		{
			return byte.TryParse(hex, NumberStyles.HexNumber, Thread.CurrentThread.CurrentCulture, out b);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0003BFC2 File Offset: 0x0003A1C2
		private void SetPosition(long bytePos)
		{
			this.SetPosition(bytePos, this._byteCharacterPos);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		private void SetPosition(long bytePos, int byteCharacterPos)
		{
			bool flag = this._byteCharacterPos != byteCharacterPos;
			if (flag)
			{
				this._byteCharacterPos = byteCharacterPos;
			}
			bool flag2 = bytePos != this._bytePos;
			if (flag2)
			{
				this._bytePos = bytePos;
				this.CheckCurrentLineChanged();
				this.CheckCurrentPositionInLineChanged();
				this.OnSelectionStartChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0003C030 File Offset: 0x0003A230
		private void SetSelectionLength(long selectionLength)
		{
			bool flag = selectionLength != this._selectionLength;
			if (flag)
			{
				this._selectionLength = selectionLength;
				this.OnSelectionLengthChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0003C064 File Offset: 0x0003A264
		private void SetHorizontalByteCount(int value)
		{
			bool flag = this._iHexMaxHBytes == value;
			if (!flag)
			{
				this._iHexMaxHBytes = value;
				this.OnHorizontalByteCountChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0003C094 File Offset: 0x0003A294
		private void SetVerticalByteCount(int value)
		{
			bool flag = this._iHexMaxVBytes == value;
			if (!flag)
			{
				this._iHexMaxVBytes = value;
				this.OnVerticalByteCountChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		private void CheckCurrentLineChanged()
		{
			long num = (long)Math.Floor((double)this._bytePos / (double)this._iHexMaxHBytes) + 1L;
			bool flag = this._byteProvider == null && this._currentLine != 0L;
			if (flag)
			{
				this._currentLine = 0L;
				this.OnCurrentLineChanged(EventArgs.Empty);
			}
			else
			{
				bool flag2 = num != this._currentLine;
				if (flag2)
				{
					this._currentLine = num;
					this.OnCurrentLineChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0003C144 File Offset: 0x0003A344
		private void CheckCurrentPositionInLineChanged()
		{
			int num = this.GetGridBytePoint(this._bytePos).X + 1;
			bool flag = this._byteProvider == null && this._currentPositionInLine != 0;
			if (flag)
			{
				this._currentPositionInLine = 0;
				this.OnCurrentPositionInLineChanged(EventArgs.Empty);
			}
			else
			{
				bool flag2 = num != this._currentPositionInLine;
				if (flag2)
				{
					this._currentPositionInLine = num;
					this.OnCurrentPositionInLineChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0003C1C0 File Offset: 0x0003A3C0
		protected virtual void OnInsertActiveChanged(EventArgs e)
		{
			bool flag = this.InsertActiveChanged != null;
			if (flag)
			{
				this.InsertActiveChanged(this, e);
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0003C1EC File Offset: 0x0003A3EC
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			bool flag = this.ReadOnlyChanged != null;
			if (flag)
			{
				this.ReadOnlyChanged(this, e);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0003C218 File Offset: 0x0003A418
		protected virtual void OnByteProviderChanged(EventArgs e)
		{
			bool flag = this.ByteProviderChanged != null;
			if (flag)
			{
				this.ByteProviderChanged(this, e);
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0003C244 File Offset: 0x0003A444
		protected virtual void OnScroll(EventArgs e)
		{
			bool flag = this.VScroll != null;
			if (flag)
			{
				this.VScroll(this, e);
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0003C270 File Offset: 0x0003A470
		protected virtual void OnHScroll(EventArgs e)
		{
			bool flag = this.HScroll != null;
			if (flag)
			{
				this.HScroll(this, e);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0003C29C File Offset: 0x0003A49C
		protected virtual void OnSelectionStartChanged(EventArgs e)
		{
			bool flag = this.SelectionStartChanged != null;
			if (flag)
			{
				this.SelectionStartChanged(this, e);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		protected virtual void OnSelectionLengthChanged(EventArgs e)
		{
			bool flag = this.SelectionLengthChanged != null;
			if (flag)
			{
				this.SelectionLengthChanged(this, e);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0003C2F4 File Offset: 0x0003A4F4
		protected virtual void OnLineInfoVisibleChanged(EventArgs e)
		{
			bool flag = this.LineInfoVisibleChanged != null;
			if (flag)
			{
				this.LineInfoVisibleChanged(this, e);
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0003C320 File Offset: 0x0003A520
		protected virtual void OnStringViewVisibleChanged(EventArgs e)
		{
			bool flag = this.StringViewVisibleChanged != null;
			if (flag)
			{
				this.StringViewVisibleChanged(this, e);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0003C34C File Offset: 0x0003A54C
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			bool flag = this.BorderStyleChanged != null;
			if (flag)
			{
				this.BorderStyleChanged(this, e);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0003C378 File Offset: 0x0003A578
		protected virtual void OnUseFixedBytesPerLineChanged(EventArgs e)
		{
			bool flag = this.UseFixedBytesPerLineChanged != null;
			if (flag)
			{
				this.UseFixedBytesPerLineChanged(this, e);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		protected virtual void OnBytesPerLineChanged(EventArgs e)
		{
			bool flag = this.BytesPerLineChanged != null;
			if (flag)
			{
				this.BytesPerLineChanged(this, e);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003C3D0 File Offset: 0x0003A5D0
		protected virtual void OnVScrollBarVisibleChanged(EventArgs e)
		{
			bool flag = this.VScrollBarVisibleChanged != null;
			if (flag)
			{
				this.VScrollBarVisibleChanged(this, e);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		protected virtual void OnHScrollBarVisibleChanged(EventArgs e)
		{
			bool flag = this.HScrollBarVisibleChanged != null;
			if (flag)
			{
				this.HScrollBarVisibleChanged(this, e);
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0003C428 File Offset: 0x0003A628
		protected virtual void OnHexCasingChanged(EventArgs e)
		{
			bool flag = this.HexCasingChanged != null;
			if (flag)
			{
				this.HexCasingChanged(this, e);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0003C454 File Offset: 0x0003A654
		protected virtual void OnHorizontalByteCountChanged(EventArgs e)
		{
			bool flag = this.HorizontalByteCountChanged != null;
			if (flag)
			{
				this.HorizontalByteCountChanged(this, e);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003C480 File Offset: 0x0003A680
		protected virtual void OnVerticalByteCountChanged(EventArgs e)
		{
			bool flag = this.VerticalByteCountChanged != null;
			if (flag)
			{
				this.VerticalByteCountChanged(this, e);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0003C4AC File Offset: 0x0003A6AC
		protected virtual void OnCurrentLineChanged(EventArgs e)
		{
			bool flag = this.CurrentLineChanged != null;
			if (flag)
			{
				this.CurrentLineChanged(this, e);
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0003C4D8 File Offset: 0x0003A6D8
		protected virtual void OnCurrentPositionInLineChanged(EventArgs e)
		{
			bool flag = this.CurrentPositionInLineChanged != null;
			if (flag)
			{
				this.CurrentPositionInLineChanged(this, e);
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003C504 File Offset: 0x0003A704
		protected virtual void OnCopied(EventArgs e)
		{
			bool flag = this.Copied != null;
			if (flag)
			{
				this.Copied(this, e);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0003C530 File Offset: 0x0003A730
		protected virtual void OnCopiedHex(EventArgs e)
		{
			bool flag = this.CopiedHex != null;
			if (flag)
			{
				this.CopiedHex(this, e);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0003C55C File Offset: 0x0003A75C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool flag = !this.Focused;
			if (flag)
			{
				base.Focus();
			}
			bool flag2 = e.Button == MouseButtons.Left;
			if (flag2)
			{
				this.SetCaretPosition(new Point(e.X + (int)this._scrollHpos, e.Y));
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0003C5B8 File Offset: 0x0003A7B8
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int num = -(e.Delta * SystemInformation.MouseWheelScrollLines / 120);
			this.PerformScrollLines(num);
			this.OnScroll(EventArgs.Empty);
			base.OnMouseWheel(e);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0003C5F4 File Offset: 0x0003A7F4
		protected override void OnMouseMove(MouseEventArgs e)
		{
			bool flag = this._byteProvider == null;
			if (flag)
			{
				base.OnMouseMove(e);
			}
			else
			{
				BytePositionInfo hexBytePositionInfo = this.GetHexBytePositionInfo(base.PointToClient(new Point(Control.MousePosition.X - (int)this._scrollHpos, Control.MousePosition.Y)));
				bool flag2 = this.DifferDict.ContainsKey(hexBytePositionInfo.Index) && hexBytePositionInfo.Index < this._byteProvider.Length && this._byteProvider.ReadByte(hexBytePositionInfo.Index) != this.DifferDict[hexBytePositionInfo.Index];
				if (flag2)
				{
					bool flag3 = this.tipIndex != hexBytePositionInfo.Index;
					if (flag3)
					{
						this.tipIndex = hexBytePositionInfo.Index;
						this.tip.Show("Original Value: " + this.DifferDict[hexBytePositionInfo.Index].ToString("X2"), this, base.PointToClient(Control.MousePosition), 1000);
					}
				}
				else
				{
					this.tipIndex = -1L;
				}
				base.OnMouseMove(e);
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0003C72D File Offset: 0x0003A92D
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.UpdateRectanglePositioning();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0003C73F File Offset: 0x0003A93F
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.CreateCaret();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0003C751 File Offset: 0x0003A951
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.DestroyCaret();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0003C763 File Offset: 0x0003A963
		private void _byteProvider_LengthChanged(object sender, EventArgs e)
		{
			this.UpdateVScrollSize();
		}

		// Token: 0x04000562 RID: 1378
		private ToolTip tip;

		// Token: 0x04000563 RID: 1379
		private Rectangle _recContent;

		// Token: 0x04000564 RID: 1380
		private Rectangle _recLineInfo;

		// Token: 0x04000565 RID: 1381
		private Rectangle _recHex;

		// Token: 0x04000566 RID: 1382
		private Rectangle _recStringView;

		// Token: 0x04000567 RID: 1383
		private StringFormat _stringFormat;

		// Token: 0x04000568 RID: 1384
		private SizeF _charSize;

		// Token: 0x04000569 RID: 1385
		private int _iHexMaxHBytes;

		// Token: 0x0400056A RID: 1386
		private int _iHexMaxVBytes;

		// Token: 0x0400056B RID: 1387
		private int _iHexMaxBytes;

		// Token: 0x0400056C RID: 1388
		private long _scrollVmin;

		// Token: 0x0400056D RID: 1389
		private long _scrollHmin;

		// Token: 0x0400056E RID: 1390
		private long _scrollVmax;

		// Token: 0x0400056F RID: 1391
		private long _scrollHmax;

		// Token: 0x04000570 RID: 1392
		private long _scrollVpos;

		// Token: 0x04000571 RID: 1393
		private long _scrollHpos;

		// Token: 0x04000572 RID: 1394
		private VScrollBar _vScrollBar;

		// Token: 0x04000573 RID: 1395
		private HScrollBar _hScrollBar;

		// Token: 0x04000574 RID: 1396
		private global::System.Windows.Forms.Timer _thumbTrackTimer = new global::System.Windows.Forms.Timer();

		// Token: 0x04000575 RID: 1397
		private long _thumbTrackPosition;

		// Token: 0x04000576 RID: 1398
		private const int THUMPTRACKDELAY = 50;

		// Token: 0x04000577 RID: 1399
		private int _lastThumbtrack;

		// Token: 0x04000578 RID: 1400
		private int _recBorderLeft = SystemInformation.Border3DSize.Width;

		// Token: 0x04000579 RID: 1401
		private int _recBorderRight = SystemInformation.Border3DSize.Width;

		// Token: 0x0400057A RID: 1402
		private int _recBorderTop = SystemInformation.Border3DSize.Height;

		// Token: 0x0400057B RID: 1403
		private int _recBorderBottom = SystemInformation.Border3DSize.Height;

		// Token: 0x0400057C RID: 1404
		private long _startByte;

		// Token: 0x0400057D RID: 1405
		private long _endByte;

		// Token: 0x0400057E RID: 1406
		private long _bytePos = -1L;

		// Token: 0x0400057F RID: 1407
		private int _byteCharacterPos;

		// Token: 0x04000580 RID: 1408
		private string _hexStringFormat = "X";

		// Token: 0x04000581 RID: 1409
		private HexBox.IKeyInterpreter _keyInterpreter;

		// Token: 0x04000582 RID: 1410
		private HexBox.EmptyKeyInterpreter _eki;

		// Token: 0x04000583 RID: 1411
		private HexBox.KeyInterpreter _ki;

		// Token: 0x04000584 RID: 1412
		private HexBox.StringKeyInterpreter _ski;

		// Token: 0x04000585 RID: 1413
		private bool _caretVisible;

		// Token: 0x04000586 RID: 1414
		private bool _abortFind;

		// Token: 0x04000587 RID: 1415
		private long _findingPos;

		// Token: 0x04000588 RID: 1416
		private bool _insertActive;

		// Token: 0x0400059E RID: 1438
		private IntPtr m_hCaret = IntPtr.Zero;

		// Token: 0x0400059F RID: 1439
		private Font _boldFont;

		// Token: 0x040005A0 RID: 1440
		private Color _backColorDisabled = Color.FromName("WhiteSmoke");

		// Token: 0x040005A1 RID: 1441
		private bool _readOnly;

		// Token: 0x040005A2 RID: 1442
		private int _bytesPerLine = 16;

		// Token: 0x040005A3 RID: 1443
		private bool _useFixedBytesPerLine;

		// Token: 0x040005A4 RID: 1444
		private bool _vScrollBarVisible;

		// Token: 0x040005A5 RID: 1445
		private bool _hScrollBarVisible;

		// Token: 0x040005A6 RID: 1446
		private IByteProvider _byteProvider;

		// Token: 0x040005A7 RID: 1447
		private bool _lineInfoVisible;

		// Token: 0x040005A8 RID: 1448
		private long _lineInfoOffset;

		// Token: 0x040005A9 RID: 1449
		private BorderStyle _borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040005AA RID: 1450
		private bool _stringViewVisible;

		// Token: 0x040005AB RID: 1451
		private long _selectionLength;

		// Token: 0x040005AC RID: 1452
		private Color _lineInfoForeColor = Color.Empty;

		// Token: 0x040005AD RID: 1453
		private Color _selectionBackColor = Color.Blue;

		// Token: 0x040005AE RID: 1454
		private Color _selectionForeColor = Color.White;

		// Token: 0x040005AF RID: 1455
		private bool _shadowSelectionVisible = true;

		// Token: 0x040005B0 RID: 1456
		private Color _shadowSelectionColor = Color.FromArgb(100, 60, 188, 255);

		// Token: 0x040005B1 RID: 1457
		private long _currentLine;

		// Token: 0x040005B2 RID: 1458
		private int _currentPositionInLine;

		// Token: 0x040005B3 RID: 1459
		private BuiltInContextMenu _builtInContextMenu;

		// Token: 0x040005B4 RID: 1460
		private IByteCharConverter _byteCharConverter;

		// Token: 0x040005B5 RID: 1461
		internal Dictionary<long, byte> DifferDict = new Dictionary<long, byte>();

		// Token: 0x040005B7 RID: 1463
		private long tipIndex = -1L;

		// Token: 0x0200021A RID: 538
		private interface IKeyInterpreter
		{
			// Token: 0x06001C66 RID: 7270
			void Activate();

			// Token: 0x06001C67 RID: 7271
			void Deactivate();

			// Token: 0x06001C68 RID: 7272
			bool PreProcessWmKeyUp(ref Message m);

			// Token: 0x06001C69 RID: 7273
			bool PreProcessWmChar(ref Message m);

			// Token: 0x06001C6A RID: 7274
			bool PreProcessWmKeyDown(ref Message m);

			// Token: 0x06001C6B RID: 7275
			PointF GetCaretPointF(long byteIndex);
		}

		// Token: 0x0200021B RID: 539
		private class EmptyKeyInterpreter : HexBox.IKeyInterpreter
		{
			// Token: 0x06001C6C RID: 7276 RVA: 0x000B2FDC File Offset: 0x000B11DC
			public EmptyKeyInterpreter(HexBox hexBox)
			{
				this._hexBox = hexBox;
			}

			// Token: 0x06001C6D RID: 7277 RVA: 0x000021C5 File Offset: 0x000003C5
			public void Activate()
			{
			}

			// Token: 0x06001C6E RID: 7278 RVA: 0x000021C5 File Offset: 0x000003C5
			public void Deactivate()
			{
			}

			// Token: 0x06001C6F RID: 7279 RVA: 0x000B2FF0 File Offset: 0x000B11F0
			public bool PreProcessWmKeyUp(ref Message m)
			{
				return this._hexBox.BasePreProcessMessage(ref m);
			}

			// Token: 0x06001C70 RID: 7280 RVA: 0x000B3010 File Offset: 0x000B1210
			public bool PreProcessWmChar(ref Message m)
			{
				return this._hexBox.BasePreProcessMessage(ref m);
			}

			// Token: 0x06001C71 RID: 7281 RVA: 0x000B3030 File Offset: 0x000B1230
			public bool PreProcessWmKeyDown(ref Message m)
			{
				return this._hexBox.BasePreProcessMessage(ref m);
			}

			// Token: 0x06001C72 RID: 7282 RVA: 0x000B3050 File Offset: 0x000B1250
			public PointF GetCaretPointF(long byteIndex)
			{
				return default(PointF);
			}

			// Token: 0x04000DE5 RID: 3557
			private HexBox _hexBox;
		}

		// Token: 0x0200021C RID: 540
		private class KeyInterpreter : HexBox.IKeyInterpreter
		{
			// Token: 0x06001C73 RID: 7283 RVA: 0x000B306B File Offset: 0x000B126B
			public KeyInterpreter(HexBox hexBox)
			{
				this._hexBox = hexBox;
			}

			// Token: 0x06001C74 RID: 7284 RVA: 0x000B307C File Offset: 0x000B127C
			public virtual void Activate()
			{
				this._hexBox.MouseDown += this.BeginMouseSelection;
				this._hexBox.MouseMove += this.UpdateMouseSelection;
				this._hexBox.MouseUp += this.EndMouseSelection;
			}

			// Token: 0x06001C75 RID: 7285 RVA: 0x000B30D4 File Offset: 0x000B12D4
			public virtual void Deactivate()
			{
				this._hexBox.MouseDown -= this.BeginMouseSelection;
				this._hexBox.MouseMove -= this.UpdateMouseSelection;
				this._hexBox.MouseUp -= this.EndMouseSelection;
			}

			// Token: 0x06001C76 RID: 7286 RVA: 0x000B312C File Offset: 0x000B132C
			private void BeginMouseSelection(object sender, MouseEventArgs e)
			{
				bool flag = e.Button != MouseButtons.Left;
				if (!flag)
				{
					this._mouseDown = true;
					bool flag2 = !this._shiftDown;
					if (flag2)
					{
						this._bpiStart = new BytePositionInfo(this._hexBox._bytePos, this._hexBox._byteCharacterPos);
						this._hexBox.ReleaseSelection();
					}
					else
					{
						this.UpdateMouseSelection(this, e);
					}
				}
			}

			// Token: 0x06001C77 RID: 7287 RVA: 0x000B31A0 File Offset: 0x000B13A0
			private void UpdateMouseSelection(object sender, MouseEventArgs e)
			{
				bool flag = !this._mouseDown;
				if (!flag)
				{
					this._bpi = this.GetBytePositionInfo(new Point(e.X, e.Y));
					long index = this._bpi.Index;
					bool flag2 = index < this._bpiStart.Index;
					long num;
					long num2;
					if (flag2)
					{
						num = index;
						num2 = this._bpiStart.Index - index;
					}
					else
					{
						bool flag3 = index > this._bpiStart.Index;
						if (flag3)
						{
							num = this._bpiStart.Index;
							num2 = index - num;
						}
						else
						{
							num = this._hexBox._bytePos;
							num2 = 0L;
						}
					}
					bool flag4 = num != this._hexBox._bytePos || num2 != this._hexBox._selectionLength;
					if (flag4)
					{
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView(this._bpi.Index);
					}
				}
			}

			// Token: 0x06001C78 RID: 7288 RVA: 0x000B3299 File Offset: 0x000B1499
			private void EndMouseSelection(object sender, MouseEventArgs e)
			{
				this._mouseDown = false;
			}

			// Token: 0x06001C79 RID: 7289 RVA: 0x000B32A4 File Offset: 0x000B14A4
			public virtual bool PreProcessWmKeyDown(ref Message m)
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				Keys keys2 = keys | Control.ModifierKeys;
				bool flag = this.MessageHandlers.ContainsKey(keys2);
				bool flag2 = flag && this.RaiseKeyDown(keys2);
				bool flag3;
				if (flag2)
				{
					flag3 = true;
				}
				else
				{
					HexBox.KeyInterpreter.MessageDelegate messageDelegate = (flag ? this.MessageHandlers[keys2] : new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Default));
					flag3 = messageDelegate(ref m);
				}
				return flag3;
			}

			// Token: 0x06001C7A RID: 7290 RVA: 0x000B331C File Offset: 0x000B151C
			protected bool PreProcessWmKeyDown_Default(ref Message m)
			{
				this._hexBox.ScrollByteIntoView();
				return this._hexBox.BasePreProcessMessage(ref m);
			}

			// Token: 0x06001C7B RID: 7291 RVA: 0x000B3348 File Offset: 0x000B1548
			protected bool RaiseKeyDown(Keys keyData)
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
				this._hexBox.OnKeyDown(keyEventArgs);
				return keyEventArgs.Handled;
			}

			// Token: 0x06001C7C RID: 7292 RVA: 0x000B3374 File Offset: 0x000B1574
			protected virtual bool PreProcessWmKeyDown_Left(ref Message m)
			{
				return this.PerformPosMoveLeft();
			}

			// Token: 0x06001C7D RID: 7293 RVA: 0x000B338C File Offset: 0x000B158C
			protected virtual bool PreProcessWmKeyDown_Up(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int byteCharacterPos = this._hexBox._byteCharacterPos;
				bool flag = num != 0L || byteCharacterPos != 0;
				if (flag)
				{
					num = Math.Max(-1L, num - (long)this._hexBox._iHexMaxHBytes);
					bool flag2 = num == -1L;
					if (flag2)
					{
						return true;
					}
					this._hexBox.SetPosition(num);
					bool flag3 = num < this._hexBox._startByte;
					if (flag3)
					{
						this._hexBox.PerformScrollLineUp();
					}
					this._hexBox.UpdateCaret();
					this._hexBox.Invalidate();
				}
				this._hexBox.ScrollByteIntoView();
				this._hexBox.ReleaseSelection();
				return true;
			}

			// Token: 0x06001C7E RID: 7294 RVA: 0x000B344C File Offset: 0x000B164C
			protected virtual bool PreProcessWmKeyDown_Right(ref Message m)
			{
				return this.PerformPosMoveRight();
			}

			// Token: 0x06001C7F RID: 7295 RVA: 0x000B3464 File Offset: 0x000B1664
			protected virtual bool PreProcessWmKeyDown_Down(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num >= this._hexBox._byteProvider.Length - (long)this._hexBox._bytesPerLine;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = Math.Min(this._hexBox._byteProvider.Length, num + (long)this._hexBox._iHexMaxHBytes);
					bool flag3 = num == this._hexBox._byteProvider.Length;
					if (flag3)
					{
						num2 = 0;
					}
					this._hexBox.SetPosition(num, num2);
					bool flag4 = num > this._hexBox._endByte - 1L;
					if (flag4)
					{
						this._hexBox.PerformScrollLineDown();
					}
					this._hexBox.UpdateCaret();
					this._hexBox.ScrollByteIntoView();
					this._hexBox.ReleaseSelection();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C80 RID: 7296 RVA: 0x000B3560 File Offset: 0x000B1760
			protected virtual bool PreProcessWmKeyDown_PageUp(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int byteCharacterPos = this._hexBox._byteCharacterPos;
				bool flag = num == 0L && byteCharacterPos == 0;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = Math.Max(0L, num - (long)this._hexBox._iHexMaxBytes);
					bool flag3 = num == 0L;
					if (flag3)
					{
						flag2 = true;
					}
					else
					{
						this._hexBox.SetPosition(num);
						bool flag4 = num < this._hexBox._startByte;
						if (flag4)
						{
							this._hexBox.PerformScrollPageUp();
						}
						this._hexBox.ReleaseSelection();
						this._hexBox.UpdateCaret();
						this._hexBox.Invalidate();
						flag2 = true;
					}
				}
				return flag2;
			}

			// Token: 0x06001C81 RID: 7297 RVA: 0x000B3618 File Offset: 0x000B1818
			protected virtual bool PreProcessWmKeyDown_PageDown(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num == this._hexBox._byteProvider.Length && num2 == 0;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = Math.Min(this._hexBox._byteProvider.Length, num + (long)this._hexBox._iHexMaxBytes);
					bool flag3 = num == this._hexBox._byteProvider.Length;
					if (flag3)
					{
						num2 = 0;
					}
					this._hexBox.SetPosition(num, num2);
					bool flag4 = num > this._hexBox._endByte - 1L;
					if (flag4)
					{
						this._hexBox.PerformScrollPageDown();
					}
					this._hexBox.ReleaseSelection();
					this._hexBox.UpdateCaret();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C82 RID: 7298 RVA: 0x000B3700 File Offset: 0x000B1900
			protected virtual bool PreProcessWmKeyDown_ShiftLeft(ref Message m)
			{
				long num = this._hexBox._bytePos;
				long num2 = this._hexBox._selectionLength;
				bool flag = num + num2 < 1L;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = num + num2 <= this._bpiStart.Index;
					if (flag3)
					{
						bool flag4 = num == 0L;
						if (flag4)
						{
							return true;
						}
						num -= 1L;
						num2 += 1L;
					}
					else
					{
						num2 = Math.Max(0L, num2 - 1L);
					}
					this._hexBox.ScrollByteIntoView();
					this._hexBox.InternalSelect(num, num2);
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C83 RID: 7299 RVA: 0x000B3798 File Offset: 0x000B1998
			protected virtual bool PreProcessWmKeyDown_ShiftUp(ref Message m)
			{
				long num = this._hexBox._bytePos;
				long num2 = this._hexBox._selectionLength;
				bool flag = num - (long)this._hexBox._iHexMaxHBytes < 0L && num <= this._bpiStart.Index;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = this._bpiStart.Index >= num + num2;
					if (flag3)
					{
						num -= (long)this._hexBox._iHexMaxHBytes;
						num2 += (long)this._hexBox._iHexMaxHBytes;
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView();
					}
					else
					{
						num2 -= (long)this._hexBox._iHexMaxHBytes;
						bool flag4 = num2 < 0L;
						if (flag4)
						{
							num = this._bpiStart.Index + num2;
							num2 = -num2;
							this._hexBox.InternalSelect(num, num2);
							this._hexBox.ScrollByteIntoView();
						}
						else
						{
							num2 -= (long)this._hexBox._iHexMaxHBytes;
							this._hexBox.InternalSelect(num, num2);
							this._hexBox.ScrollByteIntoView(num + num2);
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C84 RID: 7300 RVA: 0x000B38C4 File Offset: 0x000B1AC4
			protected virtual bool PreProcessWmKeyDown_ShiftRight(ref Message m)
			{
				long num = this._hexBox._bytePos;
				long num2 = this._hexBox._selectionLength;
				bool flag = num + num2 >= this._hexBox._byteProvider.Length;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = this._bpiStart.Index <= num;
					if (flag3)
					{
						num2 += 1L;
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView(num + num2);
					}
					else
					{
						num += 1L;
						num2 = Math.Max(0L, num2 - 1L);
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView();
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C85 RID: 7301 RVA: 0x000B397C File Offset: 0x000B1B7C
			protected virtual bool PreProcessWmKeyDown_ShiftDown(ref Message m)
			{
				long num = this._hexBox._bytePos;
				long num2 = this._hexBox._selectionLength;
				long length = this._hexBox._byteProvider.Length;
				bool flag = num + num2 + (long)this._hexBox._iHexMaxHBytes > length;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = this._bpiStart.Index <= num;
					if (flag3)
					{
						num2 += (long)this._hexBox._iHexMaxHBytes;
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView(num + num2);
					}
					else
					{
						num2 -= (long)this._hexBox._iHexMaxHBytes;
						bool flag4 = num2 < 0L;
						if (flag4)
						{
							num = this._bpiStart.Index;
							num2 = -num2;
						}
						else
						{
							num += (long)this._hexBox._iHexMaxHBytes;
						}
						this._hexBox.InternalSelect(num, num2);
						this._hexBox.ScrollByteIntoView();
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C86 RID: 7302 RVA: 0x000B3A7C File Offset: 0x000B1C7C
			protected virtual bool PreProcessWmKeyDown_Tab(ref Message m)
			{
				bool flag = this._hexBox._stringViewVisible && this._hexBox._keyInterpreter.GetType() == typeof(HexBox.KeyInterpreter);
				bool flag2;
				if (flag)
				{
					this._hexBox.ActivateStringKeyInterpreter();
					this._hexBox.ScrollByteIntoView();
					this._hexBox.ReleaseSelection();
					this._hexBox.UpdateCaret();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				else
				{
					bool flag3 = this._hexBox.Parent == null;
					if (flag3)
					{
						flag2 = true;
					}
					else
					{
						this._hexBox.Parent.SelectNextControl(this._hexBox, true, true, true, true);
						flag2 = true;
					}
				}
				return flag2;
			}

			// Token: 0x06001C87 RID: 7303 RVA: 0x000B3B34 File Offset: 0x000B1D34
			protected virtual bool PreProcessWmKeyDown_ShiftTab(ref Message m)
			{
				bool flag = this._hexBox._keyInterpreter is HexBox.StringKeyInterpreter;
				bool flag2;
				if (flag)
				{
					this._shiftDown = false;
					this._hexBox.ActivateKeyInterpreter();
					this._hexBox.ScrollByteIntoView();
					this._hexBox.ReleaseSelection();
					this._hexBox.UpdateCaret();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				else
				{
					bool flag3 = this._hexBox.Parent == null;
					if (flag3)
					{
						flag2 = true;
					}
					else
					{
						this._hexBox.Parent.SelectNextControl(this._hexBox, false, true, true, true);
						flag2 = true;
					}
				}
				return flag2;
			}

			// Token: 0x06001C88 RID: 7304 RVA: 0x000B3BD8 File Offset: 0x000B1DD8
			protected virtual bool PreProcessWmKeyDown_Back(ref Message m)
			{
				bool flag = !this._hexBox._byteProvider.SupportsDeleteBytes();
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool readOnly = this._hexBox.ReadOnly;
					if (readOnly)
					{
						flag2 = true;
					}
					else
					{
						long bytePos = this._hexBox._bytePos;
						long selectionLength = this._hexBox._selectionLength;
						long num = ((this._hexBox._byteCharacterPos == 0 && selectionLength == 0L) ? (bytePos - 1L) : bytePos);
						bool flag3 = num < 0L && selectionLength < 1L;
						if (flag3)
						{
							flag2 = true;
						}
						else
						{
							long num2 = ((selectionLength > 0L) ? selectionLength : 1L);
							this._hexBox._byteProvider.DeleteBytes(Math.Max(0L, num), num2);
							this._hexBox.UpdateVScrollSize();
							bool flag4 = selectionLength == 0L;
							if (flag4)
							{
								this.PerformPosMoveLeftByte();
							}
							this._hexBox.ReleaseSelection();
							this._hexBox.Invalidate();
							flag2 = true;
						}
					}
				}
				return flag2;
			}

			// Token: 0x06001C89 RID: 7305 RVA: 0x000B3CD0 File Offset: 0x000B1ED0
			protected virtual bool PreProcessWmKeyDown_Delete(ref Message m)
			{
				bool flag = !this._hexBox._byteProvider.SupportsDeleteBytes();
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool readOnly = this._hexBox.ReadOnly;
					if (readOnly)
					{
						flag2 = true;
					}
					else
					{
						long bytePos = this._hexBox._bytePos;
						long selectionLength = this._hexBox._selectionLength;
						bool flag3 = bytePos >= this._hexBox._byteProvider.Length;
						if (flag3)
						{
							flag2 = true;
						}
						else
						{
							long num = ((selectionLength > 0L) ? selectionLength : 1L);
							this._hexBox._byteProvider.DeleteBytes(bytePos, num);
							this._hexBox.UpdateVScrollSize();
							this._hexBox.ReleaseSelection();
							this._hexBox.Invalidate();
							flag2 = true;
						}
					}
				}
				return flag2;
			}

			// Token: 0x06001C8A RID: 7306 RVA: 0x000B3D98 File Offset: 0x000B1F98
			protected virtual bool PreProcessWmKeyDown_Home(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num < 1L;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = 0L;
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					this._hexBox.ScrollByteIntoView();
					this._hexBox.UpdateCaret();
					this._hexBox.ReleaseSelection();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C8B RID: 7307 RVA: 0x000B3E08 File Offset: 0x000B2008
			protected virtual bool PreProcessWmKeyDown_End(ref Message m)
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num >= this._hexBox._byteProvider.Length - 1L;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = this._hexBox._byteProvider.Length - 1L;
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					this._hexBox.ScrollByteIntoView();
					this._hexBox.UpdateCaret();
					this._hexBox.ReleaseSelection();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C8C RID: 7308 RVA: 0x000B3E9C File Offset: 0x000B209C
			protected virtual bool PreProcessWmKeyDown_ShiftShiftKey(ref Message m)
			{
				bool mouseDown = this._mouseDown;
				bool flag;
				if (mouseDown)
				{
					flag = true;
				}
				else
				{
					bool shiftDown = this._shiftDown;
					if (shiftDown)
					{
						flag = true;
					}
					else
					{
						this._shiftDown = true;
						bool flag2 = this._hexBox._selectionLength > 0L;
						if (flag2)
						{
							flag = true;
						}
						else
						{
							this._bpiStart = new BytePositionInfo(this._hexBox._bytePos, this._hexBox._byteCharacterPos);
							flag = true;
						}
					}
				}
				return flag;
			}

			// Token: 0x06001C8D RID: 7309 RVA: 0x000B3F0C File Offset: 0x000B210C
			protected virtual bool PreProcessWmKeyDown_ControlC(ref Message m)
			{
				this._hexBox.Copy();
				return true;
			}

			// Token: 0x06001C8E RID: 7310 RVA: 0x000B3F2C File Offset: 0x000B212C
			protected virtual bool PreProcessWmKeyDown_ControlX(ref Message m)
			{
				this._hexBox.Cut();
				return true;
			}

			// Token: 0x06001C8F RID: 7311 RVA: 0x000B3F4C File Offset: 0x000B214C
			protected virtual bool PreProcessWmKeyDown_ControlV(ref Message m)
			{
				this._hexBox.Paste();
				return true;
			}

			// Token: 0x06001C90 RID: 7312 RVA: 0x000B3F6C File Offset: 0x000B216C
			public virtual bool PreProcessWmChar(ref Message m)
			{
				bool flag = Control.ModifierKeys == Keys.Control;
				bool flag2;
				if (flag)
				{
					flag2 = this._hexBox.BasePreProcessMessage(ref m);
				}
				else
				{
					bool flag3 = this._hexBox._byteProvider.SupportsWriteByte();
					bool flag4 = this._hexBox._byteProvider.SupportsInsertBytes();
					bool flag5 = this._hexBox._byteProvider.SupportsDeleteBytes();
					long bytePos = this._hexBox._bytePos;
					long selectionLength = this._hexBox._selectionLength;
					int num = this._hexBox._byteCharacterPos;
					bool flag6 = (!flag3 && bytePos != this._hexBox._byteProvider.Length) || (!flag4 && bytePos == this._hexBox._byteProvider.Length);
					if (flag6)
					{
						flag2 = this._hexBox.BasePreProcessMessage(ref m);
					}
					else
					{
						char c = (char)m.WParam.ToInt32();
						bool flag7 = Uri.IsHexDigit(c);
						if (flag7)
						{
							bool flag8 = this.RaiseKeyPress(c);
							if (flag8)
							{
								flag2 = true;
							}
							else
							{
								bool readOnly = this._hexBox.ReadOnly;
								if (readOnly)
								{
									flag2 = true;
								}
								else
								{
									bool flag9 = bytePos == this._hexBox._byteProvider.Length;
									bool flag10 = !flag9 && flag4 && this._hexBox.InsertActive && num == 0;
									if (flag10)
									{
										flag9 = true;
									}
									bool flag11 = flag5 && flag4 && selectionLength > 0L;
									if (flag11)
									{
										this._hexBox._byteProvider.DeleteBytes(bytePos, selectionLength);
										flag9 = true;
										num = 0;
										this._hexBox.SetPosition(bytePos, num);
									}
									this._hexBox.ReleaseSelection();
									bool flag12 = flag9;
									byte b;
									if (flag12)
									{
										b = 0;
									}
									else
									{
										b = this._hexBox._byteProvider.ReadByte(bytePos);
									}
									string text = b.ToString("X", Thread.CurrentThread.CurrentCulture);
									bool flag13 = text.Length == 1;
									if (flag13)
									{
										text = "0" + text;
									}
									string text2 = c.ToString();
									bool flag14 = num == 0;
									if (flag14)
									{
										text2 += text.Substring(1, 1);
									}
									else
									{
										text2 = text.Substring(0, 1) + text2;
									}
									byte b2 = byte.Parse(text2, NumberStyles.AllowHexSpecifier, Thread.CurrentThread.CurrentCulture);
									bool flag15 = flag9;
									if (flag15)
									{
										this._hexBox._byteProvider.InsertBytes(bytePos, new byte[] { b2 });
									}
									else
									{
										this._hexBox._byteProvider.WriteByte(bytePos, b2, false);
									}
									this.PerformPosMoveRight();
									this._hexBox.Invalidate();
									flag2 = true;
								}
							}
						}
						else
						{
							flag2 = this._hexBox.BasePreProcessMessage(ref m);
						}
					}
				}
				return flag2;
			}

			// Token: 0x06001C91 RID: 7313 RVA: 0x000B4230 File Offset: 0x000B2430
			protected bool RaiseKeyPress(char keyChar)
			{
				KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs(keyChar);
				this._hexBox.OnKeyPress(keyPressEventArgs);
				return keyPressEventArgs.Handled;
			}

			// Token: 0x06001C92 RID: 7314 RVA: 0x000B425C File Offset: 0x000B245C
			public virtual bool PreProcessWmKeyUp(ref Message m)
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				Keys keys2 = keys | Control.ModifierKeys;
				Keys keys3 = keys2;
				if (keys3 == Keys.ShiftKey || keys3 == Keys.Insert)
				{
					bool flag = this.RaiseKeyUp(keys2);
					if (flag)
					{
						return true;
					}
				}
				Keys keys4 = keys2;
				bool flag2;
				if (keys4 != Keys.ShiftKey)
				{
					if (keys4 != Keys.Insert)
					{
						flag2 = this._hexBox.BasePreProcessMessage(ref m);
					}
					else
					{
						flag2 = this.PreProcessWmKeyUp_Insert(ref m);
					}
				}
				else
				{
					this._shiftDown = false;
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C93 RID: 7315 RVA: 0x000B42E4 File Offset: 0x000B24E4
			protected virtual bool PreProcessWmKeyUp_Insert(ref Message m)
			{
				this._hexBox.InsertActive = !this._hexBox.InsertActive;
				return true;
			}

			// Token: 0x06001C94 RID: 7316 RVA: 0x000B4314 File Offset: 0x000B2514
			protected bool RaiseKeyUp(Keys keyData)
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
				this._hexBox.OnKeyUp(keyEventArgs);
				return keyEventArgs.Handled;
			}

			// Token: 0x170007B3 RID: 1971
			// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000B4340 File Offset: 0x000B2540
			private Dictionary<Keys, HexBox.KeyInterpreter.MessageDelegate> MessageHandlers
			{
				get
				{
					bool flag = this._messageHandlers == null;
					if (flag)
					{
						this._messageHandlers = new Dictionary<Keys, HexBox.KeyInterpreter.MessageDelegate>();
						this._messageHandlers.Add(Keys.Left, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Left));
						this._messageHandlers.Add(Keys.Up, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Up));
						this._messageHandlers.Add(Keys.Right, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Right));
						this._messageHandlers.Add(Keys.Down, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Down));
						this._messageHandlers.Add(Keys.Prior, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_PageUp));
						this._messageHandlers.Add(Keys.Next, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_PageDown));
						this._messageHandlers.Add(Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ShiftLeft));
						this._messageHandlers.Add(Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ShiftUp));
						this._messageHandlers.Add(Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ShiftRight));
						this._messageHandlers.Add(Keys.Back | Keys.Space | Keys.Shift, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ShiftDown));
						this._messageHandlers.Add(Keys.Tab, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Tab));
						this._messageHandlers.Add(Keys.Back, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Back));
						this._messageHandlers.Add(Keys.Delete, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Delete));
						this._messageHandlers.Add(Keys.Home, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_Home));
						this._messageHandlers.Add(Keys.End, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_End));
						this._messageHandlers.Add(Keys.ShiftKey | Keys.Shift, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ShiftShiftKey));
						this._messageHandlers.Add((Keys)131139, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ControlC));
						this._messageHandlers.Add((Keys)131160, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ControlX));
						this._messageHandlers.Add((Keys)131158, new HexBox.KeyInterpreter.MessageDelegate(this.PreProcessWmKeyDown_ControlV));
					}
					return this._messageHandlers;
				}
			}

			// Token: 0x06001C96 RID: 7318 RVA: 0x000B4590 File Offset: 0x000B2790
			protected virtual bool PerformPosMoveLeft()
			{
				long num = this._hexBox._bytePos;
				long selectionLength = this._hexBox._selectionLength;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = selectionLength != 0L;
				if (flag)
				{
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					this._hexBox.ReleaseSelection();
				}
				else
				{
					bool flag2 = num == 0L && num2 == 0;
					if (flag2)
					{
						return true;
					}
					bool flag3 = num2 > 0;
					if (flag3)
					{
						num2--;
					}
					else
					{
						num = Math.Max(0L, num - 1L);
						num2++;
					}
					this._hexBox.SetPosition(num, num2);
					bool flag4 = num < this._hexBox._startByte;
					if (flag4)
					{
						this._hexBox.PerformScrollLineUp();
					}
					this._hexBox.UpdateCaret();
					this._hexBox.Invalidate();
				}
				this._hexBox.ScrollByteIntoView();
				return true;
			}

			// Token: 0x06001C97 RID: 7319 RVA: 0x000B4684 File Offset: 0x000B2884
			protected virtual bool PerformPosMoveRight()
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				long selectionLength = this._hexBox._selectionLength;
				bool flag = selectionLength != 0L;
				if (flag)
				{
					num += selectionLength;
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					this._hexBox.ReleaseSelection();
				}
				else
				{
					bool flag2 = num != this._hexBox._byteProvider.Length || num2 != 0;
					if (flag2)
					{
						bool flag3 = num2 > 0;
						if (flag3)
						{
							num = Math.Min(this._hexBox._byteProvider.Length, num + 1L);
							num2 = 0;
						}
						else
						{
							num2++;
						}
						bool flag4 = num >= this._hexBox._byteProvider.Length;
						if (flag4)
						{
							return true;
						}
						this._hexBox.SetPosition(num, num2);
						bool flag5 = num > this._hexBox._endByte - 1L;
						if (flag5)
						{
							this._hexBox.PerformScrollLineDown();
						}
						this._hexBox.UpdateCaret();
						this._hexBox.Invalidate();
					}
				}
				this._hexBox.ScrollByteIntoView();
				return true;
			}

			// Token: 0x06001C98 RID: 7320 RVA: 0x000B47BC File Offset: 0x000B29BC
			protected virtual bool PerformPosMoveLeftByte()
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num == 0L;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = Math.Max(0L, num - 1L);
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					bool flag3 = num < this._hexBox._startByte;
					if (flag3)
					{
						this._hexBox.PerformScrollLineUp();
					}
					this._hexBox.UpdateCaret();
					this._hexBox.ScrollByteIntoView();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C99 RID: 7321 RVA: 0x000B4858 File Offset: 0x000B2A58
			protected virtual bool PerformPosMoveRightByte()
			{
				long num = this._hexBox._bytePos;
				int num2 = this._hexBox._byteCharacterPos;
				bool flag = num == this._hexBox._byteProvider.Length;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					num = Math.Min(this._hexBox._byteProvider.Length, num + 1L);
					num2 = 0;
					this._hexBox.SetPosition(num, num2);
					bool flag3 = num > this._hexBox._endByte - 1L;
					if (flag3)
					{
						this._hexBox.PerformScrollLineDown();
					}
					this._hexBox.UpdateCaret();
					this._hexBox.ScrollByteIntoView();
					this._hexBox.Invalidate();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06001C9A RID: 7322 RVA: 0x000B4914 File Offset: 0x000B2B14
			public virtual PointF GetCaretPointF(long byteIndex)
			{
				return this._hexBox.GetBytePointF(byteIndex);
			}

			// Token: 0x06001C9B RID: 7323 RVA: 0x000B4934 File Offset: 0x000B2B34
			protected virtual BytePositionInfo GetBytePositionInfo(Point p)
			{
				return this._hexBox.GetHexBytePositionInfo(p);
			}

			// Token: 0x04000DE6 RID: 3558
			protected HexBox _hexBox;

			// Token: 0x04000DE7 RID: 3559
			protected bool _shiftDown;

			// Token: 0x04000DE8 RID: 3560
			private bool _mouseDown;

			// Token: 0x04000DE9 RID: 3561
			private BytePositionInfo _bpiStart;

			// Token: 0x04000DEA RID: 3562
			private BytePositionInfo _bpi;

			// Token: 0x04000DEB RID: 3563
			private Dictionary<Keys, HexBox.KeyInterpreter.MessageDelegate> _messageHandlers;

			// Token: 0x020002E5 RID: 741
			// (Invoke) Token: 0x06001EDC RID: 7900
			private delegate bool MessageDelegate(ref Message m);
		}

		// Token: 0x0200021D RID: 541
		private class StringKeyInterpreter : HexBox.KeyInterpreter
		{
			// Token: 0x06001C9C RID: 7324 RVA: 0x000B4952 File Offset: 0x000B2B52
			public StringKeyInterpreter(HexBox hexBox)
				: base(hexBox)
			{
				this._hexBox._byteCharacterPos = 0;
			}

			// Token: 0x06001C9D RID: 7325 RVA: 0x000B496C File Offset: 0x000B2B6C
			public override bool PreProcessWmKeyDown(ref Message m)
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				Keys keys2 = keys | Control.ModifierKeys;
				Keys keys3 = keys2;
				if (keys3 == Keys.Tab || keys3 == (Keys.LButton | Keys.Back | Keys.Shift))
				{
					bool flag = base.RaiseKeyDown(keys2);
					if (flag)
					{
						return true;
					}
				}
				Keys keys4 = keys2;
				bool flag2;
				if (keys4 != Keys.Tab)
				{
					if (keys4 != (Keys.LButton | Keys.Back | Keys.Shift))
					{
						flag2 = base.PreProcessWmKeyDown(ref m);
					}
					else
					{
						flag2 = this.PreProcessWmKeyDown_ShiftTab(ref m);
					}
				}
				else
				{
					flag2 = this.PreProcessWmKeyDown_Tab(ref m);
				}
				return flag2;
			}

			// Token: 0x06001C9E RID: 7326 RVA: 0x000B49F4 File Offset: 0x000B2BF4
			protected override bool PreProcessWmKeyDown_Left(ref Message m)
			{
				return this.PerformPosMoveLeftByte();
			}

			// Token: 0x06001C9F RID: 7327 RVA: 0x000B4A0C File Offset: 0x000B2C0C
			protected override bool PreProcessWmKeyDown_Right(ref Message m)
			{
				return this.PerformPosMoveRightByte();
			}

			// Token: 0x06001CA0 RID: 7328 RVA: 0x000B4A24 File Offset: 0x000B2C24
			public override bool PreProcessWmChar(ref Message m)
			{
				bool flag = Control.ModifierKeys == Keys.Control;
				bool flag2;
				if (flag)
				{
					flag2 = this._hexBox.BasePreProcessMessage(ref m);
				}
				else
				{
					bool flag3 = this._hexBox._byteProvider.SupportsWriteByte();
					bool flag4 = this._hexBox._byteProvider.SupportsInsertBytes();
					bool flag5 = this._hexBox._byteProvider.SupportsDeleteBytes();
					long bytePos = this._hexBox._bytePos;
					long selectionLength = this._hexBox._selectionLength;
					int num = this._hexBox._byteCharacterPos;
					bool flag6 = (!flag3 && bytePos != this._hexBox._byteProvider.Length) || (!flag4 && bytePos == this._hexBox._byteProvider.Length);
					if (flag6)
					{
						flag2 = this._hexBox.BasePreProcessMessage(ref m);
					}
					else
					{
						char c = (char)m.WParam.ToInt32();
						bool flag7 = base.RaiseKeyPress(c);
						if (flag7)
						{
							flag2 = true;
						}
						else
						{
							bool readOnly = this._hexBox.ReadOnly;
							if (readOnly)
							{
								flag2 = true;
							}
							else
							{
								bool flag8 = bytePos == this._hexBox._byteProvider.Length;
								bool flag9 = !flag8 && flag4 && this._hexBox.InsertActive;
								if (flag9)
								{
									flag8 = true;
								}
								bool flag10 = flag5 && flag4 && selectionLength > 0L;
								if (flag10)
								{
									this._hexBox._byteProvider.DeleteBytes(bytePos, selectionLength);
									flag8 = true;
									num = 0;
									this._hexBox.SetPosition(bytePos, num);
								}
								this._hexBox.ReleaseSelection();
								byte b = this._hexBox.ByteCharConverter.ToByte(c);
								bool flag11 = flag8;
								if (flag11)
								{
									this._hexBox._byteProvider.InsertBytes(bytePos, new byte[] { b });
								}
								else
								{
									this._hexBox._byteProvider.WriteByte(bytePos, b, false);
								}
								this.PerformPosMoveRightByte();
								this._hexBox.Invalidate();
								flag2 = true;
							}
						}
					}
				}
				return flag2;
			}

			// Token: 0x06001CA1 RID: 7329 RVA: 0x000B4C28 File Offset: 0x000B2E28
			public override PointF GetCaretPointF(long byteIndex)
			{
				Point gridBytePoint = this._hexBox.GetGridBytePoint(byteIndex);
				return this._hexBox.GetByteStringPointF(gridBytePoint);
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x000B4C54 File Offset: 0x000B2E54
			protected override BytePositionInfo GetBytePositionInfo(Point p)
			{
				return this._hexBox.GetStringBytePositionInfo(p);
			}
		}
	}
}
