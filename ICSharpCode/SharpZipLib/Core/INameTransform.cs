using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A2 RID: 162
	public interface INameTransform
	{
		// Token: 0x0600073C RID: 1852
		string TransformFile(string name);

		// Token: 0x0600073D RID: 1853
		string TransformDirectory(string name);
	}
}
