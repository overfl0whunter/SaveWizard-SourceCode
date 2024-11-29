using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using PS3SaveEditor;

namespace Rss
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class RssFeed
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00030EE4 File Offset: 0x0002F0E4
		public RssFeed()
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00030F4C File Offset: 0x0002F14C
		public RssFeed(Encoding encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00030FBC File Offset: 0x0002F1BC
		public override string ToString()
		{
			return this.url;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00030FD4 File Offset: 0x0002F1D4
		public RssChannelCollection Channels
		{
			get
			{
				return this.channels;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00030FEC File Offset: 0x0002F1EC
		public RssModuleCollection Modules
		{
			get
			{
				return this.modules;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00031004 File Offset: 0x0002F204
		public ExceptionCollection Exceptions
		{
			get
			{
				return (this.exceptions == null) ? new ExceptionCollection() : this.exceptions;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0003102C File Offset: 0x0002F22C
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00031044 File Offset: 0x0002F244
		public RssVersion Version
		{
			get
			{
				return this.rssVersion;
			}
			set
			{
				this.rssVersion = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00031050 File Offset: 0x0002F250
		public string ETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00031068 File Offset: 0x0002F268
		public DateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00031080 File Offset: 0x0002F280
		public bool Cached
		{
			get
			{
				return this.cached;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00031098 File Offset: 0x0002F298
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000310B0 File Offset: 0x0002F2B0
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000310C8 File Offset: 0x0002F2C8
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000310D4 File Offset: 0x0002F2D4
		public static RssFeed Read(string url)
		{
			return RssFeed.read(url, null, null);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000310F0 File Offset: 0x0002F2F0
		public static RssFeed Read(HttpWebRequest Request)
		{
			return RssFeed.read(Request.RequestUri.ToString(), Request, null);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00031114 File Offset: 0x0002F314
		public static RssFeed Read(RssFeed oldFeed)
		{
			return RssFeed.read(oldFeed.url, null, oldFeed);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00031134 File Offset: 0x0002F334
		public static RssFeed Read(HttpWebRequest Request, RssFeed oldFeed)
		{
			return RssFeed.read(oldFeed.url, Request, oldFeed);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00031154 File Offset: 0x0002F354
		private static RssFeed read(string url, HttpWebRequest request, RssFeed oldFeed)
		{
			RssFeed rssFeed = new RssFeed();
			Stream stream = null;
			Uri uri = new Uri(url);
			rssFeed.url = url;
			string scheme = uri.Scheme;
			if (!(scheme == "file"))
			{
				if (!(scheme == "https"))
				{
					if (!(scheme == "http"))
					{
						goto IL_01DD;
					}
				}
				bool flag = request == null;
				if (flag)
				{
					request = (HttpWebRequest)WebRequest.Create(uri);
				}
				request.Credentials = Util.GetNetworkCredential();
				string text = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(Util.GetHtaccessUser() + ":" + Util.GetHtaccessPwd()));
				request.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
				request.Headers.Add("Authorization", text);
				request.UserAgent = Util.GetUserAgent();
				request.PreAuthenticate = true;
				bool flag2 = oldFeed != null;
				if (flag2)
				{
					request.IfModifiedSince = oldFeed.LastModified;
					request.Headers.Add("If-None-Match", oldFeed.ETag);
				}
				try
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
					rssFeed.lastModified = httpWebResponse.LastModified;
					rssFeed.etag = httpWebResponse.Headers["ETag"];
					try
					{
						bool flag3 = httpWebResponse.ContentEncoding != "";
						if (flag3)
						{
							rssFeed.encoding = Encoding.GetEncoding(httpWebResponse.ContentEncoding);
						}
					}
					catch
					{
					}
					stream = httpWebResponse.GetResponseStream();
				}
				catch (WebException ex)
				{
					bool flag4 = oldFeed != null;
					if (flag4)
					{
						oldFeed.cached = true;
						return oldFeed;
					}
					throw ex;
				}
			}
			else
			{
				rssFeed.lastModified = File.GetLastWriteTime(url);
				bool flag5 = oldFeed != null && rssFeed.LastModified == oldFeed.LastModified;
				if (flag5)
				{
					oldFeed.cached = true;
					return oldFeed;
				}
				stream = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			}
			IL_01DD:
			bool flag6 = stream != null;
			if (!flag6)
			{
				throw new ApplicationException("Not a valid Url");
			}
			RssReader rssReader = null;
			try
			{
				rssReader = new RssReader(stream);
				RssElement rssElement;
				do
				{
					rssElement = rssReader.Read();
					bool flag7 = rssElement is RssChannel;
					if (flag7)
					{
						rssFeed.Channels.Add((RssChannel)rssElement);
					}
				}
				while (rssElement != null);
				rssFeed.rssVersion = rssReader.Version;
			}
			finally
			{
				rssFeed.exceptions = rssReader.Exceptions;
				rssReader.Close();
			}
			return rssFeed;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000313F0 File Offset: 0x0002F5F0
		public void Write(Stream stream)
		{
			bool flag = this.encoding == null;
			RssWriter rssWriter;
			if (flag)
			{
				rssWriter = new RssWriter(stream);
			}
			else
			{
				rssWriter = new RssWriter(stream, this.encoding);
			}
			this.write(rssWriter);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0003142C File Offset: 0x0002F62C
		public void Write(string fileName)
		{
			RssWriter rssWriter = new RssWriter(fileName);
			this.write(rssWriter);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0003144C File Offset: 0x0002F64C
		private void write(RssWriter writer)
		{
			try
			{
				bool flag = this.channels.Count == 0;
				if (flag)
				{
					throw new InvalidOperationException("Feed must contain at least one channel.");
				}
				writer.Version = this.rssVersion;
				writer.Modules = this.modules;
				foreach (object obj in this.channels)
				{
					RssChannel rssChannel = (RssChannel)obj;
					bool flag2 = rssChannel.Items.Count == 0;
					if (flag2)
					{
						throw new InvalidOperationException("Channel must contain at least one item.");
					}
					writer.Write(rssChannel);
				}
			}
			finally
			{
				bool flag3 = writer != null;
				if (flag3)
				{
					writer.Close();
				}
			}
		}

		// Token: 0x040004E1 RID: 1249
		private RssChannelCollection channels = new RssChannelCollection();

		// Token: 0x040004E2 RID: 1250
		private RssModuleCollection modules = new RssModuleCollection();

		// Token: 0x040004E3 RID: 1251
		private ExceptionCollection exceptions = null;

		// Token: 0x040004E4 RID: 1252
		private DateTime lastModified = RssDefault.DateTime;

		// Token: 0x040004E5 RID: 1253
		private RssVersion rssVersion = RssVersion.Empty;

		// Token: 0x040004E6 RID: 1254
		private bool cached = false;

		// Token: 0x040004E7 RID: 1255
		private string etag = "";

		// Token: 0x040004E8 RID: 1256
		private string url = "";

		// Token: 0x040004E9 RID: 1257
		private Encoding encoding = null;
	}
}
