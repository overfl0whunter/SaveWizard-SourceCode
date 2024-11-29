using System;
using System.Drawing;
using System.Windows.Forms;

namespace PS3SaveEditor
{
	// Token: 0x020001EC RID: 492
	public class CustomMsgBox
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x000A4C64 File Offset: 0x000A2E64
		public static DialogResult Show(string text = "", string title = "", MessageBoxButtons button = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
		{
			return CustomMsgBox.Show(null, text, title, button, icon, defaultButton);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000A4C84 File Offset: 0x000A2E84
		public static DialogResult Show(string text)
		{
			return CustomMsgBox.Show(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000A4CA8 File Offset: 0x000A2EA8
		public static DialogResult Show(Form parent = null, string text = "", string title = "", MessageBoxButtons button = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
		{
			DialogResult dialogResult;
			using (Form form = new Form())
			{
				form.Icon = CustomMsgBox.MsgBoxIconToFormIcon(icon);
				form.Font = Util.GetFontForPlatform(form.Font);
				form.Text = title;
				form.FormBorderStyle = FormBorderStyle.FixedSingle;
				bool flag = parent == null;
				if (flag)
				{
					form.StartPosition = FormStartPosition.CenterScreen;
				}
				else
				{
					form.StartPosition = FormStartPosition.Manual;
					form.Location = new Point(parent.Location.X + (parent.Width / 2 - Util.ScaleSize(140)), parent.Location.Y + (parent.Height / 2 - Util.ScaleSize(75)));
				}
				form.Width = Util.ScaleSize(280);
				form.MaximizeBox = false;
				form.MinimizeBox = false;
				Label label = new Label();
				label.Text = text;
				label.Width = Util.ScaleSize(250);
				label.Padding = new Padding(Util.ScaleSize(11), Util.ScaleSize(5), Util.ScaleSize(11), Util.ScaleSize(5));
				label.TextAlign = ContentAlignment.MiddleCenter;
				bool flag2 = Util.ScaleSize(label.PreferredWidth) > Util.ScaleSize(280);
				int num = Util.ScaleSize(label.PreferredWidth) / Util.ScaleSize(280);
				label.AutoSize = false;
				int num2 = 0;
				bool flag3 = flag2;
				if (flag3)
				{
					num2 = Util.ScaleSize(14) * num;
				}
				label.Height = Util.ScaleSize(50) + num2;
				bool flag4 = Util.IsUnixOrMacOSX();
				if (flag4)
				{
					form.Height = Util.ScaleSize(120) + num2;
				}
				else
				{
					form.Height = Util.ScaleSize(150) + num2;
				}
				form.Controls.Add(label);
				bool flag5 = button == MessageBoxButtons.OK;
				if (flag5)
				{
					Button button2 = new Button();
					button2.Text = "OK";
					button2.SetBounds(Util.ScaleSize(100), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
					button2.DialogResult = DialogResult.OK;
					form.Controls.Add(button2);
				}
				else
				{
					bool flag6 = button == MessageBoxButtons.OKCancel;
					if (flag6)
					{
						Button button3 = new Button();
						Button button4 = new Button();
						button3.Text = "OK";
						button3.SetBounds(Util.ScaleSize(50), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
						button3.DialogResult = DialogResult.OK;
						button4.Text = "Cancel";
						button4.SetBounds(Util.ScaleSize(155), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(230));
						button4.DialogResult = DialogResult.Cancel;
						bool flag7 = defaultButton == MessageBoxDefaultButton.Button1;
						if (flag7)
						{
							form.Controls.Add(button3);
							form.Controls.Add(button4);
							form.AcceptButton = button3;
						}
						else
						{
							form.Controls.Add(button4);
							form.Controls.Add(button3);
							form.AcceptButton = button4;
						}
					}
					else
					{
						bool flag8 = button == MessageBoxButtons.YesNo;
						if (flag8)
						{
							Button button5 = new Button();
							Button button6 = new Button();
							button5.Text = "Yes";
							button5.SetBounds(Util.ScaleSize(50), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
							button5.DialogResult = DialogResult.Yes;
							button6.Text = "No";
							button6.SetBounds(Util.ScaleSize(155), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
							button6.DialogResult = DialogResult.No;
							bool flag9 = defaultButton == MessageBoxDefaultButton.Button1;
							if (flag9)
							{
								form.Controls.Add(button5);
								form.Controls.Add(button6);
								form.AcceptButton = button5;
							}
							else
							{
								form.Controls.Add(button6);
								form.Controls.Add(button5);
								form.AcceptButton = button6;
							}
						}
						else
						{
							bool flag10 = button == MessageBoxButtons.RetryCancel;
							if (flag10)
							{
								Button button7 = new Button();
								Button button8 = new Button();
								button7.Text = "Retry";
								button7.SetBounds(Util.ScaleSize(50), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
								button7.DialogResult = DialogResult.Retry;
								button8.Text = "Cancel";
								button8.SetBounds(Util.ScaleSize(155), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
								button8.DialogResult = DialogResult.Cancel;
								bool flag11 = defaultButton == MessageBoxDefaultButton.Button1;
								if (flag11)
								{
									form.Controls.Add(button7);
									form.Controls.Add(button8);
									form.AcceptButton = button7;
								}
								else
								{
									form.Controls.Add(button8);
									form.Controls.Add(button7);
									form.AcceptButton = button8;
								}
							}
							else
							{
								bool flag12 = button == MessageBoxButtons.YesNoCancel;
								if (flag12)
								{
									Button button9 = new Button();
									Button button10 = new Button();
									Button button11 = new Button();
									button9.Text = "Yes";
									button9.SetBounds(Util.ScaleSize(20), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
									button9.DialogResult = DialogResult.Yes;
									button10.Text = "No";
									button10.SetBounds(Util.ScaleSize(100), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
									button10.DialogResult = DialogResult.No;
									button11.Text = "Cancel";
									button11.SetBounds(Util.ScaleSize(180), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
									button11.DialogResult = DialogResult.Cancel;
									bool flag13 = defaultButton == MessageBoxDefaultButton.Button1;
									if (flag13)
									{
										form.Controls.Add(button9);
										form.Controls.Add(button10);
										form.Controls.Add(button11);
										form.AcceptButton = button9;
									}
									else
									{
										bool flag14 = defaultButton == MessageBoxDefaultButton.Button2;
										if (flag14)
										{
											form.Controls.Add(button10);
											form.Controls.Add(button9);
											form.Controls.Add(button11);
											form.AcceptButton = button10;
										}
										else
										{
											form.Controls.Add(button9);
											form.Controls.Add(button10);
											form.Controls.Add(button11);
											form.AcceptButton = button11;
										}
									}
								}
								else
								{
									bool flag15 = button == MessageBoxButtons.AbortRetryIgnore;
									if (flag15)
									{
										Button button12 = new Button();
										Button button13 = new Button();
										Button button14 = new Button();
										button12.Text = "Abort";
										button12.SetBounds(Util.ScaleSize(20), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
										button12.DialogResult = DialogResult.Yes;
										button13.Text = "Retry";
										button13.SetBounds(Util.ScaleSize(100), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
										button13.DialogResult = DialogResult.No;
										button14.Text = "Ignore";
										button14.SetBounds(Util.ScaleSize(180), Util.ScaleSize(60 + num2), Util.ScaleSize(75), Util.ScaleSize(23));
										button14.DialogResult = DialogResult.Cancel;
										bool flag16 = defaultButton == MessageBoxDefaultButton.Button1;
										if (flag16)
										{
											form.Controls.Add(button12);
											form.Controls.Add(button13);
											form.Controls.Add(button14);
											form.AcceptButton = button12;
										}
										else
										{
											bool flag17 = defaultButton == MessageBoxDefaultButton.Button2;
											if (flag17)
											{
												form.Controls.Add(button13);
												form.Controls.Add(button12);
												form.Controls.Add(button14);
												form.AcceptButton = button13;
											}
											else
											{
												form.Controls.Add(button14);
												form.Controls.Add(button13);
												form.Controls.Add(button12);
												form.AcceptButton = button14;
											}
										}
									}
								}
							}
						}
					}
				}
				dialogResult = form.ShowDialog();
			}
			return dialogResult;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000A552C File Offset: 0x000A372C
		private static Icon MsgBoxIconToFormIcon(MessageBoxIcon msgBoxIcon)
		{
			bool flag = msgBoxIcon == MessageBoxIcon.Asterisk;
			Icon icon;
			if (flag)
			{
				icon = SystemIcons.Asterisk;
			}
			else
			{
				bool flag2 = msgBoxIcon == MessageBoxIcon.Hand;
				if (flag2)
				{
					icon = SystemIcons.Error;
				}
				else
				{
					bool flag3 = msgBoxIcon == MessageBoxIcon.Exclamation;
					if (flag3)
					{
						icon = SystemIcons.Exclamation;
					}
					else
					{
						bool flag4 = msgBoxIcon == MessageBoxIcon.Hand;
						if (flag4)
						{
							icon = SystemIcons.Hand;
						}
						else
						{
							bool flag5 = msgBoxIcon == MessageBoxIcon.Asterisk;
							if (flag5)
							{
								icon = SystemIcons.Information;
							}
							else
							{
								bool flag6 = msgBoxIcon == MessageBoxIcon.Question;
								if (flag6)
								{
									icon = SystemIcons.Question;
								}
								else
								{
									bool flag7 = msgBoxIcon == MessageBoxIcon.Hand;
									if (flag7)
									{
										icon = SystemIcons.Shield;
									}
									else
									{
										bool flag8 = msgBoxIcon == MessageBoxIcon.Exclamation;
										if (flag8)
										{
											icon = SystemIcons.Warning;
										}
										else
										{
											icon = null;
										}
									}
								}
							}
						}
					}
				}
			}
			return icon;
		}
	}
}
