using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace PS3SaveEditor
{
	// Token: 0x020001DC RID: 476
	public class SingleInstanceApplication : WindowsFormsApplicationBase
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x000935F9 File Offset: 0x000917F9
		public SingleInstanceApplication(AuthenticationMode mode)
			: base(mode)
		{
			this.InitializeAppProperties();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0009360B File Offset: 0x0009180B
		public SingleInstanceApplication()
		{
			this.InitializeAppProperties();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0009361C File Offset: 0x0009181C
		protected virtual void InitializeAppProperties()
		{
			base.IsSingleInstance = true;
			base.EnableVisualStyles = true;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0009362F File Offset: 0x0009182F
		public virtual void Run(Form mainForm)
		{
			base.MainForm = mainForm;
			this.Run(base.CommandLineArgs);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00093648 File Offset: 0x00091848
		private void Run(ReadOnlyCollection<string> commandLineArgs)
		{
			ArrayList arrayList = new ArrayList(commandLineArgs);
			string[] array = (string[])arrayList.ToArray(typeof(string));
			base.Run(array);
		}
	}
}
