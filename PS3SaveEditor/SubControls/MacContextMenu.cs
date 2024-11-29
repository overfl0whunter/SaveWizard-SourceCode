using System;
using System.Windows.Forms;
using PS3SaveEditor.Utilities;

namespace PS3SaveEditor.SubControls
{
	// Token: 0x020001F0 RID: 496
	public class MacContextMenu
	{
		// Token: 0x06001A5A RID: 6746 RVA: 0x000AC799 File Offset: 0x000AA999
		public MacContextMenu(TextBox txtBox)
		{
			this._txtBox = txtBox;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000AC7AC File Offset: 0x000AA9AC
		public ContextMenu GetMenu()
		{
			bool flag = this._macMenu == null;
			if (flag)
			{
				this._macMenu = new ContextMenu();
				this._macMenu.MenuItems.Add(new MenuItem("Undo", new EventHandler(this.UndoItemClick)));
				this._macMenu.MenuItems.Add(new MenuItem("-"));
				this._macMenu.MenuItems.Add(new MenuItem("Cut", new EventHandler(this.CutItemClick)));
				this._macMenu.MenuItems.Add(new MenuItem("Copy", new EventHandler(this.CopyItemClick)));
				this._macMenu.MenuItems.Add(new MenuItem("Paste", new EventHandler(this.PasteItemClick)));
				this._macMenu.MenuItems.Add(new MenuItem("Delete", new EventHandler(this.DeleteItemClick)));
				this._macMenu.MenuItems.Add(new MenuItem("-"));
				this._macMenu.MenuItems.Add(new MenuItem("Select All", new EventHandler(this.SelectAllItemClick)));
				this._macMenu.MenuItems[0].Enabled = false;
				this._macMenu.Popup += this.CheckMenuAvailability;
			}
			return this._macMenu;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000AC931 File Offset: 0x000AAB31
		private void UndoItemClick(object sender, EventArgs e)
		{
			this._txtBox.Undo();
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000AC940 File Offset: 0x000AAB40
		private void CutItemClick(object sender, EventArgs e)
		{
			ClipboardMac.CopyToClipboard(this._txtBox);
			this._txtBox.SelectedText = string.Empty;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000AC960 File Offset: 0x000AAB60
		private void CopyItemClick(object sender, EventArgs e)
		{
			ClipboardMac.CopyToClipboard(this._txtBox);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000AC96F File Offset: 0x000AAB6F
		private void PasteItemClick(object sender, EventArgs e)
		{
			ClipboardMac.PasteFromClipboard(this._txtBox);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000AC97E File Offset: 0x000AAB7E
		private void DeleteItemClick(object sender, EventArgs e)
		{
			this._txtBox.SelectedText = string.Empty;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000AC992 File Offset: 0x000AAB92
		private void SelectAllItemClick(object sender, EventArgs e)
		{
			this._txtBox.SelectAll();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000AC9A4 File Offset: 0x000AABA4
		private void CheckMenuAvailability(object sender, EventArgs e)
		{
			this._macMenu.MenuItems[2].Enabled = this._txtBox.SelectionLength > 0 && !this._txtBox.ReadOnly;
			this._macMenu.MenuItems[3].Enabled = this._txtBox.SelectionLength > 0;
			this._macMenu.MenuItems[5].Enabled = this._txtBox.SelectionLength > 0 && !this._txtBox.ReadOnly;
			this._macMenu.MenuItems[4].Enabled = !this._txtBox.ReadOnly;
			this._macMenu.MenuItems[7].Enabled = this._txtBox.CanSelect;
		}

		// Token: 0x04000D1E RID: 3358
		private readonly TextBox _txtBox;

		// Token: 0x04000D1F RID: 3359
		private ContextMenu _macMenu;
	}
}
