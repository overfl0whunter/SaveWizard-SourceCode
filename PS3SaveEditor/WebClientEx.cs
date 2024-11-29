using System;
using System.Net;
using System.Net.Security;
using System.Text;

namespace PS3SaveEditor
{
	// Token: 0x020001EA RID: 490
	internal class WebClientEx : WebClient
	{
		// Token: 0x060019F2 RID: 6642 RVA: 0x000A4860 File Offset: 0x000A2A60
		protected override WebRequest GetWebRequest(Uri address)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(address);
			httpWebRequest.Timeout = 20000;
			httpWebRequest.UserAgent = Util.GetUserAgent();
			httpWebRequest.PreAuthenticate = true;
			string text = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
			httpWebRequest.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
			httpWebRequest.Headers.Add("Authorization", text);
			return httpWebRequest;
		}
	}
}
