using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200000A RID: 10
	internal class TypeCriterion : SelectionCriterion
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000026C8 File Offset: 0x000008C8
		internal string AttributeString
		{
			get
			{
				return this.ObjectType.ToString();
			}
			set
			{
				bool flag = value.Length != 1 || (value[0] != 'D' && value[0] != 'F');
				if (flag)
				{
					throw new ArgumentException("Specify a single character: either D or F");
				}
				this.ObjectType = value[0];
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000271C File Offset: 0x0000091C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("type ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ")
				.Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002770 File Offset: 0x00000970
		internal override bool Evaluate(string filename)
		{
			bool flag = ((this.ObjectType == 'D') ? Directory.Exists(filename) : File.Exists(filename));
			bool flag2 = this.Operator != ComparisonOperator.EqualTo;
			if (flag2)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027B4 File Offset: 0x000009B4
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = ((this.ObjectType == 'D') ? entry.IsDirectory : (!entry.IsDirectory));
			bool flag2 = this.Operator != ComparisonOperator.EqualTo;
			if (flag2)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0400001E RID: 30
		private char ObjectType;

		// Token: 0x0400001F RID: 31
		internal ComparisonOperator Operator;
	}
}
