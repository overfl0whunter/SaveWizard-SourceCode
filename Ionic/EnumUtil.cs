using System;
using System.ComponentModel;
using System.Reflection;

namespace Ionic
{
	// Token: 0x0200000E RID: 14
	internal sealed class EnumUtil
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003ED8 File Offset: 0x000020D8
		private EnumUtil()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003EE4 File Offset: 0x000020E4
		internal static string GetDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			bool flag = array.Length != 0;
			string text;
			if (flag)
			{
				text = array[0].Description;
			}
			else
			{
				text = value.ToString();
			}
			return text;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003F3C File Offset: 0x0000213C
		internal static object Parse(Type enumType, string stringRepresentation)
		{
			return EnumUtil.Parse(enumType, stringRepresentation, false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003F58 File Offset: 0x00002158
		internal static object Parse(Type enumType, string stringRepresentation, bool ignoreCase)
		{
			if (ignoreCase)
			{
				stringRepresentation = stringRepresentation.ToLower();
			}
			foreach (object obj in Enum.GetValues(enumType))
			{
				Enum @enum = (Enum)obj;
				string text = EnumUtil.GetDescription(@enum);
				if (ignoreCase)
				{
					text = text.ToLower();
				}
				bool flag = text == stringRepresentation;
				if (flag)
				{
					return @enum;
				}
			}
			return Enum.Parse(enumType, stringRepresentation, ignoreCase);
		}
	}
}
