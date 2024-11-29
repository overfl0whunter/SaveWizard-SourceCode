using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A4 RID: 164
	public class NameFilter : IScanFilter
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0002ED94 File Offset: 0x0002CF94
		public NameFilter(string filter)
		{
			this.filter_ = filter;
			this.inclusions_ = new ArrayList();
			this.exclusions_ = new ArrayList();
			this.Compile();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
		public static bool IsValidExpression(string expression)
		{
			bool flag = true;
			try
			{
				Regex regex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002EE00 File Offset: 0x0002D000
		public static bool IsValidFilterExpression(string toTest)
		{
			bool flag = toTest == null;
			if (flag)
			{
				throw new ArgumentNullException("toTest");
			}
			bool flag2 = true;
			try
			{
				string[] array = NameFilter.SplitQuoted(toTest);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = array[i] != null && array[i].Length > 0;
					if (flag3)
					{
						bool flag4 = array[i][0] == '+';
						string text;
						if (flag4)
						{
							text = array[i].Substring(1, array[i].Length - 1);
						}
						else
						{
							bool flag5 = array[i][0] == '-';
							if (flag5)
							{
								text = array[i].Substring(1, array[i].Length - 1);
							}
							else
							{
								text = array[i];
							}
						}
						Regex regex = new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
					}
				}
			}
			catch (ArgumentException)
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		public static string[] SplitQuoted(string original)
		{
			char c = '\\';
			char[] array = new char[] { ';' };
			ArrayList arrayList = new ArrayList();
			bool flag = original != null && original.Length > 0;
			if (flag)
			{
				int i = -1;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < original.Length)
				{
					i++;
					bool flag2 = i >= original.Length;
					if (flag2)
					{
						arrayList.Add(stringBuilder.ToString());
					}
					else
					{
						bool flag3 = original[i] == c;
						if (flag3)
						{
							i++;
							bool flag4 = i >= original.Length;
							if (flag4)
							{
								throw new ArgumentException("Missing terminating escape character", "original");
							}
							bool flag5 = Array.IndexOf<char>(array, original[i]) < 0;
							if (flag5)
							{
								stringBuilder.Append(c);
							}
							stringBuilder.Append(original[i]);
						}
						else
						{
							bool flag6 = Array.IndexOf<char>(array, original[i]) >= 0;
							if (flag6)
							{
								arrayList.Add(stringBuilder.ToString());
								stringBuilder.Length = 0;
							}
							else
							{
								stringBuilder.Append(original[i]);
							}
						}
					}
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0002F050 File Offset: 0x0002D250
		public override string ToString()
		{
			return this.filter_;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0002F068 File Offset: 0x0002D268
		public bool IsIncluded(string name)
		{
			bool flag = false;
			bool flag2 = this.inclusions_.Count == 0;
			if (flag2)
			{
				flag = true;
			}
			else
			{
				foreach (object obj in this.inclusions_)
				{
					Regex regex = (Regex)obj;
					bool flag3 = regex.IsMatch(name);
					if (flag3)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		public bool IsExcluded(string name)
		{
			bool flag = false;
			foreach (object obj in this.exclusions_)
			{
				Regex regex = (Regex)obj;
				bool flag2 = regex.IsMatch(name);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0002F16C File Offset: 0x0002D36C
		public bool IsMatch(string name)
		{
			return this.IsIncluded(name) && !this.IsExcluded(name);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0002F194 File Offset: 0x0002D394
		private void Compile()
		{
			bool flag = this.filter_ == null;
			if (!flag)
			{
				string[] array = NameFilter.SplitQuoted(this.filter_);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = array[i] != null && array[i].Length > 0;
					if (flag2)
					{
						bool flag3 = array[i][0] != '-';
						bool flag4 = array[i][0] == '+';
						string text;
						if (flag4)
						{
							text = array[i].Substring(1, array[i].Length - 1);
						}
						else
						{
							bool flag5 = array[i][0] == '-';
							if (flag5)
							{
								text = array[i].Substring(1, array[i].Length - 1);
							}
							else
							{
								text = array[i];
							}
						}
						bool flag6 = flag3;
						if (flag6)
						{
							this.inclusions_.Add(new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
						}
						else
						{
							this.exclusions_.Add(new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
						}
					}
				}
			}
		}

		// Token: 0x040004A8 RID: 1192
		private string filter_;

		// Token: 0x040004A9 RID: 1193
		private ArrayList inclusions_;

		// Token: 0x040004AA RID: 1194
		private ArrayList exclusions_;
	}
}
