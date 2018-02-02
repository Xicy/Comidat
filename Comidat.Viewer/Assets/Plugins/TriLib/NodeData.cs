using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000023 RID: 35
	public class NodeData : IDisposable
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00006DB5 File Offset: 0x00004FB5
		public void Dispose()
		{
			this.ParentNodeData = null;
			this.GameObject = null;
			this.Name = null;
			this.Path = null;
		}

		// Token: 0x040000D3 RID: 211
		public GameObject GameObject;

		// Token: 0x040000D4 RID: 212
		public string Name;

		// Token: 0x040000D5 RID: 213
		public IntPtr Node;

		// Token: 0x040000D6 RID: 214
		public NodeData ParentNodeData;

		// Token: 0x040000D7 RID: 215
		public string Path;

		// Token: 0x040000D8 RID: 216
		public int Id;
	}
}
