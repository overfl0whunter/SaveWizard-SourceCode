using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200006F RID: 111
	public class TestStatus
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00021FA2 File Offset: 0x000201A2
		public TestStatus(ZipFile file)
		{
			this.file_ = file;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00021FB4 File Offset: 0x000201B4
		public TestOperation Operation
		{
			get
			{
				return this.operation_;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00021FCC File Offset: 0x000201CC
		public ZipFile File
		{
			get
			{
				return this.file_;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00021FE4 File Offset: 0x000201E4
		public ZipEntry Entry
		{
			get
			{
				return this.entry_;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00021FFC File Offset: 0x000201FC
		public int ErrorCount
		{
			get
			{
				return this.errorCount_;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00022014 File Offset: 0x00020214
		public long BytesTested
		{
			get
			{
				return this.bytesTested_;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0002202C File Offset: 0x0002022C
		public bool EntryValid
		{
			get
			{
				return this.entryValid_;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00022044 File Offset: 0x00020244
		internal void AddError()
		{
			this.errorCount_++;
			this.entryValid_ = false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002205C File Offset: 0x0002025C
		internal void SetOperation(TestOperation operation)
		{
			this.operation_ = operation;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00022066 File Offset: 0x00020266
		internal void SetEntry(ZipEntry entry)
		{
			this.entry_ = entry;
			this.entryValid_ = true;
			this.bytesTested_ = 0L;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002207F File Offset: 0x0002027F
		internal void SetBytesTested(long value)
		{
			this.bytesTested_ = value;
		}

		// Token: 0x0400037A RID: 890
		private ZipFile file_;

		// Token: 0x0400037B RID: 891
		private ZipEntry entry_;

		// Token: 0x0400037C RID: 892
		private bool entryValid_;

		// Token: 0x0400037D RID: 893
		private int errorCount_;

		// Token: 0x0400037E RID: 894
		private long bytesTested_;

		// Token: 0x0400037F RID: 895
		private TestOperation operation_;
	}
}
