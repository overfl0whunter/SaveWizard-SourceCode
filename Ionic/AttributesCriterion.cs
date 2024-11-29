using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200000B RID: 11
	internal class AttributesCriterion : SelectionCriterion
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027F8 File Offset: 0x000009F8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000028C4 File Offset: 0x00000AC4
		internal string AttributeString
		{
			get
			{
				string text = "";
				bool flag = (this._Attributes & FileAttributes.Hidden) > (FileAttributes)0;
				if (flag)
				{
					text += "H";
				}
				bool flag2 = (this._Attributes & FileAttributes.System) > (FileAttributes)0;
				if (flag2)
				{
					text += "S";
				}
				bool flag3 = (this._Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
				if (flag3)
				{
					text += "R";
				}
				bool flag4 = (this._Attributes & FileAttributes.Archive) > (FileAttributes)0;
				if (flag4)
				{
					text += "A";
				}
				bool flag5 = (this._Attributes & FileAttributes.ReparsePoint) > (FileAttributes)0;
				if (flag5)
				{
					text += "L";
				}
				bool flag6 = (this._Attributes & FileAttributes.NotContentIndexed) > (FileAttributes)0;
				if (flag6)
				{
					text += "I";
				}
				return text;
			}
			set
			{
				this._Attributes = FileAttributes.Normal;
				string text = value.ToUpper();
				int i = 0;
				while (i < text.Length)
				{
					char c = text[i];
					char c2 = c;
					if (c2 <= 'L')
					{
						if (c2 != 'A')
						{
							switch (c2)
							{
							case 'H':
							{
								bool flag = (this._Attributes & FileAttributes.Hidden) > (FileAttributes)0;
								if (flag)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.Hidden;
								break;
							}
							case 'I':
							{
								bool flag2 = (this._Attributes & FileAttributes.NotContentIndexed) > (FileAttributes)0;
								if (flag2)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.NotContentIndexed;
								break;
							}
							case 'J':
							case 'K':
								goto IL_01F1;
							case 'L':
							{
								bool flag3 = (this._Attributes & FileAttributes.ReparsePoint) > (FileAttributes)0;
								if (flag3)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.ReparsePoint;
								break;
							}
							default:
								goto IL_01F1;
							}
						}
						else
						{
							bool flag4 = (this._Attributes & FileAttributes.Archive) > (FileAttributes)0;
							if (flag4)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.Archive;
						}
					}
					else if (c2 != 'R')
					{
						if (c2 != 'S')
						{
							goto IL_01F1;
						}
						bool flag5 = (this._Attributes & FileAttributes.System) > (FileAttributes)0;
						if (flag5)
						{
							throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
						}
						this._Attributes |= FileAttributes.System;
					}
					else
					{
						bool flag6 = (this._Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
						if (flag6)
						{
							throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
						}
						this._Attributes |= FileAttributes.ReadOnly;
					}
					i++;
					continue;
					IL_01F1:
					throw new ArgumentException(value);
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002ADC File Offset: 0x00000CDC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("attributes ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ")
				.Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B30 File Offset: 0x00000D30
		private bool _EvaluateOne(FileAttributes fileAttrs, FileAttributes criterionAttrs)
		{
			bool flag = (this._Attributes & criterionAttrs) == criterionAttrs;
			return !flag || (fileAttrs & criterionAttrs) == criterionAttrs;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B60 File Offset: 0x00000D60
		internal override bool Evaluate(string filename)
		{
			bool flag = Directory.Exists(filename);
			bool flag2;
			if (flag)
			{
				flag2 = this.Operator != ComparisonOperator.EqualTo;
			}
			else
			{
				FileAttributes attributes = File.GetAttributes(filename);
				flag2 = this._Evaluate(attributes);
			}
			return flag2;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B9C File Offset: 0x00000D9C
		private bool _Evaluate(FileAttributes fileAttrs)
		{
			bool flag = this._EvaluateOne(fileAttrs, FileAttributes.Hidden);
			bool flag2 = flag;
			if (flag2)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.System);
			}
			bool flag3 = flag;
			if (flag3)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReadOnly);
			}
			bool flag4 = flag;
			if (flag4)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.Archive);
			}
			bool flag5 = flag;
			if (flag5)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.NotContentIndexed);
			}
			bool flag6 = flag;
			if (flag6)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReparsePoint);
			}
			bool flag7 = this.Operator != ComparisonOperator.EqualTo;
			if (flag7)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C24 File Offset: 0x00000E24
		internal override bool Evaluate(ZipEntry entry)
		{
			FileAttributes attributes = entry.Attributes;
			return this._Evaluate(attributes);
		}

		// Token: 0x04000020 RID: 32
		private FileAttributes _Attributes;

		// Token: 0x04000021 RID: 33
		internal ComparisonOperator Operator;
	}
}
