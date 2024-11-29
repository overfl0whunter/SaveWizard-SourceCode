using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000009 RID: 9
	internal class NameCriterion : SelectionCriterion
	{
		// Token: 0x17000004 RID: 4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002500 File Offset: 0x00000700
		internal virtual string MatchingFileSpec
		{
			set
			{
				bool flag = Directory.Exists(value);
				if (flag)
				{
					this._MatchingFileSpec = ".\\" + value + "\\*.*";
				}
				else
				{
					this._MatchingFileSpec = value;
				}
				this._regexString = "^" + Regex.Escape(this._MatchingFileSpec).Replace("\\\\\\*\\.\\*", "\\\\([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*")
					.Replace("\\*", ".*")
					.Replace("\\?", "[^\\\\\\.]") + "$";
				this._re = new Regex(this._regexString, RegexOptions.IgnoreCase);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025AC File Offset: 0x000007AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("name ").Append(EnumUtil.GetDescription(this.Operator)).Append(" '")
				.Append(this._MatchingFileSpec)
				.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000260C File Offset: 0x0000080C
		internal override bool Evaluate(string filename)
		{
			return this._Evaluate(filename);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002628 File Offset: 0x00000828
		private bool _Evaluate(string fullpath)
		{
			string text = ((this._MatchingFileSpec.IndexOf('\\') == -1) ? Path.GetFileName(fullpath) : fullpath);
			bool flag = this._re.IsMatch(text);
			bool flag2 = this.Operator != ComparisonOperator.EqualTo;
			if (flag2)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002678 File Offset: 0x00000878
		internal override bool Evaluate(ZipEntry entry)
		{
			string text = entry.FileName.Replace("/", "\\");
			return this._Evaluate(text);
		}

		// Token: 0x0400001A RID: 26
		private Regex _re;

		// Token: 0x0400001B RID: 27
		private string _regexString;

		// Token: 0x0400001C RID: 28
		internal ComparisonOperator Operator;

		// Token: 0x0400001D RID: 29
		private string _MatchingFileSpec;
	}
}
