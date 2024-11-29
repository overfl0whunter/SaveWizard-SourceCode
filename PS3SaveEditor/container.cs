using System;
using System.Collections.Generic;

namespace PS3SaveEditor
{
	// Token: 0x020001BE RID: 446
	public class container
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000710ED File Offset: 0x0006F2ED
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x000710F5 File Offset: 0x0006F2F5
		public string key { get; set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000710FE File Offset: 0x0006F2FE
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x00071106 File Offset: 0x0006F306
		public string pfs { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0007110F File Offset: 0x0006F30F
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00071117 File Offset: 0x0006F317
		public string name { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x00071120 File Offset: 0x0006F320
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x00071128 File Offset: 0x0006F328
		public int preprocess { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x00071131 File Offset: 0x0006F331
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x00071139 File Offset: 0x0006F339
		public files files { get; set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00071142 File Offset: 0x0006F342
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0007114A File Offset: 0x0006F34A
		public int? quickmode { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00071153 File Offset: 0x0006F353
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0007115B File Offset: 0x0006F35B
		public int? locked { get; set; }

		// Token: 0x060016D1 RID: 5841 RVA: 0x00071164 File Offset: 0x0006F364
		public container()
		{
			this.files = new files();
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0007117C File Offset: 0x0006F37C
		public List<cheat> GetAllCheats()
		{
			List<cheat> list = new List<cheat>();
			foreach (file file in this.files._files)
			{
				list.AddRange(file.GetAllCheats());
			}
			return list;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000711EC File Offset: 0x0006F3EC
		internal static container Copy(container folder)
		{
			container container = new container();
			container.key = folder.key;
			container.pfs = folder.pfs;
			container.name = folder.name;
			container.preprocess = folder.preprocess;
			container.files = new files();
			foreach (file file in folder.files._files)
			{
				container.files._files.Add(file.Copy(file));
			}
			container.quickmode = folder.quickmode;
			container.locked = folder.locked;
			return container;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000712BC File Offset: 0x0006F4BC
		internal int GetCheatsCount()
		{
			int num = 0;
			foreach (file file in this.files._files)
			{
				num += file.TotalCheats;
			}
			return num;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00071324 File Offset: 0x0006F524
		internal file GetSaveFile(string fileName)
		{
			foreach (file file in this.files._files)
			{
				bool flag = file.filename == fileName || Util.IsMatch(fileName, file.filename);
				if (flag)
				{
					return file;
				}
			}
			return null;
		}
	}
}
