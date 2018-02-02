using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000024 RID: 36
	public class MeshData : IDisposable
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00006DDB File Offset: 0x00004FDB
		public void Dispose()
		{
			this.UnityMesh = null;
			this.SkinnedMeshRenderer = null;
		}

		// Token: 0x040000D9 RID: 217
		public SkinnedMeshRenderer SkinnedMeshRenderer;

		// Token: 0x040000DA RID: 218
		public Mesh UnityMesh;
	}
}
