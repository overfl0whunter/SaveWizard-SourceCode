using System;
using System.Globalization;
using System.Xml.Serialization;

namespace PS3SaveEditor
{
	// Token: 0x020001C4 RID: 452
	[XmlRoot("cheat")]
	public class cheat
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000724BC File Offset: 0x000706BC
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x000724C4 File Offset: 0x000706C4
		public string id { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000724CD File Offset: 0x000706CD
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x000724D5 File Offset: 0x000706D5
		public string name { get; set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000724DE File Offset: 0x000706DE
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x000724E6 File Offset: 0x000706E6
		public string note { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000724EF File Offset: 0x000706EF
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x000724F7 File Offset: 0x000706F7
		public bool Selected { get; set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00072500 File Offset: 0x00070700
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x00072508 File Offset: 0x00070708
		public string code { get; set; }

		// Token: 0x06001726 RID: 5926 RVA: 0x00003ED8 File Offset: 0x000020D8
		public cheat()
		{
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00072511 File Offset: 0x00070711
		public cheat(string id, string description, string comment)
		{
			this.id = id;
			this.name = description;
			this.note = comment;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00072534 File Offset: 0x00070734
		public string ToEditableString()
		{
			string text = "";
			string[] array = this.code.Split(new char[] { ' ' });
			for (int i = 0; i < array.Length; i += 2)
			{
				text = string.Concat(new string[]
				{
					text,
					array[i],
					" ",
					array[i + 1],
					"\n"
				});
			}
			return text;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000725A8 File Offset: 0x000707A8
		public string ToString(bool _protected = false)
		{
			string text = "";
			bool selected = this.Selected;
			if (selected)
			{
				if (_protected)
				{
					bool flag = !string.IsNullOrEmpty(this.id);
					if (flag)
					{
						text += string.Format("<id>{0}</id>", this.id);
					}
				}
				else
				{
					bool flag2 = this.code != null;
					if (flag2)
					{
						return string.Format("<code>{0}</code>", this.code);
					}
					return string.Format("<id>{0}</id>", this.id);
				}
			}
			return text;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0007263C File Offset: 0x0007083C
		internal static cheat Copy(cheat cheat)
		{
			return new cheat
			{
				id = cheat.id,
				name = cheat.name,
				note = cheat.note,
				code = cheat.code
			};
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0007268C File Offset: 0x0007088C
		internal static byte GetBitCodeBytes(int bitCode)
		{
			byte b;
			switch (bitCode)
			{
			case 0:
				b = 1;
				break;
			case 1:
				b = 2;
				break;
			case 2:
				b = 4;
				break;
			default:
				b = 4;
				break;
			}
			return b;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000726C4 File Offset: 0x000708C4
		internal static long GetMemLocation(string value, out int bitWriteCode)
		{
			long num;
			long.TryParse(value, NumberStyles.HexNumber, null, out num);
			long num2 = num & 268435455L;
			bitWriteCode = (int)(num >> 28);
			return num2;
		}
	}
}
