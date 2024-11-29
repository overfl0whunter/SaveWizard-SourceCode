using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using PS3SaveEditor.Resources;
using PS3SaveEditor.SubControls;

namespace PS3SaveEditor
{
	// Token: 0x020001AB RID: 427
	public partial class AddCode : Form
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00068186 File Offset: 0x00066386
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0006818E File Offset: 0x0006638E
		public string Description { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00068197 File Offset: 0x00066397
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0006819F File Offset: 0x0006639F
		public string Comment { get; set; }

		// Token: 0x060015FA RID: 5626 RVA: 0x000681A8 File Offset: 0x000663A8
		public AddCode(List<string> existingCodes)
		{
			this.InitializeComponent();
			this.Font = Util.GetFontForPlatform(this.Font);
			this.m_existingCodes = existingCodes;
			this.panel1.BackColor = Color.FromArgb(127, 204, 204, 204);
			this.lblCodes.BackColor = Color.Transparent;
			this.lblComment.BackColor = Color.Transparent;
			this.lblDescription.BackColor = Color.Transparent;
			this.lblDescription.Text = Resources.lblDescription;
			this.lblComment.Text = Resources.lblComment;
			this.lblCodes.Text = Resources.lblCodes;
			this.btnSave.Text = Resources.btnSave;
			this.btnCancel.Text = Resources.btnCancel;
			base.CenterToScreen();
			this.dataGridView1.CellValueChanged += this.dataGridView1_CellValueChanged;
			this.dataGridView1.KeyDown += this.dataGridView1_KeyDown;
			this.m_bMode = AddCode.Mode.ADD_MODE;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000682D4 File Offset: 0x000664D4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool flag = base.ClientRectangle.Width == 0 || base.ClientRectangle.Height == 0;
			if (!flag)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
				}
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0006836C File Offset: 0x0006656C
		public AddCode(cheat item, List<string> existingCodes)
		{
			this.m_bMode = AddCode.Mode.EDIT_MODE;
			this.m_existingCodes = existingCodes;
			this.InitializeComponent();
			this.Text = Resources.titleCodeEntry;
			this.Text = Resources.titleEditCheat;
			this.lblDescription.Text = Resources.lblDescription;
			this.lblComment.Text = Resources.lblComment;
			this.lblCodes.Text = Resources.lblCodes;
			this.btnSave.Text = Resources.btnSave;
			this.btnCancel.Text = Resources.btnCancel;
			base.CenterToScreen();
			this.dataGridView1.CellValueChanged += this.dataGridView1_CellValueChanged;
			this.dataGridView1.KeyDown += this.dataGridView1_KeyDown;
			this.txtCode.Text = item.ToEditableString();
			this.txtDescription.Text = item.name;
			this.txtComment.Text = item.note;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00068480 File Offset: 0x00066680
		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = (e.KeyCode >= Keys.A && e.KeyCode <= Keys.F) || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete;
			if (!flag)
			{
				e.SuppressKeyPress = true;
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000684F8 File Offset: 0x000666F8
		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null;
			if (flag)
			{
				string text = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
				int num = 0;
				bool flag2 = !int.TryParse(text, NumberStyles.HexNumber, null, out num);
				if (flag2)
				{
					this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
					Util.ShowMessage(Resources.errInvalidHexCode, Resources.msgError);
				}
				else
				{
					this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num.ToString("X8");
				}
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00068600 File Offset: 0x00066800
		public static byte[] ConvertHexStringToByteArray(string hexString)
		{
			bool flag = hexString.Length % 2 != 0;
			if (flag)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[] { hexString }));
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				string text = hexString.Substring(i * 2, 2);
				array[i] = byte.Parse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00068688 File Offset: 0x00066888
		private void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = string.IsNullOrEmpty(this.txtDescription.Text.Trim());
			if (flag)
			{
				Util.ShowMessage(Resources.errInvalidDesc, Resources.msgError);
			}
			else
			{
				bool flag2 = this.m_existingCodes.IndexOf(this.txtDescription.Text) >= 0;
				if (flag2)
				{
					Util.ShowMessage(Resources.errCheatExists, Resources.msgError);
				}
				else
				{
					bool flag3 = this.txtCode.Text.Trim().Length == 0;
					if (flag3)
					{
						Util.ShowMessage(Resources.errInvalidCode, Resources.msgError);
					}
					else
					{
						foreach (string text in this.txtCode.Lines)
						{
							bool flag4 = text.Trim().Length != 17 && text.Trim().Length != 0;
							if (flag4)
							{
								Util.ShowMessage(Resources.errInvalidCode, Resources.msgError);
								return;
							}
						}
						bool flag5 = this.txtCode.Lines[0][0] == 'F';
						if (flag5)
						{
							bool flag6 = this.txtCode.Lines.Length > 16;
							if (flag6)
							{
								Util.ShowMessage(Resources.errInvalidFCode, Resources.msgError);
								return;
							}
							string text2 = this.txtCode.Text.Replace(" ", "");
							text2 = text2.Replace("\r\n", "");
							byte[] bytes = Encoding.ASCII.GetBytes(text2.Substring(0, text2.Length - 8));
							uint crc = this.GetCRC(bytes);
							string text3 = text2.Substring(text2.Length - 8, 8);
							uint num = uint.Parse(text3, NumberStyles.HexNumber);
							bool flag7 = crc != num;
							if (flag7)
							{
								Util.ShowMessage(Resources.errInvalidCode, Resources.msgError);
								return;
							}
						}
						bool flag8 = Util.ShowMessage(Resources.msgConfirmCode, Resources.warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No;
						if (!flag8)
						{
							this.Description = this.txtDescription.Text;
							this.Comment = this.txtComment.Text;
							this.Code = this.txtCode.Text.Replace("\r\n", " ").TrimEnd(new char[0]);
							this.Code = this.Code.Replace("\n", " ").TrimEnd(new char[0]);
							base.DialogResult = DialogResult.OK;
							base.Close();
						}
					}
				}
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00063981 File Offset: 0x00061B81
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00068928 File Offset: 0x00066B28
		private void txtCheatCode_TextChanged(object sender, EventArgs e)
		{
			int selectionStart = this.txtCode.SelectionStart;
			int num = this.txtCode.Lines.Length;
			bool flag = num > 1 && (this.txtCode.Lines[num - 2].Length < 17 || this.txtCode.Lines[num - 1].Length == 0);
			if (flag)
			{
				num--;
			}
			bool flag2 = num > 128;
			if (flag2)
			{
				string[] array = new string[128];
				Array.Copy(this.txtCode.Lines, array, 128);
				this.SetLinesToCode(array);
				Util.ShowMessage(string.Format(Resources.errMaxCodes, 128), this.Text);
				this.txtCode.SelectionStart = this.txtCode.TextLength;
				this.txtCode.SelectionLength = 0;
			}
			else
			{
				bool flag3 = num > 0;
				if (flag3)
				{
					string[] lines = this.txtCode.Lines;
					this.SetLinesToCode(lines);
				}
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00068A34 File Offset: 0x00066C34
		private void txtCheatCode_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Delete;
			if (flag)
			{
				int num = this.txtCode.SelectionStart - this.txtCode.GetFirstCharIndexOfCurrentLine();
				bool flag2 = Util.CurrentPlatform == Util.Platform.Linux;
				int num2;
				if (flag2)
				{
					num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart) - 1;
				}
				else
				{
					bool flag3 = Util.CurrentPlatform == Util.Platform.MacOS;
					if (flag3)
					{
						num2 = this.getCurrentLine();
					}
					else
					{
						num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart);
					}
				}
				string[] lines = this.txtCode.Lines;
				bool flag4 = lines.Length != 0;
				if (flag4)
				{
					string text = lines[num2];
					bool flag5 = num > 0 && num >= text.Length;
					if (flag5)
					{
						e.SuppressKeyPress = true;
						return;
					}
				}
				bool flag6 = num >= 17;
				if (flag6)
				{
					e.SuppressKeyPress = true;
				}
				bool flag7 = num == 8;
				if (flag7)
				{
					this.txtCode.SelectionStart++;
				}
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00068B40 File Offset: 0x00066D40
		private int getCurrentLine()
		{
			int selectionStart = this.txtCode.SelectionStart;
			int num = 0;
			string[] lines = this.txtCode.Lines;
			for (int i = lines[num].Length + 2; i < selectionStart; i += lines[num].Length + 2)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00068B9C File Offset: 0x00066D9C
		private void SetLinesToCode(string[] lines)
		{
			string text = "";
			int num = 0;
			int num2 = this.txtCode.SelectionStart;
			for (int i = 0; i < lines.Length; i++)
			{
				bool flag = i < lines.Length - 1 || lines[i].Length > 0;
				if (flag)
				{
					string text2 = lines[num].Replace(" ", "");
					for (int j = 0; j < text2.Length; j++)
					{
						bool flag2 = (text2[j] >= '0' && text2[j] <= '9') || (text2[j] >= 'a' && text2[j] <= 'f') || (text2[j] >= 'A' && text2[j] <= 'F');
						if (!flag2)
						{
							text2 = text2.Remove(j, 1);
						}
					}
					bool flag3 = text2.Length > 8;
					if (flag3)
					{
						string text3 = text2.Substring(0, 8);
						string text4 = text2.Substring(8, Math.Min(8, text2.Length - 8));
						text2 = text3 + " " + text4 + Environment.NewLine;
					}
					else
					{
						text2 += Environment.NewLine;
					}
					text += text2;
					num++;
				}
			}
			lines = this.txtCode.Lines;
			int num3 = 0;
			foreach (string text5 in lines)
			{
				bool flag4 = text5.Length > 0 && text5.Length > 17;
				if (flag4)
				{
					num2 = (num3 + 1) * 18;
				}
				num3++;
			}
			this.txtCode.Text = text;
			bool flag5 = num2 > 0;
			if (flag5)
			{
				this.txtCode.SelectionStart = num2;
				this.txtCode.ScrollToCaret();
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00068D98 File Offset: 0x00066F98
		private void HandleCodeBackSpace(ref KeyPressEventArgs e)
		{
			int num = this.txtCode.SelectionStart - this.txtCode.GetFirstCharIndexOfCurrentLine();
			bool flag = num < 0;
			if (flag)
			{
				num = this.txtCode.SelectionStart;
			}
			string[] lines = this.txtCode.Lines;
			bool flag2 = Util.CurrentPlatform == Util.Platform.Linux;
			int num2;
			if (flag2)
			{
				num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart) - 1;
			}
			else
			{
				bool flag3 = Util.CurrentPlatform == Util.Platform.MacOS;
				if (flag3)
				{
					num2 = this.getCurrentLine();
				}
				else
				{
					num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart);
				}
			}
			bool flag4 = lines.Length == 0;
			if (!flag4)
			{
				bool flag5 = num == 0 && this.txtCode.SelectionStart > 0 && lines[num2].Length > 0;
				if (flag5)
				{
					e.Handled = true;
					this.txtCode.SelectionStart -= 2;
				}
				else
				{
					bool flag6 = num == 9;
					if (flag6)
					{
						this.txtCode.SelectionStart--;
					}
				}
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00068EB0 File Offset: 0x000670B0
		private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = e.KeyChar == '\b';
			if (flag)
			{
				this.HandleCodeBackSpace(ref e);
			}
			else
			{
				bool flag2 = (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F');
				if (flag2)
				{
					int num = this.txtCode.Lines.Length;
					bool flag3 = num > 1 && this.txtCode.Lines[num - 2].Length < 17;
					if (flag3)
					{
						num--;
					}
					bool flag4 = num > 128;
					if (flag4)
					{
						e.Handled = true;
						Util.ShowMessage(string.Format(Resources.msgMaxCheats, 128), this.Text);
					}
					else
					{
						bool flag5 = Util.CurrentPlatform == Util.Platform.Linux;
						int num2;
						if (flag5)
						{
							num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart) - 1;
						}
						else
						{
							bool flag6 = Util.CurrentPlatform == Util.Platform.MacOS;
							if (flag6)
							{
								num2 = this.getCurrentLine();
							}
							else
							{
								num2 = this.txtCode.GetLineFromCharIndex(this.txtCode.SelectionStart);
							}
						}
						string text = "";
						string[] array = this.txtCode.Lines;
						bool flag7 = this.txtCode.Lines.Length != 0;
						if (flag7)
						{
							text = this.txtCode.Lines[num2];
						}
						else
						{
							array = new string[1];
						}
						int num3 = this.txtCode.SelectionStart - this.txtCode.GetFirstCharIndexOfCurrentLine();
						bool flag8 = num3 < 0;
						if (flag8)
						{
							num3 = this.txtCode.SelectionStart;
						}
						bool flag9 = num3 > 17;
						if (flag9)
						{
							num3 = 17;
						}
						int selectionStart = this.txtCode.SelectionStart;
						bool flag10 = num3 == 17;
						if (flag10)
						{
							int firstCharIndexFromLine = this.txtCode.GetFirstCharIndexFromLine(num2 + 1);
							char[] array2 = array[num2 + 1].ToCharArray();
							bool flag11 = array2.Length == 0;
							if (flag11)
							{
								array2 = new char[1];
							}
							array2[0] = e.KeyChar;
							array[num2 + 1] = new string(array2);
							this.SetLinesToCode(array);
							this.txtCode.SelectionStart = this.txtCode.GetFirstCharIndexFromLine(num2 + 1) + 1;
							bool flag12 = this.txtCode.SelectionStart > 0;
							if (flag12)
							{
								this.txtCode.ScrollToCaret();
							}
							e.Handled = true;
						}
						else
						{
							char[] array3 = text.ToCharArray();
							bool flag13 = array3.Length == 17;
							if (flag13)
							{
								bool flag14 = num3 == 8;
								if (flag14)
								{
									array3[num3 + 1] = e.KeyChar;
									array[num2] = new string(array3);
									this.SetLinesToCode(array);
									this.txtCode.SelectionStart += 2;
									e.Handled = true;
								}
								else
								{
									array3[num3] = e.KeyChar;
									array[num2] = new string(array3);
									this.SetLinesToCode(array);
									this.txtCode.SelectionStart++;
									e.Handled = true;
								}
							}
							else
							{
								bool flag15 = num3 == 8 && array3.Length == 8;
								if (flag15)
								{
									char[] array4 = new char[array3.Length + 2];
									Array.Copy(array3, array4, 8);
									array4[8] = ' ';
									array4[9] = e.KeyChar;
									array[num2] = new string(array4);
									this.SetLinesToCode(array);
									this.txtCode.SelectionStart += 2;
									e.Handled = true;
								}
								else
								{
									bool flag16 = num3 == 8 && array3.Length > 8;
									if (flag16)
									{
										char[] array5 = new char[array3.Length + 1];
										Array.Copy(array3, array5, 8);
										array5[8] = ' ';
										array5[9] = e.KeyChar;
										Array.Copy(array3, 9, array5, 10, array3.Length - 9);
										array[num2] = new string(array5);
										this.SetLinesToCode(array);
										this.txtCode.SelectionStart += 2;
										e.Handled = true;
									}
									else
									{
										bool flag17 = num3 > 8;
										if (flag17)
										{
											char[] array6 = new char[array3.Length + 1];
											Array.Copy(array3, array6, num3);
											array6[num3] = e.KeyChar;
											Array.Copy(array3, num3, array6, num3 + 1, array3.Length - num3);
											array[num2] = new string(array6);
											this.SetLinesToCode(array);
											this.txtCode.SelectionStart++;
											e.Handled = true;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag18 = e.KeyChar == '\u0001';
					if (flag18)
					{
						this.txtCode.SelectAll();
					}
					else
					{
						bool flag19 = e.KeyChar == '\u0003' || e.KeyChar == '\r' || e.KeyChar == '\u0018' || e.KeyChar == '\u0016' || e.KeyChar == '\u001a';
						if (!flag19)
						{
							e.Handled = true;
						}
					}
				}
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000693C0 File Offset: 0x000675C0
		private uint GetCRC(byte[] data)
		{
			Crc32Net crc32Net = new Crc32Net();
			crc32Net.ComputeHash(data);
			return crc32Net.CrcValue;
		}

		// Token: 0x04000A0A RID: 2570
		private const int MAX_CHEAT_CODES = 128;

		// Token: 0x04000A0B RID: 2571
		private AddCode.Mode m_bMode = AddCode.Mode.ADD_MODE;

		// Token: 0x04000A0C RID: 2572
		public string Code;

		// Token: 0x04000A0D RID: 2573
		private List<string> m_existingCodes;

		// Token: 0x02000290 RID: 656
		private enum Mode
		{
			// Token: 0x04000FCD RID: 4045
			ADD_MODE,
			// Token: 0x04000FCE RID: 4046
			EDIT_MODE
		}
	}
}
