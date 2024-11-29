using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000007 RID: 7
	internal class SizeCriterion : SelectionCriterion
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021D4 File Offset: 0x000003D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("size ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ")
				.Append(this.Size.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		internal override bool Evaluate(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			return this._Evaluate(fileInfo.Length);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		private bool _Evaluate(long Length)
		{
			bool flag;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				flag = Length > this.Size;
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				flag = Length >= this.Size;
				break;
			case ComparisonOperator.LesserThan:
				flag = Length < this.Size;
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				flag = Length <= this.Size;
				break;
			case ComparisonOperator.EqualTo:
				flag = Length == this.Size;
				break;
			case ComparisonOperator.NotEqualTo:
				flag = Length != this.Size;
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return flag;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F0 File Offset: 0x000004F0
		internal override bool Evaluate(ZipEntry entry)
		{
			return this._Evaluate(entry.UncompressedSize);
		}

		// Token: 0x04000015 RID: 21
		internal ComparisonOperator Operator;

		// Token: 0x04000016 RID: 22
		internal long Size;
	}
}
