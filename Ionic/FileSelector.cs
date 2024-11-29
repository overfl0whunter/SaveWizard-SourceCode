using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200000D RID: 13
	public class FileSelector
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002E37 File Offset: 0x00001037
		public FileSelector(string selectionCriteria)
			: this(selectionCriteria, true)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E44 File Offset: 0x00001044
		public FileSelector(string selectionCriteria, bool traverseDirectoryReparsePoints)
		{
			bool flag = !string.IsNullOrEmpty(selectionCriteria);
			if (flag)
			{
				this._Criterion = FileSelector._ParseCriterion(selectionCriteria);
			}
			this.TraverseReparsePoints = traverseDirectoryReparsePoints;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002E7C File Offset: 0x0000107C
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002EAC File Offset: 0x000010AC
		public string SelectionCriteria
		{
			get
			{
				bool flag = this._Criterion == null;
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = this._Criterion.ToString();
				}
				return text;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this._Criterion = null;
				}
				else
				{
					bool flag2 = value.Trim() == "";
					if (flag2)
					{
						this._Criterion = null;
					}
					else
					{
						this._Criterion = FileSelector._ParseCriterion(value);
					}
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002EF4 File Offset: 0x000010F4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool TraverseReparsePoints { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002F08 File Offset: 0x00001108
		private static string NormalizeCriteriaExpression(string source)
		{
			string[][] array = new string[][]
			{
				new string[] { "([^']*)\\(\\(([^']+)", "$1( ($2" },
				new string[] { "(.)\\)\\)", "$1) )" },
				new string[] { "\\((\\S)", "( $1" },
				new string[] { "(\\S)\\)", "$1 )" },
				new string[] { "^\\)", " )" },
				new string[] { "(\\S)\\(", "$1 (" },
				new string[] { "\\)(\\S)", ") $1" },
				new string[] { "(=)('[^']*')", "$1 $2" },
				new string[] { "([^ !><])(>|<|!=|=)", "$1 $2" },
				new string[] { "(>|<|!=|=)([^ =])", "$1 $2" },
				new string[] { "/", "\\" }
			};
			string text = source;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = FileSelector.RegexAssertions.PrecededByEvenNumberOfSingleQuotes + array[i][0] + FileSelector.RegexAssertions.FollowedByEvenNumberOfSingleQuotesAndLineEnd;
				text = Regex.Replace(text, text2, array[i][1]);
			}
			string text3 = "/" + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			text = Regex.Replace(text, text3, "\\");
			text3 = " " + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			return Regex.Replace(text, text3, "\u0006");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030B0 File Offset: 0x000012B0
		private static SelectionCriterion _ParseCriterion(string s)
		{
			bool flag = s == null;
			SelectionCriterion selectionCriterion;
			if (flag)
			{
				selectionCriterion = null;
			}
			else
			{
				s = FileSelector.NormalizeCriteriaExpression(s);
				bool flag2 = s.IndexOf(" ") == -1;
				if (flag2)
				{
					s = "name = " + s;
				}
				string[] array = s.Trim().Split(new char[] { ' ', '\t' });
				bool flag3 = array.Length < 3;
				if (flag3)
				{
					throw new ArgumentException(s);
				}
				SelectionCriterion selectionCriterion2 = null;
				Stack<FileSelector.ParseState> stack = new Stack<FileSelector.ParseState>();
				Stack<SelectionCriterion> stack2 = new Stack<SelectionCriterion>();
				stack.Push(FileSelector.ParseState.Start);
				int i = 0;
				while (i < array.Length)
				{
					string text = array[i].ToLower();
					string text2 = text;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
					FileSelector.ParseState parseState;
					if (num <= 1563699588U)
					{
						if (num <= 739023492U)
						{
							if (num <= 329706515U)
							{
								if (num != 254395046U)
								{
									if (num != 329706515U)
									{
										goto IL_09B3;
									}
									if (!(text2 == "ctime"))
									{
										goto IL_09B3;
									}
									goto IL_04AC;
								}
								else
								{
									if (!(text2 == "and"))
									{
										goto IL_09B3;
									}
									goto IL_033D;
								}
							}
							else if (num != 597743964U)
							{
								if (num != 739023492U)
								{
									goto IL_09B3;
								}
								if (!(text2 == ")"))
								{
									goto IL_09B3;
								}
								parseState = stack.Pop();
								bool flag4 = stack.Peek() != FileSelector.ParseState.OpenParen;
								if (flag4)
								{
									throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
								}
								stack.Pop();
								stack.Push(FileSelector.ParseState.CriterionDone);
							}
							else
							{
								if (!(text2 == "size"))
								{
									goto IL_09B3;
								}
								goto IL_05D6;
							}
						}
						else if (num <= 1058081160U)
						{
							if (num != 755801111U)
							{
								if (num != 1058081160U)
								{
									goto IL_09B3;
								}
								if (!(text2 == "filename"))
								{
									goto IL_09B3;
								}
								goto IL_07E8;
							}
							else
							{
								if (!(text2 == "("))
								{
									goto IL_09B3;
								}
								parseState = stack.Peek();
								bool flag5 = parseState != FileSelector.ParseState.Start && parseState != FileSelector.ParseState.ConjunctionPending && parseState != FileSelector.ParseState.OpenParen;
								if (flag5)
								{
									throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
								}
								bool flag6 = array.Length <= i + 4;
								if (flag6)
								{
									throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
								}
								stack.Push(FileSelector.ParseState.OpenParen);
							}
						}
						else if (num != 1361572173U)
						{
							if (num != 1563699588U)
							{
								goto IL_09B3;
							}
							if (!(text2 == "or"))
							{
								goto IL_09B3;
							}
							goto IL_033D;
						}
						else
						{
							if (!(text2 == "type"))
							{
								goto IL_09B3;
							}
							goto IL_08DA;
						}
					}
					else if (num <= 2746858573U)
					{
						if (num <= 2211460629U)
						{
							if (num != 2166136261U)
							{
								if (num != 2211460629U)
								{
									goto IL_09B3;
								}
								if (!(text2 == "length"))
								{
									goto IL_09B3;
								}
								goto IL_05D6;
							}
							else
							{
								if (text2 == null || text2.Length != 0)
								{
									goto IL_09B3;
								}
								stack.Push(FileSelector.ParseState.Whitespace);
							}
						}
						else if (num != 2369371622U)
						{
							if (num != 2746858573U)
							{
								goto IL_09B3;
							}
							if (!(text2 == "atime"))
							{
								goto IL_09B3;
							}
							goto IL_04AC;
						}
						else
						{
							if (!(text2 == "name"))
							{
								goto IL_09B3;
							}
							goto IL_07E8;
						}
					}
					else if (num <= 3429620606U)
					{
						if (num != 2888110417U)
						{
							if (num != 3429620606U)
							{
								goto IL_09B3;
							}
							if (!(text2 == "xor"))
							{
								goto IL_09B3;
							}
							goto IL_033D;
						}
						else
						{
							if (!(text2 == "mtime"))
							{
								goto IL_09B3;
							}
							goto IL_04AC;
						}
					}
					else if (num != 3791641492U)
					{
						if (num != 4191246291U)
						{
							goto IL_09B3;
						}
						if (!(text2 == "attrs"))
						{
							goto IL_09B3;
						}
						goto IL_08DA;
					}
					else
					{
						if (!(text2 == "attributes"))
						{
							goto IL_09B3;
						}
						goto IL_08DA;
					}
					IL_09CC:
					parseState = stack.Peek();
					bool flag7 = parseState == FileSelector.ParseState.CriterionDone;
					if (flag7)
					{
						stack.Pop();
						bool flag8 = stack.Peek() == FileSelector.ParseState.ConjunctionPending;
						if (flag8)
						{
							while (stack.Peek() == FileSelector.ParseState.ConjunctionPending)
							{
								CompoundCriterion compoundCriterion = stack2.Pop() as CompoundCriterion;
								compoundCriterion.Right = selectionCriterion2;
								selectionCriterion2 = compoundCriterion;
								stack.Pop();
								parseState = stack.Pop();
								bool flag9 = parseState != FileSelector.ParseState.CriterionDone;
								if (flag9)
								{
									throw new ArgumentException("??");
								}
							}
						}
						else
						{
							stack.Push(FileSelector.ParseState.CriterionDone);
						}
					}
					bool flag10 = parseState == FileSelector.ParseState.Whitespace;
					if (flag10)
					{
						stack.Pop();
					}
					i++;
					continue;
					IL_033D:
					parseState = stack.Peek();
					bool flag11 = parseState != FileSelector.ParseState.CriterionDone;
					if (flag11)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					bool flag12 = array.Length <= i + 3;
					if (flag12)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					LogicalConjunction logicalConjunction = (LogicalConjunction)Enum.Parse(typeof(LogicalConjunction), array[i].ToUpper(), true);
					selectionCriterion2 = new CompoundCriterion
					{
						Left = selectionCriterion2,
						Right = null,
						Conjunction = logicalConjunction
					};
					stack.Push(parseState);
					stack.Push(FileSelector.ParseState.ConjunctionPending);
					stack2.Push(selectionCriterion2);
					goto IL_09CC;
					IL_04AC:
					bool flag13 = array.Length <= i + 2;
					if (flag13)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					DateTime dateTime;
					try
					{
						dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd-HH:mm:ss", null);
					}
					catch (FormatException)
					{
						try
						{
							dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd-HH:mm:ss", null);
						}
						catch (FormatException)
						{
							try
							{
								dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd", null);
							}
							catch (FormatException)
							{
								try
								{
									dateTime = DateTime.ParseExact(array[i + 2], "MM/dd/yyyy", null);
								}
								catch (FormatException)
								{
									dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd", null);
								}
							}
						}
					}
					dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
					selectionCriterion2 = new TimeCriterion
					{
						Which = (WhichTime)Enum.Parse(typeof(WhichTime), array[i], true),
						Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]),
						Time = dateTime
					};
					i += 2;
					stack.Push(FileSelector.ParseState.CriterionDone);
					goto IL_09CC;
					IL_05D6:
					bool flag14 = array.Length <= i + 2;
					if (flag14)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					string text3 = array[i + 2];
					bool flag15 = text3.ToUpper().EndsWith("K");
					long num2;
					if (flag15)
					{
						num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L;
					}
					else
					{
						bool flag16 = text3.ToUpper().EndsWith("KB");
						if (flag16)
						{
							num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L;
						}
						else
						{
							bool flag17 = text3.ToUpper().EndsWith("M");
							if (flag17)
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L;
							}
							else
							{
								bool flag18 = text3.ToUpper().EndsWith("MB");
								if (flag18)
								{
									num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L;
								}
								else
								{
									bool flag19 = text3.ToUpper().EndsWith("G");
									if (flag19)
									{
										num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L * 1024L;
									}
									else
									{
										bool flag20 = text3.ToUpper().EndsWith("GB");
										if (flag20)
										{
											num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L * 1024L;
										}
										else
										{
											num2 = long.Parse(array[i + 2]);
										}
									}
								}
							}
						}
					}
					selectionCriterion2 = new SizeCriterion
					{
						Size = num2,
						Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1])
					};
					i += 2;
					stack.Push(FileSelector.ParseState.CriterionDone);
					goto IL_09CC;
					IL_07E8:
					bool flag21 = array.Length <= i + 2;
					if (flag21)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					ComparisonOperator comparisonOperator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					bool flag22 = comparisonOperator != ComparisonOperator.NotEqualTo && comparisonOperator != ComparisonOperator.EqualTo;
					if (flag22)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					string text4 = array[i + 2];
					bool flag23 = text4.StartsWith("'") && text4.EndsWith("'");
					if (flag23)
					{
						text4 = text4.Substring(1, text4.Length - 2).Replace("\u0006", " ");
					}
					selectionCriterion2 = new NameCriterion
					{
						MatchingFileSpec = text4,
						Operator = comparisonOperator
					};
					i += 2;
					stack.Push(FileSelector.ParseState.CriterionDone);
					goto IL_09CC;
					IL_08DA:
					bool flag24 = array.Length <= i + 2;
					if (flag24)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					ComparisonOperator comparisonOperator2 = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					bool flag25 = comparisonOperator2 != ComparisonOperator.NotEqualTo && comparisonOperator2 != ComparisonOperator.EqualTo;
					if (flag25)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					SelectionCriterion selectionCriterion3;
					if (!(text == "type"))
					{
						AttributesCriterion attributesCriterion = new AttributesCriterion();
						attributesCriterion.AttributeString = array[i + 2];
						selectionCriterion3 = attributesCriterion;
						attributesCriterion.Operator = comparisonOperator2;
					}
					else
					{
						TypeCriterion typeCriterion = new TypeCriterion();
						typeCriterion.AttributeString = array[i + 2];
						selectionCriterion3 = typeCriterion;
						typeCriterion.Operator = comparisonOperator2;
					}
					selectionCriterion2 = selectionCriterion3;
					i += 2;
					stack.Push(FileSelector.ParseState.CriterionDone);
					goto IL_09CC;
					IL_09B3:
					throw new ArgumentException("'" + array[i] + "'");
				}
				selectionCriterion = selectionCriterion2;
			}
			return selectionCriterion;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003B7C File Offset: 0x00001D7C
		public override string ToString()
		{
			return "FileSelector(" + this._Criterion.ToString() + ")";
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private bool Evaluate(string filename)
		{
			return this._Criterion.Evaluate(filename);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003BC8 File Offset: 0x00001DC8
		[Conditional("SelectorTrace")]
		private void SelectorTrace(string format, params object[] args)
		{
			bool flag = this._Criterion != null && this._Criterion.Verbose;
			if (flag)
			{
				Console.WriteLine(format, args);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public ICollection<string> SelectFiles(string directory)
		{
			return this.SelectFiles(directory, false);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C14 File Offset: 0x00001E14
		public ReadOnlyCollection<string> SelectFiles(string directory, bool recurseDirectories)
		{
			bool flag = this._Criterion == null;
			if (flag)
			{
				throw new ArgumentException("SelectionCriteria has not been set");
			}
			List<string> list = new List<string>();
			try
			{
				bool flag2 = Directory.Exists(directory);
				if (flag2)
				{
					string[] files = Directory.GetFiles(directory);
					foreach (string text in files)
					{
						bool flag3 = this.Evaluate(text);
						if (flag3)
						{
							list.Add(text);
						}
					}
					if (recurseDirectories)
					{
						string[] directories = Directory.GetDirectories(directory);
						foreach (string text2 in directories)
						{
							bool flag4 = this.TraverseReparsePoints || (File.GetAttributes(text2) & FileAttributes.ReparsePoint) == (FileAttributes)0;
							if (flag4)
							{
								bool flag5 = this.Evaluate(text2);
								if (flag5)
								{
									list.Add(text2);
								}
								list.AddRange(this.SelectFiles(text2, recurseDirectories));
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003D40 File Offset: 0x00001F40
		private bool Evaluate(ZipEntry entry)
		{
			return this._Criterion.Evaluate(entry);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003D60 File Offset: 0x00001F60
		public ICollection<ZipEntry> SelectEntries(ZipFile zip)
		{
			bool flag = zip == null;
			if (flag)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			foreach (ZipEntry zipEntry in zip)
			{
				bool flag2 = this.Evaluate(zipEntry);
				if (flag2)
				{
					list.Add(zipEntry);
				}
			}
			return list;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003DDC File Offset: 0x00001FDC
		public ICollection<ZipEntry> SelectEntries(ZipFile zip, string directoryPathInArchive)
		{
			bool flag = zip == null;
			if (flag)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			string text = ((directoryPathInArchive == null) ? null : directoryPathInArchive.Replace("/", "\\"));
			bool flag2 = text != null;
			if (flag2)
			{
				while (text.EndsWith("\\"))
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			foreach (ZipEntry zipEntry in zip)
			{
				bool flag3 = directoryPathInArchive == null || Path.GetDirectoryName(zipEntry.FileName) == directoryPathInArchive || Path.GetDirectoryName(zipEntry.FileName) == text;
				if (flag3)
				{
					bool flag4 = this.Evaluate(zipEntry);
					if (flag4)
					{
						list.Add(zipEntry);
					}
				}
			}
			return list;
		}

		// Token: 0x04000025 RID: 37
		internal SelectionCriterion _Criterion;

		// Token: 0x020001F8 RID: 504
		private enum ParseState
		{
			// Token: 0x04000D50 RID: 3408
			Start,
			// Token: 0x04000D51 RID: 3409
			OpenParen,
			// Token: 0x04000D52 RID: 3410
			CriterionDone,
			// Token: 0x04000D53 RID: 3411
			ConjunctionPending,
			// Token: 0x04000D54 RID: 3412
			Whitespace
		}

		// Token: 0x020001F9 RID: 505
		private static class RegexAssertions
		{
			// Token: 0x04000D55 RID: 3413
			public static readonly string PrecededByOddNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*'[^']*)";

			// Token: 0x04000D56 RID: 3414
			public static readonly string FollowedByOddNumberOfSingleQuotesAndLineEnd = "(?=[^']*'(?:[^']*'[^']*')*[^']*$)";

			// Token: 0x04000D57 RID: 3415
			public static readonly string PrecededByEvenNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*[^']*)";

			// Token: 0x04000D58 RID: 3416
			public static readonly string FollowedByEvenNumberOfSingleQuotesAndLineEnd = "(?=(?:[^']*'[^']*')*[^']*$)";
		}
	}
}
