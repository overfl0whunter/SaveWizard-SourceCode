using System;
using System.Collections;

namespace Be.Windows.Forms
{
	// Token: 0x020000D8 RID: 216
	public class ByteCollection : CollectionBase
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00035E24 File Offset: 0x00034024
		public ByteCollection()
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00035E2E File Offset: 0x0003402E
		public ByteCollection(byte[] bs)
		{
			this.AddRange(bs);
		}

		// Token: 0x17000239 RID: 569
		public byte this[int index]
		{
			get
			{
				return (byte)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00035E79 File Offset: 0x00034079
		public void Add(byte b)
		{
			base.List.Add(b);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00035E8E File Offset: 0x0003408E
		public void AddRange(byte[] bs)
		{
			base.InnerList.AddRange(bs);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00035E9E File Offset: 0x0003409E
		public void Remove(byte b)
		{
			base.List.Remove(b);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00035EB3 File Offset: 0x000340B3
		public void RemoveRange(int index, int count)
		{
			base.InnerList.RemoveRange(index, count);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00035EC4 File Offset: 0x000340C4
		public void InsertRange(int index, byte[] bs)
		{
			base.InnerList.InsertRange(index, bs);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00035ED8 File Offset: 0x000340D8
		public byte[] GetBytes()
		{
			byte[] array = new byte[base.Count];
			base.InnerList.CopyTo(0, array, 0, array.Length);
			return array;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00035F09 File Offset: 0x00034109
		public void Insert(int index, byte b)
		{
			base.InnerList.Insert(index, b);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00035F20 File Offset: 0x00034120
		public int IndexOf(byte b)
		{
			return base.InnerList.IndexOf(b);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00035F44 File Offset: 0x00034144
		public bool Contains(byte b)
		{
			return base.InnerList.Contains(b);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00035F67 File Offset: 0x00034167
		public void CopyTo(byte[] bs, int index)
		{
			base.InnerList.CopyTo(bs, index);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00035F78 File Offset: 0x00034178
		public byte[] ToArray()
		{
			byte[] array = new byte[base.Count];
			this.CopyTo(array, 0);
			return array;
		}
	}
}
