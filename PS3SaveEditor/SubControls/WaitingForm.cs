using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3SaveEditor.Resources;

namespace PS3SaveEditor.SubControls
{
	// Token: 0x020001F1 RID: 497
	public partial class WaitingForm : Form
	{
		// Token: 0x06001A65 RID: 6757 RVA: 0x000ACCA0 File Offset: 0x000AAEA0
		public WaitingForm()
		{
			this.InitializeComponent();
			this.waitLabel.ForeColor = Color.White;
			this.waitLabel.BackColor = Color.Transparent;
			this.Font = Util.GetFontForPlatform(this.Font);
			base.Load += this.WaitingForm_Load;
			this.UpdateProgress = new WaitingForm.UpdateProgressDelegate(this.UpdateProgressSafe);
			this.CloseForm = new WaitingForm.CloseDelegate(this.CloseFormSafe);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000ACD35 File Offset: 0x000AAF35
		public WaitingForm(string waitingText)
			: this()
		{
			this.waitLabel.Text = waitingText;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000ACD4C File Offset: 0x000AAF4C
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(0, 138, 213), Color.FromArgb(0, 44, 101), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000ACDB8 File Offset: 0x000AAFB8
		public void Start()
		{
			this.running = true;
			Task.Run(delegate
			{
				base.ShowDialog();
			});
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000ACDD4 File Offset: 0x000AAFD4
		public void Stop()
		{
			this.running = false;
			this.CloseThisForm(true);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000ACDE8 File Offset: 0x000AAFE8
		private void ShowThisProgress()
		{
			int num = 1;
			while (this.running)
			{
				bool flag = num > 100;
				if (flag)
				{
					num = 1;
				}
				this.SetProgress(num);
				Thread.Sleep(500);
				num++;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000ACE28 File Offset: 0x000AB028
		private void SetProgress(int val)
		{
			this.prBar.Invoke(this.UpdateProgress, new object[] { val });
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000ACE4C File Offset: 0x000AB04C
		private void CloseThisForm(bool bSuccess)
		{
			bool flag = !base.IsDisposed;
			if (flag)
			{
				base.Invoke(this.CloseForm, new object[] { bSuccess });
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000ACE84 File Offset: 0x000AB084
		private void WaitingForm_Load(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.ShowThisProgress));
			thread.Start();
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000ACEAB File Offset: 0x000AB0AB
		private void UpdateProgressSafe(int val)
		{
			this.prBar.Value = val;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000ACEBB File Offset: 0x000AB0BB
		private void CloseFormSafe(bool bSuccess)
		{
			base.DialogResult = (bSuccess ? DialogResult.OK : DialogResult.Abort);
			base.Close();
		}

		// Token: 0x04000D23 RID: 3363
		private bool running = false;

		// Token: 0x04000D24 RID: 3364
		private WaitingForm.UpdateProgressDelegate UpdateProgress;

		// Token: 0x04000D25 RID: 3365
		private WaitingForm.CloseDelegate CloseForm;

		// Token: 0x020002D2 RID: 722
		// (Invoke) Token: 0x06001ED4 RID: 7892
		private delegate void UpdateProgressDelegate(int value);

		// Token: 0x020002D3 RID: 723
		// (Invoke) Token: 0x06001ED8 RID: 7896
		private delegate void CloseDelegate(bool bSuccess);
	}
}
