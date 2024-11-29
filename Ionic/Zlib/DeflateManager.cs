using System;

namespace Ionic.Zlib
{
	// Token: 0x02000011 RID: 17
	internal sealed class DeflateManager
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003FF4 File Offset: 0x000021F4
		internal DeflateManager()
		{
			this.dyn_ltree = new short[DeflateManager.HEAP_SIZE * 2];
			this.dyn_dtree = new short[(2 * InternalConstants.D_CODES + 1) * 2];
			this.bl_tree = new short[(2 * InternalConstants.BL_CODES + 1) * 2];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000040B0 File Offset: 0x000022B0
		private void _InitializeLazyMatch()
		{
			this.window_size = 2 * this.w_size;
			Array.Clear(this.head, 0, this.hash_size);
			this.config = DeflateManager.Config.Lookup(this.compressionLevel);
			this.SetDeflater();
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			this.match_length = (this.prev_length = DeflateManager.MIN_MATCH - 1);
			this.match_available = 0;
			this.ins_h = 0;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004130 File Offset: 0x00002330
		private void _InitializeTreeData()
		{
			this.treeLiterals.dyn_tree = this.dyn_ltree;
			this.treeLiterals.staticTree = StaticTree.Literals;
			this.treeDistances.dyn_tree = this.dyn_dtree;
			this.treeDistances.staticTree = StaticTree.Distances;
			this.treeBitLengths.dyn_tree = this.bl_tree;
			this.treeBitLengths.staticTree = StaticTree.BitLengths;
			this.bi_buf = 0;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this._InitializeBlocks();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000041C0 File Offset: 0x000023C0
		internal void _InitializeBlocks()
		{
			for (int i = 0; i < InternalConstants.L_CODES; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < InternalConstants.D_CODES; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < InternalConstants.BL_CODES; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[DeflateManager.END_BLOCK * 2] = 1;
			this.opt_len = (this.static_len = 0);
			this.last_lit = (this.matches = 0);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004268 File Offset: 0x00002468
		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i <<= 1)
			{
				bool flag = i < this.heap_len && DeflateManager._IsSmaller(tree, this.heap[i + 1], this.heap[i], this.depth);
				if (flag)
				{
					i++;
				}
				bool flag2 = DeflateManager._IsSmaller(tree, num, this.heap[i], this.depth);
				if (flag2)
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004308 File Offset: 0x00002508
		internal static bool _IsSmaller(short[] tree, int n, int m, sbyte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			return num < num2 || (num == num2 && depth[n] <= depth[m]);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004340 File Offset: 0x00002540
		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			bool flag = num2 == 0;
			if (flag)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = short.MaxValue;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				bool flag2 = ++num3 < num4 && num6 == num2;
				if (!flag2)
				{
					bool flag3 = num3 < num5;
					if (flag3)
					{
						this.bl_tree[num6 * 2] = (short)((int)this.bl_tree[num6 * 2] + num3);
					}
					else
					{
						bool flag4 = num6 != 0;
						if (flag4)
						{
							bool flag5 = num6 != num;
							if (flag5)
							{
								short[] array = this.bl_tree;
								int num7 = num6 * 2;
								array[num7] += 1;
							}
							short[] array2 = this.bl_tree;
							int num8 = InternalConstants.REP_3_6 * 2;
							array2[num8] += 1;
						}
						else
						{
							bool flag6 = num3 <= 10;
							if (flag6)
							{
								short[] array3 = this.bl_tree;
								int num9 = InternalConstants.REPZ_3_10 * 2;
								array3[num9] += 1;
							}
							else
							{
								short[] array4 = this.bl_tree;
								int num10 = InternalConstants.REPZ_11_138 * 2;
								array4[num10] += 1;
							}
						}
					}
					num3 = 0;
					num = num6;
					bool flag7 = num2 == 0;
					if (flag7)
					{
						num4 = 138;
						num5 = 3;
					}
					else
					{
						bool flag8 = num6 == num2;
						if (flag8)
						{
							num4 = 6;
							num5 = 3;
						}
						else
						{
							num4 = 7;
							num5 = 4;
						}
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000044B8 File Offset: 0x000026B8
		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.treeLiterals.max_code);
			this.scan_tree(this.dyn_dtree, this.treeDistances.max_code);
			this.treeBitLengths.build_tree(this);
			int i;
			for (i = InternalConstants.BL_CODES - 1; i >= 3; i--)
			{
				bool flag = this.bl_tree[(int)(Tree.bl_order[i] * 2 + 1)] != 0;
				if (flag)
				{
					break;
				}
			}
			this.opt_len += 3 * (i + 1) + 5 + 5 + 4;
			return i;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004558 File Offset: 0x00002758
		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits((int)this.bl_tree[(int)(Tree.bl_order[i] * 2 + 1)], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000045D8 File Offset: 0x000027D8
		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			bool flag = num2 == 0;
			if (flag)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				bool flag2 = ++num3 < num4 && num6 == num2;
				if (!flag2)
				{
					bool flag3 = num3 < num5;
					if (flag3)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
						}
						while (--num3 != 0);
					}
					else
					{
						bool flag4 = num6 != 0;
						if (flag4)
						{
							bool flag5 = num6 != num;
							if (flag5)
							{
								this.send_code(num6, this.bl_tree);
								num3--;
							}
							this.send_code(InternalConstants.REP_3_6, this.bl_tree);
							this.send_bits(num3 - 3, 2);
						}
						else
						{
							bool flag6 = num3 <= 10;
							if (flag6)
							{
								this.send_code(InternalConstants.REPZ_3_10, this.bl_tree);
								this.send_bits(num3 - 3, 3);
							}
							else
							{
								this.send_code(InternalConstants.REPZ_11_138, this.bl_tree);
								this.send_bits(num3 - 11, 7);
							}
						}
					}
					num3 = 0;
					num = num6;
					bool flag7 = num2 == 0;
					if (flag7)
					{
						num4 = 138;
						num5 = 3;
					}
					else
					{
						bool flag8 = num6 == num2;
						if (flag8)
						{
							num4 = 6;
							num5 = 3;
						}
						else
						{
							num4 = 7;
							num5 = 4;
						}
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000475F File Offset: 0x0000295F
		private void put_bytes(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending, this.pendingCount, len);
			this.pendingCount += len;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004788 File Offset: 0x00002988
		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			this.send_bits((int)tree[num] & 65535, (int)tree[num + 1] & 65535);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000047B8 File Offset: 0x000029B8
		internal void send_bits(int value, int length)
		{
			bool flag = this.bi_valid > DeflateManager.Buf_size - length;
			if (flag)
			{
				this.bi_buf |= (short)((value << this.bi_valid) & 65535);
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = (short)((uint)value >> DeflateManager.Buf_size - this.bi_valid);
				this.bi_valid += length - DeflateManager.Buf_size;
			}
			else
			{
				this.bi_buf |= (short)((value << this.bi_valid) & 65535);
				this.bi_valid += length;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000048A0 File Offset: 0x00002AA0
		internal void _tr_align()
		{
			this.send_bits(DeflateManager.STATIC_TREES << 1, 3);
			this.send_code(DeflateManager.END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
			this.bi_flush();
			bool flag = 1 + this.last_eob_len + 10 - this.bi_valid < 9;
			if (flag)
			{
				this.send_bits(DeflateManager.STATIC_TREES << 1, 3);
				this.send_code(DeflateManager.END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004920 File Offset: 0x00002B20
		internal bool _tr_tally(int dist, int lc)
		{
			this.pending[this._distanceOffset + this.last_lit * 2] = (byte)((uint)dist >> 8);
			this.pending[this._distanceOffset + this.last_lit * 2 + 1] = (byte)dist;
			this.pending[this._lengthOffset + this.last_lit] = (byte)lc;
			this.last_lit++;
			bool flag = dist == 0;
			if (flag)
			{
				short[] array = this.dyn_ltree;
				int num = lc * 2;
				array[num] += 1;
			}
			else
			{
				this.matches++;
				dist--;
				short[] array2 = this.dyn_ltree;
				int num2 = ((int)Tree.LengthCode[lc] + InternalConstants.LITERALS + 1) * 2;
				array2[num2] += 1;
				short[] array3 = this.dyn_dtree;
				int num3 = Tree.DistanceCode(dist) * 2;
				array3[num3] += 1;
			}
			bool flag2 = (this.last_lit & 8191) == 0 && this.compressionLevel > CompressionLevel.Level2;
			if (flag2)
			{
				int num4 = this.last_lit << 3;
				int num5 = this.strstart - this.block_start;
				for (int i = 0; i < InternalConstants.D_CODES; i++)
				{
					num4 = (int)((long)num4 + (long)this.dyn_dtree[i * 2] * (5L + (long)Tree.ExtraDistanceBits[i]));
				}
				num4 >>= 3;
				bool flag3 = this.matches < this.last_lit / 2 && num4 < num5 / 2;
				if (flag3)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1 || this.last_lit == this.lit_bufsize;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004AB8 File Offset: 0x00002CB8
		internal void send_compressed_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			bool flag = this.last_lit != 0;
			if (flag)
			{
				do
				{
					int num2 = this._distanceOffset + num * 2;
					int num3 = (((int)this.pending[num2] << 8) & 65280) | (int)(this.pending[num2 + 1] & byte.MaxValue);
					int num4 = (int)(this.pending[this._lengthOffset + num] & byte.MaxValue);
					num++;
					bool flag2 = num3 == 0;
					if (flag2)
					{
						this.send_code(num4, ltree);
					}
					else
					{
						int num5 = (int)Tree.LengthCode[num4];
						this.send_code(num5 + InternalConstants.LITERALS + 1, ltree);
						int num6 = Tree.ExtraLengthBits[num5];
						bool flag3 = num6 != 0;
						if (flag3)
						{
							num4 -= Tree.LengthBase[num5];
							this.send_bits(num4, num6);
						}
						num3--;
						num5 = Tree.DistanceCode(num3);
						this.send_code(num5, dtree);
						num6 = Tree.ExtraDistanceBits[num5];
						bool flag4 = num6 != 0;
						if (flag4)
						{
							num3 -= Tree.DistanceBase[num5];
							this.send_bits(num3, num6);
						}
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(DeflateManager.END_BLOCK, ltree);
			this.last_eob_len = (int)ltree[DeflateManager.END_BLOCK * 2 + 1];
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004BF8 File Offset: 0x00002DF8
		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 7)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 128)
			{
				num += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < InternalConstants.LITERALS)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			this.data_type = (sbyte)((num2 > num >> 2) ? DeflateManager.Z_BINARY : DeflateManager.Z_ASCII);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004C88 File Offset: 0x00002E88
		internal void bi_flush()
		{
			bool flag = this.bi_valid == 16;
			if (flag)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = 0;
				this.bi_valid = 0;
			}
			else
			{
				bool flag2 = this.bi_valid >= 8;
				if (flag2)
				{
					byte[] array3 = this.pending;
					int num = this.pendingCount;
					this.pendingCount = num + 1;
					array3[num] = (byte)this.bi_buf;
					this.bi_buf = (short)(this.bi_buf >> 8);
					this.bi_valid -= 8;
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004D44 File Offset: 0x00002F44
		internal void bi_windup()
		{
			bool flag = this.bi_valid > 8;
			if (flag)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
			}
			else
			{
				bool flag2 = this.bi_valid > 0;
				if (flag2)
				{
					byte[] array3 = this.pending;
					int num = this.pendingCount;
					this.pendingCount = num + 1;
					array3[num] = (byte)this.bi_buf;
				}
			}
			this.bi_buf = 0;
			this.bi_valid = 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004DE0 File Offset: 0x00002FE0
		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)len;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(len >> 8);
				byte[] array3 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array3[num] = (byte)(~(byte)len);
				byte[] array4 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array4[num] = (byte)(~len >> 8);
			}
			this.put_bytes(this.window, buf, len);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004E80 File Offset: 0x00003080
		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0) ? this.block_start : (-1), this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this._codec.flush_pending();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004ED0 File Offset: 0x000030D0
		internal BlockState DeflateNone(FlushType flush)
		{
			int num = 65535;
			bool flag = num > this.pending.Length - 5;
			if (flag)
			{
				num = this.pending.Length - 5;
			}
			for (;;)
			{
				bool flag2 = this.lookahead <= 1;
				if (flag2)
				{
					this._fillWindow();
					bool flag3 = this.lookahead == 0 && flush == FlushType.None;
					if (flag3)
					{
						break;
					}
					bool flag4 = this.lookahead == 0;
					if (flag4)
					{
						goto Block_5;
					}
				}
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num2 = this.block_start + num;
				bool flag5 = this.strstart == 0 || this.strstart >= num2;
				if (flag5)
				{
					this.lookahead = this.strstart - num2;
					this.strstart = num2;
					this.flush_block_only(false);
					bool flag6 = this._codec.AvailableBytesOut == 0;
					if (flag6)
					{
						goto Block_8;
					}
				}
				bool flag7 = this.strstart - this.block_start >= this.w_size - DeflateManager.MIN_LOOKAHEAD;
				if (flag7)
				{
					this.flush_block_only(false);
					bool flag8 = this._codec.AvailableBytesOut == 0;
					if (flag8)
					{
						goto Block_10;
					}
				}
			}
			return BlockState.NeedMore;
			Block_5:
			this.flush_block_only(flush == FlushType.Finish);
			bool flag9 = this._codec.AvailableBytesOut == 0;
			if (flag9)
			{
				return (flush == FlushType.Finish) ? BlockState.FinishStarted : BlockState.NeedMore;
			}
			return (flush == FlushType.Finish) ? BlockState.FinishDone : BlockState.BlockDone;
			Block_8:
			return BlockState.NeedMore;
			Block_10:
			return BlockState.NeedMore;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005056 File Offset: 0x00003256
		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits((DeflateManager.STORED_BLOCK << 1) + (eof ? 1 : 0), 3);
			this.copy_block(buf, stored_len, true);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000507C File Offset: 0x0000327C
		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			bool flag = this.compressionLevel > CompressionLevel.None;
			int num2;
			int num3;
			if (flag)
			{
				bool flag2 = (int)this.data_type == DeflateManager.Z_UNKNOWN;
				if (flag2)
				{
					this.set_data_type();
				}
				this.treeLiterals.build_tree(this);
				this.treeDistances.build_tree(this);
				num = this.build_bl_tree();
				num2 = this.opt_len + 3 + 7 >> 3;
				num3 = this.static_len + 3 + 7 >> 3;
				bool flag3 = num3 <= num2;
				if (flag3)
				{
					num2 = num3;
				}
			}
			else
			{
				num3 = (num2 = stored_len + 5);
			}
			bool flag4 = stored_len + 4 <= num2 && buf != -1;
			if (flag4)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else
			{
				bool flag5 = num3 == num2;
				if (flag5)
				{
					this.send_bits((DeflateManager.STATIC_TREES << 1) + (eof ? 1 : 0), 3);
					this.send_compressed_block(StaticTree.lengthAndLiteralsTreeCodes, StaticTree.distTreeCodes);
				}
				else
				{
					this.send_bits((DeflateManager.DYN_TREES << 1) + (eof ? 1 : 0), 3);
					this.send_all_trees(this.treeLiterals.max_code + 1, this.treeDistances.max_code + 1, num + 1);
					this.send_compressed_block(this.dyn_ltree, this.dyn_dtree);
				}
			}
			this._InitializeBlocks();
			if (eof)
			{
				this.bi_windup();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000051D0 File Offset: 0x000033D0
		private void _fillWindow()
		{
			do
			{
				int num = this.window_size - this.lookahead - this.strstart;
				bool flag = num == 0 && this.strstart == 0 && this.lookahead == 0;
				int num2;
				if (flag)
				{
					num = this.w_size;
				}
				else
				{
					bool flag2 = num == -1;
					if (flag2)
					{
						num--;
					}
					else
					{
						bool flag3 = this.strstart >= this.w_size + this.w_size - DeflateManager.MIN_LOOKAHEAD;
						if (flag3)
						{
							Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
							this.match_start -= this.w_size;
							this.strstart -= this.w_size;
							this.block_start -= this.w_size;
							num2 = this.hash_size;
							int num3 = num2;
							do
							{
								int num4 = (int)this.head[--num3] & 65535;
								this.head[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
							}
							while (--num2 != 0);
							num2 = this.w_size;
							num3 = num2;
							do
							{
								int num4 = (int)this.prev[--num3] & 65535;
								this.prev[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
							}
							while (--num2 != 0);
							num += this.w_size;
						}
					}
				}
				bool flag4 = this._codec.AvailableBytesIn == 0;
				if (flag4)
				{
					break;
				}
				num2 = this._codec.read_buf(this.window, this.strstart + this.lookahead, num);
				this.lookahead += num2;
				bool flag5 = this.lookahead >= DeflateManager.MIN_MATCH;
				if (flag5)
				{
					this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask;
				}
			}
			while (this.lookahead < DeflateManager.MIN_LOOKAHEAD && this._codec.AvailableBytesIn != 0);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005424 File Offset: 0x00003624
		internal BlockState DeflateFast(FlushType flush)
		{
			int num = 0;
			for (;;)
			{
				bool flag = this.lookahead < DeflateManager.MIN_LOOKAHEAD;
				if (flag)
				{
					this._fillWindow();
					bool flag2 = this.lookahead < DeflateManager.MIN_LOOKAHEAD && flush == FlushType.None;
					if (flag2)
					{
						break;
					}
					bool flag3 = this.lookahead == 0;
					if (flag3)
					{
						goto Block_4;
					}
				}
				bool flag4 = this.lookahead >= DeflateManager.MIN_MATCH;
				if (flag4)
				{
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
					num = (int)this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				bool flag5 = (long)num != 0L && ((this.strstart - num) & 65535) <= this.w_size - DeflateManager.MIN_LOOKAHEAD;
				if (flag5)
				{
					bool flag6 = this.compressionStrategy != CompressionStrategy.HuffmanOnly;
					if (flag6)
					{
						this.match_length = this.longest_match(num);
					}
				}
				bool flag7 = this.match_length >= DeflateManager.MIN_MATCH;
				bool flag8;
				if (flag7)
				{
					flag8 = this._tr_tally(this.strstart - this.match_start, this.match_length - DeflateManager.MIN_MATCH);
					this.lookahead -= this.match_length;
					bool flag9 = this.match_length <= this.config.MaxLazy && this.lookahead >= DeflateManager.MIN_MATCH;
					if (flag9)
					{
						this.match_length--;
						int num2;
						do
						{
							this.strstart++;
							this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
							num = (int)this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
							num2 = this.match_length - 1;
							this.match_length = num2;
						}
						while (num2 != 0);
						this.strstart++;
					}
					else
					{
						this.strstart += this.match_length;
						this.match_length = 0;
						this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
						this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask;
					}
				}
				else
				{
					flag8 = this._tr_tally(0, (int)(this.window[this.strstart] & byte.MaxValue));
					this.lookahead--;
					this.strstart++;
				}
				bool flag10 = flag8;
				if (flag10)
				{
					this.flush_block_only(false);
					bool flag11 = this._codec.AvailableBytesOut == 0;
					if (flag11)
					{
						goto Block_14;
					}
				}
			}
			return BlockState.NeedMore;
			Block_4:
			this.flush_block_only(flush == FlushType.Finish);
			bool flag12 = this._codec.AvailableBytesOut == 0;
			if (!flag12)
			{
				return (flush == FlushType.Finish) ? BlockState.FinishDone : BlockState.BlockDone;
			}
			bool flag13 = flush == FlushType.Finish;
			if (flag13)
			{
				return BlockState.FinishStarted;
			}
			return BlockState.NeedMore;
			Block_14:
			return BlockState.NeedMore;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000057DC File Offset: 0x000039DC
		internal BlockState DeflateSlow(FlushType flush)
		{
			int num = 0;
			for (;;)
			{
				bool flag = this.lookahead < DeflateManager.MIN_LOOKAHEAD;
				if (flag)
				{
					this._fillWindow();
					bool flag2 = this.lookahead < DeflateManager.MIN_LOOKAHEAD && flush == FlushType.None;
					if (flag2)
					{
						break;
					}
					bool flag3 = this.lookahead == 0;
					if (flag3)
					{
						goto Block_4;
					}
				}
				bool flag4 = this.lookahead >= DeflateManager.MIN_MATCH;
				if (flag4)
				{
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
					num = (int)this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = DeflateManager.MIN_MATCH - 1;
				bool flag5 = num != 0 && this.prev_length < this.config.MaxLazy && ((this.strstart - num) & 65535) <= this.w_size - DeflateManager.MIN_LOOKAHEAD;
				if (flag5)
				{
					bool flag6 = this.compressionStrategy != CompressionStrategy.HuffmanOnly;
					if (flag6)
					{
						this.match_length = this.longest_match(num);
					}
					bool flag7 = this.match_length <= 5 && (this.compressionStrategy == CompressionStrategy.Filtered || (this.match_length == DeflateManager.MIN_MATCH && this.strstart - this.match_start > 4096));
					if (flag7)
					{
						this.match_length = DeflateManager.MIN_MATCH - 1;
					}
				}
				bool flag8 = this.prev_length >= DeflateManager.MIN_MATCH && this.match_length <= this.prev_length;
				if (flag8)
				{
					int num2 = this.strstart + this.lookahead - DeflateManager.MIN_MATCH;
					bool flag9 = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - DeflateManager.MIN_MATCH);
					this.lookahead -= this.prev_length - 1;
					this.prev_length -= 2;
					int num3;
					do
					{
						num3 = this.strstart + 1;
						this.strstart = num3;
						bool flag10 = num3 <= num2;
						if (flag10)
						{
							this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
							num = (int)this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						num3 = this.prev_length - 1;
						this.prev_length = num3;
					}
					while (num3 != 0);
					this.match_available = 0;
					this.match_length = DeflateManager.MIN_MATCH - 1;
					this.strstart++;
					bool flag11 = flag9;
					if (flag11)
					{
						this.flush_block_only(false);
						bool flag12 = this._codec.AvailableBytesOut == 0;
						if (flag12)
						{
							goto Block_19;
						}
					}
				}
				else
				{
					bool flag13 = this.match_available != 0;
					if (flag13)
					{
						bool flag9 = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
						bool flag14 = flag9;
						if (flag14)
						{
							this.flush_block_only(false);
						}
						this.strstart++;
						this.lookahead--;
						bool flag15 = this._codec.AvailableBytesOut == 0;
						if (flag15)
						{
							goto Block_22;
						}
					}
					else
					{
						this.match_available = 1;
						this.strstart++;
						this.lookahead--;
					}
				}
			}
			return BlockState.NeedMore;
			Block_4:
			bool flag16 = this.match_available != 0;
			if (flag16)
			{
				bool flag9 = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
				this.match_available = 0;
			}
			this.flush_block_only(flush == FlushType.Finish);
			bool flag17 = this._codec.AvailableBytesOut == 0;
			if (!flag17)
			{
				return (flush == FlushType.Finish) ? BlockState.FinishDone : BlockState.BlockDone;
			}
			bool flag18 = flush == FlushType.Finish;
			if (flag18)
			{
				return BlockState.FinishStarted;
			}
			return BlockState.NeedMore;
			Block_19:
			return BlockState.NeedMore;
			Block_22:
			return BlockState.NeedMore;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005C78 File Offset: 0x00003E78
		internal int longest_match(int cur_match)
		{
			int num = this.config.MaxChainLength;
			int num2 = this.strstart;
			int num3 = this.prev_length;
			int num4 = ((this.strstart > this.w_size - DeflateManager.MIN_LOOKAHEAD) ? (this.strstart - (this.w_size - DeflateManager.MIN_LOOKAHEAD)) : 0);
			int niceLength = this.config.NiceLength;
			int num5 = this.w_mask;
			int num6 = this.strstart + DeflateManager.MAX_MATCH;
			byte b = this.window[num2 + num3 - 1];
			byte b2 = this.window[num2 + num3];
			bool flag = this.prev_length >= this.config.GoodLength;
			if (flag)
			{
				num >>= 2;
			}
			bool flag2 = niceLength > this.lookahead;
			if (flag2)
			{
				niceLength = this.lookahead;
			}
			do
			{
				int num7 = cur_match;
				bool flag3 = this.window[num7 + num3] != b2 || this.window[num7 + num3 - 1] != b || this.window[num7] != this.window[num2] || this.window[++num7] != this.window[num2 + 1];
				if (!flag3)
				{
					num2 += 2;
					num7++;
					while (this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && num2 < num6)
					{
					}
					int num8 = DeflateManager.MAX_MATCH - (num6 - num2);
					num2 = num6 - DeflateManager.MAX_MATCH;
					bool flag4 = num8 > num3;
					if (flag4)
					{
						this.match_start = cur_match;
						num3 = num8;
						bool flag5 = num8 >= niceLength;
						if (flag5)
						{
							break;
						}
						b = this.window[num2 + num3 - 1];
						b2 = this.window[num2 + num3];
					}
				}
			}
			while ((cur_match = (int)this.prev[cur_match & num5] & 65535) > num4 && --num != 0);
			bool flag6 = num3 <= this.lookahead;
			int num9;
			if (flag6)
			{
				num9 = num3;
			}
			else
			{
				num9 = this.lookahead;
			}
			return num9;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005F44 File Offset: 0x00004144
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00005F5C File Offset: 0x0000415C
		internal bool WantRfc1950HeaderBytes
		{
			get
			{
				return this._WantRfc1950HeaderBytes;
			}
			set
			{
				this._WantRfc1950HeaderBytes = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005F68 File Offset: 0x00004168
		internal int Initialize(ZlibCodec codec, CompressionLevel level)
		{
			return this.Initialize(codec, level, 15);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005F84 File Offset: 0x00004184
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits)
		{
			return this.Initialize(codec, level, bits, DeflateManager.MEM_LEVEL_DEFAULT, CompressionStrategy.Default);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005FA8 File Offset: 0x000041A8
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits, CompressionStrategy compressionStrategy)
		{
			return this.Initialize(codec, level, bits, DeflateManager.MEM_LEVEL_DEFAULT, compressionStrategy);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005FCC File Offset: 0x000041CC
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int windowBits, int memLevel, CompressionStrategy strategy)
		{
			this._codec = codec;
			this._codec.Message = null;
			bool flag = windowBits < 9 || windowBits > 15;
			if (flag)
			{
				throw new ZlibException("windowBits must be in the range 9..15.");
			}
			bool flag2 = memLevel < 1 || memLevel > DeflateManager.MEM_LEVEL_MAX;
			if (flag2)
			{
				throw new ZlibException(string.Format("memLevel must be in the range 1.. {0}", DeflateManager.MEM_LEVEL_MAX));
			}
			this._codec.dstate = this;
			this.w_bits = windowBits;
			this.w_size = 1 << this.w_bits;
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << this.hash_bits;
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + DeflateManager.MIN_MATCH - 1) / DeflateManager.MIN_MATCH;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << memLevel + 6;
			this.pending = new byte[this.lit_bufsize * 4];
			this._distanceOffset = this.lit_bufsize;
			this._lengthOffset = 3 * this.lit_bufsize;
			this.compressionLevel = level;
			this.compressionStrategy = strategy;
			this.Reset();
			return 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000613C File Offset: 0x0000433C
		internal void Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.pendingCount = 0;
			this.nextPending = 0;
			this.Rfc1950BytesEmitted = false;
			this.status = (this.WantRfc1950HeaderBytes ? DeflateManager.INIT_STATE : DeflateManager.BUSY_STATE);
			this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			this.last_flush = 0;
			this._InitializeTreeData();
			this._InitializeLazyMatch();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000061CC File Offset: 0x000043CC
		internal int End()
		{
			bool flag = this.status != DeflateManager.INIT_STATE && this.status != DeflateManager.BUSY_STATE && this.status != DeflateManager.FINISH_STATE;
			int num;
			if (flag)
			{
				num = -2;
			}
			else
			{
				this.pending = null;
				this.head = null;
				this.prev = null;
				this.window = null;
				num = ((this.status == DeflateManager.BUSY_STATE) ? (-3) : 0);
			}
			return num;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006244 File Offset: 0x00004444
		private void SetDeflater()
		{
			switch (this.config.Flavor)
			{
			case DeflateFlavor.Store:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateNone);
				break;
			case DeflateFlavor.Fast:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateFast);
				break;
			case DeflateFlavor.Slow:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateSlow);
				break;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000062B0 File Offset: 0x000044B0
		internal int SetParams(CompressionLevel level, CompressionStrategy strategy)
		{
			int num = 0;
			bool flag = this.compressionLevel != level;
			if (flag)
			{
				DeflateManager.Config config = DeflateManager.Config.Lookup(level);
				bool flag2 = config.Flavor != this.config.Flavor && this._codec.TotalBytesIn != 0L;
				if (flag2)
				{
					num = this._codec.Deflate(FlushType.Partial);
				}
				this.compressionLevel = level;
				this.config = config;
				this.SetDeflater();
			}
			this.compressionStrategy = strategy;
			return num;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006334 File Offset: 0x00004534
		internal int SetDictionary(byte[] dictionary)
		{
			int num = dictionary.Length;
			int num2 = 0;
			bool flag = dictionary == null || this.status != DeflateManager.INIT_STATE;
			if (flag)
			{
				throw new ZlibException("Stream error.");
			}
			this._codec._Adler32 = Adler.Adler32(this._codec._Adler32, dictionary, 0, dictionary.Length);
			bool flag2 = num < DeflateManager.MIN_MATCH;
			int num3;
			if (flag2)
			{
				num3 = 0;
			}
			else
			{
				bool flag3 = num > this.w_size - DeflateManager.MIN_LOOKAHEAD;
				if (flag3)
				{
					num = this.w_size - DeflateManager.MIN_LOOKAHEAD;
					num2 = dictionary.Length - num;
				}
				Array.Copy(dictionary, num2, this.window, 0, num);
				this.strstart = num;
				this.block_start = num;
				this.ins_h = (int)(this.window[0] & byte.MaxValue);
				this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[1] & byte.MaxValue)) & this.hash_mask;
				for (int i = 0; i <= num - DeflateManager.MIN_MATCH; i++)
				{
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[i + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
					this.prev[i & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)i;
				}
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000064B4 File Offset: 0x000046B4
		internal int Deflate(FlushType flush)
		{
			bool flag = this._codec.OutputBuffer == null || (this._codec.InputBuffer == null && this._codec.AvailableBytesIn != 0) || (this.status == DeflateManager.FINISH_STATE && flush != FlushType.Finish);
			if (flag)
			{
				this._codec.Message = DeflateManager._ErrorMessage[4];
				throw new ZlibException(string.Format("Something is fishy. [{0}]", this._codec.Message));
			}
			bool flag2 = this._codec.AvailableBytesOut == 0;
			if (flag2)
			{
				this._codec.Message = DeflateManager._ErrorMessage[7];
				throw new ZlibException("OutputBuffer is full (AvailableBytesOut == 0)");
			}
			int num = this.last_flush;
			this.last_flush = (int)flush;
			bool flag3 = this.status == DeflateManager.INIT_STATE;
			if (flag3)
			{
				int num2 = DeflateManager.Z_DEFLATED + (this.w_bits - 8 << 4) << 8;
				int num3 = ((this.compressionLevel - CompressionLevel.BestSpeed) & 255) >> 1;
				bool flag4 = num3 > 3;
				if (flag4)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				bool flag5 = this.strstart != 0;
				if (flag5)
				{
					num2 |= DeflateManager.PRESET_DICT;
				}
				num2 += 31 - num2 % 31;
				this.status = DeflateManager.BUSY_STATE;
				byte[] array = this.pending;
				int num4 = this.pendingCount;
				this.pendingCount = num4 + 1;
				array[num4] = (byte)(num2 >> 8);
				byte[] array2 = this.pending;
				num4 = this.pendingCount;
				this.pendingCount = num4 + 1;
				array2[num4] = (byte)num2;
				bool flag6 = this.strstart != 0;
				if (flag6)
				{
					byte[] array3 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array3[num4] = (byte)((this._codec._Adler32 & 4278190080U) >> 24);
					byte[] array4 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array4[num4] = (byte)((this._codec._Adler32 & 16711680U) >> 16);
					byte[] array5 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array5[num4] = (byte)((this._codec._Adler32 & 65280U) >> 8);
					byte[] array6 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array6[num4] = (byte)(this._codec._Adler32 & 255U);
				}
				this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			}
			bool flag7 = this.pendingCount != 0;
			if (flag7)
			{
				this._codec.flush_pending();
				bool flag8 = this._codec.AvailableBytesOut == 0;
				if (flag8)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else
			{
				bool flag9 = this._codec.AvailableBytesIn == 0 && flush <= (FlushType)num && flush != FlushType.Finish;
				if (flag9)
				{
					return 0;
				}
			}
			bool flag10 = this.status == DeflateManager.FINISH_STATE && this._codec.AvailableBytesIn != 0;
			if (flag10)
			{
				this._codec.Message = DeflateManager._ErrorMessage[7];
				throw new ZlibException("status == FINISH_STATE && _codec.AvailableBytesIn != 0");
			}
			bool flag11 = this._codec.AvailableBytesIn != 0 || this.lookahead != 0 || (flush != FlushType.None && this.status != DeflateManager.FINISH_STATE);
			if (flag11)
			{
				BlockState blockState = this.DeflateFunction(flush);
				bool flag12 = blockState == BlockState.FinishStarted || blockState == BlockState.FinishDone;
				if (flag12)
				{
					this.status = DeflateManager.FINISH_STATE;
				}
				bool flag13 = blockState == BlockState.NeedMore || blockState == BlockState.FinishStarted;
				if (flag13)
				{
					bool flag14 = this._codec.AvailableBytesOut == 0;
					if (flag14)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				bool flag15 = blockState == BlockState.BlockDone;
				if (flag15)
				{
					bool flag16 = flush == FlushType.Partial;
					if (flag16)
					{
						this._tr_align();
					}
					else
					{
						this._tr_stored_block(0, 0, false);
						bool flag17 = flush == FlushType.Full;
						if (flag17)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					this._codec.flush_pending();
					bool flag18 = this._codec.AvailableBytesOut == 0;
					if (flag18)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			bool flag19 = flush != FlushType.Finish;
			int num5;
			if (flag19)
			{
				num5 = 0;
			}
			else
			{
				bool flag20 = !this.WantRfc1950HeaderBytes || this.Rfc1950BytesEmitted;
				if (flag20)
				{
					num5 = 1;
				}
				else
				{
					byte[] array7 = this.pending;
					int num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array7[num4] = (byte)((this._codec._Adler32 & 4278190080U) >> 24);
					byte[] array8 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array8[num4] = (byte)((this._codec._Adler32 & 16711680U) >> 16);
					byte[] array9 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array9[num4] = (byte)((this._codec._Adler32 & 65280U) >> 8);
					byte[] array10 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array10[num4] = (byte)(this._codec._Adler32 & 255U);
					this._codec.flush_pending();
					this.Rfc1950BytesEmitted = true;
					num5 = ((this.pendingCount != 0) ? 0 : 1);
				}
			}
			return num5;
		}

		// Token: 0x04000030 RID: 48
		private static readonly int MEM_LEVEL_MAX = 9;

		// Token: 0x04000031 RID: 49
		private static readonly int MEM_LEVEL_DEFAULT = 8;

		// Token: 0x04000032 RID: 50
		private DeflateManager.CompressFunc DeflateFunction;

		// Token: 0x04000033 RID: 51
		private static readonly string[] _ErrorMessage = new string[] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };

		// Token: 0x04000034 RID: 52
		private static readonly int PRESET_DICT = 32;

		// Token: 0x04000035 RID: 53
		private static readonly int INIT_STATE = 42;

		// Token: 0x04000036 RID: 54
		private static readonly int BUSY_STATE = 113;

		// Token: 0x04000037 RID: 55
		private static readonly int FINISH_STATE = 666;

		// Token: 0x04000038 RID: 56
		private static readonly int Z_DEFLATED = 8;

		// Token: 0x04000039 RID: 57
		private static readonly int STORED_BLOCK = 0;

		// Token: 0x0400003A RID: 58
		private static readonly int STATIC_TREES = 1;

		// Token: 0x0400003B RID: 59
		private static readonly int DYN_TREES = 2;

		// Token: 0x0400003C RID: 60
		private static readonly int Z_BINARY = 0;

		// Token: 0x0400003D RID: 61
		private static readonly int Z_ASCII = 1;

		// Token: 0x0400003E RID: 62
		private static readonly int Z_UNKNOWN = 2;

		// Token: 0x0400003F RID: 63
		private static readonly int Buf_size = 16;

		// Token: 0x04000040 RID: 64
		private static readonly int MIN_MATCH = 3;

		// Token: 0x04000041 RID: 65
		private static readonly int MAX_MATCH = 258;

		// Token: 0x04000042 RID: 66
		private static readonly int MIN_LOOKAHEAD = DeflateManager.MAX_MATCH + DeflateManager.MIN_MATCH + 1;

		// Token: 0x04000043 RID: 67
		private static readonly int HEAP_SIZE = 2 * InternalConstants.L_CODES + 1;

		// Token: 0x04000044 RID: 68
		private static readonly int END_BLOCK = 256;

		// Token: 0x04000045 RID: 69
		internal ZlibCodec _codec;

		// Token: 0x04000046 RID: 70
		internal int status;

		// Token: 0x04000047 RID: 71
		internal byte[] pending;

		// Token: 0x04000048 RID: 72
		internal int nextPending;

		// Token: 0x04000049 RID: 73
		internal int pendingCount;

		// Token: 0x0400004A RID: 74
		internal sbyte data_type;

		// Token: 0x0400004B RID: 75
		internal int last_flush;

		// Token: 0x0400004C RID: 76
		internal int w_size;

		// Token: 0x0400004D RID: 77
		internal int w_bits;

		// Token: 0x0400004E RID: 78
		internal int w_mask;

		// Token: 0x0400004F RID: 79
		internal byte[] window;

		// Token: 0x04000050 RID: 80
		internal int window_size;

		// Token: 0x04000051 RID: 81
		internal short[] prev;

		// Token: 0x04000052 RID: 82
		internal short[] head;

		// Token: 0x04000053 RID: 83
		internal int ins_h;

		// Token: 0x04000054 RID: 84
		internal int hash_size;

		// Token: 0x04000055 RID: 85
		internal int hash_bits;

		// Token: 0x04000056 RID: 86
		internal int hash_mask;

		// Token: 0x04000057 RID: 87
		internal int hash_shift;

		// Token: 0x04000058 RID: 88
		internal int block_start;

		// Token: 0x04000059 RID: 89
		private DeflateManager.Config config;

		// Token: 0x0400005A RID: 90
		internal int match_length;

		// Token: 0x0400005B RID: 91
		internal int prev_match;

		// Token: 0x0400005C RID: 92
		internal int match_available;

		// Token: 0x0400005D RID: 93
		internal int strstart;

		// Token: 0x0400005E RID: 94
		internal int match_start;

		// Token: 0x0400005F RID: 95
		internal int lookahead;

		// Token: 0x04000060 RID: 96
		internal int prev_length;

		// Token: 0x04000061 RID: 97
		internal CompressionLevel compressionLevel;

		// Token: 0x04000062 RID: 98
		internal CompressionStrategy compressionStrategy;

		// Token: 0x04000063 RID: 99
		internal short[] dyn_ltree;

		// Token: 0x04000064 RID: 100
		internal short[] dyn_dtree;

		// Token: 0x04000065 RID: 101
		internal short[] bl_tree;

		// Token: 0x04000066 RID: 102
		internal Tree treeLiterals = new Tree();

		// Token: 0x04000067 RID: 103
		internal Tree treeDistances = new Tree();

		// Token: 0x04000068 RID: 104
		internal Tree treeBitLengths = new Tree();

		// Token: 0x04000069 RID: 105
		internal short[] bl_count = new short[InternalConstants.MAX_BITS + 1];

		// Token: 0x0400006A RID: 106
		internal int[] heap = new int[2 * InternalConstants.L_CODES + 1];

		// Token: 0x0400006B RID: 107
		internal int heap_len;

		// Token: 0x0400006C RID: 108
		internal int heap_max;

		// Token: 0x0400006D RID: 109
		internal sbyte[] depth = new sbyte[2 * InternalConstants.L_CODES + 1];

		// Token: 0x0400006E RID: 110
		internal int _lengthOffset;

		// Token: 0x0400006F RID: 111
		internal int lit_bufsize;

		// Token: 0x04000070 RID: 112
		internal int last_lit;

		// Token: 0x04000071 RID: 113
		internal int _distanceOffset;

		// Token: 0x04000072 RID: 114
		internal int opt_len;

		// Token: 0x04000073 RID: 115
		internal int static_len;

		// Token: 0x04000074 RID: 116
		internal int matches;

		// Token: 0x04000075 RID: 117
		internal int last_eob_len;

		// Token: 0x04000076 RID: 118
		internal short bi_buf;

		// Token: 0x04000077 RID: 119
		internal int bi_valid;

		// Token: 0x04000078 RID: 120
		private bool Rfc1950BytesEmitted = false;

		// Token: 0x04000079 RID: 121
		private bool _WantRfc1950HeaderBytes = true;

		// Token: 0x020001FA RID: 506
		// (Invoke) Token: 0x06001BEC RID: 7148
		internal delegate BlockState CompressFunc(FlushType flush);

		// Token: 0x020001FB RID: 507
		internal class Config
		{
			// Token: 0x06001BEF RID: 7151 RVA: 0x000B1608 File Offset: 0x000AF808
			private Config(int goodLength, int maxLazy, int niceLength, int maxChainLength, DeflateFlavor flavor)
			{
				this.GoodLength = goodLength;
				this.MaxLazy = maxLazy;
				this.NiceLength = niceLength;
				this.MaxChainLength = maxChainLength;
				this.Flavor = flavor;
			}

			// Token: 0x06001BF0 RID: 7152 RVA: 0x000B1638 File Offset: 0x000AF838
			public static DeflateManager.Config Lookup(CompressionLevel level)
			{
				return DeflateManager.Config.Table[(int)level];
			}

			// Token: 0x04000D59 RID: 3417
			internal int GoodLength;

			// Token: 0x04000D5A RID: 3418
			internal int MaxLazy;

			// Token: 0x04000D5B RID: 3419
			internal int NiceLength;

			// Token: 0x04000D5C RID: 3420
			internal int MaxChainLength;

			// Token: 0x04000D5D RID: 3421
			internal DeflateFlavor Flavor;

			// Token: 0x04000D5E RID: 3422
			private static readonly DeflateManager.Config[] Table = new DeflateManager.Config[]
			{
				new DeflateManager.Config(0, 0, 0, 0, DeflateFlavor.Store),
				new DeflateManager.Config(4, 4, 8, 4, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 5, 16, 8, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 6, 32, 32, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 4, 16, 16, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 16, 32, 32, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 16, 128, 128, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 32, 128, 256, DeflateFlavor.Slow),
				new DeflateManager.Config(32, 128, 258, 1024, DeflateFlavor.Slow),
				new DeflateManager.Config(32, 258, 258, 4096, DeflateFlavor.Slow)
			};
		}
	}
}
