using System;

namespace Rss
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class RssModuleItem : RssElement
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x000319FE File Offset: 0x0002FBFE
		public RssModuleItem()
		{
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00031A30 File Offset: 0x0002FC30
		public RssModuleItem(string name)
		{
			this._sElementName = RssDefault.Check(name);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00031A6E File Offset: 0x0002FC6E
		public RssModuleItem(string name, bool required)
			: this(name)
		{
			this._bRequired = required;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00031A80 File Offset: 0x0002FC80
		public RssModuleItem(string name, string text)
			: this(name)
		{
			this._sElementText = RssDefault.Check(text);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00031A97 File Offset: 0x0002FC97
		public RssModuleItem(string name, bool required, string text)
			: this(name, required)
		{
			this._sElementText = RssDefault.Check(text);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00031AAF File Offset: 0x0002FCAF
		public RssModuleItem(string name, string text, RssModuleItemCollection subElements)
			: this(name, text)
		{
			this._rssSubElements = subElements;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00031AC2 File Offset: 0x0002FCC2
		public RssModuleItem(string name, bool required, string text, RssModuleItemCollection subElements)
			: this(name, required, text)
		{
			this._rssSubElements = subElements;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00031AD8 File Offset: 0x0002FCD8
		public override string ToString()
		{
			bool flag = this._sElementName != null;
			string text;
			if (flag)
			{
				text = this._sElementName;
			}
			else
			{
				bool flag2 = this._sElementText != null;
				if (flag2)
				{
					text = this._sElementText;
				}
				else
				{
					text = "RssModuleItem";
				}
			}
			return text;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00031B1C File Offset: 0x0002FD1C
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00031B34 File Offset: 0x0002FD34
		public string Name
		{
			get
			{
				return this._sElementName;
			}
			set
			{
				this._sElementName = RssDefault.Check(value);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00031B44 File Offset: 0x0002FD44
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00031B5C File Offset: 0x0002FD5C
		public string Text
		{
			get
			{
				return this._sElementText;
			}
			set
			{
				this._sElementText = RssDefault.Check(value);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00031B6C File Offset: 0x0002FD6C
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00031B84 File Offset: 0x0002FD84
		public RssModuleItemCollection SubElements
		{
			get
			{
				return this._rssSubElements;
			}
			set
			{
				this._rssSubElements = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00031B90 File Offset: 0x0002FD90
		public bool IsRequired
		{
			get
			{
				return this._bRequired;
			}
		}

		// Token: 0x04000500 RID: 1280
		private bool _bRequired = false;

		// Token: 0x04000501 RID: 1281
		private string _sElementName = "";

		// Token: 0x04000502 RID: 1282
		private string _sElementText = "";

		// Token: 0x04000503 RID: 1283
		private RssModuleItemCollection _rssSubElements = new RssModuleItemCollection();
	}
}
