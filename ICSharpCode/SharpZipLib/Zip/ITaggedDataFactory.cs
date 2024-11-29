using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200006A RID: 106
	internal interface ITaggedDataFactory
	{
		// Token: 0x06000502 RID: 1282
		ITaggedData Create(short tag, byte[] data, int offset, int count);
	}
}
