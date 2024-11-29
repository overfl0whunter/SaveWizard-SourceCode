using System;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200000C RID: 12
	internal class CompoundCriterion : SelectionCriterion
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal SelectionCriterion Right
		{
			get
			{
				return this._Right;
			}
			set
			{
				this._Right = value;
				bool flag = value == null;
				if (flag)
				{
					this.Conjunction = LogicalConjunction.NONE;
				}
				else
				{
					bool flag2 = this.Conjunction == LogicalConjunction.NONE;
					if (flag2)
					{
						this.Conjunction = LogicalConjunction.AND;
					}
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C98 File Offset: 0x00000E98
		internal override bool Evaluate(string filename)
		{
			bool flag = this.Left.Evaluate(filename);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
			{
				bool flag2 = flag;
				if (flag2)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			}
			case LogicalConjunction.OR:
			{
				bool flag3 = !flag;
				if (flag3)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			}
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(filename);
				break;
			default:
				throw new ArgumentException("Conjunction");
			}
			return flag;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D20 File Offset: 0x00000F20
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(").Append((this.Left != null) ? this.Left.ToString() : "null").Append(" ")
				.Append(this.Conjunction.ToString())
				.Append(" ")
				.Append((this.Right != null) ? this.Right.ToString() : "null")
				.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = this.Left.Evaluate(entry);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
			{
				bool flag2 = flag;
				if (flag2)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			}
			case LogicalConjunction.OR:
			{
				bool flag3 = !flag;
				if (flag3)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			}
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(entry);
				break;
			}
			return flag;
		}

		// Token: 0x04000022 RID: 34
		internal LogicalConjunction Conjunction;

		// Token: 0x04000023 RID: 35
		internal SelectionCriterion Left;

		// Token: 0x04000024 RID: 36
		private SelectionCriterion _Right;
	}
}
