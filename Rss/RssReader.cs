using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace Rss
{
	// Token: 0x020000C9 RID: 201
	public class RssReader
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x00032481 File Offset: 0x00030681
		private void InitReader()
		{
			this.reader.WhitespaceHandling = WhitespaceHandling.None;
			this.reader.XmlResolver = null;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000324A0 File Offset: 0x000306A0
		public RssReader(string url)
		{
			try
			{
				this.reader = new XmlTextReader(url);
				this.InitReader();
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Unable to retrieve file containing the RSS data.", ex);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00032560 File Offset: 0x00030760
		public RssReader(TextReader textReader)
		{
			try
			{
				this.reader = new XmlTextReader(textReader);
				this.InitReader();
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Unable to retrieve file containing the RSS data.", ex);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00032620 File Offset: 0x00030820
		public RssReader(Stream stream)
		{
			try
			{
				this.reader = new XmlTextReader(stream);
				this.InitReader();
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Unable to retrieve file containing the RSS data.", ex);
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000326E0 File Offset: 0x000308E0
		public RssElement Read()
		{
			bool flag = false;
			bool flag2 = true;
			RssElement rssElement = null;
			int num = -1;
			int num2 = -1;
			bool flag3 = this.reader == null;
			if (flag3)
			{
				throw new InvalidOperationException("RssReader has been closed, and can not be read.");
			}
			do
			{
				flag2 = true;
				try
				{
					flag = this.reader.Read();
				}
				catch (EndOfStreamException ex)
				{
					throw new EndOfStreamException("Unable to read an RssElement. Reached the end of the stream.", ex);
				}
				catch (XmlException ex2)
				{
					bool flag4 = num != -1 || num2 != -1;
					if (flag4)
					{
						bool flag5 = this.reader.LineNumber == num && this.reader.LinePosition == num2;
						if (flag5)
						{
							throw this.exceptions.LastException;
						}
					}
					num = this.reader.LineNumber;
					num2 = this.reader.LinePosition;
					this.exceptions.Add(ex2);
				}
				bool flag6 = flag;
				if (flag6)
				{
					string text = this.reader.Name.ToLower();
					XmlNodeType nodeType = this.reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						bool isEmptyElement = this.reader.IsEmptyElement;
						if (!isEmptyElement)
						{
							this.elementText = new StringBuilder();
							string text2 = text;
							uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text2);
							if (num3 <= 748388108U)
							{
								if (num3 <= 466561496U)
								{
									if (num3 != 449227134U)
									{
										if (num3 == 466561496U)
										{
											if (text2 == "source")
											{
												this.source = new RssSource();
												this.item.Source = this.source;
												for (int i = 0; i < this.reader.AttributeCount; i++)
												{
													this.reader.MoveToAttribute(i);
													string text3 = this.reader.Name.ToLower();
													if (text3 == "url")
													{
														try
														{
															this.source.Url = new Uri(this.reader.Value);
														}
														catch (Exception ex3)
														{
															this.exceptions.Add(ex3);
														}
													}
												}
											}
										}
									}
									else if (text2 == "cloud")
									{
										flag2 = false;
										this.cloud = new RssCloud();
										this.channel.Cloud = this.cloud;
										for (int j = 0; j < this.reader.AttributeCount; j++)
										{
											this.reader.MoveToAttribute(j);
											string text4 = this.reader.Name.ToLower();
											if (!(text4 == "domain"))
											{
												if (!(text4 == "port"))
												{
													if (!(text4 == "path"))
													{
														if (!(text4 == "registerprocedure"))
														{
															if (text4 == "protocol")
															{
																string text5 = this.reader.Value.ToLower();
																if (!(text5 == "xml-rpc"))
																{
																	if (!(text5 == "soap"))
																	{
																		if (!(text5 == "http-post"))
																		{
																			this.cloud.Protocol = RssCloudProtocol.Empty;
																		}
																		else
																		{
																			this.cloud.Protocol = RssCloudProtocol.HttpPost;
																		}
																	}
																	else
																	{
																		this.cloud.Protocol = RssCloudProtocol.Soap;
																	}
																}
																else
																{
																	this.cloud.Protocol = RssCloudProtocol.XmlRpc;
																}
															}
														}
														else
														{
															this.cloud.RegisterProcedure = this.reader.Value;
														}
													}
													else
													{
														this.cloud.Path = this.reader.Value;
													}
												}
												else
												{
													try
													{
														this.cloud.Port = (int)ushort.Parse(this.reader.Value);
													}
													catch (Exception ex4)
													{
														this.exceptions.Add(ex4);
													}
												}
											}
											else
											{
												this.cloud.Domain = this.reader.Value;
											}
										}
									}
								}
								else if (num3 != 566383268U)
								{
									if (num3 != 620139359U)
									{
										if (num3 == 748388108U)
										{
											if (text2 == "guid")
											{
												this.guid = new RssGuid();
												this.item.Guid = this.guid;
												for (int k = 0; k < this.reader.AttributeCount; k++)
												{
													this.reader.MoveToAttribute(k);
													string text6 = this.reader.Name.ToLower();
													if (text6 == "ispermalink")
													{
														try
														{
															this.guid.PermaLink = bool.Parse(this.reader.Value);
														}
														catch (Exception ex5)
														{
															this.exceptions.Add(ex5);
														}
													}
												}
											}
										}
									}
									else if (text2 == "rdf")
									{
										for (int l = 0; l < this.reader.AttributeCount; l++)
										{
											this.reader.MoveToAttribute(l);
											bool flag7 = this.reader.Name.ToLower() == "version";
											if (flag7)
											{
												string value = this.reader.Value;
												if (!(value == "0.90"))
												{
													if (!(value == "1.0"))
													{
														this.rssVersion = RssVersion.NotSupported;
													}
													else
													{
														this.rssVersion = RssVersion.RSS10;
													}
												}
												else
												{
													this.rssVersion = RssVersion.RSS090;
												}
											}
										}
									}
								}
								else if (text2 == "channel")
								{
									this.channel = new RssChannel();
									this.textInput = null;
									this.image = null;
									this.cloud = null;
									this.source = null;
									this.enclosure = null;
									this.category = null;
									this.item = null;
								}
							}
							else if (num3 <= 2671260646U)
							{
								if (num3 != 802971955U)
								{
									if (num3 != 2240451170U)
									{
										if (num3 == 2671260646U)
										{
											if (text2 == "item")
											{
												bool flag8 = !this.wroteChannel;
												if (flag8)
												{
													this.wroteChannel = true;
													rssElement = this.channel;
													flag = false;
												}
												this.item = new RssItem();
												this.channel.Items.Add(this.item);
											}
										}
									}
									else if (text2 == "textinput")
									{
										this.textInput = new RssTextInput();
										this.channel.TextInput = this.textInput;
									}
								}
								else if (text2 == "rss")
								{
									for (int m = 0; m < this.reader.AttributeCount; m++)
									{
										this.reader.MoveToAttribute(m);
										bool flag9 = this.reader.Name.ToLower() == "version";
										if (flag9)
										{
											string value2 = this.reader.Value;
											if (!(value2 == "0.91"))
											{
												if (!(value2 == "0.92"))
												{
													if (!(value2 == "2.0"))
													{
														this.rssVersion = RssVersion.NotSupported;
													}
													else
													{
														this.rssVersion = RssVersion.RSS20;
													}
												}
												else
												{
													this.rssVersion = RssVersion.RSS092;
												}
											}
											else
											{
												this.rssVersion = RssVersion.RSS091;
											}
										}
									}
								}
							}
							else if (num3 != 3008443898U)
							{
								if (num3 != 3104234203U)
								{
									if (num3 == 3475980913U)
									{
										if (text2 == "category")
										{
											this.category = new RssCategory();
											bool flag10 = (string)this.xmlNodeStack.Peek() == "channel";
											if (flag10)
											{
												this.channel.Categories.Add(this.category);
											}
											else
											{
												this.item.Categories.Add(this.category);
											}
											int n = 0;
											while (n < this.reader.AttributeCount)
											{
												this.reader.MoveToAttribute(n);
												string text7 = this.reader.Name.ToLower();
												if (text7 == "url")
												{
													goto IL_0656;
												}
												if (text7 == "domain")
												{
													goto IL_0656;
												}
												IL_066F:
												n++;
												continue;
												IL_0656:
												this.category.Domain = this.reader.Value;
												goto IL_066F;
											}
										}
									}
								}
								else if (text2 == "enclosure")
								{
									this.enclosure = new RssEnclosure();
									this.item.Enclosure = this.enclosure;
									for (int num4 = 0; num4 < this.reader.AttributeCount; num4++)
									{
										this.reader.MoveToAttribute(num4);
										string text8 = this.reader.Name.ToLower();
										if (!(text8 == "url"))
										{
											if (!(text8 == "length"))
											{
												if (text8 == "type")
												{
													this.enclosure.Type = this.reader.Value;
												}
											}
											else
											{
												try
												{
													this.enclosure.Length = int.Parse(this.reader.Value);
												}
												catch (Exception ex6)
												{
													this.exceptions.Add(ex6);
												}
											}
										}
										else
										{
											try
											{
												this.enclosure.Url = new Uri(this.reader.Value);
											}
											catch (Exception ex7)
											{
												this.exceptions.Add(ex7);
											}
										}
									}
								}
							}
							else if (text2 == "image")
							{
								this.image = new RssImage();
								this.channel.Image = this.image;
							}
							bool flag11 = flag2;
							if (flag11)
							{
								this.xmlNodeStack.Push(text);
							}
						}
						break;
					}
					case XmlNodeType.Attribute:
						break;
					case XmlNodeType.Text:
						this.elementText.Append(this.reader.Value);
						break;
					case XmlNodeType.CDATA:
						this.elementText.Append(this.reader.Value);
						break;
					default:
						if (nodeType == XmlNodeType.EndElement)
						{
							bool flag12 = this.xmlNodeStack.Count == 1;
							if (!flag12)
							{
								string text9 = (string)this.xmlNodeStack.Pop();
								string text10 = (string)this.xmlNodeStack.Peek();
								string text11 = text9;
								uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text11);
								if (num3 <= 748388108U)
								{
									if (num3 <= 466561496U)
									{
										if (num3 != 449227134U)
										{
											if (num3 == 466561496U)
											{
												if (text11 == "source")
												{
													this.source.Name = this.elementText.ToString();
													rssElement = this.source;
													flag = false;
												}
											}
										}
										else if (text11 == "cloud")
										{
											rssElement = this.cloud;
											flag = false;
										}
									}
									else if (num3 != 566383268U)
									{
										if (num3 == 748388108U)
										{
											if (text11 == "guid")
											{
												this.guid.Name = this.elementText.ToString();
												rssElement = this.guid;
												flag = false;
											}
										}
									}
									else if (text11 == "channel")
									{
										bool flag13 = this.wroteChannel;
										if (flag13)
										{
											this.wroteChannel = false;
										}
										else
										{
											this.wroteChannel = true;
											rssElement = this.channel;
											flag = false;
										}
									}
								}
								else if (num3 <= 2671260646U)
								{
									if (num3 != 2240451170U)
									{
										if (num3 == 2671260646U)
										{
											if (text11 == "item")
											{
												rssElement = this.item;
												flag = false;
											}
										}
									}
									else if (text11 == "textinput")
									{
										rssElement = this.textInput;
										flag = false;
									}
								}
								else if (num3 != 3008443898U)
								{
									if (num3 != 3104234203U)
									{
										if (num3 == 3475980913U)
										{
											if (text11 == "category")
											{
												this.category.Name = this.elementText.ToString();
												rssElement = this.category;
												flag = false;
											}
										}
									}
									else if (text11 == "enclosure")
									{
										rssElement = this.enclosure;
										flag = false;
									}
								}
								else if (text11 == "image")
								{
									rssElement = this.image;
									flag = false;
								}
								string text12 = text10;
								if (!(text12 == "item"))
								{
									if (!(text12 == "channel"))
									{
										if (!(text12 == "image"))
										{
											if (!(text12 == "textinput"))
											{
												if (!(text12 == "skipdays"))
												{
													if (text12 == "skiphours")
													{
														bool flag14 = text9 == "hour";
														if (flag14)
														{
															this.channel.SkipHours[(int)byte.Parse(this.elementText.ToString().ToLower())] = true;
														}
													}
												}
												else
												{
													bool flag15 = text9 == "day";
													if (flag15)
													{
														string text13 = this.elementText.ToString().ToLower();
														num3 = <PrivateImplementationDetails>.ComputeStringHash(text13);
														if (num3 <= 640179472U)
														{
															if (num3 != 303888406U)
															{
																if (num3 != 304015799U)
																{
																	if (num3 == 640179472U)
																	{
																		if (text13 == "tuesday")
																		{
																			this.channel.SkipDays[1] = true;
																		}
																	}
																}
																else if (text13 == "thursday")
																{
																	this.channel.SkipDays[3] = true;
																}
															}
															else if (text13 == "saturday")
															{
																this.channel.SkipDays[5] = true;
															}
														}
														else if (num3 <= 1291458361U)
														{
															if (num3 != 1236747314U)
															{
																if (num3 == 1291458361U)
																{
																	if (text13 == "monday")
																	{
																		this.channel.SkipDays[0] = true;
																	}
																}
															}
															else if (text13 == "friday")
															{
																this.channel.SkipDays[4] = true;
															}
														}
														else if (num3 != 2754191577U)
														{
															if (num3 == 3281405399U)
															{
																if (text13 == "wednesday")
																{
																	this.channel.SkipDays[2] = true;
																}
															}
														}
														else if (text13 == "sunday")
														{
															this.channel.SkipDays[6] = true;
														}
													}
												}
											}
											else
											{
												string text14 = text9;
												if (!(text14 == "title"))
												{
													if (!(text14 == "description"))
													{
														if (!(text14 == "name"))
														{
															if (text14 == "link")
															{
																try
																{
																	this.textInput.Link = new Uri(this.elementText.ToString());
																}
																catch (Exception ex8)
																{
																	this.exceptions.Add(ex8);
																}
															}
														}
														else
														{
															this.textInput.Name = this.elementText.ToString();
														}
													}
													else
													{
														this.textInput.Description = this.elementText.ToString();
													}
												}
												else
												{
													this.textInput.Title = this.elementText.ToString();
												}
											}
										}
										else
										{
											string text15 = text9;
											if (!(text15 == "url"))
											{
												if (!(text15 == "title"))
												{
													if (!(text15 == "link"))
													{
														if (!(text15 == "description"))
														{
															if (!(text15 == "width"))
															{
																if (text15 == "height")
																{
																	try
																	{
																		this.image.Height = (int)byte.Parse(this.elementText.ToString());
																	}
																	catch (Exception ex9)
																	{
																		this.exceptions.Add(ex9);
																	}
																}
															}
															else
															{
																try
																{
																	this.image.Width = (int)byte.Parse(this.elementText.ToString());
																}
																catch (Exception ex10)
																{
																	this.exceptions.Add(ex10);
																}
															}
														}
														else
														{
															this.image.Description = this.elementText.ToString();
														}
													}
													else
													{
														try
														{
															this.image.Link = new Uri(this.elementText.ToString());
														}
														catch (Exception ex11)
														{
															this.exceptions.Add(ex11);
														}
													}
												}
												else
												{
													this.image.Title = this.elementText.ToString();
												}
											}
											else
											{
												try
												{
													this.image.Url = new Uri(this.elementText.ToString());
												}
												catch (Exception ex12)
												{
													this.exceptions.Add(ex12);
												}
											}
										}
									}
									else
									{
										string text16 = text9;
										num3 = <PrivateImplementationDetails>.ComputeStringHash(text16);
										if (num3 <= 1860974018U)
										{
											if (num3 <= 310887988U)
											{
												if (num3 != 105518051U)
												{
													if (num3 != 232457833U)
													{
														if (num3 == 310887988U)
														{
															if (text16 == "managingeditor")
															{
																this.channel.ManagingEditor = this.elementText.ToString();
															}
														}
													}
													else if (text16 == "link")
													{
														try
														{
															this.channel.Link = new Uri(this.elementText.ToString());
														}
														catch (Exception ex13)
														{
															this.exceptions.Add(ex13);
														}
													}
												}
												else if (text16 == "lastbuilddate")
												{
													try
													{
														this.channel.LastBuildDate = DateTime.Parse(this.elementText.ToString());
													}
													catch (Exception ex14)
													{
														this.exceptions.Add(ex14);
													}
												}
											}
											else if (num3 != 686503122U)
											{
												if (num3 != 879704937U)
												{
													if (num3 == 1860974018U)
													{
														if (text16 == "generator")
														{
															this.channel.Generator = this.elementText.ToString();
														}
													}
												}
												else if (text16 == "description")
												{
													this.channel.Description = this.elementText.ToString();
												}
											}
											else if (text16 == "docs")
											{
												this.channel.Docs = this.elementText.ToString();
											}
										}
										else if (num3 <= 2556802313U)
										{
											if (num3 != 2207416544U)
											{
												if (num3 != 2223235319U)
												{
													if (num3 == 2556802313U)
													{
														if (text16 == "title")
														{
															this.channel.Title = this.elementText.ToString();
														}
													}
												}
												else if (text16 == "webmaster")
												{
													this.channel.WebMaster = this.elementText.ToString();
												}
											}
											else if (text16 == "pubdate")
											{
												try
												{
													this.channel.PubDate = DateTime.Parse(this.elementText.ToString());
												}
												catch (Exception ex15)
												{
													this.exceptions.Add(ex15);
												}
											}
										}
										else if (num3 <= 3119462523U)
										{
											if (num3 != 3104697662U)
											{
												if (num3 == 3119462523U)
												{
													if (text16 == "language")
													{
														this.channel.Language = this.elementText.ToString();
													}
												}
											}
											else if (text16 == "copyright")
											{
												this.channel.Copyright = this.elementText.ToString();
											}
										}
										else if (num3 != 3173728859U)
										{
											if (num3 == 4069068880U)
											{
												if (text16 == "rating")
												{
													this.channel.Rating = this.elementText.ToString();
												}
											}
										}
										else if (text16 == "ttl")
										{
											try
											{
												this.channel.TimeToLive = int.Parse(this.elementText.ToString());
											}
											catch (Exception ex16)
											{
												this.exceptions.Add(ex16);
											}
										}
									}
								}
								else
								{
									string text17 = text9;
									if (!(text17 == "title"))
									{
										if (!(text17 == "link"))
										{
											if (!(text17 == "description"))
											{
												if (!(text17 == "author"))
												{
													if (!(text17 == "comments"))
													{
														if (text17 == "pubdate")
														{
															try
															{
																this.item.PubDate = DateTime.Parse(this.elementText.ToString());
															}
															catch (Exception ex17)
															{
																try
																{
																	string text18 = this.elementText.ToString();
																	text18 = text18.Substring(0, text18.Length - 5);
																	text18 += "GMT";
																	this.item.PubDate = DateTime.Parse(text18);
																}
																catch
																{
																	this.exceptions.Add(ex17);
																}
															}
														}
													}
													else
													{
														this.item.Comments = this.elementText.ToString();
													}
												}
												else
												{
													this.item.Author = this.elementText.ToString();
												}
											}
											else
											{
												this.item.Description = this.elementText.ToString();
											}
										}
										else
										{
											bool flag16 = this.elementText.Length > 0;
											if (flag16)
											{
												this.item.Link = new Uri(this.elementText.ToString());
											}
											else
											{
												this.item.Link = null;
											}
										}
									}
									else
									{
										this.item.Title = this.elementText.ToString();
									}
								}
							}
						}
						break;
					}
				}
			}
			while (flag);
			return rssElement;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00033EE8 File Offset: 0x000320E8
		public ExceptionCollection Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00033F00 File Offset: 0x00032100
		public RssVersion Version
		{
			get
			{
				return this.rssVersion;
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00033F18 File Offset: 0x00032118
		public void Close()
		{
			this.textInput = null;
			this.image = null;
			this.cloud = null;
			this.channel = null;
			this.source = null;
			this.enclosure = null;
			this.category = null;
			this.item = null;
			bool flag = this.reader != null;
			if (flag)
			{
				this.reader.Close();
				this.reader = null;
			}
			this.elementText = null;
			this.xmlNodeStack = null;
		}

		// Token: 0x04000504 RID: 1284
		private Stack xmlNodeStack = new Stack();

		// Token: 0x04000505 RID: 1285
		private StringBuilder elementText = new StringBuilder();

		// Token: 0x04000506 RID: 1286
		private XmlTextReader reader = null;

		// Token: 0x04000507 RID: 1287
		private bool wroteChannel = false;

		// Token: 0x04000508 RID: 1288
		private RssVersion rssVersion = RssVersion.Empty;

		// Token: 0x04000509 RID: 1289
		private ExceptionCollection exceptions = new ExceptionCollection();

		// Token: 0x0400050A RID: 1290
		private RssTextInput textInput = null;

		// Token: 0x0400050B RID: 1291
		private RssImage image = null;

		// Token: 0x0400050C RID: 1292
		private RssCloud cloud = null;

		// Token: 0x0400050D RID: 1293
		private RssChannel channel = null;

		// Token: 0x0400050E RID: 1294
		private RssSource source = null;

		// Token: 0x0400050F RID: 1295
		private RssEnclosure enclosure = null;

		// Token: 0x04000510 RID: 1296
		private RssGuid guid = null;

		// Token: 0x04000511 RID: 1297
		private RssCategory category = null;

		// Token: 0x04000512 RID: 1298
		private RssItem item = null;
	}
}
