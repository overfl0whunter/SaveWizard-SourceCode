using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Be.Windows.Forms
{
	// Token: 0x020000D4 RID: 212
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class BuiltInContextMenu : Component
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x000358D4 File Offset: 0x00033AD4
		internal BuiltInContextMenu(HexBox hexBox)
		{
			this._hexBox = hexBox;
			this._hexBox.ByteProviderChanged += this.HexBox_ByteProviderChanged;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0003592B File Offset: 0x00033B2B
		private void HexBox_ByteProviderChanged(object sender, EventArgs e)
		{
			this.CheckBuiltInContextMenu();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00035938 File Offset: 0x00033B38
		private void CheckBuiltInContextMenu()
		{
			bool designMode = Util.DesignMode;
			if (!designMode)
			{
				bool flag = this._contextMenuStrip == null;
				if (flag)
				{
					ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
					this._cutToolStripMenuItem = new ToolStripMenuItem(this.CutMenuItemTextInternal, this.CutMenuItemImage, new EventHandler(this.CutMenuItem_Click));
					contextMenuStrip.Items.Add(this._cutToolStripMenuItem);
					this._copyToolStripMenuItem = new ToolStripMenuItem(this.CopyMenuItemTextInternal, this.CopyMenuItemImage, new EventHandler(this.CopyMenuItem_Click));
					contextMenuStrip.Items.Add(this._copyToolStripMenuItem);
					this._pasteToolStripMenuItem = new ToolStripMenuItem(this.PasteMenuItemTextInternal, this.PasteMenuItemImage, new EventHandler(this.PasteMenuItem_Click));
					contextMenuStrip.Items.Add(this._pasteToolStripMenuItem);
					contextMenuStrip.Items.Add(new ToolStripSeparator());
					this._selectAllToolStripMenuItem = new ToolStripMenuItem(this.SelectAllMenuItemTextInternal, this.SelectAllMenuItemImage, new EventHandler(this.SelectAllMenuItem_Click));
					contextMenuStrip.Items.Add(this._selectAllToolStripMenuItem);
					contextMenuStrip.Opening += this.BuildInContextMenuStrip_Opening;
					this._contextMenuStrip = contextMenuStrip;
				}
				bool flag2 = this._hexBox.ByteProvider == null && this._hexBox.ContextMenuStrip != null;
				if (flag2)
				{
					this._hexBox.ContextMenuStrip = null;
				}
				else
				{
					bool flag3 = this._hexBox.ByteProvider != null && this._hexBox.ContextMenuStrip == null;
					if (flag3)
					{
						this._hexBox.ContextMenuStrip = this._contextMenuStrip;
					}
				}
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00035AD4 File Offset: 0x00033CD4
		private void BuildInContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			this._cutToolStripMenuItem.Enabled = this._hexBox.CanCut();
			this._copyToolStripMenuItem.Enabled = this._hexBox.CanCopy();
			this._pasteToolStripMenuItem.Enabled = this._hexBox.CanPaste();
			this._selectAllToolStripMenuItem.Enabled = this._hexBox.CanSelectAll();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00035B3E File Offset: 0x00033D3E
		private void CutMenuItem_Click(object sender, EventArgs e)
		{
			this._hexBox.Copy();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00035B3E File Offset: 0x00033D3E
		private void CopyMenuItem_Click(object sender, EventArgs e)
		{
			this._hexBox.Copy();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00035B3E File Offset: 0x00033D3E
		private void PasteMenuItem_Click(object sender, EventArgs e)
		{
			this._hexBox.Copy();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00035B4D File Offset: 0x00033D4D
		private void SelectAllMenuItem_Click(object sender, EventArgs e)
		{
			this._hexBox.SelectAll();
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00035B5C File Offset: 0x00033D5C
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00035B74 File Offset: 0x00033D74
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string CopyMenuItemText
		{
			get
			{
				return this._copyMenuItemText;
			}
			set
			{
				this._copyMenuItemText = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00035B80 File Offset: 0x00033D80
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00035B98 File Offset: 0x00033D98
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string CutMenuItemText
		{
			get
			{
				return this._cutMenuItemText;
			}
			set
			{
				this._cutMenuItemText = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00035BA4 File Offset: 0x00033DA4
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00035BBC File Offset: 0x00033DBC
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string PasteMenuItemText
		{
			get
			{
				return this._pasteMenuItemText;
			}
			set
			{
				this._pasteMenuItemText = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00035BC8 File Offset: 0x00033DC8
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00035BE0 File Offset: 0x00033DE0
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string SelectAllMenuItemText
		{
			get
			{
				return this._selectAllMenuItemText;
			}
			set
			{
				this._selectAllMenuItemText = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00035BEC File Offset: 0x00033DEC
		internal string CutMenuItemTextInternal
		{
			get
			{
				return (!string.IsNullOrEmpty(this.CutMenuItemText)) ? this.CutMenuItemText : "Cut";
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00035C18 File Offset: 0x00033E18
		internal string CopyMenuItemTextInternal
		{
			get
			{
				return (!string.IsNullOrEmpty(this.CopyMenuItemText)) ? this.CopyMenuItemText : "Copy";
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00035C44 File Offset: 0x00033E44
		internal string PasteMenuItemTextInternal
		{
			get
			{
				return (!string.IsNullOrEmpty(this.PasteMenuItemText)) ? this.PasteMenuItemText : "Paste";
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00035C70 File Offset: 0x00033E70
		internal string SelectAllMenuItemTextInternal
		{
			get
			{
				return (!string.IsNullOrEmpty(this.SelectAllMenuItemText)) ? this.SelectAllMenuItemText : "SelectAll";
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00035C9C File Offset: 0x00033E9C
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00035CB4 File Offset: 0x00033EB4
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		public Image CutMenuItemImage
		{
			get
			{
				return this._cutMenuItemImage;
			}
			set
			{
				this._cutMenuItemImage = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00035CC0 File Offset: 0x00033EC0
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x00035CD8 File Offset: 0x00033ED8
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		public Image CopyMenuItemImage
		{
			get
			{
				return this._copyMenuItemImage;
			}
			set
			{
				this._copyMenuItemImage = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00035CE4 File Offset: 0x00033EE4
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00035CFC File Offset: 0x00033EFC
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		public Image PasteMenuItemImage
		{
			get
			{
				return this._pasteMenuItemImage;
			}
			set
			{
				this._pasteMenuItemImage = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00035D08 File Offset: 0x00033F08
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00035D20 File Offset: 0x00033F20
		[Category("BuiltIn-ContextMenu")]
		[DefaultValue(null)]
		public Image SelectAllMenuItemImage
		{
			get
			{
				return this._selectAllMenuItemImage;
			}
			set
			{
				this._selectAllMenuItemImage = value;
			}
		}

		// Token: 0x04000536 RID: 1334
		private HexBox _hexBox;

		// Token: 0x04000537 RID: 1335
		private ContextMenuStrip _contextMenuStrip;

		// Token: 0x04000538 RID: 1336
		private ToolStripMenuItem _cutToolStripMenuItem;

		// Token: 0x04000539 RID: 1337
		private ToolStripMenuItem _copyToolStripMenuItem;

		// Token: 0x0400053A RID: 1338
		private ToolStripMenuItem _pasteToolStripMenuItem;

		// Token: 0x0400053B RID: 1339
		private ToolStripMenuItem _selectAllToolStripMenuItem;

		// Token: 0x0400053C RID: 1340
		private string _copyMenuItemText;

		// Token: 0x0400053D RID: 1341
		private string _cutMenuItemText;

		// Token: 0x0400053E RID: 1342
		private string _pasteMenuItemText;

		// Token: 0x0400053F RID: 1343
		private string _selectAllMenuItemText = null;

		// Token: 0x04000540 RID: 1344
		private Image _cutMenuItemImage = null;

		// Token: 0x04000541 RID: 1345
		private Image _copyMenuItemImage = null;

		// Token: 0x04000542 RID: 1346
		private Image _pasteMenuItemImage = null;

		// Token: 0x04000543 RID: 1347
		private Image _selectAllMenuItemImage = null;
	}
}
