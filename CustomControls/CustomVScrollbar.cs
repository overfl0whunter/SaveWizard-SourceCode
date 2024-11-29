using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PS3SaveEditor;
using PS3SaveEditor.CustomScrollbar;

namespace CustomControls
{
	// Token: 0x020000E9 RID: 233
	public class CustomVScrollbar : UserControl
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000A92 RID: 2706 RVA: 0x0003D85C File Offset: 0x0003BA5C
		// (remove) Token: 0x06000A93 RID: 2707 RVA: 0x0003D894 File Offset: 0x0003BA94
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public new event EventHandler Scroll = null;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000A94 RID: 2708 RVA: 0x0003D8CC File Offset: 0x0003BACC
		// (remove) Token: 0x06000A95 RID: 2709 RVA: 0x0003D904 File Offset: 0x0003BB04
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ValueChanged = null;

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003D93C File Offset: 0x0003BB3C
		private int GetThumbHeight()
		{
			int num = base.Height - (this.UpArrowImage.Height + this.DownArrowImage.Height);
			float num2 = (float)this.LargeChange / (float)this.Maximum * (float)num;
			int num3 = (int)num2;
			bool flag = num3 > num;
			if (flag)
			{
				num3 = num;
				num2 = (float)num;
			}
			bool flag2 = num3 < 56;
			if (flag2)
			{
				num3 = 56;
			}
			return num3;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0003D9AC File Offset: 0x0003BBAC
		public CustomVScrollbar()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			this.moChannelColor = Color.FromArgb(125, 45, 17);
			this.UpArrowImage = Resource.uparrow;
			this.DownArrowImage = Resource.downarrow;
			this.ThumbBottomImage = Resource.ThumbBottom;
			this.ThumbBottomSpanImage = Resource.ThumbSpanBottom;
			this.ThumbTopImage = Resource.ThumbTop;
			this.ThumbTopSpanImage = Resource.ThumbSpanTop;
			this.ThumbMiddleImage = Resource.ThumbMiddle;
			base.Width = this.UpArrowImage.Width;
			base.MinimumSize = new Size(this.UpArrowImage.Width, this.UpArrowImage.Height + this.DownArrowImage.Height + this.GetThumbHeight());
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0003DB54 File Offset: 0x0003BD54
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("LargeChange")]
		public int LargeChange
		{
			get
			{
				return this.moLargeChange;
			}
			set
			{
				this.moLargeChange = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0003DB68 File Offset: 0x0003BD68
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0003DB80 File Offset: 0x0003BD80
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("SmallChange")]
		public int SmallChange
		{
			get
			{
				return this.moSmallChange;
			}
			set
			{
				this.moSmallChange = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0003DB94 File Offset: 0x0003BD94
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0003DBAC File Offset: 0x0003BDAC
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Minimum")]
		public int Minimum
		{
			get
			{
				return this.moMinimum;
			}
			set
			{
				this.moMinimum = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0003DBC0 File Offset: 0x0003BDC0
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Maximum")]
		public int Maximum
		{
			get
			{
				return this.moMaximum;
			}
			set
			{
				this.moMaximum = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0003DC04 File Offset: 0x0003BE04
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Value")]
		public int Value
		{
			get
			{
				return this.moValue;
			}
			set
			{
				this.moValue = value;
				int num = base.Height - (this.UpArrowImage.Height + this.DownArrowImage.Height);
				float num2 = (float)this.LargeChange / (float)this.Maximum * (float)num;
				int num3 = (int)num2;
				bool flag = num3 > num;
				if (flag)
				{
					num3 = num;
					num2 = (float)num;
				}
				bool flag2 = num3 < 56;
				if (flag2)
				{
					num3 = 56;
				}
				int num4 = num - num3;
				int num5 = this.Maximum - this.Minimum - this.LargeChange;
				float num6 = 0f;
				bool flag3 = num5 != 0;
				if (flag3)
				{
					num6 = (float)this.moValue / (float)num5;
				}
				float num7 = num6 * (float)num4;
				this.moThumbTop = (int)num7;
				base.Invalidate();
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0003DCE0 File Offset: 0x0003BEE0
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Channel Color")]
		public Color ChannelColor
		{
			get
			{
				return this.moChannelColor;
			}
			set
			{
				this.moChannelColor = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0003DCEC File Offset: 0x0003BEEC
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0003DD04 File Offset: 0x0003BF04
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image UpArrowImage
		{
			get
			{
				return this.moUpArrowImage;
			}
			set
			{
				this.moUpArrowImage = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0003DD10 File Offset: 0x0003BF10
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0003DD28 File Offset: 0x0003BF28
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image DownArrowImage
		{
			get
			{
				return this.moDownArrowImage;
			}
			set
			{
				this.moDownArrowImage = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0003DD34 File Offset: 0x0003BF34
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0003DD4C File Offset: 0x0003BF4C
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbTopImage
		{
			get
			{
				return this.moThumbTopImage;
			}
			set
			{
				this.moThumbTopImage = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0003DD58 File Offset: 0x0003BF58
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0003DD70 File Offset: 0x0003BF70
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbTopSpanImage
		{
			get
			{
				return this.moThumbTopSpanImage;
			}
			set
			{
				this.moThumbTopSpanImage = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0003DD94 File Offset: 0x0003BF94
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbBottomImage
		{
			get
			{
				return this.moThumbBottomImage;
			}
			set
			{
				this.moThumbBottomImage = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbBottomSpanImage
		{
			get
			{
				return this.moThumbBottomSpanImage;
			}
			set
			{
				this.moThumbBottomSpanImage = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbMiddleImage
		{
			get
			{
				return this.moThumbMiddleImage;
			}
			set
			{
				this.moThumbMiddleImage = value;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			bool flag = this.UpArrowImage != null;
			if (flag)
			{
				e.Graphics.DrawImage(this.UpArrowImage, new Rectangle(new Point(0, 0), new Size(base.Width, this.UpArrowImage.Height)));
			}
			Brush brush = new SolidBrush(this.moChannelColor);
			Brush brush2 = new SolidBrush(this.moChannelColor);
			e.Graphics.FillRectangle(brush, new Rectangle(0, this.UpArrowImage.Height, base.Width, base.Height - this.DownArrowImage.Height));
			int num = base.Height - (this.UpArrowImage.Height + this.DownArrowImage.Height);
			float num2 = (float)this.LargeChange / (float)this.Maximum * (float)num;
			int num3 = (int)num2;
			bool flag2 = num3 > num;
			if (flag2)
			{
				num3 = num;
				num2 = (float)num;
			}
			bool flag3 = num3 < 56;
			if (flag3)
			{
				num2 = 56f;
			}
			float num4 = (num2 - (float)(this.ThumbMiddleImage.Height + this.ThumbTopImage.Height + this.ThumbBottomImage.Height)) / 2f;
			int num5 = (int)num4;
			int num6 = this.moThumbTop;
			num6 += this.UpArrowImage.Height;
			e.Graphics.DrawImage(this.ThumbTopImage, new Rectangle(1, num6, base.Width - 2, this.ThumbTopImage.Height));
			num6 += this.ThumbTopImage.Height;
			Rectangle rectangle = new Rectangle(1, num6, base.Width - 2, num5);
			e.Graphics.DrawImage(this.ThumbTopSpanImage, 1f, (float)num6, (float)base.Width - 2f, num4 * 2f);
			num6 += num5;
			e.Graphics.DrawImage(this.ThumbMiddleImage, new Rectangle(1, num6, base.Width - 2, this.ThumbMiddleImage.Height));
			num6 += this.ThumbMiddleImage.Height;
			rectangle = new Rectangle(1, num6, base.Width - 2, num5 * 2);
			e.Graphics.DrawImage(this.ThumbBottomSpanImage, rectangle);
			num6 += num5;
			e.Graphics.DrawImage(this.ThumbBottomImage, new Rectangle(1, num6, base.Width - 2, num5));
			bool flag4 = this.DownArrowImage != null;
			if (flag4)
			{
				e.Graphics.DrawImage(this.DownArrowImage, new Rectangle(new Point(0, base.Height - this.DownArrowImage.Height), new Size(base.Width, this.DownArrowImage.Height)));
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0003E0B4 File Offset: 0x0003C2B4
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
				bool autoSize = base.AutoSize;
				if (autoSize)
				{
					base.Width = this.moUpArrowImage.Width;
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003E100 File Offset: 0x0003C300
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Name = "CustomVScrollbar";
			base.MouseDown += this.CustomScrollbar_MouseDown;
			base.MouseMove += this.CustomScrollbar_MouseMove;
			base.MouseUp += this.CustomScrollbar_MouseUp;
			base.ResumeLayout(false);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003E164 File Offset: 0x0003C364
		private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			Point point = base.PointToClient(Cursor.Position);
			int num = base.Height - (this.UpArrowImage.Height + this.DownArrowImage.Height);
			float num2 = (float)this.LargeChange / (float)this.Maximum * (float)num;
			int num3 = (int)num2;
			bool flag = num3 > num;
			if (flag)
			{
				num3 = num;
				num2 = (float)num;
			}
			bool flag2 = num3 < 56;
			if (flag2)
			{
				num3 = 56;
			}
			int num4 = this.moThumbTop;
			num4 += this.UpArrowImage.Height;
			Rectangle rectangle = new Rectangle(new Point(1, num4), new Size(this.ThumbMiddleImage.Width, num3));
			bool flag3 = rectangle.Contains(point);
			if (flag3)
			{
				this.nClickPoint = point.Y - num4;
				this.moThumbDown = true;
			}
			Rectangle rectangle2 = new Rectangle(new Point(1, 0), new Size(this.UpArrowImage.Width, this.UpArrowImage.Height));
			bool flag4 = rectangle2.Contains(point);
			if (flag4)
			{
				int num5 = this.Maximum - this.Minimum - this.LargeChange;
				int num6 = num - num3;
				bool flag5 = num5 > 0;
				if (flag5)
				{
					bool flag6 = num6 > 0;
					if (flag6)
					{
						bool flag7 = this.moThumbTop - this.SmallChange < 0;
						if (flag7)
						{
							this.moThumbTop = 0;
						}
						else
						{
							this.moThumbTop -= this.SmallChange;
						}
						float num7 = (float)this.moThumbTop / (float)num6;
						float num8 = num7 * (float)(this.Maximum - this.LargeChange);
						this.moValue = (int)num8;
						bool flag8 = this.ValueChanged != null;
						if (flag8)
						{
							this.ValueChanged(this, new EventArgs());
						}
						bool flag9 = this.Scroll != null;
						if (flag9)
						{
							this.Scroll(this, new EventArgs());
						}
						base.Invalidate();
					}
				}
			}
			Rectangle rectangle3 = new Rectangle(new Point(1, this.UpArrowImage.Height + num), new Size(this.UpArrowImage.Width, this.UpArrowImage.Height));
			bool flag10 = rectangle3.Contains(point);
			if (flag10)
			{
				int num9 = this.Maximum - this.Minimum - this.LargeChange;
				int num10 = num - num3;
				bool flag11 = num9 > 0;
				if (flag11)
				{
					bool flag12 = num10 > 0;
					if (flag12)
					{
						bool flag13 = this.moThumbTop + this.SmallChange > num10;
						if (flag13)
						{
							this.moThumbTop = num10;
						}
						else
						{
							this.moThumbTop += this.SmallChange;
						}
						float num11 = (float)this.moThumbTop / (float)num10;
						float num12 = num11 * (float)(this.Maximum - this.LargeChange);
						this.moValue = (int)num12;
						bool flag14 = this.ValueChanged != null;
						if (flag14)
						{
							this.ValueChanged(this, new EventArgs());
						}
						bool flag15 = this.Scroll != null;
						if (flag15)
						{
							this.Scroll(this, new EventArgs());
						}
						base.Invalidate();
					}
				}
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003E47D File Offset: 0x0003C67D
		private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			this.moThumbDown = false;
			this.moThumbDragging = false;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0003E490 File Offset: 0x0003C690
		private void MoveThumb(int y)
		{
			int num = this.Maximum - this.Minimum;
			int num2 = base.Height - (this.UpArrowImage.Height + this.DownArrowImage.Height);
			float num3 = (float)this.LargeChange / (float)this.Maximum * (float)num2;
			int num4 = (int)num3;
			bool flag = num4 > num2;
			if (flag)
			{
				num4 = num2;
				num3 = (float)num2;
			}
			bool flag2 = num4 < 56;
			if (flag2)
			{
				num4 = 56;
			}
			int num5 = this.nClickPoint;
			int num6 = num2 - num4;
			bool flag3 = this.moThumbDown && num > 0;
			if (flag3)
			{
				bool flag4 = num6 > 0;
				if (flag4)
				{
					int num7 = y - (this.UpArrowImage.Height + num5);
					bool flag5 = num7 < 0;
					if (flag5)
					{
						this.moThumbTop = 0;
					}
					else
					{
						bool flag6 = num7 > num6;
						if (flag6)
						{
							this.moThumbTop = num6;
						}
						else
						{
							this.moThumbTop = y - (this.UpArrowImage.Height + num5);
						}
					}
					float num8 = (float)this.moThumbTop / (float)num6;
					float num9 = num8 * (float)(this.Maximum - this.LargeChange);
					this.moValue = (int)num9;
					Application.DoEvents();
					base.Invalidate();
				}
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e)
		{
			bool flag = this.moThumbDown;
			if (flag)
			{
				this.moThumbDragging = true;
			}
			bool flag2 = this.moThumbDragging;
			if (flag2)
			{
				this.MoveThumb(e.Y);
			}
			bool flag3 = this.ValueChanged != null;
			if (flag3)
			{
				this.ValueChanged(this, new EventArgs());
			}
			bool flag4 = this.Scroll != null;
			if (flag4)
			{
				this.Scroll(this, new EventArgs());
			}
		}

		// Token: 0x040005DD RID: 1501
		protected Color moChannelColor = Color.Empty;

		// Token: 0x040005DE RID: 1502
		protected Image moUpArrowImage = null;

		// Token: 0x040005DF RID: 1503
		protected Image moDownArrowImage = null;

		// Token: 0x040005E0 RID: 1504
		protected Image moThumbArrowImage = null;

		// Token: 0x040005E1 RID: 1505
		protected Image moThumbTopImage = null;

		// Token: 0x040005E2 RID: 1506
		protected Image moThumbTopSpanImage = null;

		// Token: 0x040005E3 RID: 1507
		protected Image moThumbBottomImage = null;

		// Token: 0x040005E4 RID: 1508
		protected Image moThumbBottomSpanImage = null;

		// Token: 0x040005E5 RID: 1509
		protected Image moThumbMiddleImage = null;

		// Token: 0x040005E6 RID: 1510
		protected int moLargeChange = 10;

		// Token: 0x040005E7 RID: 1511
		protected int moSmallChange = 1;

		// Token: 0x040005E8 RID: 1512
		protected int moMinimum = 0;

		// Token: 0x040005E9 RID: 1513
		protected int moMaximum = 100;

		// Token: 0x040005EA RID: 1514
		protected int moValue = 0;

		// Token: 0x040005EB RID: 1515
		private int nClickPoint;

		// Token: 0x040005EC RID: 1516
		protected int moThumbTop = 0;

		// Token: 0x040005ED RID: 1517
		protected bool moAutoSize = false;

		// Token: 0x040005EE RID: 1518
		private bool moThumbDown = false;

		// Token: 0x040005EF RID: 1519
		private bool moThumbDragging = false;
	}
}
