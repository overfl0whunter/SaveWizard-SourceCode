using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x0200001A RID: 26
	public class ParallelDeflateOutputStream : Stream
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000B5D0 File Offset: 0x000097D0
		public ParallelDeflateOutputStream(Stream stream)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B5DE File Offset: 0x000097DE
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level)
			: this(stream, level, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000B5EC File Offset: 0x000097EC
		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B5FA File Offset: 0x000097FA
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000B608 File Offset: 0x00009808
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			this._outStream = stream;
			this._compressLevel = level;
			this.Strategy = strategy;
			this._leaveOpen = leaveOpen;
			this.MaxBufferPairs = 16;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000B67B File Offset: 0x0000987B
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000B683 File Offset: 0x00009883
		public CompressionStrategy Strategy { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000B68C File Offset: 0x0000988C
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public int MaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				bool flag = value < 4;
				if (flag)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000B6D4 File Offset: 0x000098D4
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000B6EC File Offset: 0x000098EC
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				bool flag = value < 1024;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				this._bufferSize = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000B720 File Offset: 0x00009920
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000B738 File Offset: 0x00009938
		public long BytesProcessed
		{
			get
			{
				return this._totalBytesProcessed;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000B750 File Offset: 0x00009950
		private void _InitializePoolOfWorkItems()
		{
			this._toWrite = new Queue<int>();
			this._toFill = new Queue<int>();
			this._pool = new List<WorkItem>();
			int num = ParallelDeflateOutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this._maxBufferPairs);
			for (int i = 0; i < num; i++)
			{
				this._pool.Add(new WorkItem(this._bufferSize, this._compressLevel, this.Strategy, i));
				this._toFill.Enqueue(i);
			}
			this._newlyCompressedBlob = new AutoResetEvent(false);
			this._runningCrc = new CRC32();
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000B810 File Offset: 0x00009A10
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			bool isClosed = this._isClosed;
			if (isClosed)
			{
				throw new InvalidOperationException();
			}
			bool flag2 = this._pendingException != null;
			if (flag2)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool flag3 = count == 0;
			if (!flag3)
			{
				bool flag4 = !this._firstWriteDone;
				if (flag4)
				{
					this._InitializePoolOfWorkItems();
					this._firstWriteDone = true;
				}
				for (;;)
				{
					this.EmitPendingBuffers(false, flag);
					flag = false;
					bool flag5 = this._currentlyFilling >= 0;
					int num;
					if (flag5)
					{
						num = this._currentlyFilling;
						goto IL_00D2;
					}
					bool flag6 = this._toFill.Count == 0;
					if (!flag6)
					{
						num = this._toFill.Dequeue();
						this._lastFilled++;
						goto IL_00D2;
					}
					flag = true;
					IL_01A9:
					if (count <= 0)
					{
						return;
					}
					continue;
					IL_00D2:
					WorkItem workItem = this._pool[num];
					int num2 = ((workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable));
					workItem.ordinal = this._lastFilled;
					Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
					count -= num2;
					offset += num2;
					workItem.inputBytesAvailable += num2;
					bool flag7 = workItem.inputBytesAvailable == workItem.buffer.Length;
					if (flag7)
					{
						bool flag8 = !ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeflateOne), workItem);
						if (flag8)
						{
							break;
						}
						this._currentlyFilling = -1;
					}
					else
					{
						this._currentlyFilling = num;
					}
					bool flag9 = count > 0;
					if (flag9)
					{
					}
					goto IL_01A9;
				}
				throw new Exception("Cannot enqueue workitem");
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(this._compressLevel, false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			bool flag = num != 1 && num != 0;
			if (flag)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			bool flag2 = array.Length - zlibCodec.AvailableBytesOut > 0;
			if (flag2)
			{
				this._outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			this._Crc32 = this._runningCrc.Crc32Result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		private void _Flush(bool lastInput)
		{
			bool isClosed = this._isClosed;
			if (isClosed)
			{
				throw new InvalidOperationException();
			}
			bool flag = this.emitting;
			if (!flag)
			{
				bool flag2 = this._currentlyFilling >= 0;
				if (flag2)
				{
					WorkItem workItem = this._pool[this._currentlyFilling];
					this._DeflateOne(workItem);
					this._currentlyFilling = -1;
				}
				if (lastInput)
				{
					this.EmitPendingBuffers(true, false);
					this._FlushFinish();
				}
				else
				{
					this.EmitPendingBuffers(false, false);
				}
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000BB28 File Offset: 0x00009D28
		public override void Flush()
		{
			bool flag = this._pendingException != null;
			if (flag)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool handlingException = this._handlingException;
			if (!handlingException)
			{
				this._Flush(false);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000BB78 File Offset: 0x00009D78
		public override void Close()
		{
			bool flag = this._pendingException != null;
			if (flag)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			bool handlingException = this._handlingException;
			if (!handlingException)
			{
				bool isClosed = this._isClosed;
				if (!isClosed)
				{
					this._Flush(true);
					bool flag2 = !this._leaveOpen;
					if (flag2)
					{
						this._outStream.Close();
					}
					this._isClosed = true;
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000BBF3 File Offset: 0x00009DF3
		public new void Dispose()
		{
			this.Close();
			this._pool = null;
			this.Dispose(true);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000BC0C File Offset: 0x00009E0C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000BC18 File Offset: 0x00009E18
		public void Reset(Stream stream)
		{
			bool flag = !this._firstWriteDone;
			if (!flag)
			{
				this._toWrite.Clear();
				this._toFill.Clear();
				foreach (WorkItem workItem in this._pool)
				{
					this._toFill.Enqueue(workItem.index);
					workItem.ordinal = -1;
				}
				this._firstWriteDone = false;
				this._totalBytesProcessed = 0L;
				this._runningCrc = new CRC32();
				this._isClosed = false;
				this._currentlyFilling = -1;
				this._lastFilled = -1;
				this._lastWritten = -1;
				this._latestCompressed = -1;
				this._outStream = stream;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			bool flag = this.emitting;
			if (!flag)
			{
				this.emitting = true;
				bool flag2 = doAll || mustWait;
				if (flag2)
				{
					this._newlyCompressedBlob.WaitOne();
				}
				do
				{
					int num = -1;
					int num2 = (doAll ? 200 : (mustWait ? (-1) : 0));
					int num3 = -1;
					for (;;)
					{
						bool flag3 = Monitor.TryEnter(this._toWrite, num2);
						if (flag3)
						{
							num3 = -1;
							try
							{
								bool flag4 = this._toWrite.Count > 0;
								if (flag4)
								{
									num3 = this._toWrite.Dequeue();
								}
							}
							finally
							{
								Monitor.Exit(this._toWrite);
							}
							bool flag5 = num3 >= 0;
							if (flag5)
							{
								WorkItem workItem = this._pool[num3];
								bool flag6 = workItem.ordinal != this._lastWritten + 1;
								if (flag6)
								{
									Queue<int> toWrite = this._toWrite;
									lock (toWrite)
									{
										this._toWrite.Enqueue(num3);
									}
									bool flag8 = num == num3;
									if (flag8)
									{
										this._newlyCompressedBlob.WaitOne();
										num = -1;
									}
									else
									{
										bool flag9 = num == -1;
										if (flag9)
										{
											num = num3;
										}
									}
								}
								else
								{
									num = -1;
									this._outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
									this._runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
									this._totalBytesProcessed += (long)workItem.inputBytesAvailable;
									workItem.inputBytesAvailable = 0;
									this._lastWritten = workItem.ordinal;
									this._toFill.Enqueue(workItem.index);
									bool flag10 = num2 == -1;
									if (flag10)
									{
										num2 = 0;
									}
								}
							}
						}
						else
						{
							num3 = -1;
						}
						IL_01B7:
						if (num3 < 0)
						{
							break;
						}
						continue;
						goto IL_01B7;
					}
				}
				while (doAll && this._lastWritten != this._latestCompressed);
				this.emitting = false;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000BF0C File Offset: 0x0000A10C
		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				int index = workItem.index;
				CRC32 crc = new CRC32();
				crc.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				this.DeflateOneSegment(workItem);
				workItem.crc = crc.Crc32Result;
				object latestLock = this._latestLock;
				lock (latestLock)
				{
					bool flag2 = workItem.ordinal > this._latestCompressed;
					if (flag2)
					{
						this._latestCompressed = workItem.ordinal;
					}
				}
				Queue<int> toWrite = this._toWrite;
				lock (toWrite)
				{
					this._toWrite.Enqueue(workItem.index);
				}
				this._newlyCompressedBlob.Set();
			}
			catch (Exception ex)
			{
				object eLock = this._eLock;
				lock (eLock)
				{
					bool flag5 = this._pendingException != null;
					if (flag5)
					{
						this._pendingException = ex;
					}
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C05C File Offset: 0x0000A25C
		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate();
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			int num = compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		[Conditional("Trace")]
		private void TraceOutput(ParallelDeflateOutputStream.TraceBits bits, string format, params object[] varParams)
		{
			bool flag = (bits & this._DesiredTrace) > ParallelDeflateOutputStream.TraceBits.None;
			if (flag)
			{
				object outputLock = this._outputLock;
				lock (outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
					Console.Write("{0:000} PDOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000C168 File Offset: 0x0000A368
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000C17C File Offset: 0x0000A37C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000C190 File Offset: 0x0000A390
		public override bool CanWrite
		{
			get
			{
				return this._outStream.CanWrite;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Position
		{
			get
			{
				return this._outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000E3 RID: 227
		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		// Token: 0x040000E4 RID: 228
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x040000E5 RID: 229
		private List<WorkItem> _pool;

		// Token: 0x040000E6 RID: 230
		private bool _leaveOpen;

		// Token: 0x040000E7 RID: 231
		private bool emitting;

		// Token: 0x040000E8 RID: 232
		private Stream _outStream;

		// Token: 0x040000E9 RID: 233
		private int _maxBufferPairs;

		// Token: 0x040000EA RID: 234
		private int _bufferSize = ParallelDeflateOutputStream.IO_BUFFER_SIZE_DEFAULT;

		// Token: 0x040000EB RID: 235
		private AutoResetEvent _newlyCompressedBlob;

		// Token: 0x040000EC RID: 236
		private object _outputLock = new object();

		// Token: 0x040000ED RID: 237
		private bool _isClosed;

		// Token: 0x040000EE RID: 238
		private bool _firstWriteDone;

		// Token: 0x040000EF RID: 239
		private int _currentlyFilling;

		// Token: 0x040000F0 RID: 240
		private int _lastFilled;

		// Token: 0x040000F1 RID: 241
		private int _lastWritten;

		// Token: 0x040000F2 RID: 242
		private int _latestCompressed;

		// Token: 0x040000F3 RID: 243
		private int _Crc32;

		// Token: 0x040000F4 RID: 244
		private CRC32 _runningCrc;

		// Token: 0x040000F5 RID: 245
		private object _latestLock = new object();

		// Token: 0x040000F6 RID: 246
		private Queue<int> _toWrite;

		// Token: 0x040000F7 RID: 247
		private Queue<int> _toFill;

		// Token: 0x040000F8 RID: 248
		private long _totalBytesProcessed;

		// Token: 0x040000F9 RID: 249
		private CompressionLevel _compressLevel;

		// Token: 0x040000FA RID: 250
		private volatile Exception _pendingException;

		// Token: 0x040000FB RID: 251
		private bool _handlingException;

		// Token: 0x040000FC RID: 252
		private object _eLock = new object();

		// Token: 0x040000FD RID: 253
		private ParallelDeflateOutputStream.TraceBits _DesiredTrace = ParallelDeflateOutputStream.TraceBits.EmitLock | ParallelDeflateOutputStream.TraceBits.EmitEnter | ParallelDeflateOutputStream.TraceBits.EmitBegin | ParallelDeflateOutputStream.TraceBits.EmitDone | ParallelDeflateOutputStream.TraceBits.EmitSkip | ParallelDeflateOutputStream.TraceBits.Session | ParallelDeflateOutputStream.TraceBits.Compress | ParallelDeflateOutputStream.TraceBits.WriteEnter | ParallelDeflateOutputStream.TraceBits.WriteTake;

		// Token: 0x020001FE RID: 510
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000D7A RID: 3450
			None = 0U,
			// Token: 0x04000D7B RID: 3451
			NotUsed1 = 1U,
			// Token: 0x04000D7C RID: 3452
			EmitLock = 2U,
			// Token: 0x04000D7D RID: 3453
			EmitEnter = 4U,
			// Token: 0x04000D7E RID: 3454
			EmitBegin = 8U,
			// Token: 0x04000D7F RID: 3455
			EmitDone = 16U,
			// Token: 0x04000D80 RID: 3456
			EmitSkip = 32U,
			// Token: 0x04000D81 RID: 3457
			EmitAll = 58U,
			// Token: 0x04000D82 RID: 3458
			Flush = 64U,
			// Token: 0x04000D83 RID: 3459
			Lifecycle = 128U,
			// Token: 0x04000D84 RID: 3460
			Session = 256U,
			// Token: 0x04000D85 RID: 3461
			Synch = 512U,
			// Token: 0x04000D86 RID: 3462
			Instance = 1024U,
			// Token: 0x04000D87 RID: 3463
			Compress = 2048U,
			// Token: 0x04000D88 RID: 3464
			Write = 4096U,
			// Token: 0x04000D89 RID: 3465
			WriteEnter = 8192U,
			// Token: 0x04000D8A RID: 3466
			WriteTake = 16384U,
			// Token: 0x04000D8B RID: 3467
			All = 4294967295U
		}
	}
}
