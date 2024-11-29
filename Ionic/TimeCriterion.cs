using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000008 RID: 8
	internal class TimeCriterion : SelectionCriterion
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002318 File Offset: 0x00000518
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Which.ToString()).Append(" ").Append(EnumUtil.GetDescription(this.Operator))
				.Append(" ")
				.Append(this.Time.ToString("yyyy-MM-dd-HH:mm:ss"));
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000238C File Offset: 0x0000058C
		internal override bool Evaluate(string filename)
		{
			DateTime dateTime;
			switch (this.Which)
			{
			case WhichTime.atime:
				dateTime = File.GetLastAccessTime(filename).ToUniversalTime();
				break;
			case WhichTime.mtime:
				dateTime = File.GetLastWriteTime(filename).ToUniversalTime();
				break;
			case WhichTime.ctime:
				dateTime = File.GetCreationTime(filename).ToUniversalTime();
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return this._Evaluate(dateTime);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002400 File Offset: 0x00000600
		private bool _Evaluate(DateTime x)
		{
			bool flag;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				flag = x > this.Time;
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				flag = x >= this.Time;
				break;
			case ComparisonOperator.LesserThan:
				flag = x < this.Time;
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				flag = x <= this.Time;
				break;
			case ComparisonOperator.EqualTo:
				flag = x == this.Time;
				break;
			case ComparisonOperator.NotEqualTo:
				flag = x != this.Time;
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return flag;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A4 File Offset: 0x000006A4
		internal override bool Evaluate(ZipEntry entry)
		{
			DateTime dateTime;
			switch (this.Which)
			{
			case WhichTime.atime:
				dateTime = entry.AccessedTime;
				break;
			case WhichTime.mtime:
				dateTime = entry.ModifiedTime;
				break;
			case WhichTime.ctime:
				dateTime = entry.CreationTime;
				break;
			default:
				throw new ArgumentException("??time");
			}
			return this._Evaluate(dateTime);
		}

		// Token: 0x04000017 RID: 23
		internal ComparisonOperator Operator;

		// Token: 0x04000018 RID: 24
		internal WhichTime Which;

		// Token: 0x04000019 RID: 25
		internal DateTime Time;
	}
}
