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
	// Token: 0x020000E8 RID: 232
	public class CustomHScrollbar : UserControl
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000A6A RID: 2666 RVA: 0x0003C9EC File Offset: 0x0003ABEC
		// (remove) Token: 0x06000A6B RID: 2667 RVA: 0x0003CA24 File Offset: 0x0003AC24
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public new event EventHandler Scroll = null;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000A6C RID: 2668 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		// (remove) Token: 0x06000A6D RID: 2669 RVA: 0x0003CA94 File Offset: 0x0003AC94
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ValueChanged = null;

		// Token: 0x06000A6E RID: 2670 RVA: 0x0003CACC File Offset: 0x0003ACCC
		private int GetThumbWidth()
		{
			int num = base.Width - (this.LeftArrowImage.Width + this.RightArrowImage.Width);
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

		// Token: 0x06000A6F RID: 2671 RVA: 0x0003CB3C File Offset: 0x0003AD3C
		public CustomHScrollbar()
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			this.moChannelColor = Color.FromArgb(51, 166, 3);
			this.LeftArrowImage = Resource.leftarrow;
			this.RightArrowImage = Resource.rightarrow;
			this.ThumbLeftImage = Resource.ThumbLeft;
			this.ThumbLeftSpanImage = Resource.ThumbSpanLeft;
			this.ThumbRightImage = Resource.ThumbRight;
			this.ThumbRightSpanImage = Resource.ThumbSpanRight;
			this.ThumbMiddleImage = Resource.ThumbMiddleH;
			base.Height = this.LeftArrowImage.Height;
			base.MinimumSize = new Size(this.LeftArrowImage.Width + this.RightArrowImage.Width + this.GetThumbWidth(), this.LeftArrowImage.Height);
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0003CCD0 File Offset: 0x0003AED0
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0003CCFC File Offset: 0x0003AEFC
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0003CD14 File Offset: 0x0003AF14
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0003CD28 File Offset: 0x0003AF28
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0003CD40 File Offset: 0x0003AF40
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

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0003CD54 File Offset: 0x0003AF54
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0003CD6C File Offset: 0x0003AF6C
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0003CD80 File Offset: 0x0003AF80
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0003CD98 File Offset: 0x0003AF98
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
				int num = base.Width - (this.LeftArrowImage.Width + this.RightArrowImage.Width);
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
				this.moThumbRight = (int)num7;
				base.Invalidate();
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0003CE5C File Offset: 0x0003B05C
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0003CE74 File Offset: 0x0003B074
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0003CE80 File Offset: 0x0003B080
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0003CE98 File Offset: 0x0003B098
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image LeftArrowImage
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image RightArrowImage
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0003CEE0 File Offset: 0x0003B0E0
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbRightImage
		{
			get
			{
				return this.moThumbRightImage;
			}
			set
			{
				this.moThumbRightImage = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0003CEEC File Offset: 0x0003B0EC
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0003CF04 File Offset: 0x0003B104
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbRightSpanImage
		{
			get
			{
				return this.moThumbRightSpanImage;
			}
			set
			{
				this.moThumbRightSpanImage = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0003CF10 File Offset: 0x0003B110
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0003CF28 File Offset: 0x0003B128
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbLeftImage
		{
			get
			{
				return this.moThumbLeftImage;
			}
			set
			{
				this.moThumbLeftImage = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0003CF34 File Offset: 0x0003B134
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0003CF4C File Offset: 0x0003B14C
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Up Arrow Graphic")]
		public Image ThumbLeftSpanImage
		{
			get
			{
				return this.moThumbLeftSpanImage;
			}
			set
			{
				this.moThumbLeftSpanImage = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0003CF58 File Offset: 0x0003B158
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0003CF70 File Offset: 0x0003B170
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

		// Token: 0x06000A8A RID: 2698 RVA: 0x0003CF7C File Offset: 0x0003B17C
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			bool flag = this.LeftArrowImage != null;
			if (flag)
			{
				e.Graphics.DrawImage(this.LeftArrowImage, new Rectangle(new Point(0, 0), new Size(this.LeftArrowImage.Width, base.Height)));
			}
			Brush brush = new SolidBrush(this.moChannelColor);
			Brush brush2 = new SolidBrush(Color.FromArgb(255, 255, 255));
			e.Graphics.FillRectangle(brush2, new Rectangle(this.LeftArrowImage.Width, 0, base.Width - this.RightArrowImage.Width, 1));
			e.Graphics.FillRectangle(brush2, new Rectangle(this.LeftArrowImage.Width, base.Height - 1, base.Width - this.RightArrowImage.Width, base.Height));
			e.Graphics.FillRectangle(brush, new Rectangle(this.LeftArrowImage.Width, 1, base.Width - this.RightArrowImage.Width, base.Height - 2));
			int num = base.Width - (this.LeftArrowImage.Width + this.RightArrowImage.Width);
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
			float num4 = (num2 - (float)(this.ThumbMiddleImage.Width + this.ThumbRightImage.Width + this.ThumbRightImage.Width)) / 2f;
			int num5 = (int)num4;
			int num6 = this.moThumbRight;
			num6 += this.LeftArrowImage.Width;
			e.Graphics.DrawImage(this.ThumbLeftImage, new Rectangle(num6, 1, this.ThumbLeftImage.Width, base.Height - 2));
			num6 += this.ThumbLeftImage.Width;
			Rectangle rectangle = new Rectangle(num6, 1, num5, base.Height - 2);
			e.Graphics.DrawImage(this.ThumbLeftSpanImage, (float)num6, 1f, num4 * 2f, (float)base.Height - 2f);
			num6 += num5;
			e.Graphics.DrawImage(this.ThumbMiddleImage, new Rectangle(num6, 1, this.ThumbMiddleImage.Width, base.Height - 2));
			num6 += this.ThumbMiddleImage.Width;
			rectangle = new Rectangle(num6, 1, num5 * 2, base.Height - 2);
			e.Graphics.DrawImage(this.ThumbRightSpanImage, rectangle);
			num6 += num5;
			e.Graphics.DrawImage(this.ThumbRightImage, new Rectangle(num6, 1, num5, base.Height - 2));
			bool flag4 = this.RightArrowImage != null;
			if (flag4)
			{
				e.Graphics.DrawImage(this.RightArrowImage, new Rectangle(new Point(base.Width - this.RightArrowImage.Width, 0), new Size(this.RightArrowImage.Width, base.Height)));
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0003D2DC File Offset: 0x0003B4DC
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

		// Token: 0x06000A8D RID: 2701 RVA: 0x0003D310 File Offset: 0x0003B510
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Name = "CustomHScrollbar";
			base.MouseDown += this.CustomScrollbar_MouseDown;
			base.MouseMove += this.CustomScrollbar_MouseMove;
			base.MouseUp += this.CustomScrollbar_MouseUp;
			base.ResumeLayout(false);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0003D374 File Offset: 0x0003B574
		private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			Point point = base.PointToClient(Cursor.Position);
			int num = base.Width - (this.LeftArrowImage.Width + this.RightArrowImage.Width);
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
			int num4 = this.moThumbRight;
			num4 += this.LeftArrowImage.Width;
			Rectangle rectangle = new Rectangle(new Point(num4, 1), new Size(num3, this.ThumbMiddleImage.Height));
			bool flag3 = rectangle.Contains(point);
			if (flag3)
			{
				this.nClickPoint = point.Y - num4;
				this.moThumbDown = true;
			}
			Rectangle rectangle2 = new Rectangle(new Point(1, 0), new Size(this.LeftArrowImage.Width, this.LeftArrowImage.Height));
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
						bool flag7 = this.moThumbRight - this.SmallChange < 0;
						if (flag7)
						{
							this.moThumbRight = 0;
						}
						else
						{
							this.moThumbRight -= this.SmallChange;
						}
						float num7 = (float)this.moThumbRight / (float)num6;
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
			Rectangle rectangle3 = new Rectangle(new Point(this.LeftArrowImage.Width + num, 1), new Size(this.LeftArrowImage.Width, this.LeftArrowImage.Height));
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
						bool flag13 = this.moThumbRight + this.SmallChange > num10;
						if (flag13)
						{
							this.moThumbRight = num10;
						}
						else
						{
							this.moThumbRight += this.SmallChange;
						}
						float num11 = (float)this.moThumbRight / (float)num10;
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

		// Token: 0x06000A8F RID: 2703 RVA: 0x0003D68D File Offset: 0x0003B88D
		private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			this.moThumbDown = false;
			this.moThumbDragging = false;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		private void MoveThumb(int x)
		{
			int num = this.Maximum - this.Minimum;
			int num2 = base.Width - (this.LeftArrowImage.Width + this.RightArrowImage.Width);
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
					int num7 = x - (this.LeftArrowImage.Width + num5);
					bool flag5 = num7 < 0;
					if (flag5)
					{
						this.moThumbRight = 0;
					}
					else
					{
						bool flag6 = num7 > num6;
						if (flag6)
						{
							this.moThumbRight = num6;
						}
						else
						{
							this.moThumbRight = x - (this.LeftArrowImage.Width + num5);
						}
					}
					float num8 = (float)this.moThumbRight / (float)num6;
					float num9 = num8 * (float)(this.Maximum - this.LargeChange);
					this.moValue = (int)num9;
					Application.DoEvents();
					base.Invalidate();
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
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
				this.MoveThumb(e.X);
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

		// Token: 0x040005C8 RID: 1480
		protected Color moChannelColor = Color.Empty;

		// Token: 0x040005C9 RID: 1481
		protected Image moUpArrowImage = null;

		// Token: 0x040005CA RID: 1482
		protected Image moDownArrowImage = null;

		// Token: 0x040005CB RID: 1483
		protected Image moThumbArrowImage = null;

		// Token: 0x040005CC RID: 1484
		protected Image moThumbRightImage = null;

		// Token: 0x040005CD RID: 1485
		protected Image moThumbRightSpanImage = null;

		// Token: 0x040005CE RID: 1486
		protected Image moThumbLeftImage = null;

		// Token: 0x040005CF RID: 1487
		protected Image moThumbLeftSpanImage = null;

		// Token: 0x040005D0 RID: 1488
		protected Image moThumbMiddleImage = null;

		// Token: 0x040005D1 RID: 1489
		protected int moLargeChange = 10;

		// Token: 0x040005D2 RID: 1490
		protected int moSmallChange = 1;

		// Token: 0x040005D3 RID: 1491
		protected int moMinimum = 0;

		// Token: 0x040005D4 RID: 1492
		protected int moMaximum = 100;

		// Token: 0x040005D5 RID: 1493
		protected int moValue = 0;

		// Token: 0x040005D6 RID: 1494
		private int nClickPoint;

		// Token: 0x040005D7 RID: 1495
		protected int moThumbRight = 0;

		// Token: 0x040005D8 RID: 1496
		protected bool moAutoSize = false;

		// Token: 0x040005D9 RID: 1497
		private bool moThumbDown = false;

		// Token: 0x040005DA RID: 1498
		private bool moThumbDragging = false;
	}
}
