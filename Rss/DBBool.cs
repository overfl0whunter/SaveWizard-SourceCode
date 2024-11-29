using System;

namespace Rss
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public struct DBBool
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00035508 File Offset: 0x00033708
		private DBBool(int value)
		{
			this.value = (sbyte)value;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00035514 File Offset: 0x00033714
		public bool IsNull
		{
			get
			{
				return this.value == 0;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00035530 File Offset: 0x00033730
		public bool IsFalse
		{
			get
			{
				return this.value < 0;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003554C File Offset: 0x0003374C
		public bool IsTrue
		{
			get
			{
				return this.value > 0;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00035568 File Offset: 0x00033768
		public static implicit operator DBBool(bool x)
		{
			return x ? DBBool.True : DBBool.False;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0003558C File Offset: 0x0003378C
		public static explicit operator bool(DBBool x)
		{
			bool flag = x.value == 0;
			if (flag)
			{
				throw new InvalidOperationException();
			}
			return x.value > 0;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000355BC File Offset: 0x000337BC
		public static DBBool operator ==(DBBool x, DBBool y)
		{
			bool flag = x.value == 0 || y.value == 0;
			DBBool dbbool;
			if (flag)
			{
				dbbool = DBBool.Null;
			}
			else
			{
				dbbool = ((x.value == y.value) ? DBBool.True : DBBool.False);
			}
			return dbbool;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00035608 File Offset: 0x00033808
		public static DBBool operator !=(DBBool x, DBBool y)
		{
			bool flag = x.value == 0 || y.value == 0;
			DBBool dbbool;
			if (flag)
			{
				dbbool = DBBool.Null;
			}
			else
			{
				dbbool = ((x.value != y.value) ? DBBool.True : DBBool.False);
			}
			return dbbool;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00035654 File Offset: 0x00033854
		public static DBBool operator !(DBBool x)
		{
			return new DBBool((int)(-(int)x.value));
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00035674 File Offset: 0x00033874
		public static DBBool operator &(DBBool x, DBBool y)
		{
			return new DBBool((int)((x.value < y.value) ? x.value : y.value));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000356A8 File Offset: 0x000338A8
		public static DBBool operator |(DBBool x, DBBool y)
		{
			return new DBBool((int)((x.value > y.value) ? x.value : y.value));
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000356DC File Offset: 0x000338DC
		public static bool operator true(DBBool x)
		{
			return x.value > 0;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000356F8 File Offset: 0x000338F8
		public static bool operator false(DBBool x)
		{
			return x.value < 0;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00035714 File Offset: 0x00033914
		public override bool Equals(object o)
		{
			bool flag;
			try
			{
				flag = (bool)(this == (DBBool)o);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00035754 File Offset: 0x00033954
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003576C File Offset: 0x0003396C
		public override string ToString()
		{
			string text;
			switch (this.value)
			{
			case -1:
				text = "false";
				break;
			case 0:
				text = "DBBool.Null";
				break;
			case 1:
				text = "true";
				break;
			default:
				throw new InvalidOperationException();
			}
			return text;
		}

		// Token: 0x0400051B RID: 1307
		public static readonly DBBool Null = new DBBool(0);

		// Token: 0x0400051C RID: 1308
		public static readonly DBBool False = new DBBool(-1);

		// Token: 0x0400051D RID: 1309
		public static readonly DBBool True = new DBBool(1);

		// Token: 0x0400051E RID: 1310
		private sbyte value;
	}
}
